using NumericWordsConversion;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class NumberConverterTest
    {
        [Test, TestCaseSource(nameof(TestCases))]
        public string MyTestCases(decimal amount)
        {
            NumberConverter amt = new NumberConverter(new WordsConversionOptions(){DecimalPlaces = 4});
            string result = amt.ToWords(amount);
            return result;
        }

        private static readonly TestCaseData[] TestCases =
        {
            new TestCaseData(0M).Returns(""),
            new TestCaseData(100M).Returns("one hundred"),
            new TestCaseData(99_000M).Returns("ninety nine thousand"),
            new TestCaseData(99_000.1M).Returns("ninety nine thousand point one"),
            new TestCaseData(0.01M).Returns("ninety nine thousand point one"),
            new TestCaseData(0.001M).Returns("ninety nine thousand point one"),
        };
    }
}