namespace NumericWordsConversion
{
    /// <summary>
    /// Options to be used for converting number to words.
    /// <br/>For currency please use CurrencyWordsConversionOptions
    /// </summary>
    public class CurrencyWordsConversionOptions : WordsConversionOptions
    {
        public string CurrencyUnit { get; set; }
        public string SubCurrencyUnit { get; set; }
        public NotationType CurrencyNotationType { get; set; } = NotationType.Postfix;
        public NotationType SubCurrencyNotationType { get; set; } = NotationType.Postfix;
        public string EndOfWordsMarker { get; set; }
    }
}
