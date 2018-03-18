using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuApp2
{
    class Cell
    {
        public const int highestPossible = 9;
        public List<int> candidates = new List<int>();
        public bool doesContainNr;
        public int containedNr;
        public int xPos;
        public int yPos;
        public int indexOfQuad;

        public Cell(int xCoordInBoard, int yCoordInBoard)
        {
            candidates = FillCandidates();
            doesContainNr = false;
            indexOfQuad = GetIndexOfQuadrant();
            xPos = xCoordInBoard;//are coordinates really important?
            yPos = yCoordInBoard;
        }

        public List<int> FillCandidates()
        {
            List<int> allPossibleCandidates = new List<int>();
            for (int i = 1; i <= Cell.highestPossible; i++) { allPossibleCandidates.Add(i); }//cell.highesPossible or just highestPossible better?
            return allPossibleCandidates;
        }

        public void PrintNr()
        {
            Console.Write(containedNr);
        }

        public void PrintCandidates()
        {
            Console.Write("{");
            for (int j = 0; j < candidates.Count; j++)
            {
                Console.Write(candidates[j]);
            }
            Console.Write("}");
        }

        public bool IsCellSolved()
        {
            return doesContainNr;
        }

        public void RemoveCandidate(int candidateToRemove)
        {
            if (candidates.Contains(candidateToRemove)) { this.candidates.Remove(candidateToRemove); }
        }

        public int GetIndexOfQuadrant()
        {
            int indexOfContainingQuadrant = xPos / 3 + yPos - (yPos % 3);
            return indexOfContainingQuadrant;
        }

        public void FillIn(int nrToFill, Board board)
        {
            if (nrToFill != 0)
            {
                this.containedNr = nrToFill;
                this.doesContainNr = true;
                board.RemoveCandidates(nrToFill, xPos, yPos);
                this.candidates = new List<int>();
            }
        }
    }
}
