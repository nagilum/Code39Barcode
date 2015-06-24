# Code 39 Barcode

C# class to easily generate code-39 barcodes without any dependecies or use of fonts.

This is an example of a barcode generated with the class. The code behind this barcode is 28052.

![Example of barcode from code 28052](https://raw.githubusercontent.com/nagilum/Code39Barcode/master/example-28052.png)

## Example:

```csharp
// Initiate a new instance of the class.
var generator = new Code39Barcode();

// Generate a barcode from the code "28052", with a image that is 40 px height,
// has 20 px padding on each side, does not include the code as text below the
// image and uses a barcode width of 2 px.
var bitmap = generator.Create(40, "28052", 20, false, 2);
```
