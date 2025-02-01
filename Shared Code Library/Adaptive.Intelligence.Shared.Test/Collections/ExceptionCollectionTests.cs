using AutoFixture;

namespace Adaptive.Intelligence.Shared.Test.Collections
{
    public class ExceptionCollectionTests
    {
        [Fact]
        public void ConstructorTest()
        {
            ExceptionCollection? list = new ExceptionCollection();
            Assert.NotNull(list);
        }
        [Fact]
        public void Constructor2Test()
        {
            ExceptionCollection? list = new ExceptionCollection(1000);
            Assert.NotNull(list);
            Assert.Equal(1000, list.Capacity);
        }
        [Fact]
        public void Constructor3Test()
        {
            List<Exception> exList = new List<Exception>()
            {
                new ArgumentException(),
                new ArgumentNullException()
            };
            ExceptionCollection? list = new ExceptionCollection(exList);
            Assert.NotNull(list);
            Assert.Equal(2, list.Count);
            Assert.IsType<ArgumentException>(list[0]);
            Assert.IsType<ArgumentNullException>(list[1]);

            list.Clear();
        }

        [Fact]
        public void FixtureTest()
        {
            Fixture fixture = new Fixture();
            ExceptionCollection? list = fixture.Create<ExceptionCollection>();
            Assert.NotNull(list);
        }

        [Fact]
        public void DisposeTest()
        {
            Fixture fixture = new Fixture();
            ExceptionCollection? list = fixture.Create<ExceptionCollection>();
            Assert.NotNull(list);

            list.Dispose();
            list.Dispose();
            list.Dispose();
            list.Dispose();
            list.Dispose();
        }

        [Fact]
        public void CloneTest()
        {
            Fixture fixture = new Fixture();
            ExceptionCollection? list = fixture.Create<ExceptionCollection>();
            Assert.NotNull(list);

            list.Add(new ArgumentException());

            ExceptionCollection? newList = list.Clone();
            Assert.NotNull(newList);
            Assert.Single(newList);

            Assert.IsType<ArgumentException>(newList[0]);

            list.Dispose();
            newList.Dispose();
        }
        [Fact]
        public void Clone2Test()
        {
            var fixture = new Fixture();
            var list = fixture.Create<ExceptionCollection>();
            Assert.NotNull(list);

            list.Add(new ArgumentException());

            ExceptionCollection? newList = (ExceptionCollection)((ICloneable)list).Clone();
            Assert.NotNull(newList);
            Assert.Single(newList);

            Assert.IsType<ArgumentException>(newList[0]);

            list.Dispose();
            newList.Dispose();
        }

    }
}
