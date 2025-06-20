using Adaptive.BlazorBasic.LanguageService.CodeDom;
using Adaptive.Intelligence.Shared;

namespace Adaptive.BlazorBasic.CodeDom;

/// <summary>
/// Represents a code expression for an I/O operation indicating the number/handle value for the file or I/O operation.
/// </summary>
/// <remarks>
/// This generally represents the "#[number]"  section of an OPEN statement or the "#[number]" of a CLOSE or PRINT statement.
/// </remarks>
/// <example>
///     This represents the "#1" part of the following statements:
///     
///     OPEN "abc.dat" FOR OUTPUT AS #1
///     PRINT #1, "Data"
///     CLOSE #1
///     
/// </example>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="ILanguageCodeExpression" />
public class BasicFileNumberExpression : DisposableObjectBase, ILanguageCodeExpression
{
    #region Private Member Declarations    
    /// <summary>
    /// The file handle value.
    /// </summary>
    private int _fileHandle = -1;
    #endregion

    #region Constructors    
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicFileNumberExpression"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public BasicFileNumberExpression()
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicFileNumberExpression"/> class.
    /// </summary>
    /// <param name="fileNumber">The file number.</param>
    public BasicFileNumberExpression(int fileNumber)
    {
        _fileHandle = fileNumber;
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicFileNumberExpression"/> class.
    /// </summary>
    /// <param name="text">The text.</param>
    public BasicFileNumberExpression(string text)
    {
        _fileHandle = ParseFileNumber(text);
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets the file number.
    /// </summary>
    /// <value>
    /// An integer containing the file number/handle value to use.
    /// </value>
    public int FileNumber => _fileHandle;
    #endregion

    #region Private Methods / Functions    
    /// <summary>
    /// Parses the file number value.
    /// </summary>
    /// <remarks>
    /// Expected format:
    ///     #1 or #23 or #1230123, etc.
    /// </remarks>
    /// <param name="text">
    /// A string containing the text to be parsed.
    /// </param>
    /// <returns>
    /// The integer value contained in the text, or -1 if unsuccessful.
    /// </returns>
    private int ParseFileNumber(string text)
    {
        int value = -1;
        text = text.Replace("#", string.Empty);
        if (!int.TryParse(text, out value))
            value = -1;

        return value;
    }
    #endregion

    #region
    #endregion

}
