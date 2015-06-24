using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

/// <summary>
/// Generate code-39 barcodes.
/// </summary>
public class Code39Barcode {
	/// <summary>
	/// The encoding library for all allowed characters.
	/// </summary>
	private readonly Dictionary<char, string> _code39Dictionary;

	/// <summary>
	/// Init a new instance of the code-39 barcode generator.
	/// </summary>
	public Code39Barcode() {
		_code39Dictionary = new Dictionary<char, string>();
		_code39Dictionary['0'] = "101001101101";
		_code39Dictionary['1'] = "110100101011";
		_code39Dictionary['2'] = "101100101011";
		_code39Dictionary['3'] = "110110010101";
		_code39Dictionary['4'] = "101001101011";
		_code39Dictionary['5'] = "110100110101";
		_code39Dictionary['6'] = "101100110101";
		_code39Dictionary['7'] = "101001011011";
		_code39Dictionary['8'] = "110100101101";
		_code39Dictionary['9'] = "101100101101";
		_code39Dictionary['A'] = "110101001011";
		_code39Dictionary['B'] = "101101001011";
		_code39Dictionary['C'] = "110110100101";
		_code39Dictionary['D'] = "101011001011";
		_code39Dictionary['E'] = "110101100101";
		_code39Dictionary['F'] = "101101100101";
		_code39Dictionary['G'] = "101010011011";
		_code39Dictionary['H'] = "110101001101";
		_code39Dictionary['I'] = "101101001101";
		_code39Dictionary['J'] = "101011001101";
		_code39Dictionary['K'] = "110101010011";
		_code39Dictionary['L'] = "101101010011";
		_code39Dictionary['M'] = "110110101001";
		_code39Dictionary['N'] = "101011010011";
		_code39Dictionary['O'] = "110101101001";
		_code39Dictionary['P'] = "101101101001";
		_code39Dictionary['Q'] = "101010110011";
		_code39Dictionary['R'] = "110101011001";
		_code39Dictionary['S'] = "101101011001";
		_code39Dictionary['T'] = "101011011001";
		_code39Dictionary['U'] = "110010101011";
		_code39Dictionary['V'] = "100110101011";
		_code39Dictionary['W'] = "110011010101";
		_code39Dictionary['X'] = "100101101011";
		_code39Dictionary['Y'] = "110010110101";
		_code39Dictionary['Z'] = "100110110101";
		_code39Dictionary['-'] = "100101011011";
		_code39Dictionary['.'] = "110010101101";
		_code39Dictionary[' '] = "100110101101";
		_code39Dictionary['$'] = "100100100101";
		_code39Dictionary['/'] = "100100101001";
		_code39Dictionary['+'] = "100101001001";
		_code39Dictionary['%'] = "101001001001";
		_code39Dictionary['*'] = "100101101101";
	}

	/// <summary>
	/// Generate a code-39 barcode.
	/// </summary>
	/// <param name="height">Height (in px) of the finished generated image.</param>
	/// <param name="content">The code to create barcode from.</param>
	/// <param name="padding">Padding (in px) to apply to each side of the image.</param>
	/// <param name="includeCode">Whether or not to write out the code on the generated image (below the barcode).</param>
	/// <param name="barWidth">Width of the bars to draw.</param>
	/// <returns></returns>
	public Bitmap Create(int height, string content, int padding = 0, bool includeCode = false, int barWidth = 1) {
		var encodedBits = new StringBuilder();

		encodedBits.Append("1001011011010");

		foreach (var c in content.ToUpper()) {
			string encoded;

			if (!_code39Dictionary.TryGetValue(c, out encoded))
				throw new ArgumentOutOfRangeException("content", "Characher '" + c + "' is not compatible with this Code39 implementation!");

			encodedBits.Append(encoded + '0');
		}

		encodedBits.Append("100101101101");

		var offsetLeft = padding;
		var width = (encodedBits.Length*barWidth) + (offsetLeft*2);
		var canvas = new Bitmap(width, height);
		var graphics = Graphics.FromImage(canvas);
		var textLayerSizeHeight = 0;

		graphics.FillRectangle(Brushes.White, new Rectangle(0, 0, width, height));

		if (includeCode) {
			var textLayerSize = graphics.MeasureString(content, new Font("Tahoma", 8));
			var textLayerSizeWidth = (int)Math.Ceiling(textLayerSize.Width);

			textLayerSizeHeight = (int)Math.Ceiling(textLayerSize.Height);

			graphics.DrawString(content, new Font("Tahoma", 8), Brushes.Black,
				new RectangleF((width / 2) - (textLayerSizeWidth / 2), height - textLayerSizeHeight, textLayerSizeWidth,
					textLayerSizeHeight));
		}

		foreach (var c in encodedBits.ToString()) {
			var rectangle = new Rectangle(offsetLeft += barWidth, 0, barWidth, height - textLayerSizeHeight);
			graphics.FillRectangle(c == '0' ? Brushes.White : Brushes.Black, rectangle);
		}

		return canvas;
	}
}