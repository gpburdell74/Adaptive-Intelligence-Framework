using Adaptive.Intelligence.BlazorBasic.Parser;
using Adaptive.Intelligence.BlazorBasic.Services;
using Adaptive.Intelligence.LanguageService.CodeDom;
using Adaptive.Intelligence.LanguageService.Tokenization;
using System.Text;

namespace Adaptive.Intelligence.BlazorBasic.CodeDom;

/// <summary>
/// Represents the PRINT command.
/// </summary>
/// <example>
/// Expected Formats:
///     PRINT ""
///     PRINT "Abcdef ghi"
///     PRINT 123
///     PRINT A + B
///     PRINT myVariable
/// 
/// </example>
/// <seealso cref="BasicCodeStatement" />
public class BasicPrintStatement : BasicCodeStatement
{
    #region Private Member Declarations    
    /// <summary>
    /// The expression to be written.
    /// </summary>
    private BlazorBasicExpression? _data;
    #endregion

    #region Constructor / Dispose Methods    
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicPrintStatement"/> class.
    /// </summary>
    /// <param name="service">The reference to the <see cref="BlazorBasicLanguageService" /> to use to find and compare
    /// text to language reserved words and operators and other items.</param>
    /// <param name="codeLine">An <see cref="ITokenizedCodeLine" /> containing the code line to be parsed.</param>
    public BasicPrintStatement(BlazorBasicLanguageService service, ITokenizedCodeLine codeLine) : base(service, codeLine)
    {
        CommandExpression = new BlazorBasicReservedWordExpression(service, KeywordNames.CommandPrint);
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
            _data?.Dispose();
        }

        _data = null;
        base.Dispose(disposing);
    }

    #endregion

    #region Public Properties 
    /// <summary>
    /// Gets the reference to the expression providing the content to write.
    /// </summary>
    /// <value>
    /// A <see cref="BlazorBasicExpression"/> defining the content.
    /// </value>
    public BlazorBasicExpression? DataExpression => _data;

    /// <summary>
    /// Gets the value of how the current number of tabs being printed is to be modified.
    /// </summary>
    /// <value>
    /// The tab modification.
    /// </value>
    public override RenderTabState TabModification => RenderTabState.None;
    #endregion

    #region Protected Method Overrides    
    /// <summary>
    /// Parses the specified code content.
    /// </summary>
    /// <param name="codeLine">A string containing the code to be parsed.</param>
    /// <exception cref="System.NotImplementedException"></exception>
    protected override void ParseIntoExpressions(ITokenizedCodeLine codeLine)
    {
        if (codeLine.Count < 3)
            throw new SyntaxErrorException(codeLine.LineNumber);

        // Expected:
        // PRINT<space> <data content> [..., data expression, data expression, data expression ... ]
        _data = BlazorBasicExpressionFactory.CreateFromTokens(Service, codeLine.LineNumber,
            new ManagedTokenList(codeLine.TokenList), 2);
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
        builder.Append(KeywordNames.CommandPrint);
        builder.Append(ParseConstants.Space);
        if (_data != null)
        {
            builder.Append(_data.Render());
        }
        return builder.ToString();
    }
    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="System.String" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
        return Render() ?? nameof(BasicPrintStatement);
    }
    #endregion

}
