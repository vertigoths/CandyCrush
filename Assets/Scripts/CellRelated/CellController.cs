using System.Collections.Generic;
using BlockRelated;
using UnityEngine;

namespace CellRelated
{
    public class CellController : MonoBehaviour
    {
        private Cell[][] _cells;

        public void SetCells(Cell[][] cells)
        {
            _cells = cells;
        }

        public void OnChange(Cell firstCell, Cell secondCell, bool verticalMove)
        {
            if (verticalMove)
            {
                LookupForward(0, _cells[firstCell.GetVerticalIndex()]);
                LookupForward(0, _cells[secondCell.GetVerticalIndex()]);
            }
        }

        private void LookupForward(int startIndex, Cell[] row)
        {
            var blockList = new HashSet<Block>();

            for (var i = startIndex; i < row.Length - 1; i++)
            {
                var canFinish = CheckForConsecutiveBlocks(blockList, row[i], row[i + 1]);

                if (canFinish)
                {
                    break;
                }
            }

            HandleBlocks(blockList);
        }
        
        private void LookupBackward(int startIndex, Cell[] row)
        {
            var blockList = new HashSet<Block>();

            for (var i = startIndex; i < row.Length - 1; i++)
            {
                var canFinish = CheckForConsecutiveBlocks(blockList, row[i], row[i + 1]);

                if (canFinish)
                {
                    break;
                }
            }

            HandleBlocks(blockList);
        }

        private void HandleBlocks(HashSet<Block> blockList)
        {
            if (blockList == null) return;
            
            foreach (var block in blockList)
            {
                block.gameObject.SetActive(false);
            }
        }

        private bool CheckForConsecutiveBlocks(HashSet<Block> blockList, Cell left, Cell right)
        {
            if (left != null && right != null)
            {
                var blockFirst = left.GetBlock();
                var blockSecond = right.GetBlock();
                    
                var blockDataFirst = blockFirst.GetBlockData().blockType;
                var blockDataSecond = blockSecond.GetBlockData().blockType;

                if (blockDataFirst == blockDataSecond)
                {
                    blockList.Add(blockFirst);
                    blockList.Add(blockSecond);
                }
                else
                {
                    if (blockList.Count >= 3)
                    {
                        return true;
                    }
                        
                    blockList.Clear();
                }
            }
            else
            {
                if (blockList.Count >= 3)
                {
                    return true;
                }
                    
                blockList.Clear();
            }

            return false;
        }
    }
}
