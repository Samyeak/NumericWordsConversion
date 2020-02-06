namespace NumericWordsConversion {

    using System;
    using System.Collections.Concurrent;
    using System.Globalization;
    using System.Linq;
    using JetBrains.Annotations;
    using static System.String;

    public class CurrencyWordsConverter {

        /// <summary>
        /// This cache seems to have cut the UnitTests run length in half (25ms instead of 54ms).
        /// </summary>
        [NotNull]
        private ConcurrentDictionary<Decimal , String> Cache { get; } = new ConcurrentDictionary<Decimal, String>();

        [NotNull]
        private ConversionFactory ConversionFactory { get; }

        [NotNull]
        private CurrencyWordsConversionOptions Options { get; }

        /// <summary>Creates an instance of NumberConverter with specified options.
        /// <para>Default options: Culture: International, OutputFormat: English, DecimalPlaces : 2</para>
        /// </summary>
        public CurrencyWordsConverter( [CanBeNull] CurrencyWordsConversionOptions? options = null ) {
            this.Options = options ?? GlobalOptions.CurrencyWordsOptions;
            this.ConversionFactory = Utilities.InitializeConversionFactory( this.Options );
        }

        /// <summary>Converts to words as per defined option</summary>
        /// <param name="number"></param>
        [NotNull]
        public string ToWords( decimal number ) {

            //TODO Add in a ConcurrentDictionary cache, self-limiting so it doesn't eat up too much memory.
            if ( this.Cache.TryGetValue( number, out var result ) && !IsNullOrWhiteSpace( result ) ) {
                return result;
            }

            if ( number <= 0 ) {
                return Empty; //TODO Add support for negative numbers and zero!
            }

            var fractionalDigits = number % 1;
            var integralDigitsString = number.ToString( CultureInfo.InvariantCulture ).Split( '.' ).ElementAt( 0 );

            var fractionalDigitsString = fractionalDigits.ToString( this.Options.DecimalPlaces > -1 ? $"F{this.Options.DecimalPlaces}" : "G", CultureInfo.InvariantCulture )
                                             .Split( '.' ).ElementAtOrDefault( 1 ) ?? Empty;

            if ( Decimal.Parse( integralDigitsString ) <= 0 && Decimal.Parse( fractionalDigitsString ) <= 0 ) {
                return Empty;
            }

            var integralWords = Empty;

            if ( Decimal.Parse( integralDigitsString ) > 0 ) {
                integralWords = this.ConversionFactory.ConvertDigits( integralDigitsString );

                integralWords = this.Options.CurrencyNotationType == NotationType.Prefix ?
                    $"{this.Options.CurrencyUnit} {integralWords}" :
                    $"{integralWords} {this.Options.CurrencyUnit}";
            }

            if ( Int32.Parse( fractionalDigitsString ) <= 0 || IsNullOrEmpty( fractionalDigitsString ) ) {
                result = Concat( integralWords, IsNullOrEmpty( this.Options.EndOfWordsMarker ) ? "" : $" {this.Options.EndOfWordsMarker}" ).CapitalizeFirstLetter();

                return this.Cache[ number ] = result;
            }

            var fractionalWords = this.ConversionFactory.ConvertDigits( fractionalDigitsString );

            fractionalWords = this.Options.SubCurrencyNotationType == NotationType.Prefix ?
                $"{this.Options.SubCurrencyUnit} {fractionalWords}" :
                $"{fractionalWords} {this.Options.SubCurrencyUnit}";

            fractionalWords = ( $"{integralWords} " + $"{( IsNullOrEmpty( this.Options.CurrencyUnitSeparator ) ? "" : $"{this.Options.CurrencyUnitSeparator} " )}" +
                                $"{fractionalWords.TrimEnd()}{( IsNullOrEmpty( this.Options.EndOfWordsMarker ) ? "" : $" {this.Options.EndOfWordsMarker}" )}" ).Trim()
                .CapitalizeFirstLetter();

            return this.Cache[ number ] = fractionalWords;
        }

    }

}