using Adaptive.Intelligence.Shared.Logging;
using Adaptive.Intelligence.Shared.Properties;
using System.IO.Compression;
using System.Text;

namespace Adaptive.Intelligence.Shared.IO
{
	/// <summary>
	/// Provides static methods / functions for compressing and decompressing data.
	/// </summary>
	public static class AdaptiveCompression
	{
		#region Public Static Methods / Functions

		#region Compression
		/// <summary>
		/// Compresses the specified string into a byte array.
		/// </summary>
		/// <param name="sourceContent">
		/// The Unicode string to be compressed.
		/// </param>
		/// <returns>
		/// A byte array containing the compressed data.
		/// </returns>
		/// <exception cref="System.ArgumentNullException">sourceContent</exception>
		public static byte[] Compress(string sourceContent)
		{
			if (sourceContent == null)
				throw new ArgumentNullException(nameof(sourceContent));

			return Compress(Encoding.Unicode.GetBytes(sourceContent));
		}
		/// <summary>
		/// Compresses the specified source data into a byte array.
		/// </summary>
		/// <param name="sourceStream">
		/// An open, readable <see cref="MemoryStream"/> instance containing
		/// the content to be compressed.
		/// </param>
		/// <returns>
		/// A byte array containing the compressed data.
		/// </returns>
		/// <exception cref="System.ArgumentNullException">sourceStream</exception>
		/// <exception cref="System.ArgumentOutOfRangeException">Cannot read from source stream.</exception>
		public static byte[] Compress(MemoryStream sourceStream)
		{
			if (sourceStream == null)
				throw new ArgumentNullException(nameof(sourceStream));
			if (!sourceStream.CanRead)
				throw new ArgumentOutOfRangeException(nameof(sourceStream), Resources.ErrorStreamRead);

			return Compress(sourceStream.ToArray());
		}
		/// <summary>
		/// Compresses the specified byte array.
		/// </summary>
		/// <param name="sourceContent">
		/// The byte array containing the data to be compressed.
		/// </param>
		/// <returns>
		/// A byte array containing the compressed data.
		/// </returns>
		/// <exception cref="System.ArgumentNullException">sourceContent</exception>
		public static byte[] Compress(byte[] sourceContent)
		{
			if (sourceContent == null)
				throw new ArgumentNullException(nameof(sourceContent));

			byte[]? compressedData = null;

			// Create the streams.
			MemoryStream outputStream = new MemoryStream();
			GZipStream? compressionStream = CreateCompressionStream(outputStream);
			if (compressionStream != null)
			{
				// Perform the actual compression.
				if (WriteData(compressionStream, sourceContent))
				{
					compressionStream.Dispose();
					compressedData = outputStream.ToArray();
				}
				else
					compressionStream.Dispose();
			}

			// On failure, return the original content.
			if (compressedData == null)
			{
				compressedData = new byte[sourceContent.Length];
				Array.Copy(sourceContent, compressedData, sourceContent.Length);
			}
			outputStream.Dispose();

			return compressedData;
		}
		#endregion

		#region Decompression
		/// <summary>
		/// Decompresses the specified data stream.
		/// </summary>
		/// <param name="sourceContent">
		/// An open, readable <see cref="MemoryStream"/> instance containing the
		/// compressed data.
		/// </param>
		/// <returns>
		/// A byte array containing the uncompressed data.
		/// </returns>
		/// <exception cref="System.ArgumentNullException">sourceContent</exception>
		/// <exception cref="System.ArgumentOutOfRangeException">Cannot read from specified stream.</exception>
		public static byte[]? Decompress(MemoryStream sourceContent)
		{
			if (sourceContent == null)
				throw new ArgumentNullException(nameof(sourceContent));
			if (!sourceContent.CanRead)
				throw new ArgumentOutOfRangeException(nameof(sourceContent), Resources.ErrorStreamRead);

			byte[]? result = null;

			GZipStream? decompressionStream = CreateDecompressionStream(sourceContent);
			if (decompressionStream != null)
				result = DecompressContent(decompressionStream);

			return result;
		}
		/// <summary>
		/// Decompresses the specified data array.
		/// </summary>
		/// <param name="sourceContent">
		/// A byte array containing the compressed data.
		/// </param>
		/// <returns>
		/// A byte array containing the uncompressed data.
		/// </returns>
		/// <exception cref="ArgumentNullException">sourceContent</exception>
		public static byte[]? Decompress(byte[] sourceContent)
		{
			if (sourceContent == null)
				throw new ArgumentNullException(nameof(sourceContent));

			byte[]? result = null;

			using (MemoryStream inStream = new MemoryStream(sourceContent))
			{
				GZipStream? decompressionStream = CreateDecompressionStream(inStream);
				if (decompressionStream != null)
					result = DecompressContent(decompressionStream);
			}
			return result;
		}
		/// <summary>
		/// Decompresses the specified data array and writes it to the destination stream.
		/// </summary>
		/// <param name="sourceContent">
		/// A byte array containing the compressed data.
		/// </param>
		/// <returns>
		/// A byte array containing the uncompressed data.
		/// </returns>
		/// <exception cref="ArgumentNullException">sourceContent</exception>
		public static byte[]? Decompress(byte[] sourceContent, Stream destinationStream)
		{
			if (sourceContent == null)
				throw new ArgumentNullException(nameof(sourceContent));

			byte[]? result = null;

			using (MemoryStream inStream = new MemoryStream(sourceContent))
			{
				GZipStream? decompressionStream = CreateDecompressionStream(inStream);
				if (decompressionStream != null)
				{
					destinationStream.Write(DecompressContent(decompressionStream));
					destinationStream.Flush();
				}
			}
			return result;
		}
		#endregion

