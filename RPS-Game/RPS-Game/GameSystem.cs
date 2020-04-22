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

        Player[] players;
        public Player[] Players
        {
            get => players;
            set => players = value;
        }
        public int roundCount; //Stores current round number in the game.
        public int ties; //Stores number of tie rounds.
        public List<RPS[]> record; //Stores all choices of previous rounds.

        public GameSystem() {

            players = new Player[2];
            roundCount = 0;
            ties = 0;
            record = new List<RPS[]>();
            PromptPlayerNames();

        }

        #endregion

        #region New Methods

        private void PromptPlayerNames()
        {
            //Console prompt for Player 1 name.
            Console.Write("Player 1 Name: ");
            players[0] = new Player(Console.ReadLine());

            //Console prompt for Player 2 name.
            Console.Write("Player 2 Name: ");
            players[1] = new Player(Console.ReadLine());
        }

        public void Start()
        {
            Random rand = new Random();

            do
            {
                roundCount++;
                RPS[] choice = new RPS[2];
                choice[0] = (RPS) rand.Next(3);
                choice[1] = (RPS) rand.Next(3);

                Console.Write($"Round {roundCount}: {players[0].name} chose {choice[0]}, {players[1].name} chose {choice[1]}. - ");

                switch (CompareRPS(choice))
                {
                    case -1:
                        players[1].wins++;
                        Console.WriteLine($"{players[1].name} Won");
                        break;
                    case 0:
                        ties++;
                        Console.WriteLine($"It's a Tie");
                        break;
                    case 1:
                        players[0].wins++;
                        Console.WriteLine($"{players[0].name} Won");
                        break;
                }

                record.Add(choice);
            } while (players[0].wins < 2 && players[1].wins < 2);

            if (players[0].wins == 2) Console.WriteLine($"{players[0].name} Wins {players[0].wins} - {players[1].wins} With {ties} Ties.");
            else Console.WriteLine($"{players[1].name} Wins {players[1].wins} - {players[0].wins} With {ties} Ties.");
        }

        public int CompareRPS(RPS[] choice)
        {
            if (choice[0] == choice[1]) return 0;
            else if ((int) choice[0] + 1 == (int) choice[1] || (int) choice[1] + 2 == (int) choice[0]) return -1;
            else return 1;
        }

    }
    
    #endregion
    
}