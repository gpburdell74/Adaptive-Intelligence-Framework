using System.Collections.Specialized;

namespace Adaptive.Intelligence.Shared.Tests
{
	public class AdaptiveStringCollectionTests
	{
		[Fact]
		public void Constructor_Default_ShouldCreateEmptyCollection()
		{
			var collection = new AdaptiveStringCollection();
			Assert.Equal(0, collection.Count);
		}

		[Fact]
		public void ClearTest()
		{
			AdaptiveStringCollection collection = new AdaptiveStringCollection();
			collection.Clear();
			collection.Clear();
			collection.Clear();
			collection.Clear();

			collection.Add("ABCDE");
			collection.Clear();

			Assert.Equal(0, collection.Count);
			collection.Clear();
		}

		[Fact]
		public void Constructor_WithSourceList_ShouldPopulateCollection()
		{
			var sourceList = new List<string> { "one", "two", "three" };
			AdaptiveStringCollection collection = new AdaptiveStringCollection(sourceList);
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
			Assert.Equal(1, initialCollection.Count);
		}

		[Fact]
		public void Clone_ShouldCreateDeepCopy()
		{
			AdaptiveStringCollection original = new AdaptiveStringCollection(new List<string> { "one" });
			AdaptiveStringCollection clone = original.Clone();

			Assert.Equal(original.Count, clone.Count);
			Assert.NotSame(original, clone);

			Assert.True(ContentsAreEqual(original.ToList(), clone.ToList()));
		}
		[Fact]
		public void CloneInterfaceTest()
		{
			AdaptiveStringCollection original = new AdaptiveStringCollection(new List<string> { "one" });
			object cloneBoxed = ((ICloneable)original).Clone();
			AdaptiveStringCollection clone = (AdaptiveStringCollection)cloneBoxed;


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
		public void EqualsDifferentObjecTypesReturnsFalse()
		{
			AdaptiveStringCollection collectionOne = new AdaptiveStringCollection(new List<string> { "one" });
			System.Collections.Specialized.StringCollection collectionTwo = new System.Collections.Specialized.StringCollection();
			collectionTwo.Add("two");

			Assert.False(collectionOne.Equals(collectionTwo));

		}
		[Fact]
		public void EqualsNullReturnsFalse()
		{
			AdaptiveStringCollection collectionOne = new AdaptiveStringCollection(new List<string> { "one" });

			List<string>? subListThatsNull = null;

			Assert.False(collectionOne.Equals(subListThatsNull));

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

		private bool ContentsAreEqual(List<string> leftList, List<string> rightList)
		{
			bool isGood = true;
			Assert.Equal(leftList.Count, rightList.Count);
			for (int count = 0; count < rightList.Count; count++)
			{
				if (leftList[count] != rightList[count])
					isGood = false;
			}
			return isGood;
		}
	}
}
