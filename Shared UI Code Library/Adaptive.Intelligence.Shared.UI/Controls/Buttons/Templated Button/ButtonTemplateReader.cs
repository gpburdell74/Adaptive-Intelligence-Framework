using Adaptive.Intelligence.Shared.IO;
using Adaptive.Intelligence.Shared.Logging;
using System.Drawing.Drawing2D;

namespace Adaptive.Intelligence.Shared.UI;

/// <summary>
/// Provides the methods and functions for reading a <see cref="ButtonTemplate"/> from an underlying stream.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
internal sealed class ButtonTemplateReader : DisposableObjectBase
{
    #region Private Member Declarations

    /// <summary>
    /// The default font instance.
    /// </summary>
    private static readonly Font DefaultFont = new Font("Tahoma", 12f);

    /// <summary>
    /// The source stream to read from.
    /// </summary>
    private Stream? _sourceStream;

    /// <summary>
    /// The reader instance.
    /// </summary>
    private SafeBinaryReader? _reader;
    #endregion

    #region Constructor / Dispose Methods

    /// <summary>
    /// Initializes a new instance of the <see cref="ButtonTemplateReader"/> class.
    /// </summary>
    /// <param name="sourceStream">
    /// The destination <see cref="Stream"/> instance to be read from.
    /// </param>
    public ButtonTemplateReader(Stream? sourceStream)
    {
        _sourceStream = sourceStream;
        if (_sourceStream != null)
        {
            _reader = new SafeBinaryReader(_sourceStream);
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
            _reader?.Dispose();
        }

        _reader = null;
        _sourceStream = null;
        base.Dispose(disposing);
    }
    #endregion

    #region Public Methods / Functions

    /// <summary>
    /// Closes this instance.
    /// </summary>
    public void Close()
    {
        _reader?.Close();
        _reader?.Dispose();
        _reader = null;
        _sourceStream = null;
    }

    /// <summary>
    /// Attempts to read the content of the template from the underlying stream.
    /// </summary>
    /// <returns>
    /// An <see cref="OperationalResult"/> containing the <see cref="ButtonTemplate"/> if successful;
    /// otherwise, contains the result of the operation.
    /// </returns>
    public OperationalResult<ButtonTemplate> Read()
    {
        OperationalResult<ButtonTemplate> result = new OperationalResult<ButtonTemplate>();
        result.Success = true;
        ButtonTemplate? template = new ButtonTemplate();

        if (_reader != null)
        {
            ButtonStateTemplate? checkedTemplate = ReadStateTemplate(result);
            ButtonStateTemplate? disabledTemplate = ReadStateTemplate(result);
            ButtonStateTemplate? hoverTemplate = ReadStateTemplate(result);
            ButtonStateTemplate? normalTemplate = ReadStateTemplate(result);
            ButtonStateTemplate? pressedTemplate = ReadStateTemplate(result);

            if (result.Success)
            {
                template = new ButtonTemplate(checkedTemplate, normalTemplate, disabledTemplate, hoverTemplate, pressedTemplate);
                result.DataContent = template;
            }
            else
            {
                checkedTemplate?.Dispose();
                disabledTemplate?.Dispose();
                hoverTemplate?.Dispose();
                normalTemplate?.Dispose();
                pressedTemplate?.Dispose();
            }
        }
        return result;
    }
    #endregion

    #region Private Methods / Functions    

    /// <summary>
    /// Ensures the reader object is ready to read data.
    /// </summary>
    /// <param name="result">
    /// The <see cref="OperationalResult"/> instance tracking the result of the operation.
    /// </param>
    /// <returns>
    /// <b>true</b> if the data can be read; otherwise, <b>false</b>.
    /// </returns>
    private bool CheckReader(OperationalResult result)
    {
        if (result.Success)
        {
            if (_reader == null || !_reader.CanRead || _reader.HasExceptions)
            {
                result.Success = false;
                result.Message = "Unable to read from the stream.";
            }
        }
        return result.Success;
    }

