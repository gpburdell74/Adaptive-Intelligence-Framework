using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.Shared.IO;
using Adaptive.Intelligence.Shared.Security;

namespace Adaptive.Taz.Cryptography
{
	/// <summary>
	/// Provides a mechanism for writing encrypted binary data to a stream where the instance tracks its own exceptions.	
	/// </summary>
	/// <seealso cref="ExceptionTrackingBase" />
	/// <seealso cref="ISafeBinaryWriter" />
	public sealed class SecureBinaryWriter : ExceptionTrackingBase, ISafeBinaryWriter
	{
		#region Private Member Declarations				
		/// <summary>
		/// The AES cryptography provider.
		/// </summary>
		private AesProvider? _aes;
		/// <summary>
		/// The key manager instance.
		/// </summary>
		private KeyManager? _keyManager;
		/// <summary>
		/// The reference to the stream to be written to.
		/// </summary>
		private Stream? _outputStream;
		/// <summary>
		/// The underlying writer instance.
		/// </summary>
		private BinaryWriter? _writer;
		/// <summary>
		/// A flag indicating whether the writer instance's scope is local.
		/// </summary>
		private bool _writerLocal;
		#endregion

		#region Constructor / Dispose Methods		
		/// <summary>
		/// Initializes a new instance of the <see cref="SecureBinaryWriter"/> class.
		/// </summary>
		/// <param name="manager">
		/// The reference to the <see cref="KeyManager"/> instance used to manage the encryption keys.
		/// </param>
		/// <param name="outputStream">
		/// The output <see cref="Stream"/> to be written to.
		/// </param>
		public SecureBinaryWriter(KeyManager manager, Stream outputStream)
		{
			_keyManager = manager;
			_aes = manager.CreateAesProvider();
			_outputStream = outputStream;
			_writer = new BinaryWriter(outputStream);
			_writerLocal = true;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="SecureBinaryWriter"/> class.
		/// </summary>
		/// <param name="manager">
		/// The reference to the <see cref="KeyManager"/> instance used to manage the encryption keys.
		/// </param>
		/// <param name="writer">
		/// The <see cref="BinaryWriter"/> instance to be used.
		/// </param>
		public SecureBinaryWriter(KeyManager manager, BinaryWriter writer)
		{
			_keyManager = manager;
			_aes = manager.CreateAesProvider();
			_writer = writer;
			_outputStream = writer.BaseStream;
			_writerLocal = false;
		}
		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources asynchronously.
		/// </summary>
		/// <returns>
		/// A task that represents the asynchronous dispose operation.
		/// </returns>
		/// <exception cref="System.NotImplementedException"></exception>
		public async ValueTask DisposeAsync()
		{
			Dispose(true);
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
				if (_writerLocal)
					_writer?.Dispose();
			}

			_aes = null;
			_keyManager = null;
			_writer = null;
			_outputStream = null;
			base.Dispose(disposing);
		}
		#endregion

		#region Public Properties		
		/// <summary>
		/// Returns the stream associated with the writer. It flushes all pending
		/// writes before returning. All subclasses should override Flush to
		/// ensure that all buffered data is sent to the stream.
		/// </summary>
		/// <value>
		/// The underlying <see cref="Stream" /> that is being written to.
		/// </value>
		public Stream? BaseStream => _outputStream;
		/// <summary>
		/// Gets a value indicating whether this instance can write data.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance can write to an underlying stream; otherwise, <c>false</c>.
		/// </value>
		public bool CanWrite
		{
			get
			{
				return (_outputStream != null && _writer != null && _keyManager != null && _aes != null && _outputStream.CanWrite);
			}
		}
		/// <summary>
		/// Gets the reference to the underlying binary writer instance.
		/// </summary>
		/// <value>
		/// The <see cref="BinaryWriter" /> instance being used to do the writing.
		/// </value>
		public BinaryWriter? Writer => _writer;
		#endregion

