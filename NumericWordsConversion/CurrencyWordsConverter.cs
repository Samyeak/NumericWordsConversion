namespace NumericWordsConversion {
    using System.Globalization;
    using System.Linq;
    using static System.String;
    public class CurrencyWordsConverter {
        private readonly ConversionFactory _conversionFactory;
        private readonly CurrencyWordsConversionOptions _options;

        #region Constructors

        /// <summary>
        /// Creates an instance of NumberConverter with default options
        /// <br/> Culture: International, OutputFormat: English, DecimalPlaces : 2
        /// </summary>
        public CurrencyWordsConverter() {
            this._options = GlobalOptions.CurrencyWordsOptions;
            _conversionFactory = Utilities.InitializeConversionFactory( _options );
        }

        /// <summary>
        /// Creates an instance of NumberConverter with specified options
        /// </summary>
        public CurrencyWordsConverter( CurrencyWordsConversionOptions options ) {
            this._options = options;
            _conversionFactory = Utilities.InitializeConversionFactory( _options );
        }
        #endregion

        /// <summary>
        /// Converts to words as per defined option
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public string ToWords( decimal number ) {
            if ( number <= 0 ) {
                return Empty;   //TODO Add support for negative numbers.
            }

            var fractionalDigits = number % 1;
            var integralDigitsString = number
                .ToString( CultureInfo.InvariantCulture )
                .Split( '.' )
                .ElementAt( 0 );
            var fractionalDigitsString = fractionalDigits.ToString( _options.DecimalPlaces > -1 ? $"F{_options.DecimalPlaces}" : "G",
                                                 CultureInfo.InvariantCulture )
                                             .Split( '.' )
                                             .ElementAtOrDefault( 1 ) ?? Empty;
            if ( decimal.Parse( integralDigitsString ) <= 0 && decimal.Parse( fractionalDigitsString ) <= 0 ) {
                return Empty;
            }

            var integralWords = Empty;
            if ( decimal.Parse( integralDigitsString ) > 0 ) {
                integralWords = _conversionFactory.ConvertDigits( integralDigitsString );
                integralWords = _options.CurrencyNotationType == NotationType.Prefix
                    ? _options.CurrencyUnit + " " + integralWords
                    : integralWords + " " + _options.CurrencyUnit;
            }

            if ( int.Parse( fractionalDigitsString ) <= 0 || IsNullOrEmpty( fractionalDigitsString ) ) {
                return Concat( integralWords, ( IsNullOrEmpty( this._options.EndOfWordsMarker ) ? "" : " " + this._options.EndOfWordsMarker ) ).CapitalizeFirstLetter();
            }

            var fractionalWords = _conversionFactory.ConvertDigits( fractionalDigitsString );
            fractionalWords = _options.SubCurrencyNotationType == NotationType.Prefix
                ? _options.SubCurrencyUnit + " " + fractionalWords
                : fractionalWords + " " + _options.SubCurrencyUnit;

            fractionalWords = (
                $"{integralWords} " +
                $"{( IsNullOrEmpty( _options.CurrencyUnitSeparator ) ? "" : _options.CurrencyUnitSeparator + " " )}" +
                $"{fractionalWords.TrimEnd()}{( IsNullOrEmpty( _options.EndOfWordsMarker ) ? "" : " " + _options.EndOfWordsMarker )}" )
                    .Trim().CapitalizeFirstLetter();

            return fractionalWords;

        }
    }
}