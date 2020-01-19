namespace NumericWordsConversion
{
    /// <summary>
    /// Options to be used for converting number to words.
    /// <br/>For currency please use CurrencyWordsConversionOptions
    /// </summary>
    public class CurrencyWordsConversionOptions
    {
        /// <summary>
        /// The culture to be used in order to convert the number to words.
        /// Default value: International<br/>
        /// </summary>
        public Culture Culture { get; set; } = Culture.International;
        /// <summary>
        /// Has control over the string format of the output words.
        /// <br/>Default value: English
        /// <br/>Output Format depends upon the culture specified.
        /// </summary>
        public OutputFormat OutputFormat { get; set; } = OutputFormat.English;
        /// <summary>
        /// Number of digits after decimal point.<br/>
        /// Default Value: 2<br/>
        /// Assign -1 to support all provided decimal values <br/>
        /// Excessive values than the specified digits will be truncated
        /// </summary>
        public int DecimalPlaces { get; set; } = 2;
        /// <summary>
        /// Separator to be placed between numbers and their decimal values<br/>
        /// Default value differs wrt Culture and Output Format<br/>
        /// Assign an empty string to ignore the separator
        /// </summary>
        public string DecimalSeparator { get; set; }
    }
    public class CurrencyConverter
    {
    }
}
