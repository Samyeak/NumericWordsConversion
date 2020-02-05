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
            this._options = options;
            Utilities.ManageSuitableResources(out this._ones, out this._tens, out this._scale, options);
        }
        public ConversionFactory(NumericWordsConversionOptions options, string[] ones, string[] tens, string[] scale)
        {
            this._options = options;
            this._ones = ones;
            this._tens = tens;
            this._scale = scale;
        }

        internal string ToOnesWords(ushort digit) => this._ones[digit];

        internal string ToTensWord(string tenth)
        {
            int dec = Convert.ToUInt16(tenth, CultureInfo.InvariantCulture);
            if (dec <= 0) {
                return Empty;
            }

            string words;

            if (dec < this._options.ResourceLimitIndex)
            {
                words = this._ones[dec];
            }
            else
            {
                if (dec % 10 == 0)
                {
                    words = this._tens[dec / 10];
                }
                else
                {
                    int first = Convert.ToUInt16(tenth.Substring(0, 1), CultureInfo.InvariantCulture);
                    int second = Convert.ToUInt16(tenth.Substring(1, 1), CultureInfo.InvariantCulture);
                    words = Concat( this._tens[first], " ", this._ones[second]);
                }
            }

            return words;
        }

        internal string ToHundredthWords(string hundred)
        {
            var inWords = Empty;
            if (hundred.Length == 3)
            {
                int hundredth = Convert.ToInt16(hundred.Substring(0, 1), CultureInfo.InvariantCulture);
                inWords = hundredth > 0 ? Concat( this._ones[hundredth], " ", this._scale[1], " ") : Empty;
                hundred = hundred.Substring(1, 2);
            }
            inWords += this.ToTensWord(hundred);
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

            var builder = new StringBuilder();
            int scaleMapIndex;
            if ( this._options.Culture == Culture.International) {
                scaleMapIndex = (int)Math.Ceiling((decimal)digits.Length / 3);
            }
            else {
                scaleMapIndex = (digits.Length - 3) < 1 ? 1 : digits.Length / 2;
            }

            for (var i = scaleMapIndex; i > 0; i--)
            {
                string inWords;
                switch (i)
                {
                    case 1: //For the Hundreds, tens and ones
                        inWords = this.ToHundredthWords(digits);
                        if (!IsNullOrEmpty(inWords)) {
                            builder.Append(Concat(inWords.Trim(), " "));
                        }

                        break;
                    default: //For Everything Greater than hundreds
                        if ( this._options.Culture == Culture.International)
                        {
                            var length = (digits.Length % ((i - 1) * 3 + 1)) + 1;
                            var hundreds = digits.Substring(0, length);
                            digits = digits.Remove(0, length);
                            inWords = this.ToHundredthWords(hundreds);
                        }
                        else
                        {
                            var length = (digits.Length % 2 == 0) ? 1 : 2;
                            var hundreds = digits.Substring(0, length);
                            digits = digits.Remove(0, length);
                            inWords = this.ToTensWord(hundreds);
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
