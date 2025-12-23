using Adaptive.Intelligence.Shared;
using Xunit;

namespace Adaptive.Intelligence.Shared.Test.Utility
{
    public class IdGeneratorTests
    {
        [Fact]
        public void Next_IncrementsId()
        {
            // Arrange
            IdGenerator.SetLastId(2000);
            int first = IdGenerator.Next();
            int second = IdGenerator.Next();

            // Assert
            Assert.Equal(2001, first);
            Assert.Equal(2002, second);
        }

        [Fact]
        public void GetLastId_ReturnsCurrentId()
        {
            IdGenerator.SetLastId(3000);
            Assert.Equal(3000, IdGenerator.GetLastId());
            IdGenerator.Next();
            Assert.Equal(3001, IdGenerator.GetLastId());
        }

        [Fact]
        public void SetLastId_ResetsCounter()
        {
            IdGenerator.SetLastId(4000);
            Assert.Equal(4000, IdGenerator.GetLastId());
            Assert.Equal(4001, IdGenerator.Next());
        }

        [Fact]
        public void Next_IsThreadSafe()
        {
            IdGenerator.SetLastId(5000);
            int[] results = new int[100];
            Parallel.For(0, 100, i =>
            {
                results[i] = IdGenerator.Next();
            });
            Assert.Equal(100, results.Distinct().Count());
            Assert.Equal(5100, IdGenerator.GetLastId());
        }
    }
}