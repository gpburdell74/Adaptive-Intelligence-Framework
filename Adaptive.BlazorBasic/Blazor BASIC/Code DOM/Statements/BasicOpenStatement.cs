using Adaptive.BlazorBasic.Services;
using Adaptive.LanguageService.Tokenization;

namespace Adaptive.BlazorBasic.CodeDom;

/// <summary>
/// Represents an OPEN statement.
/// </summary>
/// <example>
/// 
///     OPEN "abc.dat" FOR INPUT AS #1
///     
/// </example>
/// <seealso cref="BasicCodeStatement" />
public class BasicOpenStatement : BasicCodeStatement
{
    #region Private Member Declarations
    /// <summary>
    /// The file name expression.
    /// </summary>
    private BasicFileNameExpression? _fileName;
    /// <summary>
    /// The file direction expression (INPUT, OUTPUT, APPEND)
    /// </summary>
    private BasicFileDirectionExpression? _fileDirection;
    /// <summary>
    /// The file number / handle expression (#1, #2, etc.).
    /// </summary>
    private BasicFileNumberExpression? _fileNumber;
    #endregion

    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicOpenStatement"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public BasicOpenStatement(BlazorBasicLanguageService service) : base(service)
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicOpenStatement"/> class.
    /// </summary>
    /// <param name="codeLine">An <see cref="ITokenizedCodeLine" /> containing the code line to be parsed.</param>
    public BasicOpenStatement(BlazorBasicLanguageService service, ITokenizedCodeLine codeLine) : base(service, codeLine)
    {
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
            _fileName?.Dispose();
            _fileDirection?.Dispose();
            _fileNumber?.Dispose();
        }

        _fileName = null;
        _fileDirection = null;
        _fileNumber = null;

        base.Dispose(disposing);
    }
    #endregion

    #region Public Properties
    #endregion

    #region Protected Method Overrides
    /// <summary>
    /// Parses the specified code content.
    /// </summary>
    /// <param name="codeLine">
    /// An <see cref="ITokenizedCodeLine"/> containing the code tokens to parse.
    /// </param>
    protected override void ParseIntoExpressions(ITokenizedCodeLine codeLine)
    {
        // Expected format:
        // OPEN "<fileName>" FOR <direction> AS <#fileNumber>

        int nextIndex = ReadFileNameExpression(codeLine);

        // Next should be: "FOR"
        IToken forToken = codeLine[nextIndex];
        if (forToken.TokenType != TokenType.ReservedWord ||
            forToken.Text.ToUpper() != "FOR")
        {
            throw new Exception("Expected 'FOR' after file name.");
        }
        nextIndex += 2;

        IToken? directionToken = codeLine[nextIndex];
        if (directionToken != null && directionToken.TokenType == TokenType.ReservedWord &&
            (directionToken.Text == "OUTPUT" ||
            directionToken.Text == "INPUT" ||
            directionToken.Text == "APPEND"))
        {
            _fileDirection = new BasicFileDirectionExpression(Service, directionToken.Text);
            nextIndex += 2;
        }
        else
        {
            throw new Exception("?SYNTAX ERROR");
        }

        IToken? asToken = codeLine[nextIndex];
        if (asToken.TokenType != TokenType.ReservedWord ||
            asToken.Text.ToUpper() != "AS")
        {
            throw new Exception("Expected 'AS'.");
        }
        nextIndex += 2;

        IToken? numberToken = codeLine[nextIndex];
        if (numberToken != null)
        {
            _fileNumber = new BasicFileNumberExpression(Service, numberToken.Text!);
        }
    }
    #endregion

    #region Private Methods / Functions
    private int ReadFileNameExpression(ITokenizedCodeLine codeLine)
    {
        int nextIndex = -1;
        // Expected: 
        //  "somefilename.txt" or 
        //  fileVariableName

        // Index 1 should be a separator delimiter.
        if (codeLine[1].TokenType != TokenType.SeparatorDelimiter)
        {
            throw new Exception("Invalid file specification.");
        }
        else if (codeLine[2].TokenType == TokenType.VariableName)
        {
            _fileName = new BasicFileNameExpression(Service, codeLine[2]);
            // Next token is a separator, so the next valid token is #5.
            nextIndex = 4;
        }
        else if ((codeLine.TokenList[2].TokenType == TokenType.StringDelimiter) &&
                 (codeLine.TokenList[4].TokenType == TokenType.StringDelimiter))
        {
            // File name is a string literal.
            _fileName = new BasicFileNameExpression(Service, codeLine[3]);
            nextIndex = 6;
        }
        else
        {
            throw new Exception("Invalid file specification.");
        }

        return nextIndex;
    }
    #endregion
}
