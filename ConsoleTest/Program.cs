using AmountToWordsHelper;
using System;
using System.Threading;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            AmtTests();
            Console.ReadKey();
        }

        private static void AmtTests()
        {
            decimal amount = 88_77_66_55_44_33_22_11_321M;
            string max = amount.ToUnicodeWords();

            AmountToWords amt = new AmountToWords(AmountToWords.Culture.English);
            var result = amt.ConvertToEnglishWords(111_222_333_444M);
        }
    }
}
