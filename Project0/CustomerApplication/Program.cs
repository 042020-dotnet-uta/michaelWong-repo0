using System;

namespace CustomerApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            UserTerminal ui = new UserTerminal();
            new UserBuilder().BuildUser("admin");
        }
    }
}
