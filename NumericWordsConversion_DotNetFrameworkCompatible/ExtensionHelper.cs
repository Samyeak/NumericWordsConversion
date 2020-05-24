using System.Net;

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

        public static string ToCurrencyWords(this decimal amount, Culture culture, OutputFormat outputFormat = OutputFormat.English)
        {
            var currencyOptions = new CurrencyWordsConversionOptions(){
                Culture = culture,
                OutputFormat = outputFormat
            };
            var converter = new CurrencyWordsConverter(currencyOptions);   
            return converter.ToWords(amount);
        }
    }
}
