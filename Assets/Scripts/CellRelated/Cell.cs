using System;
using BlockRelated;
using UnityEngine;

namespace CellRelated
{
    public class Cell : MonoBehaviour
    {
        private Block _block;

        public void SetBlock(BlockSo blockData)
        {
            _block = GetComponentInChildren<Block>();
            
            _block.SetBlockData(blockData);
            _block.SetCell(this);
        }
    }
}