		#region Public Methods / Functions		
		/// <summary>
		/// Closes this writer and releases any system resources associated with the
		/// writer. Following a call to Close, any operations on the writer
		/// may raise exceptions.
		/// </summary>
		public void Close()
		{
			try
			{
				_aes?.Dispose();
				_writer?.Close();
			}
			catch (Exception ex)
			{
				Exceptions?.Add(ex);
			}
		}
		/// <summary>
		/// Clears all buffers for this writer and causes any buffered data to be
		/// written to the underlying device.
		/// </summary>
		public void Flush()
		{
			try
			{
				_writer?.Flush();
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
				if (_writer != null)
					newPosition = _writer.Seek(offset, origin);
			}
			catch (Exception ex)
			{
				Exceptions?.Add(ex);
			}
			return newPosition;
		}
		/// <summary>
		/// Sets the writer to use the standard key data for the encryption keys.
		/// </summary>
		public void SetForKeyStandard()
		{
			_aes?.Dispose();
			_aes = _keyManager.CreateAesProvider();
		}

		/// <summary>
		/// Sets the writer to use the key variant for the encryption keys.
		/// </summary>
		public void SetForKeyVariant()
		{
			_aes?.Dispose();
			_aes = _keyManager.CreateAesProviderForVariant();
		}

		/// <summary>
		/// Writes a boolean to this stream. A single byte is written to the stream
		/// with the value 0 representing false or the value 1 representing true.
		/// </summary>
		/// <param name="value">
		/// The boolean value to be written.
		/// </param>
		public void Write(bool value)
		{
			try
			{
				WriteEncrypted(value);
			}
			catch (Exception ex)
			{
				Exceptions?.Add(ex);
			}
		}
		/// <summary>
		/// Writes a byte to this stream. The current position of the stream is
		/// advanced by one.
		/// </summary>
		/// <param name="value">
		/// The byte value to be written.
		/// </param>
		public void Write(byte value)
		{
			try
			{
				WriteEncrypted(value);
			}
			catch (Exception ex)
			{
				Exceptions?.Add(ex);
			}
		}
		/// <summary>
		/// Writes a signed byte to this stream. The current position of the stream
		/// is advanced by one.
		/// </summary>
		/// <param name="value">
		/// The <see cref="sbyte"/> value to be written.
		/// </param>
		public void Write(sbyte value)
		{
			try
			{
				WriteEncrypted(value);
			}
			catch (Exception ex)
			{
				Exceptions?.Add(ex);
			}
		}
		/// <summary>
		/// Writes a byte array to this stream.
		/// This default implementation calls the Write(Object, int, int)
		/// method to write the byte array.
		/// </summary>
		/// <param name="buffer">
		/// The byte array to be written to the stream.
		/// </param>
		public void Write(byte[] buffer)
		{
			try
			{
				WriteEncrypted(buffer);
			}
			catch (Exception ex)
			{
				Exceptions?.Add(ex);
			}
		}
		/// <summary>
		/// Writes a section of a byte array to this stream.
		/// This default implementation calls the Write(Object, int, int)
		/// method to write the byte array.
		/// </summary>
		/// <param name="buffer">
		/// The byte array to be written to the stream.
		/// </param>
		/// <param name="index">
		/// The ordinal index at which to begin reading from the byte array.
		/// </param>
		/// <param name="count">
		/// The number of bytes to be written.
		/// </param>
		public void Write(byte[] buffer, int index, int count)
		{
			try
			{
				byte[] subBuffer = new byte[count];
				Array.Copy(buffer, index, subBuffer, 0, count);
				WriteEncrypted(subBuffer);
				Array.Clear(subBuffer, 0, subBuffer.Length);
			}
			catch (Exception ex)
			{
				Exceptions?.Add(ex);
			}
		}

		/// <summary>
		/// Writes a character to this stream. The current position of the stream is
		/// advanced by two.
		/// Note this method cannot handle surrogates properly in UTF-8.
		/// </summary>
		/// <param name="ch">
		/// The <see cref="char"/> value to be written to the stream.
		/// </param>
		public void Write(char ch)
		{
			try
			{
				WriteEncrypted(ch);
			}
			catch (Exception ex)
			{
				Exceptions?.Add(ex);
			}
		}

