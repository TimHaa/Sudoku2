using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SudokuApp2
{
    class Board
    {
        public int Size { get; private set; }//quadratic
        public int QuadrantSideLength { get; private set; }
        public int SizeByQuadrantIndex { get; private set; }
        public Cell[,] Cells;
        public Row[] Rows;
        public Col[] Cols;
        public Quadrant[] Quadrants;
        public Board(int size, int quadrantSize)
        {
            Size = size;
            QuadrantSideLength = quadrantSize;
            SizeByQuadrantIndex = GetSizeByQuadrantIndex();
            Cells = new Cell[Size, Size];
            Cells = FillCells();
            Cols = new Col[Size];
            Cols = FillCols();
            Rows = new Row[Size];
            Rows = FillRows();
            Quadrants = new Quadrant[SizeByQuadrantIndex];
            Quadrants = FillQuadrants();
        }

        private int GetSizeByQuadrantIndex()
        {
            int sizeInQuadrants = Size / QuadrantSideLength;
            return sizeInQuadrants * sizeInQuadrants;
        }

        private Cell[,] FillCells()
        {
            Cell[,] boardInCells = new Cell[Size, Size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    boardInCells[i, j] = new Cell(i, j, Size);
                }
            }
            return boardInCells;
        }

        public void RemoveCandidates(int candidate, int xCoord, int yCoord)
        {
            for (int i = 0; i < Size; i++)
            {
                this.Rows[yCoord].Cells[i].RemoveCandidate(candidate);
                this.Cols[xCoord].Cells[i].RemoveCandidate(candidate);
            }
            for (int i = 0; i < SizeByQuadrantIndex; i++)
            {
                this.Quadrants[this.Cells[xCoord, yCoord].GetIndexOfQuadrant()].Cells[i].RemoveCandidate(candidate);
            }
        }

        private Row[] FillRows()
        {
            Row[] boardInRows = new Row[Size];
            for (int y = 0; y < Size; y++)
            {
                boardInRows[y] = new Row(y, Size);
                boardInRows[y].SetCells(this);
            }
            return boardInRows;
        }

        public Col[] FillCols()
        {
            Col[] boardInCols = new Col[Size];
            for (int x = 0; x < Size; x++)
            {
                boardInCols[x] = new Col(x, Size);
                boardInCols[x].SetCells(this);
            }
            return boardInCols;
        }
        
        private Quadrant[] FillQuadrants()
        {
            Quadrant[] newBoard = new Quadrant[SizeByQuadrantIndex];
            for (int i = 0; i < SizeByQuadrantIndex; i++)
            {
                newBoard[i] = new Quadrant(i, QuadrantSideLength);
                newBoard[i].SetCells(this);
            }
            return newBoard;
        }

        public void Print()
        {
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    Cells[x, y].PrintNr();
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void PrintByRows()
        {
            for (int y = 0; y < Size; y++)
            {
                for (int i = 0; i < Size; i++)
                {
                    Rows[y].Cells[i].PrintNr();
                }
                Console.WriteLine();
            }
        }

        public void PrintByCols()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int x = 0; x < Size; x++)
                {
                    Cols[x].Cells[i].PrintNr();
                }
                Console.WriteLine();
            }
        }

        public void PrintByQuads()
        {
            for (int i = 0; i < SizeByQuadrantIndex; i++)
            {
                Quadrants[i].Print();
            }
        }

        public void PrintCandidates()
        {
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    this.Cells[x, y].PrintCandidates();
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
