namespace NumericWordsConversion {

    using System;
    using System.ComponentModel;
    using JetBrains.Annotations;

    /// <summary>Options to be used for converting number to words. <br />For currency please use CurrencyWordsConversionOptions</summary>
    public class NumericWordsConversionOptions {

        [CanBeNull]
        private readonly string? _decimalSeparator;

        /// <summary>In order to use generic algorithm for all the numeral system, this is used to map suitable resources as per different numeral system.</summary>
        internal int ResourceLimitIndex => this.OutputFormat == OutputFormat.English ? 20 : 100;

        /// <summary>The culture to be used in order to convert the number to words. Default value: International<br /></summary>
        public Culture Culture { get; set; }

        /// <summary>
        /// Number of digits after decimal point.<br /> Default Value: 2<br /> Assign -1 to support all provided decimal values <br /> Excessive values than the specified digits will
        /// be truncated
        /// </summary>
        public int DecimalPlaces { get; set; }

        //International English

        /// <summary>Has control over the string format of the output words.
        /// <para>Default value: English</para>
        /// <para>Output Format depends upon the culture specified.</para>
        /// </summary>
        public OutputFormat OutputFormat { get; set; }

        public const String DefaultDecimalSeparator = "point"!;

        public NumericWordsConversionOptions( OutputFormat? format = null, Culture? culture = null, int? decimalPlaces = 2, [CanBeNull] String? defaultDecimalSeparator = null ) {
            if ( format != null && !Enum.IsDefined( enumType: typeof( OutputFormat ), value: format.Value ) ) {
                throw new InvalidEnumArgumentException( argumentName: nameof( format ), invalidValue: ( Int32 ) format, enumClass: typeof( OutputFormat ) );
            }

            if ( culture != null && !Enum.IsDefined( enumType: typeof( Culture ), value: culture.Value ) ) {
                throw new InvalidEnumArgumentException( argumentName: nameof( culture ), invalidValue: ( Int32 ) culture, enumClass: typeof( Culture ) );
            }

            this.OutputFormat = format ?? OutputFormat.English;
            this.Culture = culture ?? Culture.International;

            this.DecimalPlaces = decimalPlaces ?? 2;

            if ( this.DecimalPlaces < 0 ) {
                this.DecimalPlaces = 0;
            }
            else if ( this.DecimalPlaces > 10 ) {
                this.DecimalPlaces = 10; //or some other *reasonable* limit.
            }

            this._decimalSeparator = defaultDecimalSeparator ?? DefaultDecimalSeparator;
        }

        /// <summary>Separator to be placed between numbers and their decimal values.
        /// <para>Default value differs with Culture and Output Format.</para>
        /// <para>Assign an empty string to ignore the separator.</para>
        /// <para>Uses default separator if null.</para>
        /// </summary>
        [NotNull]
        public String GetDecimalSeparator() {
            var s = this._decimalSeparator;

            if ( s != null ) {
                return s;
            }

            return ( this.Culture, this.OutputFormat ) switch {
                (Culture.International, OutputFormat.English ) => "point", //changed so if the enum order is changed in the future, then this still defaults to English.
                (Culture.Nepali, OutputFormat.English ) => "point",
                (Culture.Nepali, OutputFormat.Devnagari ) => "bzdnj",
                (Culture.Nepali, OutputFormat.Unicode ) => "दशमलव",
                (Culture.Hindi, OutputFormat.English ) => "point",
                (Culture.Hindi, OutputFormat.Devnagari ) => "bzdnj",
                (Culture.Hindi, OutputFormat.Unicode ) => "दशमलव",
                _ => throw new ArgumentOutOfRangeException( $"Combination of {nameof( this.Culture )} {this.Culture:G} format {this.OutputFormat:G} is unknown." )
            };
        }

    }

}