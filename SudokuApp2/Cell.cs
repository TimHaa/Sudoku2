using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuApp2
{
    class Cell
    {
        public int HighestPossible { get; private set; }
        public List<int> Candidates { get; set; }
        public bool DoesContainNr { get; set; }
        public int ContainedNr { get; set; }
        public int XPos { get; set; }
        public int YPos { get; set; }
        public int IndexOfQuad { get; set; }

        public Cell(int xCoordInBoard, int yCoordInBoard, int highestPossibleCandidate)
        {
            HighestPossible = highestPossibleCandidate;
            Candidates = new List<int>();
            Candidates = FillCandidates();
            DoesContainNr = false;
            IndexOfQuad = GetIndexOfQuadrant();
            XPos = xCoordInBoard;
            YPos = yCoordInBoard;
        }

        public List<int> FillCandidates()
        {
            List<int> allPossibleCandidates = new List<int>();
            for (int i = 1; i <= HighestPossible; i++) { allPossibleCandidates.Add(i); }
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
