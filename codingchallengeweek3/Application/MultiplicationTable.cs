using System;

namespace Application
{
    public class MultiplicationTable
    {
        //Generates a mutliplication table from inputted integer.
        public void MultTable(string input)
        {
            //Validates integer input.
            int num;
            var check = Int32.TryParse(input, out num);
            if (! check) throw new FormatException(input + " is not an integer.");

            //Generates the multiplication table.
            else
            {
                for (var x = 1; x <= num; x++)
                {
                    for (var y = 1; y <= num; y++)
                    {
                        Console.Write($"{x} x {y} = {x*y}");
                        if (y != num) Console.Write(", ");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}