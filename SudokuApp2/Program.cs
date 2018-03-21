using System;
using System.Linq;
using System.Collections.Generic;

namespace SudokuApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            //TODO: CleanUp
            //TODO: Add CheckForLockedTriangle
            SudokuSolver s = new SudokuSolver();
            s.Solve();
            

            
            Console.ReadLine();
        }

    }
}
