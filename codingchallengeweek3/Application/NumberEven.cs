using System;

namespace Application
{
    public class NumberEven
    {
        //Checks if an integer inputted is even or odd.
        public void IsEven(String input)
        {
            //Validates integer input.
            int num;
            bool check = Int32.TryParse(input, out num);
            if (! check) throw new FormatException(input + " is a string, not a number.");

            //Integer was inputted.
            else
            {
                switch(num%2)
                {
                    //Even.
                    case 0:
                        Console.WriteLine(input + " is an even number.");
                        break;
                    //Odd.
                    case 1:
                        Console.WriteLine(input + " is not an even number.");
                        break;
                }
            }
        }
    }
}