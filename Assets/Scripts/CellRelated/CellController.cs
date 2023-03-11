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
    }
}
