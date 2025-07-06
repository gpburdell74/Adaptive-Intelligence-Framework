namespace Adaptive.Intelligence.LanguageService.Execution;

/// <summary>
/// Provides the signature definition for the API for accessing the file system.
/// </summary>
/// <seealso cref="System.IDisposable" />
public interface IFileSystemProvider : IDisposable 
{
    /// <summary>
    /// Closes the file.
    /// </summary>
    /// <param name="fileStream">
    /// The <see cref="FileStream"/> instance to be closed.
    /// </param>
    void CloseFile(FileStream? fileStream);

    /// <summary>
    /// Gets the current directory.
    /// </summary>
    /// <returns>
    /// A string containing the fully-qualified path of the current directory, or null if the current directory cannot be determined.
    /// </returns>
    string? GetCurrentDirectory();

    /// <summary>
    /// Attempts to open the specified file.
    /// </summary>
    /// <param name="filePath">
    /// A string containing the fully-qualified path and name of the file.
    /// </param>
    /// <param name="mode">
    /// A <see cref="FileMode"/> value that specifies how the operating system should open the file.
    /// </param>
    /// <param name="access">
    /// A <see cref="FileAccess"/> value that specifies the operations that can be performed on the file.
    /// </param>
    /// <param name="share">
    /// A <see cref="FileShare"/> value that specifies the type of access other threads have to the file.
    /// </param>
    /// <returns></returns>
    FileStream? OpenFile(string filePath, FileMode mode, FileAccess access, FileShare share);

    /// <summary>
    /// Sets the current directory.
    /// </summary>
    /// <param name="path">
    /// A string containing the path to set as the current directory.
    /// </param>
    void SetCurrentDirectory(string? path); 
}
