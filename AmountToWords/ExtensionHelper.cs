using System;
using System.Collections.Generic;
using System.Text;
using static AmountToWordsHelper.AmountToWords;

namespace AmountToWordsHelper
{
    public static class ExtensionHelper
    {
        public static string ToWords(this decimal amount, Culture culture = Culture.English)
        {
            AmountToWords amountToWords = new AmountToWords(culture);

            string words = amountToWords.ConvertToWords(amount);

            return words;
        }

        /// <summary>
        /// Converts the input decimal to unicode amount in words
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static string ToUnicodeWords(this decimal amount)
        {
            AmountToWords amountToWords = new AmountToWords(Culture.Nepali);
            string words = amountToWords.ConvertToWords(amount);
            return words;
        }

        /// <summary>
        /// Converts the input decimal to english words
        /// eg. 32152.88 => Thirty Two Thousand One Hundred Fifty Two Rupees Eighty Eight Paisa Only
        /// </summary>
        /// <param name="amount">32152.88</param>
        /// <returns>Thirty Two Thousand One Hundred Fifty Two Rupees Eighty Eight Paisa Only</returns>
        public static string ToEnglishWords(this decimal amount)
        {
            AmountToWords amountToWords = new AmountToWords(Culture.English);
            string words = amountToWords.ConvertToWords(amount);
            return words;
        }
    }
}
