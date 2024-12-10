using Adaptive.Intelligence.Shared.IO;
using NSubstitute.Routing.Handlers;

namespace Adaptive.Intelligence.Shared.Test.IO
{
	public class SafeBinaryReaderTests
	{
        [Fact]
        public void CloseTest()
        {
            // Arrange
            MemoryStream ms = CreateStream<string>("X", "Y", "Z");
            SafeBinaryReader reader = new SafeBinaryReader(ms);

            // Act.
            reader.Close();
            reader.Dispose();
            reader.Close();
            reader.Dispose();

            ms.Dispose();
        }

        [Fact]
        public void ConstructorReaderTest()
        {
            MemoryStream ms = new MemoryStream();
            BinaryReader binaryReader = new BinaryReader(ms);
            SafeBinaryReader reader = new SafeBinaryReader(binaryReader);

            Assert.NotNull(reader);
            Assert.NotNull(reader.BaseStream);
            Assert.True(reader.CanRead);
            Assert.Empty(reader.ExceptionMessages);
            Assert.Empty(reader.Exceptions);
            Assert.False(reader.HasExceptions);
            Assert.Null(reader.FirstException);

            Assert.NotNull(reader.Reader);
            Assert.Equal(binaryReader, reader.Reader);
            reader.Dispose();


            Assert.NotNull(binaryReader);
            binaryReader.Dispose();
            ms.Dispose();
        }

        [Fact]
        public void ConstructorTest()
		{
			MemoryStream ms = new MemoryStream();
			SafeBinaryReader reader = new SafeBinaryReader(ms);

			Assert.NotNull(reader);
			Assert.NotNull(reader.BaseStream);
			Assert.True(reader.CanRead);
			Assert.Empty(reader.ExceptionMessages);
			Assert.Empty(reader.Exceptions);
			Assert.False(reader.HasExceptions);
			Assert.Null(reader.FirstException);
			Assert.NotNull(reader.Reader);

			reader.Dispose();
			ms.Dispose();
		}
		[Fact]
		public void DisposeTest()
		{
			// Arrange
			MemoryStream ms = CreateStream<string>("X", "Y", "Z");
			SafeBinaryReader reader = new SafeBinaryReader(ms);

			// Act.
			reader.Dispose();
			reader.Dispose();
			reader.Dispose();
			reader.Dispose();

			ms.Dispose();
		}
        [Fact]
        public async Task DisposeTestAsync()
        {
            // Arrange
            MemoryStream ms = CreateStream<string>("X", "Y", "Z");
            SafeBinaryReader reader = new SafeBinaryReader(ms);

            // Act.
            await reader.DisposeAsync();
            await reader.DisposeAsync();
            await reader.DisposeAsync();
            await reader.DisposeAsync();

            ms.Dispose();
        }

        [Fact]
        public void ReadBadIndexTest()
        {
            // Arrange.
            MemoryStream ms = new MemoryStream();
            ms.Write(MakeBuffer(0, 256));
            ms.Write(MakeBuffer(2, 256));
            ms.Write(MakeBuffer(16, 256));
            ms.Write(MakeBuffer(255, 256));
            ms.Flush();
            ms.Seek(0, SeekOrigin.Begin);

            // Act.
            SafeBinaryReader reader = new SafeBinaryReader(ms);
            byte[] buffer = new byte[256];
            byte[] buffer1 = new byte[256];
            byte[] buffer2 = new byte[256];
            byte[] buffer3 = new byte[256];

            // Act.
            reader.Read(buffer, 999, 256);
            reader.Read(buffer1, 0, 0);
            reader.Read(buffer2, -3, 256);
            reader.Read(buffer3, 1024, 256);

            // Assert.
            Assert.NotNull(buffer);
            Assert.NotNull(buffer1);
            Assert.NotNull(buffer2);
            Assert.NotNull(buffer3);

            Assert.Equal(256, buffer.Length);
            Assert.Equal(256, buffer1.Length);
            Assert.Equal(256, buffer2.Length);
            Assert.Equal(256, buffer3.Length);

            Assert.True(ContainsValues(0, buffer));
            Assert.True(ContainsValues(0, buffer1));
            Assert.True(ContainsValues(0, buffer2));
            Assert.True(ContainsValues(0, buffer3));

            // Clean up.
            reader.Dispose();
            ms.Dispose();
        }

