using System;
using System.Globalization;
using System.Linq;

namespace NumericWordsConversion
{
    internal static class Utilities
    {
        /// <summary>
        /// Capitalizes the first letter of input string
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        internal static string CapitalizeFirstLetter(this string words)
        {
            if (string.IsNullOrEmpty(words)) {
                throw new ArgumentException("Input string must not be null or empty");
            }

            return words.First().ToString(CultureInfo.InvariantCulture).ToUpper(CultureInfo.InvariantCulture) + words.Substring(1);
        }

        /// <summary>
        /// Used to initialize the conversion factory as per the specified options
        /// </summary>
        /// <param name="options"></param>
        internal static ConversionFactory InitializeConversionFactory(NumericWordsConversionOptions options)
        {
            ManageSuitableResources(out var ones, out var tens, out var scale, options);
            return new ConversionFactory(options, ones, tens, scale);
        }

        /// <summary>
        /// Output resources for words conversion as per the specified options
        /// </summary>
        /// <param name="ones">Output parameter for ones digit</param>
        /// <param name="tens">Output parameter for tens digit</param>
        /// <param name="scale">Output parameter for scale of specified culture</param>
        /// <param name="options">Options used for resources output</param>
        internal static void ManageSuitableResources(out string[] ones, out string[] tens, out string[] scale, NumericWordsConversionOptions options)
        {
            switch (options.Culture)
            {
                case Culture.Nepali:
                    switch (options.OutputFormat)
                    {
                        case OutputFormat.English:
                            ones = WordResources.OnesEnglish;
                            tens = WordResources.TensEnglish;
                            scale = WordResources.ScaleNepEnglish;
                            break;
                        case OutputFormat.Unicode:
                            ones = WordResources.OnesNep;
                            tens = WordResources.TensNep;
                            scale = WordResources.ScaleNep;
                            break;
                        case OutputFormat.Devnagari:
                            ones = WordResources.OnesDevnagari;
                            tens = WordResources.TensDevnagari;
                            scale = WordResources.ScaleDevnagari;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                case Culture.International:
                    scale = WordResources.ScaleEng;
                    ones = WordResources.OnesEnglish;
                    tens = WordResources.TensEnglish;
                    break;
                case Culture.Hindi:
                    switch (options.OutputFormat)
                    {
                        case OutputFormat.English:
                            ones = WordResources.OnesEnglish;
                            tens = WordResources.TensEnglish;
                            scale = WordResources.ScaleNepEnglish;
                            break;
                        case OutputFormat.Unicode:
                            ones = WordResources.OnesHindi;
                            tens = WordResources.TensHindi;
                            scale = WordResources.ScaleHindi;
                            break;
                        case OutputFormat.Devnagari:
                            throw new NotSupportedException("Devnagari Output is not currently supported for Hindi Numeric System");
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(options.Culture), options.Culture, "Invalid Culture in Conversion Options");
            }
        }
    }
}
