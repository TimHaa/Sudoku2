using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuApp2
{
    class SudokuSolver
    {
        public SudokuSolver() { }

        public void Solve()
        {
            int i = 0;
            Board sudokuBoard = GetInput();
            while (IsEmptyCellLeft(sudokuBoard) && i < 5)//TODO remove second condition later
            {
                CheckForSolutions(sudokuBoard);
                CheckForSharedCandidates(sudokuBoard);
                sudokuBoard.PrintByCells();
                sudokuBoard.PrintCandidates();
                i++;
            }
        }

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

        public bool IsEmptyCellLeft(Board boardInput)
        {
            bool isZeroFound = false;
            for (int y = 0; y < Board.sizeY; y++)
            {
                for (int x = 0; x < Board.sizeX; x++)
                {
                    Cell currCell = boardInput.cells[x, y];
                    if (!currCell.doesContainNr) { isZeroFound = true; }
                }
            }
            return isZeroFound;
        }

        public void CheckForSolutions(Board currentBoard)
        {
            for (int y = 0; y < Board.sizeY; y++)
            {
                for (int x = 0; x < Board.sizeX; x++)
                {
                    Cell currCell = currentBoard.cells[x, y];
                    if (currCell.candidates.Count == 1) { currCell.FillIn(currCell.candidates[0], currentBoard); }
                }
            }
        }
        public void CheckForSharedCandidates(Board board)//leserlichkeit wichtiger als kompaktheit?
        {
            for (int y = 0; y < Board.sizeY; y++)
            {
                board.rows[y].EvaluateClonedCells();
            }
            for (int x = 0; x < Board.sizeX; x++)
            {
                board.cols[x].EvaluateClonedCells();
            }
        }

        
        
    }
}
