namespace NumericWordsConversion
{
    public static class ExtensionHelper
    {
        public static string ToNumericWords(this decimal amount)
        {
            return GlobalOptions.NumericWordsConverter.ToWords(amount);
        }

        public static string ToCurrencyWords(this decimal amount)
        {
            return GlobalOptions.CurrencyWordsConverter.ToWords(amount);
        }

    }
}
