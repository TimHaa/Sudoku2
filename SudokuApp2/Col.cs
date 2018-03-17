using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuApp2
{
    class Col
    {
        public const int size = 9;
        public int xPos;
        public Cell[] cells = new Cell[size];
        public Col(int xCoordInBoard)
        {
            xPos = xCoordInBoard;
            //cells = FillCell();
        }
        
        public void SetCells(Board targetBoard)
        {
            for (int i = 0; i < Col.size; i++)
            {
                this.cells[i] = targetBoard.cells[xPos, i];
            }
        }

        //private Cell[] FillCell()
        //{
        //    Cell[] newCol = new Cell[Col.size];
        //    for (int i = 0; i < Col.size; i++)
        //    {
        //        newCol[i] = new Cell(this.xPos, i);
        //    }
        //    return newCol;
        //}
    }
}
