namespace Adaptive.Intelligence.Shared.Tests.Collections
{
    public class AdaptiveStringCollectionTests
    {
        [Fact]
        public void Constructor_Default_ShouldCreateEmptyCollection()
        {
            var collection = new AdaptiveStringCollection();
            Assert.Empty(collection);
        }

        [Fact]
        public void ClearTest()
        {
            var collection = new AdaptiveStringCollection();
            collection.Clear();
            collection.Clear();
            collection.Clear();
            collection.Clear();

            collection.Add("ABCDE");
            collection.Clear();

            Assert.Empty(collection);
            collection.Clear();
        }

        [Fact]
        public void Constructor_WithSourceList_ShouldPopulateCollection()
        {
            var sourceList = new List<string> { "one", "two", "three" };
            var collection = new AdaptiveStringCollection(sourceList);
            Assert.Equal(sourceList.Count, collection.Count);
        }

        [Fact]
        public void AddRange_WithNonNullSource_ShouldAddItems()
        {
            var initialCollection = new AdaptiveStringCollection(new List<string> { "one" });
            var sourceData = new AdaptiveStringCollection(new List<string> { "two", "three" });
            initialCollection.AddRange(sourceData);
            Assert.Equal(3, initialCollection.Count);
        }

        [Fact]
        public void AddRange_WithNullSource_ShouldNotChangeCollection()
        {
            var initialCollection = new AdaptiveStringCollection(new List<string> { "one" });
            initialCollection.AddRange(null);
            Assert.Single(initialCollection);
        }

        [Fact]
        public void Clone_ShouldCreateDeepCopy()
        {
            var original = new AdaptiveStringCollection(new List<string> { "one" });
            var clone = original.Clone();

            Assert.Equal(original.Count, clone.Count);
            Assert.NotSame(original, clone);

            Assert.True(ContentsAreEqual(original.ToList(), clone.ToList()));
        }
        [Fact]
        public void CloneInterfaceTest()
        {
            var original = new AdaptiveStringCollection(new List<string> { "one" });
            object cloneBoxed = ((ICloneable)original).Clone();
            var clone = (AdaptiveStringCollection)cloneBoxed;


            Assert.Equal(original.Count, clone.Count);
            Assert.NotSame(original, clone);

            Assert.True(ContentsAreEqual(original.ToList(), clone.ToList()));

        }
        [Fact]
        public void Equals_WithEqualCollection_ShouldReturnTrue()
        {
            var collectionOne = new AdaptiveStringCollection(new List<string> { "one" });
            var collectionTwo = new AdaptiveStringCollection(new List<string> { "one" });
            Assert.True(collectionOne.Equals(collectionTwo));
        }

        [Fact]
        public void Equals_WithDifferentCollection_ShouldReturnFalse()
        {
            var collectionOne = new AdaptiveStringCollection(new List<string> { "one" });
            var collectionTwo = new AdaptiveStringCollection(new List<string> { "two" });
            Assert.False(collectionOne.Equals(collectionTwo));
        }
        [Fact]
        public void EqualsDifferentObjectTypesReturnsFalse()
        {
            var collectionOne = new AdaptiveStringCollection(new List<string> { "one" });
            var collectionTwo = new System.Collections.Specialized.StringCollection();
            collectionTwo.Add("two");

            Assert.False(collectionOne.Equals(collectionTwo));

        }
        [Fact]
        public void EqualsNullReturnsFalse()
        {
            var collectionOne = new AdaptiveStringCollection(new List<string> { "one" });

            List<string>? subListThatsNull = null;

            Assert.False(collectionOne.Equals(subListThatsNull));

        }
        [Fact]
        public void EqualsSuccess()
        {
            var collectionOne = new AdaptiveStringCollection(new List<string> { "one" });

            Assert.True(collectionOne.Equals(collectionOne));
        }

        [Fact]
        public void Set_WithNewList_ShouldReplaceCollection()
        {
            var originalList = new List<string> { "one" };
            var newList = new List<string> { "two", "three" };
            var collection = new AdaptiveStringCollection(originalList);
            collection.Set(newList);
            Assert.Equal(newList.Count, collection.Count);
        }

        [Fact]
        public void ToList_ShouldReturnCorrectList()
        {
            var sourceList = new List<string> { "one", "two", "three" };
            var collection = new AdaptiveStringCollection(sourceList);
            var resultList = collection.ToList();

            Assert.True(ContentsAreEqual(sourceList, resultList));
        }

        private static bool ContentsAreEqual(List<string> leftList, List<string> rightList)
        {
            var isGood = true;
            Assert.Equal(leftList.Count, rightList.Count);
            for (var count = 0; count < rightList.Count; count++)
            {
                if (leftList[count] != rightList[count])
                {
                    isGood = false;
                }
            }

            return isGood;
        }
    }
}
