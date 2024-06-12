using System.Reflection;

namespace Adaptive.Intelligence.Shared.Tests
{
	public class ExportIgnoreAttributeTests
	{
		private class TestClass
		{
			[ExportIgnore]
			public int IgnoredProperty { get; set; }

			public int NotDecoratedProperty { get; set; }
		}

		[Fact]
		public void ExportIgnoreAttribute_ShouldBeApplicableToProperty()
		{
			// Arrange
			var property = typeof(TestClass).GetProperty(nameof(TestClass.IgnoredProperty));

			// Act
			var attribute = property.GetCustomAttribute(typeof(ExportIgnoreAttribute));

			// Assert
			Assert.NotNull(attribute);
		}

		[Fact]
		public void ExportIgnoreAttribute_ShouldNotBeFoundOnNonDecoratedProperty()
		{
			// Arrange
			var nonDecoratedProperty = typeof(TestClass).GetProperty(nameof(TestClass.NotDecoratedProperty));

			// Act
			var attribute = nonDecoratedProperty.GetCustomAttribute(typeof(ExportIgnoreAttribute));

			// Assert
			Assert.Null(attribute); // Assuming there's no ExportIgnoreAttribute on the non-decorated property
		}
	}
}