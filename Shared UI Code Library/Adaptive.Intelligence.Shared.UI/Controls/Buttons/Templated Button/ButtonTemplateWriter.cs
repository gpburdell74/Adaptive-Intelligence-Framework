using Adaptive.Intelligence.Shared.IO;
using Adaptive.Intelligence.Shared.Logging;
using System.Drawing.Imaging;

namespace Adaptive.Intelligence.Shared.UI;

/// <summary>
/// Provides the methods and functions for writing a <see cref="ButtonTemplate"/> to an underlying stream.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
internal sealed class ButtonTemplateWriter : DisposableObjectBase
{
    #region Private Member Declarations    
    /// <summary>
    /// The source stream to write to.
    /// </summary>
    private Stream? _destinationStream;

    /// <summary>
    /// The writer instance.
    /// </summary>
    private SafeBinaryWriter? _writer;
    #endregion

    #region Constructor / Dispose Methods

    /// <summary>
    /// Initializes a new instance of the <see cref="ButtonTemplateWriter"/> class.
    /// </summary>
    /// <param name="destinationStream">
    /// The destination <see cref="Stream"/> instance to be written to.
    /// </param>
    public ButtonTemplateWriter(Stream? destinationStream)
    {
        _destinationStream = destinationStream;
        if (_destinationStream != null)
        {
            _writer = new SafeBinaryWriter(_destinationStream);
        }
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
            _writer?.Dispose();
        }

