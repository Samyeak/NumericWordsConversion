namespace NumericWordsConversion
{

    using JetBrains.Annotations;

    public static class ExtensionHelper
    {
        [NotNull]
        public static string ToNumericWords(this decimal amount) => GlobalOptions.NumericWordsConverter.ToWords(amount);

        [NotNull]
        public static string ToCurrencyWords(this decimal amount) => GlobalOptions.CurrencyWordsConverter.ToWords(amount);

    }
}
