using System;

namespace CodingChallenge1
{
    class Program
    {
        static void Main(string[] args)
        {   
            int sweet = 0; //Counter for number of sweet.
            int salty = 0; //Counter for number of salty.
            int sweetnsalty = 0; //Counter for number of sweetnsalty.
            for (int x = 1; x <= 100; x++) //Iterates x=1 to 100.
            {   
                switch (x%15)
                {
                    //Is a multiple of 3 and a multiple of 5.
                    case 0:
                        Console.WriteLine("sweet'nsalty");
                        sweetnsalty++;
                        break;
                    //Is a multiple of 3, but not 5.
                    case 3:
                    case 6:
                    case 9:
                    case 12:
                        Console.WriteLine("sweet");
                        sweet++;
                        break;
                    //Is a multiple of 5, but not 3.
                    case 5:
                    case 10:
                        Console.WriteLine("salty");
                        salty++;
                        break;
                    //Not a multiple of 3 or 5.
                    default:
                        Console.WriteLine(x);
                        break;
                }
            }
            //Displays count of each group.
            Console.WriteLine($"Sweet: {sweet}\nSalty: {salty}\nSweetn'Salty: {sweetnsalty}");
        }
    }
}