    /// <summary>
    /// Converts the specified byte array to an image object.
    /// </summary>
    /// <param name="result">
    /// The <see cref="OperationalResult"/> tracking the result of the write operations.
    /// </param>
    /// <param name="dataSource">
    /// A byte array containing the image data.
    /// </param>
    /// <returns>
    /// An <see cref="Image"/> if successful; otherwise, returns <b>null</b>.
    /// </returns>
    private static Image? ImageFromBytes(OperationalResult result, byte[]? dataSource)
    {
        Image? newImage = null;

        if (result.Success && dataSource != null)
        {
            MemoryStream sourceStream = new MemoryStream(dataSource);
            try
            {
                newImage = Image.FromStream(sourceStream);
            }
            catch (Exception ex)
            {
                ExceptionLog.LogException(ex);
                result.AddException(ex);
                result.Success = false;
            }
            finally
            {
                sourceStream.Dispose();
            }
        }
        return newImage;
    }

    /// <summary>
    /// Reads the <see cref="ButtonStateTemplate"/> instance.
    /// </summary>
    /// <param name="result">
    /// The <see cref="OperationalResult"/> instance tracking the result of the operation.
    /// </param>
    /// <returns>
    /// The <see cref="ButtonStateTemplate"/> instance if successful; otherwise, returns <b>null</b>.
    /// </returns>
    private ButtonStateTemplate? ReadStateTemplate(OperationalResult result)
    {
        ButtonStateTemplate? stateTemplate = null;

        if (result.Success)
        {
            stateTemplate = new ButtonStateTemplate();
            stateTemplate.BorderStyle = (BorderStyle)ReadInt32(result);
            stateTemplate.BorderColor = ReadColor(result);
            stateTemplate.EndColor = ReadColor(result);
            stateTemplate.StartColor = ReadColor(result);
            stateTemplate.ForeColor = ReadColor(result);
            stateTemplate.Font = ReadFont(result);
            stateTemplate.Mode =(LinearGradientMode) ReadInt32(result);
            stateTemplate.BorderWidth  = ReadInt32(result);
            stateTemplate.CornerRadius = ReadInt32(result);
            stateTemplate.ShadowText = ReadBoolean(result);
            stateTemplate.Image = ReadImage(result);
            stateTemplate.ImageAlign = (ContentAlignment) ReadInt32(result);
            stateTemplate.TextAlign = (ContentAlignment)ReadInt32(result);
            stateTemplate.TextImageRelation =(TextImageRelation) ReadInt32(result);
        }
        if (!result.Success)
        {
            stateTemplate?.Dispose();
            stateTemplate = null;
        }

        return stateTemplate;
    }

    /// <summary>
    /// Reads the <see cref="Color"/> value from the stream, if able.
    /// </summary>
    /// <remarks>
    /// This method reads the color in the format of the A, R, G, and B integers, then the IsSystem flag
    /// and then the color name.
    /// </remarks>
    /// <param name="result">
    /// The <see cref="OperationalResult"/> instance tracking the result of the operation.
    /// </param>
    /// <returns>
    /// The <see cref="Color"/> value that was read.
    /// </returns>
    private Color ReadColor(OperationalResult result)
    {
        Color color = Color.Empty;

        if (result.Success)
        {
            if (_reader == null || !_reader.CanRead)
            {
                result.Success = false;
                result.Message = "Unable to read from the stream.";
            }
            else
            {
                int a= ReadInt32(result);
                int r = ReadInt32(result);
                int g = ReadInt32(result);
                int b = ReadInt32(result);
                bool sysColor = ReadBoolean(result);
                string? name = ReadString(result);

                if (result.Success)
                {
                    if (sysColor && name != null)
                    {
                        color = Color.FromKnownColor((KnownColor)Enum.Parse(typeof(KnownColor), name));
                    }
                    else
                    {
                        color = Color.FromArgb(a, r, g, b);
                    }
                }
            }
        }
        return color;
    }

    /// <summary>
    /// Reads the <see cref="Font"/> value from the stream, if able.
    /// </summary>
    /// <remarks>
    /// This method reads the font in the format of:
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
    /// <returns>
    /// The <see cref="Font"/> instance that was read.
    /// <returns>
    private Font ReadFont(OperationalResult result)
    {
        Font? newFont = null;

        if (result.Success)
        {
            if (_reader == null || !_reader.CanRead)
            {
                result.Success = false;
                result.Message = "Unable to write to the stream.";
            }
            else
            {
                string? fontFamily = ReadString(result);
                string? name = ReadString(result);
                bool bold = ReadBoolean(result);
                bool italic = ReadBoolean(result);
                bool underline = ReadBoolean(result);
                float emSize = ReadSingle(result);
                int style = ReadInt32(result);

                if (result.Success)
                {
                    FontStyle calcStyle = (FontStyle)style;
                    if (bold)
                        calcStyle |= FontStyle.Bold;
                    if (italic)
                        calcStyle |= FontStyle.Italic;
                    if (underline)
                        calcStyle |= FontStyle.Underline;

                    newFont = new Font(fontFamily!, emSize, calcStyle);
                }
            }
        }
        if (newFont == null)
            return DefaultFont;

        return newFont;
    }

