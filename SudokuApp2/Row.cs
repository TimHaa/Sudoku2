﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SudokuApp2
{
    class Row
    {
        public const int Size = Board.SizeY;
        public int YPos;
        public Cell[] cells = new Cell[9];

        public Row(int yCoordInBoard)
        {
            YPos = yCoordInBoard;
        }

        public void SetCells(Board targetBoard)
        {
            for (int i = 0; i < Row.Size; i++)
            {
                this.cells[i] = targetBoard.cells[i, YPos];
            }
        }

        public void ComputeClonedCells()
        {
            for (int x = 0; x < Board.SizeX; x++)
            {
                Cell currentCell = cells[x];
                List<int> cloneList = new List<int>();
                int cloneCount = CountCellClones(currentCell, cloneList);
                if (IsCandidateCountEqualCloneCount(currentCell, cloneCount))
                {
                    RemoveLockedCandidates(currentCell.Candidates, cloneList);
                }
            }
        }
        public int CountCellClones(Cell currCell, List<int> cloneList)
        {
            int count = 0;
            for (int i = 0; i < Row.Size; i++)
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
            for (int i = 0; i < Row.Size; i++)
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
                cells[indexOfCell].Candidates.Remove(nr);
            }
        }
        public bool AreClones(Cell input1, Cell input2)
        {
            bool noDiff = true;
            if (input1.Candidates.Count != 0 && input2.Candidates.Count != 0)
            {
                foreach (int candidate in input2.Candidates)
                {
                    if (!input1.Candidates.Contains(candidate))
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
        public bool IsCandidateCountEqualCloneCount(Cell input, int coupleCount)
        {
            return (input.Candidates.Count == coupleCount && coupleCount != 0);
        }
        public bool IsClone(List<int> cloneList, int index)
        {
            return cloneList.Contains(index);
        }


       
        
        public void ComputeClonedNums()
        {
            string[] possibleCellsPerNum = GetPossibleCellsPerNum();
            for (int num = 0; num < Cell.HighestPossible; num++)
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
            string[] possibleCellsPerNum = new string[Cell.HighestPossible];
            for (int nr = 0; nr < Cell.HighestPossible; nr++)
            {
                for (int x = 0; x < Row.Size; x++)
                {
                    if (IsCellPossible(nr, cells[x]))
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
            for (int n = 0; n < Cell.HighestPossible; n++)
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
            for (int x = 0; x < Row.Size; x++)
            {
                if (!exceptions.Contains(Convert.ToChar(x + '0')))
                {
                    cells[x].RemoveCandidate(numToRemove + 1);
                }
            }
        }
        public void CleanLockedCells(string possibleCells, List<int> cloneList)
        {
            for (int n = 0; n <= Cell.HighestPossible; n++)
            {
                if (!cloneList.Contains(n))
                {
                    RemoveCandidateIn(n, possibleCells);
                }
            }
        }
        public void RemoveCandidateIn(int numToRemove, string positions)
        {
            for (int x = 0; x < Row.Size; x++)
            {
                if (positions.Contains(x.ToString()))
                {
                    cells[x].RemoveCandidate(numToRemove + 1);
                }
            }
        }
    }
}
