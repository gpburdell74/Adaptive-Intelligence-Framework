namespace Adaptive.Intelligence.Shared.Tests
{
	public class AdaptiveFormatTests
	{
		[Fact]
		public void FormatBoolean_True_ReturnsYes()
		{
			// Arrange
			bool value = true;
			string expected = "Yes";

			// Act
			string actual = AdaptiveFormat.FormatBoolean(value);

			// Assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void FormatBoolean_False_ReturnsNo()
		{
			// Arrange
			bool value = false;
			string expected = "No";

			// Act
			string actual = AdaptiveFormat.FormatBoolean(value);

			// Assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void FormatByteString_GreaterThanGBSize_ReturnsGB()
		{
			// Arrange
			long numberOfBytes = 1048576 * 1024 + 1;
			string expected = "1.0 GB";

			// Act
			string actual = AdaptiveFormat.FormatByteString(numberOfBytes);

			// Assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void FormatByteString_GreaterThanMBSize_ReturnsMB()
		{
			// Arrange
			long numberOfBytes = 1048576 + 1;
			string expected = "1.0 MB";

			// Act
			string actual = AdaptiveFormat.FormatByteString(numberOfBytes);

			// Assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void FormatByteString_GreaterThanKBSize_ReturnsKB()
		{
			// Arrange
			long numberOfBytes = 1024 + 1;
			string expected = "1.0 KB";

			// Act
			string actual = AdaptiveFormat.FormatByteString(numberOfBytes);

			// Assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void FormatByteString_LessThanKBSize_ReturnsBytes()
		{
			// Arrange
			long numberOfBytes = 1;
			string expected = "1 byte(s)";

			// Act
			string actual = AdaptiveFormat.FormatByteString(numberOfBytes);

			// Assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void FormatAddressAllNull_ReturnsEmptyString()
		{
			// Arrange
			string line1 = null;
			string line2 = null;
			string expected = "";

			// Act
			string actual = AdaptiveFormat.FormatAddress(line1, line2);

			// Assert
			Assert.Equal(expected, actual);
		}
		[Fact]
		public void FormatAddressLine1NotNull_ReturnsAddress()
		{
			// Arrange
			string? line1 = "3780 Ridgefair Dr.";
			string? line2 = null;
			string expected = "3780 Ridgefair Dr.\r\n";

			// Act
			string actual = AdaptiveFormat.FormatAddress(line1, line2);

			// Assert
			Assert.Equal(expected, actual);
		}
		[Fact]
		public void FormatAddressLine1Line2NotNull_ReturnsAddress()
		{
			// Arrange
			string? line1 = "3780 Ridgefair Dr.";
			string? line2 = "Room 42";
			string expected = "3780 Ridgefair Dr.\r\nRoom 42\r\n";

			// Act
			string actual = AdaptiveFormat.FormatAddress(line1, line2);

			// Assert
			Assert.Equal(expected, actual);
		}
		[Fact]
		public void FormatAddressLine1IsNull_ReturnsAddress()
		{
			// Arrange
			string? line1 = null;
			string? line2 = "Room 42";
			string expected = "Room 42\r\n";

			// Act
			string actual = AdaptiveFormat.FormatAddress(line1, line2);

			// Assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void FormatDateTest()
		{
			// Arrange.
			DateTime now = DateTime.Now.Date;
			string result = string.Empty;

			// Act
			result = AdaptiveFormat.FormatDate(now);
			string expected = now.ToString("MM/dd/yyyy");

			// Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public void FormatDateAndTimeTest()
		{
			// Arrange.
			DateTime now = DateTime.Now;
			string result = string.Empty;

			// Act
			result = AdaptiveFormat.FormatDateAndTime(now);
			string expected = now.ToString("MM/dd/yyyy hh:mm:ss tt");

			// Assert
			Assert.Equal(expected, result);
		}
		[Fact]
		public void FormatDateAndTimeAsSeperateObjectsTest()
		{
			// Arrange.
			DateTime now = DateTime.Now.Date;
			Time time = new Time(4, 32, 45);

			string result = string.Empty;

			// Act
			result = AdaptiveFormat.FormatDateAndTime(now, time);
			string expected = now.ToString("MM/dd/yyyy") + " 4:32 AM";

			// Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public void FormatDateValue_ValidDate_ReturnsFormattedString()
		{
			// Arrange
			DateTime testDate = new DateTime(2023, 1, 1);
			string expected = "01/01/2023";

			// Act
			string actual = AdaptiveFormat.FormatDateValue(testDate);

			// Assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void FormatDateValue_RidiculousDate_ReturnsEmptyString()
		{
			// Arrange
			DateTime testDate = DateTime.MinValue; // Assuming DateTime.MinValue is considered a "RidiculousDate"
			string expected = "";

			// Act
			string actual = AdaptiveFormat.FormatDateValue(testDate);

			// Assert
			Assert.Equal(expected, actual);
		}

		[Theory]
		[InlineData("M", "Male")]
		[InlineData("F", "Female")]
		[InlineData("", "")]
		public void FormatGender_ValidInput_ReturnsExpectedOutput(string input, string expected)
		{
			// Act
			string actual = AdaptiveFormat.FormatGender(input);

			// Assert
			Assert.Equal(expected, actual);
		}
		[Fact]
		public void FormatMailingAddressTest()
		{
			IStandardPostalAddress address = new USAddress
			{
				AddressLine1 = "3780 Ridgefair Dr.",
				City = "Cumming",
				StateAbbreviation = "GA",
				ZipCode = "30040"
			};

			string result = AdaptiveFormat.FormatMailingAddress(address);
			string expected = "3780 Ridgefair Dr.\r\nCumming, GA 30040";

			Assert.Equal(expected, result);

			IStandardPostalAddress address2 = new USAddress
			{
				AddressLine1 = "1234 10th St.",
				AddressLine2 = "Suite 100",
				City = "Atlanta",
				StateAbbreviation = "GA",
				ZipCode = "30332",
				ZipPlus4 = "1234",
				StateName = "Georgia",
				AddressLine3 = "c/o George Burdell"
			};

			string result2 = AdaptiveFormat.FormatMailingAddress(address2);
			string expected2 = "1234 10th St.\r\nSuite 100\r\nc/o George Burdell\r\nAtlanta, GA 30332-1234";

			Assert.Equal(expected2, result2);

			IStandardPostalAddress address3 = new USAddress
			{
				AddressLine1 = "1234 10th St.",
				AddressLine2 = "Suite 100",
				City = "Atlanta",
				ZipCode = "30332",
				ZipPlus4 = "1234",
				StateName = "Georgia",
				AddressLine3 = "c/o George Burdell"
			};

			string result3 = AdaptiveFormat.FormatMailingAddress(address3);
			string expected3 = "1234 10th St.\r\nSuite 100\r\nc/o George Burdell\r\nAtlanta, Georgia 30332-1234";

			Assert.Equal(expected3, result3);

		}
		[Fact]
		public void FormatMailingAddress_NullAddress_ReturnsNull()
		{
			// Arrange
			IStandardPostalAddress address = null;

			// Act
			var result = AdaptiveFormat.FormatMailingAddress(address);

			// Assert
			Assert.Null(result);
		}

		[Theory]
		[InlineData("12345", true)]
		[InlineData("12345abc", false)]
		[InlineData("", false)]
		public void IsNumeric_InputString_ReturnsExpectedBool(string input, bool expected)
		{
			// Act
			bool actual = AdaptiveFormat.IsNumeric(input);

			// Assert
			Assert.Equal(expected, actual);
		}

		[Theory]
		[InlineData("Hello World", "helloworld")]
		[InlineData("123, ABC.", "123abc")]
		public void LowerNormalizeString_InputString_ReturnsNormalizedString(string input, string expected)
		{
			// Act
			string actual = AdaptiveFormat.LowerNormalizeString(input);

			// Assert
			Assert.Equal(expected, actual);
		}
	}
}
