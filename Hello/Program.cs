using System;
using System.Text.RegularExpressions;

namespace Hello
{
    
    class Program
    {

        public static void Main(String[] args)
        {

            Regex rx = new Regex(@"^[\p{Lu}\p{Ll}\p{Nd}]{8,}$");
            System.Console.WriteLine(rx.IsMatch("faoiasdj wfafasa"));
            System.Console.WriteLine(rx.IsMatch("W11h81^(21"));
            System.Console.WriteLine(rx.IsMatch("_"));
            System.Console.WriteLine(rx.IsMatch(""));
        }

    }

}