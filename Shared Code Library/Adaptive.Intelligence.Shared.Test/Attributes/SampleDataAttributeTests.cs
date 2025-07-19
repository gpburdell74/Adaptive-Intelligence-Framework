namespace Adaptive.Intelligence.Shared.Test.Attributes
{
    public class SampleDataAttributeTests
    {
        [Fact]
        public void Constructor_Default_ShouldInitializeWithNullSampleData()
        {
            var attribute = new SampleDataAttribute();
            Assert.Null(attribute.SampleData);
        }

        [Theory]
        [InlineData("Example Data")]
        [InlineData("")]
        [InlineData(null)]
        public void Constructor_WithSampleDataText_ShouldSetSampleDataProperty(string sampleData)
        {
            var attribute = new SampleDataAttribute(sampleData);
            Assert.Equal(sampleData, attribute.SampleData);
        }

        [Fact]
        public void SampleDataProperty_ShouldReturnCorrectSampleData()
        {
            string sampleData = "Test Data";
            var attribute = new SampleDataAttribute(sampleData);
            Assert.Equal(sampleData, attribute.SampleData);
        }
    }
}