using Adaptive.Intelligence.Shared.IO;

namespace Adaptive.Intelligence.Shared.Tests.Utility.Financial
{
	public class BINRuleTests
	{
		[Fact]
		public void Dispose_ShouldClearProperties()
		{
			var binRule = new BINRule
			{
				BankOrIssuerName = "Test Bank",
				PrefixMax = "999999",
				PrefixMin = "000000",
				CardNumberMaxLength = 16,
				CardNumberMinLength = 13,
				ImageData = new byte[] { 0x00, 0x01, 0x02 }
			};

			binRule.Dispose();

			Assert.Null(binRule.BankOrIssuerName);
			Assert.Null(binRule.PrefixMax);
			Assert.Null(binRule.PrefixMin);
			Assert.Null(binRule.ImageData);
		}

		[Fact]
		public void PropertySetAndGet_ShouldReturnExpectedValues()
		{
			var binRule = new BINRule
			{
				BankOrIssuerName = "Test Bank",
				PrefixMax = "999999",
				PrefixMin = "000000",
				CardNumberMaxLength = 16,
				CardNumberMinLength = 13,
				ImageData = new byte[] { 0x00, 0x01, 0x02 }
			};

			Assert.Equal("Test Bank", binRule.BankOrIssuerName);
			Assert.Equal("999999", binRule.PrefixMax);
			Assert.Equal("000000", binRule.PrefixMin);
			Assert.Equal(16, binRule.CardNumberMaxLength);
			Assert.Equal(13, binRule.CardNumberMinLength);
			Assert.Equal(new byte[] { 0x00, 0x01, 0x02 }, binRule.ImageData);
		}

		[Theory]
		[InlineData("1234567890123", true)] // Within range and valid length
		[InlineData("9999997890123", true)] // Max prefix and valid length
		[InlineData("0000007890123", true)] // Min prefix and valid length
		[InlineData("123456", false)] // Valid prefix but invalid length
		[InlineData("9876543210987", true)] // Valid length
		public void Matches_ShouldReturnExpectedResult(string cardNumber, bool expectedResult)
		{
			var binRule = new BINRule
			{
				PrefixMax = "999999",
				PrefixMin = "000000",
				CardNumberMaxLength = 16,
				CardNumberMinLength = 13
			};

			var result = binRule.Matches(cardNumber);

			Assert.Equal(expectedResult, result);
		}
		[Theory]
		[InlineData("1234567890123", false)]
		[InlineData("9999997890123", false)]
		[InlineData("0000007890123", false)]
		[InlineData("123456", false)]
		[InlineData("3006543210987", true)] // Valid length
		public void Matches_ShouldReturnExpectedResult_Limited(string cardNumber, bool expectedResult)
		{
			var binRule = new BINRule
			{
				PrefixMax = "555666",
				PrefixMin = "223222",
				CardNumberMaxLength = 16,
				CardNumberMinLength = 13
			};

			var result = binRule.Matches(cardNumber);

			Assert.Equal(expectedResult, result);
		}
		[Fact]
		public void ImageTest()
		{
			BINRule rule = new BINRule();
			rule.ImageData = SafeIO.ReadBytesFromFile("D:\\Adaptive.Intelligence\\Graphics\\Credit Cards\\Amex.jpg");
			Assert.NotNull(rule.ImageData);

			rule.ImageData = SafeIO.ReadBytesFromFile("D:\\Adaptive.Intelligence\\Graphics\\Credit Cards\\VISA.jpg");
			Assert.NotNull(rule.ImageData);

			rule.Dispose();
		}
	}
}
