using Xunit;

namespace Adaptive.Intelligence.Shared.Test.Bases
{
    // Example concrete implementation for testing
    public class ReadOnlyBusinessBaseMock : Adaptive.Intelligence.Shared.ReadOnlyBusinessBase
    {
        public string Name { get; }

        public ReadOnlyBusinessBaseMock(string name)
        {
            Name = name;
        }

        protected override bool PerformLoad<IdType>(IdType? id) where IdType : default
        {
            return true;
        }

        protected override Task<bool> PerformLoadAsync<IdType>(IdType? id) where IdType : default
        {
            return Task.FromResult(true);
        }
    }

    public class ReadOnlyBusinessBaseTests
    {
        [Fact]
        public void Constructor_SetsProperty()
        {
            var obj = new ReadOnlyBusinessBaseMock("TestName");
            Assert.Equal("TestName", obj.Name);
        }
    }
}