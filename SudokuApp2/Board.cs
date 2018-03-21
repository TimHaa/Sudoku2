using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SudokuApp2
{
    class Board
    {
        public const int sizeX = 9;
        public const int sizeY = 9;
        public const int sizeByQuadIndex = 9;
        public Cell[,] cells = new Cell[sizeX, sizeY];
        public Row[] rows = new Row[sizeY];
        public Col[] cols = new Col[sizeX];
        public Quadrant[] quadrants = new Quadrant[sizeByQuadIndex];
        public Board()
        {
            cells = FillCells();
            cols = FillCols();
            rows = FillRows();
            quadrants = FillQuadrants();
        }

        private Cell[,] FillCells()
        {
            Cell[,] boardInCells = new Cell[Board.sizeX, Board.sizeY];
            for (int i = 0; i < Board.sizeX; i++)
            {
                for (int j = 0; j < Board.sizeY; j++)
                {
                    boardInCells[i, j] = new Cell(i, j);
                }
            }
            return boardInCells;
        }

        public void RemoveCandidates(int candidate, int xCoord, int yCoord)//schöner mit enumerator
        {//TODO Remove Candidates in Row, Col und Quad aufteilen für CheckIfCellsOfNrHaveSameRow()/Col/Quad
            for (int i = 0; i < Row.size; i++)
            {
                this.rows[yCoord].cells[i].RemoveCandidate(candidate);
            }
            for (int i = 0; i < Col.size; i++)
            {
                this.cols[xCoord].cells[i].RemoveCandidate(candidate);
            }
            for (int i = 0; i < Quadrant.size; i++)
            {
                this.quadrants[this.cells[xCoord, yCoord].GetIndexOfQuadrant()].cells[i].RemoveCandidate(candidate);
            }
        }

        private Row[] FillRows()
        {
            Row[] boardInRows = new Row[Board.sizeY];
            for (int y = 0; y < Board.sizeY; y++)
            {
                boardInRows[y] = new Row(y);//warum ist das nötig?
                boardInRows[y].SetCells(this);
            }
            return boardInRows;
        }

        public Col[] FillCols()
        {
            Col[] boardInCols = new Col[Board.sizeX];
            for (int x = 0; x < sizeX; x++)
            {
                boardInCols[x] = new Col(x);
                boardInCols[x].SetCells(this);
            }
            return boardInCols;
        }
        
        private Quadrant[] FillQuadrants()
        {
            Quadrant[] newBoard = new Quadrant[Board.sizeByQuadIndex];
            for (int i = 0; i < Board.sizeByQuadIndex; i++)
            {
                newBoard[i] = new Quadrant(i);
                newBoard[i].SetCells(this);
            }
            return newBoard;
        }

        public void Print()
        {
            for (int y = 0; y < Board.sizeY; y++)
            {
                for (int x = 0; x < Board.sizeX; x++)
                {
                    cells[x, y].PrintNr();
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void PrintByRows()
        {
            for (int y = 0; y < Board.sizeY; y++)
            {
                for (int i = 0; i < Board.sizeX; i++)
                {
                    rows[y].cells[i].PrintNr();
                }
                Console.WriteLine();
            }
        }

        public void PrintByCols()
        {
            for (int i = 0; i < Board.sizeY; i++)
            {
                for (int x = 0; x < Board.sizeY; x++)
                {
                    cols[x].cells[i].PrintNr();
                }
                Console.WriteLine();
            }
        }

        public void PrintByQuads()
        {
            for (int i = 0; i < sizeByQuadIndex; i++)
            {
                for (int j = 0; j < Quadrant.size; j++)
                {
                    this.quadrants[i].cells[j].PrintNr();
                }
                Console.WriteLine();
            }
        }

        public void PrintCandidates()
        {
            for (int y = 0; y < Board.sizeY; y++)
            {
                for (int x = 0; x < Board.sizeX; x++)
                {
                    this.cells[x, y].PrintCandidates();
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
