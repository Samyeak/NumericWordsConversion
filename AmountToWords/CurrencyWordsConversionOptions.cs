using System;
using System.Diagnostics;

namespace NumericWordsConversion
{
    /// <summary>
    /// Options to be used for converting number to words.
    /// <br/>For currency please use CurrencyWordsConversionOptions
    /// </summary>
    public class CurrencyWordsConversionOptions : WordsConversionOptions
    {
        public CurrencyWordsConversionOptions()
        {
        }
        public string CurrencyUnit
        {
            get
            {
                if (_currencyUnit == null)
                {
                    WordResources.CurrencyDefaults.TryGetValue((Culture, OutputFormat), out var units);
                    _currencyUnit = units.CurrencyUnit;
                }
                return _currencyUnit;
            }
            set => _currencyUnit = value;
        }

        private string _currencyUnit;
        public string SubCurrencyUnit
        {
            get
            {
                if (_subCurrencyUnit == null)
                {
                    WordResources.CurrencyDefaults.TryGetValue((Culture, OutputFormat), out var units);
                    _subCurrencyUnit = units.SubCurrencyUnit;
                }
                return _subCurrencyUnit;
            }
            set => _subCurrencyUnit = value;
        }
        private string _subCurrencyUnit;
        public string EndOfWordsMarker
        {
            get
            {
                if (_endOfWordsMarker == null)
                {
                    WordResources.CurrencyDefaults.TryGetValue((Culture, OutputFormat), out var units);
                    _endOfWordsMarker = units.EndOfWordsMarker;
                }
                return _endOfWordsMarker;
            }
            set => _endOfWordsMarker = value;
        }
        private string _endOfWordsMarker;

        public NotationType CurrencyNotationType { get; set; } = NotationType.Postfix;
        public NotationType SubCurrencyNotationType { get; set; } = NotationType.Postfix;
        public string CurrencyUnitSeparator { get; set; } = string.Empty;
    }
}
