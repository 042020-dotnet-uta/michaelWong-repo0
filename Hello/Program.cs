using System;

namespace Hello
{

    public delegate int Del(String message);
    
    class Program
    {

        public static void Main(string[] args)
        {
            Del print = (String message) =>
            {
                Console.WriteLine("1: " + message);
                return 0;
            };

            Del printMore = (String message) =>
            {
                Console.WriteLine("2: I am the delegate.");
                return 1;
            };

            Del printing = (String message) =>
            {
                Console.WriteLine("3: I am printing.");
                return 2;
            };

            Del allDelegates = print + printMore + printing;
            Console.WriteLine(allDelegates);
            
        }
    }
}