		/// <summary>
		/// Writes a character array to this stream.
		/// This default implementation calls the Write(Object, int, int)
		/// method to write the character array.
		/// </summary>
		/// <param name="chars">
		/// The <see cref="char"/> array to be written to the stream.
		/// </param>
		public void Write(char[] chars)
		{
			try
			{
				WriteEncrypted(chars);
			}
			catch (Exception ex)
			{
				Exceptions?.Add(ex);
			}
		}

		/// <summary>
		/// Writes a section of a character array to this stream.
		/// This default implementation calls the Write(Object, int, int)
		/// method to write the character array.
		/// </summary>
		/// <param name="chars">
		/// The <see cref="char"/> array to be written to the stream.
		/// </param>
		/// <param name="index">
		/// The ordinal index at which to begin reading from the character array.
		/// </param>
		/// <param name="count">
		/// The number of characters to be written.
		/// </param>
		public void Write(char[] chars, int index, int count)
		{
			try
			{
				char[] subBuffer = new char[count];
				Array.Copy(chars, index, subBuffer, 0, count);
				WriteEncrypted(subBuffer);
				Array.Clear(subBuffer, 0, subBuffer.Length);
			}
			catch (Exception ex)
			{
				Exceptions?.Add(ex);
			}
		}

		/// <summary>
		/// Writes the specified double value to the stream.
		/// </summary>
		/// <param name="value">
		/// The <see cref="double"/> value to be written.
		/// </param>
		public void Write(double value)
		{
			try
			{
				WriteEncrypted(value);
			}
			catch (Exception ex)
			{
				Exceptions?.Add(ex);
			}
		}

		/// <summary>
		/// Writes the specified date/time value to the stream.  The value is converted to a filetime vlaue and written
		/// as a long integer.
		/// </summary>
		/// <param name="dateTime">
		/// The <see cref="DateTime"/> value to be written.
		/// </param>
		public void Write(DateTime dateTime)
		{
			try
			{
				long value = dateTime.ToFileTime();
				WriteEncrypted(value);
			}
			catch (Exception ex)
			{
				Exceptions?.Add(ex);
			}
		}

		/// <summary>
		/// Writes the specified decimal value to the stream.
		/// </summary>
		/// <param name="value">
		/// The <see cref="decimal"/> value to be written.
		/// </param>
		public void Write(decimal value)
		{
			try
			{
				WriteEncrypted(value);
			}
			catch (Exception ex)
			{
				Exceptions?.Add(ex);
			}
		}

		/// <summary>
		/// Writes the specified short integer value to the stream.
		/// </summary>
		/// <param name="value">
		/// The <see cref="short"/> value to be written.
		/// </param>
		public void Write(short value)
		{
			try
			{
				WriteEncrypted(value);
			}
			catch (Exception ex)
			{
				Exceptions?.Add(ex);
			}
		}

		/// <summary>
		/// Writes the specified unsigned short integer value to the stream.
		/// </summary>
		/// <param name="value">
		/// The <see cref="ushort"/> value to be written.
		/// </param>
		public void Write(ushort value)
		{
			try
			{
				WriteEncrypted(value);
			}
			catch (Exception ex)
			{
				Exceptions?.Add(ex);
			}
		}

		/// <summary>
		/// Writes the specified integer value to the stream.
		/// </summary>
		/// <param name="value">
		/// The <see cref="int"/> value to be written.
		/// </param>
		public void Write(int value)
		{
			try
			{
				WriteEncrypted(value);
			}
			catch (Exception ex)
			{
				Exceptions?.Add(ex);
			}
		}

