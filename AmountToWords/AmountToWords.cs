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

        //SCALES make readonly
        private string[] scale = { "", "hundred", "thousand", "lakh", "crore", "arba", "kharba", "neel", "padma", "shankha" };
        private readonly string[] scaleNep = { "", "सय", "हजार", "लाख", "करोड", "अरब", "खरब", "नील", "पद्म", "शंख" };

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

        private string PaisaToWords(string paisa, OutputFormat outputFormat)
        {
            if (Convert.ToInt16(paisa, CultureInfo.InvariantCulture) > 0)
            {
                int dec = Convert.ToInt16(paisa, CultureInfo.InvariantCulture);
                string paisaInWords;

                if (dec < 1)
                {
                    paisaInWords = string.Empty;
                }
                else
                {
                    if (dec < GetResourceLimitIndex(outputFormat))
                    {
                        paisaInWords = ones[dec];
                    }
                    else
                    {
                        if (dec % 10 == 0)
                        {
                            paisaInWords = tens[dec / 10];
                        }
                        else
                        {
                            var index = dec.ToString(CultureInfo.InvariantCulture).ToCharArray();
                            paisaInWords = string.Concat(tens[Convert.ToInt16(index[0].ToString(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture)], " ", ones[Convert.ToInt16(index[1].ToString(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture)]);
                        }
                    }
                }
                return string.Concat(paisaInWords, " ", _subAmtUnit);
            }
            return string.Empty;
        }

        private int GetResourceLimitIndex(OutputFormat outputFormat) => outputFormat == OutputFormat.English ? 20 : 100;

        public AmountToWords(Culture culture)
        {
            _culture = culture;
            switch (culture)
            {
                case Culture.Nepali:
                    ones = onesNep;
                    tens = tensNep;
                    scale = scaleNep;

                    _amtUnit = "रूपैयाँ";
                    _subAmtUnit = "पैसा";
                    _postfix = "मात्र";
                    break;
                case Culture.English:
                    _amtUnit = "rupees";
                    _subAmtUnit = "paisa";
                    _postfix = "only";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(culture), culture, "Invalid Culture in AmountToWords");
            }
        }
        public AmountToWords() : this(Culture.English) { }
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
            ValidateInputDecimal(amt);

            OutputFormat outputFormat = _culture == Culture.English ? OutputFormat.English : OutputFormat.Unicode;

            StringBuilder wordBuilder = new StringBuilder();
            string inWords;

            string amtstring = $"{amt:F2}";
            string[] amount = amtstring.Split('.');
            string number = amount[0];
            //11,22,33,123
            int upperScale = number.Length / 2;
            if (amt < 1)
            {
                inWords = PaisaToWords(amount[1], outputFormat);
            }
            else
            {
                string wholeNumber = number;

                for (int i = upperScale; i >= 0; i--)
                {
                    switch (i)
                    {
                        case 0: //For the Decimals
                            string paisa = amount[1];
                            if (Convert.ToInt16(paisa, CultureInfo.InvariantCulture) > 0)
                                wordBuilder.Append(" " + PaisaToWords(paisa, outputFormat));
                            break;
                        case 1: //For the Hundreds, tens and ones
                                //check 0
                            inWords = ToHundredthWords(wholeNumber, outputFormat);
                            wordBuilder.Append(string.Concat(inWords, " ", _amtUnit));
                            break;
                        default: //For Everything Greater than hundreds
                            string digits;
                            digits = wholeNumber.Substring(0, 2);
                            wholeNumber = wholeNumber.Remove(0, 2);
                            int value = Convert.ToInt16(digits, CultureInfo.InvariantCulture);

                            if (value == 0)
                            {
                                continue;
                            }
                            else if (value < GetResourceLimitIndex(outputFormat))
                            {
                                inWords = ones[value];
                            }
                            else if (value % 10 == 0)
                            {
                                inWords = tens[value / 10];
                            }
                            else
                            {
                                int first = Convert.ToInt16(digits.Substring(0, 1), CultureInfo.InvariantCulture);
                                int second = Convert.ToInt16(digits.Substring(1, 1), CultureInfo.InvariantCulture);
                                inWords = string.Concat(tens[first], " ", ones[second]);
                            }

                            wordBuilder.Append(string.Concat(inWords, " ", scale[i], " "));
                            break;
                    }
                }
                inWords = wordBuilder.ToString();
            }
            inWords = string.Concat(inWords, " ", _postfix);

            return CapitalizeFirstLetter(inWords);
        }

        public string ConvertToEnglishWords(decimal amt)
        {
            ValidateInputDecimal(amt);
            scale = new []{ "", "hundred", "thousand", "million", "billion", "trillion", "quadrillion", "quintillion", "sextillion", "septillion" };

            OutputFormat outputFormat = _culture == Culture.English ? OutputFormat.English : OutputFormat.Unicode;

            StringBuilder wordBuilder = new StringBuilder();
            string inWords;

            string amtstring = $"{amt:F2}";
            string[] amount = amtstring.Split('.');
            string number = amount[0];
            //111,222,333,123
            int upperScale = number.Length / 3;
            if (amt < 1)
            {
                inWords = PaisaToWords(amount[1], outputFormat);
            }
            else
            {
                string wholeNumber = number;

                for (int i = upperScale; i >= 0; i--)
                {
                    switch (i)
                    {
                        case 0: //For the Decimals
                            string paisa = amount[1];
                            if (Convert.ToInt16(paisa, CultureInfo.InvariantCulture) > 0)
                                wordBuilder.Append(" " + PaisaToWords(paisa, outputFormat));
                            break;
                        case 1: //For the Hundreds, tens and ones
                                //check 0
                            inWords = ToHundredthWords(wholeNumber, outputFormat);
                            wordBuilder.Append(string.Concat(inWords, " ", _amtUnit));
                            break;
                        default: //For Everything Greater than hundreds
                            string hundreds = wholeNumber.Substring(0, 3);
                            wholeNumber = wholeNumber.Remove(0, 3);
                            inWords = ToHundredthWords(hundreds, outputFormat);
                            //wordBuilder.Append(string.Concat(inWords, " ", _amtUnit));

                            wordBuilder.Append(string.Concat(inWords, " ", scale[i], " "));
                            break;
                    }
                }
                inWords = wordBuilder.ToString();
            }
            inWords = string.Concat(inWords, " ", _postfix);

            return CapitalizeFirstLetter(inWords);
        }

        private string ToHundredthWords(string hundred, OutputFormat outputFormat)
        {
            string inWords = string.Empty;
            if (hundred.Length == 3)
            {
                int hundredth = Convert.ToInt16(hundred.Substring(0, 1), CultureInfo.InvariantCulture);
                int tensAmount = Convert.ToInt16(hundred.Substring(1, 2), CultureInfo.InvariantCulture);
                int tenth = Convert.ToInt16(hundred.Substring(1, 1), CultureInfo.InvariantCulture);
                int oneth = Convert.ToInt16(hundred.Substring(2, 1), CultureInfo.InvariantCulture);

                inWords = hundredth > 0 ? string.Concat(ones[hundredth], " ", scale[1], " ") : string.Empty;
                inWords += tensAmount > 0 ? (tensAmount < GetResourceLimitIndex(outputFormat) ? ones[tensAmount] : string.Concat(tens[tenth], " ", ones[oneth])) : string.Empty;
            }

            return inWords;
        }

        private static string CapitalizeFirstLetter(string input)
        {
            if (input == null) throw new ArgumentNullException("amount", "Amount in words must not be null");
            if (string.IsNullOrEmpty(input)) throw new ArgumentException("Amount in words cannot be empty");
            return input.First().ToString(CultureInfo.InvariantCulture).ToUpper(CultureInfo.InvariantCulture) + input.Substring(1);
        }
        private static void ValidateInputDecimal(decimal amt)
        {
            string[] amountStr = amt.ToString(CultureInfo.InvariantCulture).Split('.');
            if (amountStr[0].Length > 19)
                throw new Exception("Input digits exceeds maximum supported length(max:19)");
        }


    }

}
