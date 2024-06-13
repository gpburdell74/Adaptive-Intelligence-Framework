using Adaptive.Intelligence.Shared.IO;
using System.IO.Compression;
using System.Text;

namespace Adaptive.Intelligence.Shared.Tests.IO
{
	public class SafeIOTests
	{
		#region Copy File
		[Fact]
		public void CopyFile_FileExists_CopiesSuccessfully()
		{
			// Arrange
			string originalFile = Path.GetTempFileName();
			string newFile = originalFile + ".copy";

			// Act
			bool result = SafeIO.CopyFile(originalFile, newFile);

			// Assert
			Assert.True(result);
			Assert.True(File.Exists(newFile));

			// Cleanup
			File.Delete(originalFile);
			File.Delete(newFile);
		}
		[Fact]
		public void CopyFile_OriginalFileDoesNotExist_ReturnsFalse()
		{
			// Arrange
			string originalFile = Path.GetRandomFileName(); // Ensuring the file does not exist
			string newFile = Path.GetTempFileName();

			// Act
			bool result = SafeIO.CopyFile(originalFile, newFile);

			// Assert
			Assert.False(result);

			// Cleanup
			File.Delete(newFile); // Cleanup created temp file
		}

		[Fact]
		public void CopyFile_NewFileAlreadyExists_ReturnsFalse()
		{
			// Arrange
			string originalFile = Path.GetTempFileName();
			string newFile = Path.GetTempFileName(); // New file already exists

			// Act
			bool result = SafeIO.CopyFile(originalFile, newFile);

			// Assert
			Assert.False(result);

			// Cleanup
			File.Delete(originalFile);
			File.Delete(newFile);
		}

		[Fact]
		public void CopyFile_OriginalFileInUse_ReturnsFalse()
		{
			// Arrange
			string originalFile = Path.GetTempFileName();
			string newFile = originalFile + ".copy";

			// Using FileStream to keep the original file in use
			using (FileStream stream = new FileStream(originalFile, FileMode.Open, FileAccess.Read, FileShare.None))
			{
				// Act
				bool result = SafeIO.CopyFile(originalFile, newFile);

				// Assert
				Assert.False(result);
			}

			// Cleanup
			File.Delete(originalFile);
			// No need to delete newFile as the operation should not succeed
		}
		#endregion

		#region DecompressGZFileAsync

