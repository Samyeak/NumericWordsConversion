using NumericWordsConversion;
using NUnit.Framework;

namespace UnitTests
{
    public class ExtensionFunctionNepali
    {
        [SetUp]
        public void Setup()
        {
            NumericWordsConfiguration.ConfigureConversionDefaults(options =>
            {
                options.SetDefaultCurrencyWordsOptions(new CurrencyWordsConversionOptions
                {
                    Culture = Culture.Nepali,
                    OutputFormat = OutputFormat.Unicode,
                });
            });
        }

        [Test]
        public void Extension()
        {
            decimal amt = 30246.20M;
            string expectedResult = "तीस हजार दुई सय छयालिस रूपैयाँ बीस पैसा मात्र";
            string actualResult = amt.ToCurrencyWords();
            Assert.That(Is.Equals(expectedResult, actualResult));
        }
        [Test]
        public void ShankhaExtension()
        {
            decimal amt = 88_77_66_55_44_33_22_11_321M;
            string expectedResult = "अठासी शंख सत्हत्तर पद्म छैसठी नील पच्पन्न खरब चौवालिस अरब तेतीस करोड बाइस लाख एघार हजार तीन सय एकाइस रूपैयाँ मात्र";
            string actualResult = amt.ToCurrencyWords();
            Assert.That(Is.Equals(expectedResult, actualResult));
        }

        [Test]
        public void PadmaAndTwentyPaisaExtension()
        {
            decimal amt = 77_66_55_44_33_22_11_321.88123456789M;
            string expectedResult = "सत्हत्तर पद्म छैसठी नील पच्पन्न खरब चौवालिस अरब तेतीस करोड बाइस लाख एघार हजार तीन सय एकाइस रूपैयाँ अठासी पैसा मात्र";
            string actualResult = amt.ToCurrencyWords();
            Assert.That(Is.Equals(expectedResult, actualResult));
        }

        [Test]
        public void PaisaOnly()
        {
            decimal amt = 0.88M;
            string expectedResult = "अठासी पैसा मात्र";

            string actualResult = amt.ToCurrencyWords();
            Assert.That(Is.Equals(expectedResult, actualResult));
        }

        [Test]
        public void NepaliExtension()
        {
            decimal amt = 32152.88M;
            string actualResult = amt.ToCurrencyWords();
            string expectedResult = "बतीस हजार एक सय बाउन्न रूपैयाँ अठासी पैसा मात्र";
            Assert.That(Is.Equals(expectedResult, actualResult));
        }
    }
}