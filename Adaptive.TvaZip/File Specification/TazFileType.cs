namespace Adaptive.Taz
{
	/// <summary>
	/// Lists the file sub-types that are currently supported.
	/// </summary>
	public enum TazFileType : byte
	{
		/// <summary>
		/// Indicates the archive file is not encrypted.
		/// </summary>
		Clear = FileSpecConstants.ClearTypeIndicator,
		/// <summary>
		/// Indicates the archive file is encrypted.
		/// </summary>
		Secure = FileSpecConstants.SecureTypeIndicator
	}
}
