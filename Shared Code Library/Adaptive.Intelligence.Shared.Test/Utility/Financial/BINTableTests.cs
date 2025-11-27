namespace Adaptive.Intelligence.Shared.Test.Utility.Financial;
public class BINTableTests
{
    [Fact]
    public void CreateTest()
    {
        // Arrange & Act
        BINTable table = new BINTable();

        // Assert
        Assert.NotNull(table);
        
        // Alleviate.
        table.Dispose();

    }

    [Fact]
    public void GetMatchingRule_Basic_VISA_Test_ReturnsRule()
    {
        BINTable table = new BINTable();

        BINRule? rule = table.GetMatchingRule("4000123412341234");

        Assert.NotNull(rule);
        Assert.Equal(rule.BankOrIssuerName, "VISA");

        table.Dispose();

    }
    [Fact]
    public void GetMatchingRule_InvalidCardNumber_TooShort_ReturnsNull()
    {
        BINTable table = new BINTable();

        BINRule? rule = table.GetMatchingRule("A");

        Assert.Null(rule);
        

        table.Dispose();

    }
    [Fact]
    public void GetMatchingRule_NullCardNumber_ReturnsNull()
    {
        BINTable table = new BINTable();

        BINRule? rule = table.GetMatchingRule(string.Empty);
        Assert.Null(rule);

        table.Dispose();
    }

    [Theory]
    [InlineData("4000123412341234", "VISA")]
    [InlineData("340000123412341", "American Express")]
    [InlineData("340712123412341", "American Express")]
    [InlineData("340999123412341", "American Express")] 
    [InlineData("370000123412341", "American Express")] 
    [InlineData("379999123412341", "American Express")]
    [InlineData("5610001234123412", "BankCard")]
    [InlineData("5610991234123412", "BankCard")]
    [InlineData("5602211234123412", "BankCard")]
    [InlineData("5602251234123412", "BankCard")]
    [InlineData("6229991234123412", "China Union Pay")]
    [InlineData("6220001234123412", "China Union Pay")]
    [InlineData("6249991234123412", "China Union Pay")]
    [InlineData("6240001234123412", "China Union Pay")]
    [InlineData("6260001234123412", "China Union Pay")]
    [InlineData("6269991234123412", "China Union Pay")]
    [InlineData("6280001234123412", "China Union Pay")]
    [InlineData("6289991234123412", "China Union Pay")]
    [InlineData("5019001234123412", "Dankort")]
    [InlineData("5019991234123412", "Dankort")]
    public void GetMatchingRule_Valid_ReturnsRule(string cardNumber, string expectedBankName)
    {
        // Arrange
        BINTable table = new BINTable();
        // Act
        BINRule? rule = table.GetMatchingRule(cardNumber);
        // Assert
        Assert.NotNull(rule);
        Assert.Equal(expectedBankName, rule.BankOrIssuerName);
        // Cleanup
        table.Dispose();
    }
}
