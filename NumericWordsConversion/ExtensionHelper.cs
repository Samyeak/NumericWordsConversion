namespace NumericWordsConversion
{
    public static class ExtensionHelper
    {
        public static string ToNumericWords(this decimal amount) => GlobalOptions.NumericWordsConverter.ToWords(amount);

        public static string ToCurrencyWords(this decimal amount) => GlobalOptions.CurrencyWordsConverter.ToWords(amount);

    }
}
