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
            int i = 0;
            while (IsEmptyCellLeft(sudokuBoard)&& i < 6)
            {
                CheckForSolutions(sudokuBoard);
                LockSharedCandidates(sudokuBoard);
                LockSharedCells(sudokuBoard);
                sudokuBoard.Print();
                sudokuBoard.PrintCandidates();
                i++;
            }
            sudokuBoard.Print();
        }

        public Board GetInput()
        {
            Board newBoard = new Board(9, 3);
            for (int i = 0; i < newBoard.Size; i++)
            {
                string currentRow = GetRowInput(newBoard.Size);
                for (int j = 0; j < currentRow.Length; j++)
                {
                    int currentNr = Convert.ToInt16(currentRow[j]) - '0';
                    newBoard.Rows[i].Cells[j].FillIn(currentNr, newBoard);
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

        public string GetRowInput(int lineLength)
        {
            string lineInput = Console.ReadLine();
            if (lineInput.Length == lineLength && IsDigitsOnly(lineInput)) { return lineInput; }
            else
            {
                Console.WriteLine("Wrong Input");
                return GetRowInput(lineLength);
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
            for (int y = 0; y < boardInput.Size; y++)
            {
                for (int x = 0; x < boardInput.Size; x++)
                {
                    Cell currCell = boardInput.Cells[x, y];
                    if (!currCell.DoesContainNr) { isZeroFound = true; }
                }
            }
            return isZeroFound;
        }



        public void CheckForSolutions(Board currentBoard)
        {
            for (int y = 0; y < currentBoard.Size; y++)
            {
                for (int x = 0; x < currentBoard.Size; x++)
                {
                    Cell currCell = currentBoard.Cells[x, y];
                    if (currCell.Candidates.Count == 1) { currCell.FillIn(currCell.Candidates[0], currentBoard); }
                }
            }
        }
        public void LockSharedCandidates(Board board)
        {
            for (int y = 0; y < board.Size; y++)
            {
                board.Rows[y].ComputeClonedCells();
            }
            for (int x = 0; x < board.Size; x++)
            {
                board.Cols[x].ComputeClonedCells();
            }
            for (int i = 0; i < board.SizeByQuadrantIndex; i++)
            {
                board.Quadrants[i].ComputeClonedCells();
            }
        }
        public void LockSharedCells(Board board)
        {
            for (int y = 0; y < board.Size; y++)
            {
                board.Rows[y].ComputeClonedNums();
            }
            for (int x = 0; x < board.Size; x++)
            {
                board.Cols[x].ComputeClonedNums();
            }
            for (int i = 0; i < board.SizeByQuadrantIndex; i++)
            {
                board.Quadrants[i].ComputeClonedNums(board);
            }
        }
        
        
    }
}
