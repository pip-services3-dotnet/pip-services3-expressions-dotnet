# <img src="https://github.com/pip-services/pip-services/raw/master/design/Logo.png" alt="Pip.Services Logo" style="max-width:30%"> <br/> Data processing and persistence components for .NET Changelog

## <a name="3.6.0"></a> 3.6.0 (2025-12-24)

### Breaking Changes
* Migrate to .NET 10.0

## <a name="3.5.1"></a> 3.5.1 (2023-12-15)

### Breaking Changes
* Migrate to .NET 8.0

## <a name="3.4.1-3.4.4"></a> 3.4.1-3.4.4 (2023-07-07)

### Features
* Added Decimal Token Type 
* Updated parser to extract decimal constants
* **Variants**: Fixed issue in decimal converter
* Added Average function to DefaultFunctionCollection

## <a name="3.4.0"></a> 3.4.0 (2022-08-04)

### Breaking Changes
* Migrate to .NET 6.0

## <a name="3.3.2"></a> 3.3.2 (2022-06-17)
### Features
* **Variants**: Extended with decimal type and operations

## <a name="3.3.0"></a> 3.3.0 (2021-09-01)

### Breaking Changes
* Migrate to .NET Core 5.0

## <a name="3.2.0"></a> 3.2.0 (2021-06-11) 

### Features
* Updated references as PipServices3.Components have got minor changes

## <a name="3.1.0"></a> 3.1.0 (2021-03-19)

### Features
* **IO Streaming**: Replaced pushback reader with scanner
* Added **Mustache** template processing with tokenizers and parsers
* Added line and column numbers to errors
* Added escaping strings using tripple brackets '{{{ }}}' according to JSON rules

## <a name="3.0.6"></a> 3.0.6 (2020-11-09)

### Bug Fixes
* Extended expression words with dot symbol (required for complex variables, e.g. coordinate.x > 10)

## <a name="3.0.5"></a> 3.0.5 (2020-10-05)

### Features
* Implemented LIKE operation

## <a name="3.0.4"></a> 3.0.4 (2020-09-30)

### Bug Fixes
* Fixed logical variant operations to get boolean instead null

## <a name="3.0.3"></a> 3.0.3 (2020-09-25)

### Features
* Added new funtion TIMEOFDAY

## <a name="3.0.2"></a> 3.0.2 (2020-09-18)

### Features
* Added new funtions: TIMESPAN, DAYOFWEEK
* Rename TIME function to TICKS

## <a name="3.0.1"></a> 3.0.1 (2020-09-18)

### Features
* Added new funtions: ARRAY, DATE
* Added operation NOTIN
* New tests

### Bug Fixes
* Fixed EOF processing in comment states
* Fixed some exceptions

## <a name="3.0.0"></a> 3.0.0 (2020-09-05)

### Features
* IO Streaming: IPushbackReader and StringPushbackReader
* Variant (dynamic) values
* Tokenizers (Lexical analyzers)
* Expression calculator
* CSV content processor