		#endregion

		#region Private Static Methods / Functions
		/// <summary>
		/// Attempts to create the compression stream.
		/// </summary>
		/// <param name="outputStream">
		/// The output <see cref="MemoryStream"/> to which the compressed data
		/// will be written.
		/// </param>
		/// <returns>
		/// A new <see cref="GZipStream"/> instance, or <b>null</b> if the operation
		/// fails.
		/// </returns>
		private static GZipStream? CreateCompressionStream(MemoryStream? outputStream)
		{
			GZipStream? compressionStream = null;

			if (outputStream != null)
			{
				try
				{
					compressionStream = new GZipStream(outputStream, CompressionLevel.Optimal, true);
				}
				catch (Exception ex)
				{
					ExceptionLog.LogException(ex);
				}
			}
			return compressionStream;
		}
		/// <summary>
		/// Attempts to create the de-compression stream.
		/// </summary>
		/// <param name="inputStream">
		/// The output <see cref="MemoryStream"/> from which the compressed data
		/// will be read.
		/// </param>
		/// <returns>
		/// A new <see cref="GZipStream"/> instance, or <b>null</b> if the operation
		/// fails.
		/// </returns>
		private static GZipStream? CreateDecompressionStream(MemoryStream? inputStream)
		{
			GZipStream? decompressionStream = null;
			if (inputStream != null)
			{
				try
				{
					decompressionStream = new GZipStream(inputStream, CompressionMode.Decompress);
				}
				catch (Exception ex)
				{
					ExceptionLog.LogException(ex);
				}
			}
			return decompressionStream;
		}
		/// <summary>
		/// Writes the original data to the compression stream.
		/// </summary>
		/// <param name="compressionStream">
		/// The <see cref="GZipStream"/> compression stream.
		/// </param>
		/// <param name="sourceContent">
		/// The original data to be compressed.
		/// </param>
		/// <returns>
		/// <b>true</b> if the operation is successful; otherwise,
		/// returns <b>false</b>.
		/// </returns>
		private static bool WriteData(GZipStream? compressionStream, byte[]? sourceContent)
		{
			bool success = false;
			if ((compressionStream != null) && (sourceContent != null))
			{
				try
				{
					compressionStream.Write(sourceContent, 0, sourceContent.Length);
					compressionStream.Flush();
					success = true;
				}
				catch (Exception ex)
				{
					ExceptionLog.LogException(ex);
				}
			}
			return success;
		}
		/// <summary>
		/// Decompresses the content from the provided stream.
		/// </summary>
		/// <param name="decompressionStream">
		/// The <see cref="GZipStream"/> decompression stream.
		/// </param>
		/// <returns>
		/// A byte array containing the uncompressed data, or <b>null</b>.
		/// </returns>
		private static byte[]? DecompressContent(GZipStream? decompressionStream)
		{
			byte[]? result = null;

			if (decompressionStream != null)
			{
				using (MemoryStream outputStream = new MemoryStream())
				{
					try
					{
						decompressionStream.CopyTo(outputStream);
						decompressionStream.Dispose();
						result = outputStream.ToArray();
					}
					catch (Exception ex)
					{
						ExceptionLog.LogException(ex);
					}
				}
			}
			return result;
		}
		#endregion
	}
}
