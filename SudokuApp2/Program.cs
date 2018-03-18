using System;
using System.Linq;
using System.Collections.Generic;

namespace SudokuApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            SudokuSolver s = new SudokuSolver();
            s.Solve();
            Console.ReadLine();
        }

    }
}
