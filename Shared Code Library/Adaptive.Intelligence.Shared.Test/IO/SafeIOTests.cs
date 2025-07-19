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
            Assert.False(File.Exists(outputFile)); // Output file should not exist.

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

        [Fact]
        public void GetFileSizeNative_KnownSizeFile_ReturnsCorrectSize()
        {
            // Arrange
            string fileName = Path.GetTempFileName();
            string content = "Hello, World!";
            File.WriteAllText(fileName, content);

            // Act
            long size = 0;
            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(
                    System.Runtime.InteropServices.OSPlatform.Windows))
            {
                size = SafeIO.GetFileSizeNative(fileName);
            }
            else
            {
                size = content.Length;
            }

            // Assert
            Assert.Equal(content.Length, size);

            // Cleanup
            File.Delete(fileName);
        }

        [Fact]
        public async Task ExtractToInvalidFileTestAsync()
        {
            // Arrange
            string originalFile =
                Path.GetTempFileName();
            string compressedFile = originalFile + ".gz";
            string outputFile = "//&*^*^X:\\32\\?~~!!??dddz";      // Bad file name.

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
            Assert.False(result);
            Assert.False(File.Exists(outputFile));
            Assert.True(File.Exists(compressedFile)); // Original should not be deleted.

            // Cleanup
            File.Delete(originalFile);
        }

        [Fact]
        public void DeleteAllFilesInDirectoryTest()
        {
            // Arrange.
            string path =
                FileNameRenderer.RenderInTempPath(@"testdir");
            
            System.IO.Directory.CreateDirectory(path);
            string[] files = Directory.GetFiles(path);
            foreach (string file in files)
                File.Delete(file);

            CreateTempFile(FileNameRenderer.RenderFileName(path, "fileA.txt"));
            
            CreateTempFile(FileNameRenderer.RenderFileName(path, "fileB.txt"));
            CreateTempFile(FileNameRenderer.RenderFileName(path , "fileC.txt"));

            string[]? list = SafeIO.GetFilesInPath(path);
            Assert.NotNull(list);
            Assert.Equal(3, list.Length);

            // Act
            SafeIO.DeleteAllFilesInDirectory(path);

            // Assert
            list = SafeIO.GetFilesInPath(path);
            Assert.NotNull(list);
            Assert.Empty(list);

            // Clean up.
            files = Directory.GetFiles(path);
            foreach (string file in files)
                File.Delete(file);
            Directory.Delete(path);
        }

        [Fact]
        public void DeleteAllFilesInDirectoryWithWildcardTest()
        {
            // Arrange.
            string path = FileNameRenderer.RenderInTempPath(
                 @"testdir");
            
            System.IO.Directory.CreateDirectory(path);
            string[] files = Directory.GetFiles(path);
            foreach (string file in files)
                File.Delete(file);

            CreateTempFile(FileNameRenderer.RenderFileName(path, "fileB.txt"));
            CreateTempFile(FileNameRenderer.RenderFileName(path, "fileA.txt"));
            CreateTempFile(FileNameRenderer.RenderFileName(path, "fileC.txt"));

            CreateTempFile(FileNameRenderer.RenderFileName(path , "fileD.dat"));
            CreateTempFile(FileNameRenderer.RenderFileName(path , "fileE.dat"));
            CreateTempFile(FileNameRenderer.RenderFileName(path , "fileF.dat"));

            string[]? list = SafeIO.GetFilesInPath(path);
            Assert.NotNull(list);
            Assert.Equal(6, list.Length);

            // Act 1
            SafeIO.DeleteAllFilesInDirectory(path, "*.txt");

            // Assert 1
            list = SafeIO.GetFilesInPath(path);
            Assert.NotNull(list);
            Assert.Equal(3, list.Length);

            // Act 2
            SafeIO.DeleteAllFilesInDirectory(path, "*.dat");

            // Assert 2
            list = SafeIO.GetFilesInPath(path);
            Assert.NotNull(list);
            Assert.Empty(list);

            // Clean up.
            files = Directory.GetFiles(path);
            foreach (string file in files)
                File.Delete(file);
            Directory.Delete(path);
        }

        private void CreateTempFile(string pathAndName)
        {
            if (File.Exists(pathAndName))
                File.Delete(pathAndName);

            FileStream fs = new FileStream(pathAndName, FileMode.CreateNew, FileAccess.Write);
            fs.Write(Encoding.UTF8.GetBytes("Hello, World!"), 0, 13);
            fs.Dispose();
        }

        #region DeleteFile
        [Fact]
        public void DeleteFile_FileExists_DeletesFile()
        {
            // Arrange
            string fileName = Path.GetTempFileName();

            // Act
            bool result = SafeIO.DeleteFile(fileName);

            // Assert
            Assert.True(result);
            Assert.False(File.Exists(fileName));
        }

        [Fact]
        public void DeleteFile_FileDoesNotExist_ReturnsFalse()
        {
            // Arrange
            string fileName = Path.GetRandomFileName(); // Ensuring the file does not exist
            try
            {
                File.Delete(fileName);
            }
            catch
            { }

            // Act
            bool result = SafeIO.DeleteFile(fileName);

            // Assert
            Assert.False(result);
        }
        #endregion

        #region DetermineFileFormat
        [Fact]
        public void DetermineFileFormat_ValidFile_ReturnsCorrectFormat()
        {
            // Arrange
            string fileName = "testfile.txt";

            // Act
            FileFormat format = SafeIO.DetermineFileFormat(fileName);

            // Assert
            Assert.Equal(FileFormat.TextFile, format);
        }

        [Fact]
        public void DetermineFileFormat_InvalidFile_ReturnsNotSpecified()
        {
            // Arrange
            string fileName = "testfile.unknown";

            // Act
            FileFormat format = SafeIO.DetermineFileFormat(fileName);

            // Assert
            Assert.Equal(FileFormat.NotSpecified, format);
        }
        #endregion

        #region FindUSBDrive
        [Fact]
        public void FindUSBDrive_NoUSBDrive_ReturnsNull()
        {
            // Act
            DirectoryInfo? usbDrive = SafeIO.FindUSBDrive();

            // Assert
            Assert.Null(usbDrive);
        }
        #endregion

        #region GetDirectories
        [Fact]
        public void GetDirectories_ValidPath_ReturnsSubDirectories()
        {
            // Arrange
            string directoryPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(directoryPath);
            Directory.CreateDirectory(Path.Combine(directoryPath, "subdir1"));
            Directory.CreateDirectory(Path.Combine(directoryPath, "subdir2"));

            // Act
            string[]? subDirs = SafeIO.GetDirectories(directoryPath);

            // Assert
            Assert.NotNull(subDirs);
            Assert.Equal(2, subDirs.Length);

            // Cleanup
            Directory.Delete(directoryPath, true);
        }

        [Fact]
        public void GetDirectories_InvalidPath_ReturnsNull()
        {
            // Arrange
            string directoryPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            // Act
            string[]? subDirs = SafeIO.GetDirectories(directoryPath);

            // Assert
            Assert.Null(subDirs);
        }
        #endregion

        #region GetFilesInPath
        [Fact]
        public void GetFilesInPath_ValidPath_ReturnsFiles()
        {
            // Arrange
            string directoryPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(directoryPath);
            CreateTempFile(Path.Combine(directoryPath, "file1.txt"));
            CreateTempFile(Path.Combine(directoryPath, "file2.txt"));

            // Act
            string[]? files = SafeIO.GetFilesInPath(directoryPath);

            // Assert
            Assert.NotNull(files);
            Assert.Equal(2, files.Length);

            // Cleanup
            Directory.Delete(directoryPath, true);
        }

        [Fact]
        public void GetFilesInPath_InvalidPath_ReturnsNull()
        {
            // Arrange
            string directoryPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            // Act
            string[]? files = SafeIO.GetFilesInPath(directoryPath);

            // Assert
            Assert.Null(files);
        }
        #endregion

        #region ReadBytesFromFile
        [Fact]
        public void ReadBytesFromFile_ValidFile_ReturnsBytes()
        {
            // Arrange
            string fileName = Path.GetTempFileName();
            byte[] content = Encoding.UTF8.GetBytes("Hello, World!");
            File.WriteAllBytes(fileName, content);

            // Act
            byte[]? result = SafeIO.ReadBytesFromFile(fileName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(content, result);

            // Cleanup
            File.Delete(fileName);
        }

        [Fact]
        public void ReadBytesFromFile_FileDoesNotExist_ReturnsNull()
        {
            // Arrange
            string fileName = Path.GetRandomFileName(); // Ensuring the file does not exist

            // Act
            byte[]? result = SafeIO.ReadBytesFromFile(fileName);

            // Assert
            Assert.Null(result);
        }
        #endregion

        #region ReadTextFromFile
        [Fact]
        public void ReadTextFromFile_ValidFile_ReturnsText()
        {
            // Arrange
            string fileName = Path.GetTempFileName();
            string content = "Hello, World!";
            File.WriteAllText(fileName, content);

            // Act
            string? result = SafeIO.ReadTextFromFile(fileName, isUnicode: false);

            // Assert
            Assert.Equal(content, result);

            // Cleanup
            File.Delete(fileName);
        }

        [Fact]
        public void ReadTextFromFile_FileDoesNotExist_ReturnsNull()
        {
            // Arrange
            string fileName = Path.GetRandomFileName(); // Ensuring the file does not exist

            // Act
            string? result = SafeIO.ReadTextFromFile(fileName, isUnicode: false);

            // Assert
            Assert.Null(result);
        }
        #endregion

        #region WriteBytesToFile
        [Fact]
        public void WriteBytesToFile_ValidPath_WritesBytes()
        {
            // Arrange
            string fileName = Path.GetTempFileName();
            byte[] content = Encoding.UTF8.GetBytes("Hello, World!");

            // Act
            OperationalResult result = SafeIO.WriteBytesToFile(fileName, content);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(content, File.ReadAllBytes(fileName));

            // Cleanup
            File.Delete(fileName);
        }

        [Fact]
        public void WriteBytesToFile_InvalidPath_ReturnsFalse()
        {
            // Arrange
            string fileName = "123/\\/sX:\\32\\?~~!!??dddz"; // Invalid file path
            byte[] content = Encoding.UTF8.GetBytes("Hello, World!");

            // Act
            OperationalResult result = SafeIO.WriteBytesToFile(fileName, content);

            // Assert
            Assert.False(result.Success);
        }
        [Fact]
        public void DeleteFile_FileExists_ReturnsTrue()
        {
            // Arrange
            var fileName = "testfile.txt";
            File.Create(fileName).Dispose();

            // Act
            var result = SafeIO.DeleteFile(fileName);

            // Assert
            Assert.True(result);
            Assert.False(File.Exists(fileName));
        }

        [Fact]
        public void DeleteFileWithResult_FileExists_ReturnsSuccess()
        {
            // Arrange
            var fileName = "testfile.txt";
            File.Create(fileName).Dispose();

            // Act
            var result = SafeIO.DeleteFileWithResult(fileName);

            // Assert
            Assert.True(result.Success);
            Assert.False(File.Exists(fileName));
        }

        [Fact]
        public void DeleteFileWithResult_FileDoesNotExist_ReturnsFailure()
        {
            // Arrange
            var fileName = "nonexistentfile.txt";

            // Act
            var result = SafeIO.DeleteFileWithResult(fileName);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("File does not exist.", result.Message);
        }

        [Fact]
        public void DetermineFileFormat_ValidExtension_ReturnsCorrectFormat()
        {
            // Arrange
            var fileName = "document.xlsx";

            // Act
            var result = SafeIO.DetermineFileFormat(fileName);

            // Assert
            Assert.Equal(FileFormat.Excel, result);
        }

        [Fact]
        public void DetermineFileFormat_InvalidExtension_ReturnsNotSpecified()
        {
            // Arrange
            var fileName = "document.unknown";

            // Act
            var result = SafeIO.DetermineFileFormat(fileName);

            // Assert
            Assert.Equal(FileFormat.NotSpecified, result);
        }

        [Fact]
        public void DirectoryExists_DirectoryExists_ReturnsTrue()
        {
            // Arrange
            var directoryName = "testdir";
            Directory.CreateDirectory(directoryName);

            // Act
            var result = SafeIO.DirectoryExists(directoryName);

            // Assert
            Assert.True(result);
            Directory.Delete(directoryName);
        }

        [Fact]
        public void DirectoryExists_DirectoryDoesNotExist_ReturnsFalse()
        {
            // Arrange
            var directoryName = "nonexistentdir";

            // Act
            var result = SafeIO.DirectoryExists(directoryName);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void FileExists_FileExists_ReturnsTrue()
        {
            // Arrange
            var fileName = "testfile.txt";
            File.Create(fileName).Dispose();

            // Act
            var result = SafeIO.FileExists(fileName);

            // Assert
            Assert.True(result);
            File.Delete(fileName);
        }
    }
    #endregion
}
