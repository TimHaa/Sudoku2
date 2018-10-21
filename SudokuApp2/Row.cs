using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SudokuApp2
{
    class Row : CellGroup
    {
        public int YPos { get; set; }

        public Row(int yCoordInBoard, int size):base(size)
        {
            YPos = yCoordInBoard;
        }

        public override void SetCells(Board targetBoard)
        {
            for (int i = 0; i < Size; i++)
            {
                Cells[i] = targetBoard.Cells[i, YPos];
            }
        }
    }
}
