using System;
using System.Collections.Generic;

namespace RPS_Game
{

    //ints correspond to RPS values.
    public enum RPS
    {
        rock,
        paper,
        scissors
    }

    public class GameSystem
    {

        #region Fields and Constructor

        public readonly Player[] players;
        public readonly List<RPS[]> record; //Stores all choices of previous rounds.
        public int roundCount; //Stores current round number in the game.
        public int ties; //Stores number of tie rounds.

        public GameSystem()
        {

            players = new Player[2];
            record = new List<RPS[]>();
            roundCount = 0;
            ties = 0;
            PromptPlayerNames(); //Console prompt for player names.

        }

        #endregion

        #region New Methods

        /*
        Method for getting the player names.
        Writes to console and prompts user input.
        Uses String inputs to instantiate two Player objects.
        */
        private void PromptPlayerNames()
        {
            //Console prompt for Player 1 name.
            Console.Write("Player 1 Name: ");
            players[0] = new Player(Console.ReadLine());

            //Console prompt for Player 2 name.
            Console.Write("Player 2 Name: ");
            players[1] = new Player(Console.ReadLine());
        }

        /*
        Method that starts the game simulation.
        Simulates rounds of RPS. Ends when a player has acquired two wins.
        RPS choices each round are randomly generated.
        Round results are stored in this.record.
        Console output details each round result and choices.
        */
        public void Start()
        {
            Random rand = new Random();

            do
            {
                roundCount++; //Increment current round number.
                RPS[] choice = new RPS[2]; //Stores RPS choices for each player.

                //Generates a random int from {0,1,2}. Each int is typecast to RPS.
                choice[0] = (RPS)rand.Next(3);
                choice[1] = (RPS)rand.Next(3);

                Console.Write($"Round {roundCount}: {players[0].name} chose {choice[0]}, {players[1].name} chose {choice[1]}. - ");

                //Determines the result of each round from the RPS choices.
                switch (CompareRPS(choice))
                {
                    case -1: //Player 2 wins.
                        players[1].wins++;
                        Console.WriteLine($"{players[1].name} Won");
                        break;
                    case 0: //Tied round.
                        ties++;
                        Console.WriteLine($"It's a Tie");
                        break;
                    case 1: //Player 1 wins.
                        players[0].wins++;
                        Console.WriteLine($"{players[0].name} Won");
                        break;
                }

                record.Add(choice); //Stores the round choices.
            } while (players[0].wins < 2 && players[1].wins < 2); //Ends when a player acquires two wins.

            if (players[0].wins == 2) Console.WriteLine($"{players[0].name} Wins {players[0].wins} - {players[1].wins} With {ties} Ties.");
            else Console.WriteLine($"{players[1].name} Wins {players[1].wins} - {players[0].wins} With {ties} Ties.");
        }

        public int CompareRPS(RPS[] choice)
        {
            if (choice[0] == choice[1]) return 0; //Tied round. Both choices are same.
            else if ((int)choice[0] + 1 == (int)choice[1] //Checks for R - P or P - S.
                || (int)choice[1] + 2 == (int)choice[0]) //Checks for S - R.
                return -1; //Player 2 wins.
            else return 1; //Player 1 wins.
        }

    }

    #endregion

}