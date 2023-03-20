using System;
using System.Collections.Generic;
using System.Linq;
using BlockRelated;
using Controllers;
using UnityEngine;

namespace CellRelated
{
    public class CellController : MonoBehaviour
    {
        private Cell[,] _cells;

        public void SetCells(Cell[,] cells)
        {
            _cells = cells;
        }

        public void OnChange(Cell firstCell, Cell secondCell, bool verticalMove)
        {
            if (verticalMove)
            {
                LookupForward(0, GetRow(firstCell.GetVerticalIndex()));
                LookupForward(0, GetRow(secondCell.GetVerticalIndex()));

                var minVerticalIndex = Math.Min(firstCell.GetVerticalIndex(), secondCell.GetVerticalIndex());
                var maxVerticalIndex = Math.Max(firstCell.GetVerticalIndex(), secondCell.GetVerticalIndex());
                
                LookupForward(maxVerticalIndex, GetColumn(firstCell.GetHorizontalIndex()));
                LookupBackward(minVerticalIndex, GetColumn(firstCell.GetHorizontalIndex()));
            }
            else
            {
                LookupForward(0, GetColumn(firstCell.GetHorizontalIndex()));
                LookupForward(0, GetColumn(secondCell.GetHorizontalIndex()));
                
                var minHorizontalIndex = Math.Min(firstCell.GetHorizontalIndex(), secondCell.GetHorizontalIndex());
                var maxHorizontalIndex = Math.Max(firstCell.GetHorizontalIndex(), secondCell.GetHorizontalIndex());
                
                LookupForward(minHorizontalIndex, GetRow(firstCell.GetVerticalIndex()));
                LookupBackward(maxHorizontalIndex, GetRow(firstCell.GetVerticalIndex()));
            }
        }

        private void LookupForward(int startIndex, Cell[] cell)
        {
            var blockList = new HashSet<Block>();

            for (var i = startIndex; i < cell.Length - 1; i++)
            {
                var canFinish = CheckForConsecutiveBlocks(blockList, cell[i], cell[i + 1]);

                if (canFinish)
                {
                    break;
                }
            }

            if (blockList.Count >= 3)
            {
                HandleBlocks(blockList);
            }
        }
        
        private void LookupBackward(int startIndex, Cell[] cell)
        {
            var blockList = new HashSet<Block>();

            for (var i = startIndex; i > 0; i--)
            {
                var canFinish = CheckForConsecutiveBlocks(blockList, cell[i], cell[i - 1]);

                if (canFinish)
                {
                    break;
                }
            }

            if (blockList.Count >= 3)
            {
                HandleBlocks(blockList);
            }
        }

        private void HandleBlocks(HashSet<Block> blockList)
        {
            EffectManager.Instance.PlayHypeEffect();
            AudioManager.Instance.PlayMatchSound();

            var isHorizontalMatch = IsHorizontalMatch(blockList);

            if (isHorizontalMatch)
            {
                HandleHorizontalMatch(blockList);
            }
            else
            {
                HandleVerticalMatch(blockList);
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

                    return false;
                }
               
                if (blockList.Count >= 3)
                {
                    return true;
                }

                blockList.Clear();
                return false;
            }
            
            if (blockList.Count >= 3)
            {
                return true;
            }

            blockList.Clear();
            return false;
        }

        private Cell[] GetColumn(int index)
        {
            return Enumerable.Range(0, _cells.GetLength(0))
                .Select(x => _cells[x, index])
                .ToArray();
        }
        
        private Cell[] GetRow(int index)
        {
            return Enumerable.Range(0, _cells.GetLength(1))
                .Select(x => _cells[index, x])
                .ToArray();
        }

        private Block HandleHorizontalMatch(HashSet<Block> blockList)
        {
            foreach (var block in blockList)
            {
                var cell = block.GetCell();
                var index = cell.GetVerticalIndex();

                while (index != 0)
                {
                    var upperCell = MoveUpperCell(index, cell);

                    if (upperCell)
                    {
                        cell = upperCell;
                    }

                    index--;
                }

                var position = block.transform.position + Vector3.back;
                EffectManager.Instance.PlayBlockEffect(position);
                
                block.gameObject.SetActive(false);
            }

            return blockList.Last();
        }

        private Block HandleVerticalMatch(HashSet<Block> blockList)
        {
            var sorted = blockList.OrderByDescending(block => block.GetCell().GetVerticalIndex()).ToList();

            foreach (var block in sorted)
            {
                var cell = block.GetCell();
                var index = cell.GetVerticalIndex() - blockList.Count + 1;

                if (index > 0)
                {
                    MoveUpperCell(index, cell);
                }
                
                var position = block.transform.position + Vector3.back;
                EffectManager.Instance.PlayBlockEffect(position);
                
                block.gameObject.SetActive(false);
            }

            return sorted.Last();
        }

        private bool IsHorizontalMatch(HashSet<Block> blockList)
        {
            return blockList.First().GetCell().GetVerticalIndex() == blockList.Last().GetCell().GetVerticalIndex();
        }

        private Cell MoveUpperCell(int index, Cell cell)
        {
            var upperCell = _cells[index - 1, cell.GetHorizontalIndex()];

            if (upperCell)
            {
                var newBlock = upperCell.GetBlock();
                        
                newBlock.ChangeCellTo(cell, this);
            }

            return upperCell;
        }
    }
}
