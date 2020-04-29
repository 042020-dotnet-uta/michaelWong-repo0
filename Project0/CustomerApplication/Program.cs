using System;

namespace CustomerApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.ReadLine();
            Console.Clear();
            UserTerminal ui = new UserTerminal();
            ui.Run();
        }
    }
}