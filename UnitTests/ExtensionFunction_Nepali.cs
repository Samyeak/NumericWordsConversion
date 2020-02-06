namespace UnitTests {

    using System;
    using NumericWordsConversion;
    using NUnit.Framework;

    public class ExtensionFunctionNepali {

        [SetUp]
        public void Setup() {
            NumericWordsConfiguration.ConfigureConversionDefaults( options => OptionsInitializer.SetDefaultCurrencyWordsOptions( new CurrencyWordsConversionOptions {
                Culture = Culture.Nepali, OutputFormat = OutputFormat.Unicode
            } ) );
        }

        [Test]
        public void Extension() {
            const Decimal amt = 30246.20M;
            const String expectedResult = "तीस हजार दुई सय छयालिस रूपैयाँ बीस पैसा मात्र";
            var actualResult = amt.ToCurrencyWords();
            Assert.AreEqual( expectedResult, actualResult );
        }

        [Test]
        public void ShankhaExtension() {
            const Decimal amt = 88_77_66_55_44_33_22_11_321M;
            const String expectedResult = "अठासी शंख सत्हत्तर पद्म छैसठी नील पच्पन्न खरब चौवालिस अरब तेतीस करोड बाइस लाख एघार हजार तीन सय एकाइस रूपैयाँ मात्र";
            var actualResult = amt.ToCurrencyWords();
            Assert.AreEqual( expectedResult, actualResult );
        }

        [Test]
        public void PadmaAndTwentyPaisaExtension() {
            const Decimal amt = 77_66_55_44_33_22_11_321.88123456789M;
            const String expectedResult = "सत्हत्तर पद्म छैसठी नील पच्पन्न खरब चौवालिस अरब तेतीस करोड बाइस लाख एघार हजार तीन सय एकाइस रूपैयाँ अठासी पैसा मात्र";
            var actualResult = amt.ToCurrencyWords();
            Assert.AreEqual( expectedResult, actualResult );
        }

        [Test]
        public void PaisaOnly() {
            const Decimal amt = 0.88M;
            const String expectedResult = "अठासी पैसा मात्र";

            var actualResult = amt.ToCurrencyWords();
            Assert.AreEqual( expectedResult, actualResult );
        }

        [Test]
        public void NepaliExtension() {
            const Decimal amt = 32152.88M;
            var actualResult = amt.ToCurrencyWords();
            const String expectedResult = "बतीस हजार एक सय बाउन्न रूपैयाँ अठासी पैसा मात्र";
            Assert.AreEqual( expectedResult, actualResult );
        }

    }

}