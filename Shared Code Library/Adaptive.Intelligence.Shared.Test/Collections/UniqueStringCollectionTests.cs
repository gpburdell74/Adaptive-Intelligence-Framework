using AutoFixture;

namespace Adaptive.Intelligence.Shared.Test.Collections
{
    public class UniqueStringCollectionTests
    {
        private readonly Fixture _fixture;

        public UniqueStringCollectionTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void ConstructorTest()
        {
            UniqueStringCollection? list = new UniqueStringCollection();
            Assert.NotNull(list);
        }

        [Fact]
        public void Add_NewValue_AddsToCollection()
        {
            // Arrange
            var collection = new UniqueStringCollection();
            var newValue = _fixture.Create<string>();

            // Act
            var result = collection.Add(newValue);

            // Assert
            Assert.Equal(0, result);
            Assert.True(collection.Contains(newValue));
        }

        [Fact]
        public void Add_DuplicateValue_DoesNotAddToCollection()
        {
            // Arrange
            var collection = new UniqueStringCollection();
            var newValue = _fixture.Create<string>();
            collection.Add(newValue);

            // Act
            var result = collection.Add(newValue);

            // Assert
            Assert.Equal(-1, result);
            Assert.Single(collection);
        }

        [Fact]
        public void AddRange_NewValues_AddsToCollection()
        {
            // Arrange
            var collection = new UniqueStringCollection();
            var newValues = _fixture.CreateMany<string>(5);

            // Act
            collection.AddRange(newValues);

            // Assert
            foreach (var value in newValues)
            {
                Assert.True(collection.Contains(value));
            }
            Assert.Equal(5, collection.Count);
        }

        [Fact]
        public void AddRange_DuplicateValues_DoesNotAddDuplicates()
        {
            // Arrange
            var collection = new UniqueStringCollection();
            var newValues = _fixture.CreateMany<string>(5);
            collection.AddRange(newValues);

            // Act
            collection.AddRange(newValues);

            // Assert
            foreach (var value in newValues)
            {
                Assert.True(collection.Contains(value));
            }
            Assert.Equal(5, collection.Count);
        }

        [Fact]
        public void Add_NullValue_DoesNotAddToCollection()
        {
            // Arrange
            var collection = new UniqueStringCollection();

            // Act
            var result = collection.Add(null);

            // Assert
            Assert.Equal(-1, result);
            Assert.Empty(collection);
        }
    }
}