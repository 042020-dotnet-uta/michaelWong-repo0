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
        public readonly List<Round> rounds;
        private int _roundNumber;
        private int roundNumber
        {
            get
            {
                _roundNumber++;
                return _roundNumber;
            }
        }
        public readonly Action NewGame;
        
        public int ties; //Stores number of tie rounds.

        public GameSystem()
        {

            players = new Player[2];
            rounds = new List<Round>();
            ties = 0;
            NewGame = (Action) PromptPlayerNames + StartGame;

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
        Console output details each round result and choices.
        */
        public void StartGame()
        {
            _roundNumber = 0;
            do
            {
                StartNewRound(); //Instantiates a new Round object.
            } while (players[0].wins < 2 && players[1].wins < 2); //Ends when a player acquires two wins.

            if (players[0].wins == 2) Console.WriteLine($"{players[0].name} Wins {players[0].wins} - {players[1].wins} With {ties} Ties.");
            else Console.WriteLine($"{players[1].name} Wins {players[1].wins} - {players[0].wins} With {ties} Ties.");
        }

        /*
        Method to instantiate and simulate a Round.
        Outputs to console the Player RPS choices and the round outcome.
        Stores Round object into rounds field.
        */
        private void StartNewRound()
        {

            Round newRound = new Round(roundNumber);
            rounds.Add(newRound);
            newRound.StartRound();

            Console.Write($"Round {newRound.roundCount}: {players[0].name} chose {newRound.choices[0]}, {players[1].name} chose {newRound.choices[1]}. - ");
            
            switch (newRound.result)
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

        }


    }

    #endregion

}