		/// <summary>
		/// Writes the specified unsigned integer value to the stream.
		/// </summary>
		/// <param name="value">
		/// The <see cref="uint"/> value to be written.
		/// </param>
		public void Write(uint value)
		{
			try
			{
				WriteEncrypted(value);
			}
			catch (Exception ex)
			{
				Exceptions?.Add(ex);
			}
		}

		/// <summary>
		/// Writes an eight-byte signed integer to this stream. The current position
		/// of the stream is advanced by eight.
		/// </summary>
		/// <param name="value">
		/// The <see cref="long"/> value to be written.
		/// </param>
		public void Write(long value)
		{
			try
			{
				WriteEncrypted(value);
			}
			catch (Exception ex)
			{
				Exceptions?.Add(ex);
			}
		}

		/// <summary>
		/// Writes an eight-byte unsigned integer to this stream. The current
		/// position of the stream is advanced by eight.
		/// </summary>
		/// <param name="value">
		/// The <see cref="ulong"/> value to be written.
		/// </param>
		public void Write(ulong value)
		{
			try
			{
				WriteEncrypted(value);
			}
			catch (Exception ex)
			{
				Exceptions?.Add(ex);
			}
		}

		/// <summary>
		/// Writes a float to this stream. The current position of the stream is
		/// advanced by four.
		/// </summary>
		/// <param name="value">
		/// The <see cref="float"/> value to be written.
		/// </param>
		public void Write(float value)
		{
			try
			{
				WriteEncrypted(value);
			}
			catch (Exception ex)
			{
				Exceptions?.Add(ex);
			}
		}

		/// <summary>
		/// Writes a half to this stream. The current position of the stream is
		/// advanced by two.
		/// </summary>
		/// <param name="value">
		/// The <see cref="Half"/> value to be written.
		/// </param>
		public void Write(Half value)
		{
			try
			{
				WriteEncrypted(value);
			}
			catch (Exception ex)
			{
				Exceptions?.Add(ex);
			}
		}

		/// <summary>
		/// Writes a length-prefixed string to this stream in the BinaryWriter's
		/// current Encoding. This method first writes the length of the string as
		/// an encoded unsigned integer with variable length, and then writes that many characters
		/// to the stream.
		/// </summary>
		/// <param name="value">
		/// The <see cref="string"/> value to be written.
		/// </param>
		public void Write(string value)
		{
			try
			{
				WriteEncrypted(value);
			}
			catch (Exception ex)
			{
				Exceptions?.Add(ex);
			}
		}

		/// <summary>
		/// Writes the content of the read-only span of bytes to the stream.
		/// </summary>
		/// <param name="buffer">
		/// The <see cref="ReadOnlySpan{T}" /> of <see cref="byte" /> containing the data to be written
		/// </param>
		public void Write(ReadOnlySpan<byte> buffer)
		{
			try
			{
				WriteEncrypted(buffer.ToArray());
			}
			catch (Exception ex)
			{
				Exceptions?.Add(ex);
			}
		}

		/// <summary>
		/// Writes the content of the read-only span of chars to the stream.
		/// </summary>
		/// <param name="chars">
		/// The <see cref="ReadOnlySpan{T}" /> of <see cref="char" /> containing the data to be written
		/// </param>
		public void Write(ReadOnlySpan<char> chars)
		{
			try
			{
				WriteEncrypted(chars.ToArray());
			}
			catch (Exception ex)
			{
				Exceptions?.Add(ex);
			}
		}

		/// <summary>
		/// Writes the integer to the strea as a 7-bit encoded value.
		/// </summary>
		/// <param name="value">
		/// The integer value to be written.
		/// </param>
		public void Write7BitEncodedInt(int value)
		{
			try
			{
				WriteEncrypted(value);
			}
			catch (Exception ex)
			{
				Exceptions?.Add(ex);
			}
		}

		/// <summary>
		/// Writes the long integer to the strea as a 7-bit encoded value.
		/// </summary>
		/// <param name="value">
		/// The long value to be written.
		/// </param>
		public void Write7BitEncodedInt64(long value)
		{
			try
			{
				WriteEncrypted(value);
			}
			catch (Exception ex)
			{
				Exceptions?.Add(ex);
			}
		}

