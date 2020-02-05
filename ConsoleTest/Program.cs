using NumericWordsConversion;
using System;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var converter = new CurrencyWordsConverter();
            const Decimal number = 123_000M;
            var words = converter.ToWords(number);
            //words = number.To
            Console.WriteLine(words);
            Console.ReadKey();
        }

    }
}
