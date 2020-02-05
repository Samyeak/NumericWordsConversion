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
            this._conversionFactory = Utilities.InitializeConversionFactory( this._options );
        }

        /// <summary>
        /// Creates an instance of NumberConverter with specified options
        /// </summary>
        public CurrencyWordsConverter( CurrencyWordsConversionOptions options ) {
            this._options = options;
            this._conversionFactory = Utilities.InitializeConversionFactory( this._options );
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
            var fractionalDigitsString = fractionalDigits.ToString( this._options.DecimalPlaces > -1 ? $"F{this._options.DecimalPlaces}" : "G",
                                                 CultureInfo.InvariantCulture )
                                             .Split( '.' )
                                             .ElementAtOrDefault( 1 ) ?? Empty;
            if ( decimal.Parse( integralDigitsString ) <= 0 && decimal.Parse( fractionalDigitsString ) <= 0 ) {
                return Empty;
            }

            var integralWords = Empty;
            if ( decimal.Parse( integralDigitsString ) > 0 ) {
                integralWords = this._conversionFactory.ConvertDigits( integralDigitsString );
                integralWords = this._options.CurrencyNotationType == NotationType.Prefix
                    ?
                    this._options.CurrencyUnit + " " + integralWords
                    : integralWords + " " + this._options.CurrencyUnit;
            }

            if ( int.Parse( fractionalDigitsString ) <= 0 || IsNullOrEmpty( fractionalDigitsString ) ) {
                return Concat( integralWords, ( IsNullOrEmpty( this._options.EndOfWordsMarker ) ? "" : " " + this._options.EndOfWordsMarker ) ).CapitalizeFirstLetter();
            }

            var fractionalWords = this._conversionFactory.ConvertDigits( fractionalDigitsString );
            fractionalWords = this._options.SubCurrencyNotationType == NotationType.Prefix
                ?
                this._options.SubCurrencyUnit + " " + fractionalWords
                : fractionalWords + " " + this._options.SubCurrencyUnit;

            fractionalWords = (
                $"{integralWords} " +
                $"{( IsNullOrEmpty( this._options.CurrencyUnitSeparator ) ? "" : this._options.CurrencyUnitSeparator + " " )}" +
                $"{fractionalWords.TrimEnd()}{( IsNullOrEmpty( this._options.EndOfWordsMarker ) ? "" : " " + this._options.EndOfWordsMarker )}" )
                    .Trim().CapitalizeFirstLetter();

            return fractionalWords;

        }
    }
}