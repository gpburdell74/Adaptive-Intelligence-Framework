using Adaptive.Intelligence.Shared.Logging;
using System.Security.Cryptography;

namespace Adaptive.Intelligence.Shared.Security
{
	/// <summary>
	/// Provides a utility class for performing AES symmetric cryptographic operations.
	/// </summary>
	/// <seealso cref="DisposableObjectBase" />
	/// <seealso cref="AesCryptoServiceProvider"/>
	public sealed class AesProvider : DisposableObjectBase
	{
		#region Private Member Declarations
		/// <summary>
		/// The AES (Cryptography Next Generation) provider instance to use.
		/// </summary>
		private Aes? _provider;
		#endregion

		#region Constructor / Dispose Methods
		/// <summary>
		/// Initializes a new instance of the <see cref="AesProvider"/> class.
		/// </summary>
		/// <remarks>
		/// This is the default constructor.
		/// </remarks>
		public AesProvider()
		{
			_provider = AesCng.Create();
			_provider.BlockSize = 128;
			_provider.Mode = CipherMode.CBC;
			_provider.Padding = PaddingMode.PKCS7;
			_provider.GenerateKey();
			_provider.GenerateIV();
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
			}
			_provider = null;
			base.Dispose(disposing);
		}
		#endregion

		#region Public Cryptographic Methods / Functions
		/// <summary>
		/// Attempts to decrypt the provided data.
		/// </summary>
		/// <remarks>
		/// The key data must be imported into the current instance before this operation
		/// is used.  This method assumes OAEP padding is used.
		/// </remarks>
		/// <param name="encryptedData">
		/// A byte array containing the encrypted data.
		/// </param>
		/// <returns>
		/// A byte array containing the decrypted data, if successful; otherwise, returns
		/// <b>null</b>.
		/// </returns>
		public byte[]? Decrypt(byte[]? encryptedData)
		{
			byte[]? result = null;

			if (_provider != null && encryptedData != null)
			{
				// Create the stream and cryptographic actors.
				MemoryStream outStream = new MemoryStream(32767);
				ICryptoTransform decryptor = _provider.CreateDecryptor();
				CryptoStream stream = new CryptoStream(outStream, decryptor, CryptoStreamMode.Write);

				// Attempt to perform the operation.
				try
				{
					stream.Write(encryptedData, 0, encryptedData.Length);
					stream.Flush();
					stream.FlushFinalBlock();
				}
				catch (Exception ex)
				{
					ExceptionLog.LogException(ex);
				}

				// Get the return value.
				try
				{
					result = outStream.ToArray();
					stream.Dispose();
				}
				catch (Exception ex)
				{
					ExceptionLog.LogException(ex);
				}

				// Dispose.
				decryptor.Dispose();
				outStream.Dispose();
			}

			return result;
		}
		/// <summary>
		/// Attempts to decrypt the provided data.
		/// </summary>
		/// <remarks>
		/// The key data must be imported into the current instance before this operation
		/// is used.  This method assumes OAEP padding is used.
		/// </remarks>
		/// <param name="encryptedData">
		/// A base-64 encoded string representing the byte array containing the encrypted data.
		/// </param>
		/// <returns>
		/// A byte array containing the decrypted data, if successful; otherwise, returns
		/// <b>null</b>.
		/// </returns>
		public byte[]? DecryptFromBase64String(string? encryptedData)
		{
			if (!string.IsNullOrEmpty(encryptedData))
				return Decrypt(Convert.FromBase64String(encryptedData));
			else
				return null;
		}
		/// <summary>
		/// Attempts to encrypt the provided data.
		/// </summary>
		/// <remarks>
		/// This method assumes OAEP padding is used.
		/// </remarks>
		/// <param name="clearData">
		/// A byte array containing the clear data.
		/// </param>
		/// <returns>
		/// A byte array containing the encrypted data, if successful; otherwise, returns
		/// <b>null</b>.
		/// </returns>
		public byte[]? Encrypt(byte[]? clearData)
		{
			byte[]? result = null;

			if (_provider != null && clearData != null)
			{
				// Create the stream(s) and cryptographic actors.
				MemoryStream outStream = new MemoryStream(32767);
				ICryptoTransform encryptor = _provider.CreateEncryptor();
				CryptoStream stream = new CryptoStream(outStream, encryptor, CryptoStreamMode.Write);

				// Attempt to perform the operation.
				try
				{
					stream.Write(clearData, 0, clearData.Length);
					stream.Flush();
					stream.FlushFinalBlock();
					result = outStream.ToArray();
				}
				catch (Exception ex)
				{
					ExceptionLog.LogException(ex);
				}

				// Dispose.
				stream.Dispose();
				encryptor.Dispose();
				outStream.Dispose();
			}
			return result;
		}
		#endregion

