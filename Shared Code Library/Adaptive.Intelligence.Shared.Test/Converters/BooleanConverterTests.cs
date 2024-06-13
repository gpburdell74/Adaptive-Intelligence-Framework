namespace Adaptive.Intelligence.Shared.Tests.Converters
{
	public class BooleanConverterTests
	{
		private readonly BooleanConverter _converter = new BooleanConverter();

		[Fact]
		public void Convert_WhenTrue_ReturnsYes()
		{
			var result = _converter.Convert(true);
			Assert.Equal(Constants.TrueFormatted, result);
		}

		[Fact]
		public void Convert_WhenFalse_ReturnsNo()
		{
			var result = _converter.Convert(false);
			Assert.Equal(Constants.FalseFormatted, result);
		}

		[Theory]
		[InlineData(Constants.TrueValueYes)]
		[InlineData(Constants.TrueValueSi)]
		[InlineData(Constants.TrueValueTrue)]
		[InlineData(Constants.TrueValueBT)]
		[InlineData(Constants.TrueValueBY)]
		[InlineData(Constants.TrueValueMinus1)]
		[InlineData(Constants.TrueValueOK)]
		public void ConvertBack_WhenTrueString_ReturnsTrue(string trueString)
		{
			var result = _converter.ConvertBack(trueString);
			Assert.True(result);
		}

		[Theory]
		[InlineData("")]
		[InlineData("no")]
		[InlineData("false")]
		[InlineData("0")]
		public void ConvertBack_WhenNotTrueString_ReturnsFalse(string notTrueString)
		{
			var result = _converter.ConvertBack(notTrueString);
			Assert.False(result);
		}
		[Fact]
		public void ConvertBack_WithVariousTrueRepresentations_ReturnsTrue()
		{
			// Testing various representations that should be interpreted as true
			var trueRepresentations = new string[] { "Yes", "Si", "True", ".T.", ".t.", ".Y.", ".y.","-1", "OK" };
			foreach (var representation in trueRepresentations)
			{
				var result = _converter.ConvertBack(representation);
				Assert.True(result, $"Expected true for representation: {representation}");
			}
		}

		[Fact]
		public void ConvertBack_WithVariousFalseRepresentations_ReturnsFalse()
		{
			// Testing various representations that should be interpreted as false
			var falseRepresentations = new string[] { "No", "0", "False", ".n.", "N", ".N.", "Neg", "-2" };
			foreach (var representation in falseRepresentations)
			{
				var result = _converter.ConvertBack(representation);
				Assert.False(result, $"Expected false for representation: {representation}");
			}
		}

		[Theory]
		[InlineData("yes", true)] // Case insensitivity test
		[InlineData("YES", true)] // Upper case test
		[InlineData("si", true)]  // Different language (Spanish) for yes
		[InlineData("no", false)] // Negative case
		[InlineData("NO", false)] // Negative case with upper case
		[InlineData("nO", false)] // Negative case with mixed case
		public void ConvertBack_CaseInsensitivityAndNegatives_ReturnsExpected(string input, bool expected)
		{
			var result = _converter.ConvertBack(input);
			Assert.Equal(expected, result);
		}

	}
}
