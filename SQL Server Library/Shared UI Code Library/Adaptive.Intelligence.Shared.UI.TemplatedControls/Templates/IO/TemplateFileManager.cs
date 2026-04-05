using Adaptive.Intelligence.Shared.IO;
using Adaptive.Intelligence.Shared.Logging;
using Adaptive.Intelligence.Shared.UI.TemplatedControls.IO;
using System.Text.Json;

namespace Adaptive.Intelligence.Shared.UI.TemplatedControls.Templates.IO;

public static class TemplateFileManager
{
    /// <summary>
    /// Attempts to read the template from the specified file.
    /// </summary>
    /// <param name="fileName">
    /// A string containing the fully-qualified path and name of the file to be read.
    /// </param>
    /// <returns>
    /// An <see cref="OperationalResult{T}"/> containing the <see cref="ButtonTemplate"/> that was loaded
    /// if successful; otherwise, contains the error.
    /// </returns>
    public static ButtonTemplate? LoadButtonTemplate(string fileName, TemplateFormats format, OperationalResult result)
    {
        ButtonTemplate? template = null;

        FileStream? sourceStream = OpenFileForReading(fileName, result);
        if (result.Success && sourceStream != null && sourceStream.CanRead)
        {
            switch (format)
            {
                case TemplateFormats.OldVersion92Format:
                    ButtonTemplateReader reader = new ButtonTemplateReader(sourceStream);
                    template = reader.Read(result);
                    reader.Close();
                    break;

                case TemplateFormats.JsonTextFormat:
                case TemplateFormats.JsonTextMinifiedFormat:
                    template = ReadFromJson<ButtonTemplate>(sourceStream, result);
                    break;

                default:
                    result.Success = false;
                    result.Message = "The specified template format is not supported.";
                    break;
            }
        }
        else
        {
            result.SetFailureMessage("Stream is not available.");
        }
        return template;
    }

    /// <summary>
    /// Attempts to save the template to the specified file.
    /// </summary>
    /// <param name="template">
    /// The reference to the template instance to be saved.
    /// </param>
    /// <param name="fileName">
    /// A string containing the fully-qualified path and name of the file to be written.
    /// </param>
    /// <param name="format">
    /// The <see cref="TemplateFormats"/> enumeration value specifying the file format.
    /// </param>
    /// <param name="result">
    /// The <see cref="OperationalResult"/> instance used to contain the result of the operation.
    /// </param>
    public static void SaveButtonTemplate(ButtonTemplate template, string fileName, TemplateFormats format, OperationalResult result)
    {
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            AllowDuplicateProperties = true,
            IgnoreReadOnlyProperties = true,
            MaxDepth = 64,
            AllowOutOfOrderMetadataProperties = true,
            WriteIndented = (format == TemplateFormats.JsonTextFormat)
        };
        options.Converters.Add(new ColorJsonConverter());

