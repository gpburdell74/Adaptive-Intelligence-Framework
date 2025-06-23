using Adaptive.Intelligence.BlazorBasic.Services;
using Adaptive.Intelligence.LanguageService.CodeDom;
using Adaptive.Intelligence.LanguageService.Tokenization;

namespace Adaptive.Intelligence.BlazorBasic.CodeDom;

/// <summary>
/// Represents the command to start a DO ... loop.
/// </summary>
/// <example>
///     DO
/// </example>
/// <seealso cref="BasicCodeStatement" />
public class BasicIfStatement : BasicCodeStatement
{

    #region Private Member Declarations    
    /// <summary>
    /// The conditional statement.
    /// </summary>
    private BasicCodeConditionalExpression _condition;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicIfStatement"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public BasicIfStatement(BlazorBasicLanguageService service) : base(service)
    {
        _condition = new BasicCodeConditionalExpression(service);
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicIfStatement"/> class.
    /// </summary>
    /// <param name="codeLine">An <see cref="ITokenizedCodeLine" /> containing the code line to be parsed.</param>
    public BasicIfStatement(BlazorBasicLanguageService service, ITokenizedCodeLine codeLine) : base(service, codeLine)
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
            _condition?.Dispose();
        }

        _condition = null;
        base.Dispose(disposing);
    }
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
        LineNumber = codeLine.LineNumber;

        IToken? lastToken = codeLine[codeLine.Count - 1];
        if (lastToken == null || lastToken.TokenType != TokenType.ReservedWord || lastToken.Text != "THEN")
            throw new Exception("?SYNTAX ERROR");

        _condition = new BasicCodeConditionalExpression(Service, codeLine);

    }
    #endregion
}
