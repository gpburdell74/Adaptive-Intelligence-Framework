using Adaptive.Intelligence.Shared.Properties;
using System.Security.Cryptography;

namespace Adaptive.Intelligence.Shared.Security.IO
{
	/// <summary>
	/// Provides a secure reader implementation for reading encrypted data streams.
	/// </summary>
	/// <seealso cref="ExceptionTrackingBase" />
	/// <seealso cref="ICryptoReader" />
	public sealed class SecureReader : ExceptionTrackingBase, ICryptoReader
	{
		#region Private Member Declarations		
		/// <summary>
		/// The source stream to read from.
		/// </summary>
		private Stream? _sourceStream;
		/// <summary>
		/// The reader for the source stream.
		/// </summary>
		private BinaryReader? _reader;
		/// <summary>
		/// The key table to use.
		/// </summary>
		private AesKeyTable? _keyTable;
		/// <summary>
		/// The SHA-512 hash provider instance.
		/// </summary>
		private SHA512? _hasher;
		/// <summary>
		/// The AES cryptographic provider instance.
		/// </summary>
		private AesProvider? _provider;
		/// <summary>
		/// The data block size.
		/// </summary>
		private const int BlockSize = 65536;
		#endregion

		#region Constructor / Dispose Methods		
		/// <summary>
		/// Initializes a new instance of the <see cref="SecureReader"/> class.
		/// </summary>
		/// <param name="sourceStream">
		/// The source <see cref="Stream"/> containing the encrypted content to be read from.
		/// </param>
		public SecureReader(Stream sourceStream)
		{
			_sourceStream = sourceStream;
			_hasher = SHA512.Create();
			_provider = new AesProvider();
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
				_provider?.Dispose();
				_hasher?.Dispose();
				_keyTable?.Dispose();
			}

			_provider = null;
			_reader = null;
			_sourceStream = null;
			_keyTable = null;
			_hasher = null;

			base.Dispose(disposing);
		}
		#endregion

		#region Public Methods / Functions		
		/// <summary>
		/// Initializes the key data being used by the reader.
		/// </summary>
		/// <param name="keyTable">The reference to the <see cref="AesKeyTable" /> implementation containing
		/// the cryptographic key data to be used.
		/// </param>
		public void InitializeKeys(IKeyTable keyTable)
		{
			_keyTable = (AesKeyTable)keyTable;
		}
		/// <summary>
		/// Reads and decrypts the content from the original source and writes the clear content to
		/// the provided stream.
		/// </summary>
		/// <param name="destinationStream">The source <see cref="Stream" /> to which the decrypted data is to be written.</param>
		/// <exception cref="System.InvalidOperationException">
		/// Keys have not been initialized.
		/// or
		/// Specified source stream cannot be written to.
		/// </exception>
		public void ReadStream(Stream destinationStream)
		{
			if (_keyTable == null)
				throw new InvalidOperationException(Resources.ErrorKeysNotInitialized);
			if (!destinationStream.CanWrite)
				throw new InvalidOperationException(Resources.ErrorStreamRead);
			if (_sourceStream == null)
				throw new InvalidOperationException(Resources.ErrorStreamWrite);
			
			// Create the reader and writer instances.
			BinaryWriter? writer = CreateWriter(destinationStream);
			if (writer != null && CreateReader())
			{
				// Read the stream content.
				ReadStreamContent(writer);
				writer.Flush();
				writer.Dispose();
			}
		}
		#endregion

