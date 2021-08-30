using Battleship.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Output
{
    public class ConsoleWriter
    {
        public void WriteGridToConsole(Grid grid)
        {
            Console.WriteLine();
            Console.WriteLine(new string('-', 30));
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.Write("|");
                    if (grid.Squares[i, j] == null) Console.Write(" " + " ");
                    if (grid.Squares[i, j] == false) Console.Write(" " + "O");
                    if (grid.Squares[i, j] == true) Console.Write(" " + "X");
                }
                Console.Write("|");
                Console.WriteLine();
                Console.WriteLine(new string('-', 30));
            }
            Console.WriteLine("\n\n");
        }

        public void Write(string text)
        {
            Console.WriteLine(text);
        }
    }
}
