using NumericWordsConversion;
using System;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            
            CurrencyWordsConverter converter = new CurrencyWordsConverter();
            decimal number = 123_000M;
            string words = converter.ToWords(number);
            //words = number.To
            Console.WriteLine(words);
            Console.ReadKey();
        }

    }
}
