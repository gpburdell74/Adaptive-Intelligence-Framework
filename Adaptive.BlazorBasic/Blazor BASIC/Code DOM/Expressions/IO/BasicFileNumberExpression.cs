using Adaptive.BlazorBasic.LanguageService.CodeDom;
using Adaptive.BlazorBasic.Services;
using Adaptive.Intelligence.Shared;
using Adaptive.LanguageService.Tokenization;

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
public class BasicFileNumberExpression : BasicExpression, ILanguageCodeExpression
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
    /// <param name="service">
    /// The reference to the <see cref="BlazorBasicLanguageService" /> to use to find and compare
    /// text to language reserved words and operators and other items.
    /// </param>
    public BasicFileNumberExpression(BlazorBasicLanguageService service) : base(service)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BasicFileNumberExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the <see cref="ILanguageService{FunctionsEnum, KeywordsEnum}" /> to use to find and compare
    /// text to language reserved words and operators and other items.
    /// </param>
    /// <param name="fileNumber">The file number.</param>
    public BasicFileNumberExpression(BlazorBasicLanguageService service, int fileNumber) : base(service)
    {
        _fileHandle = fileNumber;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BasicFileNumberExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the <see cref="BlazorBasicLanguageService" /> to use to find and compare
    /// text to language reserved words and operators and other items.
    /// </param>
    /// <param name="text">
    /// A string containing the text representing the file number assignment to be parsed.
    /// </param>
    public BasicFileNumberExpression(BlazorBasicLanguageService service, string text) : base(service, text)
    {
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

    #region Protected Method Overrides    
    /// <summary>
    /// Parses the content expression into a parameter definition.
    /// </summary>
    /// <param name="expression">A string containing the expression to be parsed.</param>
    protected override void ParseLiteralContent(string? expression)
    {
        int value = -1;
        string literalCode = NormalizeString(expression);

        if (!literalCode.StartsWith("#"))
            throw new Exception("?SYNTAX ERROR");

        string numericValue = literalCode.Substring(1, literalCode.Length - 1);
        if (!int.TryParse(numericValue, out value))
        {
            value = -1;
            throw new Exception("ARGUMENT INVALID");
        }

        _fileHandle = value;
    }
    /// <summary>
    /// Parses the code line.
    /// </summary>
    /// <param name="codeLine">A <see cref="ITokenizedCodeLine" /> containing the code tokens for the entire line of code.</param>
    /// <param name="startIndex">An integer indicating the ordinal position in <paramref name="codeLine" /> to start parsing the expression.</param>
    /// <param name="endIndex">An integer indicating the ordinal position in <paramref name="codeLine" /> to end parsing the expression.</param>
    protected override void ParseCodeLine(ITokenizedCodeLine codeLine, int startIndex, int endIndex)
    {
        IToken? token = codeLine[startIndex];
        if (token == null || token.Text == null)
            throw new Exception("?SYNTAX ERROR");
        ParseLiteralContent(token.Text);
    }
    #endregion
}
