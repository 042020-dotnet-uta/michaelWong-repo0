using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace RPS_Game
{

    /*
    RPS values.
    0 - rock
    1 - paper
    2 - scissor
    */
    public enum RPS
    {
        rock,
        paper,
        scissors
    }

    public class GameSystem
    {

        /*
        Class GameSystem for a game of RPS.
        After instantiated, use Run() to start a new game.
        Console prompt for player names input.
        Wins required to win the game of RPS is 2.
        Outputs to console.
        */

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
        public int ties; //Stores number of tie rounds.
        public readonly Action Run;

        //Logger
        private readonly ILogger _logger;

        public GameSystem(ILogger<GameSystem> logger)
        {

            players = new Player[2];
            rounds = new List<Round>();
            ties = 0;
            Run = () =>
            {
                Console.WriteLine("Starting New Game!\n");
                PromptPlayerNames();
                StartGame();
            };

            //Inject the Logging Service
            _logger = logger;

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

            Console.WriteLine($"Player names are: {players[0].name} | {players[1].name}\n");
        }

        /*
        Method that starts the game simulation.
        Game ends once a player has two round wins.
        Generates new rounds and starts the round.
        Console game output details once a player has won.
        */
        public void StartGame()
        {
            _roundNumber = 0;
            do
            {
                RunNewRound(); //Instantiates a new Round object.
            } while (players[0].wins < 2 && players[1].wins < 2); //Ends when a player acquires two wins.

            if (players[0].wins == 2) Console.WriteLine($"\n{players[0].name} Wins {players[0].wins} - {players[1].wins} With {ties} Ties.");
            else Console.WriteLine($"\n{players[1].name} Wins {players[1].wins} - {players[0].wins} With {ties} Ties.");
            _logger.LogInformation($"{players[0].wins} - {players[1].wins} - {ties}");
        }

        /*
        Method to create a new round.
        Stores Round object into rounds field.
        Starts the round.
        Outputs to console the Player RPS choices and the round outcome.
        */
        private void RunNewRound()
        {

            Round newRound = new Round(roundNumber);
            rounds.Add(newRound);
            newRound.Run();
            
            PrintResult(newRound);

        }

        /*
        Method to print the round details.
        Round N: X chose RPS, Y chose RPS - {result}
        */
        private void PrintResult(Round round)
        {
            String output = $"Round {round.roundCount}: {players[0].name} chose {round.choices[0]}, {players[1].name} chose {round.choices[1]}. - ";
            
            switch (round.result)
            {
                case -1: //Player 2 wins.
                    players[1].wins++;
                    output += $"{players[1].name} Won";
                    break;
                case 0: //Tied round.
                    ties++;
                    output += $"It's a Tie";
                    break;
                case 1: //Player 1 wins.
                    players[0].wins++;
                    output += $"{players[0].name} Won";
                    break;
            }

            Console.WriteLine(output);
        }


    }

    #endregion

}