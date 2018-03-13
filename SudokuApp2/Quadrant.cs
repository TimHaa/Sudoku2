using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuApp2
{
    class Quadrant
    {
        public int index;
        public int xPos;
        public int yPos;
        public Cell[,] cells = new Cell[3,3];

        public Quadrant(int xCoordInBoard, int yCoordInBoard)
        {
            index = GetIndexOfQuad(xCoordInBoard, yCoordInBoard);
            xPos = xCoordInBoard;
            yPos = yCoordInBoard;
            cells = FillCell();
        }

        private int GetIndexOfQuad(int xCoord, int yCoord)
        {
            int indexOfQuad = xCoord + yCoord * 3;
            return indexOfQuad;
        }

        private Cell[,] FillCell()
        {
            Cell[,] newQuadrant = new Cell[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    newQuadrant[i, j] = new Cell(this, i, j);
                }
            }
            return newQuadrant;
        }
    }
}
