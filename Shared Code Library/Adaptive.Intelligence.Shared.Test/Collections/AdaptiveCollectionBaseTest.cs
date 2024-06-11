namespace Adaptive.Intelligence.Shared.Collections
{
	/// <summary>
	/// Since the class being tested is abstract...
	/// </summary>
	/// <seealso cref="Adaptive.Intelligence.Shared.AdaptiveCollectionBase&lt;System.Int32&gt;" />
	public sealed class TestCollection : AdaptiveCollectionBase<int>
	{
		public TestCollection() : base()
		{
		}
		public TestCollection(int capacity) : base(capacity)
		{
		}
		public TestCollection(IEnumerable<int> sourceData) : base(sourceData)
		{
		}

		public void TestAccessRecordException(Exception ex)
		{
			base.RecordException(ex);
		}
	}

	public class AdaptiveCollectionBaseTest
	{
		[Fact]
		public void CreateTest()
		{
			TestCollection c = new TestCollection();
			Assert.NotNull(c);

			c = new TestCollection(23);
			Assert.NotNull(c);

			int[] data = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
			c = new TestCollection(data);
			Assert.NotNull(c);
			for (int count = 1; count < 11; count++)
			{
				Assert.Equal(count, c[count - 1]);
			}
			Assert.Equal(10, c.Count);
		}
		[Fact]
		public void DisposeTest()
		{
			TestCollection? c = new TestCollection();
			c.Add(1);
			c.Add(2);
			c.Add(3);
			c.Clear();
			c.Clear();
			c.Clear();
			c.Clear();
			c.Clear();
			c.Clear();
			c = null;
		}

		[Fact]
		public void RecordExceptionTest()
		{
			TestCollection c = new TestCollection();
			Exception ex = new Exception("Test Exception");
			c.TestAccessRecordException(ex);
		}

		[Fact]
		public void IsNullOrEmptyTest()
		{
			TestCollection? c = null;

			bool result = AdaptiveCollectionBase<int>.IsNullOrEmpty(c);
			Assert.True(result);

			result = TestCollection.IsNullOrEmpty(c);
			Assert.True(result);

			c = new TestCollection();
			result = AdaptiveCollectionBase<int>.IsNullOrEmpty(c);
			Assert.True(result);

			result = TestCollection.IsNullOrEmpty(c);
			Assert.True(result);

			c.Add(1);

			result = AdaptiveCollectionBase<int>.IsNullOrEmpty(c);
			Assert.False(result);

			result = TestCollection.IsNullOrEmpty(c);
			Assert.False(result);

			c.Clear();
			result = AdaptiveCollectionBase<int>.IsNullOrEmpty(c);
			Assert.True(result);

			result = TestCollection.IsNullOrEmpty(c);
			Assert.True(result);
		}

	}
}
