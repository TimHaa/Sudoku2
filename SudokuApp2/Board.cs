using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SudokuApp2
{
    class Board
    {
        public const int SizeX = 9;
        public const int SizeY = 9;
        public const int SizeByQuadIndex = 9;
        public Cell[,] cells = new Cell[SizeX, SizeY];
        public Row[] rows = new Row[SizeY];
        public Col[] cols = new Col[SizeX];
        public Quadrant[] quadrants = new Quadrant[SizeByQuadIndex];
        public Board()
        {
            cells = FillCells();
            cols = FillCols();
            rows = FillRows();
            quadrants = FillQuadrants();
        }

        private Cell[,] FillCells()
        {
            Cell[,] boardInCells = new Cell[Board.SizeX, Board.SizeY];
            for (int i = 0; i < Board.SizeX; i++)
            {
                for (int j = 0; j < Board.SizeY; j++)
                {
                    boardInCells[i, j] = new Cell(i, j);
                }
            }
            return boardInCells;
        }

        public void RemoveCandidates(int candidate, int xCoord, int yCoord)
        {
            for (int i = 0; i < Row.Size; i++)
            {
                this.rows[yCoord].cells[i].RemoveCandidate(candidate);
            }
            for (int i = 0; i < Col.Size; i++)
            {
                this.cols[xCoord].cells[i].RemoveCandidate(candidate);
            }
            for (int i = 0; i < Quadrant.Size; i++)
            {
                this.quadrants[this.cells[xCoord, yCoord].GetIndexOfQuadrant()].cells[i].RemoveCandidate(candidate);
            }
        }

        private Row[] FillRows()
        {
            Row[] boardInRows = new Row[Board.SizeY];
            for (int y = 0; y < Board.SizeY; y++)
            {
                boardInRows[y] = new Row(y);//warum ist das nötig?
                boardInRows[y].SetCells(this);
            }
            return boardInRows;
        }

        public Col[] FillCols()
        {
            Col[] boardInCols = new Col[Board.SizeX];
            for (int x = 0; x < SizeX; x++)
            {
                boardInCols[x] = new Col(x);
                boardInCols[x].SetCells(this);
            }
            return boardInCols;
        }
        
        private Quadrant[] FillQuadrants()
        {
            Quadrant[] newBoard = new Quadrant[Board.SizeByQuadIndex];
            for (int i = 0; i < Board.SizeByQuadIndex; i++)
            {
                newBoard[i] = new Quadrant(i);
                newBoard[i].SetCells(this);
            }
            return newBoard;
        }

        public void Print()
        {
            for (int y = 0; y < Board.SizeY; y++)
            {
                for (int x = 0; x < Board.SizeX; x++)
                {
                    cells[x, y].PrintNr();
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void PrintByRows()
        {
            for (int y = 0; y < Board.SizeY; y++)
            {
                for (int i = 0; i < Board.SizeX; i++)
                {
                    rows[y].cells[i].PrintNr();
                }
                Console.WriteLine();
            }
        }

        public void PrintByCols()
        {
            for (int i = 0; i < Board.SizeY; i++)
            {
                for (int x = 0; x < Board.SizeY; x++)
                {
                    cols[x].cells[i].PrintNr();
                }
                Console.WriteLine();
            }
        }

        public void PrintByQuads()
        {
            for (int i = 0; i < SizeByQuadIndex; i++)
            {
                for (int j = 0; j < Quadrant.Size; j++)
                {
                    this.quadrants[i].cells[j].PrintNr();
                }
                Console.WriteLine();
            }
        }

        public void PrintCandidates()
        {
            for (int y = 0; y < Board.SizeY; y++)
            {
                for (int x = 0; x < Board.SizeX; x++)
                {
                    this.cells[x, y].PrintCandidates();
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
