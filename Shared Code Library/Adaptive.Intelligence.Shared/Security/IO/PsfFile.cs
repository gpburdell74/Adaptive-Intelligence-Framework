using Adaptive.Intelligence.Shared.IO;

namespace Adaptive.Intelligence.Shared.Security.IO
{
	/// <summary>
	/// Provides the class for reading and writing Personal Security File (PSF) files.
	/// </summary>
	/// <seealso cref="ExceptionTrackingBase" />
	public sealed class PsfFile : ExceptionTrackingBase
	{
		#region Private Member Declarations		
		/// <summary>
		/// The file stream to read from or write to.
		/// </summary>
		private FileStream? _fileStream;
		/// <summary>
		/// The writer instance.
		/// </summary>
		private PsfFileWriter? _writer;
		/// <summary>
		/// The reader instance.
		/// </summary>
		private PsfFileReader? _reader;
		#endregion

		#region Constructor / Dispose Methods		
		/// <summary>
		/// Initializes a new instance of the <see cref="PsfFile"/> class.
		/// </summary>
		/// <remarks>
		/// This is the default constructor.
		/// </remarks>
		public PsfFile()
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
				Close();

			base.Dispose(disposing);
		}
		#endregion

		#region Public Methods / Functions		
		/// <summary>
		/// Closes the file.
		/// </summary>
		public void Close()
		{
			// Dispose.
			_reader?.Dispose();
			_writer?.Dispose();
			_fileStream?.Dispose();

			// De-reference.
			_reader = null;
			_writer = null;
			_fileStream = null;
		}
		/// <summary>
		/// Opens a specified PSF file for writing data to.
		/// </summary>
		/// <param name="fileName">
		/// A string containing the fully-qualified path and name of the file to be written.
		/// </param>
		/// <param name="userId">
		/// A string containing the user identifier value or other such passkey value to use.
		/// </param>
		/// <param name="passCode">
		/// A string containing the password or other pass code value to use.
		/// </param>
		/// <param name="pinValue">
		/// An integer containing the PIN value to use.
		/// </param>
		/// <returns>
		/// <b>true</b> if the file is successfully opened and created; otherwise, returns <b>false</b>.
		/// If the function returns <b>false</b>, check the <see cref="Exceptions"/> property.
		/// </returns>
		public bool OpenForWriting(string fileName, string userId, string passCode, int pinValue)
		{
			// 1. Ensure any old file is deleted first.
			// 
			if (SafeIO.FileExists(fileName))
				SafeIO.DeleteFile(fileName);

			// 2. Open the file stream.
			try
			{
				_fileStream = new FileStream(fileName, FileMode.CreateNew, FileAccess.Write);
			}
			catch (Exception ex)
			{
				Exceptions.Add(ex);
				_fileStream = null;
			}

			// 3. Create and prepare the writer.
			if (_fileStream != null)
			{
				_writer = new PsfFileWriter(_fileStream);
				_writer.Open(userId, passCode, pinValue);
			}

			return (_fileStream != null);
		}
		/// <summary>
		/// Opens a specified PSF file for reading data from.
		/// </summary>
		/// <param name="fileName">
		/// A string containing the fully-qualified path and name of the file to be read.
		/// </param>
		/// <param name="userId">
		/// A string containing the user identifier value or other such passkey value to use.
		/// </param>
		/// <param name="passCode">
		/// A string containing the password or other pass code value to use.
		/// </param>
		/// <param name="pinValue">
		/// An integer containing the PIN value to use.
		/// </param>
		/// <returns>
		/// <b>true</b> if the file is successfully opened and can be read from; otherwise, returns <b>false</b>.
		/// If the function returns <b>false</b>, check the <see cref="Exceptions"/> property.
		/// </returns>
		public bool OpenForReading(string fileName, string userId, string passCode, int pinValue)
		{
			// 1. Ensure any the file exists.
			if (SafeIO.FileExists(fileName))
			{
				// 2. Open the file stream.
				try
				{
					_fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
				}
				catch (Exception ex)
				{
					Exceptions.Add(ex);
					_fileStream = null;
				}

				// 3. Create and prepare the reader.
				if (_fileStream != null)
				{
					_reader = new PsfFileReader(_fileStream);
					_reader.Open(userId, passCode, pinValue);
				}
			}

			return (_fileStream != null);
		}
		/// <summary>
		/// Attempts to read the content of the file.
		/// </summary>
		/// <returns>
		/// A <see cref="MemoryStream"/> containing the data from the file, if successful;
		/// otherwise, returns <b>null</b>.
		/// </returns>
		public MemoryStream? Read()
		{
			MemoryStream? clearData = null;

			if (_reader != null)
			{
				MemoryStream? compressedData = _reader.Read();
				if (compressedData != null)
				{
					clearData = UncompressStream(compressedData);
					compressedData.Dispose();
				}
			}
			return clearData;
		}
		/// <summary>
		/// Attempts the write the content of the source stream to the file.
		/// </summary>
		/// <param name="sourceStream">
		/// The source <see cref="Stream"/> instance whose contents are to be written to the PSF file.
		/// </param>
		public void Write(Stream sourceStream)
		{
			if (_writer != null)
			{
				// 1. Compress the data.
				MemoryStream compressed = PrepStream(sourceStream);

				// 2. Encrypt and write the compressed content.
				_writer.Write(compressed);

				// 3. Clear.
				compressed.Dispose();
			}
		}
		#endregion

		#region Private Methods / Functions		
		/// <summary>
		/// Preps the data from the original stream for the writing process.
		/// </summary>
		/// <param name="sourceStream">The source <see cref="Stream"/> instance to be read from.
		/// </param>
		/// <returns>
		/// The <see cref="MemoryStream"/> containing the compressed data content to be written.
		/// </returns>
		private static MemoryStream PrepStream(Stream sourceStream)
		{
			// Read from the original stream.
			BinaryReader reader = new BinaryReader(sourceStream);
			byte[] clear = reader.ReadBytes((int)sourceStream.Length);
			reader.Dispose();

			// Compress the data and splice the bits.
			byte[] compressed = AdaptiveCompression.Compress(clear);
			MemoryStream readyStream = new MemoryStream(BitSplicer.SpliceBits(compressed)!);

			// Clear memory and return.
			CryptoUtil.SecureClear(compressed);
			CryptoUtil.SecureClear(clear);
			return readyStream;
		}
		/// <summary>
		/// Decompresses the original content for processing.
		/// </summary>
		/// <param name="compressedData">
		/// A <see cref="MemoryStream"/> containing the compressed data.
		/// </param>
		/// <returns>
		/// A <see cref="MemoryStream"/> containing the decompressed data.
		/// </returns>
		private static MemoryStream? UncompressStream(MemoryStream compressedData)
		{
			MemoryStream? uncompressed = null;

			// Un-splice the bits.
			byte[] spliced = compressedData.ToArray();
			byte[]? unspliced = BitSplicer.UnSpliceBits(spliced);
			if (unspliced != null)
			{
				byte[]? decompressed = AdaptiveCompression.Decompress(unspliced);
				CryptoUtil.SecureClear(spliced);
				CryptoUtil.SecureClear(unspliced);

				// De-compressed the data.
				if (decompressed != null)
					uncompressed = new MemoryStream(decompressed);
			}
			return uncompressed;
		}
		#endregion
	}
}