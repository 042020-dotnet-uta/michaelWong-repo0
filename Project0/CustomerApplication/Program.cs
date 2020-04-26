using System;

namespace CustomerApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            User user = new UserBuilder().BuildUser("Customer");
            Console.WriteLine(user);
        }
    }
}
