using System.Net.NetworkInformation;
using AmountToWordsHelper;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class NepaliCultureEnglishOutput
    {
        [Test, TestCaseSource("WordCases")]
        public void DecimalOnly(decimal amount, string words)
        {
            AmountToWords amt = new AmountToWords(AmountToWords.Culture.Nepali, AmountToWords.OutputFormat.English);
            string result = amt.ConvertToWords(amount);
            Assert.AreEqual(words, result);
        }

        static object[] WordCases =
        {
            new object[] { 0M, "" },
            new object[] { 0.01M, "One paisa only" },
            new object[] { 0.10M, "Ten paisa only" },
            new object[] { 0.88M, "Eighty eight paisa only" },
            new object[] { 1M   , "One rupees only" },
            new object[] { 1.01M, "One rupees one paisa only" },
            new object[] { 1.10M, "One rupees ten paisa only" },
            new object[] { 1.88M, "One rupees eighty eight paisa only" },
            new object[] { 10M   , "Ten rupees only" },
            new object[] { 10.01M, "Ten rupees one paisa only" },
            new object[] { 10.10M, "Ten rupees ten paisa only" },
            new object[] { 10.88M, "Ten rupees eighty eight paisa only" },
            new object[] { 19M   , "Nineteen rupees only" },
            new object[] { 19.01M, "Nineteen rupees one paisa only" },
            new object[] { 19.10M, "Nineteen rupees ten paisa only" },
            new object[] { 19.88M, "Nineteen rupees eighty eight paisa only" },
            new object[] { 21M   , "Twenty one rupees only" },
            new object[] { 21.01M, "Twenty one rupees one paisa only" },
            new object[] { 21.10M, "Twenty one rupees ten paisa only" },
            new object[] { 21.88M, "Twenty one rupees eighty eight paisa only" },
            new object[] { 100M   , "One hundred rupees only" },
            new object[] { 100.01M, "One hundred rupees one paisa only" },
            new object[] { 100.10M, "One hundred rupees ten paisa only" },
            new object[] { 100.88M, "One hundred rupees eighty eight paisa only" },
            new object[] { 550M   , "Five hundred fifty rupees only" },
            new object[] { 550.01M, "Five hundred fifty rupees one paisa only" },
            new object[] { 550.10M, "Five hundred fifty rupees ten paisa only" },
            new object[] { 550.88M, "Five hundred fifty rupees eighty eight paisa only" },
            new object[] { 555M   , "Five hundred fifty five rupees only" },
            new object[] { 555.01M, "Five hundred fifty five rupees one paisa only" },
            new object[] { 555.10M, "Five hundred fifty five rupees ten paisa only" },
            new object[] { 555.88M, "Five hundred fifty five rupees eighty eight paisa only" },
            new object[] { 1555M   , "One thousand five hundred fifty five rupees only" },
            new object[] { 1555.01M, "One thousand five hundred fifty five rupees one paisa only" },
            new object[] { 1555.10M, "One thousand five hundred fifty five rupees ten paisa only" },
            new object[] { 1555.88M, "One thousand five hundred fifty five rupees eighty eight paisa only" },
            new object[] { 10555M   , "Ten thousand five hundred fifty five rupees only" },
            new object[] { 10555.01M, "Ten thousand five hundred fifty five rupees one paisa only" },
            new object[] { 10555.10M, "Ten thousand five hundred fifty five rupees ten paisa only" },
            new object[] { 10555.88M, "Ten thousand five hundred fifty five rupees eighty eight paisa only" },

            new object[] { 1_00_000M   , "One lakh rupees only" },
            new object[] { 1_00_000.01M, "One lakh rupees one paisa only" },
            new object[] { 1_00_000.10M, "One lakh rupees ten paisa only" },
            new object[] { 1_00_000.88M, "One lakh rupees eighty eight paisa only" },
            new object[] { 1_00_555.01M, "One lakh five hundred fifty five rupees one paisa only" },
            new object[] { 1_00_555.10M, "One lakh five hundred fifty five rupees ten paisa only" },
            new object[] { 1_00_555.88M, "One lakh five hundred fifty five rupees eighty eight paisa only" },

            new object[] { 91_00_000M   , "Ninety one lakh rupees only" },
            new object[] { 91_00_000.01M, "Ninety one lakh rupees one paisa only" },
            new object[] { 91_00_000.10M, "Ninety one lakh rupees ten paisa only" },
            new object[] { 91_00_000.88M, "Ninety one lakh rupees eighty eight paisa only" },
            new object[] { 91_00_555.01M, "Ninety one lakh five hundred fifty five rupees one paisa only" },
            new object[] { 91_00_555.10M, "Ninety one lakh five hundred fifty five rupees ten paisa only" },
            new object[] { 91_00_555.88M, "Ninety one lakh five hundred fifty five rupees eighty eight paisa only" },

            new object[] { 9_01_00_000M   , "Nine crore one lakh rupees only" },
            new object[] { 9_01_00_000.01M, "Nine crore one lakh rupees one paisa only" },
            new object[] { 9_01_00_000.10M, "Nine crore one lakh rupees ten paisa only" },
            new object[] { 9_01_00_000.88M, "Nine crore one lakh rupees eighty eight paisa only" },
            new object[] { 9_01_00_555.01M, "Nine crore one lakh five hundred fifty five rupees one paisa only" },
            new object[] { 9_01_00_555.10M, "Nine crore one lakh five hundred fifty five rupees ten paisa only" },
            new object[] { 9_01_00_555.88M, "Nine crore one lakh five hundred fifty five rupees eighty eight paisa only" },

            //new object[] { 909_100_000M   , "Nine hundred nine million one hundred thousand rupees only" },
            //new object[] { 909_100_000.01M, "Nine hundred nine million one hundred thousand rupees one paisa only" },
            //new object[] { 909_100_000.10M, "Nine hundred nine million one hundred thousand rupees ten paisa only" },
            //new object[] { 909_100_000.88M, "Nine hundred nine million one hundred thousand rupees eighty eight paisa only" },
            //new object[] { 909_100_555.01M, "Nine hundred nine million one hundred thousand five hundred fifty five rupees one paisa only" },
            //new object[] { 909_100_555.10M, "Nine hundred nine million one hundred thousand five hundred fifty five rupees ten paisa only" },
            //new object[] { 909_100_555.88M, "Nine hundred nine million one hundred thousand five hundred fifty five rupees eighty eight paisa only" },

            //new object[] { 462_909_100_000M   , "Four hundred sixty two billion nine hundred nine million one hundred thousand rupees only" },
            //new object[] { 462_909_100_000.01M, "Four hundred sixty two billion nine hundred nine million one hundred thousand rupees one paisa only" },
            //new object[] { 462_909_100_000.10M, "Four hundred sixty two billion nine hundred nine million one hundred thousand rupees ten paisa only" },
            //new object[] { 462_909_100_000.88M, "Four hundred sixty two billion nine hundred nine million one hundred thousand rupees eighty eight paisa only" },
            //new object[] { 462_909_100_555.01M, "Four hundred sixty two billion nine hundred nine million one hundred thousand five hundred fifty five rupees one paisa only" },
            //new object[] { 462_909_100_555.10M, "Four hundred sixty two billion nine hundred nine million one hundred thousand five hundred fifty five rupees ten paisa only" },
            //new object[] { 462_909_100_555.88M, "Four hundred sixty two billion nine hundred nine million one hundred thousand five hundred fifty five rupees eighty eight paisa only" },

            //new object[] { 1_000_909_100_000M   , "One trillion nine hundred nine million one hundred thousand rupees only" },
            //new object[] { 1_000_909_100_000.01M, "One trillion nine hundred nine million one hundred thousand rupees one paisa only" },
            //new object[] { 1_000_909_100_000.10M, "One trillion nine hundred nine million one hundred thousand rupees ten paisa only" },
            //new object[] { 1_000_909_100_000.88M, "One trillion nine hundred nine million one hundred thousand rupees eighty eight paisa only" },
            //new object[] { 1_000_909_100_555.01M, "One trillion nine hundred nine million one hundred thousand five hundred fifty five rupees one paisa only" },
            //new object[] { 1_000_909_100_555.10M, "One trillion nine hundred nine million one hundred thousand five hundred fifty five rupees ten paisa only" },
            //new object[] { 1_000_909_100_555.88M, "One trillion nine hundred nine million one hundred thousand five hundred fifty five rupees eighty eight paisa only" },

            //new object[] { 1M   , "One rupees only" },
            //new object[] { 10M   , "Ten rupees only" },
            //new object[] { 100M   , "One hundred rupees only" },
            //new object[] { 1_000M, "One thousand rupees only" },
            //new object[] { 10_000M, "Ten thousand rupees only" },
            //new object[] { 100_000M, "One hundred thousand rupees only" },
            //new object[] {   1_000_000M, "One million rupees only" },
            //new object[] {  10_000_000M, "Ten million rupees only" },
            //new object[] { 100_000_000M, "One hundred million rupees only" },
            //new object[] {   1_000_000_000M, "One billion rupees only" },
            //new object[] {  10_000_000_000M, "Ten billion rupees only" },
            //new object[] { 100_000_000_000M, "One hundred billion rupees only" },
            //new object[] {   1_000_000_000_000M, "One trillion rupees only" },
            //new object[] {  10_000_000_000_000M, "Ten trillion rupees only" },
            //new object[] { 100_000_000_000_000M, "One hundred trillion rupees only" },
            //new object[] { 10_000_000_000_000_000_000_000_000_000M, "Ten octillion rupees only" },
        };
    }

    
}