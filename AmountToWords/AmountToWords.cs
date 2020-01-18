namespace AmountToWordsHelper
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    public class AmountToWords
    {
        private readonly string _amtUnit;
        private readonly string _subAmtUnit;
        private readonly string _postfix;
        private readonly Culture _culture;
        private readonly OutputFormat _outputFormat;


        private readonly string[] _ones;
        private readonly string[] _tens;
        private readonly string[] _scale;


        public enum OutputFormat
        {
            English,
            Devnagari,
            Unicode
        }
        public enum Culture
        {
            International,
            Nepali,
            Hindi
        }
        private int GetResourceLimitIndex(OutputFormat outputFormat) => outputFormat == OutputFormat.English ? 20 : 100;

        public AmountToWords(Culture culture) : this(culture, OutputFormat.English) { }
        public AmountToWords() : this(Culture.International, OutputFormat.English) { }

        public AmountToWords(Culture culture, OutputFormat outputFormat)
        {
            _culture = culture;
            switch (culture)
            {
                case Culture.Nepali:
                    switch (outputFormat)
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
                    switch (outputFormat)
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
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(culture), culture, "Invalid Culture in AmountToWords");
            }
        }
        public AmountToWords(Culture culture, string amountUnit, string subAmountUnit) : this(culture)
        {
            _amtUnit = amountUnit;
            _subAmtUnit = subAmountUnit;
        }

        /// <summary>
        /// Converts to amount in words of specified culture
        /// </summary>
        /// <param name="amt"></param>
        /// <returns></returns>
        public string ConvertToWords(decimal amt)
        {
            if (amt <= 0) return string.Empty;

            StringBuilder wordBuilder = new StringBuilder();
            string inWords;

            string[] amount = $"{amt:F2}".Split('.');
            string number = amount[0];
            {
                int scaleMapIndex = 0;
                string wholeNumber = number;
                if (number != "0")
                    if (_culture == Culture.International)
                        scaleMapIndex = (int)Math.Ceiling((decimal)number.Length / 3);
                    else
                        scaleMapIndex = (number.Length - 3) < 1 ? 1 : number.Length / 2;

                for (int i = scaleMapIndex; i >= 0; i--)
                {
                    switch (i)
                    {
                        case 0: //For the Decimals
                            string paisa = amount[1];
                            if (Convert.ToInt16(paisa, CultureInfo.InvariantCulture) > 0)
                                wordBuilder.Append(" " + ToTensWord(paisa, _outputFormat) + " " + _subAmtUnit);
                            break;
                        case 1: //For the Hundreds, tens and ones
                            inWords = ToHundredthWords(wholeNumber, _outputFormat);
                            if (!string.IsNullOrEmpty(inWords))
                                wordBuilder.Append(string.Concat(inWords.Trim(), " "));
                            wordBuilder.Append(_amtUnit);
                            break;
                        default: //For Everything Greater than hundreds
                            if (_culture == Culture.International)
                            {
                                int lastIndex = (wholeNumber.Length % ((i - 1) * 3 + 1)) + 1;
                                string hundreds = wholeNumber.Substring(0, lastIndex);
                                wholeNumber = wholeNumber.Remove(0, lastIndex);
                                inWords = ToHundredthWords(hundreds, _outputFormat);
                            }
                            else
                            {
                                int length = (wholeNumber.Length % 2 == 0) ? 1 : 2;
                                string digits = wholeNumber.Substring(0, length);
                                wholeNumber = wholeNumber.Remove(0, length);
                                inWords = ToTensWord(digits, _outputFormat);
                            }

                            if (!string.IsNullOrEmpty(inWords.Trim()))
                                wordBuilder.Append(string.Concat(inWords.Trim(), " ", _scale[i], " "));
                            break;
                    }
                }
                inWords = wordBuilder.ToString();
            }
            inWords = string.Concat(inWords, " ", _postfix);

            return CapitalizeFirstLetter(inWords.Trim());

            //Local Functions
            string CapitalizeFirstLetter(string words)
            {
                if (words == null) throw new ArgumentNullException(nameof(words), "Amount in words must not be null");
                if (string.IsNullOrEmpty(words)) throw new ArgumentException("Amount in words cannot be empty");
                return words.First().ToString(CultureInfo.InvariantCulture).ToUpper(CultureInfo.InvariantCulture) + words.Substring(1);
            }
        }

        private string ToTensWord(string tenth, OutputFormat outputFormat)
        {
            int dec = Convert.ToInt16(tenth, CultureInfo.InvariantCulture);
            if (dec <= 0) return string.Empty;
            string words;

            if (dec < GetResourceLimitIndex(outputFormat))
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

            inWords += ToTensWord(hundred, outputFormat);

            return inWords.Trim();
        }

    }

}
