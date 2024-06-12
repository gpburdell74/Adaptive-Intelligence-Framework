namespace Adaptive.Intelligence.Shared.Tests
{
	public class DateConverterTests
	{
		[Fact]
		public void Convert_EmptyString_ReturnsDefaultDate()
		{
			var converter = new DateConverter();
			var result = converter.Convert(string.Empty);
			Assert.Equal(new DateTime(1900, 1, 1), result);
		}

		[Theory]
		[InlineData("2023/04/01", 2023, 4, 1)]
		[InlineData("2023-04-01", 2023, 4, 1)]
		[InlineData("2023.04.01", 2023, 4, 1)]
		public void Convert_ValidDateString_ReturnsCorrectDate(string dateString, int year, int month, int day)
		{
			DateConverter converter = new DateConverter();
			DateTime expectedDate = new DateTime(year, month, day);
			DateTime result = converter.Convert(dateString);
			Assert.Equal(expectedDate, result);
		}

		[Fact]
		public void Convert_InvalidDateString_ReturnsDefaultDate()
		{
			var converter = new DateConverter();
			var result = converter.Convert("InvalidDate");
			Assert.Equal(new DateTime(1900, 1, 1), result);
		}

		[Fact]
		public void ConvertBack_DateTime_ReturnsUSFormattedString()
		{
			var converter = new DateConverter();
			var date = new DateTime(2023, 4, 1);
			var result = converter.ConvertBack(date);
			Assert.Equal("04/01/2023", result); // Assuming Constants.USDateFormat is "MM/dd/yyyy"
		}
	}
}
