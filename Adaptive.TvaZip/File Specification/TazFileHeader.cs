using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.Shared.IO;
using Adaptive.Taz.Interfaces;

namespace Adaptive.Taz
{
	/// <summary>
	/// Represents and manaes the file header for TAZ file.
	/// </summary>
	/// <seealso cref="ExceptionTrackingBase" />
	public sealed class TazFileHeader : ExceptionTrackingBase, IBinarySerializable
	{
		#region Private Member Declarations		
		/// <summary>
		/// The 3-byte TAZ file prefix.
		/// </summary>
		private byte[] _tazFilePrefix = new byte[3];
		/// <summary>
		/// The major version number.
		/// </summary>
		private int _majorVersion;
		/// <summary>
		/// The minor version number.
		/// </summary>
		private int _minorVersion;
		/// <summary>
		/// The TAZ File sub-type indicator value.
		/// </summary>
		private byte _fileTypeIndicator;
		/// <summary>
		/// The index of the byte in the file where the directory starts.
		/// </summary>
		private long _directoryStartPostion;
		#endregion

		#region Constructor / Dispose Methods		
		/// <summary>
		/// Initializes a new instance of the <see cref="TazFileHeader"/> class.
		/// </summary>
		/// <remarks>
		/// This is the default constructor.
		/// </remarks>
		public TazFileHeader() 
		{
			// Set the defaults.
			_tazFilePrefix = new byte[3];
			Array.Copy(FileSpecConstants.FileHeader, 0, _tazFilePrefix, 0, 3);
			_majorVersion = FileSpecConstants.FormatMajorVersion;
			_minorVersion = FileSpecConstants.FormatMinorVersion;
			_fileTypeIndicator = (byte)TazFileType.Clear;
			_directoryStartPostion = 0;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="TazFileHeader"/> class.
		/// </summary>
		/// <param name="isSecure">
		/// A value indicating whether the TAZ file is an encrypted file.
		/// </param>
		public TazFileHeader(bool isSecure)
		{
			// Set the defaults.
			_tazFilePrefix = new byte[3];
			Array.Copy(FileSpecConstants.FileHeader, 0, _tazFilePrefix, 0, 3);
			_minorVersion = FileSpecConstants.FormatMinorVersion;
			_directoryStartPostion = 0;
			if (isSecure)
				_fileTypeIndicator = (byte)TazFileType.Secure;
			else
				_fileTypeIndicator = (byte)TazFileType.Clear;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="TazFileHeader"/> class.
		/// </summary>
		/// <param name="partOne">
		/// A byte containing the 1st app ID indicator byte.
		/// </param>
		/// <param name="partTwo">
		/// A byte containing the 2nd app ID indicator byte.
		/// </param>
		/// <param name="partThree">
		/// A byte containing the 3rd app ID indicator byte.
		/// </param>
		/// <param name="major">
		/// An integer containing the major version number.
		/// </param>
		/// <param name="minor">
		/// An integer containing the minor version number.
		/// </param>
		/// <param name="type">
		/// A byte specifying the file sub-type indicator.
		/// </param>
		private TazFileHeader(byte partOne, byte partTwo, byte partThree, int major, int minor, byte type, long directoryStart)
		{
			_tazFilePrefix = new byte[3];
			_tazFilePrefix[0] = partOne;
			_tazFilePrefix[1] = partTwo;
			_tazFilePrefix[2] = partThree;
			_majorVersion = major;
			_minorVersion = minor;
			_directoryStartPostion = directoryStart;
			_fileTypeIndicator = type;
		}
		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
		/// <b>false</b> to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			ByteArrayUtil.Clear(_tazFilePrefix);
			base.Dispose(disposing);
		}
		#endregion

		#region Public Properties		
		/// <summary>
		/// Gets the reference to the three-byte application indicator.
		/// </summary>
		/// <value>
		/// A byte array containing the first 3 bytes of the file.
		/// </value>
		public byte[]? ApplicationIndicator => _tazFilePrefix;
		/// <summary>
		/// Gets or sets the directory startint position.
		/// </summary>
		/// <value>
		/// A <see cref="long"/> indicating the index of the byte in the file where the directory starts.
		/// </value>
		public long DirectoryStart
		{
			get=> _directoryStartPostion;
			set => _directoryStartPostion = value;
		}
		/// <summary>
		/// Gets the file format's major version number.
		/// </summary>
		/// <value>
		/// An integer indicating the version number.
		/// </value>
		public int FormatMajorVersion => _majorVersion;
		/// <summary>
		/// Gets the file format's minor version number.
		/// </summary>
		/// <value>
		/// An integer indicating the version number.
		/// </value>
		public int FileMinorVersion => _minorVersion;
		/// <summary>
		/// Gets the file type indicator.
		/// </summary>
		/// <value>
		/// An enumerated <see cref="TazFileType"/> value indicating the file sub-type.  This is used to 
		/// determine whether or not encryption is used.
		/// </value>
		public TazFileType FileTypeIndicator => (TazFileType)_fileTypeIndicator;
		/// <summary>
		/// Gets a value indicating whether the file is a secure (encrypted) file.
		/// </summary>
		/// <value>
		///   <c>true</c> if this header is for an encrypted TAZ file; otherwise, <c>false</c>.
		/// </value>
		public bool IsSecureFile
		{
			get => (_fileTypeIndicator== FileSpecConstants.SecureTypeIndicator);
		}
		/// <summary>
		/// Gets a value indicating whether the
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is taz file; otherwise, <c>false</c>.
		/// </value>
		public bool IsTazFile
		{
			get
			{
				// Ensure the correct 3 application indicator bytes were read.
				bool prefixIsOk =
					(
						(_tazFilePrefix != null && _tazFilePrefix.Length == 3) &&
						(
							(_tazFilePrefix[0] == FileSpecConstants.FileHeader[0] &&
								_tazFilePrefix[1] == FileSpecConstants.FileHeader[1] &&
								_tazFilePrefix[2] == FileSpecConstants.FileHeader[2]
							)
						)
					);

				// Ensure the file sub-type is correctly set.
				bool typeIsOk = false;
				if (prefixIsOk)
				{
					typeIsOk = _fileTypeIndicator == (byte)TazFileType.Secure ||
								_fileTypeIndicator == (byte)TazFileType.Clear;
				}

				return typeIsOk && prefixIsOk;
			}
		}
		#endregion

		#region Public Methods / Functions
		/// <summary>
		/// Populates the current instance from the provided byte array.
		/// </summary>
		/// <param name="data">A byte array containing the data for the instance, usually provided by <see cref="ToBytes" />.</param>
		public void FromBytes(byte[] data)
		{
			// Read the application indicator bytes.
			_tazFilePrefix[0] = data[0];
			_tazFilePrefix[1] = data[1];
			_tazFilePrefix[2] = data[2];

			// Extract the version numbers from the array.
			_majorVersion = BitConverter.ToInt32(data, 3);
			_majorVersion = BitConverter.ToInt32(data, 7);

			// Read the file sub-type.
			_fileTypeIndicator = data[11];

			// Read the directory start position.
			_directoryStartPostion = BitConverter.ToInt64(data, 12);
		}
		/// <summary>
		/// Converts the current instance to a byte array.
		/// </summary>
		/// <returns>
		/// A byte array containing the header data.
		/// </returns>
		public byte[] ToBytes()
		{
			byte[] content = new byte[FileSpecConstants.FileHeaderLength];

			// Write the application indicator bytes.
			content[0] = _tazFilePrefix[0];
			content[1] = _tazFilePrefix[1];
			content[2] = _tazFilePrefix[2];

			// Convert the major version to a 4-byte array and copy it.
			BitConverter.GetBytes(_majorVersion).CopyTo(content, 3);
			// Convert the minor version to a 4-byte array and copy it.
			BitConverter.GetBytes(_minorVersion).CopyTo(content, 7);

			// Set the file sub-type.
			content[11] = (byte)_fileTypeIndicator;

			// Convert the directory start position n to a 8-byte array and copy it.
			BitConverter.GetBytes(_directoryStartPostion).CopyTo(content, 12);

			return content;
		}
		#endregion

		#region Public Static Methods / Functions		
		/// <summary>
		/// Attempts to read the header data from a file that is not open.
		/// </summary>
		/// <param name="filePathAndName">
		/// A string containing the fully-qualified path and name of the file to read.
		/// </param>
		/// <returns>
		/// A <see cref="TazFileHeader"/> instance if the data is valid; otherwise, returns <b>null</b>.
		/// </returns>
		public static TazFileHeader? ReadFromClosedFile(string filePathAndName)
		{
			TazFileHeader? header = null;
			FileStream? fileStream = null;

			try
			{
				fileStream = new FileStream(filePathAndName, FileMode.Open, FileAccess.Read);
			}
			catch (Exception ex)
			{
				// Log exception.
				fileStream = null;
			}
			if (fileStream != null)
			{
				SafeBinaryReader reader = new SafeBinaryReader(fileStream);
				header = ReadFromOpenFile(reader);
				reader.Dispose();
				fileStream.Dispose();
			}
			return header;
		}
		/// <summary>
		/// Attempts to read the TAZ header from the current position in the file.
		/// </summary>
		/// <param name="reader">
		/// The <see cref="ISafeBinaryReader"/> instance used to perform the data read.
		/// </param>
		/// <returns>
		/// If sucessful, returns a <see cref="TazFileHeader"/> instance containing the header data;
		/// otherwise, returns <b>null</b>.
		/// </returns>
		public static TazFileHeader? ReadFromOpenFile(ISafeBinaryReader reader)
		{
			TazFileHeader? header = null;

			// Read the neccessary data.
			byte[]? data = reader.ReadBytes(FileSpecConstants.FileHeaderLength);

			// Only valid if 12 bytes can be read.
			if (data != null && data.Length == FileSpecConstants.FileHeaderLength)
			{
				header = new TazFileHeader(
					data[0], data[1], data[2],          // Application indictor.
					BitConverter.ToInt32(data, 3),     // Major version # (int32)
					BitConverter.ToInt32(data, 7),     // Minor version # (int32)
					data[11],                           // File Type indicator.
					BitConverter.ToInt64(data, 12)    // Directory start position.
				);
			}
			ByteArrayUtil.Clear(data);
			return header;
		}
		/// <summary>
		/// Writes the header to the open file.
		/// </summary>
		/// <param name="header">
		/// The <see cref="TazFileHeader"/> instance whose contents are to be written.
		/// </param>
		/// <param name="writer">
		/// The <see cref="ISafeBinaryWriter"/> implementation to use to write the content.
		/// </param>
		public static void WriteHeader(TazFileHeader header, ISafeBinaryWriter writer)
		{
			if (writer != null)
			{
				writer.Write(header.ToBytes());
				writer.Flush();
			}
		}
		#endregion

	}
}
