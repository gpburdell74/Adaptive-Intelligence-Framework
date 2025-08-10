namespace Adaptive.Intelligence.Shared.Test.Data_Structures
{
    public class NameValuePairTests
    {
        [Fact]
        public void TestConstructorAndProperties()
        {
            NameValuePair<int> item = new NameValuePair<int>();
            Assert.Null(item.Name);
            Assert.Equal(0, item.Value);

            item = new NameValuePair<int>("Test", 1);
            Assert.Equal("Test", item.Name);
            Assert.Equal(1, item.Value);

        }
        [Fact]
        public void DisposeTest()
        {
            NameValuePair<int> item = new NameValuePair<int>();
            item.Dispose();
            Assert.Null(item.Name);

            item.Dispose();
            item.Dispose();
            item.Dispose();

            item = new NameValuePair<int>("Test", 1);
            Assert.Equal("Test", item.Name);
            Assert.Equal(1, item.Value);

            item.Dispose();
            Assert.Null(item.Name);
            item.Dispose();
            item.Dispose();
        }
        [Fact]
        public void PropertyTests()
        {
            NameValuePair<int> item = new NameValuePair<int>();

            item.Name = "Test";
            item.Value = 3;
            Assert.Equal("Test", item.Name);
            Assert.Equal(3, item.Value);

            item.Name = null;
            item.Value = 32;
            Assert.Null(item.Name);
            Assert.Equal(32, item.Value);

            item.Dispose();
        }
    }
}
