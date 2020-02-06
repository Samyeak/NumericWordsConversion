namespace ConsoleTest {

    using System;
    using NumericWordsConversion;

    public static class Program {

        public static void Main() {

            var converter = new CurrencyWordsConverter();
            const Decimal number = 123_000M;
            var words = converter.ToWords( number );

            //words = number.To
            Console.WriteLine( words );
            Console.ReadKey();
        }

    }

}