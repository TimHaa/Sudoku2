using System;
using System.Linq;
using System.Collections.Generic;

namespace SudokuApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            //TODO Add abstract class for Row, Col and Quadrant to inherit from
            //TODO Add CheckForLockedTriangle
            SudokuSolver s = new SudokuSolver();
            s.Solve();

            Console.ReadLine();
        }

    }
}
