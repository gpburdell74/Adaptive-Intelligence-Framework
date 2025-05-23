using Adaptive.Intelligence.Shared.IO;
using Adaptive.Intelligence.Shared.Logging;

namespace Adaptive.Intelligence.Shared.UI;

/// <summary>
/// Provides methods and functions for managing a button template file.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
internal sealed class ButtonTemplateFile : DisposableObjectBase
{
    #region Public Methods / Functions
    /// <summary>
    /// Attempts to read the template from the specified file.
    /// </summary>
    /// <param name="fileName">
    /// A string containing the fully-qualified path and name of the file to be written.
    /// </param>
    /// <returns>
    /// An <see cref="OperationalResult{T}"/> containing the <see cref="ButtonTemplate"/> that was loaded
    /// if successful; otherwise, contains the error.
    /// </returns>
    public OperationalResult<ButtonTemplate> LoadTemplate(string fileName)
    {
        OperationalResult<ButtonTemplate> loadResult;

        OperationalResult<FileStream> result = OpenFileForReading(fileName);
        if (result.Success)
        {
            FileStream? fileStream = result.DataContent;
            if (fileStream != null)
            {
                ButtonTemplateReader reader = new ButtonTemplateReader(fileStream);
                loadResult = reader.Read();
                reader.Close();

                fileStream.Close();
                fileStream.Dispose();
            }
            else
            {
                loadResult = new OperationalResult<ButtonTemplate>(false, "Stream is not available.");
            }
        }
        else
        {
            loadResult = new OperationalResult<ButtonTemplate>(false);
            loadResult.Success = false;
            result.CopyTo(loadResult);
        }

        result.Dispose();

        return loadResult;

    }

    /// <summary>
    /// Attempts to save the template to the specified file.
    /// </summary>
    /// <param name="fileName">
    /// A string containing the fully-qualified path and name of the file to be written.
    /// </param>
    /// <param name="template">
    /// The <see cref="ButtonTemplate"/> instance to be saved.
    /// </param>
    /// <returns>
    /// An <see cref="OperationalResult"/> containing the result of the operation.
    /// </returns>
    public OperationalResult SaveTemplate(string fileName, ButtonTemplate template)
    {
        OperationalResult saveResult;

        OperationalResult<FileStream> result = OpenFileForWriting(fileName);
        if (result.Success)
        {
            FileStream? fileStream = result.DataContent;
            if (fileStream != null)
            {
                ButtonTemplateWriter writer = new ButtonTemplateWriter(fileStream);
                saveResult = writer.Write(template);
                writer.Close();

                fileStream.Close();
                fileStream.Dispose();
            }
            else
            {
                saveResult = new OperationalResult(false, "The stream is not available.");
            }
        }
        else
        {
            saveResult = new OperationalResult();
            result.CopyTo(saveResult);
        }

        result.Dispose();
        return saveResult;
    }
    #endregion

    #region Private Methods / Functions
    /// <summary>
    /// Attempts to open the specified file for reading.
    /// </summary>
    /// <param name="fileName">
    /// A string containing the fully-qualified path and name of the file to be read.
    /// </param>
    /// <returns>
    /// An <see cref="OperationalResult{T}"/> of <see cref="FileStream"/> containing the opened stream if successful;
    /// otherwise, returns <b>null</b>.
    /// </returns>
    private OperationalResult<FileStream> OpenFileForReading(string fileName)
    {
        OperationalResult<FileStream> result = new OperationalResult<FileStream>();
        try
        {
            FileStream sourceStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            result.Success = true;
            result.DataContent = sourceStream;
        }
        catch(Exception ex)
        {
            ExceptionLog.LogException(ex);
            result.AddException(ex);
        }
        return result;
    }

    /// <summary>
    /// Attempts to open the specified file for writing.
    /// </summary>
    /// <remarks>
    /// If a file by the same name exists, it will be deleted before the new file is written.
    /// </remarks>
    /// <param name="fileName">
    /// A string containing the fully-qualified path and name of the file to be written.
    /// </param>
    /// <returns>
    /// An <see cref="OperationalResult{T}"/> of <see cref="FileStream"/> containing the opened stream if successful;
    /// otherwise, returns <b>null</b>.
    /// </returns>
    private OperationalResult<FileStream> OpenFileForWriting(string fileName)
    {
        OperationalResult<FileStream> result = new OperationalResult<FileStream>();

        try
        {
            SafeIO.DeleteFile(fileName);
            FileStream fileStream = new FileStream(fileName, FileMode.CreateNew, FileAccess.Write, FileShare.None);
            result.DataContent = fileStream;
            result.Success = true;
        }
        catch (Exception ex)
        {
            ExceptionLog.LogException(ex);
            result.AddException(ex);
            result.Success = false;
        }

        return result;
    }
    #endregion
}
