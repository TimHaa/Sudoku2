using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SudokuApp2
{
    class Quadrant
    {
        public const int size = Board.sizeByQuadIndex;
        public int index;
        public int xPos;
        public int yPos;
        public string xRange;
        public string yRange;
        public Cell[] cells = new Cell[Quadrant.size];

        public Quadrant(int indexOfQuadrant)
        {
            index = indexOfQuadrant;
            xPos = ConvertIndexToXCoord(indexOfQuadrant);
            yPos = ConvertIndexToYCoord(indexOfQuadrant);
            xRange = GetXRange();
            yRange = GetYRange();
        }
        private string GetXRange()
        {
            if (xPos == 0) { return "012"; }
            if (xPos == 1) { return "345"; }
            if (xPos == 2) { return "678"; }
            else { return "false Input"; }
        }
        private string GetYRange()
        {
            if (yPos == 0) { return "012"; }
            if (yPos == 1) { return "345"; }
            if (yPos == 2) { return "678"; }
            else { return "false Input"; }
        }
        public int ConvertIndexToXCoord(int index)
        {
            int xCoordInQuad = index % 3;
            return xCoordInQuad;
        }
        public int ConvertIndexToYCoord(int index)
        {
            int yCoordInQuad = index / 3;
            return yCoordInQuad;
        }
        public void Print()
        {
            for (int i = 0; i < Quadrant.size; i++)
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
            for (int j = 0; j < Quadrant.size; j++)
            {
                int xPosCell = ConvertCellIndexToXInBoard(j);
                int yPosCell = ConvertCellIndexToYInBoard(j);
                this.cells[j] = targetBoard.cells[xPosCell, yPosCell];
            }
        }

        public void ComputeClonedCells()
        {
            for (int i = 0; i < Board.sizeByQuadIndex; i++)
            {
                Cell currentCell = cells[i];
                List<int> cloneList = new List<int>();
                int cloneCount = CountCellClones(currentCell, cloneList);
                if (IsCandidateCountEqualCloneCount(currentCell, cloneCount))
                {
                    RemoveLockedCandidates(currentCell.candidates, cloneList);
                }
            }
        }
        public int CountCellClones(Cell currCell, List<int> cloneList)
        {
            int count = 0;
            for (int i = 0; i < Quadrant.size; i++)
            {
                Cell iteratingCell = cells[i];
                if (AreClones(currCell, iteratingCell))
                {
                    count++;
                    cloneList.Add(i);
                }

            }
            return count;
        }
        public void RemoveLockedCandidates(List<int> candidatesToRemove, List<int> cloneList)
        {
            for (int i = 0; i < Quadrant.size; i++)
            {
                if (!IsClone(cloneList, i))
                {
                    RemoveCandidates(candidatesToRemove, i);
                }
            }
        }
        public void RemoveCandidates(List<int> candidatesToRemove, int indexOfCell)
        {
            foreach (int nr in candidatesToRemove)
            {
                cells[indexOfCell].candidates.Remove(nr);
            }
        }
        public bool AreClones(Cell input1, Cell input2)
        {
            bool noDiff = true;
            if (input1.candidates.Count != 0 && input2.candidates.Count != 0)
            {
                foreach (int candidate in input2.candidates)
                {
                    if (!input1.candidates.Contains(candidate))
                    {
                        noDiff = false;
                        break;
                    }
                }
            }
            else
            { noDiff = false; }
            return noDiff;
            //return (input1.candidates.SequenceEqual(input2.candidates));
        }
        public bool IsCandidateCountEqualCloneCount(Cell input, int coupleCount)
        {
            return (input.candidates.Count == coupleCount && coupleCount != 0);
        }
        public bool IsClone(List<int> cloneList, int index)
        {
            return cloneList.Contains(index);
        }



        public void ComputeClonedNums(Board targetBoard)
        {
            string[] possibleCellsPerNum = GetPossibleCellsPerNum();
            for (int num = 0; num < Cell.highestPossible; num++)
            {
                string possibleCells = possibleCellsPerNum[num];
                List<int> cloneList = new List<int>();
                int cloneCount = CountNumClones(num, possibleCellsPerNum, cloneList);
                if (possibleCells != null && cloneCount == possibleCells.Length)
                {
                    CleanLockedCells(possibleCells, cloneList);
                }
            }
            CheckForCellsWithAlignedCoords(possibleCellsPerNum, targetBoard);
        }
        public string[] GetPossibleCellsPerNum()
        {
            string[] possibleCellsPerNum = new string[Cell.highestPossible];
            //for (int i = 0; i < 9; i++) { possibleCellsPerNum[i] = ""; }//doesnt work with possibleCells != "" above
            for (int nr = 0; nr < Cell.highestPossible; nr++)
            {
                for (int y = 0; y < Quadrant.size; y++)
                {
                    if (IsCellPossible(nr, this.cells[y]))
                    {
                        possibleCellsPerNum[nr] += y.ToString();
                    }
                }
            }
            return possibleCellsPerNum;
        }
        public bool IsCellPossible(int nr, Cell currCell)
        {
            nr++;
            return currCell.candidates.Contains(nr);
        }

        public int CountNumClones(int currNum, string[] possibleCells, List<int> cloneList)
        {
            int count = 0;
            for (int n = 0; n < Cell.highestPossible; n++)
            {
                if (IsString2ContainedIn1(possibleCells[currNum], possibleCells[n]))
                {
                    count++;
                    cloneList.Add(n);
                }
            }
            return count;
        }
        public bool IsString2ContainedIn1(string containingString, string input)
        {
            bool isContained = true;
            if (input != null && containingString != null)
            {
                foreach (char c in input)
                {
                    if (!containingString.Contains(c))
                    {
                        isContained = false;
                    }
                }
            }
            else
            {
                isContained = false;
            }
            return isContained;
        }

        
        public void RemoveCandidateExcept(int numToRemove, string exceptions)
        {
            for (int y = 0; y < Quadrant.size; y++)
            {
                if (!exceptions.Contains(Convert.ToChar(y + '2')))
                {
                    cells[y].RemoveCandidate(numToRemove + 1);
                }
            }
        }
        public void CleanLockedCells(string possibleCells, List<int> cloneList)
        {
            for (int n = 0; n <= Cell.highestPossible; n++)
            {
                if (!cloneList.Contains(n))
                {
                    RemoveCandidateIn(n, possibleCells);
                }
            }
        }
        public void RemoveCandidateIn(int numToRemove, string positions)
        {
            for (int y = 0; y < Quadrant.size; y++)
            {
                if (positions.Contains(y.ToString()))
                {
                    cells[y].RemoveCandidate(numToRemove + 1);
                }
            }
        }

        public void CheckForCellsWithAlignedCoords(string[] cellsPerNum, Board board)
        {
            for (int num = 0; num < 9; num++)
            {
                if (cellsPerNum[num] != "" && cellsPerNum[num] != null)
                {
                    int y = 0;
                    int x = 0;
                    if (AreSameRow(cellsPerNum[num], ref y))
                    {
                        board.rows[y].RemoveCandidateExcept(num, xRange);
                        //Console.WriteLine("annihilated " + (num + 1) + " in row " + y + "(except" + xRange + ") because " + cellsPerNum[num] + " quadrant " + this.index);
                        //board.PrintCandidates();
                    }
                    else if (AreSameCol(cellsPerNum[num], ref x))
                    {
                        board.cols[x].RemoveCandidateExcept(num, yRange);
                        //Console.WriteLine("annihilated " + (num + 1) + "in col " + x+ "(except" + yRange + ") because " + cellsPerNum[num] + " quadrant " + this.index);
                        //board.PrintCandidates();
                    }
                }
            }
        }
        public bool AreSameRow(string cellsOfNum, ref int y)
        {
            bool sameRow = true;
            foreach (char c in cellsOfNum)
            {
                if (IsRowDiff(c, cellsOfNum[0])) { sameRow = false; }
            }
            if (sameRow) { y = Convert.ToInt16(cellsOfNum[0] - '0') / 3 + yPos * 3; }
            return sameRow;
        }
        private bool IsRowDiff(char c, char first)
        {
            if (0 <= Convert.ToInt16(c - '0') && Convert.ToInt16(c - '0') < 9 && 0 <= Convert.ToInt16(first - '0') && Convert.ToInt16(first - '0') < 9)
            {
                return cells[(Convert.ToInt16(c - '0'))].yPos != cells[(Convert.ToInt16(first - '0'))].yPos;

            }
            else { Console.WriteLine("false input"); return true; }
        }
        public bool AreSameCol(string cellsOfNum, ref int x)
        {
            bool sameCol = true;
            foreach (char c in cellsOfNum)
            {
                if (IsColDiff(c, cellsOfNum[0])) { sameCol = false; }
            }
            if (sameCol) { x = Convert.ToInt16(cellsOfNum[0] - '0')%3 + xPos * 3; }
            return sameCol;
        }
        private bool IsColDiff(char c, char first)
        {
            if (0 <= Convert.ToInt16(c - '0') && Convert.ToInt16(c - '0') < 9 && 0 <= Convert.ToInt16(first - '0') && Convert.ToInt16(first - '0') < 9)
            {
                return cells[(Convert.ToInt16(c - '0'))].xPos != cells[(Convert.ToInt16(first - '0'))].xPos;

            }
            else { Console.WriteLine("false input"); return true; }
        }
    }
}