        _writer = null;
        _destinationStream = null;
        base.Dispose(disposing);
    }
    #endregion

    #region Public Methods / Functions

    /// <summary>
    /// Closes this instance.
    /// </summary>
    public void Close()
    {
        _writer?.Close();
        _writer?.Dispose();
        _writer = null;
        _destinationStream = null;
    }

    /// <summary>
    /// Attempts to write the content of the template to the underlying stream.
    /// </summary>
    /// <param name="template">
    /// The <see cref="ButtonTemplate"/> instance to be written.
    /// </param>
    /// <returns>
    /// An <see cref="OperationalResult"/> containing the result of the operation.
    /// </returns>
    public OperationalResult Write(ButtonTemplate template)
    {
        OperationalResult result = new OperationalResult();
        result.Success = true;

        if (_writer != null && template != null)
        {
            WriteStateTemplate(result, template.Checked);
            WriteStateTemplate(result, template.Disabled);
            WriteStateTemplate(result, template.Hover);
            WriteStateTemplate(result, template.Normal);
            WriteStateTemplate(result, template.Pressed);
        }
        return result;
    }
    #endregion

    #region Private Methods / Functions    
    /// <summary>
    /// Writes the <see cref="ButtonStateTemplate"/> instance.
    /// </summary>
    /// <param name="result">
    /// The <see cref="OperationalResult"/> instance tracking the result of the operation.
    /// </param>
    /// <param name="stateTemplate">
    /// The <see cref="ButtonStateTemplate"/> instance whose content is to be written.
    /// </param>
    private void WriteStateTemplate(OperationalResult result, ButtonStateTemplate? stateTemplate)
    {
        if (result.Success)
        {
            if (stateTemplate == null)
            {
                result.Success = false;
                result.Message = "No state template specified.";
            }
            else
            {
                WriteIfOk(result, (int)stateTemplate.BorderStyle);
                WriteIfOk(result, stateTemplate.BorderColor);
                WriteIfOk(result, stateTemplate.EndColor);
                WriteIfOk(result, stateTemplate.StartColor);
                WriteIfOk(result, stateTemplate.ForeColor);
                WriteIfOk(result, stateTemplate.Font);
                WriteIfOk(result, (int)stateTemplate.Mode);
                WriteIfOk(result, (int)stateTemplate.BorderWidth);
                WriteIfOk(result, (int)stateTemplate.CornerRadius);
                WriteIfOk(result, stateTemplate.ShadowText);
                WriteIfOk(result, stateTemplate.Image);
                WriteIfOk(result, (int)stateTemplate.ImageAlign);
                WriteIfOk(result, (int)stateTemplate.TextAlign);
                WriteIfOk(result, (int)stateTemplate.TextImageRelation);
            }
        }
    }

    /// <summary>
    /// Writes the <see cref="Color"/> value to the stream, if able.
    /// </summary>
    /// <remarks>
    /// This method writes the color in the format of the A, R, G, and B integers, then the IsSystem flag
    /// and then the color name.
    /// </remarks>
    /// <param name="result">
    /// The <see cref="OperationalResult"/> instance tracking the result of the operation.
    /// </param>
    /// <param name="value">
    /// The <see cref="Color"/> value to be written.
    /// </param>
    private void WriteIfOk(OperationalResult result, Color value)
    {
        if (result.Success)
        {
            if (_writer == null || !_writer.CanWrite)
            {
                result.Success = false;
                result.Message = "Unable to write to the stream.";
            }
            else
            {
                WriteIfOk(result, value.A);
                WriteIfOk(result, value.R);
                WriteIfOk(result, value.G);
                WriteIfOk(result, value.B);
                WriteIfOk(result, value.IsSystemColor);
                WriteIfOk(result, value.Name);

            }
        }
    }

    /// <summary>
    /// Writes the <see cref="Font"/> value to the stream, if able.
    /// </summary>
    /// <remarks>
    /// This method writes the font in the format of:
    ///     Font Family Name
    ///     Font Name
    ///     Is Bold?
    ///     Is Italic?
    ///     Is Underlined?
    ///     Em Size
    ///     Font Style
    /// </remarks>
    /// <param name="result">
    /// The <see cref="OperationalResult"/> instance tracking the result of the operation.
    /// </param>
    /// <param name="font">
    /// The <see cref="Font"/> value to be written.
    /// </param>
    private void WriteIfOk(OperationalResult result, Font? font)
    {
        if (result.Success)
        {
            if (_writer == null || !_writer.CanWrite || font == null)
            {
                result.Success = false;
                result.Message = "Unable to write to the stream.";
            }
            else
            {
                WriteIfOk(result, font.FontFamily.Name);
                WriteIfOk(result, font.Name);
                WriteIfOk(result, font.Bold);
                WriteIfOk(result, font.Italic);
                WriteIfOk(result, font.Underline);
                WriteIfOk(result, font.Size);
                WriteIfOk(result, (int)font.Style);
            }
        }
    }

    /// <summary>
    /// Writes the <see cref="Image"/> value to the stream, if able.
    /// </summary>
    /// <remarks>
    /// This method writes the font in the format of an integer length indicator, and then
    /// an array of bytes of that specified length.
    /// </remarks>
    /// <param name="result">
    /// The <see cref="OperationalResult"/> instance tracking the result of the operation.
    /// </param>
    /// <param name="image">
    /// The <see cref="Image"/> instance to be written.
    /// </param>
    private void WriteIfOk(OperationalResult result, Image? image)
    {
        if (result.Success)
        {
            if (_writer == null || !_writer.CanWrite)
            {
                result.Success = false;
                result.Message = "Unable to write to the stream.";
            }
            else if (image != null)
            {
                byte[]? imageBytes = ImageToBytes(result, image);
                if (imageBytes != null)
                {
                    WriteIfOk(result, imageBytes.Length);
                    WriteIfOk(result, imageBytes);
                }
            }
            else
            {
                WriteIfOk(result, (int)0);
            }
        }
    }

    /// <summary>
    /// Writes the <see cref="bool"/> value to the stream, if able.
    /// </summary>
    /// <param name="result">
    /// The <see cref="OperationalResult"/> instance tracking the result of the operation.
    /// </param>
    /// <param name="value">
    /// The <see cref="bool"/> instance to be written.
    /// </param>
    private void WriteIfOk(OperationalResult result, bool value)
    {
        if (result.Success)
        {
            if (_writer != null && _writer.CanWrite && !_writer.HasExceptions)
            {
                _writer.Write(value);
            }
            else
            {
                result.Success = false;
                result.Message = "Unable to write to the stream.";
            }
            PostWrite(result, _writer);
        }
    }

    /// <summary>
    /// Writes the <see cref="int"/> value to the stream, if able.
    /// </summary>
    /// <param name="result">
    /// The <see cref="OperationalResult"/> instance tracking the result of the operation.
    /// </param>
    /// <param name="value">
    /// The <see cref="int"/> instance to be written.
    /// </param>
    private void WriteIfOk(OperationalResult result, int value)
    {
        if (result.Success)
        {

            if (_writer != null && _writer.CanWrite && !_writer.HasExceptions)
            {
                _writer.Write(value);
            }
            else
            {
                result.Success = false;
                result.Message = "Unable to write to the stream.";
            }
            PostWrite(result, _writer);
        }
    }

    /// <summary>
    /// Writes the <see cref="string"/> value to the stream, if able.
    /// </summary>
    /// <param name="result">
    /// The <see cref="OperationalResult"/> instance tracking the result of the operation.
    /// </param>
    /// <param name="value">
    /// The <see cref="string"/> instance to be written.
    /// </param>
    private void WriteIfOk(OperationalResult result, string value)
    {
        if (result.Success)
        {

            if (_writer != null && _writer.CanWrite && !_writer.HasExceptions)
            {
                _writer.Write(value);
            }
            else
            {
                result.Success = false;
                result.Message = "Unable to write to the stream.";
            }
            PostWrite(result, _writer);
        }
    }

    /// <summary>
    /// Writes the <see cref="float"/> value to the stream, if able.
    /// </summary>
    /// <param name="result">
    /// The <see cref="OperationalResult"/> instance tracking the result of the operation.
    /// </param>
    /// <param name="value">
    /// The <see cref="float"/> instance to be written.
    /// </param>
    private void WriteIfOk(OperationalResult result, float value)
    {
        if (result.Success)
        {

            if (_writer != null && _writer.CanWrite && !_writer.HasExceptions)
            {
                _writer.Write(value);
            }
            else
            {
                result.Success = false;
                result.Message = "Unable to write to the stream.";
            }
            PostWrite(result, _writer);
        }
    }

    /// <summary>
    /// Writes the <see cref="byte"/> array value to the stream, if able.
    /// </summary>
    /// <param name="result">
    /// The <see cref="OperationalResult"/> instance tracking the result of the operation.
    /// </param>
    /// <param name="value">
    /// The <see cref="byte"/> array instance to be written.
    /// </param>
    private void WriteIfOk(OperationalResult result, byte[] value)
    {
        if (result.Success)
        {
            if (_writer != null && _writer.CanWrite && !_writer.HasExceptions)
            {
                _writer.Write(value);
            }
            else
            {
                result.Success = false;
                result.Message = "Unable to write to the stream.";
            }
            PostWrite(result, _writer);
        }
    }

    /// <summary>
    /// Completes the tasks/state changes after writing data.
    /// </summary>
    /// <param name="result">
    /// The <see cref="OperationalResult"/> tracking the result of the write operations.
    /// </param>
    /// <param name="writer">
    /// The <see cref="SafeBinaryWriter"/> instance being used to write data.
    /// </param>
    private static void PostWrite(OperationalResult result, SafeBinaryWriter? writer)
    {
        if (writer == null)
            result.Success = false;
        else
        {
            result.Success = (!writer.HasExceptions);
            if (!result.Success)
                result.AddExceptions(writer.Exceptions);
            writer.ClearExceptions();
            writer.Flush();
        }
    }

    /// <summary>
    /// Converts the specified image object to a byte array.
    /// </summary>
    /// <param name="result">
    /// The <see cref="OperationalResult"/> tracking the result of the write operations.
    /// </param>
    /// <param name="image">
    /// The <see cref="Image"/> instance.
    /// </param>
    /// <returns>
    /// A byte array containing the representation of the image if successful; otherwise, returns <b>null</b>.
    /// </returns>
    private static byte[]? ImageToBytes(OperationalResult result, Image image)
    {
        byte[]? imageBytes = null;

        MemoryStream storageStream = new MemoryStream(2500);
        try
        {
            image.Save(storageStream, ImageFormat.Png);
            storageStream.Position = 0;
            imageBytes = storageStream.ToArray();
        }
        catch (Exception ex)
        {
            ExceptionLog.LogException(ex);
            result.AddException(ex);
            result.Success = false;
        }
        finally
        {
            storageStream.Dispose();
        }
        return imageBytes;
    }
    #endregion
}
