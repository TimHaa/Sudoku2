using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SudokuApp2
{
    abstract class CellGroup
    {
        protected int Size;
        public Cell[] Cells { get; set; }

        public CellGroup(int size)
        {
            Size = size;
            Cells = new Cell[Size];
        }
        public CellGroup()
        { }

        public abstract void SetCells(Board targetBoard);



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
                if (!IsClone(i, cloneList))
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
        public bool IsClone(int index, List<int> cloneList)
        {
            return cloneList.Contains(index);
        }



        public void ComputeClonedNums()
        {
            string[] possibleCellsPerNum = GetPossibleCellsPerNum();
            for (int num = 0; num < Cells[0].HighestPossible; num++)//oder besser Boardsize
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
                for (int y = 0; y < Size; y++)
                {
                    if (IsCellPossible(nr, Cells[y]))
                    {
                        possibleCellsPerNum[nr] += y.ToString();
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
            for (int y = 0; y < Size; y++)
            {
                if (!exceptions.Contains(Convert.ToChar(y + '0')))
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
    }
}
