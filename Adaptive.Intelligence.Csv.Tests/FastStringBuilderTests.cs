using Adaptive.Intelligence.Unsafe;

namespace Adaptive.Intelligence.Csv.Tests
{
    public class FastStringBuilderTests : IDisposable
    {
        private FastStringBuilder _builder;

        public FastStringBuilderTests()
        {
            _builder = new FastStringBuilder(100);
        }

        public void Dispose()
        {
            _builder.Dispose();
        }

        [Fact]
        public void Constructor_InitializesLengthZero()
        {
            Assert.Equal(0, _builder.Length);
        }

        [Fact]
        public void Append_Char_IncreasesLength()
        {
            _builder.Append('A');
            Assert.Equal(1, _builder.Length);
            Assert.Equal("A", _builder.ToString());
        }

        [Fact]
        public void Append_String_IncreasesLength()
        {
            _builder.Append("Hello");
            Assert.Equal(5, _builder.Length);
            Assert.Equal("Hello", _builder.ToString());
        }

        [Fact]
        public void AppendLine_AppendsNewLine()
        {
            _builder.AppendLine();
            Assert.Equal(Environment.NewLine.Length, _builder.Length);
            Assert.Equal(Environment.NewLine, _builder.ToString());
        }

        [Fact]
        public void AppendLine_String_AppendsStringAndNewLine()
        {
            _builder.AppendLine("Test");
            Assert.Equal("Test" + Environment.NewLine, _builder.ToString());
        }

        [Fact]
        public void Clear_ResetsLengthAndContent()
        {
            _builder.Append("Hello");
            _builder.Clear();
            Assert.Equal(0, _builder.Length);
            Assert.Equal(string.Empty, _builder.ToString());
        }

        [Fact]
        public void ToString_ReturnsCorrectContent()
        {
            _builder.Append('A');
            _builder.Append('B');
            _builder.Append('C');
            Assert.Equal("ABC", _builder.ToString());
        }

        [Fact]
        public void Append_EmptyString_DoesNotChangeLength()
        {
            _builder.Append("");
            Assert.Equal(0, _builder.Length);
            Assert.Equal(string.Empty, _builder.ToString());
        }

        [Fact]
        public void Append_ExceedingBuffer_DoesNotCrash()
        {
            var longString = new string('X', 200); // Exceeds buffer
            _builder.Append(longString);
            Assert.True(_builder.Length < 200);
        }
    }
}