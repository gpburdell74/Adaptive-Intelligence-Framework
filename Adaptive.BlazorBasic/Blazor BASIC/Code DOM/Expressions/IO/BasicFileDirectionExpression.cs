using Adaptive.Intelligence.BlazorBasic.Services;
using Adaptive.Intelligence.LanguageService.CodeDom;
using Adaptive.Intelligence.LanguageService.Execution;
using Adaptive.Intelligence.LanguageService.Tokenization;
using Adaptive.Intelligence.BlazorBasic.CodeDom.Expressions;
using Adaptive.Intelligence.LanguageService.CodeDom.Expressions;

namespace Adaptive.Intelligence.BlazorBasic.CodeDom;

/// <summary>
/// Represents a code expression for an I/O operation indicating the direction of the I/O or manner of creation of a file.
/// </summary>
/// <remarks>
/// This generally represents the "AS OUTPUT" or "AS INPUT", etc., section of an OPEN statement.
/// </remarks>
/// <seealso cref="BasicExpression" />
/// <seealso cref="ICodeExpression" />
public class BasicFileDirectionExpression : BasicExpression, ICodeExpression
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
            throw new BasicSyntaxErrorException(codeLine.LineNumber);
            
        ParseLiteralContent(token.Text);
    }
    /// <summary>
    /// Parses the content expression into a parameter definition.
    /// </summary>
    /// <param name="expression">A string containing the expression to be parsed.</param>
    protected override void ParseLiteralContent(string? expression)
    {
        if (expression == null)
            throw new BasicSyntaxErrorException(0);

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
                throw new BasicInvalidArgumentException(0, "Invalid file direction specified: " + expression);

        }
    }
    #endregion

    #region Public Methods / Functions
    /// <summary>
    /// Evaluates the expression.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="engine">The execution engine instance.</param>
    /// <param name="environment">The execution environment instance.</param>
    /// <param name="scope">The <see cref="T:Adaptive.Intelligence.LanguageService.Execution.IScopeContainer" /> instance, such as a procedure or function, in which scoped
    /// variables are declared.</param>
    /// <returns>
    /// The result of the object evaluation.
    /// </returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public override object Evaluate(IExecutionEngine engine, IExecutionEnvironment environment, IScopeContainer scope) 
    {
        return string.Empty;
    }

    /// <summary>
    /// Renders the content of the expression into a string.
    /// </summary>
    /// <returns>
    /// A string containing the expression rendered into Blazor BASIC code.
    /// </returns>
    public override string? Render()
    {
        string modeText = string.Empty;
        
        switch (_mode)
        {
            case FileMode.Append:
                modeText =KeywordNames.IOAppend;
                break;

            case FileMode.CreateNew:
                modeText = KeywordNames.IOOutput;
                break;

            case FileMode.Open:
            case FileMode.OpenOrCreate:
                switch (_access)
                {
                    case FileAccess.Read:
                        modeText = KeywordNames.IOInput;
                        break;

                    case FileAccess.Write:
                        modeText = KeywordNames.IOOutput;
                        break;

                    case FileAccess.ReadWrite:
                        modeText = KeywordNames.IORandom;
                        break;
                }
                break;
        }
        return modeText;
    }

    #endregion
}

