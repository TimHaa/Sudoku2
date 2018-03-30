using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuApp2
{
    class Cell
    {
        public const int HighestPossible = 9;
        public List<int> Candidates = new List<int>();
        public bool DoesContainNr;
        public int ContainedNr;
        public int XPos;
        public int YPos;
        public int IndexOfQuad;

        public Cell(int xCoordInBoard, int yCoordInBoard)
        {
            Candidates = FillCandidates();
            DoesContainNr = false;
            IndexOfQuad = GetIndexOfQuadrant();
            XPos = xCoordInBoard;
            YPos = yCoordInBoard;
        }

        public List<int> FillCandidates()
        {
            List<int> allPossibleCandidates = new List<int>();
            for (int i = 1; i <= Cell.HighestPossible; i++) { allPossibleCandidates.Add(i); }
            return allPossibleCandidates;
        }

        public void PrintNr()
        {
            Console.Write(ContainedNr);
        }

        public void PrintCandidates()
        {
            Console.Write("{");
            for (int j = 0; j < Candidates.Count; j++)
            {
                Console.Write(Candidates[j]);
            }
            Console.Write("}\t");
        }

        public bool IsCellSolved()
        {
            return DoesContainNr;
        }

        public void RemoveCandidate(int candidateToRemove)
        {
            if (Candidates.Contains(candidateToRemove))
            {
                this.Candidates.Remove(candidateToRemove);
            }
        }

        public int GetIndexOfQuadrant()
        {
            int indexOfContainingQuadrant = XPos / 3 + YPos - (YPos % 3);
            return indexOfContainingQuadrant;
        }

        public void FillIn(int nrToFill, Board board)
        {
            if (nrToFill != 0)
            {
                this.ContainedNr = nrToFill;
                this.DoesContainNr = true;
                board.RemoveCandidates(nrToFill, XPos, YPos);
                this.Candidates = new List<int>();
            }
        }
    }
}
