using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuApp2
{
    class SudokuSolver
    {
        public SudokuSolver() { }

        public Board GetInput()
        {
            Board newBoard = new Board();
            for (int i = 0; i < Board.sizeY; i++)
            {
                string currentRow = GetRowInput();
                for (int j = 0; j < currentRow.Length; j++)
                {
                    int currentNr = Convert.ToInt16(currentRow[j]) - '0';
                    newBoard.rows[i].cells[j].FillIn(currentNr, newBoard);
                }
            }
            return newBoard;
        }

        public string GetRowInput()//errorhandling
        {
            string lineInput = Console.ReadLine();
            if (lineInput.Length == 9 && IsDigitsOnly(lineInput)) { return lineInput; }
            else
            {
                Console.WriteLine("Wrong Input");
                return GetRowInput();
            }
        }

        private bool IsDigitsOnly(string input)
        {
            foreach (char c in input)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }

        

        
    }
}
