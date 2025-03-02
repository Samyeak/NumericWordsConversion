
using NumericWordsConversion;
using NUnit.Framework;

namespace UnitTests
{
    public class InternationalNepaliTest
    {
        public CurrencyWordsConverter AmtToWords { get; set; }

        [SetUp]
        public void Setup()
        {
            AmtToWords = new CurrencyWordsConverter(new CurrencyWordsConversionOptions
            {
                Culture = Culture.International,
                CurrencyUnit = "rupees",
                SubCurrencyUnit = "paisa"
            });
        }

        [Test]
        public void ZeroPaisa()
        {
            string actualResult = AmtToWords.ToWords(32152M);
            string expectedResult = "Thirty two thousand one hundred fifty two rupees only";
            Assert.That(Is.Equals(expectedResult, actualResult));
        }

        [Test]
        public void OnePaisa()
        {
            string actualResult = AmtToWords.ToWords(32152.01M);
            string expectedResult = "Thirty two thousand one hundred fifty two rupees one paisa only";
            Assert.That(Is.Equals(expectedResult, actualResult));
        }

        [Test]
        public void TenPaisa()
        {
            string actualResult = AmtToWords.ToWords(32152.10M);
            string expectedResult = "Thirty two thousand one hundred fifty two rupees ten paisa only";
            Assert.That(Is.Equals(expectedResult, actualResult));
        }

        [Test]
        public void ElevenPaisa()
        {
            string actualResult = AmtToWords.ToWords(32152.11M);
            string expectedResult = "Thirty two thousand one hundred fifty two rupees eleven paisa only";
            Assert.That(Is.Equals(expectedResult, actualResult));
        }

        [Test]
        public void ThirtyPaisa()
        {
            string actualResult = AmtToWords.ToWords(32152.30M);
            string expectedResult = "Thirty two thousand one hundred fifty two rupees thirty paisa only";
            Assert.That(Is.Equals(expectedResult, actualResult));
        }

        [Test]
        public void EightyEightPaisa()
        {
            string actualResult = AmtToWords.ToWords(32152.88M);
            string expectedResult = "Thirty two thousand one hundred fifty two rupees eighty eight paisa only";
            Assert.That(Is.Equals(expectedResult, actualResult));
        }

    }
}
