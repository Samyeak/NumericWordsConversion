using NUnit.Framework;
using System;
using NumericWordsConversion;

namespace UnitTests
{
    public class ExtensionFunctionEnglish
    {
        [SetUp]
        public void Setup()
        {
            NumericWordsConfiguration.ConfigureConversionDefaults(options =>
            {
                options.SetDefaultCurrencyWordsOptions(new CurrencyWordsConversionOptions
                {
                    Culture = Culture.International,
                    OutputFormat = OutputFormat.English,
                    CurrencyUnit = "rupees",
                    SubCurrencyUnit = "paisa"
                });
            });
        }

        [Test]
        public void Extension()
        {
            var amt = 36253.20M;
            var expectedResult = "Thirty six thousand two hundred fifty three rupees twenty paisa only";
            var actualResult = amt.ToCurrencyWords();
            Assert.AreEqual(expectedResult, actualResult);
        }

        //[Test]
        //public void MaxLengthException()
        //{
        //    decimal amt = 9_88_77_66_55_44_33_22_11_321M;
        //    TestDelegate testDelegate = new TestDelegate(() => amt.ToCurrencyWords());
        //    Assert.Throws<Exception>(testDelegate, "Input digits exceeds maximum supported length(max:19)");
        //}

        [Test]
        public void ThreeDigitFloorPaisa()
        {
            var amt = 11_321.924M;
            var expectedResult = "Eleven thousand three hundred twenty one rupees ninety two paisa only";
            var result = amt.ToCurrencyWords();
            Assert.AreEqual(result, expectedResult);
        }

        [Test]
        public void ThreeDigitCeilingPaisa()
        {
            var amt = 11_321.929M;
            var expectedResult = "Eleven thousand three hundred twenty one rupees ninety three paisa only";
            var result = amt.ToCurrencyWords();
            Assert.AreEqual(result, expectedResult);
        }

        [Test]
        public void ShankhaExtension()
        {
            var amt = 9_800_777_660_544_100_110_321.99M;
            var expectedResult = "Nine sextillion eight hundred quintillion seven hundred seventy seven quadrillion six hundred sixty trillion five hundred forty four billion one hundred million one hundred ten thousand three hundred twenty one rupees ninety nine paisa only";
            var actualResult = amt.ToCurrencyWords();
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void PaisaOnly()
        {
            var amt = 0.20M;
            var expectedResult = "Twenty paisa only";

            var actualResult = amt.ToCurrencyWords();
            Assert.AreEqual(expectedResult, actualResult);
        }

    }
}