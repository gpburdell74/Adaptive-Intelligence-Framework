namespace Adaptive.Taz
{

	/* ************************************************************************************************************
	 * File Structure For TAZ Files:
	 * 
	 * [Header]							 12 bytes
	 * [Directory Location Indicator]	  8 bytes
	 * [Data Section]						(Variable)    <-- May or may not be secured with primary encryption keys.
	 * [Directory]							(Variable)	  <-- May or may not be secured with secondary encryption keys.	
	 * ************************************************************************************************************

	/* ************************************************************************************************************
	 * File Header For TAZ files:
	 *	'T'							1 byte
	 *  'A'							1 byte
	 *  'Z'						    1 byte
	 *  Major Version				4 bytes (int32)
	 *  Minor Version				4 bytes (int32)
	 *  File Type Indicator			1 byte
	 * ********************************************************************************************************* */

	/* ************************************************************************************************************
	 * File Structure For Clear TAZ Files:
	 * 
	 * [Header]								12 bytes
	 * [Directory Location Indicator]		 8 bytes
	 * [Data Section]						   (Variable)    
	 *		[File Entry Count]				 4 bytes (int 32)
	 *		[File Entry]
	 *			[Length Indicator]			 4 bytes (int 32)
	 *			[Compressed File Content]	 (Variable)
	 *		...
	 *		...
	 *		...
	 *		
	 * [Directory]							 (Variable)
	 *		[Directory Entry Count]			 4 bytes (int 32)
	 *		[Directory Entry]				(Variable)
	 *			[Original File Name]		(Variable Length String as byte array)
	 *			[Original File Path]		(Variable Length String as byte array)
	 *			[Original File size]		 8 bytes (int 64)
	 *			[Compressed Data Size]		 8 bytes (int 64)
	 *			[Position In File]			 8 bytes (int 64)
	 *			[SHA-512 Hash Of Original]	64 bytes
	 *		...
	 *		...
	 *		...
	 * ********************************************************************************************************** */

	/* ************************************************************************************************************
	 * File Structure For Secure TAZ Files:
	 * 
	 * [Header]								12 bytes
	 * [Directory Location Indicator]		 8 bytes
	 * [Data Section]						   (Variable)    
	 *		[File Entry Count]				 4 bytes (int 32)
	 *		[File Entry]												<-- Encrypted with primary key data.
	 *			[Length Indicator]			 4 bytes (int 32)			<-- Length of encrypted byte array.
	 *			[Encrypted Content]			(Variable)					<-- Encrypted with primary key data.
	 *				-- Decrypts Into: 
	 *					[Compressed File Content]	 (Variable)			<-- (no length indictor needed):
	 *		...
	 *		...
	 *		...
	 *		
	 * [Directory]							 (Variable)					<-- Encrypted with secondary key data.
	 *		[Directory Entry Count]			 4 bytes (int 32)
	 *		[Directory Entry]				(Variable)					<-- Encrypted with secondary key data.
	 *			[Length Indicator]			 4 bytes (int 32)
	 *			[Original File Name]		(Variable)	<-- Encrypted with secondary key data as byte array.
	 *			[Original File Path]		(Variable)	<-- Encrypted with secondary key data as byte array.
	 *			[Original File Size]		(Variable)	<-- Encrypted with secondary key data as byte array.
	 *			[Compressed Data Size]		(Variable)	<-- Encrypted with secondary key data as byte array.
	 *			[Position In File]			(Variable)	<-- Encrypted with secondary key data as byte array.
	 *			[SHA-512 Hash Of Original]	(Variable)	<-- Encrypted with secondary key data as byte array.
	 *		...
	 *		...
	 *		...
	 * ********************************************************************************************************** */


	/// <summary>
	/// Provides the constants that define areas within the TVA ZIP file specification.
	/// </summary>
	public static class FileSpecConstants
	{
		#region Public Static and Constants

		#region File Format Constants
		/// <summary>
		/// Specifies the first three bytes for a TAZ file. 
		/// </summary>
		public static byte[] FileHeader = new byte[] { 84, 65, 90 };

		/// <summary>
		/// Specifies the first three bytes for a TAZ file as a string.
		/// </summary>
		public const string FileHeaderText = "TAZ";
		/// <summary>
		/// The file extension for non-encrypted TAZ files.
		/// </summary>
		public const string ClearFileExtension = ".taz";
		/// <summary>
		/// The file extension for encrypted TAZ files.
		/// </summary>
		public const string SecureFileExtension = ".taz.secure";

		/// <summary>
		/// The file header size, in bytes.
		/// </summary>
		public const int AppHeaderSize = 3;
		/// <summary>
		/// The standard size of the file header.
		/// </summary>
		public const int FileHeaderLength = 20;
		/// <summary>
		/// The location of the directory index value in the file. Should always be written at the end of the file header.
		/// </summary>
		public const int DirectoryIndex = 12;
		/// <summary>
		/// The start of the data content after the file header and directory index.  (8 bytes for an int64 value). 
		/// </summary>
		public const int DataIndex = 20;

		/// <summary>
		/// The major version of the file format.
		/// </summary>
		public const int FormatMajorVersion = 1;
		/// <summary>
		/// The minor version of the file format.
		/// </summary>
		public const int FormatMinorVersion = 2;
		/// <summary>
		/// The byte in the header that indicates the TAZ file is encrypted.
		/// </summary>
		public const byte SecureTypeIndicator = (byte)41;
		/// <summary>
		/// The byte in the header that indicates the TAZ file is NOT encrypted.
		/// </summary>
		public const byte ClearTypeIndicator = (byte)42;

		/// <summary>
		/// The minimum user PIN size (as a string).
		/// </summary>
		public const int MinPinSize = 6;
		#endregion

		#region File Naming Constants		
		/// <summary>
		/// The extension for clear archive files.
		/// </summary>
		public const string ExtensionTaz = "taz";
		/// <summary>
		/// The extension for secure archive files.
		/// </summary>
		public const string ExtensionSecure = "secure";
		/// <summary>
		/// The extension for secure archive files.
		/// </summary>
		public const string ExtensionTazSecure = "taz.secure";
		/// <summary>
		/// The default clear extension.
		/// </summary>
		public const string DefaultClearExtension = ExtensionTaz;
		/// <summary>
		/// The default secure extension.
		/// </summary>
		public const string DefaultSecureExtension = ExtensionTaz;
		/// <summary>
		/// The file filter for opening any TAZ file.
		/// </summary>
		public const string FileFilter = "TAZ Files (*.taz)|*.taz|TAZ Secure Files (*.taz.secure)|*.taz.secure|All Files (*.*)|*.*";
		/// <summary>
		/// The file filter for opening a clear TAZ file.
		/// </summary>
		public const string FileFilterClearOnly = "TAZ Files (*.taz)|*.taz|All Files (*.*)|*.*";
		/// <summary>
		/// The file filter for opening a secure TAZ file.
		/// </summary>
		public const string FileFilterSecureOnly = "TAZ Secure Files (*.taz.secure)|*.taz.secure|All Files (*.*)|*.*";
		#endregion

		#endregion
	}
}
