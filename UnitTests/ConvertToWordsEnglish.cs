using NumericWordsConversion;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class ToWordsEnglish
    {
        public CurrencyWordsConverter AmtToWords { get; set; }

        [SetUp]
        public void Setup()
        {
            this.AmtToWords = new CurrencyWordsConverter(new CurrencyWordsConversionOptions
            {
                Culture = Culture.International,
                OutputFormat = OutputFormat.English,
                CurrencyUnit = "rupees",
                SubCurrencyUnit = "paisa"
            });
        }

        [Test]
        public void ZeroPaisa()
        {
            var actualResult = this.AmtToWords.ToWords(32152M);
            var expectedResult = "Thirty two thousand one hundred fifty two rupees only";
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void OnePaisa()
        {
            var actualResult = this.AmtToWords.ToWords(32152.01M);
            var expectedResult = "Thirty two thousand one hundred fifty two rupees one paisa only";
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void TenPaisa()
        {
            var actualResult = this.AmtToWords.ToWords(32152.10M);
            var expectedResult = "Thirty two thousand one hundred fifty two rupees ten paisa only";
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void ElevenPaisa()
        {
            var actualResult = this.AmtToWords.ToWords(32152.11M);
            var expectedResult = "Thirty two thousand one hundred fifty two rupees eleven paisa only";
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void ThirtyPaisa()
        {
            var actualResult = this.AmtToWords.ToWords(32152.30M);
            var expectedResult = "Thirty two thousand one hundred fifty two rupees thirty paisa only";
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void EightyEightPaisa()
        {
            var actualResult = this.AmtToWords.ToWords(32152.88M);
            var expectedResult = "Thirty two thousand one hundred fifty two rupees eighty eight paisa only";
            Assert.AreEqual(expectedResult, actualResult);
        }

    }
}
