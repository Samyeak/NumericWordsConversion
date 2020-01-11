using AmountToWordsHelper;
using NUnit.Framework;
using System;

namespace UnitTests
{
    public class ExtensionFunctionEnglish
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Extension()
        {
            decimal amt = 36253.20M;
            string expectedResult = "Thirty six thousand two hundred fifty three rupees twenty paisa only";
            string actualResult = amt.ToWords();
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void MaxLengthException()
        {
            decimal amt = 9_88_77_66_55_44_33_22_11_321M;
            TestDelegate testDelegate = new TestDelegate(() => amt.ToWords());
            Assert.Throws<Exception>(testDelegate, "Input digits exceeds maximum supported length(max:19)");
        }

        [Test]
        public void ThreeDigitFloorPaisa()
        {
            decimal amt = 11_321.924M;
            string expectedResult = "Eleven thousand three hundred twenty one rupees ninety two paisa only";
            var result = amt.ToWords();
            Assert.AreEqual(result, expectedResult);
        }

        [Test]
        public void ThreeDigitCeilingPaisa()
        {
            decimal amt = 11_321.929M;
            string expectedResult = "Eleven thousand three hundred twenty one rupees ninety three paisa only";
            var result = amt.ToWords();
            Assert.AreEqual(result, expectedResult);
        }

        [Test]
        public void ShankhaExtension()
        {
            decimal amt = 8877665544332211321.99M;
            string expectedResult = "Eighty eight shankha seventy seven padma sixty six neel fifty five kharba forty four arba thirty three crore twenty two lakh eleven thousand three hundred twenty one rupees ninety nine paisa only";
            string actualResult = amt.ToWords();
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void PaisaOnly()
        {
            decimal amt = 0.20M;
            string expectedResult = "Twenty paisa only";

            string actualResult = amt.ToWords();
            Assert.AreEqual(expectedResult, actualResult);
        }

    }
}