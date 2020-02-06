namespace NumericWordsConversion {

    using System;
    using JetBrains.Annotations;

    public static class GlobalOptions {

        [NotNull]
        public static CurrencyWordsConverter CurrencyWordsConverter { get; internal set; }

        /// <summary>Default option used for conversion if no explicit option during conversion or initialization of CurrencyWords</summary>
        [NotNull]
        public static CurrencyWordsConversionOptions CurrencyWordsOptions { get; internal set; }

        [NotNull]
        public static NumericWordsConverter NumericWordsConverter { get; internal set; }

        /// <summary>Default option used for conversion if no explicit option during conversion or initialization of NumericWords</summary>
        [NotNull]
        public static NumericWordsConversionOptions NumericWordsOptions { get; internal set; }

        static GlobalOptions() {

            //Initializing these in the ctor allows for the code lines to reformatted without causing order of initialization issues.
            CurrencyWordsOptions = new CurrencyWordsConversionOptions();
            CurrencyWordsConverter = new CurrencyWordsConverter( CurrencyWordsOptions );
            NumericWordsOptions = new NumericWordsConversionOptions();
            NumericWordsConverter = new NumericWordsConverter( NumericWordsOptions );
        }

    }

    /// <summary>Configuration Manager for Words Conversion</summary>
    public static class NumericWordsConfiguration {

        /// <summary>Configure Default Conversion Options for words conversion</summary>
        /// <param name="options">Define options for numeric or currency words</param>
        public static void ConfigureConversionDefaults( [NotNull] Action<OptionsInitializer> options ) {
            if ( options == null ) {
                throw new NullReferenceException( "Numeric Words Options Initializer Option cannot be null" );
            }

            var initializer = new OptionsInitializer();
            options.Invoke( initializer ); //TODO Using an Action here feels wrong..
        }

    }

    public class OptionsInitializer {

        //TODO This class should be static or entirely removed.

        /// <summary>Configures default options to be used while converting currency to words. Uses default options if assigned null</summary>
        /// <param name="currencyWordsOptions"></param>
        public static void SetDefaultCurrencyWordsOptions( [CanBeNull] CurrencyWordsConversionOptions currencyWordsOptions ) {
            GlobalOptions.CurrencyWordsOptions = currencyWordsOptions ?? new CurrencyWordsConversionOptions();
            GlobalOptions.CurrencyWordsConverter = new CurrencyWordsConverter( GlobalOptions.CurrencyWordsOptions );
        }

        /// <summary>Configures default options to be used while converting numeric to words. Uses default options if assigned null</summary>
        /// <param name="numericWordsOptions">Options for Numeric Words Conversion</param>
        public static void SetDefaultNumericWordsOptions( [CanBeNull] NumericWordsConversionOptions numericWordsOptions ) {
            GlobalOptions.NumericWordsOptions = numericWordsOptions ?? new NumericWordsConversionOptions();
            GlobalOptions.NumericWordsConverter = new NumericWordsConverter( GlobalOptions.NumericWordsOptions );
        }

    }

}