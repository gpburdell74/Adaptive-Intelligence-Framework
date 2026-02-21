using Adaptive.Intelligence.Shared.IO;
using Adaptive.Intelligence.Shared.UI.TemplatedControls.States;

namespace Adaptive.Intelligence.Shared.UI.TemplatedControls.IO;

/// <summary>
/// Provides the methods and functions for writing a <see cref="PanelTemplate"/> to an underlying stream.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
internal sealed class PanelTemplateWriter : DisposableObjectBase
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
    /// Initializes a new instance of the <see cref="PanelTemplateWriter"/> class.
    /// </summary>
    /// <param name="destinationStream">
    /// The destination <see cref="Stream"/> instance to be written to.
    /// </param>
    public PanelTemplateWriter(Stream? destinationStream)
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
    /// The <see cref="PanelTemplate"/> instance to be written.
    /// </param>
    /// <returns>
    /// An <see cref="OperationalResult"/> containing the result of the operation.
    /// </returns>
    public OperationalResult WriteOldFormat(PanelTemplate template)
    {
        OperationalResult result = new OperationalResult();
        result.Success = true;

        if (_writer != null && template != null)
        {
            WriteStateTemplate(result, template.Disabled);
            WriteStateTemplate(result, template.Hover);
            WriteStateTemplate(result, template.Normal);
        }
        return result;
    }
    #endregion

    #region Private Methods / Functions    
    /// <summary>
    /// Writes the <see cref="StateTemplate"/> instance.
    /// </summary>
    /// <param name="result">
    /// The <see cref="OperationalResult"/> instance tracking the result of the operation.
    /// </param>
    /// <param name="stateTemplate">
    /// The <see cref="StateTemplate"/> instance whose content is to be written.
    /// </param>
    private void WriteStateTemplate(OperationalResult result, StateTemplate? stateTemplate)
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
                WriteIfOk(result, (int)stateTemplate.TextAlign);
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
    private void WriteIfOk(OperationalResult result, FontTemplate? font)
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
                WriteIfOk(result, font.FontFamily);
                WriteIfOk(result, string.Empty);
                WriteIfOk(result, false);
                WriteIfOk(result, false);
                WriteIfOk(result, false);
                WriteIfOk(result, font.Size);
                WriteIfOk(result, (int)font.Style);
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
    #endregion
}