        [Fact]
        public void ReadBooleanTest()
        {
            // Arrange
            MemoryStream ms = CreateStream<bool>(true, false, true);

            // Assert
            SafeBinaryReader reader = new SafeBinaryReader(ms);
            Assert.True(reader.ReadBoolean());
            Assert.False(reader.ReadBoolean());
            Assert.True(reader.ReadBoolean());

            // Clean  up.
            reader.Dispose();
            ms.Dispose();
        }

        [Fact]
        public void ReadByteArrayTest()
        {
            // Arrange.
            byte[] data = new byte[126];
            for (int count = 0; count < 126; count++)
                data[count] = (byte)count;
            MemoryStream ms = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(ms);
            writer.Write(data.Length);
            writer.Write(data);
            writer.Flush();
            ms.Position = 0;

            // Act.
            SafeBinaryReader reader = new SafeBinaryReader(ms);
            byte[]? newData = reader.ReadByteArray();
            Assert.NotNull(newData);
            Assert.Equal(126, newData.Length);
            for (int count = 0; count < 126; count++)
            {
                Assert.Equal((byte)count, newData[count]);
            }

            // Clean up.
            reader.Dispose();
            ms.Dispose();
        }
        [Fact]
        public void ReadNullableByteArrayNotNullTest()
        {
            // Arrange.
            byte[] data = new byte[126];
            for (int count = 0; count < 126; count++)
                data[count] = (byte)count;
            MemoryStream ms = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(ms);
            writer.Write(false);
            writer.Write(data.Length);
            writer.Write(data);
            writer.Flush();
            ms.Position = 0;

            // Act.
            SafeBinaryReader reader = new SafeBinaryReader(ms);
            byte[]? newData = reader.ReadNullableByteArray();
            Assert.NotNull(newData);
            Assert.Equal(126, newData.Length);
            for (int count = 0; count < 126; count++)
            {
                Assert.Equal((byte)count, newData[count]);
            }

            // Clean up.
            reader.Dispose();
            ms.Dispose();
        }
        [Fact]
        public void ReadNullableByteArrayIsNullTest()
        {
            // Arrange.
            MemoryStream ms = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(ms);
            writer.Write(true);
            writer.Flush();
            ms.Position = 0;

            // Act.
            SafeBinaryReader reader = new SafeBinaryReader(ms);
            byte[]? newData = reader.ReadNullableByteArray();
            Assert.Null(newData);

            // Clean up.
            reader.Dispose();
            ms.Dispose();
        }

        [Fact]
        public void ReadByteTest()
        {
            // Arrange
            MemoryStream ms = CreateStream<byte>(23, 42, 255);

            // Assert
            SafeBinaryReader reader = new SafeBinaryReader(ms);
            Assert.Equal((byte)23, reader.ReadByte());
            Assert.Equal((byte)42, reader.ReadByte());
            Assert.Equal((byte)255, reader.ReadByte());

            // Clean  up.
            reader.Dispose();
            ms.Dispose();
        }

        [Fact]
        public void ReadDecimalTest()
        {
            // Arrange
            MemoryStream ms = CreateStream<decimal>(23.45m, 42.67m, -16.89m);

            // Assert
            SafeBinaryReader reader = new SafeBinaryReader(ms);
            Assert.Equal(23.45m, reader.ReadDecimal());
            Assert.Equal(42.67m, reader.ReadDecimal());
            Assert.Equal(-16.89m, reader.ReadDecimal());

            // Clean up.
            reader.Dispose();
            ms.Dispose();
        }

        [Fact]
        public void ReadDoubleTest()
        {
            // Arrange
            MemoryStream ms = CreateStream<double>(23.45, 42.67, -16.89);

            // Assert
            SafeBinaryReader reader = new SafeBinaryReader(ms);
            Assert.Equal(23.45, reader.ReadDouble());
            Assert.Equal(42.67, reader.ReadDouble());
            Assert.Equal(-16.89, reader.ReadDouble());

            // Clean up.
            reader.Dispose();
            ms.Dispose();
        }

