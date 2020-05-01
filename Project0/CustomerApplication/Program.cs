using System;

namespace CustomerApplication
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.Clear();
            UserTerminal ui = new UserTerminal();
            ui.Run();
        }
    }
}