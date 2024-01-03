using System;
using System.Globalization;
using System.Linq;
using static System.String;

namespace NumericWordsConversion
{
    /// <summary>
    /// Used to convert number to words
    /// <remarks>For Currency: Use <code>CurrencyWordsConverter</code></remarks>>
    /// </summary>
    public class NumericWordsConverter
    {
        private readonly ConversionFactory _conversionFactory;
        private readonly NumericWordsConversionOptions _options;

        #region Constructors

        /// <summary>
        /// Creates an instance of NumberConverter with default options
        /// <br/> Culture: International, OutputFormat: English, DecimalPlaces : 2
        /// </summary>
        public NumericWordsConverter()
        {
            _options = GlobalOptions.NumericWordsOptions;
            _conversionFactory = Utilities.InitializeConversionFactory(_options);
        }

        /// <summary>
        /// Creates an instance of NumberConverter with specified options
        /// </summary>
        public NumericWordsConverter(NumericWordsConversionOptions options)
        {
            _options = options;
            _conversionFactory = Utilities.InitializeConversionFactory(_options);
        }
        #endregion

        /// <summary>
        /// Convert number to words as per specified options
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public string ToWords(decimal number)
        {
            string integralDigitsString = number
                .ToString(CultureInfo.InvariantCulture)
                .Split('.')[0];

            decimal fractionalDigits = number % 1;

            string integralWords = _conversionFactory.ConvertDigits(integralDigitsString);
            string fractionalDigitsString = (_options.DecimalPlaces > -1 ? decimal.Parse(fractionalDigits.ToString($"F{_options.DecimalPlaces}", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture)
                .ToString($"G{_options.DecimalPlaces}", CultureInfo.InvariantCulture)
                : fractionalDigits.ToString("G", CultureInfo.InvariantCulture)
                )
                    .Split('.')
                    .ElementAtOrDefault(1);
            if (fractionalDigits <= 0 || IsNullOrEmpty(fractionalDigitsString)) return integralWords.CapitalizeFirstLetter();

            string fractionalWords = Empty;
            fractionalDigitsString
                .ToList()
                .ForEach(x => fractionalWords += _conversionFactory.ToOnesWords(Convert.ToUInt16(x.ToString(CultureInfo.InvariantCulture))) + " ");

            return $"{integralWords} {_options.DecimalSeparator} {fractionalWords.TrimEnd()}".CapitalizeFirstLetter();
        }
    }
}