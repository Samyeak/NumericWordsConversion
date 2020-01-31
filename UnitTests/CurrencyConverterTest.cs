using NumericWordsConversion;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class CurrencyConverterTest
    {
        [Test, TestCaseSource(nameof(TestCases))]
        public string MyTestCases(decimal amount)
        {
            CurrencyWordsConversionOptions options = new CurrencyWordsConversionOptions()
            {
                Culture = Culture.International,
            };

            CurrencyConverter amt = new CurrencyConverter(options);
            string result = amt.ToWords(amount);
            return result;
        }

        [Test, TestCaseSource(nameof(NepaliUnicodeCases))]
        public string NepaliUnicodeTest(decimal amount)
        {
            CurrencyWordsConversionOptions options = new CurrencyWordsConversionOptions()
            {
                Culture = Culture.Nepali,
                OutputFormat = OutputFormat.Unicode,
                EndOfWordsMarker = "मात्र"
            };

            CurrencyConverter amt = new CurrencyConverter(options);
            string result = amt.ToWords(amount);
            return result;
        }

        private static readonly TestCaseData[] TestCases =
        {
            new TestCaseData(0M).Returns(""),
            new TestCaseData(0.001M).Returns(""),
            new TestCaseData(0.01M).Returns("One cents only"),
            new TestCaseData(100M).Returns("One hundred dollar only"),
            new TestCaseData(10555.01M).Returns("Ten thousand five hundred fifty five dollar one cents only"),
            new TestCaseData(99_000M).Returns("Ninety nine thousand dollar only"),
            new TestCaseData(99_000.1M).Returns("Ninety nine thousand dollar ten cents only"),
            new TestCaseData( 10_000_000_000_000_000_000_000_000_000M).Returns("Ten octillion dollar only")
        };

        private static readonly TestCaseData[] NepaliUnicodeCases =
        {
            new TestCaseData(0M).Returns(""),
            new TestCaseData(0.001M).Returns(""),
            new TestCaseData(0.01M).Returns("एक पैसा मात्र"),
            new TestCaseData(100M).Returns("एक सय रूपैयाँ मात्र"),
            new TestCaseData(10555.01M).Returns("दस हजार पाँच सय पच्पन्न रूपैयाँ एक पैसा मात्र"),
            new TestCaseData(99_000M).Returns("उनान्सय हजार रूपैयाँ मात्र"),
            new TestCaseData(99_000.1M).Returns("उनान्सय हजार रूपैयाँ दस पैसा मात्र"),
            new TestCaseData( 10_000_000_000_000_000_000_000_000_000M).Returns("दस परर्ध रूपैयाँ मात्र")
        };
    }
}