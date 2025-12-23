using System;
using Adaptive.Intelligence.Shared;
using Xunit;

namespace Adaptive.Intelligence.Shared.Test.Utility
{
    public class ByteArrayUtilTests
    {
        [Fact]
        public void Clear_SetsAllElementsToDefault()
        {
            var arr = new byte[] { 1, 2, 3 };
            ByteArrayUtil.Clear(arr);
            Assert.All(arr, b => Assert.Equal(0, b));
        }

        [Fact]
        public void Clear_NullArray_DoesNotThrow()
        {
            byte[]? arr = null;
            ByteArrayUtil.Clear(arr);
            Assert.Null(arr);
        }

        [Fact]
        public void CreatePinnedArray_ReturnsArrayOfCorrectLength()
        {
            var arr = ByteArrayUtil.CreatePinnedArray(5);
            Assert.Equal(5, arr.Length);
        }

        [Fact]
        public void CreateRandomArray_ReturnsArrayOfCorrectLength_AndNotAllZero()
        {
            var arr = ByteArrayUtil.CreateRandomArray(10);
            Assert.Equal(10, arr.Length);
            // Not all bytes should be zero (very unlikely)
            Assert.Contains(arr, b => b != 0);
        }

        [Fact]
        public void ConcatenateArrays_CombinesArraysCorrectly()
        {
            var a = new byte[] { 1, 2 };
            var b = new byte[] { 3, 4, 5 };
            var result = ByteArrayUtil.ConcatenateArrays(a, b);
            Assert.Equal(new byte[] { 1, 2, 3, 4, 5 }, result);
        }

        [Fact]
        public void CopyToNewArray_ReturnsCopyWithSameValues()
        {
            var arr = new[] { 1, 2, 3 };
            var copy = ByteArrayUtil.CopyToNewArray(arr);
            Assert.NotSame(arr, copy);
            Assert.Equal(arr, copy);
        }

        [Fact]
        public void CopyToNewArray_Null_ReturnsNull()
        {
            int[]? arr = null;
            var copy = ByteArrayUtil.CopyToNewArray(arr);
            Assert.Null(copy);
        }

        [Theory]
        [InlineData(null, true)]
        [InlineData(new byte[0], true)]
        [InlineData(new byte[] { 1 }, false)]
        public void IsNullOrEmpty_WorksAsExpected(byte[]? arr, bool expected)
        {
            Assert.Equal(expected, ByteArrayUtil.IsNullOrEmpty(arr));
        }
    }
}