    /// <summary>
    /// Reads the <see cref="Image"/> value from the stream, if able.
    /// </summary>
    /// <remarks>
    /// This method reads the image in the format of an integer length indicator, and then
    /// an array of bytes of that specified length.
    /// </remarks>
    /// <param name="result">
    /// The <see cref="OperationalResult"/> instance tracking the result of the operation.
    /// </param>
    /// <returns>
    /// The <see cref="Image"/> instance that was read.
    /// </returns>
    private Image? ReadImage(OperationalResult result)
    {
        Image? newImage = null;

        if (result.Success)
        {
            if (_reader == null || !_reader.CanRead)
            {
                result.Success = false;
                result.Message = "Unable to read from the stream.";
            }
            else
            {
                int dataLength = ReadInt32(result);
                if (dataLength > 0)
                {
                    byte[]? sourceData = ReadBytes(result, dataLength);
                    if (result.Success && sourceData != null)
                    {
                        newImage = ImageFromBytes(result, sourceData);
                    }
                    if (sourceData != null)
                        ByteArrayUtil.Clear(sourceData);
                }
            }
        }
        return newImage;
    }

    /// <summary>
    /// Reads the <see cref="bool"/> value to the stream, if able.
    /// </summary>
    /// <param name="result">
    /// The <see cref="OperationalResult"/> instance tracking the result of the operation.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/> value that was read.
    /// </returns>
    private bool ReadBoolean(OperationalResult result)
    {
        bool readValue = false;

        if (CheckReader(result))
        {
            readValue = _reader!.ReadBoolean();
        }
        return readValue;
    }

    /// <summary>
    /// Reads the <see cref="int"/> value to the stream, if able.
    /// </summary>
    /// <param name="result">
    /// The <see cref="OperationalResult"/> instance tracking the result of the operation.
    /// </param>
    /// <returns>
    /// The <see cref="int"/> value that was read.
    /// </returns>
    private int ReadInt32(OperationalResult result)
    {
        int readValue = 0;

        if (CheckReader(result))
        {
            readValue = _reader!.ReadInt32();
        }
        return readValue;
    }

    /// <summary>
    /// Reads the <see cref="float"/> value to the stream, if able.
    /// </summary>
    /// <param name="result">
    /// The <see cref="OperationalResult"/> instance tracking the result of the operation.
    /// </param>
    /// <returns>
    /// The <see cref="float"/> value that was read.
    /// </returns>
    private float ReadSingle(OperationalResult result)
    {
        float readValue = 0;

        if (CheckReader(result))
        {
            readValue = _reader!.ReadSingle();
        }
        return readValue;
    }

    /// <summary>
    /// Reads the <see cref="string"/> value to the stream, if able.
    /// </summary>
    /// <param name="result">
    /// The <see cref="OperationalResult"/> instance tracking the result of the operation.
    /// </param>
    /// <returns>
    /// The <see cref="string"/> value that was read.
    /// </returns>
    private string? ReadString(OperationalResult result)
    {
        string? readValue = null;

        if (CheckReader(result))
        {
            readValue = _reader!.ReadString();
        }
        return readValue;
    }

    /// <summary>
    /// Reads the <see cref="byte"/> array from the stream, if able.
    /// </summary>
    /// <param name="result">
    /// The <see cref="OperationalResult"/> instance tracking the result of the operation.
    /// </param>
    /// <param name="length">
    /// An integer specifying the number of bytes to be read.
    /// </param>
    /// <returns>
    /// The <see cref="byte"/> array that was read.
    /// </returns>
    private byte[]? ReadBytes(OperationalResult result, int length)
    {
        byte[]? readValue = null;

        if (CheckReader(result))
        {
            readValue = _reader!.ReadBytes(length);
        }
        return readValue;
    }
    #endregion
}
