using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.Shared.IO;
using System.Security.Cryptography;

namespace Adaptive.Taz.IO
{
	/// <summary>
	/// Provides the reader mechanism for reading TAZ files from a clear (unencrypted) format.	
	/// </summary>
	/// <seealso cref="ExceptionTrackingBase" />
	/// <seealso cref="ITazContentReader" />
	public sealed class TazReader : ExceptionTrackingBase, ITazContentReader
	{
		#region Private Member Declarations		
		/// <summary>
		/// The SHA-512 hash provider instance.
		/// </summary>
		private SHA512? _hashProvider;
		/// <summary>
		/// The path in which the archive will be created.
		/// </summary>
		private string? _path;
		/// <summary>
		/// The file name.
		/// </summary>
		private string? _fileName;
		/// <summary>
		/// The output stream to write to.
		/// </summary>
		private FileStream? _sourceStream;
		/// <summary>
		/// The reader instance.
		/// </summary>
		private ISafeBinaryReader? _reader;
		#endregion

		#region Constructor / Dispose Methods
		/// <summary>
		/// Initializes a new instance of the <see cref="TazReader"/> class.
		/// </summary>
		/// <param name="pathAndFileName">
		/// A string containing the fully-qualified path and name of the archive file.
		/// </param>
		public TazReader(string pathAndFileName)
		{
			_path = Path.GetDirectoryName(pathAndFileName);
			_fileName = Path.GetFileName(pathAndFileName);
			_hashProvider = SHA512.Create();
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
				_hashProvider?.Dispose();
			}

			_hashProvider = null;
			_path = null;
			_fileName = null;
			base.Dispose(disposing);
		}
		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources asynchronously.
		/// </summary>
		/// <returns>
		/// A task that represents the asynchronous dispose operation.
		/// </returns>
		public async ValueTask DisposeAsync()
		{
			await Task.Yield();
			Dispose(true);
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets a value indicating whether this instance can be used to read data.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
		/// </value>
		public bool CanRead => _reader != null && _sourceStream != null && _sourceStream.CanRead;
		/// <summary>
		/// Gets the current position in the file being read.
		/// </summary>
		/// <value>
		/// A <see cref="long"/> specifying the index of current position in the file.
		/// </value>
		public long CurrentPosition
		{
			get
			{
				if (_sourceStream == null)
					return -1;
				else
					return _sourceStream.Position;
			}
		}
		#endregion

		#region Public Methods / Functions		
		/// <summary>
		/// Closes the file and all underlying readers and streams.
		/// </summary>
		/// <returns></returns>
		/// <exception cref="System.NotImplementedException"></exception>
		public async Task CloseFileAsync()
		{
			try
			{
				if (_reader != null)
					await _reader.DisposeAsync().ConfigureAwait(false);

				if (_sourceStream != null)
					await _sourceStream.DisposeAsync().ConfigureAwait(false);
			}
			catch(Exception ex)
			{
				Exceptions?.Add(ex);
			}
			_reader = null;
			_sourceStream = null;
		}
		/// <summary>
		/// Performs the initialization and other operations to prepare to read an archive file.
		/// </summary>
		/// <returns>
		/// A value indicating whether the operation was successful.
		/// </returns>
		public async Task<bool> InitializeFileAsync()
		{
			bool success = false;
			await Task.Yield();

			if (OpenFileForReading() && _sourceStream != null)
			{
				_reader = new SafeBinaryReader(_sourceStream);
				success = true;
			}
			else
			{
				_reader = null;
				_sourceStream = null;
			}
			return success;
		}
		/// <summary>
		/// Reads the next byte array from the underlying stream.
		/// </summary>
		/// <param name="position">A <see cref="long" /> value indicating the position at which to begin reading.</param>
		/// <returns>
		/// The byte array that was read, or <b>null</b> if the operation fails.
		/// </returns>
		/// <remarks>
		/// This method assumes the array is preceded by an integer length indicator.
		/// </remarks>
		public byte[]? ReadArray(long position)
		{
			byte[]? data = null;

			if (CanRead)
			{
				_sourceStream.Seek(position, SeekOrigin.Begin);
				data = ReadArray();
			}
			return data;
		}
		/// <summary>
		/// Reads the next byte array from the underlying stream.
		/// </summary>
		/// <remarks>
		/// This method assumes the array is preceded by an integer length indicator.
		/// </remarks>
		/// <returns>
		/// The byte array that was read, or <b>null</b> if the operation fails.
		/// </returns>
		public byte[]? ReadArray()
		{
			byte[]? data = null;

			if (_reader != null)
			{
				try
				{
					// Read the length indicator.
					int length = _reader.ReadInt32();

					// Attempt to read the array.
					if (length > 0)
					{
						data = _reader.ReadBytes(length);
					}
				}
				catch (Exception ex)
				{
					Exceptions?.Add(ex);
				}
			}
			return data;
		}
		/// <summary>
		/// Reads the directory content into memory.
		/// </summary>
		/// <param name="directoryStart">A <see cref="long" /> specifying the location in the file at which to read the directory data.</param>
		/// <returns>
		/// A <see cref="TazDirectory" /> instance that was read from the file, or <b>null</b> if the operation failed.
		/// </returns>
		public TazDirectory? ReadDirectory(long directoryStart)
		{
			TazDirectory? directory = null;

			if (CanRead)
			{
				// Ensure we have the latest header data.
				TazFileHeader header = ReadHeader();

				// Move to the directory location.
				_sourceStream.Seek(header.DirectoryStart, SeekOrigin.Begin);

				// Read the array length.
				int length = _reader.ReadInt32();

				// Read the directory.
				// Read the array.
				byte[] directoryData = _reader.ReadBytes(length);
				directory = new TazDirectory();
				directory.FromBytes(directoryData);
				ByteArrayUtil.Clear(directoryData);
			}
			return directory;
		}
		/// <summary>
		/// Reads the file header content into memory.
		/// </summary>
		/// <returns>
		/// A <see cref="TazFileHeader" /> instance that was read from the file, or <b>null</b> if the operation failed.
		/// </returns>
		public TazFileHeader? ReadHeader()
		{
			TazFileHeader? header = null;
			if (CanRead)
			{
				// Ensure we are at the start of the file.
				_sourceStream.Seek(0, SeekOrigin.Begin);

				// Write the content.
				header = TazFileHeader.ReadFromOpenFile(_reader);
			}

			return header;
		}
		#endregion

		#region Private Methods / Functions
		/// <summary>
		/// Opens the file for reading.
		/// </summary>
		/// <returns>
		/// <b>true</b> if the operation is successful; otherwise, returns <b>false</b>.
		/// </returns>
		private bool OpenFileForReading()
		{
			bool success = false;
			string fn = _path + "\\" + _fileName;

			if (!string.IsNullOrEmpty(fn))
			{
				// Try to open the file for reading.	
				try
				{
					_sourceStream = new FileStream(fn, FileMode.Open, FileAccess.Read);
					success = _sourceStream.CanRead;
				}
				catch
				{
					_sourceStream = null;
				}
			}

			return success;
		}
		#endregion

	}
}
