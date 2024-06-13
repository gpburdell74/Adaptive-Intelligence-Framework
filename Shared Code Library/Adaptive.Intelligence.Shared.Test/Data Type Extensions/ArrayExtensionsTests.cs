namespace Adaptive.Intelligence.Shared.Tests.Extensions
{
	/// <summary>
	/// Unit Tests for the <see cref="ArrayExtensions"/> class.
	/// </summary>
	public class ArrayExtensionsTests
	{
		[Fact]
		public void IsNullOrEmptyNullArrayReturnsTrue()
		{
			// Arrange
			byte[] byteArray = null;
			int[] intArray = null;
			string[] stringArray = null;
			bool[] boolArray = null;

			// Act
			bool byteResult = ArrayExtensions.IsNullOrEmpty(byteArray);
			bool intResult = ArrayExtensions.IsNullOrEmpty(intArray);
			bool stringResult = ArrayExtensions.IsNullOrEmpty(stringArray);
			bool boolResult = ArrayExtensions.IsNullOrEmpty(boolArray);

			// Assert
			Assert.True(byteResult);
			Assert.True(intResult);
			Assert.True(stringResult);
			Assert.True(boolResult);
		}
		[Fact]
		public void IsNullOrEmptyEmptyReturnsTrue()
		{
			// Arrange
			byte[] byteArray = new byte[0];
			int[] intArray = new int[0];
			string[] stringArray = new string[0];
			bool[] boolArray = new bool[0];

			// Act
			bool byteResult = ArrayExtensions.IsNullOrEmpty(byteArray);
			bool intResult = ArrayExtensions.IsNullOrEmpty(intArray);
			bool stringResult = ArrayExtensions.IsNullOrEmpty(stringArray);
			bool boolResult = ArrayExtensions.IsNullOrEmpty(boolArray);

			// Assert
			Assert.True(byteResult);
			Assert.True(intResult);
			Assert.True(stringResult);
			Assert.True(boolResult);
		}
		[Fact]
		public void IsNullOrEmptyPopulatedReturnsFalse()
		{
			// Arrange
			byte[] byteArray = new byte[1] { 0 };
			int[] intArray = new int[1] { 32 };
			string[] stringArray = new string[1] { "X" };
			bool[] boolArray = new bool[1] { true };

			// Act
			bool byteResult = ArrayExtensions.IsNullOrEmpty(byteArray);
			bool intResult = ArrayExtensions.IsNullOrEmpty(intArray);
			bool stringResult = ArrayExtensions.IsNullOrEmpty(stringArray);
			bool boolResult = ArrayExtensions.IsNullOrEmpty(boolArray);

			// Assert
			Assert.False(byteResult);
			Assert.False(intResult);
			Assert.False(stringResult);
			Assert.False(boolResult);
		}
		[Fact]
		public void ClearByteArrayTest()
		{
			byte[] data = new byte[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

			ArrayExtensions.ClearArray(data);
			Assert.Equal(10, data.Length);
			foreach (byte b in data)
				Assert.Equal((byte)0, b);

		}
		[Fact]
		public void ClearIntArrayTest()
		{
			int[] data = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

			ArrayExtensions.ClearArray(data);

			Assert.Equal(10, data.Length);
			foreach (int i in data)
				Assert.Equal(0, i);

		}
		[Fact]
		public void ClearStringArrayTest()
		{
			string[] data = new string[10] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };

			ArrayExtensions.ClearArray(data);
			Assert.Equal(10, data.Length);

			foreach (string s in data)
				Assert.Null(s);

		}
		[Fact]
		public void ClearNullByteArrayTest()
		{
			byte[]? data = null;
			ArrayExtensions.ClearArray(data);

		}
		[Fact]
		public void ClearNullIntArrayTest()
		{
			int[]? data = null;
			ArrayExtensions.ClearArray(data);

		}
		[Fact]
		public void ClearNullStringArrayTest()
		{
			string[]? data = null;

			ArrayExtensions.ClearArray(data);

		}

		[Fact]
		public void ByteArrayCompareLengthTest()
		{
			byte[] a = new byte[3] { 1, 2, 3 };
			byte[] b = new byte[4] { 4, 5, 6, 7 };
			byte[] c = new byte[5] { 8, 9, 10, 11, 12 };

			int compResult = ArrayExtensions.Compare(a, b);
			Assert.True(compResult < 0);

			compResult = ArrayExtensions.Compare(a, a);
			Assert.True(compResult == 0);

			compResult = ArrayExtensions.Compare(c, b);
			Assert.True(compResult > 0);

			compResult = ArrayExtensions.Compare(b, c);
			Assert.True(compResult < 0);

		}

		[Fact]
		public void ByteArraysAreNullCompareTest()
		{
			byte[] a = null;
			byte[] b = null;

			int compResult = ArrayExtensions.Compare(a, b);
			Assert.Equal(0, compResult);
		}
		[Fact]
		public void ByteArraysInstanceCompareTest()
		{
			byte[] a = new byte[3];
			byte[] b = null;

			int compResult = ArrayExtensions.Compare(a, b);
			Assert.Equal(1, compResult);

			compResult = ArrayExtensions.Compare(b, a);
			Assert.Equal(-1, compResult);

		}
		[Fact]
		public void ByteArrayDataCompareTest()
		{
			byte[] a = new byte[] { 1, 2, 3 };
			byte[] b = new byte[] { 1, 2, 3 };

			int compResult = ArrayExtensions.Compare(a, b);
			Assert.Equal(0, compResult);

			a = new byte[] { 1, 3, 3 };
			b = new byte[] { 1, 2, 3 };

			compResult = ArrayExtensions.Compare(a, b);
			Assert.Equal(1, compResult);

			compResult = ArrayExtensions.Compare(b, a);
			Assert.Equal(-1, compResult);

		}
	}
}
