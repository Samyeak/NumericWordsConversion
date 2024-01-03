namespace NumericWordsConversion
{
    using System.Globalization;
    using System.Linq;
    using static System.String;
    public class CurrencyWordsConverter
    {
        private readonly ConversionFactory _conversionFactory;
        private readonly CurrencyWordsConversionOptions _options;

        #region Constructors

        /// <summary>
        /// Creates an instance of NumberConverter with default options
        /// <br/> Culture: International, OutputFormat: English, DecimalPlaces : 2
        /// </summary>
        public CurrencyWordsConverter()
        {
            _options = GlobalOptions.CurrencyWordsOptions;
            _conversionFactory = Utilities.InitializeConversionFactory(_options);
        }

        /// <summary>
        /// Creates an instance of NumberConverter with specified options
        /// </summary>
        public CurrencyWordsConverter(CurrencyWordsConversionOptions options)
        {
            _options = options;
            _conversionFactory = Utilities.InitializeConversionFactory(_options);
        }
        #endregion

        /// <summary>
        /// Converts to words as per defined option
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public string ToWords(decimal number)
        {
            if (number <= 0) return Empty;
            decimal fractionalDigits = number % 1;
            string integralDigitsString = number
                .ToString(CultureInfo.InvariantCulture)
                .Split('.')
                .ElementAt(0);
            string fractionalDigitsString = fractionalDigits.ToString(_options.DecimalPlaces > -1 ? $"F{_options.DecimalPlaces}" : "G",
                                                    CultureInfo.InvariantCulture)
                                                .Split('.')
                                                .ElementAtOrDefault(1) ?? Empty;
            if (decimal.Parse(integralDigitsString, CultureInfo.InvariantCulture) <= 0 && decimal.Parse(fractionalDigitsString, CultureInfo.InvariantCulture) <= 0) return Empty;

            string integralWords = Empty;
            if (decimal.Parse(integralDigitsString, CultureInfo.InvariantCulture) > 0)
            {
                integralWords = _conversionFactory.ConvertDigits(integralDigitsString);
                integralWords = _options.CurrencyNotationType == NotationType.Prefix
                    ? _options.CurrencyUnit + " " + integralWords
                    : integralWords + " " + _options.CurrencyUnit;
            }

            if (int.Parse(fractionalDigitsString) <= 0 || IsNullOrEmpty(fractionalDigitsString)) return Concat(integralWords, IsNullOrEmpty(_options.EndOfWordsMarker) ? "" : " " + _options.EndOfWordsMarker).CapitalizeFirstLetter();

            string fractionalWords = _conversionFactory.ConvertDigits(fractionalDigitsString);
            fractionalWords = _options.SubCurrencyNotationType == NotationType.Prefix
                ? _options.SubCurrencyUnit + " " + fractionalWords
                : fractionalWords + " " + _options.SubCurrencyUnit;

            fractionalWords = (
                $"{integralWords} " +
                $"{(IsNullOrEmpty(_options.CurrencyUnitSeparator) ? "" : _options.CurrencyUnitSeparator + " ")}" +
                $"{fractionalWords.TrimEnd()}{(IsNullOrEmpty(_options.EndOfWordsMarker) ? "" : " " + _options.EndOfWordsMarker)}")
                    .Trim().CapitalizeFirstLetter();

            return fractionalWords;

        }
    }
}