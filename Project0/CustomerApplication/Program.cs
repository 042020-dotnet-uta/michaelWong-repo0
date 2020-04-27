using System;

namespace CustomerApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine(new ProductBuilder("23948293").BuildProduct());
        }
    }
}
