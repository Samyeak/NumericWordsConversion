using NumericWordsConversion;
using System;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            NumberConverter converter = new NumberConverter();
            decimal number = 123_000M;
            string words = converter.ToWords(number);
            Console.WriteLine(words);
            Console.ReadKey();
        }

    }
}