        [Fact]
        public void ReadInt16Test()
        {
            // Arrange
            MemoryStream ms = CreateStream<short>(23, -42, 16);

            // Assert
            SafeBinaryReader reader = new SafeBinaryReader(ms);
            Assert.Equal((short)23, reader.ReadInt16());
            Assert.Equal((short)-42, reader.ReadInt16());
            Assert.Equal((short)16, reader.ReadInt16());

            // Clean  up.
            reader.Dispose();
            ms.Dispose();
        }

        [Fact]
        public void ReadInt32Test()
        {
            // Arrange
            MemoryStream ms = CreateStream<int>(23, 42, -16);

            // Assert
            SafeBinaryReader reader = new SafeBinaryReader(ms);
            Assert.Equal(23, reader.ReadInt32());
            Assert.Equal(42, reader.ReadInt32());
            Assert.Equal(-16, reader.ReadInt32());

            // Clean  up.
            reader.Dispose();
            ms.Dispose();
        }

        [Fact]
        public void ReadInt64Test()
        {
            // Arrange
            MemoryStream ms = CreateStream<long>(23, 42, -16);

            // Assert
            SafeBinaryReader reader = new SafeBinaryReader(ms);
            Assert.Equal(23, reader.ReadInt64());
            Assert.Equal(42, reader.ReadInt64());
            Assert.Equal(-16, reader.ReadInt64());

            // Clean  up.
            reader.Dispose();
            ms.Dispose();
        }

        [Fact]
        public void ReadSByteTest()
        {
            // Arrange
            MemoryStream ms = CreateStream<sbyte>(23, 42, -126);

            // Assert
            SafeBinaryReader reader = new SafeBinaryReader(ms);
            Assert.Equal((sbyte)23, reader.ReadSByte());
            Assert.Equal((sbyte)42, reader.ReadSByte());
            Assert.Equal((sbyte)-126, reader.ReadSByte());

            // Clean  up.
            reader.Dispose();
            ms.Dispose();
        }

        [Fact]
        public void ReadSingleTest()
        {
            // Arrange
            MemoryStream ms = CreateStream<float>(23.45f, 42.67f, -16.89f);

            // Assert
            SafeBinaryReader reader = new SafeBinaryReader(ms);
            Assert.Equal(23.45f, reader.ReadSingle());
            Assert.Equal(42.67f, reader.ReadSingle());
            Assert.Equal(-16.89f, reader.ReadSingle());

            // Clean up.
            reader.Dispose();
            ms.Dispose();
        }

        [Fact]
        public void ReadStringTest()
        {
            // Arrange
            MemoryStream ms = CreateStream<string>(
                "Hello World 1",
                "Hello World2",
                string.Empty);

            // Assert
            SafeBinaryReader reader = new SafeBinaryReader(ms);
            Assert.Equal("Hello World 1", reader.ReadString());
            Assert.Equal("Hello World2", reader.ReadString());
            Assert.Equal(string.Empty, reader.ReadString());

            // Clean  up.
            reader.Dispose();
            ms.Dispose();
        }

        [Fact]
        public void ReadTest()
		{
			// Arrange.
			MemoryStream ms = new MemoryStream();
			ms.Write(MakeBuffer(0, 256));
			ms.Write(MakeBuffer(2, 256));
			ms.Write(MakeBuffer(16, 256));
			ms.Write(MakeBuffer(255, 256));
			ms.Flush();
			ms.Seek(0, SeekOrigin.Begin);

			// Act.
			SafeBinaryReader reader = new SafeBinaryReader(ms);
			byte[] buffer = new byte[256];
			byte[] buffer1 = new byte[256];
			byte[] buffer2 = new byte[256];
			byte[] buffer3 = new byte[256];

			// Act.
			reader.Read(buffer, 0, 256);
			reader.Read(buffer1, 0, 256);
			reader.Read(buffer2, 0, 256);
			reader.Read(buffer3, 0, 256);

			// Assert.
			Assert.NotNull(buffer);
			Assert.NotNull(buffer1);
			Assert.NotNull(buffer2);
			Assert.NotNull(buffer3);

			Assert.Equal(256, buffer.Length);
			Assert.Equal(256, buffer1.Length);
			Assert.Equal(256, buffer2.Length);
			Assert.Equal(256, buffer3.Length);

			Assert.True(ContainsValues(0, buffer));
			Assert.True(ContainsValues(2, buffer1));
			Assert.True(ContainsValues(16, buffer2));
			Assert.True(ContainsValues(255, buffer3));

			// Clean up.
			reader.Dispose();
			ms.Dispose();
		}
        [Fact]
        public void ReadUInt16Test()
        {
            // Arrange
            MemoryStream ms = CreateStream<ushort>(23, 42, 65535);

            // Assert
            SafeBinaryReader reader = new SafeBinaryReader(ms);
            Assert.Equal((ushort)23, reader.ReadUInt16());
            Assert.Equal((ushort)42, reader.ReadUInt16());
            Assert.Equal((ushort)65535, reader.ReadUInt16());

            // Clean up.
            reader.Dispose();
            ms.Dispose();
        }

