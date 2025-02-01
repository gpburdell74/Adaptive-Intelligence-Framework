using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.Shared.IO;
using Adaptive.Intelligence.Shared.Security;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

namespace Adaptive.Taz.Cryptography;

/// <summary>
/// Provides a mechanism for reading encrypted binary data from a stream where the instance tracks its own exceptions.	
/// </summary>
/// <seealso cref="ExceptionTrackingBase" />
/// <seealso cref="ISafeBinaryWriter" />
public sealed class SecureBinaryReader : ExceptionTrackingBase, ISafeBinaryReader
{
    #region Private Member Declarations				
    /// <summary>
    /// The key manager instance.
    /// </summary>
    private KeyManager? _keyManager;
    /// <summary>
    /// The AES cryptography provider.
    /// </summary>
    private AesProvider? _aes;
    /// <summary>
    /// The underlying reader instance.
    /// </summary>
    private BinaryReader? _reader;
    /// <summary>
    /// A flag indicating whether the reader instance's scope is local.
    /// </summary>
    private bool _readerLocal;
    /// <summary>
    /// The reference to the stream to be read from.
    /// </summary>
    private Stream? _sourceStream;
    #endregion

    #region Constructor / Dispose Methods		
    /// <summary>
    /// Initializes a new instance of the <see cref="SecureBinaryWriter"/> class.
    /// </summary>
    /// <param name="manager">
    /// The reference to the <see cref="KeyManager"/> instance used to manage the encryption keys.
    /// </param>
    /// <param name="sourceStream">
    /// The output <see cref="Stream"/> to be read from.
    /// </param>
    public SecureBinaryReader(KeyManager manager, Stream sourceStream)
    {
        _keyManager = manager;
        _aes = manager.CreateAesProvider();
        _sourceStream = sourceStream;
        _reader = new BinaryReader(sourceStream);
        _readerLocal = true;
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="SecureBinaryWriter"/> class.
    /// </summary>
    /// <param name="manager">
    /// The reference to the <see cref="KeyManager"/> instance used to manage the encryption keys.
    /// </param>
    /// <param name="reader">
    /// The <see cref="BinaryReader"/> instance to be used.
    /// </param>
    public SecureBinaryReader(KeyManager manager, BinaryReader reader)
    {
        _keyManager = manager;
        _aes = manager.CreateAesProvider();
        _reader = reader;
        _sourceStream = reader.BaseStream;
        _readerLocal = false;
    }
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        if (!IsDisposed && disposing)
        {
            _aes?.Dispose();
            if (_readerLocal)
                _reader?.Dispose();
        }

        _aes = null;
        _keyManager = null;
        _reader = null;
        _sourceStream = null;
        base.Dispose(disposing);
    }
    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources asynchronously.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous dispose operation.
    /// </returns>

    public async ValueTask DisposeAsync()
    {
        Dispose(true);
    }
    #endregion

    #region Public Properties		
    /// <summary>
    /// Returns the stream associated with the reader.
    /// </summary>
    /// <value>
    /// The underlying <see cref="Stream" /> that is being read from.
    /// </value>
    public Stream? BaseStream => _sourceStream;
    /// <summary>
    /// Gets a value indicating whether this instance can read data.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance can read from an underlying stream; otherwise, <c>false</c>.
    /// </value>
    public bool CanRead
    {
        get
        {
            return (_sourceStream != null && _reader != null && _keyManager != null && _aes != null && _sourceStream.CanRead);
        }
    }
    /// <summary>
    /// Gets the reference to the underlying binary reader instance.
    /// </summary>
    /// <value>
    /// The <see cref="BinaryReader" /> instance being used to do the reading.
    /// </value>
    public BinaryReader? Reader => _reader;
    #endregion

