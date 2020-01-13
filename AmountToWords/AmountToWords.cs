using System.Drawing;

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

        /**
         * Number Notation has been added up to 10^39
         * Decimal type supports up to 10^27
         */
        private readonly string[] scale = { "", "hundred", "thousand", "lakh", "crore", "arba", "kharba", "neel", "padma", "shankha", "Upadh", "Anka", "Jald", "Madh", "Parardha", "Anta", "Mahaanta", "Shishanta", "Singhar", "Maha Singhar", "Adanta Singhar" }; //Pow(10,39)
        private readonly string[] scaleNep = { "", "सय", "हजार", "लाख", "करोड", "अरब", "खरब", "नील", "पद्म", "शंख", "उपाध", "अंक", "जल्द", "मध", "परर्ध", "अन्त", "महाअन्त", "शिशन्त", "सिंघर", "महासिंहर", "अदन्त सिंहर" };
        private readonly string[] scaleEng = { "", "hundred", "thousand", "million", "billion", "trillion", "quadrillion", "quintillion", "sextillion", "septillion", "octillion" , "nonillion", "decillion", "undecillion", "duodecillion " };

        //ENGLISH WORDS RESOURCE
        private readonly string[] ones = { "", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        private readonly string[] tens = { "", "", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

        //NEPALI WORDS RESOURCE
        private readonly string[] tensNep = { "", "दस", "बीस", "तीस", "चालीस", "पचास", "साठी", "सतरी", "अस्सी", "नब्बे" };
        private readonly string[] onesNep = new[]{ "सुन्य", "एक", "दुई", "तीन", "चार", "पाँच", "छ", "सात", "आठ", "नौ", "दस",
                "एघार", "बाह्र", "तेह्र", "चौध", "पन्ध्र", "सोह्र", "सत्र", "अठाह्र", "उन्नाइस", "बीस", "एकाइस",
                "बाइस", "तेइस", "चौबीस", "पचीस", "छब्बीस", "सत्ताइस", "अठ्ठाइस", "उनन्तीस", "तीस",
                "एकतीस", "बतीस", "तेतीस", "चौतीस", "पैतीस", "छतीस", "सरतीस", "अरतीस", "उननचालीस", "चालीस",
                "एकचालीस", "बयालिस", "तीरचालीस", "चौवालिस", "पैंतालिस", "छयालिस", "सरचालीस", "अरचालीस", "उननचास", "पचास",
                "एकाउन्न", "बाउन्न", "त्रिपन्न", "चौवन्न", "पच्पन्न", "छपन्न", "सन्ताउन्न", "अन्ठाउँन्न", "उनान्न्साठी ", "साठी",
                "एकसाठी", "बासाठी", "तीरसाठी", "चौंसाठी", "पैसाठी", "छैसठी", "सत्सठ्ठी", "अर्सठ्ठी", "उनन्सत्तरी", "सतरी",
                "एकहत्तर", "बहत्तर", "त्रिहत्तर", "चौहत्तर", "पचहत्तर", "छहत्तर", "सत्हत्तर", "अठ्हत्तर", "उनास्सी", "अस्सी",
                "एकासी", "बयासी", "त्रीयासी", "चौरासी", "पचासी", "छयासी", "सतासी", "अठासी", "उनान्नब्बे", "नब्बे",
                "एकान्नब्बे", "बयान्नब्बे", "त्रियान्नब्बे", "चौरान्नब्बे", "पंचान्नब्बे", "छयान्नब्बे", "सन्तान्‍नब्बे", "अन्ठान्नब्बे", "उनान्सय"
            };

        public enum OutputFormat
        {
            English,
            Devnagari,
            Unicode
        }
        public enum Culture
        {
            English,
            Nepali
        }
        private int GetResourceLimitIndex(OutputFormat outputFormat) => outputFormat == OutputFormat.English ? 20 : 100;

        public AmountToWords(Culture culture) : this(culture, OutputFormat.English) { }
        public AmountToWords() : this(Culture.English, OutputFormat.English) { }

        public AmountToWords(Culture culture, OutputFormat outputFormat)
        {
            _culture = culture;
            switch (culture)
            {
                case Culture.Nepali:
                    if (outputFormat == OutputFormat.English)
                    {
                        _amtUnit = "rupees";
                        _subAmtUnit = "paisa";
                        _postfix = "only";
                    }
                    else
                    {
                        ones = onesNep;
                        tens = tensNep;
                        scale = scaleNep;
                        _outputFormat = OutputFormat.Unicode;

                        _amtUnit = "रूपैयाँ";
                        _subAmtUnit = "पैसा";
                        _postfix = "मात्र";
                    }
                    break;
                case Culture.English:
                    _outputFormat = OutputFormat.English;
                    scale = scaleEng;
                    _amtUnit = "rupees";
                    _subAmtUnit = "paisa";
                    _postfix = "only";
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
            ValidateInputDecimal(amt, _culture);
            if (amt <= 0) return string.Empty;

            StringBuilder wordBuilder = new StringBuilder();
            string inWords;

            string[] amount = $"{amt:F2}".Split('.');
            string number = amount[0];
            {
                int scaleMapIndex = 0;
                string wholeNumber = number;
                if (number != "0")
                    if (_culture == Culture.English)
                        scaleMapIndex = (int)Math.Ceiling((decimal)number.Length / 3);
                    else
                        scaleMapIndex = (number.Length - 3) < 1 ? 1 : (int)Math.Floor((decimal)(number.Length) / 2);

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
                            if (_culture == Culture.English)
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
                                wordBuilder.Append(string.Concat(inWords.Trim(), " ", scale[i], " "));
                            break;
                    }
                }
                inWords = wordBuilder.ToString();
            }
            inWords = string.Concat(inWords, " ", _postfix);

            return CapitalizeFirstLetter(inWords.Trim());
        }


        private string ToTensWord(string tenth, OutputFormat outputFormat)
        {
            int dec = Convert.ToInt16(tenth, CultureInfo.InvariantCulture);
            if (dec <= 0) return string.Empty;
            string words;

            if (dec < GetResourceLimitIndex(outputFormat))
            {
                words = ones[dec];
            }
            else
            {
                if (dec % 10 == 0)
                {
                    words = tens[dec / 10];
                }
                else
                {
                    int first = Convert.ToInt16(tenth.Substring(0, 1), CultureInfo.InvariantCulture);
                    int second = Convert.ToInt16(tenth.Substring(1, 1), CultureInfo.InvariantCulture);
                    words = string.Concat(tens[first], " ", ones[second]);
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
                inWords = hundredth > 0 ? string.Concat(ones[hundredth], " ", scale[1], " ") : string.Empty;
                hundred = hundred.Substring(1, 2);
            }

            inWords += ToTensWord(hundred, outputFormat);

            return inWords.Trim();
        }

        private static string CapitalizeFirstLetter(string input)
        {
            if (input == null) throw new ArgumentNullException("amount", "Amount in words must not be null");
            if (string.IsNullOrEmpty(input)) throw new ArgumentException("Amount in words cannot be empty");
            return input.First().ToString(CultureInfo.InvariantCulture).ToUpper(CultureInfo.InvariantCulture) + input.Substring(1);
        }
        private static void ValidateInputDecimal(decimal amt, Culture culture)
        {
            string[] amountStr = amt.ToString(CultureInfo.InvariantCulture).Split('.');
            //if (culture == Culture.Nepali)
            //{
            //    if (amountStr[0].Length > 19)
            //        throw new Exception("Input digits exceeds maximum supported length(max:19)");
            //}
        }


    }

}
