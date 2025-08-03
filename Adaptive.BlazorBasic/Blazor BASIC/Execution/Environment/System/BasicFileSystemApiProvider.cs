using Adaptive.Intelligence.LanguageService.Execution;
using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.Shared.Logging;

namespace Adaptive.Intelligence.BlazorBasic.Execution;

/// <summary>
/// Provides the implementation of the file system API for the language interpreter.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="IFileSystemProvider" />
public sealed class BasicFileSystemApiProvider : DisposableObjectBase, IFileSystemProvider
{
    /// <summary>
    /// Closes the file.
    /// </summary>
    /// <param name="fileStream">
    /// The <see cref="FileStream" /> instance to be closed.
    /// </param>
    public void CloseFile(FileStream? fileStream)
    {
        try
        {
            fileStream?.Close();
            fileStream?.Dispose();
        }
        catch (Exception ex)
        {
            ExceptionLog.LogException(ex);
        }
    }

    /// <summary>
    /// Gets the current directory.
    /// </summary>
    /// <returns>
    /// A string containing the fully-qualified path of the current directory, or null if the current directory cannot be determined.
    /// </returns>
    public string? GetCurrentDirectory()
    {
        string? path = null;
        try
        {
            path = Directory.GetCurrentDirectory();
        }
        catch(Exception ex )
        {
            ExceptionLog.LogException(ex);
        }
        return path;
    }

    /// <summary>
    /// Attempts to open the specified file.
    /// </summary>
    /// <param name="filePath">
    /// A string containing the fully-qualified path and name of the file.
    /// </param>
    /// <param name="mode">
    /// A <see cref="FileMode" /> value that specifies how the operating system should open the file.
    /// </param>
    /// <param name="access">
    /// A <see cref="FileAccess" /> value that specifies the operations that can be performed on the file.
    /// </param>
    /// <param name="share">
    /// A <see cref="FileShare" /> value that specifies the type of access other threads have to the file.
    /// </param>
    /// <returns>
    /// The opened <see cref="FileStream"/> instance if successful; otherwise, returns <b>null</b>.
    /// </returns>
    public FileStream? OpenFile(string filePath, FileMode mode, FileAccess access, FileShare share)
    {
        FileStream? stream = null;

        try
        {
            stream = new FileStream(filePath, mode, access, share);
        }
        catch(Exception ex)
        {
            ExceptionLog.LogException(ex);
        }
        return stream;
    }

    /// <summary>
    /// Sets the current directory.
    /// </summary>
    /// <param name="path">
    /// A string containing the path to set as the current directory.
    /// </param>
    public void SetCurrentDirectory(string? path)
    {
        try
        {
            Directory.SetCurrentDirectory(path);
        }
        catch(Exception ex)
        {
            ExceptionLog.LogException(ex);
        }
    }
}
