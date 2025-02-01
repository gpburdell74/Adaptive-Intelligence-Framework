using Adaptive.Intelligence.Shared.IO;
using AutoFixture;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adaptive.Intelligence.Shared.Test.IO
{
    public class SafeBinaryWriterTests
    {
        [Fact]
        public void CreateTest()
        {
            MemoryStream ms = new MemoryStream();
            SafeBinaryWriter writer = new SafeBinaryWriter(ms);

            Stream? streamRef = writer.BaseStream;
            Assert.NotNull(streamRef);
            Assert.IsType<MemoryStream>(streamRef);

            BinaryWriter? rawWriter = writer.Writer;
            Assert.NotNull(rawWriter);

            Assert.True(writer.CanWrite);
            ms.Close();
            ms.Dispose();
            Assert.False(writer.CanWrite);
            writer.Dispose();
        }

        [Fact]
        public void CloseTest()
        {
            MemoryStream ms = new MemoryStream();
            SafeBinaryWriter writer = new SafeBinaryWriter(ms);

            ms.Close();
            ms.Dispose();
            ms = null;
            GC.Collect();

            writer.Close();
            writer.Close();
            writer.Dispose();
            writer.Close();
            writer.Close();
            writer.Close();
            writer.Dispose();

        }
        [Fact]
        public async Task DisposeWithLocalWriterTestAsync()
        {
            await Task.Yield();
            MemoryStream ms = new MemoryStream();
            SafeBinaryWriter writer = new SafeBinaryWriter(ms);

            await writer.DisposeAsync();
            await ms.DisposeAsync();

        }
        [Fact]
        public async Task DisposeTestAsync()
        {
            await Task.Yield();
            MemoryStream ms = new MemoryStream();
            BinaryWriter origWriter = new BinaryWriter(ms);
            SafeBinaryWriter writer = new SafeBinaryWriter(origWriter);

            await writer.DisposeAsync();
            origWriter.Dispose();
            await ms.DisposeAsync();

        }
        [Fact]
        public void FromWriterTest()
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter origWriter = new BinaryWriter(ms);
            SafeBinaryWriter writer = new SafeBinaryWriter(origWriter);

            writer.Dispose();
            origWriter.Dispose();
            ms.Dispose();

        }
        [Fact]
        public void WriteBooleanTest()
        {
            MemoryStream ms = new MemoryStream();
            SafeBinaryWriter writer = new SafeBinaryWriter(ms);

            writer.Write(true);
            writer.Write(false);
            writer.Flush();

            ms.Position = 0;
            SafeBinaryReader reader = new SafeBinaryReader(ms);
            bool value = reader.ReadBoolean();
            Assert.True(value);
            value = reader.ReadBoolean();
            Assert.False(value);

            writer.Dispose();
            ms.Dispose();
            writer.Dispose();
        }
        [Fact]
        public void WriteByteTest()
        {
            MemoryStream ms = new MemoryStream();
            SafeBinaryWriter writer = new SafeBinaryWriter(ms);

            for (int count = 0; count < 256; count++)
            {
                writer.Write((byte)count);
            }
            writer.Flush();

            ms.Position = 0;
            SafeBinaryReader reader = new SafeBinaryReader(ms);

            for (int count = 0; count < 256; count++)
            {
                byte data = reader.ReadByte();
                Assert.Equal((byte)count, data);
            }

            writer.Dispose();
            ms.Dispose();
            writer.Dispose();
        }
        [Fact]
        public void WriteSByteTest()
        {
            MemoryStream ms = new MemoryStream();
            SafeBinaryWriter writer = new SafeBinaryWriter(ms);

            for (int count = -127; count < 128; count++)
            {
                writer.Write((sbyte)count);
            }
            writer.Flush();

            ms.Position = 0;
            SafeBinaryReader reader = new SafeBinaryReader(ms);

            for (int count = -127; count < 128; count++)
            {
                sbyte data = reader.ReadSByte();
                Assert.Equal((sbyte)count, data);
            }

            writer.Dispose();
            ms.Dispose();
            writer.Dispose();

        }
        [Fact]
        public void WriteByteArrayTest()
        {
            MemoryStream ms = new MemoryStream();
            SafeBinaryWriter writer = new SafeBinaryWriter(ms);

            byte[] data = new byte[256];
            for (int count = 0; count < 256; count++)
                data[count] = (byte)count;

            writer.Write(data);
            writer.Flush();

            ms.Position = 0;
            SafeBinaryReader reader = new SafeBinaryReader(ms);
            byte[]? result = reader.ReadBytes(256);
            Assert.NotNull(result);
            Assert.Equal(result.Length, data.Length);

            for (int count = 0; count < 256; count++)
            {
                Assert.Equal(data[count], result[count]);
            }

            reader.Dispose();
            writer.Dispose();
            ms.Dispose();
        }
        [Fact]
        public void WRitePartialByteArrayTest()
        {
            MemoryStream ms = new MemoryStream();
            SafeBinaryWriter writer = new SafeBinaryWriter(ms);

            byte[] data = new byte[256];
            for (int count = 0; count < 256; count++)
                data[count] = (byte)count;

            writer.Write(data, 100, 50);
            writer.Flush();
            writer.Seek(0, SeekOrigin.Begin);

            ms.Position = 0;
            SafeBinaryReader reader = new SafeBinaryReader(ms);
            byte[]? result = reader.ReadBytes(50);
            Assert.NotNull(result);
            Assert.Equal(50, result.Length);

            for (int count = 0; count < 50; count++)
            {
                Assert.Equal(data[100 + count], result[count]);
            }

            reader.Dispose();
            writer.Close();
            writer.Dispose();
            ms.Dispose();
        }
        [Fact]
        public void WriteCharTest()
        {
            MemoryStream ms = new MemoryStream();
            SafeBinaryWriter writer = new SafeBinaryWriter(ms);

            for (int count = 0; count < 256; count++)
            {
                writer.Write((char)count);
            }
            writer.Flush();

            ms.Position = 0;
            SafeBinaryReader reader = new SafeBinaryReader(ms);

            for (int count = 0; count < 256; count++)
            {
                char data = reader.ReadChar();
                Assert.Equal((char)count, data);
            }

            writer.Dispose();
            ms.Dispose();
            writer.Dispose();
        }
        [Fact]
        public void WriteCharArrayTest()
        {
            MemoryStream ms = new MemoryStream();
            SafeBinaryWriter writer = new SafeBinaryWriter(ms);

            char[] data = new char[256];
            for (int count = 0; count < 256; count++)
                data[count] = (char)count;

            writer.Write(data);
            writer.Flush();

            ms.Position = 0;
            SafeBinaryReader reader = new SafeBinaryReader(ms);
            char[]? result = reader.ReadCharArray(256);
            Assert.NotNull(result);
            Assert.Equal(result.Length, data.Length);

            for (int count = 0; count < 256; count++)
            {
                Assert.Equal(data[count], result[count]);
            }

            reader.Dispose();
            writer.Dispose();
            ms.Dispose();
        }
        [Fact]
        public void WritePartialCharArrayTest()
        {
            MemoryStream ms = new MemoryStream();
            SafeBinaryWriter writer = new SafeBinaryWriter(ms);

            char[] data = new char[256];
            for (int count = 0; count < 256; count++)
                data[count] = (char)count;

            writer.Write(data, 100, 50);
            writer.Flush();
            writer.Seek(0, SeekOrigin.Begin);

            ms.Position = 0;
            SafeBinaryReader reader = new SafeBinaryReader(ms);
            char[]? result = reader.ReadCharArray(50);
            Assert.NotNull(result);
            Assert.Equal(50, result.Length);

            for (int count = 0; count < 50; count++)
            {
                Assert.Equal(data[100 + count], result[count]);
            }

            reader.Dispose();
            writer.Close();
            writer.Dispose();
            ms.Dispose();
        }

        [Fact]
        public void WriteDoubleTest()
        {
            MemoryStream ms = new MemoryStream();
            SafeBinaryWriter writer = new SafeBinaryWriter(ms);

            writer.Write((double)0);
            writer.Write(double.MaxValue);
            writer.Write(double.MinValue);

            writer.Flush();
            ms.Position = 0;
            SafeBinaryReader reader = new SafeBinaryReader(ms);

            double a = reader.ReadDouble();
            double b = reader.ReadDouble();
            double c = reader.ReadDouble();

            Assert.Equal((double)0, a);
            Assert.Equal(double.MaxValue, b);
            Assert.Equal(double.MinValue, c);

            writer.Dispose();
            ms.Dispose();
            writer.Dispose();
        }

        [Fact]
        public void WriteDateTimeTest()
        {
            DateTime dateTime = DateTime.Now;

            MemoryStream ms = new MemoryStream();
            SafeBinaryWriter writer = new SafeBinaryWriter(ms);

            writer.Write(dateTime);
            writer.Write(dateTime.AddYears(25));
            writer.Write(DateTime.MinValue);

            writer.Flush();
            ms.Position = 0;
            SafeBinaryReader reader = new SafeBinaryReader(ms);

            DateTime a = reader.ReadDateTime();
            DateTime b = reader.ReadDateTime();
            DateTime c = reader.ReadDateTime();

            Assert.Equal(dateTime, a);
            Assert.Equal(dateTime.AddYears(25), b);
            Assert.Equal(DateTime.MinValue, c);

            writer.Dispose();
            ms.Dispose();
            writer.Dispose();

        }

        [Fact]
        public void WriteDecimalTest()
        {
            MemoryStream ms = new MemoryStream();
            SafeBinaryWriter writer = new SafeBinaryWriter(ms);

            writer.Write((decimal)0);
            writer.Write(decimal.MaxValue);
            writer.Write(decimal.MinValue);

            writer.Flush();
            ms.Position = 0;
            SafeBinaryReader reader = new SafeBinaryReader(ms);

            decimal a = reader.ReadDecimal();
            decimal b = reader.ReadDecimal();
            decimal c = reader.ReadDecimal();

            Assert.Equal((decimal)0, a);
            Assert.Equal(decimal.MaxValue, b);
            Assert.Equal(decimal.MinValue, c);

            writer.Dispose();
            ms.Dispose();
            writer.Dispose();
        }

        [Fact]
        public void WriteInt16Test()
        {
            MemoryStream ms = new MemoryStream();
            SafeBinaryWriter writer = new SafeBinaryWriter(ms);

            writer.Write((short)0);
            writer.Write(short.MaxValue);
            writer.Write(short.MinValue);

            writer.Flush();
            ms.Position = 0;
            SafeBinaryReader reader = new SafeBinaryReader(ms);

            short a = reader.ReadInt16();
            short b = reader.ReadInt16();
            short c = reader.ReadInt16();

            Assert.Equal((short)0, a);
            Assert.Equal(short.MaxValue, b);
            Assert.Equal(short.MinValue, c);

            writer.Dispose();
            ms.Dispose();
            writer.Dispose();
        }

        [Fact]
        public void WriteUInt16Test()
        {
            MemoryStream ms = new MemoryStream();
            SafeBinaryWriter writer = new SafeBinaryWriter(ms);

            writer.Write((ushort)0);
            writer.Write(ushort.MaxValue);
            writer.Write(ushort.MinValue);

            writer.Flush();
            ms.Position = 0;
            SafeBinaryReader reader = new SafeBinaryReader(ms);

            ushort a = reader.ReadUInt16();
            ushort b = reader.ReadUInt16();
            ushort c = reader.ReadUInt16();

            Assert.Equal((ushort)0, a);
            Assert.Equal(ushort.MaxValue, b);
            Assert.Equal(ushort.MinValue, c);

            writer.Dispose();
            ms.Dispose();
            writer.Dispose();
        }

        [Fact]
        public void WriteInt32Test()
        {
            MemoryStream ms = new MemoryStream();
            SafeBinaryWriter writer = new SafeBinaryWriter(ms);

            writer.Write((int)0);
            writer.Write(int.MaxValue);
            writer.Write(int.MinValue);

            writer.Flush();
            ms.Position = 0;
            SafeBinaryReader reader = new SafeBinaryReader(ms);

            int a = reader.ReadInt32();
            int b = reader.ReadInt32();
            int c = reader.ReadInt32();

            Assert.Equal((int)0, a);
            Assert.Equal(int.MaxValue, b);
            Assert.Equal(int.MinValue, c);

            writer.Dispose();
            ms.Dispose();
            writer.Dispose();
        }

        [Fact]
        public void WriteUInt32Test()
        {
            MemoryStream ms = new MemoryStream();
            SafeBinaryWriter writer = new SafeBinaryWriter(ms);

            writer.Write((uint)0);
            writer.Write(uint.MaxValue);
            writer.Write(uint.MinValue);

            writer.Flush();
            ms.Position = 0;
            SafeBinaryReader reader = new SafeBinaryReader(ms);

            uint a = reader.ReadUInt32();
            uint b = reader.ReadUInt32();
            uint c = reader.ReadUInt32();

            Assert.Equal((uint)0, a);
            Assert.Equal(uint.MaxValue, b);
            Assert.Equal(uint.MinValue, c);

            writer.Dispose();
            ms.Dispose();
            writer.Dispose();
        }

        [Fact]
        public void WriteInt64Test()
        {
            MemoryStream ms = new MemoryStream();
            SafeBinaryWriter writer = new SafeBinaryWriter(ms);

            writer.Write((long)0);
            writer.Write(long.MaxValue);
            writer.Write(long.MinValue);

            writer.Flush();
            ms.Position = 0;
            SafeBinaryReader reader = new SafeBinaryReader(ms);

            long a = reader.ReadInt64();
            long b = reader.ReadInt64();
            long c = reader.ReadInt64();

            Assert.Equal((long)0, a);
            Assert.Equal(long.MaxValue, b);
            Assert.Equal(long.MinValue, c);

            writer.Dispose();
            ms.Dispose();
            writer.Dispose();
        }

        [Fact]
        public void WriteUInt64Test()
        {
            MemoryStream ms = new MemoryStream();
            SafeBinaryWriter writer = new SafeBinaryWriter(ms);

            writer.Write((ulong)0);
            writer.Write(ulong.MaxValue);
            writer.Write(ulong.MinValue);

            writer.Flush();
            ms.Position = 0;
            SafeBinaryReader reader = new SafeBinaryReader(ms);

            ulong a = reader.ReadUInt64();
            ulong b = reader.ReadUInt64();
            ulong c = reader.ReadUInt64();

            Assert.Equal((ulong)0, a);
            Assert.Equal(ulong.MaxValue, b);
            Assert.Equal(ulong.MinValue, c);

            writer.Dispose();
            ms.Dispose();
            writer.Dispose();
        }

        [Fact]
        public void WriteFloatTest()
        {
            MemoryStream ms = new MemoryStream();
            SafeBinaryWriter writer = new SafeBinaryWriter(ms);

            writer.Write((float)0);
            writer.Write(float.MaxValue);
            writer.Write(float.MinValue);

            writer.Flush();
            ms.Position = 0;
            SafeBinaryReader reader = new SafeBinaryReader(ms);

            float a = reader.ReadSingle();
            float b = reader.ReadSingle();
            float c = reader.ReadSingle();

            Assert.Equal((float)0, a);
            Assert.Equal(float.MaxValue, b);
            Assert.Equal(float.MinValue, c);

            writer.Dispose();
            ms.Dispose();
            writer.Dispose();
        }

        [Fact]
        public void WriteStringsTest()
        {
            MemoryStream ms = new MemoryStream();
            SafeBinaryWriter writer = new SafeBinaryWriter(ms);

            Fixture fixture = new Fixture();
            List<string> data = new List<string>();
            fixture.AddManyTo(data, 1000);

            for (int count = 0; count < 1000; count++)
                writer.Write(data[count]);
            writer.Flush();

            ms.Position = 0;
            SafeBinaryReader reader = new SafeBinaryReader(ms);

            for (int count = 0; count < 1000; count++)
            {
                string? line = reader.ReadString();
                Assert.Equal(line, data[count]);
            }

            writer.Dispose();
            ms.Dispose();
            writer.Dispose();

        }

        [Fact]
        public void WriteEmptyStringTest()
        {
            MemoryStream ms = new MemoryStream();
            SafeBinaryWriter writer = new SafeBinaryWriter(ms);

            writer.Write(string.Empty);
            writer.Flush();

            ms.Position = 0;
            SafeBinaryReader reader = new SafeBinaryReader(ms);

            string? line = reader.ReadString();
            Assert.Equal(line, string.Empty);

            writer.Dispose();
            reader.Dispose();
            ms.Dispose();
        }

        [Fact]
        public void WriteNullStringTest()
        {
            MemoryStream ms = new MemoryStream();
            SafeBinaryWriter writer = new SafeBinaryWriter(ms);

            writer.Write((string)null);
            writer.Flush();

            ms.Position = 0;
            SafeBinaryReader reader = new SafeBinaryReader(ms);

            string? line = reader.ReadString();
            Assert.Null(line);

            writer.Dispose();
            reader.Dispose();
            ms.Dispose();

        }

        [Fact]
        public void WriteReadOnlySpanCharTest()
        {
            ReadOnlySpan<char> span = new ReadOnlySpan<char>(
                new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K' });

            MemoryStream ms = new MemoryStream();
            SafeBinaryWriter writer = new SafeBinaryWriter(ms);

            writer.Write((ReadOnlySpan<char>)span);
            writer.Flush();

            ms.Position = 0;
            SafeBinaryReader reader = new SafeBinaryReader(ms);

            ReadOnlySpan<char> data = reader.ReadString();
            for (int count = 0; count < data.Length; count++)
            {
                Assert.Equal(span[count], data[count]);
            }

            writer.Dispose();
            reader.Dispose();
            ms.Dispose();

        }

        [Fact]
        public void WriteReadOnlySpanByteTest()
        {
            ReadOnlySpan<byte> span = new ReadOnlySpan<byte>(
                new byte[] { 65, 66, 67, 68, 69, 70, 71, 0, 255, 32 });

            MemoryStream ms = new MemoryStream();
            SafeBinaryWriter writer = new SafeBinaryWriter(ms);

            writer.Write(span);
            writer.Flush();

            ms.Position = 0;
            SafeBinaryReader reader = new SafeBinaryReader(ms);

            ReadOnlySpan<byte> data = reader.ReadBytes(span.Length);
            for (int count = 0; count < data.Length; count++)
            {
                Assert.Equal(span[count], data[count]);
            }

            writer.Dispose();
            reader.Dispose();
            ms.Dispose();

        }
        [Fact]
        public void Write7BitEncodedIntTest()
        {
            int v = 56;

            MemoryStream ms = new MemoryStream();
            SafeBinaryWriter writer = new SafeBinaryWriter(ms);

            writer.Write7BitEncodedInt(v);
            writer.Flush();

            ms.Position = 0;
            SafeBinaryReader reader = new SafeBinaryReader(ms);
            int w = reader.Read7BitEncodedInt32();

            Assert.Equal(v, w);

            reader.Dispose();
            writer.Dispose();
            ms.Dispose();
        }
        [Fact]
        public void Write7BitEncodedInt64Test()
        {
            long v = 22056;

            MemoryStream ms = new MemoryStream();
            SafeBinaryWriter writer = new SafeBinaryWriter(ms);

            writer.Write7BitEncodedInt64(v);
            writer.Flush();

            ms.Position = 0;
            SafeBinaryReader reader = new SafeBinaryReader(ms);
            long w = reader.Read7BitEncodedInt64();

            Assert.Equal(v, w);

            reader.Dispose();
            writer.Dispose();
            ms.Dispose();

        }

        [Fact]
        public void WriteByteExceptionTest()
        {
            MemoryStream ms = new MemoryStream();
            SafeBinaryWriter writer = new SafeBinaryWriter(ms);

            writer.Write((byte)0);
            ms.Close();
            ms.Dispose();
            writer.Write((byte)128);

            Assert.True(writer.HasExceptions);
            Assert.True(writer.Exceptions.Count == 1);

            writer.Dispose();
        }
        [Fact]
        public void WriteBoolExceptionTest()
        {
            MemoryStream ms = new MemoryStream();
            SafeBinaryWriter writer = new SafeBinaryWriter(ms);

            writer.Write(true);
            ms.Close();
            ms.Dispose();
            writer.Write(false);

            Assert.True(writer.HasExceptions);
            Assert.True(writer.Exceptions.Count == 1);

            writer.Dispose();
        }
        [Fact]
        public void WriteShortExceptionTest()
        {
            MemoryStream ms = new MemoryStream();
            SafeBinaryWriter writer = new SafeBinaryWriter(ms);

            writer.Write((short)32);
            ms.Close();
            ms.Dispose();
            writer.Write((short)128);

            Assert.True(writer.HasExceptions);
            Assert.True(writer.Exceptions.Count == 1);

            writer.Dispose();
        }
        [Fact]
        public void WriteInt32ExceptionTest()
        {
            MemoryStream ms = new MemoryStream();
            SafeBinaryWriter writer = new SafeBinaryWriter(ms);

            writer.Write((int)32);
            ms.Close();
            ms.Dispose();
            writer.Write((int)128);

            Assert.True(writer.HasExceptions);
            Assert.True(writer.Exceptions.Count == 1);

            writer.Dispose();
        }
        [Fact]
        public void WriteInt64ExceptionTest()
        {
            MemoryStream ms = new MemoryStream();
            SafeBinaryWriter writer = new SafeBinaryWriter(ms);

            writer.Write((long)32);
            ms.Close();
            ms.Dispose();
            writer.Write((long)128);

            Assert.True(writer.HasExceptions);
            Assert.True(writer.Exceptions.Count == 1);

            writer.Dispose();
        }
        [Fact]
        public void WriteUShortExceptionTest()
        {
            MemoryStream ms = new MemoryStream();
            SafeBinaryWriter writer = new SafeBinaryWriter(ms);

            writer.Write((ushort)32);
            ms.Close();
            ms.Dispose();
            writer.Write((ushort)128);

            Assert.True(writer.HasExceptions);
            Assert.True(writer.Exceptions.Count == 1);

            writer.Dispose();
        }
        [Fact]
        public void WriteUInt32ExceptionTest()
        {
            MemoryStream ms = new MemoryStream();
            SafeBinaryWriter writer = new SafeBinaryWriter(ms);

            writer.Write((uint)32);
            ms.Close();
            ms.Dispose();
            writer.Write((uint)128);

            Assert.True(writer.HasExceptions);
            Assert.True(writer.Exceptions.Count == 1);

            writer.Dispose();
        }
        [Fact]
        public void WriteUInt64ExceptionTest()
        {
            MemoryStream ms = new MemoryStream();
            SafeBinaryWriter writer = new SafeBinaryWriter(ms);

            writer.Write((ulong)32);
            ms.Close();
            ms.Dispose();
            writer.Write((ulong)128);

            Assert.True(writer.HasExceptions);
            Assert.True(writer.Exceptions.Count == 1);

            writer.Dispose();
        }

        [Fact]
        public void WriteManagedByteArrayTest()
        {
            MemoryStream ms = new MemoryStream();
            SafeBinaryWriter writer = new SafeBinaryWriter(ms);

            byte[] data = new byte[256];
            for (int count = 0; count < 256; count++)
                data[count] = (byte)count;

            writer.WriteByteArray(data);
            writer.Flush();

            ms.Position = 0;
            SafeBinaryReader reader = new SafeBinaryReader(ms);
            bool isNull = reader.ReadBoolean();
            Assert.False(isNull);
            int length = reader.ReadInt32();
            Assert.Equal(256, length);

            byte[]? result = reader.ReadBytes(length);
            Assert.NotNull(result);
            Assert.Equal(result.Length, data.Length);

            for (int count = 0; count < 256; count++)
            {
                Assert.Equal(data[count], result[count]);
            }

            reader.Dispose();
            writer.Dispose();
            ms.Dispose();

        }

        [Fact]
        public void WriteManagedByteArrayNullTest()
        {
            MemoryStream ms = new MemoryStream();
            SafeBinaryWriter writer = new SafeBinaryWriter(ms);

            writer.WriteByteArray(null);
            writer.Flush();

            ms.Position = 0;
            SafeBinaryReader reader = new SafeBinaryReader(ms);
            bool isNull = reader.ReadBoolean();
            Assert.True(isNull);

            reader.Dispose();
            writer.Dispose();
            ms.Dispose();

        }

        [Fact]
        public void WriteHalfTest()
        {
            MemoryStream ms = new MemoryStream();
            SafeBinaryWriter writer = new SafeBinaryWriter(ms);

            Half h = (Half)97.22345;

            writer.Write(h);
            writer.Flush();

            ms.Position = 0;
            SafeBinaryReader reader = new SafeBinaryReader(ms);
            Half x = reader.ReadHalf();
            Assert.Equal(h, x);

            reader.Dispose();
            writer.Dispose();
            ms.Dispose();


        }

        [Fact]
        public void WriteHalfExceptionTest()
        {
            MemoryStream ms = new MemoryStream();
            SafeBinaryWriter writer = new SafeBinaryWriter(ms);

            ms.Dispose();
            Half h = (Half)97.22345;
            writer.Write(h);

            Assert.True(writer.HasExceptions);
            Assert.True(writer.Exceptions.Count == 1);


            writer.Dispose();
            ms.Dispose();


        }

        [Fact]
        public void WriteByteArrayExceptionTest()
        {
            MemoryStream ms = new MemoryStream();
            SafeBinaryWriter writer = new SafeBinaryWriter(ms);

            ms.Dispose();
            byte[] data = new byte[1023];
            writer.Write(data);

            Assert.True(writer.HasExceptions);
            Assert.True(writer.Exceptions.Count == 1);

            writer.Dispose();
            ms.Dispose();

        }
    }
}
