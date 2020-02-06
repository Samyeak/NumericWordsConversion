namespace NumericWordsConversion {

    using System;

    /// <summary>Options to be used for converting currency number to words.</summary>
    public class CurrencyWordsConversionOptions : NumericWordsConversionOptions {

        private string? _currencyUnit;

        private string? _endOfWordsMarker;

        private string? _subCurrencyUnit;

        public int CacheLimit { get; set; } = 1024 * 1024;

        /// <summary>Defines whether the Currency Unit must be appended before or after the words <br />Default: Postfix
        /// <remarks>Example: Prefix => Rupees one; Postfix => One rupees</remarks>
        /// </summary>
        public NotationType CurrencyNotationType { get; set; } = NotationType.Postfix;

        /// <summary>Unit of Currency to be concatenated with output If null, uses the default unit as per specified culture if available
        /// <remarks>Example: Dollar, Rupees, रूपैंया, Pound, etc</remarks>
        /// </summary>
        public string CurrencyUnit {
            get {
                if ( this._currencyUnit != null ) {
                    return this._currencyUnit;
                }

                WordResources.CurrencyDefaults.TryGetValue( ( this.Culture, this.OutputFormat ), out var units );
                this._currencyUnit = units.CurrencyUnit;

                return this._currencyUnit;
            }

            set => this._currencyUnit = value;
        }

        /// <summary>Currency Unit Separator <br />Default: Empty
        /// <remarks>Example: One rupees <code>and</code> ten paisa only</remarks>
        /// </summary>
        public string CurrencyUnitSeparator { get; set; } = String.Empty;

        /// <summary>Word appended at end of the sentence.
        /// <remarks>Example: One rupees <code>only</code></remarks>
        /// </summary>
        public string EndOfWordsMarker {
            get {
                if ( this._endOfWordsMarker != null ) {
                    return this._endOfWordsMarker;
                }

                WordResources.CurrencyDefaults.TryGetValue( ( this.Culture, this.OutputFormat ), out var units );
                this._endOfWordsMarker = units.EndOfWordsMarker;

                return this._endOfWordsMarker;
            }

            set => this._endOfWordsMarker = value;
        }

        /// <summary>Defines whether the Sub Currency Unit must be appended before or after the words <br />Default: Postfix
        /// <remarks>Example: Prefix => Paisa one; Postfix => One paisa</remarks>
        /// </summary>
        public NotationType SubCurrencyNotationType { get; set; } = NotationType.Postfix;

        /// <summary>Sub Unit of Currency to be concatenated with output If null, uses the default unit as per specified culture if available
        /// <remarks>Example: Cents, Paisa, पैसा, penny, etc</remarks>
        /// </summary>
        public string SubCurrencyUnit {
            get {
                if ( this._subCurrencyUnit != null ) {
                    return this._subCurrencyUnit;
                }

                WordResources.CurrencyDefaults.TryGetValue( ( this.Culture, this.OutputFormat ), out var units );
                this._subCurrencyUnit = units.SubCurrencyUnit;

                return this._subCurrencyUnit;
            }

            set => this._subCurrencyUnit = value;
        }

    }

}