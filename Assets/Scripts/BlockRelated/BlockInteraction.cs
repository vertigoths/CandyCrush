using System;
using CellRelated;
using UnityEngine;

namespace BlockRelated
{
    public class BlockInteraction : MonoBehaviour
    {
        private Block _lastInteractedBlock;
        public static BlockInteraction Instance;

        private CellController _cellController;

        private void Awake()
        {
            if (Instance == null)
            {
                _cellController = FindObjectOfType<CellController>();
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this);
            }
        }

        public void Select(Block interactedBlock)
        {
            _lastInteractedBlock = interactedBlock;
        }

        public void DeSelect()
        {
            _lastInteractedBlock = null;
        }

        public void Interact(Block interactedBlock)
        {
            if (_lastInteractedBlock)
            {
                var firstCell = _lastInteractedBlock.GetCell();
                var secondCell = interactedBlock.GetCell();
                var isValid = IsValidMovement(firstCell, secondCell);

                if (isValid)
                {
                    interactedBlock.ChangeCellTo(firstCell);
                    _lastInteractedBlock.ChangeCellTo(secondCell);

                    var isVertical = IsVerticalMove(firstCell, secondCell);
                    _cellController.OnChange(firstCell, secondCell, isVertical);
                }

                _lastInteractedBlock = null;
            }
        }

        private bool IsValidMovement(Cell firstCell, Cell secondCell)
        {
            var verticalDiff = Mathf.Abs(firstCell.GetVerticalIndex() - secondCell.GetVerticalIndex());
            var horizontalDiff = Mathf.Abs(firstCell.GetHorizontalIndex() - secondCell.GetHorizontalIndex());

            return verticalDiff + horizontalDiff <= 1;
        }

        private bool IsVerticalMove(Cell firstCell, Cell secondCell)
        {
            var verticalDiff = Mathf.Abs(firstCell.GetVerticalIndex() - secondCell.GetVerticalIndex());
            var horizontalDiff = Mathf.Abs(firstCell.GetHorizontalIndex() - secondCell.GetHorizontalIndex());

            return verticalDiff > horizontalDiff;
        }
    }
}
