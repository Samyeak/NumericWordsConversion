namespace NumericWordsConversion
{
    using System;
    using System.Globalization;
    using System.Linq;
    using static System.String;
    public class CurrencyConverter
    {
        private readonly ConversionFactory _conversionFactory;
        private readonly CurrencyWordsConversionOptions _options;
        private readonly string[] _ones;
        private readonly string[] _tens;
        private readonly string[] _scale;

        #region Constructors
        /// <summary>
        /// Creates an instance of NumberConverter with default options
        /// <br/> Culture: International, OutputFormat: English, DecimalPlaces : 2
        /// </summary>
        public CurrencyConverter() : this(new CurrencyWordsConversionOptions()) { }

        /// <summary>
        /// Creates an instance of NumberConverter with specified options
        /// </summary>
        public CurrencyConverter(CurrencyWordsConversionOptions options)
        {
            this._options = options;
            switch (options.Culture)
            {
                case Culture.Nepali:
                    switch (options.OutputFormat)
                    {
                        case OutputFormat.English:
                            _ones = WordResources.OnesEnglish;
                            _tens = WordResources.TensEnglish;
                            _scale = WordResources.ScaleNepEnglish;

                            break;
                        case OutputFormat.Unicode:
                            _ones = WordResources.OnesNep;
                            _tens = WordResources.TensNep;
                            _scale = WordResources.ScaleNep;

                            break;
                        case OutputFormat.Devnagari:
                            _ones = WordResources.OnesDevnagari;
                            _tens = WordResources.TensDevnagari;
                            _scale = WordResources.ScaleDevnagari;

                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                case Culture.International:
                    _scale = WordResources.ScaleEng;
                    _ones = WordResources.OnesEnglish;
                    _tens = WordResources.TensEnglish;
                    break;
                case Culture.Hindi:
                    switch (options.OutputFormat)
                    {
                        case OutputFormat.English:
                            _ones = WordResources.OnesEnglish;
                            _tens = WordResources.TensEnglish;
                            _scale = WordResources.ScaleNepEnglish;
                            break;
                        case OutputFormat.Unicode:
                            _ones = WordResources.OnesHindi;
                            _tens = WordResources.TensHindi;
                            _scale = WordResources.ScaleHindi;
                            break;
                        case OutputFormat.Devnagari:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(options.Culture), options.Culture, "Invalid Culture in AmountToWords");
            }

            _conversionFactory = new ConversionFactory(_options, _ones, _tens, _scale);
        }
        #endregion

        public string ToWords(decimal number)
        {
            string integralDigitsString = number
                .ToString(CultureInfo.InvariantCulture)
                .Split('.')
                .ElementAt(0);

            decimal fractionalDigits = number % 1;

            string integralWords = _conversionFactory.ConvertDigits(integralDigitsString);
            string fractionalDigitsString = fractionalDigits.ToString(_options.DecimalPlaces > -1 ? $"G{_options.DecimalPlaces}" : "G",
                CultureInfo.InvariantCulture)
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

