using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SudokuApp2
{
    class Quadrant:CellGroup
    {
        public int Index { get; set; }
        public int XPos { get; set; }
        public int YPos { get; set; }
        public string XRange { get; set; }
        public string YRange { get; set; }

        public Quadrant(int indexOfQuadrant, int sideLength)//:base{}
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
        public override void SetCells(Board targetBoard)
        {
            for (int j = 0; j < Size; j++)
            {
                int xPosCell = ConvertCellIndexToXInBoard(j);
                int yPosCell = ConvertCellIndexToYInBoard(j);
                this.Cells[j] = targetBoard.Cells[xPosCell, yPosCell];
            }
        }
        public void ComputeClonedNums(Board targetBoard)//overloads inherited method
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
