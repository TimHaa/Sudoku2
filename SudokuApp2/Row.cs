using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SudokuApp2
{
    class Row
    {
        public const int size = 9;
        public int yPos;
        public Cell[] cells = new Cell[9];

        public Row(int yCoordInBoard)
        {
            yPos = yCoordInBoard;
        }

        public void SetCells(Board targetBoard)
        {
            for (int i = 0; i < Row.size; i++)
            {
                this.cells[i] = targetBoard.cells[i, yPos];
            }
        }

        public void EvaluateClonedCells()
        {
            for (int x = 0; x < Board.sizeX; x++)
            {
                Cell currentCell = cells[x];
                List<int> cloneList = new List<int>();
                int cloneCount = CountClones(currentCell, cloneList);
                if (IsCandidateCountEqualCloneCount(currentCell, cloneCount))
                {
                    RemoveLockedClonesFromCandidates(currentCell.candidates, cloneList, x);
                }
            }
        }
        public int CountClones(Cell currCell, List<int> cloneList)
        {
            int count = 0;
            for (int i = 0; i < Row.size; i++)
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
        public void RemoveLockedClonesFromCandidates(List<int> candidatesToRemove, List<int> cloneList, int position)
        {
            for (int i = 0; i < Row.size; i++)
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
            return (input1.candidates.SequenceEqual(input2.candidates));
        }
        public bool IsCandidateCountEqualCloneCount(Cell input, int coupleCount)
        {
            return (input.candidates.Count == coupleCount && coupleCount != 0);
        }
        public bool IsClone(List<int> cloneList, int index)
        {
            return cloneList.Contains(index);
        }
    }
}
