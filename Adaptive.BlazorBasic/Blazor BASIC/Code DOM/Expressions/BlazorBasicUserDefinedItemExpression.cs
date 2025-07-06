using Adaptive.Intelligence.BlazorBasic.Services;
using Adaptive.Intelligence.LanguageService.Execution;
using Adaptive.Intelligence.LanguageService.Tokenization;
using System.Text;
using Adaptive.Intelligence.BlazorBasic.CodeDom.Expressions;
using Adaptive.Intelligence.LanguageService.CodeDom.Expressions;

namespace Adaptive.Intelligence.BlazorBasic.CodeDom;

/// <summary>
/// Represents and manages a procedure call expression.
/// </summary>
/// <seealso cref="BasicExpression" />
public sealed class BlazorBasicUserDefinedItemExpression : BasicExpression
{
    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicUserDefinedItemExpression"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public BlazorBasicUserDefinedItemExpression(BlazorBasicLanguageService service) : base(service)
    {

    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicCodeConditionalExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the language service instance.
    /// </param>
    /// <param name="expression">
    /// A string containing the expression to be parsed.
    /// </param>
    public BlazorBasicUserDefinedItemExpression(BlazorBasicLanguageService service, string expression) : base(service)
    {
        ParseContent(expression);
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicCodeConditionalExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the language service instance.
    /// </param>
    /// <param name="codeLine">
    /// The <see cref="List{T}"/> of <see cref="IToken"/> instances from the parent code line instance 
    /// containing the data to be parsed.
    /// </param>
    public BlazorBasicUserDefinedItemExpression(BlazorBasicLanguageService service, List<IToken> codeLine) : base(service)
    {
        ParseCodeLine(codeLine);
    }
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        UserDefinedText = null;
        base.Dispose(disposing);
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets or sets the name of the procedure to call.
    /// </summary>
    /// <value>
    /// A string containing the name of the procedure to call.
    /// </value>
    public string? UserDefinedText { get; set; }
    #endregion

    #region Private Methods / Functions
    /// <summary>
    /// Parses the content expression into a parameter definition.
    /// </summary>
    /// <param name="expression">
    /// A string containing the expression to be parsed.
    /// </param>
    /// <exception cref="Adaptive.Intelligence.Shared.ExceptionEventArgs.Exception"></exception>
    private void ParseContent(string expression)
    {
        expression = expression.Trim();

        // Expected (abstract) format:
        // <procedureName><space>[(][<parameter list expression>][)]
    }

    protected override void ParseLiteralContent(string? expression)
    {
        UserDefinedText = expression;
    }
    /// <summary>
    /// Parses the code line.
    /// </summary>
    /// <param name="codeLine">A <see cref="List{T}" /> of <see cref="IToken" /> instances containing the expression to be parsed.</param>
    protected void ParseCodeLine(List<IToken> codeLine)
    {
        StringBuilder builder = new StringBuilder();

        foreach(IToken token in codeLine)
        {
            builder.Append(token.Text);
        }

        UserDefinedText= builder.ToString();
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
        return UserDefinedText;
    }
    #endregion

    #region Public Methods / Functions
    /// <summary>
    /// Evaluates the expression.
    /// </summary>
    /// <param name="engine">
    /// The reference to the execution engine instance.
    /// </param>
    /// <param name="environment">
    /// The reference to the execution environment instance.
    /// </param>
    /// <param name="scope">
    /// The <see cref="IScopeContainer" /> instance, such as a procedure or function, in which scoped
    /// variables are declared.</param>
    /// <returns>
    /// A string containing the user-defined text value.
    /// </returns>
    public override object Evaluate(IExecutionEngine engine, IExecutionEnvironment environment, IScopeContainer scope)
    {
        return UserDefinedText;
    }

    protected override void ParseCodeLine(ITokenizedCodeLine codeLine, int startIndex, int endIndex)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Private Methods / Functions
    #endregion
}
