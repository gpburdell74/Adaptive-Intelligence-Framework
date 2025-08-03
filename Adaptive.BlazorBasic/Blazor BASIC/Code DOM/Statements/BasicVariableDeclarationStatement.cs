using Adaptive.Intelligence.BlazorBasic.Services;
using Adaptive.Intelligence.LanguageService.CodeDom;
using Adaptive.Intelligence.LanguageService.Tokenization;
using System.Text;
using Adaptive.Intelligence.BlazorBasic.CodeDom.Statements;
using Adaptive.Intelligence.BlazorBasic.CodeDom.Expressions;
using Adaptive.Intelligence.LanguageService.CodeDom.Statements;
using Adaptive.Intelligence.LanguageService.CodeDom.Expressions;

namespace Adaptive.Intelligence.BlazorBasic.CodeDom;

/// <summary>
/// Represents the DIM command.
/// </summary>
/// <example>
/// Expected Formats:
///     DIM [variableName] AS [Type]
/// </example>
/// <seealso cref="BasicCodeStatement" />
public class BasicVariableDeclarationStatement : BasicCodeStatement, ICodeVariableDeclarationStatement
{
    #region Private Member Declarations    
    /// <summary>
    /// The variable name.
    /// </summary>
    private BasicVariableNameExpression? _variable;
    /// <summary>
    /// The data type expression.
    /// </summary>
    private BlazorBasicDataTypeExpression? _dataType;
    #endregion

    #region Constructor / Dispose Methods    
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicVariableDeclarationStatement"/> class.
    /// </summary>
    /// <param name="service">The reference to the <see cref="BlazorBasicLanguageService" /> to use to find and compare
    /// text to language reserved words and operators and other items.</param>
    /// <param name="codeLine">An <see cref="ITokenizedCodeLine" /> containing the code line to be parsed.</param>
    public BasicVariableDeclarationStatement(BlazorBasicLanguageService service, ITokenizedCodeLine codeLine) : base(service, codeLine)
    {
        CommandExpression = new BlazorBasicReservedWordExpression(service, KeywordNames.CommandDim);
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
            _dataType?.Dispose();
            _variable?.Dispose();
        }

        _dataType = null;
        _variable = null;
        base.Dispose(disposing);
    }

    #endregion

    #region Public Properties 
    /// <summary>
    /// Gets the reference to the expression defining type of the data.
    /// </summary>
    /// <value>
    /// A <see cref="BlazorBasicDataTypeExpression"/> instance representing the data type of the variable.
    /// </value>
    public BlazorBasicDataTypeExpression? DataType => _dataType;
    ICodeDataTypeExpression? ICodeVariableDeclarationStatement.DataType => _dataType;

    /// <summary>
    /// Gets the reference to the expression providing the variable name.
    /// </summary>
    /// <value>
    /// A <see cref="BasicVariableNameExpression"/> defining the variable name/reference.
    /// </value>
    public BasicVariableNameExpression? VariableReference => _variable;
    ICodeVariableNameExpression? ICodeVariableDeclarationStatement.VariableReference => _variable;

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
        if (codeLine.Count < 5)
            throw new BasicSyntaxErrorException(codeLine.LineNumber);

        // Expected:
        // DIM<space><name><space>AS<datatypename>
        IToken? nameToken = codeLine[2];
        IToken? dataTypeToken = codeLine[6];

        if (nameToken == null || string.IsNullOrEmpty(nameToken.Text))
        {
            throw new BasicSyntaxErrorException(codeLine.LineNumber, "No variable name specified.");
        }

        if (dataTypeToken == null || string.IsNullOrEmpty(dataTypeToken.Text))
        {
            throw new BasicSyntaxErrorException(codeLine.LineNumber, "No data type name specified.");
        }

        _variable = new BasicVariableNameExpression(Service, nameToken.Text!);
        _dataType = new BlazorBasicDataTypeExpression(Service, "AS " + dataTypeToken.Text!);
        Expressions.Add(_variable);
        Expressions.Add(_dataType);
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
        builder.Append(KeywordNames.CommandDim);
        builder.Append(ParseConstants.Space);
        if (_variable != null)
        {
            builder.Append(_variable.Render());
            builder.Append(ParseConstants.Space);
        }
        builder.Append(KeywordNames.KeywordAs);
        builder.Append(ParseConstants.Space);
        if (_dataType != null)
        {
            builder.Append(_dataType.Render());
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
        return Render() ?? string.Empty;
    }
    #endregion

}
