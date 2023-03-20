using System;
using CellRelated;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BlockRelated
{
    public class Block : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerUpHandler
    {
        private Cell _cell;
        private BlockSo _blockData;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetBlockData(BlockSo blockSo)
        {
            _blockData = blockSo;
            _spriteRenderer.sprite = _blockData.sprite;
        }

        public BlockSo GetBlockData()
        {
            return _blockData;
        }

        public void SetCell(Cell cell)
        {
            _cell = cell;
        }

        public Cell GetCell()
        {
            return _cell;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            BlockInteraction.Instance.Select(this);
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            BlockInteraction.Instance.Interact(this);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            BlockInteraction.Instance.DeSelect();
        }

        public void ChangeCellTo(Cell newCell, CellController cellController)
        {
            var prevCell = _cell;

            var to = newCell.transform.position;

            transform.DOMove(to, 5f)
                .SetEase(Ease.Linear)
                .SetSpeedBased(true)
                .OnComplete(() =>
                {
                    SetCell(newCell);
                    transform.SetParent(newCell.transform);
                    newCell.SetBlock(this);
                    
                    OnBlockChange(prevCell, newCell, cellController);
                });
        }

        private bool IsVerticalMove(Cell firstCell, Cell secondCell)
        {
            var verticalDiff = Mathf.Abs(firstCell.GetVerticalIndex() - secondCell.GetVerticalIndex());
            var horizontalDiff = Mathf.Abs(firstCell.GetHorizontalIndex() - secondCell.GetHorizontalIndex());

            return verticalDiff > horizontalDiff;
        }

        private void OnBlockChange(Cell firstCell, Cell secondCell, CellController cellController)
        {
            var isVertical = IsVerticalMove(firstCell, secondCell);
            
            cellController.OnChange(firstCell, secondCell, isVertical);
        }
    }
}
