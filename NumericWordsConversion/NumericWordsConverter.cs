namespace NumericWordsConversion {

    using System;
    using System.Collections.Concurrent;
    using System.Globalization;
    using System.Linq;
    using JetBrains.Annotations;
    using static System.String;

    /// <summary>Used to convert number to words
    /// <remarks>For Currency: Use <code>CurrencyWordsConverter</code></remarks>
    /// ></summary>
    public class NumericWordsConverter {

        private readonly ConversionFactory _conversionFactory;

        private readonly NumericWordsConversionOptions _options;

        private ConcurrentDictionary<Decimal, String> Cache { get; } = new ConcurrentDictionary<Decimal, String>();

        /// <summary>Creates an instance of NumberConverter with default options.
        /// <para>Culture: International, OutputFormat: English, DecimalPlaces : 2</para>
        /// </summary>
        public NumericWordsConverter() {
            this._options = GlobalOptions.NumericWordsOptions;
            this._conversionFactory = Utilities.InitializeConversionFactory( this._options );
        }

        /// <summary>Creates an instance of NumberConverter with specified options</summary>
        public NumericWordsConverter( [NotNull] NumericWordsConversionOptions options ) {
            this._options = options ?? throw new ArgumentNullException( nameof( options ) );
            this._conversionFactory = Utilities.InitializeConversionFactory( this._options );
        }

        /// <summary>Convert number to words as per specified options</summary>
        /// <param name="number"></param>
        [NotNull]
        public string ToWords( decimal number ) {
            if ( this.Cache.TryGetValue( number, out var result ) ) {
                return result;
            }

            var integralDigitsString = number.ToString( CultureInfo.InvariantCulture ).Split( '.' ).ElementAt( 0 );

            var fractionalDigits = number % 1;

            var integralWords = this._conversionFactory.ConvertDigits( integralDigitsString );

            var fractionalDigitsString =
                ( this._options.DecimalPlaces > -1 ?
                    Decimal.Parse( fractionalDigits.ToString( $"F{this._options.DecimalPlaces}", CultureInfo.InvariantCulture ) )
                        .ToString( $"G{this._options.DecimalPlaces}", CultureInfo.InvariantCulture ) :
                    fractionalDigits.ToString( "G", CultureInfo.InvariantCulture ) ).Split( '.' ).ElementAtOrDefault( 1 );

            if ( fractionalDigits <= 0 || IsNullOrEmpty( fractionalDigitsString ) ) {
                return integralWords.CapitalizeFirstLetter();
            }

            var fractionalWords = Empty;

            fractionalDigitsString.ToList().ForEach( x =>
                fractionalWords += $"{this._conversionFactory.ToOnesWords( Convert.ToUInt16( x.ToString( CultureInfo.InvariantCulture ) ) )} " );

            result = $"{integralWords} {this._options.GetDecimalSeparator()} {fractionalWords.TrimEnd()}".CapitalizeFirstLetter();

            return this.Cache[ number ] = result;
        }

    }

}