    #region Public Methods / Functions		
    /// <summary>
    /// Closes this reader and releases any system resources associated with the
    /// reader. Following a call to Close, any operations on the reader
    /// may raise exceptions.
    /// </summary>
    public void Close()
    {
        try
        {
            _aes?.Dispose();
            _reader?.Close();
        }
        catch (Exception ex)
        {
            Exceptions?.Add(ex);
        }
    }
    /// <summary>
    /// Moves the current position in the file to the specified location.
    /// </summary>
    /// <param name="offset">An integer specifying the byte offset index.</param>
    /// <param name="origin">A <see cref="SeekOrigin" /> enumerated value indicating the relative start position.</param>
    /// <returns>
    /// A <see cref="long" /> specifying the new position in the file.
    /// </returns>
    public long Seek(int offset, SeekOrigin origin)
    {
        long newPosition = -1;
        try
        {
            if (_sourceStream != null)
                newPosition = _sourceStream.Seek(offset, origin);
        }
        catch (Exception ex)
        {
            Exceptions?.Add(ex);
        }
        return newPosition;
    }
    /// <summary>
    /// Reads the byte array into the specified buffer.
    /// </summary>
    /// <param name="buffer">The byte array buffer to read data into.</param>
    /// <param name="startIndex">The ordinal index of the array at which to start writing.</param>
    /// <param name="numberOfBytes">An integer specifying the number of bytes to be read.</param>
    /// <exception cref="System.NotImplementedException">
    /// This method cannot be implemented in this application.
    /// </exception>
    public void Read(byte[] buffer, int startIndex, int numberOfBytes)
    {
        throw new NotImplementedException();
    }
    /// <summary>
    /// Reads the next boolean value from the <see cref="Stream" />.
    /// </summary>
    /// <returns>
    /// The <see cref="bool" /> value that was read.
    /// </returns>
    public bool ReadBoolean()
    {
        bool value = false;
        try
        {
            byte[]? data = ReadNextEncryptedArray();
            if (data != null)
            {
                value = Translate<bool>(data);
                ByteArrayUtil.Clear(data);
            }
        }
        catch (Exception ex)
        {
            Exceptions?.Add(ex);
        }
        return value;
    }
    /// <summary>
    /// Reads the next byte value from the <see cref="Stream" />.
    /// </summary>
    /// <returns>
    /// The <see cref="byte" /> value that was read.
    /// </returns>
    public byte ReadByte()
    {
        byte value = 0;
        try
        {
            byte[]? data = ReadNextEncryptedArray();
            if (data != null)
            {
                value = Translate<byte>(data);
                ByteArrayUtil.Clear(data);
            }
        }
        catch (Exception ex)
        {
            Exceptions?.Add(ex);
        }
        return value;
    }
    /// <summary>
    /// Reads the next signed byte value from the <see cref="Stream" />.
    /// </summary>
    /// <returns>
    /// The <see cref="sbyte" /> value that was read.
    /// </returns>
    public sbyte ReadSByte()
    {
        sbyte value = 0;
        try
        {
            byte[]? data = ReadNextEncryptedArray();
            if (data != null)
            {
                value = Translate<sbyte>(data);
                ByteArrayUtil.Clear(data);
            }
        }
        catch (Exception ex)
        {
            Exceptions?.Add(ex);
        }
        return value;
    }
    /// <summary>
    /// Reads the next byte array value from the <see cref="Stream" />.
    /// </summary>
    /// <remarks>
    /// This method assumes a <see cref="int"/> length indicator precedes the byte array.  If the length indicator is zero (0),
    /// <b>null</b> is returned.
    /// </remarks>
    /// <returns>
    /// The <see cref="byte" /> array that was read, or <b>null</b>.
    /// </returns>
    public byte[]? ReadByteArray()
    {
        byte[]? value = null;
        try
        {
            value = ReadNextEncryptedArray();
        }
        catch (Exception ex)
        {
            Exceptions?.Add(ex);
        }
        return value;
    }
    /// <summary>
    /// Reads a byte array from the <see cref="Stream"/>.
    /// </summary>
    /// <param name="count">
    /// The number of bytes to be read.
    /// </param>
    /// <returns>
    /// The <see cref="byte"/> array that was read.
    /// </returns>
    public byte[]? ReadBytes(int count)
    {
        byte[]? value = null;
        try
        {
            value = ReadNextEncryptedArray();
        }
        catch (Exception ex)
        {
            Exceptions?.Add(ex);
        }
        return value;
    }
    /// <summary>
    /// Reads a character from the <see cref="Stream"/>.
    /// </summary>
    /// <returns>
    /// The <see cref="char"/> value that was read.
    /// </returns>
    public char ReadChar()
    {
        char value = (char)0;
        try
        {
            byte[]? data = ReadNextEncryptedArray();
            if (data != null)
            {
                value = Translate<char>(data);
                ByteArrayUtil.Clear(data);
            }
        }
        catch (Exception ex)
        {
            Exceptions?.Add(ex);
        }
        return value;
    }
    /// <summary>
    /// Reads a character array from the <see cref="Stream"/>.
    /// </summary>
    /// <remarks>
    /// This method assumes a <see cref="int"/> length indicator precedes the byte array.  If the length indicator is zero (0),
    /// <b>null</b> is returned.
    /// </remarks>
    /// <returns>
    /// The <see cref="char"/> array that was read.
    /// </returns>
    public char[]? ReadCharArray()
    {
        char[]? value = null;
        try
        {
            byte[]? data = ReadNextEncryptedArray();
            if (data != null)
            {
                value = Translate<char[]>(data);
                ByteArrayUtil.Clear(data);
            }
        }
        catch (Exception ex)
        {
            Exceptions?.Add(ex);
        }
        return value;
    }
    /// <summary>
    /// Reads a character array from the <see cref="Stream"/>.
    /// </summary>
    /// <param name="count">
    /// The number of characters to be read.
    /// </param>
    /// <returns>
    /// The <see cref="char"/> array that was read.
    /// </returns>
    public char[]? ReadCharArray(int count)
    {
        char[]? value = null;
        try
        {
            byte[]? data = ReadNextEncryptedArray();
            if (data != null)
            {
                value = Translate<char[]>(data);
                ByteArrayUtil.Clear(data);
            }
        }
        catch (Exception ex)
        {
            Exceptions?.Add(ex);
        }
        return value;
    }
    /// <summary>
    /// Reads the specified date/time value from the stream.
    /// </summary>
    /// <remarks>
    /// This method assumes the date/time value is stored as a long integer containing the filetime value.
    /// </remarks>
    /// <returns>
    /// The <see cref="DateTime" /> value that was read.
    /// </returns>
    public DateTime ReadDateTime()
    {
        DateTime dateValue = DateTime.MinValue;
        try
        {
            long fileTime = ReadInt64();
            dateValue = DateTime.FromFileTime(fileTime);
        }
        catch (Exception ex)
        {
            Exceptions?.Add(ex);
        }
        return dateValue;
    }
    /// <summary>
    /// Reads the specified double-precision value from the stream.
    /// </summary>
    /// <returns>
    /// The <see cref="double" /> value that was read.
    /// </returns>
    public double ReadDouble()
    {
        double value = 0;
        try
        {
            byte[]? data = ReadNextEncryptedArray();
            if (data != null)
            {
                value = Translate<double>(data);
                ByteArrayUtil.Clear(data);
            }
        }
        catch (Exception ex)
        {
            Exceptions?.Add(ex);
        }
        return value;
    }
    /// <summary>
    /// Reads the specified <see cref="decimal" /> value from the stream.
    /// </summary>
    /// <returns>
    /// The <see cref="decimal" /> value that was read.
    /// </returns>
    public decimal ReadDecimal()
    {
        decimal value = 0;
        try
        {
            byte[]? data = ReadNextEncryptedArray();
            if (data != null)
            {
                value = Translate<decimal>(data);
                ByteArrayUtil.Clear(data);
            }
        }
        catch (Exception ex)
        {
            Exceptions?.Add(ex);
        }
        return value;
    }
    /// <summary>
    /// Reads the specified short integer value from the stream.
    /// </summary>
    /// <returns>
    /// The <see cref="short" /> returns that was read.
    /// </returns>
    public short ReadInt16()
    {
        short value = 0;
        try
        {
            byte[]? data = ReadNextEncryptedArray();
            if (data != null)
            {
                value = Translate<short>(data);
                ByteArrayUtil.Clear(data);
            }
        }
        catch (Exception ex)
        {
            Exceptions?.Add(ex);
        }
        return value;
    }
    /// <summary>
    /// Reads the specified unsigned short integer returns from the stream.
    /// </summary>
    /// <returns></returns>
    /// <returns>
    /// The <see cref="ushort" /> returns that was read.
    /// </returns>
    public ushort ReadUInt16()
    {
        ushort value = 0;
        try
        {
            byte[]? data = ReadNextEncryptedArray();
            if (data != null)
            {
                value = Translate<ushort>(data);
                ByteArrayUtil.Clear(data);
            }
        }
        catch (Exception ex)
        {
            Exceptions?.Add(ex);
        }
        return value;
    }
    /// <summary>
    /// Reads the specified integer returns from the stream.
    /// </summary>
    /// <returns></returns>
    /// <returns>
    /// The <see cref="int" /> returns that was read.
    /// </returns>
    public int ReadInt32()
    {
        int value = 0;
        try
        {
            byte[]? data = ReadNextEncryptedArray();
            if (data != null)
            {
                value = Translate<int>(data);
                ByteArrayUtil.Clear(data);
            }
        }
        catch (Exception ex)
        {
            Exceptions?.Add(ex);
        }
        return value;
    }
    /// <summary>
    /// Reads the specified unsigned integer returns from the stream.
    /// </summary>
    /// <returns></returns>
    /// <returns>
    /// The <see cref="uint" /> returns that was read.
    /// </returns>
    public uint ReadUInt32()
    {
        uint value = 0;
        try
        {
            byte[]? data = ReadNextEncryptedArray();
            if (data != null)
            {
                value = Translate<uint>(data);
                ByteArrayUtil.Clear(data);
            }
        }
        catch (Exception ex)
        {
            Exceptions?.Add(ex);
        }
        return value;
    }
    /// <summary>
    /// Reads the specified long integer returns from the stream.
    /// </summary>
    /// <returns></returns>
    /// <returns>
    /// The <see cref="long" /> returns that was read.
    /// </returns>
    public long ReadInt64()
    {
        long value = 0;
        try
        {
            byte[]? data = ReadNextEncryptedArray();
            if (data != null)
            {
                value = Translate<long>(data);
                ByteArrayUtil.Clear(data);
            }
        }
        catch (Exception ex)
        {
            Exceptions?.Add(ex);
        }
        return value;
    }
    /// <summary>
    /// Reads the specified unsigned long integer returns from the stream.
    /// </summary>
    /// <returns></returns>
    /// <returns>
    /// The <see cref="ulong" /> returns that was read.
    /// </returns>
    public ulong ReadUInt64()
    {
        ulong value = 0;
        try
        {
            byte[]? data = ReadNextEncryptedArray();
            if (data != null)
            {
                value = Translate<ulong>(data);
                ByteArrayUtil.Clear(data);
            }
        }
        catch (Exception ex)
        {
            Exceptions?.Add(ex);
        }
        return value;
    }
    /// <summary>
    /// Reads the single-precision returns from the stream.
    /// </summary>
    /// <returns></returns>
    /// <returns>
    /// The <see cref="float" /> returns that was read.
    /// </returns>
    public float ReadSingle()
    {
        float value = 0;
        try
        {
            byte[]? data = ReadNextEncryptedArray();
            if (data != null)
            {
                value = Translate<float>(data);
                ByteArrayUtil.Clear(data);
            }
        }
        catch (Exception ex)
        {
            Exceptions?.Add(ex);
        }
        return value;
    }
    /// <summary>
    /// Reads the specified Half returns from the stream.
    /// </summary>
    /// <returns></returns>
    /// <returns>
    /// The <see cref="Half" /> returns that was read.
    /// </returns>
    public Half ReadHalf()
    {
        Half value = Half.MinValue;
        try
        {
            byte[]? data = ReadNextEncryptedArray();
            if (data != null)
            {
                value = Translate<Half>(data);
                ByteArrayUtil.Clear(data);
            }
        }
        catch (Exception ex)
        {
            Exceptions?.Add(ex);
        }
        return value;
    }
    /// <summary>
    /// Reads the string.
    /// </summary>
    /// <returns></returns>
    public string? ReadString()
    {
        string? value = null;
        try
        {
            byte[]? data = ReadNextEncryptedArray();
            if (data != null)
            {
                value = Translate<string>(data);
                ByteArrayUtil.Clear(data);
            }
        }
        catch (Exception ex)
        {
            Exceptions?.Add(ex);
        }
        return value;
    }
    /// <summary>
    /// Reads the integer from the stream as a 7-bit encoded returns.
    /// </summary>
    /// <returns>
    /// The <see cref="int" /> returns that was read.
    /// </returns>
    public int Read7BitEncodedInt32()
    {
        int value = 0;
        try
        {
            byte[]? data = ReadNextEncryptedArray();
            if (data != null)
            {
                value = Translate<int>(data);
                ByteArrayUtil.Clear(data);
            }
        }
        catch (Exception ex)
        {
            Exceptions?.Add(ex);
        }
        return value;
    }
    /// <summary>
    /// Reads the long integer from the stream as a 7-bit encoded returns.
    /// </summary>
    /// <returns>
    /// The <see cref="long" /> returns that was read.
    /// </returns>
    public long Read7BitEncodedInt64()
    {
        long value = 0;
        try
        {
            byte[]? data = ReadNextEncryptedArray();
            if (data != null)
            {
                value = Translate<long>(data);
                ByteArrayUtil.Clear(data);
            }
        }
        catch (Exception ex)
        {
            Exceptions?.Add(ex);
        }
        return value;
    }
    #endregion

