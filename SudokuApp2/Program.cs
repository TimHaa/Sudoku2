using System;

namespace SudokuApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Board b = new Board();
            for(int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.WriteLine(b.quadrants[i,j].cells[0, 0].candidates[0]);
                }
            }
            Console.ReadLine();
        }
    }
}
