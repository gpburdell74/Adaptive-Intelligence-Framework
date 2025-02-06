namespace Adaptive.Intelligence.Shared.Tests.Utility.Financial
{
    public class BINRuleFactoryTests
    {
        [Fact]
        public void CreateBINRulesList_ReturnsCorrectCount()
        {
            // Arrange
            int expectedRuleCount = 21;

            // Act
            BINRuleCollection ruleList = BINRuleFactory.CreateBINRulesList();

            // Assert
            Assert.Equal(expectedRuleCount, ruleList.Count);
        }

        [Fact]
        public void CreateBINRulesList_ContainsExpectedCardTypes()
        {
            // Arrange
            var expectedCardTypes = new[] { "American Express", "VISA", "Master Card" };

            // Act
            var ruleList = BINRuleFactory.CreateBINRulesList();
            var cardTypes = ruleList.Select(rule => rule.BankOrIssuerName).Distinct();

            // Assert
            foreach (var cardType in expectedCardTypes)
            {
                Assert.Contains(cardType, cardTypes);
            }
        }

        [Fact]
        public void CreateBINRulesList_RulesHaveCorrectProperties()
        {
            // Act
            var ruleList = BINRuleFactory.CreateBINRulesList();
            var amexRule = ruleList.FirstOrDefault(rule => rule.BankOrIssuerName == "American Express");

            // Assert
            Assert.NotNull(amexRule);
            Assert.Equal("340000", amexRule.PrefixMin);
            Assert.Equal("349999", amexRule.PrefixMax);
            Assert.Equal(15, amexRule.CardNumberMinLength);
            Assert.Equal(15, amexRule.CardNumberMaxLength);
        }
    }
}
