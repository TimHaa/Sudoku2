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
            Board sudokuBoard = GetInput();
            sudokuBoard.PrintCandidates();

            while (IsEmptyCellLeft(sudokuBoard))
            {
                CheckForSolutions(sudokuBoard);
                LockSharedCandidates(sudokuBoard);
                LockSharedCells(sudokuBoard);
                sudokuBoard.Print();
                sudokuBoard.PrintCandidates();
            }
            sudokuBoard.Print();
        }

        public Board GetInput()
        {
            Board newBoard = new Board();
            for (int i = 0; i < Board.SizeY; i++)
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

        public string GetHardInput(int index)
        {
            string[] inputSud = new string[9];
            inputSud[0] = "020608000";
            inputSud[1] = "580009700";
            inputSud[2] = "000040000";

            inputSud[3] = "370000500";
            inputSud[4] = "600000004";
            inputSud[5] = "008000013";

            inputSud[6] = "000020000";
            inputSud[7] = "009800036";
            inputSud[8] = "000306090";
            return inputSud[index];
        }

        public string GetRowInput()
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
            for (int y = 0; y < Board.SizeY; y++)
            {
                for (int x = 0; x < Board.SizeX; x++)
                {
                    Cell currCell = boardInput.cells[x, y];
                    if (!currCell.DoesContainNr) { isZeroFound = true; }
                }
            }
            return isZeroFound;
        }



        public void CheckForSolutions(Board currentBoard)
        {
            for (int y = 0; y < Board.SizeY; y++)
            {
                for (int x = 0; x < Board.SizeX; x++)
                {
                    Cell currCell = currentBoard.cells[x, y];
                    if (currCell.Candidates.Count == 1) { currCell.FillIn(currCell.Candidates[0], currentBoard); }
                }
            }
        }
        public void LockSharedCandidates(Board board)
        {
            for (int y = 0; y < Board.SizeY; y++)
            {
                board.rows[y].ComputeClonedCells();
            }
            for (int x = 0; x < Board.SizeX; x++)
            {
                board.cols[x].ComputeClonedCells();
            }
            for (int i = 0; i < Board.SizeByQuadIndex; i++)
            {
                board.quadrants[i].ComputeClonedCells();
            }
        }
        public void LockSharedCells(Board board)
        {
            for (int y = 0; y < Board.SizeY; y++)
            {
                board.rows[y].ComputeClonedNums();
            }
            for (int x = 0; x < Board.SizeX; x++)
            {
                board.cols[x].ComputeClonedNums();
            }
            for (int i = 0; i < Board.SizeByQuadIndex; i++)
            {
                board.quadrants[i].ComputeClonedNums(board);
            }
        }
        
        
    }
}