        FileStream? destStream = OpenFileForWriting(fileName, result);
        if (result.Success && destStream != null && destStream.CanWrite)
        {
            switch (format)
            {
                case TemplateFormats.OldVersion92Format:
                    ButtonTemplateWriter writer = new ButtonTemplateWriter(destStream);
                    writer.WriteOldFormat(template);
                    writer.Close();
                    break;

                case TemplateFormats.JsonTextFormat:
                case TemplateFormats.JsonTextMinifiedFormat:
                    WriteToJson<ButtonTemplate>(template, destStream, result, options);
                    break;

                default:
                    result.Success = false;
                    result.Message = "The specified template format is not supported.";
                    break;
            }
        }
        else
        {
            result.SetFailureMessage("Stream is not available.");
        }
    }

    /// <summary>
    /// Attempts to read the template from the specified file.
    /// </summary>
    /// <param name="fileName">
    /// A string containing the fully-qualified path and name of the file to be read.
    /// </param>
    /// <returns>
    /// The <see cref="PanelTemplate"/> that was loaded
    /// if successful; otherwise, returns <b>null</b>.
    /// </returns>
    public static PanelTemplate? LoadPanelTemplate(string fileName, TemplateFormats format, OperationalResult result)
    {
        PanelTemplate? template = null;

        FileStream? sourceStream = OpenFileForReading(fileName, result);
        if (result.Success && sourceStream != null && sourceStream.CanRead)
        {
            switch (format)
            {
                case TemplateFormats.OldVersion92Format:
                    PanelTemplateReader reader = new PanelTemplateReader(sourceStream);
                    template = reader.Read(result);
                    reader.Close();
                    break;

                case TemplateFormats.JsonTextFormat:
                case TemplateFormats.JsonTextMinifiedFormat:
                    template = ReadFromJson<PanelTemplate>(sourceStream, result);
                    break;

                default:
                    result.Success = false;
                    result.Message = "The specified template format is not supported.";
                    break;
            }
        }
        else
        {
            result.SetFailureMessage("Stream is not available.");
        }
        return template;
    }

    /// <summary>
    /// Attempts to save the template to the specified file.
    /// </summary>
    /// <param name="template">
    /// The reference to the template instance to be saved.
    /// </param>
    /// <param name="fileName">
    /// A string containing the fully-qualified path and name of the file to be written.
    /// </param>
    /// <param name="format">
    /// The <see cref="TemplateFormats"/> enumeration value specifying the file format.
    /// </param>
    /// <param name="result">
    /// The <see cref="OperationalResult"/> instance used to contain the result of the operation.
    /// </param>
    public static void SavePanelTemplate(PanelTemplate template, string fileName, TemplateFormats format, OperationalResult result)
    {
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            AllowDuplicateProperties = true,
            IgnoreReadOnlyProperties = true,
            MaxDepth = 64,
            AllowOutOfOrderMetadataProperties = true,
            WriteIndented = (format == TemplateFormats.JsonTextFormat)
        };
        options.Converters.Add(new ColorJsonConverter());

        FileStream? destStream = OpenFileForWriting(fileName, result);
        if (result.Success && destStream != null && destStream.CanWrite)
        {
            switch (format)
            {
                case TemplateFormats.OldVersion92Format:
                    PanelTemplateWriter writer = new PanelTemplateWriter(destStream);
                    writer.WriteOldFormat(template);
                    writer.Close();
                    break;

                case TemplateFormats.JsonTextFormat:
                case TemplateFormats.JsonTextMinifiedFormat:
                    WriteToJson<PanelTemplate>(template, destStream, result, options);
                    break;

                default:
                    result.Success = false;
                    result.Message = "The specified template format is not supported.";
                    break;
            }
        }
        else
        {
            result.SetFailureMessage("Stream is not available.");
        }
    }

    #region Private Methods / Functions
    /// <summary>
    /// Attempts to open the specified file for reading.
    /// </summary>
    /// <param name="fileName">
    /// A string containing the fully-qualified path and name of the file to be read.
    /// </param>
    /// <param name="result">
    /// The <see cref="OperationalResult"/> instance used to contain the result of the operation.
    /// </param>
    /// <returns>
    /// The <see cref="FileStream"/> containing the opened stream if successful;
    /// otherwise, returns <b>null</b>.
    /// </returns>
    private static FileStream? OpenFileForReading(string fileName, OperationalResult result)
    {
        FileStream? sourceStream = null;

        try
        {
            sourceStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            result.Success = true;
        }
        catch (Exception ex)
        {
            ExceptionLog.LogException(ex);
            result.AddException(ex);
            result.Success = false;
        }

        return sourceStream;
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
    /// <param name="result">
    /// The <see cref="OperationalResult"/> instance used to contain the result of the operation.
    /// </param>
    /// <returns>
    /// A <see cref="FileStream"/> containing the opened stream if successful;
    /// otherwise, returns <b>null</b>.
    /// </returns>
    private static FileStream? OpenFileForWriting(string fileName, OperationalResult result)
    {
        FileStream? fileStream = null;

        try
        {
            SafeIO.DeleteFile(fileName);
            fileStream = new FileStream(fileName, FileMode.CreateNew, FileAccess.Write, FileShare.None);
            result.Success = true;
        }
        catch (Exception ex)
        {
            ExceptionLog.LogException(ex);
            result.AddException(ex);
            result.Success = false;
        }

        return fileStream;
    }

    /// <summary>
    /// Reads the text from the open JSON file and creates the specified object instance from the data.
    /// </summary>
    /// <typeparam name="T">
    /// The data type of the template object to be created.
    /// </typeparam>
    /// <param name="sourceStream">
    /// The source <see cref="FileStream"/> instance to be read from.
    /// </param>
    /// <param name="result">
    /// An <see cref="OperationalResult"/> instance used to contain the result of the operation.
    /// </param>
    /// <returns>
    /// A new instance of <typeparamref name="T"/> if successful, populated with the data from the file;
    /// otherwise, returns <b>null</b>.
    /// </returns>
    private static T? ReadFromJson<T>(FileStream sourceStream, OperationalResult result)
    {
        T? templateInstance = default;

        using StreamReader? textReader = new StreamReader(sourceStream);
        textReader.BaseStream.Seek(0, SeekOrigin.Begin);

        try
        {
            // Read the file...

            string jsonText = textReader.ReadToEnd();

            // Deserialize if possible.
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                AllowDuplicateProperties = true,
                IgnoreReadOnlyProperties = true,
                MaxDepth = 64,
                AllowOutOfOrderMetadataProperties = true
            };
            options.Converters.Add(new ColorJsonConverter());

            templateInstance = JsonSerializer.Deserialize<T>(jsonText, options);
            result.Success = true;
        }
        catch (Exception ex)
        {
            ExceptionLog.LogException(ex);
            result.AddException(ex);
        }

        return templateInstance;

    }

    /// <summary>
    /// Writes the specified object instance to the specified destination stream in 
    /// JSON text format.
    /// </summary>
    /// <typeparam name="T">
    /// The data type of the object beng written.
    /// </typeparam>
    /// <param name="templateInstance">
    /// The template instance to be saved.
    /// </param>
    /// <param name="destStream">
    /// The reference to the destination <see cref="Stream"/>.
    /// </param>
    /// <param name="result">
    /// An <see cref="OperationalResult"/> containing the result of the operation.
    /// </param>
    private static void WriteToJson<T>(T templateInstance, FileStream destStream, OperationalResult result, JsonSerializerOptions options)
    {
        try
        {

            string jsonText = JsonSerializer.Serialize<T>(templateInstance, options);
            using StreamWriter? textWriter = new StreamWriter(destStream);
            textWriter.Write(jsonText);
            textWriter.Flush();
            result.Success = true;
        }
        catch (Exception ex)
        {
            ExceptionLog.LogException(ex);
            result.AddException(ex);
            result.Success = false;
        }
    }
    #endregion
}
