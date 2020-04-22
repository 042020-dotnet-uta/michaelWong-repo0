using System;
using System.Collections.Generic;

namespace RPS_Game
{
    class Program
    {
        static void Main(string[] args)
        {
            String[] users = new String[2];
            int[] score = new int[2];
            List<RPS[]> record = new List<RPS[]>();
            
            //Console prompt for Player 1 name.
            Console.Write("Player 1 Name: ");
            users[0] = Console.ReadLine();

            //Console prompt for Player 2 name.
            Console.Write("Player 2 Name: ");
            users[1] = Console.ReadLine();

            do //Loop until either player wins.
            {
                RPS[] choice = new RPS[2];
                choice[0] = GenerateRPS();
                choice[1] = GenerateRPS();

                switch(CompareRPS(choice))
                {
                    case -1:
                        score[1]++; //Player 2 wins. Increment score.
                        Console.WriteLine($"{choice[0]} - {choice[1]} ! {users[1]} Wins");
                        break;
                    case 0:
                        Console.WriteLine($"{choice[0]} - {choice[1]} ! It's a Tie");
                        break;
                    case 1:
                        score[0]++; //Player 1 wins. Increment score.
                        Console.WriteLine($"{choice[0]} - {choice[1]} ! {users[0]} Wins");
                        break;
                }

                record.Add(choice); //Stores RPS values.

            }
            while (score[0] < 2 && score[1] < 2);

            if (score[0] == 2) Console.WriteLine($"{users[0]} Wins!");
            else Console.WriteLine($"{users[1]} Wins!");

            Console.WriteLine(record.ToString());

        }

        /*
        Method to generate a random RPS value.
        Random int from {0, 1, 2} typecast as RPS.
        */
        static Random rand = new Random();
        static RPS GenerateRPS()
        {
            return (RPS) rand.Next(3);
        }

        /*
        Method to compare RPS values.
        Returns 0 if values at indexes 0 and 1 are equal.
        Returns 1 if value at index 0 beats index 1.
        Returns -1 if value at index 1 beats index 0.
        */
        static int CompareRPS(RPS[] input)
        {
            if (input[0] == input[1]) return 0;
            else if (((int)input[0] + 1) == (int)input[1] //R - P or P - S
                || (int)input[1] + 2 == (int)input[0]) return -1; //S - R
            else return 1; 
        }

    }

    //RPS values as ints.
    enum RPS
    {
        rock,
        paper,
        scissor
    }

}
