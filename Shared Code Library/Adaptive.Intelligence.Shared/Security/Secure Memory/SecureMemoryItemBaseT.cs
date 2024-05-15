using Adaptive.Intelligence.Shared.Logging;
using System.Security.Cryptography;

namespace Adaptive.Intelligence.Shared.Security
{
	/// <summary>
	/// Provides the base implementation for storing data items in memory securely.
	/// </summary>
	/// <typeparam name="T">
	/// The data type of the content being stored.
	/// </typeparam>
	/// <seealso cref="DisposableObjectBase" />
	public abstract class SecureMemoryItemBase<T> : DisposableObjectBase 
	{
		#region Private Member Declarations		
		/// <summary>
		/// The default number of random number generator key iterations.
		/// </summary>
		private const int DefaultKeyIterations = 2048;

		/// <summary>
		/// The number of iterations to use when generating the private key data.
		/// </summary>
		private int _iterations = DefaultKeyIterations;
		/// <summary>
		/// The AES cryptographic instance.
		/// </summary>
		private Aes? _aes;
		/// <summary>
		/// The encryptor transformation instance to use.
		/// </summary>
		private ICryptoTransform? _encryptor;
		/// <summary>
		/// The decryptor transformation instance to use.
		/// </summary>
		private ICryptoTransform? _decryptor;
		/// <summary>
		/// The stored content.
		/// </summary>
		private byte[]? _storage;
		/// <summary>
		/// The size of the data storage content.
		/// </summary>
		private int _storageLength = -1;
		#endregion