		#region Private Methods / Functions		
		/// <summary>
		/// Creates the binary writer instance for the destination stream.
		/// </summary>
		/// <param name="destinationStream">The destination stream.</param>
		/// <returns>
		/// The new <see cref="BinaryWriter"/> instance, or <b>null</b> if the operation fails.
		/// </returns>
		private BinaryWriter? CreateWriter(Stream destinationStream)
		{
			BinaryWriter? writer = null;
			try
			{
				writer = new BinaryWriter(destinationStream);
			}
			catch (Exception ex)
			{
				Exceptions.Add(ex);
				writer = null;
			}
			return writer;
		}
		/// <summary>
		/// Creates the binary reader instance.
		/// </summary>
		/// <returns>
		/// <b>true</b> if the operation is successful; otherwise, returns <b>false</b>.
		/// </returns>
		private bool CreateReader()
		{
			if (_sourceStream != null)
			{
				try
				{
					_reader = new BinaryReader(_sourceStream);
				}
				catch (Exception ex)
				{
					Exceptions.Add(ex);
					_reader = null;
				}
			}
			return (_reader != null);
		}
		/// <summary>
		/// Reads and decrypts the content of the source stream, and writes the clear content to the
		/// output <i>writer</i> instance.
		/// </summary>
		/// <param name="writer">
		/// The <see cref="BinaryWriter"/> instance used to write the decrypted data content.
		/// </param>
		private void ReadStreamContent(BinaryWriter writer)
		{
			// Read the number of total blocks.
			int length = _reader!.ReadInt32();
			byte[] encryptedLengthData = _reader.ReadBytes(length);
			int blockCount = DecryptInt(encryptedLengthData, _keyTable.Senary);
			CryptoUtil.SecureClear(encryptedLengthData);

			// Read the size of the last block.
			length = _reader.ReadInt32();
			byte[] encryptedSizeData = _reader.ReadBytes(length);
			int lastBlockSize = DecryptInt(encryptedSizeData, _keyTable.Senary);
			CryptoUtil.SecureClear(encryptedSizeData);

			int position = 0;
			_keyTable.Reset();
			do
			{
				// Read the block's encrypted size data.
				length = _reader.ReadInt32();
				encryptedSizeData = _reader.ReadBytes(length);
				int actualLength = DecryptInt(encryptedSizeData, _keyTable.Next());
				CryptoUtil.SecureClear(encryptedSizeData);

				// Read the next block of encrypted data.
				// Decrypt the data and the hash.
				byte[] encryptedContent = _reader.ReadBytes(actualLength);
				byte[]? decryptedData = DecryptBlock(encryptedContent, lastBlockSize);
				if (decryptedData != null)
				{
					// Now write the block of data to the output stream.
					writer.Write(decryptedData);
				}
				writer.Flush();

				// Clear memory.
				CryptoUtil.SecureClear(decryptedData);
				CryptoUtil.SecureClear(encryptedContent);
				position++;
			} while (position < blockCount);
		}
		/// <summary>
		/// Decrypts the integer value from the provided data.
		/// </summary>
		/// <param name="encryptedInt">
		/// A byte array containing the encrypted integer data.
		/// </param>
		/// <param name="keyData">
		/// A byte array containing the key data.
		/// </param>
		/// <returns>
		/// The decrypted integer value.
		/// </returns>
		private int DecryptInt(byte[] encryptedInt, byte[] keyData)
		{
			int value = 0;

			_provider!.SetKeyIV(keyData);
			byte[]? intBits = _provider!.Decrypt(encryptedInt);
			if (intBits != null)
			{
				value = BitConverter.ToInt32(intBits, 0);
				CryptoUtil.SecureClear(intBits);
			}
			return value;
		}
		/// <summary>
		/// Decrypts the data block.
		/// </summary>
		/// <param name="obfuscatedCryptoContent">
		/// A byte array containing the content of the obfuscated cryptographic data.
		/// </param>
		/// <param name="lastBlockSize">
		/// An integer specifying the original size of the last data block.
		/// </param>
		/// <returns>
		/// A byte array containing the decrypted content, or <b>null</b> if the operation fails.
		/// </returns>
		/// <exception cref="System.Security.Cryptography.CryptographicUnexpectedOperationException">The data could not be decrypted.</exception>
		/// <exception cref="Adaptive.Intelligence.Shared.ExceptionEventArgs.Exception">The hash data does not match the original. This data may have been modified.</exception>
		private byte[]? DecryptBlock(byte[] obfuscatedCryptoContent, int lastBlockSize)
		{
			int size = BlockSize;

			// Set the key data.
			_provider!.SetKeyIV(_keyTable!.Next());

			// De-obfuscate the bits.
			byte[]? encrypted = BitSplicer.UnSpliceBits(obfuscatedCryptoContent);

			// Decrypt the data and set the expected data block size.
			byte[]? decrypted = _provider!.Decrypt(encrypted);
			if (decrypted == null)
				throw new CryptographicUnexpectedOperationException(Resources.ErrorCantDecrypt);

			if (decrypted.Length < BlockSize + 64)
				size = lastBlockSize;

			// Split the data and the has - perform the hash comparison.
			byte[] clearData = new byte[size];
			byte[] clearHash = new byte[64];
			Array.Copy(decrypted, 0, clearData, 0, size);
			Array.Copy(decrypted, size, clearHash, 0, 64);

			// Compare the hash data.
			byte[] newHash = _hasher!.ComputeHash(clearData);
			if (ArrayExtensions.Compare(clearHash, newHash) != 0)
			{
				// If the hash values do not match, wipe out the decrypted content and throw
				// an exception.
				CryptoUtil.SecureClear(clearData);
				throw new Exception(Resources.ErrorBadHash);
			}

			CryptoUtil.SecureClear(encrypted);
			CryptoUtil.SecureClear(decrypted);
			CryptoUtil.SecureClear(clearHash);
			CryptoUtil.SecureClear(newHash);
			return clearData;

		}
		#endregion
	}
}
