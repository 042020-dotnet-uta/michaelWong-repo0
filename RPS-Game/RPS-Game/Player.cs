using System;

namespace RPS_Game
{
    public class Player
    {
        public readonly String name;
        public int wins;

        public Player(String name)
        {
            this.name = name;
            wins = 0;
        }
    }
}