using Adaptive.BlazorBasic.LanguageService.CodeDom;
using Adaptive.BlazorBasic.Services;
using Adaptive.Intelligence.Shared;
using Adaptive.LanguageService.Tokenization;

namespace Adaptive.BlazorBasic.CodeDom;

/// <summary>
/// Represents a code expression for an I/O operation indicating the direction of the I/O or manner of creation of a file.
/// </summary>
/// <remarks>
/// This generally represents the "AS OUTPUT" or "AS INPUT", etc., section of an OPEN statement.
/// </remarks>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="ILanguageCodeExpression" />
public class BasicFileDirectionExpression : BasicExpression, ILanguageCodeExpression
{
    #region Private Member Declarations    
    /// <summary>
    /// The file mode setting.
    /// </summary>
    private FileMode _mode = FileMode.Open;
    /// <summary>
    /// The file access setting.
    /// </summary>
    private FileAccess _access = FileAccess.Read;
    #endregion

    #region Constructor / Dispose Methods        
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicFileDirectionExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the <see cref="BlazorBasicLanguageService" /> to use to find and compare
    /// text to language reserved words and operators and other items.
    /// </param>
    public BasicFileDirectionExpression(BlazorBasicLanguageService service) : base(service)
    {

    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicFileDirectionExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the <see cref="BlazorBasicLanguageService" /> to use to find and compare
    /// text to language reserved words and operators and other items.
    /// </param>
    /// <param name="text">
    /// A string containing the code to be parsed.
    /// </param>
    public BasicFileDirectionExpression(BlazorBasicLanguageService service, string text)
         : base(service)
    {
        ParseLiteralContent(text);
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicFileDirectionExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the <see cref="ILanguageService{FunctionsEnum, KeywordsEnum}" /> to use to find and compare
    /// text to language reserved words and operators and other items.
    /// </param>
    /// <param name="mode">The mode.</param>
    /// <param name="access">The access.</param>
    public BasicFileDirectionExpression(BlazorBasicLanguageService service, FileMode mode, FileAccess access)
        : base(service)
    {
        _mode = mode;
        _access = access;
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

    #region Protected Method Overrides    
    /// <summary>
    /// Parses the code line.
    /// </summary>
    /// <param name="codeLine">A <see cref="ITokenizedCodeLine" /> containing the code tokens for the entire line of code.</param>
    /// <param name="startIndex">An integer indicating the ordinal position in <paramref name="codeLine" /> to start parsing the expression.</param>
    /// <param name="endIndex">An integer indicating the ordinal position in <paramref name="codeLine" /> to end parsing the expression.</param>
    /// <exception cref="System.NotImplementedException"></exception>
    protected override void ParseCodeLine(ITokenizedCodeLine codeLine, int startIndex, int endIndex)
    {
        IToken? token = codeLine[startIndex];
        if (token == null)
            throw new SyntaxErrorException(codeLine.LineNumber);
            
        ParseLiteralContent(token.Text);
    }
    /// <summary>
    /// Parses the content expression into a parameter definition.
    /// </summary>
    /// <param name="expression">A string containing the expression to be parsed.</param>
    protected override void ParseLiteralContent(string? expression)
    {
        if (expression == null)
            throw new SyntaxErrorException(0);

        BlazorBasicKeywords keyword = Service.Keywords.GetKeywordType(expression);
        switch (keyword)
        {
            case BlazorBasicKeywords.Input:
                _mode = FileMode.Open;
                _access = FileAccess.Read;
                break;

            case BlazorBasicKeywords.Output:
                _mode = FileMode.OpenOrCreate;
                _access = FileAccess.Write;
                break;

            case BlazorBasicKeywords.Append:
                _mode = FileMode.Append;
                _access = FileAccess.Write;
                break;

            case BlazorBasicKeywords.Random:
                _mode = FileMode.Open;
                _access = FileAccess.ReadWrite;
                break;

            default:
                throw new InvalidArgumentException(0, "Invalid file direction specified: " + expression);

        }
        #endregion
    }
}

 