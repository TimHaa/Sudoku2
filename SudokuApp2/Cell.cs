using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuApp2
{
    class Cell
    {
        public Quadrant containingQuad;
        public List<int> candidates = new List<int>();
        public bool doesContainNr;
        public int index;
        public int xPos;
        public int yPos;

        public Cell(Quadrant containingQuadrant, int xCoordInQuad, int yCoordInQuad)
        {
            containingQuad = containingQuadrant;
            candidates = FillCandidates();
            doesContainNr = false;
            index = GetIndexInQuad(xCoordInQuad, yCoordInQuad);
            xPos = xCoordInQuad;
            yPos = yCoordInQuad;
        }

        public List<int> FillCandidates()
        {
            List<int> allPossibleCandidates = new List<int>();
            for (int i = 1; i <= 9; i++) { allPossibleCandidates.Add(i); }
            return allPossibleCandidates;
        }
        
        public int GetIndexInQuad(int xCoordinate, int yCoordinate)
        {
            int indexInQuadrant = xCoordinate + yCoordinate * 3;
            return indexInQuadrant;
        }

        public int GetIndexOfContainingQuad(int xPos, int yPos)
        {
            return containingQuad.index;
        }
    }
}
