using System;
using System.Globalization;
using System.Linq;

namespace NumericWordsConversion
{
    internal static class Utilities
    {
        internal static string CapitalizeFirstLetter(this string words)
        {
            if (string.IsNullOrEmpty(words)) throw new ArgumentException("Input string must not be null or empty");
            return words.First().ToString(CultureInfo.InvariantCulture).ToUpper(CultureInfo.InvariantCulture) + words.Substring(1);
        }
    }
}
