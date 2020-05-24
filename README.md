# Numeric Words Conversion
![Build](https://github.com/Samyeak/NumericWordsConversion/workflows/.NET%20Core/badge.svg)
![Unit Tests](https://github.com/Samyeak/NumericWordsConversion/workflows/Unit%20Tests/badge.svg?event=push)

Numeric Words Conversion is a C# library for converting numeric into words. The goal is to create a simple customizable library which can easily be used to convert any numbers or currencies to words.

## Usage
### For Numeric
```csharp
decimal amount = 100000.12;
NumericWordsConverter converter = new NumericWordsConverter();
string words = converter.ToWords(amount)
//OR SIMPLY
string words = amount.ToNumericWords();
//Outputs: One hundred thousand point one two
```
* Supports Multiple Numeral Systems with multiple output formats
	* **International English** `//Eg: One hundred thousand `
	* **Nepali and Hindi**
		Supports multiple output formats
		* Output Format : **English** 
			 `Eg: 100000.1 outputs One lakh point one`
		* Output Format : **Unicode** 
			 `Eg: 100000.1 outputs एक लाख दशमलव एक`
		* Output Format : **Devnagari** 
			 `Eg: 100000.1 outputs Ps nfv bzdnj Ps //Makes sense in Devnagari fonts`


### For Currency
Supports Nepali, Hindi and International currency system out of the box.
By default, uses International currency system.
```csharp
decimal amount = 100000.12;
CurrencyWordsConverter converter = new CurrencyWordsConverter();
string words = converter.ToWords(amount)
//OR SIMPLY
string words = amount.ToCurrencyWords();
//Outputs: One hundred thousand dollar and twelve cents only
```
And you can customize it as you like by passing the options parameter
```csharp
CurrencyWordsConverter converter = new CurrencyWordsConverter(new CurrencyWordsConversionOptions()
            {
                Culture = Culture.Nepali,
                OutputFormat = OutputFormat.English
            });
string words = converter.ToWords(100000.12M);
//Outputs: One lakh rupees and twelve paisa only
```
**You can even specify your own currency units and fully customize it your own way.**
```csharp
CurrencyWordsConverter converter = new CurrencyWordsConverter(new CurrencyWordsConversionOptions()
            {
                Culture = Culture.International,
                OutputFormat = OutputFormat.English,
                CurrencyUnitSeparator = string.Empty,
                CurrencyUnit = "pound",
                SubCurrencyUnit = "pence",
                EndOfWordsMarker = ""
            });
string words = converter.ToWords(00000.12M);
//Outputs: One hundred thousand pound twelve pence
```

**Need single conversion options through out the application? You can override the default options as desired.  Just use the following code in startup file of the project**
```csharp
NumericWordsConfiguration.ConfigureConversionDefaults(options =>
            {
            //For Numeric Words Conversion
                options.SetDefaultNumericWordsOptions(new NumericWordsConversionOptions
                {
                    Culture = Culture.International,
                    DecimalSeparator = "dot",
                    DecimalPlaces = 2
                });
            });
            //For Currency Words Conversion
                options.SetDefaultCurrencyWordsOptions(new CurrencyWordsConversionOptions
                {
                    Culture = Culture.Nepali,
                    OutputFormat = OutputFormat.English,
                    CurrencyUnitSeparator = "and",
                    CurrencyUnit = "rupee",
                    CurrencyNotationType = NotationType.Prefix,
                    SubCurrencyUnit = "paisa",
                    EndOfWordsMarker = "and"
                });
```
And simply use the extension method anywhere in the application, as
```csharp
decimal amount = 100000.01M;
amount.ToNumericWords(); //Outputs: One hundred thousand dot zero one.
amount.ToCurrencyWords(); //Outputs: Rupees one lakh and one paisa.
```

## License
[Under GNU GENERAL PUBLIC LICENSE V3](https://www.gnu.org/licenses/gpl-3.0.en.html)

<!--
## Install

#### [NuGet Gallery](http://nuget.org/packages/todo)
```
Install-Package todo
```
-->
