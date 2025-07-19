namespace Adaptive.Intelligence.Shared.Test.Data_Structures
{
    public class USAddressTests
    {
        [Fact]
        public void TestConstructorAndProperties()
        {
            var address = new USAddress
            {
                AddressLine1 = "123 Main St",
                AddressLine2 = "Suite 100",
                AddressLine3 = "Building 5",
                City = "Anytown",
                StateName = "Anystate",
                StateAbbreviation = "AS",
                ZipCode = "12345",
                ZipPlus4 = "6789"
            };

            Assert.Equal("123 Main St", address.AddressLine1);
            Assert.Equal("Suite 100", address.AddressLine2);
            Assert.Equal("Building 5", address.AddressLine3);
            Assert.Equal("Anytown", address.City);
            Assert.Equal("Anystate", address.StateName);
            Assert.Equal("AS", address.StateAbbreviation);
            Assert.Equal("12345", address.ZipCode);
            Assert.Equal("6789", address.ZipPlus4);
        }

        [Fact]
        public void TestDisposeMethod()
        {
            IStandardPostalAddress address = new USAddress
            {
                AddressLine1 = "123 Main St",
                AddressLine2 = "Suite 100",
                AddressLine3 = "Building 5",
                City = "Anytown",
                StateName = "Anystate",
                StateAbbreviation = "AS",
                ZipCode = "12345",
                ZipPlus4 = "6789"
            };

            address.Dispose();

            Assert.Null(address.AddressLine1);
            Assert.Null(address.AddressLine2);
            Assert.Null(address.AddressLine3);
            Assert.Null(address.City);
            Assert.Null(address.StateName);
            Assert.Null(address.StateAbbreviation);
            Assert.Null(address.ZipCode);
            Assert.Null(address.ZipPlus4);

            address.Dispose();
            address.Dispose();
            address.Dispose();
            address.Dispose();
            address.Dispose();
            address.Dispose();
        }

        [Theory]
        [InlineData("12345", true)]
        [InlineData("1234", false)]
        [InlineData("abcde", false)]
        [InlineData(null, false)]
        public void TestZipCodeIsValid(string zipPlus4, bool expectedValidity)
        {
            var address = new USAddress { ZipPlus4 = zipPlus4 };
            Assert.Equal(expectedValidity, address.ZipCodeIsValid);
        }

        [Theory]
        [InlineData("6789", true)]
        [InlineData("678", false)]
        [InlineData("abcd", false)]
        [InlineData(null, false)]
        public void TestZipPlus4IsValid(string zipPlus4, bool expectedValidity)
        {
            var address = new USAddress { ZipPlus4 = zipPlus4 };
            Assert.Equal(expectedValidity, address.ZipPlus4IsValid);
        }
    }
}
