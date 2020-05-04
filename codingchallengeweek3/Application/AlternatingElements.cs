using System;
using System.Text.RegularExpressions;

namespace Application
{
    public class AlternatingElements
    {
        //Takes two lists and shuffles the elements into a single list.
        public void Shuffle(string input)
        {
            //Valids input is two lists.
            Regex inputValidation = new Regex(@"^\[(\d+|(""[\w\d]+""))\s*(,\s*(\d+|(""[\w\d]+""))\s*){4}\]\s*\+\s*\[(\d+|(""[\w\d]+""))\s*(,\s*(\d+|(""[\w\d]+""))\s*){4}\]$");
            if (!inputValidation.IsMatch(input)) throw new FormatException("Incorrect format input.");
            else
            {
                //Parse the lists.
                var splitArrays = Regex.Split(input, @"]\s*\+\s*\[");

                //Parsing first list.
                var array1String = splitArrays[0];
                var array1 = array1String.Split(",");
                array1[0] = array1[0].Replace("[", "");

                //Parsing second list.
                var array2String = splitArrays[1];
                var array2 = array2String.Split(",");
                array2[4] = array2[4].Replace("]", "");

                //Merge the lists.
                string[] finalArray = new string[] { array1[0].Trim(), array2[0].Trim(), array1[1].Trim(), array2[1].Trim(), array1[2].Trim(), array2[2].Trim(), array1[3].Trim(), array2[3].Trim(), array1[4].Trim(), array2[4].Trim()};
                Console.WriteLine("[" + string.Join(',' , finalArray) + "]");
            }
        }
    }
}