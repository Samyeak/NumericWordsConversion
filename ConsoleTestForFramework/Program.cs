using NumericWordsConversion;
using System;
namespace ConsoleTestForFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            decimal amount = 50_000_000.01M;
            string words = amount.ToCurrencyWords(Culture.Bhutanese);
            Console.WriteLine(words);
            Console.WriteLine(amount.ToCurrencyWords(Culture.International));
            Console.WriteLine(amount.ToCurrencyWords(Culture.Nepali));
            Console.WriteLine(amount.ToCurrencyWords(Culture.Nepali, OutputFormat.Devnagari));
            Console.WriteLine(amount.ToCurrencyWords(Culture.Nepali, OutputFormat.Unicode));
            Console.WriteLine(amount.ToCurrencyWords(Culture.Hindi));
            Console.ReadKey();
        }
    }
}
