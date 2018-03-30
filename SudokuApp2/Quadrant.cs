using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SudokuApp2
{
    class Quadrant
    {
        public int Size { get; private set; }
        public int Index { get; set; }
        public int XPos { get; set; }
        public int YPos { get; set; }
        public string XRange { get; set; }
        public string YRange { get; set; }
        public Cell[] Cells { get; set; }

        public Quadrant(int indexOfQuadrant, int sideLength)
        {
            Size = GetSizeOfIndex(sideLength);
            Index = indexOfQuadrant;
            XPos = ConvertIndexToXCoord(indexOfQuadrant);
            YPos = ConvertIndexToYCoord(indexOfQuadrant);
            XRange = GetXRange();
            YRange = GetYRange();
            Cells = new Cell[Size];
        }
        private int GetSizeOfIndex(int side)
        {
            return side * side;
        }

        private string GetXRange()
        {
            if (XPos == 0) { return "012"; }
            if (XPos == 1) { return "345"; }
            if (XPos == 2) { return "678"; }
            else { return "false Input"; }
        }
        private string GetYRange()
        {
            if (YPos == 0) { return "012"; }
            if (YPos == 1) { return "345"; }
            if (YPos == 2) { return "678"; }
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
            for (int i = 0; i < Size; i++)
            {
                this.Cells[i].PrintNr();
            }
            Console.WriteLine();
        }
        public int ConvertCellIndexToXInBoard(int indexOfCell)
        {
            int xCoordInBoard = indexOfCell % 3 + XPos * 3;
            return xCoordInBoard;
        }
        public int ConvertCellIndexToYInBoard(int indexOfCell)
        {
            int yCoordInBoard = indexOfCell / 3 + YPos * 3;
            return yCoordInBoard;
        }
        public void SetCells(Board targetBoard)
        {
            for (int j = 0; j < Size; j++)
            {
                int xPosCell = ConvertCellIndexToXInBoard(j);
                int yPosCell = ConvertCellIndexToYInBoard(j);
                this.Cells[j] = targetBoard.Cells[xPosCell, yPosCell];
            }
        }

        public void ComputeClonedCells()
        {
            for (int i = 0; i < Size; i++)
            {
                Cell currentCell = Cells[i];
                List<int> cloneList = new List<int>();
                int cloneCount = CountCellClones(currentCell, cloneList);
                if (IsCandidateCountEqualCloneCount(currentCell.Candidates.Count, cloneCount))
                {
                    RemoveLockedCandidates(currentCell.Candidates, cloneList);
                }
            }
        }
        public int CountCellClones(Cell currCell, List<int> cloneList)
        {
            int count = 0;
            for (int i = 0; i < Size; i++)
            {
                Cell iteratingCell = Cells[i];
                if (AreClones(currCell.Candidates, iteratingCell.Candidates))
                {
                    count++;
                    cloneList.Add(i);
                }

            }
            return count;
        }
        public void RemoveLockedCandidates(List<int> candidatesToRemove, List<int> cloneList)
        {
            for (int i = 0; i < Size; i++)
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
                Cells[indexOfCell].Candidates.Remove(nr);
            }
        }
        public bool AreClones(List<int> cell1Candidates, List<int> cell2Candidates)
        {
            bool noDiff = true;
            if (cell1Candidates.Count != 0 && cell2Candidates.Count != 0)
            {
                foreach (int candidate in cell2Candidates)
                {
                    if (!cell1Candidates.Contains(candidate))
                    {
                        noDiff = false;
                        break;
                    }
                }
            }
            else
            { noDiff = false; }
            return noDiff;
        }
        public bool IsCandidateCountEqualCloneCount(int candidateCount, int coupleCount)
        {
            return (candidateCount == coupleCount && coupleCount != 0);
        }
        public bool IsClone(List<int> cloneList, int index)
        {
            return cloneList.Contains(index);
        }



        public void ComputeClonedNums(Board targetBoard)
        {
            string[] possibleCellsPerNum = GetPossibleCellsPerNum();
            for (int num = 0; num < Cells[0].HighestPossible; num++)
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
            string[] possibleCellsPerNum = new string[Cells[0].HighestPossible];
            for (int nr = 0; nr < Cells[0].HighestPossible; nr++)
            {
                for (int y = 0; y < Size; y++)
                {
                    if (IsNrInCandidates(nr, Cells[y].Candidates))
                    {
                        possibleCellsPerNum[nr] += y.ToString();
                    }
                }
            }
            return possibleCellsPerNum;
        }
        public bool IsNrInCandidates(int nr, List<int> candidates)
        {
            nr++;
            return candidates.Contains(nr);
        }

        public int CountNumClones(int currNum, string[] possibleCells, List<int> cloneList)
        {
            int count = 0;
            for (int n = 0; n < Cells[0].HighestPossible; n++)
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
            for (int y = 0; y < Size; y++)
            {
                if (!exceptions.Contains(Convert.ToChar(y + '2')))
                {
                    Cells[y].RemoveCandidate(numToRemove + 1);
                }
            }
        }
        public void CleanLockedCells(string possibleCells, List<int> cloneList)
        {
            for (int n = 0; n <= Cells[0].HighestPossible; n++)
            {
                if (!cloneList.Contains(n))
                {
                    RemoveCandidateIn(n, possibleCells);
                }
            }
        }
        public void RemoveCandidateIn(int numToRemove, string positions)
        {
            for (int y = 0; y < Size; y++)
            {
                if (positions.Contains(y.ToString()))
                {
                    Cells[y].RemoveCandidate(numToRemove + 1);
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
                        board.Rows[y].RemoveCandidateExcept(num, XRange);
                    }
                    else if (AreSameCol(cellsPerNum[num], ref x))
                    {
                        board.Cols[x].RemoveCandidateExcept(num, YRange);
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
            if (sameRow) { y = Convert.ToInt16(cellsOfNum[0] - '0') / 3 + YPos * 3; }
            return sameRow;
        }
        private bool IsRowDiff(char c, char first)
        {
            if (0 <= Convert.ToInt16(c - '0') && Convert.ToInt16(c - '0') < 9 && 0 <= Convert.ToInt16(first - '0') && Convert.ToInt16(first - '0') < 9)
            {
                return Cells[(Convert.ToInt16(c - '0'))].YPos != Cells[(Convert.ToInt16(first - '0'))].YPos;

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
            if (sameCol) { x = Convert.ToInt16(cellsOfNum[0] - '0')%3 + XPos * 3; }
            return sameCol;
        }
        private bool IsColDiff(char c, char first)
        {
            if (0 <= Convert.ToInt16(c - '0') && Convert.ToInt16(c - '0') < 9 && 0 <= Convert.ToInt16(first - '0') && Convert.ToInt16(first - '0') < 9)
            {
                return Cells[(Convert.ToInt16(c - '0'))].XPos != Cells[(Convert.ToInt16(first - '0'))].XPos;

            }
            else { Console.WriteLine("false input"); return true; }
        }
    }
}
