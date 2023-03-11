using System;
using BlockRelated;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CellRelated
{
    public class CellGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject cellPrefab;
        [SerializeField] private GameObject border;
        [SerializeField] private BlockSo[] blockData; 

        [SerializeField] private int horizontalLength;
        [SerializeField] private int verticalLength;

        private float _mainOffset;
        [SerializeField] private float offset;
        
        private CellController _cellController;

        private void Awake()
        {
            _cellController = FindObjectOfType<CellController>();
            _mainOffset = transform.localScale.x;
        }

        private void Start()
        {
            GenerateCells();
        }

        public void GenerateCells()
        {
            var cells = new Cell[verticalLength][];
            for (var index = 0; index < verticalLength; index++)
            {
                cells[index] = new Cell[horizontalLength];
            }

            for (var i = 0; i < verticalLength; i++)
            {
                for (var j = 0; j < horizontalLength; j++)
                {
                    var cellObject = Instantiate(cellPrefab, transform, true);
                    var spawnPosition = new Vector3(i * _mainOffset + offset * i, (j * _mainOffset + offset * j) * -1f);
                    cellObject.transform.localPosition = spawnPosition;

                    var cell = cellObject.GetComponent<Cell>();
                    var block = GetRandomBlockData();
                    cell.SetBlock(block);

                    cells[i][j] = cell;
                }
            }
            
            GenerateBorders();
            
            _cellController.SetCells(cells);
        }

        private BlockSo GetRandomBlockData()
        {
            return blockData[Random.Range(0, blockData.Length)];
        }

        private void GenerateBorders()
        {
            var leftBorder = Instantiate(border, transform, true);
            leftBorder.transform.localPosition = new Vector3(-_mainOffset, 0f, 0f);

            var rightBorder = Instantiate(border, transform, true);
            rightBorder.transform.localPosition = new Vector3((horizontalLength) * _mainOffset, 0f, 0f);

            var topBorder = Instantiate(border, transform, true);
            topBorder.transform.localPosition = new Vector3(0f, _mainOffset, 0f);
            topBorder.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
            
            var bottomBorder = Instantiate(border, transform, true);
            bottomBorder.transform.localPosition = new Vector3(0f, (verticalLength) * -_mainOffset, 0f);
            bottomBorder.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
        }
    }
}
