using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SudokuApp2
{
    class Col
    {
        public const int size = 9;
        public int xPos;
        public Cell[] cells = new Cell[size];
        public Col(int xCoordInBoard)
        {
            xPos = xCoordInBoard;
        }
        
        public void SetCells(Board targetBoard)
        {
            for (int i = 0; i < Col.size; i++)
            {
                this.cells[i] = targetBoard.cells[xPos, i];
            }
        }
        
        public void EvaluateClonedCells()
        {
            for (int y = 0; y < Board.sizeY; y++)
            {
                Cell currentCell = cells[y];
                List<int> cloneList = new List<int>();
                int cloneCount = CountClones(currentCell, cloneList);
                if (IsCandidateCountEqualCloneCount(currentCell, cloneCount))
                {
                    RemoveLockedClonesFromCandidates(currentCell.candidates, cloneList, y);
                }
            }
        }
        public int CountClones(Cell currCell, List<int> cloneList)
        {
            int count = 0;
            if (currCell.candidates != new List<int>())
            {
                for (int i = 0; i < Col.size; i++)
                {
                    Cell iteratingCell = cells[i];
                    if (AreClones(currCell, iteratingCell))
                    {
                        count++;
                        cloneList.Add(i);
                    }
                }
            }
            return count;
        }
        public void RemoveLockedClonesFromCandidates(List<int> candidatesToRemove, List<int> cloneList, int position)
        {
            for (int i = 0; i < Col.size; i++)
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
        public bool IsClone(int index, List<int> cloneList)
        {
            return cloneList.Contains(index);
        }
    }
}
