using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace NumericWordsConversion
{
    public class NumberConverter
    {
        private readonly WordsConversionOptions _options;
        private string[] _ones;
        private string[] _tens;
        private string[] _scale;

        //TODO: MANAGE; make readonly
        private OutputFormat _outputFormat;
        private string _amtUnit;
        private string _subAmtUnit;
        private string _postfix;


        #region Constructors
        /// <summary>
        /// Creates an instance of NumberConverter with default options
        /// <br/> Culture: International, OutputFormat: English, DecimalPlaces : 2
        /// </summary>
        public NumberConverter() : this(new WordsConversionOptions()) { }

        /// <summary>
        /// Creates an instance of NumberConverter with specified options
        /// </summary>
        public NumberConverter(WordsConversionOptions options)
        {
            this._options = options;
            AssignResource(_options);

            void AssignResource(WordsConversionOptions opts)
            {
                switch (opts.Culture)
                {
                    case Culture.Nepali:
                        switch (opts.OutputFormat)
                        {
                            case OutputFormat.English:
                                _amtUnit = "rupees";
                                _subAmtUnit = "paisa";
                                _postfix = "only";

                                _ones = WordResources.OnesEnglish;
                                _tens = WordResources.TensEnglish;
                                _scale = WordResources.ScaleNepEnglish;

                                break;
                            case OutputFormat.Unicode:
                                _outputFormat = OutputFormat.Unicode;
                                _ones = WordResources.OnesNep;
                                _tens = WordResources.TensNep;
                                _scale = WordResources.ScaleNep;

                                _amtUnit = "रूपैयाँ";
                                _subAmtUnit = "पैसा";
                                _postfix = "मात्र";
                                break;
                            case OutputFormat.Devnagari:
                                _outputFormat = OutputFormat.Devnagari;
                                _ones = WordResources.OnesDevnagari;
                                _tens = WordResources.TensDevnagari;
                                _scale = WordResources.ScaleDevnagari;

                                _amtUnit = "¿k}ofF";
                                _subAmtUnit = "k};f";
                                _postfix = "dfq";
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                        break;
                    case Culture.International:
                        _outputFormat = OutputFormat.English;
                        _scale = WordResources.ScaleEng;
                        _amtUnit = "rupees";
                        _subAmtUnit = "paisa";
                        _postfix = "only";

                        _ones = WordResources.OnesEnglish;
                        _tens = WordResources.TensEnglish;
                        break;
                    case Culture.Hindi:
                        switch (opts.OutputFormat)
                        {
                            case OutputFormat.English:
                                _amtUnit = "rupees";
                                _subAmtUnit = "paisa";
                                _postfix = "only";

                                _ones = WordResources.OnesEnglish;
                                _tens = WordResources.TensEnglish;
                                _scale = WordResources.ScaleNepEnglish;

                                break;
                            case OutputFormat.Unicode:
                                _outputFormat = OutputFormat.Unicode;
                                _ones = WordResources.OnesHindi;
                                _tens = WordResources.TensHindi;
                                _scale = WordResources.ScaleHindi;

                                _amtUnit = "रुपये";
                                _subAmtUnit = "पैसा";
                                _postfix = "मात्र";
                                break;
                            case OutputFormat.Devnagari:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(opts.Culture), opts.Culture, "Invalid Culture in AmountToWords");
                }
            }
        }

        #endregion



        public string ToWords(decimal number)
        {
            string integralDigits = ((int)number).ToString(CultureInfo.InvariantCulture); //stringArray.ElementAt(0),
            decimal fractionalDigits = number % 1;
            /* == Alternative Approach
                string[] stringArray = number.ToString(CultureInfo.InvariantCulture).Split('.');
                string integralDigits = stringArray.ElementAt(0),
                string fractionalDigits = stringArray.ElementAtOrDefault(1).ToString(":F2") ?? string.Empty;
             */

            StringBuilder wordBuilder = new StringBuilder();

            string integralWords = ConvertDigits(integralDigits);

            if (fractionalDigits <= 0) return integralWords;

            string fractionalDigitsString = fractionalDigits
                .ToString(_options.DecimalPlaces > -1 ? $"F{_options.DecimalPlaces}" : "G", CultureInfo.InvariantCulture)
                .Split('.')
                .ElementAt(1);
            string fractionalWords = ConvertDigits(fractionalDigitsString);
            return $"{integralWords} {_options.DecimalSeparator} {fractionalWords}";

            string ConvertDigits(string digits)
            {
                StringBuilder builder = new StringBuilder();
                int scaleMapIndex;
                if (_options.Culture == Culture.International)
                    scaleMapIndex = (int)Math.Ceiling((decimal)digits.Length / 3);
                else
                    scaleMapIndex = (digits.Length - 3) < 1 ? 1 : digits.Length / 2;
                for (int i = scaleMapIndex; i > 0; i--)
                {
                    string inWords;
                    switch (i)
                    {
                        case 1: //For the Hundreds, tens and ones
                            inWords = ToHundredthWords(digits, _outputFormat);
                            if (!string.IsNullOrEmpty(inWords))
                                builder.Append(string.Concat(inWords.Trim(), " "));
                            break;
                        default: //For Everything Greater than hundreds
                            if (_options.Culture == Culture.International)
                            {
                                int length = (digits.Length % ((i - 1) * 3 + 1)) + 1;
                                string hundreds = digits.Substring(0, length);
                                digits = digits.Remove(0, length);
                                inWords = ToHundredthWords(hundreds, _outputFormat);
                            }
                            else
                            {
                                int length = (digits.Length % 2 == 0) ? 1 : 2;
                                string hundreds = digits.Substring(0, length);
                                digits = digits.Remove(0, length);
                                inWords = ToTensWord(hundreds);
                            }

                            if (!string.IsNullOrEmpty(inWords.Trim()))
                                builder.Append(string.Concat(inWords.Trim(), " ", _scale[i], " "));
                            break;
                    }
                }
                return builder.ToString().Trim();
            }
        }


        private string ToTensWord(string tenth)
        {
            int dec = Convert.ToInt16(tenth, CultureInfo.InvariantCulture);
            if (dec <= 0) return string.Empty;
            string words;

            if (dec < _options.ResourceLimitIndex)
            {
                words = _ones[dec];
            }
            else
            {
                if (dec % 10 == 0)
                {
                    words = _tens[dec / 10];
                }
                else
                {
                    int first = Convert.ToInt16(tenth.Substring(0, 1), CultureInfo.InvariantCulture);
                    int second = Convert.ToInt16(tenth.Substring(1, 1), CultureInfo.InvariantCulture);
                    words = string.Concat(_tens[first], " ", _ones[second]);
                }
            }

            return words;
        }

        private string ToHundredthWords(string hundred, OutputFormat outputFormat)
        {
            string inWords = string.Empty;
            if (hundred.Length == 3)
            {
                int hundredth = Convert.ToInt16(hundred.Substring(0, 1), CultureInfo.InvariantCulture);
                inWords = hundredth > 0 ? string.Concat(_ones[hundredth], " ", _scale[1], " ") : string.Empty;
                hundred = hundred.Substring(1, 2);
            }

            inWords += ToTensWord(hundred);

            return inWords.Trim();
        }

        void Test()
        {
            new WordsConversionOptions()
            {
                OutputFormat = OutputFormat.Devnagari,
            };
        }
    }
}
