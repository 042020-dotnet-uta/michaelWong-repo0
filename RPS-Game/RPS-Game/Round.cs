using System;

namespace RPS_Game
{

    public class Round
    {
        private static Random random = new Random();
        public readonly RPS[] choices;
        public readonly int roundCount;
        public readonly int result;

        public Round(int n)
        {
            choices = new RPS[] { (RPS) random.Next(3), (RPS) random.Next(3) };
            roundCount = n;
            result = CompareRPS(); //Calculates the results of the round. Depends on the random generated choices.
        }

        /*
        Method to determine the result of the round.
        Returns an int to represent the result.
        1: Player 1 wins.
        0: Both players tie.
        -1: Player 2 wins.
        */
        private int CompareRPS()
        {
            if (choices[0] == choices[1]) return 0; //Tied round. Both choices are same.
            else if ((int)choices[0] + 1 == (int)choices[1] //Checks for R - P or P - S.
                || (int)choices[1] + 2 == (int)choices[0]) //Checks for S - R.
                return -1; //Player 2 wins.
            else return 1; //Player 1 wins.
        }

    }

}