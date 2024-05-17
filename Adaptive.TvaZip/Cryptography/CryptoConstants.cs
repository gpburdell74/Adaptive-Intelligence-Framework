namespace Adaptive.Taz.Cryptography
{
	/// <summary>
	/// Provides constants definitions for cryptographic operations.
	/// </summary>
	internal static class CryptoConstants
	{
		/// <summary>
		/// The hash algorithm parameter value for SHA-512.
		/// </summary>
		public const string AlgorithmSHA512 = "SHA512";
		/// <summary>
		/// The Crypto-Derive Key algorithm name to use.
		/// </summary>
		public const string DeriveKeyAlgorithm = "TripleDES";
		/// <summary>
		/// The numer of iterations to use.
		/// </summary>
		public const int Iterations = 10240;
		/// <summary>
		/// The size of the initialization vector for CryptoDeriveKey operations.
		/// </summary>
		public const int IVSize = 192;
		/// <summary>
		/// The first multiplier to apply to a PIN number.
		/// </summary>
		public const int PinMultiplierOne = 1;
		/// <summary>
		/// The second multiplier to apply to a PIN number.
		/// </summary>
		public const int PinMultiplierTwo = 3;
		/// <summary>
		/// The third multiplier to apply to a PIN number.
		/// </summary>
		public const int PinMultiplierThree = 13;

	}
}
