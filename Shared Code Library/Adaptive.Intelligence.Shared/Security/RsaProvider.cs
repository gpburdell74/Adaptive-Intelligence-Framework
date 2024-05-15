using Adaptive.Intelligence.Shared.Logging;
using System.Security.Cryptography;

namespace Adaptive.Intelligence.Shared.Security
{
	/// <summary>
	/// Provides a utility class for performing RSA cryptographic operations.
	/// </summary>
	/// <seealso cref="DisposableObjectBase" />
	/// <seealso cref="RSACryptoServiceProvider"/>
	/// <seealso cref="RSAParameters"/>
	public sealed class RsaProvider : DisposableObjectBase
	{
		#region Private Member Declarations
		/// <summary>
		/// The RSA provider instance to use.
		/// </summary>
		private RSACryptoServiceProvider? _provider;
		/// <summary>
		/// The RSA parameters instance containing the current key data.
		/// </summary>
		private RSAParameters? _currentKey;
		#endregion

		#region Constructor / Dispose Methods
		/// <summary>
		/// Initializes a new instance of the <see cref="RsaProvider"/> class.
		/// </summary>
		/// <remarks>
		/// This is the default constructor.
		/// </remarks>
		public RsaProvider()
		{
			_provider = new RSACryptoServiceProvider();
			_currentKey = _provider.ExportParameters(true);
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
				ClearKeyMemory();
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

			if (encryptedData != null && encryptedData.Length > 0 && _provider != null)
			{
				try
				{
					result = _provider.Decrypt(encryptedData, true);
				}
				catch (Exception ex)
				{
					ExceptionLog.LogException(ex);
				}
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
			{
				byte[]? encryptedBytes = null;
				try
				{
					encryptedBytes = Convert.FromBase64String(encryptedData);
				}
				catch (Exception ex)
				{
					ExceptionLog.LogException(ex);
					encryptedBytes = null;
				}

				if (encryptedBytes != null)
					return Decrypt(encryptedBytes);
				else
					return null;
			}
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

			if (clearData != null && clearData.Length > 0 && _provider != null)
			{
				try
				{
					result = _provider.Encrypt(clearData, true);
				}
				catch (Exception ex)
				{
					ExceptionLog.LogException(ex);
				}
			}

			return result;
		}
		#endregion

		#region Public Key-Related Methods / Functions
		/// <summary>
		/// Gets the RSA public key value for exporting to another client or consumer.
		/// </summary>
		/// <returns>
		/// A string containing the base-64 encoding byte array that contains the RSA public key
		/// for use by another client/user to encrypt data.
		/// </returns>
		public string? GetKeyValueForExport()
		{
			if (_currentKey == null)
				return null;
			else if (!_currentKey.HasValue || _currentKey.Value.Modulus == null || _currentKey.Value.Exponent == null)
				return null;
			else 
				return
					Convert.ToBase64String(_currentKey.Value.Modulus) +
					Convert.ToBase64String(_currentKey.Value.Exponent);
		}
		/// <summary>
		/// Gets the RSA private key value for storage and later re-importing.
		/// </summary>
		/// <returns>
		/// A string containing the base-64 encoding byte array that contains the RSA private key
		/// data.  This is to be imported at a later date to decrypt data that was encrypted
		/// with the associated public key.
		/// </returns>
		public string? GetPrivateKeyValueForStorage()
		{
			string? serialized = null;

			byte[]? data = SerializePrivateKey();
			if (data != null)
			{
				serialized = Convert.ToBase64String(data);
				Array.Clear(data, 0, data.Length);
			}
			return serialized;
		}
		/// <summary>
		/// Imports the public key from another provider as represented in the
		/// provided string data.
		/// </summary>
		/// <param name="keyData">
		/// A base-64 encoded string containing the concatenated modulus and exponent
		/// byte array(s).
		/// </param>
		public void ImportPublicKeyFromBase64String(string? keyData)
		{
			if (!string.IsNullOrEmpty(keyData) && keyData.Length > 4)
			{
				int length = keyData.Length - 4;

				// Exponent will be 4 characters (3 bytes).
				byte[] exponent = Convert.FromBase64String(keyData.Substring(length, 4));
				byte[] modulus = Convert.FromBase64String(keyData.Substring(0, length));
				ImportPublicKey(modulus, exponent);
			}
		}
		/// <summary>
		/// Imports the public key from another provider.
		/// </summary>
		/// <param name="modulus">
		/// A byte array containing the modulus data.
		/// </param>
		/// <param name="exponent">
		/// A byte array containing the exponent data.
		/// </param>
		public void ImportPublicKey(byte[]? modulus, byte[]? exponent)
		{
			if (modulus != null && exponent != null)
			{
				ClearKeyMemory();

				RSAParameters rsaParams = new RSAParameters
				{
					Modulus = modulus,
					Exponent = exponent
				};
				_provider?.ImportParameters(rsaParams);
				_currentKey = rsaParams;
			}
		}
		/// <summary>
		/// Sets the RSA private key value from the provided data.
		/// </summary>
		/// <param name="keyData">
		/// A byte array containing the concatenation of all the fields on the
		/// internal <see cref="RSAParameters"/> instance containing the key data.
		/// </param>
		public void SetPrivateKey(byte[] keyData)
		{
			// Remove the old item.
			ClearKeyMemory();

			using (MemoryStream stream = new MemoryStream(keyData))
			{
				using (BinaryReader reader = new BinaryReader(stream))
				{
					RSAParameters rsaParams = new RSAParameters
					{
						D = reader.ReadBytes(128),
						DP = reader.ReadBytes(64),
						DQ = reader.ReadBytes(64),
						Exponent = reader.ReadBytes(3),
						InverseQ = reader.ReadBytes(64),
						Modulus = reader.ReadBytes(128),
						P = reader.ReadBytes(64),
						Q = reader.ReadBytes(64)
					};
					_currentKey = rsaParams;
				}
			}

			_provider?.ImportParameters(_currentKey.Value);
		}
		/// <summary>
		/// Sets the RSA private key value from the provided data.
		/// </summary>
		/// <param name="keyData">
		/// A base-64 encoded string representing the byte array containing the concatenation of
		/// all the fields on the internal <see cref="RSAParameters"/> instance containing the key data.
		/// </param>
		public void SetPrivateKeyFromBase64String(string keyData)
		{
			byte[]? content = null;

			try
			{
				content = Convert.FromBase64String(keyData);
			}
			catch (ArgumentNullException ex)
			{
				ExceptionLog.LogException(ex);
			}
			catch (FormatException formatEx)
			{
				ExceptionLog.LogException(formatEx);
			}

			if (content != null)
			{
				SetPrivateKey(content);
				Array.Clear(content, 0, content.Length);
			}
		}
		/// <summary>
		/// Serializes the private key data into a single byte array.
		/// </summary>
		/// <returns>
		/// A byte array containing the concatenation of all the fields on the
		/// internal <see cref="RSAParameters"/> instance containing the key data.
		/// </returns>
		public byte[]? SerializePrivateKey()
		{
			byte[]? returnData = null;

			if (_currentKey != null && _currentKey.HasValue)
			{
				MemoryStream stream = new MemoryStream(16384);
				BinaryWriter writer = new BinaryWriter(stream);

				try
				{
					if (_currentKey.Value.D != null)
						writer.Write(_currentKey.Value.D);

					if (_currentKey.Value.DP != null)
						writer.Write(_currentKey.Value.DP);

					if (_currentKey.Value.DQ != null)
						writer.Write(_currentKey.Value.DQ);

					if (_currentKey.Value.Exponent != null)
						writer.Write(_currentKey.Value.Exponent);

					if (_currentKey.Value.InverseQ != null)
						writer.Write(_currentKey.Value.InverseQ);

					if (_currentKey.Value.Modulus != null)
						writer.Write(_currentKey.Value.Modulus);

					if (_currentKey.Value.P != null)
						writer.Write(_currentKey.Value.P);

					if (_currentKey.Value.Q != null)
						writer.Write(_currentKey.Value.Q);
					writer.Flush();
					returnData = stream.ToArray();
				}
				catch (Exception ex)
				{
					ExceptionLog.LogException(ex);
				}

				writer.Dispose();
				stream.Dispose();
			}
			return returnData;
		}
		#endregion

		#region Private Methods / Functions
		/// <summary>
		/// Clears the arrays in the encryption key parameter container.
		/// </summary>
		private void ClearKeyMemory()
		{
			if (_currentKey != null && _currentKey.HasValue)
			{
				if (_currentKey.Value.D != null)
					Array.Clear(_currentKey.Value.D, 0, _currentKey.Value.D.Length);

				if (_currentKey.Value.DP != null)
					Array.Clear(_currentKey.Value.DP, 0, _currentKey.Value.DP.Length);

				if (_currentKey.Value.DQ != null)
					Array.Clear(_currentKey.Value.DQ, 0, _currentKey.Value.DQ.Length);

				if (_currentKey.Value.Exponent != null)
					Array.Clear(_currentKey.Value.Exponent, 0, _currentKey.Value.Exponent.Length);

				if (_currentKey.Value.InverseQ != null)
					Array.Clear(_currentKey.Value.InverseQ, 0, _currentKey.Value.InverseQ.Length);

				if (_currentKey.Value.Modulus != null)
					Array.Clear(_currentKey.Value.Modulus, 0, _currentKey.Value.Modulus.Length);

				if (_currentKey.Value.P != null)
					Array.Clear(_currentKey.Value.P, 0, _currentKey.Value.P.Length);

				if (_currentKey.Value.Q != null)
					Array.Clear(_currentKey.Value.Q, 0, _currentKey.Value.Q.Length);
			}
		}
		#endregion
	}
}