    #region Private Member Declarations		
    /// <summary>
    /// Translates the provided byte array to a specified data type.
    /// </summary>
    /// <typeparam name="T">
    /// The data type of the data to be translated.	
    /// </typeparam>
    /// <param name="value">
    /// A byte array containing the representation of the value.
    /// </param>
    /// <returns>
    /// The instance of <typeparamref name="T"/> that was translated.
    /// </returns>
    private T? Translate<T>(byte[] value)
    {
        T? returnContent = default(T);
        if (typeof(T) == typeof(string))
            returnContent = (T)(object)string.Empty;
        object? converted = null;

        switch (returnContent)
        {
            case bool boolValue:
                converted = BitConverter.ToBoolean(value);
                break;

            case byte byteValue:
                converted = value[0];
                break;

            case byte[] byteArray:
                converted = ByteArrayUtil.CopyToNewArray(value);
                break;

            case sbyte sbyteValue:
                converted = (sbyte)value[0];
                break;

            case char charValue:
                converted = BitConverter.ToChar(value);
                break;

            case char[] charArray:
                List<char> list = new List<char>();
                MemoryStream ms = new MemoryStream(value);
                BinaryReader reader = new BinaryReader(ms);
                do
                {
                    char c = reader.ReadChar();
                    list.Add(c);
                } while (ms.Position < ms.Length);

                converted = list.ToArray();
                list.Clear();
                reader.Dispose();
                ms.Dispose();
                break;

            case short shortValue:
                converted = BitConverter.ToInt16(value);
                break;

            case ushort ushortValue:
                converted = BitConverter.ToUInt16(value);
                break;

            case int intValue:
                converted = BitConverter.ToInt32(value);
                break;

            case uint uintValue:
                converted = BitConverter.ToUInt32(value);
                break;

            case long longValue:
                converted = BitConverter.ToInt64(value);
                break;

            case ulong ulongValue:
                converted = BitConverter.ToUInt64(value);
                break;

            case float floatValue:
                converted = BitConverter.ToSingle(value);
                break;

            case double doubleValue:
                converted = BitConverter.ToDouble(value);
                break;

            case decimal decimalValue:
                converted = SafeConverter.DecimalFromArrray(value);
                break;

            case string stringValue:
                converted = System.Text.Encoding.UTF8.GetString(value);
                break;
        }
        returnContent = (T?)converted;

        return returnContent;
    }
    /// <summary>
    /// Reads the next encrypted byte array.
    /// </summary>
    /// <remarks>
    /// This assumes the byte array is preceded with an integer length indicator.
    /// </remarks>
    /// <returns>
    /// An encrypted byte array from the data store.
    /// </returns>
    public byte[]? ReadNextEncryptedArray()
    {
        byte[]? clearContent = null;

        if (_reader != null && _aes != null)
        {
            int length = _reader.ReadInt32();
            if (length > 0)
            {
                byte[] encrypted = _reader.ReadBytes(length);
                clearContent = _aes.Decrypt(encrypted);
                ByteArrayUtil.Clear(encrypted);
            }
        }
        return clearContent;
    }
    /// <summary>
    /// Sets the writer to use the key variant for the encryption keys.
    /// </summary>
    public void SetForKeyVariant()
    {
        _aes?.Dispose();
        _aes = _keyManager?.CreateAesProviderForVariant();
    }
    /// <summary>
    /// Sets the writer to use the standard key data for the encryption keys.
    /// </summary>
    public void SetForKeyStandard()
    {
        _aes?.Dispose();
        _aes = _keyManager?.CreateAesProvider();
    }
    #endregion
}