namespace Adaptive.Intelligence.Shared.Test.Bases
{
    public class BusinessCollectionBaseTests
    {
        [Fact]
        public void Constructor_InitializesEmptyCollection()
        {
            var collection = new BusinessCollectionBase<MockBusinessBase>();
            Assert.Empty(collection);
        }

        [Fact]
        public void Constructor_WithCapacity_SetsInitialCapacity()
        {
            var collection = new BusinessCollectionBase<MockBusinessBase>(10);
            Assert.Empty(collection); // Collection should be empty but with specified capacity
        }

        [Fact]
        public void Constructor_WithIEnumerable_PopulatesCollection()
        {
            var items = new List<MockBusinessBase> { new MockBusinessBase(), new MockBusinessBase() };
            var collection = new BusinessCollectionBase<MockBusinessBase>(items);
            Assert.Equal(items.Count, collection.Count);
        }

        [Fact]
        public async Task RemoveAndDeleteAsync_RemovesAndDeletesItem()
        {
            var item = new MockBusinessBase();
            var collection = new BusinessCollectionBase<MockBusinessBase> { item };
            bool result = await collection.RemoveAndDeleteAsync(item);

            Assert.True(result);
            Assert.Empty(collection);
        }

        [Fact]
        public async Task RemoveAndDeleteAsync_ReAddsItemOnDeleteFailure()
        {
            MockBusinessBase item = new MockBusinessBase();
            item.SaveFailureFlagForTest = true;

            var collection = new BusinessCollectionBase<MockBusinessBase> { item };
            bool result = await collection.RemoveAndDeleteAsync(item);

            Assert.False(result);
            Assert.Contains(item, collection);
        }

        [Fact]
        public void IsNullOrEmpty_ReturnsTrueForNull()
        {
            bool result = BusinessCollectionBase<MockBusinessBase>.IsNullOrEmpty(null);
            Assert.True(result);
        }

        [Fact]
        public void IsNullOrEmpty_ReturnsTrueForEmpty()
        {
            var collection = new BusinessCollectionBase<MockBusinessBase>();
            bool result = BusinessCollectionBase<MockBusinessBase>.IsNullOrEmpty(collection);
            Assert.True(result);
        }

        [Fact]
        public void IsNullOrEmpty_ReturnsFalseForNonEmpty()
        {
            var collection = new BusinessCollectionBase<MockBusinessBase> { new MockBusinessBase() };
            bool result = BusinessCollectionBase<MockBusinessBase>.IsNullOrEmpty(collection);
            Assert.False(result);
        }

        [Fact]
        public async Task InnerRemoveTest()
        {
            BusinessCollectionBase<MockBusinessBase> collection = new BusinessCollectionBase<MockBusinessBase>();

            MockBusinessBase item = new MockBusinessBase();
            collection.Add(item);
            await collection.RemoveAndDeleteAsync(item);
        }
        [Fact]
        public async Task InnerRemoveTest_HandleException_WhenNotInCollection()
        {
            BusinessCollectionBase<MockBusinessBase> collection = new BusinessCollectionBase<MockBusinessBase>();

            MockBusinessBase item = new MockBusinessBase();
            item.SaveFailureFlagForTest = true;

            await collection.RemoveAndDeleteAsync(item);
        }
        [Fact]
        public async Task InnerRemoveTest_HandleException_WhenSaveFails()
        {
            BusinessCollectionBase<MockBusinessBase> collection = new BusinessCollectionBase<MockBusinessBase>();

            MockBusinessBase item = new MockBusinessBase();
            collection.Add(item);
            item.SaveFailureFlagForTest = true;

            await collection.RemoveAndDeleteAsync(item);

            Assert.Single(collection);
        }
    }
}