using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SudokuApp2
{
    class Quadrant
    {
        public const int sizeByIndex = 9;
        public int index;
        public int xPos;
        public int yPos;
        public Cell[] cells = new Cell[Quadrant.sizeByIndex];

        public Quadrant(int indexOfQuadrant)
        {
            index = indexOfQuadrant;
            xPos = ConvertIndexToXCoord(indexOfQuadrant);
            yPos = ConvertIndexToYCoord(indexOfQuadrant);
        }

        public int ConvertIndexToXCoord(int index)
        {
            int yCoordInQuad = index % 3;
            return yCoordInQuad;
        }

        public int ConvertIndexToYCoord(int index)
        {
            int yCoordInQuad = index / 3;
            return yCoordInQuad;
        }

        public void Print()
        {
            for (int i = 0; i < Quadrant.sizeByIndex; i++)
            {
                this.cells[i].PrintNr();
            }
            Console.WriteLine();
        }

        public int ConvertCellIndexToXInBoard(int indexOfCell)
        {
            int xCoordInBoard = indexOfCell % 3 + xPos * 3;
            return xCoordInBoard;
        }

        public int ConvertCellIndexToYInBoard(int indexOfCell)
        {
            int yCoordInBoard = indexOfCell / 3 + yPos * 3;
            return yCoordInBoard;
        }

        public void SetCells(Board targetBoard)
        {
            for (int j = 0; j < Quadrant.sizeByIndex; j++)
            {
                int xPosCell = ConvertCellIndexToXInBoard(j);
                int yPosCell = ConvertCellIndexToYInBoard(j);
                this.cells[j] = targetBoard.cells[xPosCell, yPosCell];
            }
        }

        

        //public int ConvertCoordsBoardToQuad(int coordInBoard)
        //{
        //    int coordInQuad = coordInBoard - xPos;
        //    return coordInQuad;
        //}

        //public int ConvertCoordsQuadToBoard(int coordInQuad)
        //{
        //    int coordInBoard = coordInQuad - xPos;
        //    return coordInBoard;
        //}
    }
}
