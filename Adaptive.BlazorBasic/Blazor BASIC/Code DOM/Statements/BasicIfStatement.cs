using Adaptive.Intelligence.BlazorBasic.Services;
using Adaptive.Intelligence.LanguageService.CodeDom;
using Adaptive.Intelligence.LanguageService.Tokenization;
using System.Text;

namespace Adaptive.Intelligence.BlazorBasic.CodeDom;

/// <summary>
/// Represents the command to start a IF...THEN... END IF block.
/// </summary>
/// <example>
///     IF
/// </example>
/// <seealso cref="BasicCodeStatement" />
public class BasicIfStatement : BasicCodeStatement
{
    #region Private Member Declarations
    /// <summary>
    /// The conditional statement.
    /// </summary>
    private BlazorBasicCodeConditionalExpression? _condition;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicIfStatement"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the <see cref="BlazorBasicLanguageService"/> instance.
    /// </param>
    public BasicIfStatement(BlazorBasicLanguageService service) : base(service)
    {
        _condition = new BlazorBasicCodeConditionalExpression(service);
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicIfStatement"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the <see cref="BlazorBasicLanguageService"/> instance.
    /// </param>
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

    public BlazorBasicCodeConditionalExpression ConditionalExpression => _condition;

    /// <summary>
    /// Gets the value of how the current number of tabs being printed is to be modified.
    /// </summary>
    /// <value>
    /// The tab modification.
    /// </value>
    public override RenderTabState TabModification => RenderTabState.IndentAfter;

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

        // Find the indices for the comparison portion of the code line.
        int leftIndex = codeLine.IndexOf(TokenType.ExpressionStartDelimiter);
        if (leftIndex == -1)
            leftIndex = 2;

        int thenIndex = codeLine.IndexOf(TokenType.ReservedWord, "THEN");
        if (thenIndex == -1)
            throw new BasicSyntaxErrorException(codeLine.LineNumber, "Expected: THEN");

        int rightIndex = codeLine.LastIndexOf(TokenType.ExpressionEndDelimiter);
        if (rightIndex == -1)
            rightIndex = thenIndex - 1;

        _condition = new BlazorBasicCodeConditionalExpression(Service, codeLine.SubExpression(leftIndex, rightIndex));
    }
    #endregion

    #region Public Methods / Functions    
    /// <summary>
    /// Renders the content of the expression into a string.
    /// </summary>
    /// <returns>
    /// A string containing the expression rendered into Blazor BASIC code.
    /// </returns>
    public override string? Render()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append(KeywordNames.CommandIf);
        builder.Append(ParseConstants.Space);
        builder.Append(_condition.Render());
        builder.Append(ParseConstants.Space);
        builder.Append(KeywordNames.KeywordThen);

        return builder.ToString();
    }
    #endregion
}
