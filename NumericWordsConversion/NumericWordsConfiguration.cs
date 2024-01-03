using System;

namespace NumericWordsConversion
{
    /// <summary>
    /// Configuration Manager for Words Conversion
    /// </summary>
    public static class NumericWordsConfiguration
    {
        /// <summary>
        /// Configure Default Conversion Options for words conversion
        /// </summary>
        /// <param name="options">Define options for numeric or currency words</param>
        public static void ConfigureConversionDefaults(Action<OptionsInitializer> options)
        {
            if (options == null) throw new NullReferenceException("Numeric Words Options Initializer Option cannot be null");

            OptionsInitializer initializer = new OptionsInitializer();
            options.Invoke(initializer);
        }

    }
    public static class GlobalOptions
    {
        /// <summary>
        /// Default option used for conversion if no explicit option during conversion or initialization of NumericWords
        /// </summary>
        public static NumericWordsConversionOptions NumericWordsOptions { get; internal set; } = new NumericWordsConversionOptions();
        /// <summary>
        /// Default option used for conversion if no explicit option during conversion or initialization of CurrencyWords
        /// </summary>
        public static CurrencyWordsConversionOptions CurrencyWordsOptions { get; internal set; } = new CurrencyWordsConversionOptions();

        public static NumericWordsConverter NumericWordsConverter { get; internal set; } = new NumericWordsConverter(NumericWordsOptions);

        public static CurrencyWordsConverter CurrencyWordsConverter { get; internal set; } = new CurrencyWordsConverter(CurrencyWordsOptions);
    }

    public class OptionsInitializer
    {

        /// <summary>
        /// Configures default options to be used while converting numeric to words.
        /// Uses default options if assigned null
        /// </summary>
        /// <param name="numericWordsOptions">Options for Numeric Words Conversion</param>
        public void SetDefaultNumericWordsOptions(NumericWordsConversionOptions numericWordsOptions)
        {
            GlobalOptions.NumericWordsOptions = numericWordsOptions ?? new NumericWordsConversionOptions();
            GlobalOptions.NumericWordsConverter = new NumericWordsConverter(GlobalOptions.NumericWordsOptions);
        }

        /// <summary>
        /// Configures default options to be used while converting currency to words.
        /// Uses default options if assigned null
        /// </summary>
        /// <param name="currencyWordsOptions"></param>
        public void SetDefaultCurrencyWordsOptions(CurrencyWordsConversionOptions currencyWordsOptions)
        {
            GlobalOptions.CurrencyWordsOptions = currencyWordsOptions ?? new CurrencyWordsConversionOptions();
            GlobalOptions.CurrencyWordsConverter = new CurrencyWordsConverter(GlobalOptions.CurrencyWordsOptions);
        }
    }
}
