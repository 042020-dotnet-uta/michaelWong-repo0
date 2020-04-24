using System;
using System.Collections.Generic;

namespace Hello
{
    
    class Program
    {

        public delegate void StringDelegate(String message, out String outMessage);

        public static void Main(String[] args)
        {

            Console.WriteLine("Awaiting User Input...");
            String message = "";
            message = Console.ReadLine();
            Console.WriteLine();

            StringDelegate PrintMessage = (String message, out String outMessage) => 
            {
                Console.WriteLine(message);
                outMessage = message;
            };

            StringDelegate CountLength = (String message, out String outMessage) => 
            {
                outMessage = message + $" is {message.Length} characters long.";
                Console.WriteLine(outMessage);
            };

            StringDelegate AppendMessage = (String message, out String outMessage) => 
            {
                Console.WriteLine("Appended Message");
                outMessage = message + " Appended";
            };

            List<StringDelegate> delegates = new List<StringDelegate>();
            delegates.Add(PrintMessage);
            delegates.Add(CountLength);
            delegates.Add(AppendMessage);
            delegates.Add(PrintMessage);
            delegates.Add(AppendMessage);
            delegates.Add(CountLength);
            delegates.Add(AppendMessage);

            foreach (StringDelegate stringDel in delegates)
            {
                stringDel(message, out message);
            }

        }

    }

}