		/// <summary>
		/// Writes the byte array to the stream.
		/// </summary>
		/// <param name="data">A byte array containing the data to be written, or <b>null</b>.</param>
		/// <remarks>
		/// This method writes a null/not null indicator, then, if the data is not <see langword="null" />, then writes the <see cref="T:System.Int32" /> length indicator, and
		/// then, if present, writes the byte array.
		/// </remarks>
		public void WriteByteArray(byte[]? data)
		{
			if (data == null)
				WriteEncrypted<bool>(true);
			else
			{
				WriteEncrypted(false);
				WriteEncrypted(data);
			}
		}
		#endregion

		#region Private Methods / Functions
		/// <summary>
		/// Translates the provided data type to a byte array.
		/// </summary>
		/// <typeparam name="T">
		/// The data type of the data to be translated.	
		/// </typeparam>
		/// <param name="value">
		/// The data value.
		/// </param>
		/// <returns>
		/// A byte array representing the data value.
		/// </returns>
		private byte[]? GetBytes<T>(T value)
		{
			byte[]? returnContent = null;

			switch (value)
			{
				case bool boolValue:
					returnContent = BitConverter.GetBytes(boolValue);
					break;

				case byte byteValue:
					returnContent = new byte[] { byteValue };
					break;

				case byte[] byteArray:
					returnContent = ByteArrayUtil.CopyToNewArray(byteArray);
					break;

				case sbyte sbyteValue:
					returnContent = new byte[] { (byte)sbyteValue };
					break;

				case char charValue:
					returnContent = BitConverter.GetBytes(charValue);
					break;

				case char[] charArray:
					MemoryStream ms = new MemoryStream();
					foreach (char c in charArray)
					{
						ms.Write(BitConverter.GetBytes(c));
					}
					returnContent = ms.ToArray();
					ms.Dispose();
					break;

				case short shortValue:
					returnContent = BitConverter.GetBytes(shortValue);
					break;

				case ushort ushortValue:
					returnContent = BitConverter.GetBytes(ushortValue);
					break;

				case int intValue:
					returnContent = BitConverter.GetBytes(intValue);
					break;

				case uint uintValue:
					returnContent = BitConverter.GetBytes(uintValue);
					break;

				case long longValue:
					returnContent = BitConverter.GetBytes(longValue);
					break;

				case ulong ulongValue:
					returnContent = BitConverter.GetBytes(ulongValue);
					break;

				case float floatValue:
					returnContent = BitConverter.GetBytes(floatValue);
					break;

				case double doubleValue:
					returnContent = BitConverter.GetBytes(doubleValue);
					break;

				case decimal decimalValue:
					returnContent = SafeConverter.DecimalToArrray(decimalValue);
					break;

				case string stringValue:
					returnContent = System.Text.Encoding.UTF8.GetBytes(stringValue);
					break;
			}

			return returnContent;
		}
		/// <summary>
		/// Encrypts the provided value, and then writes the encrypted byte array content to the stream.
		/// </summary>
		/// <remarks>
		/// Each array is prefixed with an integer value indicating the length of the array.
		/// </remarks>
		/// <typeparam name="T">
		/// The data type of the value being encrypted and written.
		/// </typeparam>
		/// <param name="value">
		/// The value to be encrypted and written.
		/// </param>
		private void WriteEncrypted<T>(T value)
		{
			if (_writer != null && _aes != null)
			{
				byte[]? data = GetBytes<T>(value);
				if (data == null || data.Length == 0)
					_writer.Write((int)0);
				else
				{
					byte[]? encrypted = _aes.Encrypt(data);
					if (encrypted != null)
					{
						_writer.Write((int)encrypted.Length);
						_writer.Write(encrypted);
						_writer.Flush();
						ByteArrayUtil.Clear(encrypted);
					}
					ByteArrayUtil.Clear(data);
				}
			}
		}
		#endregion
	}
}