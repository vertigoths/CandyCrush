using System;
using BlockRelated;
using UnityEngine;

namespace CellRelated
{
    public class Cell : MonoBehaviour
    {
        private Block _block;
        private int _verticalIndex;
        private int _horizontalIndex;

        public void SetBlock(BlockSo blockData)
        {
            _block = GetComponentInChildren<Block>();

            _block.SetBlockData(blockData);
            _block.SetCell(this);
        }

        public void SetBlock(Block block)
        {
            _block = block;
        }

        public void SetIndices(int verticalIndex, int horizontalIndex)
        {
            _verticalIndex = verticalIndex;
            _horizontalIndex = horizontalIndex;
        }

        public int GetVerticalIndex()
        {
            return _verticalIndex;
        }

        public int GetHorizontalIndex()
        {
            return _horizontalIndex;
        }

        public Block GetBlock()
        {
            return _block;
        }
    }
}
