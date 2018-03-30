using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SudokuApp2
{
    class Row
    {
        private int Size;
        public int YPos { get; set; }
        public Cell[] Cells { get; set; }

        public Row(int yCoordInBoard, int sizeX)
        {
            Size = sizeX;
            YPos = yCoordInBoard;
            Cells = new Cell[9];
        }

        public void SetCells(Board targetBoard)
        {
            for (int i = 0; i < Size; i++)
            {
                Cells[i] = targetBoard.Cells[i, YPos];
            }
        }


        public void ComputeClonedCells()
        {
            for (int x = 0; x < Size; x++)
            {
                Cell currentCell = Cells[x];
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
        public bool AreClones(List<int> input1Candidates, List<int> input2Candidates)
        {
            bool noDiff = true;
            if (input1Candidates.Count != 0 && input2Candidates.Count != 0)
            {
                foreach (int candidate in input2Candidates)
                {
                    if (!input1Candidates.Contains(candidate))
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


       
        
        public void ComputeClonedNums()
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
        }
        public string[] GetPossibleCellsPerNum()
        {
            string[] possibleCellsPerNum = new string[Cells[0].HighestPossible];
            for (int nr = 0; nr < Cells[0].HighestPossible; nr++)
            {
                for (int x = 0; x < Size; x++)
                {
                    if (IsCellPossible(nr, Cells[x]))
                    {
                        possibleCellsPerNum[nr] += x.ToString();
                    }
                }
            }
            return possibleCellsPerNum;
        }
        public bool IsCellPossible(int nr, Cell currCell)
        {
            return currCell.Candidates.Contains(nr + 1);
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
            for (int x = 0; x < Size; x++)
            {
                if (!exceptions.Contains(Convert.ToChar(x + '0')))
                {
                    Cells[x].RemoveCandidate(numToRemove + 1);
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
            for (int x = 0; x < Size; x++)
            {
                if (positions.Contains(x.ToString()))
                {
                    Cells[x].RemoveCandidate(numToRemove + 1);
                }
            }
        }
    }
}
