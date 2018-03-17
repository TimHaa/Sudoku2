using System;

using System.Collections.Generic;

namespace SudokuApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            SudokuSolver s = new SudokuSolver();
            Board b = s.GetInput();
            Console.WriteLine();
            b.PrintByCols();
            Console.WriteLine();
            b.PrintCandidates();
            Console.ReadLine();
        }

    }
}