		[Fact]
		public async Task DecompressGZFileAsync_ValidGZFile_DecompressesSuccessfully()
		{
			// Arrange
			string originalFile = Path.GetTempFileName();
			string compressedFile = originalFile + ".gz";
			string outputFile = originalFile + ".out";

			// Create a compressed file
			using (var originalFileStream = File.Create(originalFile))
			using (var compressedFileStream = File.Create(compressedFile))
			using (var compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
			{
				byte[] data = Encoding.UTF8.GetBytes("Hello, World!");
				originalFileStream.Write(data, 0, data.Length);
				originalFileStream.Flush();
				compressionStream.Write(data, 0, data.Length);
			}

			// Act
			bool result = await SafeIO.DecompressGZFileAsync(compressedFile, outputFile, deleteOriginal: true);

			// Assert
			Assert.True(result);
			Assert.True(File.Exists(outputFile));
			Assert.False(File.Exists(compressedFile)); // Original should be deleted

			// Cleanup
			File.Delete(originalFile);
			File.Delete(outputFile);
		}
		[Fact]
		public async Task DecompressGZFileAsync_InputFileDoesNotExist_ReturnsFalse()
		{
			// Arrange
			string inputFile = Path.GetRandomFileName(); // Ensuring the file does not exist
			string outputFile = Path.GetTempFileName();

			// Act
			bool result = await SafeIO.DecompressGZFileAsync(inputFile, outputFile, deleteOriginal: false);

			// Assert
			Assert.False(result);

			// Cleanup
			File.Delete(outputFile); // Cleanup created temp file
		}
		[Fact]
		public async Task DecompressGZFileAsync_InvalidGZFile_ReturnsFalse()
		{
			// Arrange
			string inputFile = Path.GetTempFileName();
			string outputFile = Path.GetTempFileName() + ".out";
			// Create an invalid GZ file
			File.WriteAllText(inputFile, "Invalid GZ content");

			// Act
			bool result = await SafeIO.DecompressGZFileAsync(inputFile, outputFile, deleteOriginal: true);

			// Assert
			Assert.False(result);
			Assert.True(File.Exists(inputFile)); // Input file should still exist.
			Assert.False(File.Exists(outputFile)); // Outpit file should not exist.

			// Cleanup
			File.Delete(inputFile);
			File.Delete(outputFile);
		}
		[Fact]
		public async Task DecompressGZFileAsync_DeleteOriginalFalse_OriginalFileNotDeleted()
		{
			// Arrange
			string originalFile = Path.GetTempFileName();
			string compressedFile = originalFile + ".gz";
			string outputFile = originalFile + ".out";

			// Create a valid compressed file
			using (var originalFileStream = File.Create(originalFile))
			using (var compressedFileStream = File.Create(compressedFile))
			using (var compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
			{
				byte[] data = Encoding.UTF8.GetBytes("Sample data for testing.");
				originalFileStream.Write(data, 0, data.Length);
				compressionStream.Write(data, 0, data.Length);
			}

			// Act
			bool result = await SafeIO.DecompressGZFileAsync(compressedFile, outputFile, deleteOriginal: false);

			// Assert
			Assert.True(result);
			Assert.True(File.Exists(compressedFile)); // Original file should still exist

			// Cleanup
			File.Delete(originalFile);
			File.Delete(compressedFile);
			File.Delete(outputFile);
		}
		#endregion

		#region DeleteAllFilesInDirectory
		[Fact]
		public void DeleteAllFilesInDirectory_ValidDirectory_DeletesAllFiles()
		{
			// Arrange
			string directoryPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
			Directory.CreateDirectory(directoryPath);
			// Create some temporary files in the directory
			for (int i = 0; i < 5; i++)
			{
				File.Create(Path.Combine(directoryPath, $"tempFile{i}.txt")).Dispose();
			}

			// Act
			bool result = SafeIO.DeleteAllFilesInDirectory(directoryPath);

			// Assert
			Assert.True(result);
			Assert.Empty(Directory.GetFiles(directoryPath));

			// Cleanup
			Directory.Delete(directoryPath, true);
		}
		[Fact]
		public void DeleteAllFilesInDirectory_DirectoryDoesNotExist_ReturnsFalse()
		{
			// Arrange
			string directoryPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

			// Act
			bool result = SafeIO.DeleteAllFilesInDirectory(directoryPath);

			// Assert
			Assert.False(result);
		}
		[Fact]
		public void DeleteAllFilesInDirectory_DirectoryIsEmpty_ReturnsTrue()
		{
			// Arrange
			string directoryPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
			Directory.CreateDirectory(directoryPath);

			// Act
			bool result = SafeIO.DeleteAllFilesInDirectory(directoryPath);

			// Assert
			Assert.True(result);

			// Cleanup
			Directory.Delete(directoryPath, true);
		}
		#endregion

		#region GetAppPath Tests

		[Fact]
		public void GetAppPath_ReturnsValidPath()
		{
			// Act
			var appPath = SafeIO.GetAppPath();

			// Assert
			Assert.NotNull(appPath);
			Assert.True(Directory.Exists(appPath));
		}
		#endregion

		[Fact]
		public void FileExists_FileDoesNotExist_ReturnsFalse()
		{
			// Arrange
			string pathAndFileName = Path.GetRandomFileName();

			// Act
			bool exists = SafeIO.FileExists(pathAndFileName);

			// Assert
			Assert.False(exists);
		}

		[Fact]
		public void GetFileSize_KnownSizeFile_ReturnsCorrectSize()
		{
			// Arrange
			string fileName = Path.GetTempFileName();
			string content = "Hello, World!";
			File.WriteAllText(fileName, content);

			// Act
			int size = SafeIO.GetFileSize(fileName);

			// Assert
			Assert.Equal(content.Length, size);

			// Cleanup
			File.Delete(fileName);
		}
	}
}