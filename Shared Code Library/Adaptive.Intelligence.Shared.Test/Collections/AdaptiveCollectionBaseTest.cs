namespace Adaptive.Intelligence.Shared.Tests.Collections
{
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
            var c = new TestCollection();
            Assert.NotNull(c);

            c = new TestCollection(23);
            Assert.NotNull(c);

            var data = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            c = new TestCollection(data);
            Assert.NotNull(c);
            for (var count = 1; count < 11; count++)
            {
                Assert.Equal(count, c[count - 1]);
            }
            Assert.Equal(10, c.Count);
        }
        [Fact]
        public void DisposeTest()
        {
            var c = new TestCollection();
            c.Add(1);
            c.Add(2);
            c.Add(3);
            c.Clear();
            c.Clear();
            c.Clear();
            c.Clear();
            c.Clear();
            c.Clear();
        }

        [Fact]
        public void RecordExceptionTest()
        {
            var c = new TestCollection();
            var ex = new Exception("Test Exception");
            c.TestAccessRecordException(ex);
        }

        [Fact]
        public void IsNullOrEmptyTest()
        {
            TestCollection? c = null;

            var result = c.IsNullOrEmpty();
            Assert.True(result);

            result = ListExtensions.IsNullOrEmpty(c);
            Assert.True(result);

            c = new TestCollection();
            result = ListExtensions.IsNullOrEmpty(c);
            Assert.True(result);

            result = ListExtensions.IsNullOrEmpty(c);
            Assert.True(result);

            c.Add(1);

            result = ListExtensions.IsNullOrEmpty(c);
            Assert.False(result);

            result = c.IsNullOrEmpty();
            Assert.False(result);

            c.Clear();
            result = ListExtensions.IsNullOrEmpty(c);
            Assert.True(result);

            result = c.IsNullOrEmpty();
            Assert.True(result);
        }
    }
}
