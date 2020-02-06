using NumericWordsConversion;
using NUnit.Framework;

namespace UnitTests
{

    using System;

    public class ToWordsNepali
    {
        public CurrencyWordsConverter AmtToWords { get; set; }

        [SetUp]
        public void Setup()
        {
            this.AmtToWords = new CurrencyWordsConverter(new CurrencyWordsConversionOptions
            {
                Culture = Culture.Nepali,
                OutputFormat = OutputFormat.Unicode,
            });
        }

        [Test]
        public void ZeroPaisa()
        {
            var actualResult = this.AmtToWords.ToWords(32152M);
            const String expectedResult = "बतीस हजार एक सय बाउन्न रूपैयाँ मात्र";
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void OnePaisa()
        {
            //Arrange
            const String expectedResult = "बतीस हजार एक सय बाउन्न रूपैयाँ एक पैसा मात्र";
            //Act
            var actualResult = this.AmtToWords.ToWords(32152.01M);
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void TenPaisa()
        {
            var actualResult = this.AmtToWords.ToWords(32152.10M);
            const String expectedResult = "बतीस हजार एक सय बाउन्न रूपैयाँ दस पैसा मात्र";
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void ElevenPaisa()
        {
            var actualResult = this.AmtToWords.ToWords(32152.11M);
            const String expectedResult = "बतीस हजार एक सय बाउन्न रूपैयाँ एघार पैसा मात्र";
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void ThirtyPaisa()
        {
            var actualResult = this.AmtToWords.ToWords(32152.30M);
            const String expectedResult = "बतीस हजार एक सय बाउन्न रूपैयाँ तीस पैसा मात्र";
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void EightyEightPaisa()
        {
            var actualResult = this.AmtToWords.ToWords(32152.88M);
            const String expectedResult = "बतीस हजार एक सय बाउन्न रूपैयाँ अठासी पैसा मात्र";
            Assert.AreEqual(expectedResult, actualResult);
        }

    }
}
