using NumericWordsConversion;
using NUnit.Framework;

namespace UnitTests
{
    public class ToUnicodeWords
    {
        public CurrencyWordsConverter amtToWords { get; set; }

        [SetUp]
        public void Setup()
        {
            amtToWords = new CurrencyWordsConverter(new CurrencyWordsConversionOptions
            {
                Culture = Culture.Nepali,
                OutputFormat = OutputFormat.Unicode,
            });
        }

        [Test]
        public void ZeroPaisa()
        {
            string actualResult = amtToWords.ToWords(32152M);
            string expectedResult = "बतीस हजार एक सय बाउन्न रूपैयाँ मात्र";
            Assert.That(Is.Equals(expectedResult, actualResult));
        }

        [Test]
        public void OnePaisa()
        {
            string actualResult = amtToWords.ToWords(32152.01M);
            string expectedResult = "बतीस हजार एक सय बाउन्न रूपैयाँ एक पैसा मात्र";
            Assert.That(Is.Equals(expectedResult, actualResult));
        }

        [Test]
        public void TenPaisa()
        {
            string actualResult = amtToWords.ToWords(32152.10M);
            string expectedResult = "बतीस हजार एक सय बाउन्न रूपैयाँ दस पैसा मात्र";
            Assert.That(Is.Equals(expectedResult, actualResult));
        }

        [Test]
        public void ElevenPaisa()
        {
            string actualResult = amtToWords.ToWords(32152.11M);
            string expectedResult = "बतीस हजार एक सय बाउन्न रूपैयाँ एघार पैसा मात्र";
            Assert.That(Is.Equals(expectedResult, actualResult));
        }

        [Test]
        public void ThirtyPaisa()
        {
            string actualResult = amtToWords.ToWords(32152.30M);
            string expectedResult = "बतीस हजार एक सय बाउन्न रूपैयाँ तीस पैसा मात्र";
            Assert.That(Is.Equals(expectedResult, actualResult));
        }

        [Test]
        public void EightyEightPaisa()
        {
            string actualResult = amtToWords.ToWords(32152.88M);
            string expectedResult = "बतीस हजार एक सय बाउन्न रूपैयाँ अठासी पैसा मात्र";
            Assert.That(Is.Equals(expectedResult, actualResult));
        }

    }
}
