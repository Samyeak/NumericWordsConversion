using System;
using System.Globalization;
using System.Text;
using static System.String;

namespace NumericWordsConversion
{
    /// <summary>
    /// Used to simply map numbers with their respective words output as per the specified options
    /// </summary>
    internal class ConversionFactory
    {
        private readonly NumericWordsConversionOptions _options;
        private readonly string[] _ones;
        private readonly string[] _tens;
        private readonly string[] _scale;

        public ConversionFactory(NumericWordsConversionOptions options)
        {
            _options = options;
            Utilities.ManageSuitableResources(out _ones, out _tens, out _scale, options);
        }
        public ConversionFactory(NumericWordsConversionOptions options, string[] ones, string[] tens, string[] scale)
        {
            _options = options;
            _ones = ones;
            _tens = tens;
            _scale = scale;
        }

        internal string ToOnesWords(ushort digit) => _ones[digit];

        internal string ToTensWord(string tenth)
        {
            int dec = Convert.ToUInt16(tenth, CultureInfo.InvariantCulture);
            if (dec <= 0) {
                return Empty;
            }

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
                    int first = Convert.ToUInt16(tenth.Substring(0, 1), CultureInfo.InvariantCulture);
                    int second = Convert.ToUInt16(tenth.Substring(1, 1), CultureInfo.InvariantCulture);
                    words = Concat(_tens[first], " ", _ones[second]);
                }
            }

            return words;
        }

        internal string ToHundredthWords(string hundred)
        {
            string inWords = Empty;
            if (hundred.Length == 3)
            {
                int hundredth = Convert.ToInt16(hundred.Substring(0, 1), CultureInfo.InvariantCulture);
                inWords = hundredth > 0 ? Concat(_ones[hundredth], " ", _scale[1], " ") : Empty;
                hundred = hundred.Substring(1, 2);
            }
            inWords += ToTensWord(hundred);
            return inWords.Trim();
        }

        /// <summary>
        /// Responsible for converting any input digits to words
        /// </summary>
        /// <param name="digits"></param>
        /// <returns></returns>
        internal string ConvertDigits(string digits)
        {
            if (digits == "0") {
                return this._ones[0];
            }

            StringBuilder builder = new StringBuilder();
            int scaleMapIndex;
            if (_options.Culture == Culture.International) {
                scaleMapIndex = (int)Math.Ceiling((decimal)digits.Length / 3);
            }
            else {
                scaleMapIndex = (digits.Length - 3) < 1 ? 1 : digits.Length / 2;
            }

            for (int i = scaleMapIndex; i > 0; i--)
            {
                string inWords;
                switch (i)
                {
                    case 1: //For the Hundreds, tens and ones
                        inWords = ToHundredthWords(digits);
                        if (!IsNullOrEmpty(inWords)) {
                            builder.Append(Concat(inWords.Trim(), " "));
                        }

                        break;
                    default: //For Everything Greater than hundreds
                        if (_options.Culture == Culture.International)
                        {
                            int length = (digits.Length % ((i - 1) * 3 + 1)) + 1;
                            string hundreds = digits.Substring(0, length);
                            digits = digits.Remove(0, length);
                            inWords = ToHundredthWords(hundreds);
                        }
                        else
                        {
                            int length = (digits.Length % 2 == 0) ? 1 : 2;
                            string hundreds = digits.Substring(0, length);
                            digits = digits.Remove(0, length);
                            inWords = ToTensWord(hundreds);
                        }

                        if (!IsNullOrEmpty(inWords.Trim())) {
                            builder.Append(Concat(inWords.Trim(), " ", this._scale[i], " "));
                        }

                        break;
                }
            }
            return builder.ToString().Trim();
        }
    }
}
