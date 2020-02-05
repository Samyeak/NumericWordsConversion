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
            var amt = 30246.20M;
            var expectedResult = "तीस हजार दुई सय छयालिस रूपैयाँ बीस पैसा मात्र";
            var actualResult = amt.ToCurrencyWords();
            Assert.AreEqual(expectedResult, actualResult);
        }
        [Test]
        public void ShankhaExtension()
        {
            var amt = 88_77_66_55_44_33_22_11_321M;
            var expectedResult = "अठासी शंख सत्हत्तर पद्म छैसठी नील पच्पन्न खरब चौवालिस अरब तेतीस करोड बाइस लाख एघार हजार तीन सय एकाइस रूपैयाँ मात्र";
            var actualResult = amt.ToCurrencyWords();
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void PadmaAndTwentyPaisaExtension()
        {
            var amt = 77_66_55_44_33_22_11_321.88123456789M;
            var expectedResult = "सत्हत्तर पद्म छैसठी नील पच्पन्न खरब चौवालिस अरब तेतीस करोड बाइस लाख एघार हजार तीन सय एकाइस रूपैयाँ अठासी पैसा मात्र";
            var actualResult = amt.ToCurrencyWords();
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void PaisaOnly()
        {
            var amt = 0.88M;
            var expectedResult = "अठासी पैसा मात्र";

            var actualResult = amt.ToCurrencyWords();
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void NepaliExtension()
        {
            var amt = 32152.88M;
            var actualResult = amt.ToCurrencyWords();
            var expectedResult = "बतीस हजार एक सय बाउन्न रूपैयाँ अठासी पैसा मात्र";
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}