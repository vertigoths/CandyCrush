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

        public void ChangeCellTo(Cell newCell)
        {
            SetCell(newCell);
            
            transform.SetParent(newCell.transform);
            newCell.SetBlock(this);

            var to = newCell.transform.position;

            transform.DOMove(to, 0.2f)
                .SetEase(Ease.Linear);
        }
    }
}
