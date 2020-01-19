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
            new TestCaseData(0M).Returns("Zero"),
            new TestCaseData(0.001M).Returns("Zero point zero zero one"),
            new TestCaseData(0.01M).Returns("Zero point zero one"),
            new TestCaseData(100M).Returns("One hundred"),
            new TestCaseData(10555.01M).Returns("Ten thousand five hundred fifty five point zero one"),
            new TestCaseData(99_000M).Returns("Ninety nine thousand"),
            new TestCaseData(99_000.1M).Returns("Ninety nine thousand point one"),
            new TestCaseData( 10_000_000_000_000_000_000_000_000_000M).Returns("Ten octillion"),
        };
    }
}