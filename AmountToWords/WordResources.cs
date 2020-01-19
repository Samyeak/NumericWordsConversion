namespace NumericWordsConversion
{
    internal static class WordResources
    {
        /**
         * Number Notation has been added up to 10^39
         * Decimal type supports up to 10^27
         */
        public static readonly string[] ScaleEng = { "", "hundred", "thousand", "million", "billion", "trillion", "quadrillion", "quintillion", "sextillion", "septillion", "octillion", "nonillion", "decillion", "undecillion", "duodecillion " };
        public static readonly string[] ScaleNep = { "", "सय", "हजार", "लाख", "करोड", "अरब", "खरब", "नील", "पद्म", "शंख", "उपाध", "अंक", "जल्द", "मध", "परर्ध", "अन्त", "महाअन्त", "शिशन्त", "सिंघर", "महासिंहर", "अदन्त सिंहर" };
        public static readonly string[] ScaleNepEnglish = { "", "hundred", "thousand", "lakh", "crore", "arba", "kharba", "neel", "padma", "shankha", "Upadh", "Anka", "Jald", "Madh", "Parardha", "Anta", "Mahaanta", "Shishanta", "Singhar", "Maha Singhar", "Adanta Singhar" }; //Pow(10,39)
        public static readonly string[] ScaleDevnagari = { "", ";o", "xhf/", "nfv", "s/f]8", "c/a", "v/a", "gLn", "kß", "z+v", "pkfw", "c+s", "hNb", "dw", "k/w{", "cGt", "dxfcGt", "lzzGt", "l;+3/", "dxfl;+x/", "cbGt l;+x/" };
        public static readonly string[] ScaleHindi = { "", "सौ", "हजार", "लाख", "करोड़", "अरब", "खरब", "नील", "पद्म", "शंख", "उपाध", "अंक", "जल्द", "मध", "परर्ध", "अन्त", "महाअन्त", "शिशन्त", "सिंघर", "महासिंहर", "अदन्त सिंहर" };

        //ENGLISH WORDS RESOURCE
        public static readonly string[] OnesEnglish = { "", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        public static readonly string[] TensEnglish = { "", "", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

        //NEPALI WORDS RESOURCE
        public static readonly string[] TensNep = { "", "दस", "बीस", "तीस", "चालीस", "पचास", "साठी", "सतरी", "अस्सी", "नब्बे" };
        public static readonly string[] TensDevnagari = { "", "b;", "aL;", "tL;", "rfnL;", "krf;", ";f7L", ";t/L", "c:;L", "gAa]" };

        public static readonly string[] OnesNep = new[]{ "सुन्य", "एक", "दुई", "तीन", "चार", "पाँच", "छ", "सात", "आठ", "नौ", "दस",
            "एघार", "बाह्र", "तेह्र", "चौध", "पन्ध्र", "सोह्र", "सत्र", "अठाह्र", "उन्नाइस", "बीस", "एकाइस",
            "बाइस", "तेइस", "चौबीस", "पचीस", "छब्बीस", "सत्ताइस", "अठ्ठाइस", "उनन्तीस", "तीस",
            "एकतीस", "बतीस", "तेतीस", "चौतीस", "पैतीस", "छतीस", "सरतीस", "अरतीस", "उननचालीस", "चालीस",
            "एकचालीस", "बयालिस", "तीरचालीस", "चौवालिस", "पैंतालिस", "छयालिस", "सरचालीस", "अरचालीस", "उननचास", "पचास",
            "एकाउन्न", "बाउन्न", "त्रिपन्न", "चौवन्न", "पच्पन्न", "छपन्न", "सन्ताउन्न", "अन्ठाउँन्न", "उनान्न्साठी", "साठी",
            "एकसाठी", "बासाठी", "तीरसाठी", "चौंसाठी", "पैसाठी", "छैसठी", "सत्सठ्ठी", "अर्सठ्ठी", "उनन्सत्तरी", "सतरी",
            "एकहत्तर", "बहत्तर", "त्रिहत्तर", "चौहत्तर", "पचहत्तर", "छहत्तर", "सत्हत्तर", "अठ्हत्तर", "उनास्सी", "अस्सी",
            "एकासी", "बयासी", "त्रीयासी", "चौरासी", "पचासी", "छयासी", "सतासी", "अठासी", "उनान्नब्बे", "नब्बे",
            "एकान्नब्बे", "बयान्नब्बे", "त्रियान्नब्बे", "चौरान्नब्बे", "पंचान्नब्बे", "छयान्नब्बे", "सन्तान्‍नब्बे", "अन्ठान्नब्बे", "उनान्सय"
        };

        public static readonly string[] OnesDevnagari = new[]{ ";'Go", "Ps", "b'O{", "tLg", "rf/", "kfFr", "5", ";ft", "cf7", "gf}", "b;",
            "P3f/", "afx|", "t]x|", "rf}w", "kG„", ";f]x|", ";q", "c7fx|", "pGgfO;", "aL;", "PsfO;",
            "afO;", "t]O;", "rf}aL;", "krL;", "5AaL;", ";QfO;", "c¶fO;", "pgGtL;", "tL;",
            "PstL;", "atL;", "t]tL;", "rf}tL;", "k}tL;", "5tL;", ";/tL;", "c/tL;", "pggrfnL;", "rfnL;",
            "PsrfnL;", "aofln;", "tL/rfnL;", "rf}jfln;", "k}+tfln;", "5ofln;", ";/rfnL;", "c/rfnL;", "pggrf;", "krf;",
            "PsfpGg", "afpGg", "lqkGg", "rf}jGg", "kRkGg", "5kGg", ";GtfpGg", "cG7fpFGg", @"pgfGg\;f7L", ";f7L",
            "Ps;f7L", "af;f7L", "tL/;f7L", "rf}+;f7L", "k};f7L", "5};7L", ";T;¶L", "c;{¶L", "pgG;Q/L", ";t/L",
            "PsxQ/", "axQ/", "lqxQ/", "rf}xQ/", "krxQ/", "5xQ/", ";TxQ/", @"c7\xQ/", "pgf:;L", "c:;L",
            "Psf;L", "aof;L", "qLof;L", "rf}/f;L", "krf;L", "5of;L", ";tf;L", "c7f;L", "pgfGgAa]", "gAa]",
            "PsfGgAa]", "aofGgAa]", "lqofGgAa]", "rf}/fGgAa]", "k+rfGgAa]", "5ofGgAa]", ";GtfG‍gAa]", "cG7fGgAa]", "pgfG;o"
        };

        public static readonly string[] TensHindi = { "", "दस", "बीस", "तीस", "चालीस", "पचास", "साठ", "सत्तर", "अस्सी", "नब्बे" };

        public static readonly string[] OnesHindi = new[]{ "सुन्य", "एक", "दो", "तीन", "चार", "पाँच", "छह", "सात", "आठ", "नौ", "दस",
            "ग्यारह", "बारह", "तेरह", "चौदह", "पन्द्रह", "सोलह", "सत्रह", "अठारह", "उन्नीस", "बीस",
            "इक्कीस", "बाईस", "तेईस", "चौबीस", "पच्चीस", "छब्बीस", "सत्ताईस", "अट्ठाईस", "उनतीस", "तीस",
            "इकतीस", "बत्तीस", "तैंतीस", "चौंतीस", "पैंतीस", "छत्तीस", "सैंतीस", "अड़तीस", "उनतालीस", "चालीस",
            "इकतालीस", "बयालीस", "तैंतालीस", "चौवालीस", "पैंतालीस", "छियालीस", "सैंतालीस", "अड़तालीस", "उनचास", "पचास",
            "इक्यावन", "बावन", "तिरेपन", "चौवन", "पचपन", "छप्पन", "सत्तावन", "अट्ठावन", "उनसठ", "साठ",
            "इकसठ", "बासठ", "तिरेसठ", "चौंसठ", "पैंसठ", "छियासठ", "सड़सठ", "अड़सठ", "उनहत्तर", "सत्तर",
            "इकहत्तर", "बहत्तर", "तिहत्तर", "चौहत्तर", "पचहत्तर", "छिहत्तर", "सतहत्तर", "अठहत्तर", "उनासी", "अस्सी",
            "इक्यासी", "बयासी", "तिरासी", "चौरासी", "पचासी", "छियासी", "सत्तासी", "अट्ठासी", "नवासी", "नब्बे",
            "इक्यानबे", "बानबे", "तिरानबे", "चौरानबे", "पंचानबे", "छियानबे", "सत्तानबे", "अट्ठानबे", "निन्यानबे"
        };
    }
}
