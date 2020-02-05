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
            this._options = GlobalOptions.NumericWordsOptions;
            this._conversionFactory = Utilities.InitializeConversionFactory( this._options);
        }

        /// <summary>
        /// Creates an instance of NumberConverter with specified options
        /// </summary>
        public NumericWordsConverter(NumericWordsConversionOptions options)
        {
            this._options = options;
            this._conversionFactory = Utilities.InitializeConversionFactory( this._options);
        }
        #endregion

        /// <summary>
        /// Convert number to words as per specified options
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public string ToWords(decimal number)
        {
            var integralDigitsString = number
                .ToString(CultureInfo.InvariantCulture)
                .Split('.')
                .ElementAt(0);

            var fractionalDigits = number % 1;

            var integralWords = this._conversionFactory.ConvertDigits(integralDigitsString);
            var fractionalDigitsString = ( this._options.DecimalPlaces > -1 ? decimal.Parse(fractionalDigits.ToString($"F{this._options.DecimalPlaces}", CultureInfo.InvariantCulture))
                .ToString($"G{this._options.DecimalPlaces}", CultureInfo.InvariantCulture)
                : fractionalDigits.ToString("G", CultureInfo.InvariantCulture)
                )
                    .Split('.')
                    .ElementAtOrDefault(1);
            if (fractionalDigits <= 0 || IsNullOrEmpty(fractionalDigitsString)) {
                return integralWords.CapitalizeFirstLetter();
            }

            var fractionalWords = Empty;
            fractionalDigitsString
                .ToList()
                .ForEach(x => fractionalWords += this._conversionFactory.ToOnesWords(Convert.ToUInt16(x.ToString(CultureInfo.InvariantCulture))) + " ");

            return $"{integralWords} {this._options.DecimalSeparator} {fractionalWords.TrimEnd()}".CapitalizeFirstLetter();
        }
    }
}