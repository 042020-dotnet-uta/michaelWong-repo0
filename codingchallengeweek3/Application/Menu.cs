using System;

namespace Application
{
    public class Menu
    {
        //Displays the interface the user sees and checks command input.
        public void Run()
        {
            //Loop that handles control.
            int choice = -1;
            do
            {
                try
                {
                    Console.WriteLine("0:\tExit\n1:\tIs the number even?\n2:\tMultiplication Table\n3:\tAlternating Elements");
                    Console.Write("\n> ");
                    choice = Int32.Parse(Console.ReadLine());
                    Console.Clear();
                    switch (choice)
                    {
                        case 1:
                            Console.Write("Enter an integer:\n> ");
                            new NumberEven().IsEven(Console.ReadLine());
                            break;
                        case 2:
                            Console.Write("Enter an integer:\n> ");
                            new MultiplicationTable().MultTable(Console.ReadLine());
                            break;
                        case 3:
                            Console.Write("Enter two lists of 5 elements (strings or ints) and an addition sign:\nEx: [1,\"Jonie\", \"Chachi\", 15, 0] + [0,\"loves\", \"50\", 5,3]\n> ");
                            new AlternatingElements().Shuffle(Console.ReadLine());
                            break;
                    }
                    Console.WriteLine("Press enter to continue.");
                    Console.ReadLine();
                    Console.Clear();
                }
                catch (System.Exception ex)
                {
                    //Catches exceptions thrown by methods within the switch case.
                    Console.WriteLine(ex.Message + "\nPress enter to continue");
                    Console.ReadLine();
                    Console.Clear();
                }
            } while (choice != 0);
        }
    }
}