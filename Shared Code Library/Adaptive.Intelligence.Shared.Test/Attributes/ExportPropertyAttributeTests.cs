using System.Reflection;

namespace Adaptive.Intelligence.Shared.Tests.Attributes
{
    public class ExportPropertyAttributeTests
    {
        private class TestClass
        {
            [ExportProperty]
            public int ExportedProperty { get; set; }

            public int NonDecoratedProperty { get; set; }
        }

        [Fact]
        public void ExportPropertyAttribute_ShouldBeApplicableToProperty()
        {
            // Arrange
            var property = typeof(TestClass).GetProperty(nameof(TestClass.ExportedProperty));

            // Act
            var attribute = property?.GetCustomAttribute(typeof(ExportPropertyAttribute));

            // Assert
            Assert.NotNull(attribute);
        }

        [Fact]
        public void ExportPropertyAttribute_ShouldNotBeFoundOnNonDecoratedProperty()
        {
            // Arrange
            var nonDecoratedProperty = typeof(TestClass).GetProperty(nameof(TestClass.NonDecoratedProperty));

            // Act
            var attribute = nonDecoratedProperty.GetCustomAttribute(typeof(ExportPropertyAttribute));

            // Assert
            Assert.Null(attribute); // Assuming there's no ExportPropertyAttribute on the non-decorated property
        }
    }
}
