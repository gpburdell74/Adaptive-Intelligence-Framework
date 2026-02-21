using System.Reflection;

namespace Adaptive.Intelligence.Shared.Test.Attributes
{
    public class ImportIgnoreAttributeTests
    {
        private class TestClass
        {
            [ImportIgnore]
            public int IgnoredProperty { get; set; }

            public int NotDecoratedProperty { get; set; }
        }

        [Fact]
        public void ImportIgnoreAttribute_ShouldBeApplicableToProperty()
        {
            // Arrange
            var property = typeof(TestClass).GetProperty(nameof(TestClass.IgnoredProperty));

            // Act
            Assert.NotNull(property); // Ensure the property exists);
            var attribute = property.GetCustomAttribute(typeof(ImportIgnoreAttribute));

            // Assert
            Assert.NotNull(attribute);
        }

        [Fact]
        public void ImportIgnoreAttribute_ShouldNotBeFoundOnNonDecoratedProperty()
        {
            // Arrange
            var property = typeof(TestClass).GetProperty(nameof(TestClass.NotDecoratedProperty));

            // Adding a non-decorated property for comparison
            var nonDecoratedProperty = typeof(TestClass).GetProperty(nameof(TestClass.NotDecoratedProperty));

            // Act
            Assert.NotNull(nonDecoratedProperty); // Ensure the property exists
            var attribute = nonDecoratedProperty.GetCustomAttribute(typeof(ImportIgnoreAttribute));

            // Assert
            Assert.Null(attribute);
        }
    }
}