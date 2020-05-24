namespace NumericWordsConversion
{
    /// <summary>
    /// Options to be used for converting currency number to words.
    /// </summary>
    public class CurrencyWordsConversionOptions : NumericWordsConversionOptions
    {
        /// <summary>
        /// Unit of Currency to be concatenated with output
        /// If null, uses the default unit as per specified culture if available
        /// <remarks>Example: Dollar, Rupees, रूपैंया, Pound, etc</remarks>
        /// </summary>
        public string CurrencyUnit
        {
            get
            {
                if (_currencyUnit != null) return _currencyUnit;
                WordResources.CurrencyDefaults.TryGetValue((Culture, OutputFormat), out var units);
                _currencyUnit = units.CurrencyUnit;
                return _currencyUnit;
            }
            set => _currencyUnit = value;
        }

        private string _currencyUnit;
        /// <summary>
        /// Sub Unit of Currency to be concatenated with output
        /// If null, uses the default unit as per specified culture if available
        /// <remarks>Example: Cents, Paisa, पैसा, penny, etc</remarks>
        /// </summary>
        public string SubCurrencyUnit
        {
            get
            {
                if (_subCurrencyUnit != null) return _subCurrencyUnit;
                WordResources.CurrencyDefaults.TryGetValue((Culture, OutputFormat), out var units);
                _subCurrencyUnit = units.SubCurrencyUnit;
                return _subCurrencyUnit;
            }
            set => _subCurrencyUnit = value;
        }
        private string _subCurrencyUnit;
        /// <summary>
        /// Word appended at end of the sentence.
        /// <remarks>Example: One rupees <code>only</code></remarks>
        /// </summary>
        public string EndOfWordsMarker
        {
            get
            {
                if (_endOfWordsMarker != null) return _endOfWordsMarker;
                WordResources.CurrencyDefaults.TryGetValue((Culture, OutputFormat), out var units);
                _endOfWordsMarker = units.EndOfWordsMarker;
                return _endOfWordsMarker;
            }
            set => _endOfWordsMarker = value;
        }
        private string _endOfWordsMarker;

        /// <summary>
        /// Defines whether the Currency Unit must be appended before or after the words
        /// <br/>Default: Postfix
        /// <remarks>Example: Prefix => Rupees one; Postfix => One rupees</remarks>
        /// </summary>
        public NotationType CurrencyNotationType { get; set; } = NotationType.Postfix;
        /// <summary>
        /// Defines whether the Sub Currency Unit must be appended before or after the words
        /// <br/>Default: Postfix
        /// <remarks>Example: Prefix => Paisa one; Postfix => One paisa</remarks>
        /// </summary>
        public NotationType SubCurrencyNotationType { get; set; } = NotationType.Postfix;
        /// <summary>
        /// Currency Unit Separator
        /// <br/>Default: Empty
        /// <remarks>Example: One rupees <code>and</code> ten paisa only</remarks>
        /// </summary>
        public string CurrencyUnitSeparator { get; set; } = string.Empty;
    }
}
