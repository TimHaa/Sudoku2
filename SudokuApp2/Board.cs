using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuApp2
{
    class Board
    {
        public Quadrant[,] quadrants = new Quadrant[3,3];
        public Board()
        {
            quadrants = FillQuadrants();
        }
        private Quadrant[,] FillQuadrants()
        {
            Quadrant[,] newBoard = new Quadrant[3,3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    newBoard[i, j] = new Quadrant(i, j);
                }
            }
            return newBoard;
        }
    }
}
