namespace NumericWordsConversion {

    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using JetBrains.Annotations;

    internal static class Utilities {

        /// <summary>Capitalizes the first letter of input string</summary>
        /// <param name="words"></param>
        /// <returns></returns>
        [NotNull]
        internal static string CapitalizeFirstLetterOld( this string words ) {
            if ( String.IsNullOrEmpty( words ) ) {
                throw new ArgumentException( "Input string must not be null or empty" );
            }

            return words.First().ToString( CultureInfo.InvariantCulture ).ToUpper( CultureInfo.InvariantCulture ) + words.Substring( 1 );
        }

        /// <summary>Used to initialize the conversion factory as per the specified options</summary>
        /// <param name="options"></param>
        [NotNull]
        internal static ConversionFactory InitializeConversionFactory( NumericWordsConversionOptions options ) {
            ManageSuitableResources( out var ones, out var tens, out var scale, options );

            return new ConversionFactory( options, ones, tens, scale );
        }

        /// <summary>Output resources for words conversion as per the specified options</summary>
        /// <param name="ones">Output parameter for ones digit</param>
        /// <param name="tens">Output parameter for tens digit</param>
        /// <param name="scale">Output parameter for scale of specified culture</param>
        /// <param name="options">Options used for resources output</param>
        internal static void ManageSuitableResources( [NotNull] [ItemNotNull] out string[] ones, [NotNull] [ItemNotNull] out string[] tens,
            [NotNull] [ItemNotNull] out string[] scale, NumericWordsConversionOptions options ) {
            switch ( options.Culture ) {
                case Culture.Nepali:
                    switch ( options.OutputFormat ) {
                        case OutputFormat.English:
                            ones = WordResources.OnesEnglish;
                            tens = WordResources.TensEnglish;
                            scale = WordResources.ScaleNepEnglish;

                            break;

                        case OutputFormat.Unicode:
                            ones = WordResources.OnesNep;
                            tens = WordResources.TensNep;
                            scale = WordResources.ScaleNep;

                            break;

                        case OutputFormat.Devnagari:
                            ones = WordResources.OnesDevnagari;
                            tens = WordResources.TensDevnagari;
                            scale = WordResources.ScaleDevnagari;

                            break;

                        default: throw new InvalidOperationException( "Invalid value." );
                    }

                    break;

                case Culture.International:
                    scale = WordResources.ScaleEng;
                    ones = WordResources.OnesEnglish;
                    tens = WordResources.TensEnglish;

                    break;

                case Culture.Hindi:
                    switch ( options.OutputFormat ) {
                        case OutputFormat.English:
                            ones = WordResources.OnesEnglish;
                            tens = WordResources.TensEnglish;
                            scale = WordResources.ScaleNepEnglish;

                            break;

                        case OutputFormat.Unicode:
                            ones = WordResources.OnesHindi;
                            tens = WordResources.TensHindi;
                            scale = WordResources.ScaleHindi;

                            break;

                        case OutputFormat.Devnagari: throw new NotSupportedException( "Devnagari Output is not currently supported for Hindi Numeric System" );
                        default: throw new InvalidOperationException( "Invalid value.");
                    }

                    break;

                default: throw new InvalidOperationException( "Invalid Culture in Conversion Options" );
            }
        }

        /// <summary>Modifies the <paramref name="memory" /> and makes the first letter capitalized.</summary>
        /// <param name="memory"></param>
        [DebuggerStepThrough]
        public static void CapitalizeFirstLetter( this Memory<Char> memory ) {
            if ( memory.IsEmpty ) {
                return;
            }

            ref var first = ref memory.Span[ 0 ];
            first = Char.ToUpper( first, CultureInfo.InvariantCulture );
        }

        /// <summary>Returns the <paramref name="text" /> with the first letter capitalized.</summary>
        /// <param name="text"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [NotNull]
        [Pure]
        public static String CapitalizeFirstLetter( [CanBeNull] this String text ) {
            if ( String.IsNullOrEmpty( text ) ) {
                return String.Empty;
            }

            if ( text.Length == 1 ) {
                return Char.ToUpper( text[ 0 ], CultureInfo.InvariantCulture ).ToString( CultureInfo.InvariantCulture );
            }

            return Char.ToUpper( text[ 0 ], CultureInfo.InvariantCulture ) + text.Substring( 1 );
        }

        /// <summary>Returns null if <paramref name="self" /> is <see cref="String.IsNullOrEmpty" />.</summary>
        /// <param name="self"></param>
        /// <returns></returns>
        [CanBeNull]
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        [DebuggerStepThrough]
        [Pure]
        public static String? NullIfEmpty( [CanBeNull] this String self ) => String.IsNullOrEmpty( self ) ? null : self;

        /// <summary>Trim the ToString() of the object; returning null if null, empty, or whitespace.</summary>
        /// <param name="self"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [CanBeNull]
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        [Pure]
        public static String? Trimmed<T>( [CanBeNull] this T self ) {
            switch ( self ) {
                case null: return null;
                case String s: return s.Trim().NullIfEmpty();
                default: return self.ToString()?.Trim().NullIfEmpty();
            }
        }

    }

}