		#region Public Key-Related Methods / Functions
		/// <summary>
		/// Gets the AES key and initialization vector values for exporting to another
		/// client or consumer.
		/// </summary>
		/// <returns>
		/// A string containing the base-64 encoding byte array that contains the key and
		/// initialization vector.
		/// </returns>
		public string? GetKey()
		{
			if (_provider != null)
			{
				byte[] keyData = ByteArrayUtil.CreatePinnedArray(48);
				Array.Copy(_provider.Key, 0, keyData, 0, 32);
				Array.Copy(_provider.IV, 0, keyData, 32, 16);
				string keyValue = Convert.ToBase64String(keyData);
				CryptoUtil.SecureClear(keyData);
				return keyValue;
			}
			else
				return null;
		}
		/// <summary>
		/// Gets the key and initialization vector data as a single byte array.
		/// </summary>
		/// <returns>
		/// A byte array concatenating the key value and the initialization vector value.
		/// </returns>
		public byte[]? GetKeyData()
		{
			byte[]? keyData = null;
			if (_provider != null)
			{
				keyData = ByteArrayUtil.CreatePinnedArray(48);
				Array.Copy(_provider.Key, 0, keyData, 0, 32);
				Array.Copy(_provider.IV, 0, keyData, 32, 16);
			}
			return keyData;
		}
		/// <summary>
		/// Generates a new encryption key and initialization vector.
		/// </summary>
		public void GenerateNewKey()
		{
			if (_provider == null)
			{
                _provider = AesCng.Create();
                _provider.BlockSize = 128;
				_provider.Mode = CipherMode.CBC;
				_provider.Padding = PaddingMode.PKCS7;
			}
			
			_provider.GenerateKey();
			_provider.GenerateIV();
		}
		/// <summary>
		/// Sets the initialization vector value.
		/// </summary>
		/// <param name="ivData">
		/// A 16-element byte array containing the initialization vector.
		/// </param>
		public void SetIV(byte[]? ivData)
		{
			if (_provider != null && ivData != null && ivData.Length == 16)
			{
				_provider.IV = ivData;
			}
		}
		/// <summary>
		/// Sets the cryptographic key value.
		/// </summary>
		/// <param name="keyData">
		/// A 32-element byte array containing the cryptographic key.
		/// </param>
		public void SetKey(byte[]? keyData)
		{
			if (_provider != null && keyData != null && keyData.Length == 32)
			{
				_provider.Key = keyData;
			}
		}
		/// <summary>
		/// Sets the key and initialization vector (IV) from the supplied byte array.
		/// </summary>
		/// <param name="keyData">
		/// A byte array containing the 32-byte key data followed by the 16-byte IV
		/// value.
		/// </param>
		public void SetKeyIV(byte[]? keyData)
		{
			if (keyData != null)
			{
				byte[] key = ByteArrayUtil.CreatePinnedArray(32);
				byte[] iv = ByteArrayUtil.CreatePinnedArray(16);

				Array.Copy(keyData, 0, key, 0, 32);
				Array.Copy(keyData, 32, iv, 0, 16);
				SetKey(key);
				SetIV(iv);

				CryptoUtil.SecureClear(key);
				CryptoUtil.SecureClear(iv);
			}
		}
		/// <summary>
		/// Sets the cryptographic key and initialization vector values from the provided
		/// key data.
		/// </summary>
		/// <param name="keyData">
		/// A base-64 encoded string containing the byte array where the first 32 elements of
		/// the byte array contain the cryptographic key, and the last 16 elements contain
		/// the initialize vector.
		/// </param>
		public void SetKeyIVFromBase64String(string? keyData)
		{
			if (keyData != null)
			{
				byte[] content = Convert.FromBase64String(keyData);
				byte[] key = ByteArrayUtil.CreatePinnedArray(32);
				byte[] iv = ByteArrayUtil.CreatePinnedArray(16);

				Array.Copy(content, 0, key, 0, 32);
				Array.Copy(content, 32, iv, 0, 16);

				SetKey(key);
				SetIV(iv);

				CryptoUtil.SecureClear(content);
				CryptoUtil.SecureClear(key);
				CryptoUtil.SecureClear(iv);
			}
		}
		#endregion
	}
}