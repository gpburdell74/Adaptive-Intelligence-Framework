using Adaptive.Intelligence.BlazorBasic.Parser;
using Adaptive.Intelligence.BlazorBasic.Services;
using Adaptive.Intelligence.LanguageService.CodeDom;
using Adaptive.Intelligence.LanguageService.Tokenization;
using System.Text;

namespace Adaptive.Intelligence.BlazorBasic.CodeDom;

/// <summary>
/// Represents the WRITE command.
/// </summary>
/// <example>
/// Expected Formats:
///     WRITE #[fileNumber], [expression]
///     e.g.
///     WRITE #1, "Data" or 
///     WRITE #2, myVariable
/// 
/// </example>
/// <seealso cref="BasicCodeStatement" />
public class BasicWriteStatement : BasicCodeStatement
{
    #region Private Member Declarations    
    /// <summary>
    /// The file number expression.
    /// </summary>
    private BasicFileNumberExpression? _fileNumber;
    /// <summary>
    /// The expression to be written.
    /// </summary>
    private BlazorBasicExpression? _data;
    #endregion

    #region Constructor / Dispose Methods    
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicWriteStatement"/> class.
    /// </summary>
    /// <param name="service">The reference to the <see cref="BlazorBasicLanguageService" /> to use to find and compare
    /// text to language reserved words and operators and other items.</param>
    /// <param name="codeLine">An <see cref="ITokenizedCodeLine" /> containing the code line to be parsed.</param>
    public BasicWriteStatement(BlazorBasicLanguageService service, ITokenizedCodeLine codeLine) : base(service, codeLine)
    {
        CommandExpression = new BlazorBasicReservedWordExpression(service, KeywordNames.CommandWrite);
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
            _fileNumber?.Dispose();
            _data?.Dispose();
        }

        _fileNumber = null;
        _data = null;
        base.Dispose(disposing);
    }

    #endregion

    #region Public Properties 
    /// <summary>
    /// Gets the reference to the expression defining the file handle to write to.
    /// </summary>
    /// <value>
    /// A <see cref="BasicFileNumberExpression"/> instance representing the file number/handle expression.
    /// </value>
    public BasicFileNumberExpression? FileNumber => _fileNumber;
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
        if (codeLine.Count < 6)
            throw new SyntaxErrorException(codeLine.LineNumber);

        // Expected:
        // WRITE<space><#><integer>, <data content> [..., data expression, data expression, data expression ... ]
        IToken? handleToken = codeLine[2];
        _fileNumber = new BasicFileNumberExpression(Service, handleToken.Text);

        int index = 3;
        do
        {
            IToken next = codeLine[index];
            if (next.TokenType != TokenType.SeparatorDelimiter)
            {
                if (Service.Functions.IsBuiltInFunction(next.Text))
                    Expressions.Add(new BlazorBasicFunctionCallExpression(Service, next.Text));
                else if (next.TokenType == TokenType.UserDefinedItem)
                    Expressions.Add(new BlazorBasicVariableNameExpression(Service, next.Text));
                else
                {
                    ManagedTokenList list = new ManagedTokenList();
                    list.AddRange(codeLine.TokenList);
                    Expressions.Add(BlazorBasicExpressionFactory.CreateFromTokens(Service, codeLine.LineNumber, list, index));
                }
            }
            index++;
        }
        while (index < codeLine.Count);
   
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
        builder.Append(KeywordNames.CommandWrite);
        builder.Append(ParseConstants.Space);
        if (_fileNumber != null)
        {
            builder.Append(_fileNumber.Render());
            builder.Append(ParseConstants.Space);
        }
        builder.Append(ParseConstants.Comma);
        builder.Append(ParseConstants.Space);
        int length = Expressions.Count;
        for(int count = 0; count < length; count++)
        {
            builder.Append(Expressions[count].Render());
            if (count < length -1)
            {
                builder.Append(ParseConstants.Comma);
                builder.Append(ParseConstants.Space);
            }
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
        return Render() ?? nameof(BasicWriteStatement);
    }
    #endregion

}
