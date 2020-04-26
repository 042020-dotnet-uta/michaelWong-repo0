using System;

namespace RPS_Game
{

    public class Round
    {
        /*
        Round Class for each round that occurs in a game.
        Use StartRound method after the round is instantiated to start the round (generates RPS choices and result).
        */

        private static Random random = new Random();
        public RPS[] choices;
        public readonly int roundCount;
        public readonly Action Run;
        private int _result;
        public int result
        {
            get
            {
                return _result;
            }
            private set
            {
                _result = value;
            }
        }

        public Round(int n)
        {
            roundCount = n;
            Run = (Action) GenerateRPS + (Action) CompareRPS;
        }

        /*
        Method to generate the RPS choices for each player.
        Randomly assigns rock, paper or scissor to each player.
        */
        private void GenerateRPS() 
        {
            choices = new RPS[] { (RPS) random.Next(3), (RPS) random.Next(3) };
        }

        /*
        Method to determine the result of the round.
        Assigns an int to represent the result.
        1: Player 1 wins.
        0: Both players tie.
        -1: Player 2 wins.
        */
        private void CompareRPS()
        {
            if (choices[0] == choices[1]) result = 0; //Tied round. Both choices are same.
            else if ((int)choices[0] + 1 == (int)choices[1] //Checks for R - P or P - S.
                || (int)choices[1] + 2 == (int)choices[0]) //Checks for S - R.
                result = -1; //Player 2 wins.
            else result = 1; //Player 1 wins.
        }

    }

}