		#region Constructor / Dispose Methods		
		/// <summary>
		/// Initializes a new instance of the <see cref="SecureMemoryItemBase{T}"/> class.
		/// </summary>
		/// <remarks>
		/// This is the default constructor.
		/// </remarks>
		protected SecureMemoryItemBase()
		{
			Initialize();
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="SecureMemoryItemBase{T}"/> class.
		/// </summary>
		/// <param name="iterations">
		/// The number of key generation iterations to execute.
		/// </param>
		protected SecureMemoryItemBase(int iterations)
		{
			_iterations = iterations;
			Initialize();
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="SecureMemoryItemBase{T}"/> class.
		/// </summary>
		/// <param name="value">
		/// The value to be securely stored in memory.
		/// </param>
		protected SecureMemoryItemBase(T value)
		{
			Initialize();
			Value = value;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="SecureMemoryItemBase{T}"/> class.
		/// </summary>
		/// <param name="iterations">
		/// The number of key generation iterations to execute.
		/// </param>
		/// <param name="value">
		/// The value to be securely stored in memory.
		/// </param>
		protected SecureMemoryItemBase(int iterations, T value)
		{
			_iterations = iterations;
			Initialize();
			Value = value;
		}
		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
		/// <b>false</b> to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			// Wipe the memory storage.
			ClearStorage();

			// Dispose.
			_encryptor?.Dispose();
			_decryptor?.Dispose();
			_aes?.Dispose();

			_aes = null;
			_encryptor = null;
			_decryptor = null;
			_storage = null;
			_storageLength = -1;
			_iterations = 0;
			base.Dispose(disposing);
		}
		#endregion

		#region Public Properties		
		/// <summary>
		/// Gets a value indicating whether this instance represents a <b>null</b> value.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance represents a <b>null</b> value; otherwise, <c>false</c>.
		/// </value>
		public bool IsNull => _storageLength < 1;
		/// <summary>
		/// Gets or sets the value being stored in memory.
		/// </summary>
		/// <value>
		/// The <typeparamref name="T"/> value currently stored in memory, or the default value if nothing is stored.
		/// </value>
		public T? Value
		{
			get
			{
				T? clearValue = default(T);
				try
				{
					clearValue = TranslateValue();
				}
				catch(Exception  ex)
				{
					ExceptionLog.LogException(ex);
				}
				return clearValue!;
			}
			set
			{
				ClearStorage();

				try
				{
					SetValue(value);
				}
				catch (Exception ex)
				{
					ExceptionLog.LogException(ex);
				}
			}
		}
		#endregion

		#region Protected Abstract Methods / Functions		
		/// <summary>
		/// Translates provided byte array into a value of <typeparamref name="T"/>.
		/// </summary>
		/// <param name="content">
		/// A byte array containing the binary representation of the value.
		/// </param>
		/// <returns></returns>
		protected abstract T? TranslateValueFromBytes(byte[]? content);
		/// <summary>
		/// Translates the value to a byte array.
		/// </summary>
		/// <param name="value">
		/// The <typeparamref name="T"/> value to be translated.
		/// </param>
		/// <returns>
		/// A byte array representing the binary representation of the specified value.
		/// </returns>
		protected abstract byte[]? TranslateValueToBytes(T? value);
		#endregion

		#region Public Methods / Functions		
		/// <summary>
		/// Clears and removes the byte array acting as the storage for the memory item.
		/// </summary>
		public void ClearStorage()
		{
			if (_storage != null && _storageLength > 0)
			{
				Array.Clear(_storage, 0, _storageLength);
				_storage = null;
				_storageLength = -1;
			}
		}
		#endregion

		#region Private Methods / Functions		
		/// <summary>
		/// Initializes this instance for use.
		/// </summary>
		private void Initialize()
		{
			byte[]? randomizedPassword = new byte[256];
			byte[]? salt = new byte[64];

			// Generate a random list of password values and the salt.
			RandomNumberGenerator? rng = RandomNumberGenerator.Create();

			try
			{
				// Get 256 bytes as a random password with a randomized salt value.
				rng.GetBytes(randomizedPassword, 0, 256);
				rng.GetNonZeroBytes(salt);
			}
			catch (Exception ex)
			{
				ExceptionLog.LogException(ex);
				randomizedPassword = null;
				salt = null;
			}
			rng.Dispose();

			if (randomizedPassword != null && salt != null)
			{
				// Derive the instance's cryptographic keys from the randomly generated values.
				Rfc2898DeriveBytes? keyGenerator = new Rfc2898DeriveBytes(randomizedPassword, salt, _iterations, HashAlgorithmName.SHA512);
				byte[] key = keyGenerator.GetBytes(32);
				byte[] iv = keyGenerator.GetBytes(16);
				keyGenerator.Dispose();
				keyGenerator = null;

				// Create the cryptographic engine.
				_aes = Aes.Create();
				_aes.Key = key;
				_aes.IV = iv;

				_encryptor = _aes.CreateEncryptor();
				_decryptor = _aes.CreateDecryptor();

				// Clear memory.
				Array.Clear(key, 0, key.Length);
				Array.Clear(iv, 0, iv.Length);
				Array.Clear(randomizedPassword, 0, randomizedPassword.Length);
				Array.Clear(salt, 0, salt.Length);
			}
		}
		/// <summary>
		/// Reads and returns the value from encrypted storage.
		/// </summary>
		/// <returns>
		/// A byte array containing the clear representation of the value in memory.
		/// </returns>
		private byte[]? ReadFromStorage()
		{
			byte[]? returnData = null;

			if (_storage != null && _storageLength > 0 && _decryptor != null && _aes != null)
			{
				// Create the stream and reader object(s).
				MemoryStream sourceStream = new MemoryStream(_storage);
				CryptoStream decryptionStream = new CryptoStream(sourceStream, _decryptor, CryptoStreamMode.Read);
				BinaryReader reader = new BinaryReader(decryptionStream);

				// Try to decrypt.
				byte[]? interimData;

				try
				{
					int length = (int)sourceStream.Length;
					interimData = reader.ReadBytes(length);
				}
				catch (Exception ex)
				{
					ExceptionLog.LogException(ex);
					interimData = null;
				}

				if (interimData != null)
				{
					// De-splice the bits for an added bit of fun.
					returnData = BitSplicer.UnSpliceBits(interimData);
					Array.Clear(interimData, 0, interimData.Length);
				}

				// Dispose and clear.
				reader.Dispose();
				decryptionStream.Dispose();
				sourceStream.Dispose();
					
			}
			return returnData;
		}
		/// <summary>
		/// Sets the value.
		/// </summary>
		/// <param name="value">
		/// Translates the provided value into a byte array, and then encrypts the content and stores in memory.
		/// </param>
		private void SetValue(T? value)
		{
			// Remove any old data.
			ClearStorage();

			// Translate the value to a byte array.
			byte[]? data = TranslateValueToBytes(value);

			if (data != null)
				WriteToStorage(data);
		}
		/// <summary>
		/// Translates the content currently being securely stored to a value.
		/// </summary>
		/// <returns>
		/// A value of <typeparamref name="T"/> being stored securely in memory, or the default if the operation
		/// fails.
		/// </returns>
		private T TranslateValue()
		{
			T? returnValue = default(T);

			byte[]? content = ReadFromStorage();
			if (content != null)
			{
				returnValue = TranslateValueFromBytes(content);
			}

			return returnValue!;
		}
		/// <summary>
		/// Writes the provided byte array to local storage.
		/// </summary>
		/// <param name="dataContentToSecure">The data content to secure.</param>
		private void WriteToStorage(byte[]? dataContentToSecure)
		{
			if (dataContentToSecure != null && _encryptor != null && _aes != null)
			{ 
				// Splice the bits for an added bit of fun.
				byte[]? contentToEncrypt = BitSplicer.SpliceBits(dataContentToSecure);
				if (contentToEncrypt != null)
				{ 
					// Create the stream and writer object(s).
					MemoryStream destinationStream = new MemoryStream(contentToEncrypt.Length * 2);
					CryptoStream encryptionStream = new CryptoStream(destinationStream, _encryptor, CryptoStreamMode.Write);
					BinaryWriter writer = new BinaryWriter(encryptionStream);
				
					try
					{ 
						// Attempt to write all data to the stream.
						writer.Write(contentToEncrypt);
						writer.Flush();
						encryptionStream.Flush();
						encryptionStream.FlushFinalBlock();

						// If successful, store the encrypted content in local memory.
						_storage = destinationStream.ToArray();
						_storageLength = _storage.Length;

					}
					catch (Exception ex)
					{
						ExceptionLog.LogException(ex);
						_storage = null;
						_storageLength = -1;
					}

					// Dispose.
					writer.Dispose();
					encryptionStream.Dispose();
					destinationStream.Dispose();
					Array.Clear(contentToEncrypt, 0, contentToEncrypt.Length);
				}
			}
		}
		#endregion
	}
}