        [Fact]
        public void ReadUInt32Test()
        {
            // Arrange
            MemoryStream ms = CreateStream<uint>(23, 42, 4294967295);

            // Assert
            SafeBinaryReader reader = new SafeBinaryReader(ms);
            Assert.Equal((uint)23, reader.ReadUInt32());
            Assert.Equal((uint)42, reader.ReadUInt32());
            Assert.Equal((uint)4294967295, reader.ReadUInt32());

            // Clean up.
            reader.Dispose();
            ms.Dispose();
        }

        [Fact]
        public void ReadUInt64Test()
        {
            // Arrange
            MemoryStream ms = CreateStream<ulong>(23, 42, 18446744073709551615);

            // Assert
            SafeBinaryReader reader = new SafeBinaryReader(ms);
            Assert.Equal((ulong)23, reader.ReadUInt64());
            Assert.Equal((ulong)42, reader.ReadUInt64());
            Assert.Equal((ulong)18446744073709551615, reader.ReadUInt64());

            // Clean up.
            reader.Dispose();
            ms.Dispose();
        }

        [Fact]
        public void SeekTest()
		{
			// Arrange
			MemoryStream ms = CreateStream<string>("X", "Y", "Z");
			SafeBinaryReader reader = new SafeBinaryReader(ms);

			// Act.
			reader.Seek(0, SeekOrigin.Begin);
			reader.Seek(3, SeekOrigin.Begin);
			reader.Seek(0, SeekOrigin.End);
			ms.Close();
			reader.Seek(90920, SeekOrigin.End);

			Assert.NotNull(reader.FirstException);
			Assert.True(reader.HasExceptions);

			// Clean up.
			reader.Dispose();
			ms.Dispose();
		}
        private static bool ContainsValues(byte value, byte[] buffer)
        {
            bool isGood = true;
            for (int x = 0; x < buffer.Length; x++)
            {
                if (buffer[x] != value)
                {
                    isGood = false;
                    break;
                }
            }
            return isGood;

        }

        private static MemoryStream CreateStream<T>(T data1, T data2, T data3)
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(ms);

            Write<T>(writer, data1);
            Write<T>(writer, data2);
            Write<T>(writer, data3);
            writer.Flush();
            writer = null;

            ms.Seek(0, SeekOrigin.Begin);

            return ms;
        }

        private static byte[] MakeBuffer(byte value, int length)
        {
            byte[] buffer = new byte[length];
            for (int x = 0; x < length; x++)
            {
                buffer[x] = value;
            }
            return buffer;
        }

        private static void Write<T>(BinaryWriter writer, T data)
        {
			switch (data)
			{
				case bool bdata:
					writer.Write(bdata);
					break;
				case byte bdata:
					writer.Write(bdata);
					break;
				case int bdata:
					writer.Write(bdata);
					break;
				case short bdata:
					writer.Write(bdata);
					break;
				case long bdata:
					writer.Write(bdata);
					break;
				case ushort bdata:
					writer.Write(bdata);
					break;
				case uint bdata:
					writer.Write(bdata);
					break;
				case ulong bdata:
					writer.Write(bdata);
					break;
				case string bdata:
					writer.Write(bdata);
					break;

				case Half bdata:
					writer.Write(bdata);
					break;

				case sbyte bdata:
					writer.Write(bdata);
					break;

                case decimal bdata:
                    writer.Write(bdata);
                    break;

                case double bdata:
                    writer.Write(bdata);
                    break;

                case float bdata:
                    writer.Write(bdata);
                    break;

                default:
					throw new Exception("X");
			}
		}
	}
}

