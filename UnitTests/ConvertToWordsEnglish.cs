using NumericWordsConversion;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class ConvertToWordsEnglish
    {
        public AmountToWords amtToWords { get; set; }

        [SetUp]
        public void Setup()
        {
            amtToWords = new AmountToWords();
        }

        [Test]
        public void ZeroPaisa()
        {
            string actualResult = amtToWords.ConvertToWords(32152M);
            string expectedResult = "Thirty two thousand one hundred fifty two rupees only";
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void OnePaisa()
        {
            string actualResult = amtToWords.ConvertToWords(32152.01M);
            string expectedResult = "Thirty two thousand one hundred fifty two rupees one paisa only";
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void TenPaisa()
        {
            string actualResult = amtToWords.ConvertToWords(32152.10M);
            string expectedResult = "Thirty two thousand one hundred fifty two rupees ten paisa only";
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void ElevenPaisa()
        {
            string actualResult = amtToWords.ConvertToWords(32152.11M);
            string expectedResult = "Thirty two thousand one hundred fifty two rupees eleven paisa only";
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void ThirtyPaisa()
        {
            string actualResult = amtToWords.ConvertToWords(32152.30M);
            string expectedResult = "Thirty two thousand one hundred fifty two rupees thirty paisa only";
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void EightyEightPaisa()
        {
            string actualResult = amtToWords.ConvertToWords(32152.88M);
            string expectedResult = "Thirty two thousand one hundred fifty two rupees eighty eight paisa only";
            Assert.AreEqual(expectedResult, actualResult);
        }

    }
}
