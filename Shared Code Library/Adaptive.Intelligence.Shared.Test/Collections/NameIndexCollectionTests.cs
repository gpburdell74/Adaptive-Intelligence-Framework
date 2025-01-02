using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adaptive.Intelligence.Shared.Test.Collections
{
    public class NameIndexCollectionTests
    {
        private readonly Fixture _fixture;

        public NameIndexCollectionTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void ConstructorTest()
        {
            TestNameIndexCollection? list = new TestNameIndexCollection();
            Assert.NotNull(list);
        }
        [Fact]
        public void Constructor2Test()
        {
            TestNameIndexCollection? list = new TestNameIndexCollection(1000);
            Assert.NotNull(list);
            Assert.Equal(1000, list.Capacity);
        }
        [Fact]
        public void Constructor3Test()
        {
            List<TestItem> sourcelist = new List<TestItem>
            {
                _fixture.Create<TestItem>(),
                _fixture.Create<TestItem>()
            };

            string a = sourcelist[0].Name;
            string b = sourcelist[1].Name;

            TestNameIndexCollection? list = new TestNameIndexCollection(2);
            list.AddRange(sourcelist);
            Assert.NotNull(list);
            Assert.Equal(2, list.Count);
            Assert.IsType<TestItem>(list[0]);
            Assert.IsType<TestItem>(list[1]);

            Assert.NotNull(list[a]);
            Assert.NotNull(list[b]);
            list.Clear();
        }

        [Fact]
        public void Add_Item_AddsToCollection()
        {
            // Arrange
            var collection = new TestNameIndexCollection();
            var item = _fixture.Create<TestItem>();

            // Act
            collection.Add(item);

            // Assert
            Assert.Contains(item, collection);
            Assert.True(collection.Contains(item.Name));
        }

        [Fact]
        public void AddRange_Items_AddsToCollection()
        {
            // Arrange
            var collection = new TestNameIndexCollection();
            var items = _fixture.CreateMany<TestItem>(5);

            // Act
            collection.AddRange(items);

            // Assert
            foreach (var item in items)
            {
                Assert.Contains(item, collection);
                Assert.True(collection.Contains(item.Name));
            }
        }

        [Fact]
        public void Contains_ItemExists_ReturnsTrue()
        {
            // Arrange
            var collection = new TestNameIndexCollection();
            var item = _fixture.Create<TestItem>();
            collection.Add(item);

            // Act
            var result = collection.Contains(item.Name);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Contains_ItemDoesNotExist_ReturnsFalse()
        {
            // Arrange
            var collection = new TestNameIndexCollection();
            var itemName = _fixture.Create<string>();

            // Act
            var result = collection.Contains(itemName);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Clear_Collection_ClearsAllItems()
        {
            // Arrange
            var collection = new TestNameIndexCollection();
            var items = _fixture.CreateMany<TestItem>(5);
            collection.AddRange(items);

            // Act
            collection.Clear();

            // Assert
            Assert.Empty(collection);
        }

        [Fact]
        public void Insert_Item_InsertsAtIndex()
        {
            // Arrange
            var collection = new TestNameIndexCollection();
            var item = _fixture.Create<TestItem>();

            // Act
            collection.Insert(0, item);

            // Assert
            Assert.Equal(item, collection[0]);
            Assert.True(collection.Contains(item.Name));
        }

        [Fact]
        public void Remove_Item_RemovesFromCollection()
        {
            // Arrange
            var collection = new TestNameIndexCollection();
            var item = _fixture.Create<TestItem>();
            collection.Add(item);

            // Act
            collection.Remove(item);

            // Assert
            Assert.DoesNotContain(item, collection);
            Assert.False(collection.Contains(item.Name));
        }

        [Fact]
        public void RemoveByName_Item_RemovesFromCollection()
        {
            // Arrange
            var collection = new TestNameIndexCollection();
            var item = _fixture.Create<TestItem>();
            collection.Add(item);

            // Act
            collection.Remove(item.Name);

            // Assert
            Assert.DoesNotContain(item, collection);
            Assert.False(collection.Contains(item.Name));
        }

        [Fact]
        public void RemoveAt_Index_RemovesFromCollection()
        {
            // Arrange
            var collection = new TestNameIndexCollection();
            var item = _fixture.Create<TestItem>();
            collection.Add(item);

            // Act
            collection.RemoveAt(0);

            // Assert
            Assert.DoesNotContain(item, collection);
            Assert.False(collection.Contains(item.Name));
        }

        [Fact]
        public void Indexer_Get_ReturnsCorrectItem()
        {
            // Arrange
            var collection = new TestNameIndexCollection();
            var item = _fixture.Create<TestItem>();
            collection.Add(item);

            // Act
            var result = collection[item.Name];

            // Assert
            Assert.Equal(item, result);
        }

        [Fact]
        public void Indexer_Set_UpdatesItem()
        {
            // Arrange
            var collection = new TestNameIndexCollection();
            var item = _fixture.Create<TestItem>();
            collection.Add(item);
            var newItem = _fixture.Create<TestItem>();
            newItem.Name = item.Name;

            // Act
            collection[item.Name] = newItem;

            // Assert
            Assert.Equal(newItem, collection[item.Name]);
        }
    }

    public class TestItem
    {
        public string Name { get; set; } = string.Empty;
    }

    public class TestNameIndexCollection : NameIndexCollection<TestItem>
    {
        public TestNameIndexCollection() : base()
        {

        }
        public TestNameIndexCollection(int capacity) : base(capacity)
        {
        }
        protected override string GetName(TestItem item)
        {
            return item.Name;
        }
    }
}