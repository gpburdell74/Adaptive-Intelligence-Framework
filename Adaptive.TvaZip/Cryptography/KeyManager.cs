using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.Shared.Security;

namespace Adaptive.Taz.Cryptography
{
	/// <summary>
	/// Manages the key data for user-based cryptographic operations.
	/// </summary>
	/// <seealso cref="ExceptionTrackingBase" />
	public sealed class KeyManager : ExceptionTrackingBase
	{
		#region Private Member Declarations		
		/// <summary>
		/// The key data container.
		/// </summary>
		private OpsKeyContainer? _container;
		#endregion

		#region Constructor / Dispose Methods		
		/// <summary>
		/// Initializes a new instance of the <see cref="KeyManager"/> class.
		/// </summary>
		/// <remarks>
		/// This is the default constructor.
		/// </remarks>
		public KeyManager()
		{
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
				_container?.Dispose();
			}

			_container = null;
			base.Dispose(disposing);
		}
		#endregion

		#region Public Properties		
		/// <summary>
		/// Gets a value indicating whether this instance has been initialized with user data.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is initialized and the key manager is present; otherwise, <c>false</c>.
		/// </value>
		public bool IsInitialized => _container != null;
		#endregion

		#region Public Methods / Functions				
		/// <summary>
		/// Creates the AES cryptographic provider instance with the key data currently stored in the key manager.
		/// </summary>
		/// <returns>
		/// An <see cref="AesProvider"/> initialized to use the key manager's key data.
		/// </returns>
		public AesProvider? CreateAesProvider()
		{
			AesProvider? provider = null;
			if (_container != null)
			{
				provider = new AesProvider();
				provider.SetKey(_container.Key.Key);
				provider.SetIV(_container.Key.IV);
			}
			return provider;
		}
		/// <summary>
		/// Creates the AES cryptographic provider instance with the key variant data currently stored in the key manager.
		/// </summary>
		/// <returns>
		/// An <see cref="AesProvider"/> initialized to use the key manager's key variant data.
		/// </returns>
		public AesProvider? CreateAesProviderForVariant()
		{
			AesProvider? provider = null;
			if (_container != null)
			{
				provider = new AesProvider();
				provider.SetKey(_container.Variant.Key);
				provider.SetIV(_container.Variant.IV);
			}
			return provider;
		}
		/// <summary>
		/// Sets the content of the key manager to the specified user parameters.
		/// </summary>
		/// <param name="userId">The user identifier.</param>
		/// <param name="password">The password.</param>
		/// <param name="userPIN">The user pin.</param>
		public void SetForUser(string userId, string password, string userPIN)
		{
			// Remove old data.
			_container?.Dispose();

			// Generate user key data.
			KeyGenerator generator = new KeyGenerator();
			_container = generator.GenerateUserKeyData(userId, password, userPIN);
			generator.Dispose();
		}
		/// <summary>
		/// Resets this instance to an empty state.
		/// </summary>
		public void Reset()
		{
			_container?.Dispose();
			_container = null;
		}
		#endregion
	}
}
