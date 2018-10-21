using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SudokuApp2
{
    class Col: CellGroup
    {
        public int XPos { get; set; }

        public Col(int xCoordInBoard, int size) : base(xCoordInBoard, size)
        {
            Cells = new Cell[Size];
        }
        
        public override void SetCells(Board targetBoard)
        {
            for (int i = 0; i < Size; i++)
            {
                this.Cells[i] = targetBoard.Cells[XPos, i];
            }
        }
    }
}
