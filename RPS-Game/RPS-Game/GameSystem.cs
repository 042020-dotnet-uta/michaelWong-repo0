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
            NewGame = (Action) ( () => Console.WriteLine("Starting New Game!\n") ) + (Action) PromptPlayerNames + StartGame;

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
        Game ends once a player has two round wins.
        Generates new rounds and starts the round.
        Console game output details once a player has won.
        */
        public void StartGame()
        {
            Console.WriteLine();
            _roundNumber = 0;
            do
            {
                StartNewRound(); //Instantiates a new Round object.
            } while (players[0].wins < 2 && players[1].wins < 2); //Ends when a player acquires two wins.

            if (players[0].wins == 2) Console.WriteLine($"\n{players[0].name} Wins {players[0].wins} - {players[1].wins} With {ties} Ties.");
            else Console.WriteLine($"\n{players[1].name} Wins {players[1].wins} - {players[0].wins} With {ties} Ties.");
        }

        /*
        Method to create a new round.
        Stores Round object into rounds field.
        Starts the round.
        Outputs to console the Player RPS choices and the round outcome.
        */
        private void StartNewRound()
        {

            Round newRound = new Round(roundNumber);
            rounds.Add(newRound);
            newRound.StartRound();
            
            PrintResult(newRound);

        }

        /*
        Method to print the round details.
        Round N: X chose RPS, Y chose RPS - {result}
        */
        private void PrintResult(Round round)
        {
            Console.Write($"Round {round.roundCount}: {players[0].name} chose {round.choices[0]}, {players[1].name} chose {round.choices[1]}. - ");
            
            switch (round.result)
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