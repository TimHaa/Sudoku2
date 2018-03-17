using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuApp2
{
    class Row
    {
        public const int size = 9;
        public int yPos;
        public Cell[] cells = new Cell[9];

        public Row(int yCoordInBoard)
        {
            yPos = yCoordInBoard;
        }

        public void SetCells(Board targetBoard)
        {
            for (int i = 0; i < Row.size; i++)
            {
                this.cells[i] = targetBoard.cells[i, yPos];
            }
        }

        
    }
}
