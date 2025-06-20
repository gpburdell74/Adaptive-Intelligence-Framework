using Adaptive.BlazorBasic.LanguageService.CodeDom;
using Adaptive.Intelligence.Shared;

namespace Adaptive.BlazorBasic.CodeDom;

/// <summary>
/// Represents a code expression for an I/O operation indicating the direction of the I/O or manner of creation of a file.
/// </summary>
/// <remarks>
/// This generally represents the "AS OUTPUT" or "AS INPUT", etc., section of an OPEN statement.
/// </remarks>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="ILanguageCodeExpression" />
public class BasicFileDirectionExpression : DisposableObjectBase, ILanguageCodeExpression
{
    private FileMode _mode = FileMode.Open;
    private FileAccess _access = FileAccess.Read;

    #region Constructor / Dispose Methods    
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicFileDirectionExpression"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public BasicFileDirectionExpression()
    {

    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicFileDirectionExpression"/> class.
    /// </summary>
    /// <param name="text">
    /// A string containing the code to be parsed.
    /// </param>
    public BasicFileDirectionExpression(string text)
    {
        DetermineFileMode(text);
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicFileDirectionExpression"/> class.
    /// </summary>
    /// <param name="mode">The mode.</param>
    /// <param name="access">The access.</param>
    public BasicFileDirectionExpression(FileMode mode, FileAccess access)
    {
        _mode = mode;
        _access = access;
    }
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets the file access setting.
    /// </summary>
    /// <value>
    /// A <see cref="FileAccess"/> enumerated value.
    /// </value>
    public FileAccess Access => _access;
    /// <summary>
    /// Gets the file mode setting.
    /// </summary>
    /// <value>
    /// A <see cref="FileMode"/> enumerated value.
    /// </value>
    public FileMode Mode => _mode;
    #endregion

    #region Private Methods / Functions
    /// <summary>
    /// Determines the file mode and access enumeration values from the provided text.
    /// </summary>
    /// <param name="text">The text.</param>
    private void DetermineFileMode(string text)
    {
        switch (text.ToLower().Trim())
        {
            case "input":
                _mode = FileMode.Open;
                _access = FileAccess.Read;
                break;

            case "output":
                _mode = FileMode.OpenOrCreate;
                _access = FileAccess.Write;
                break;

            case "append":
                _mode = FileMode.Append;
                _access = FileAccess.Write;
                break;

            case "random":
                _mode = FileMode.Open;
                _access = FileAccess.ReadWrite;
                break;
        }
    }
    #endregion
}
