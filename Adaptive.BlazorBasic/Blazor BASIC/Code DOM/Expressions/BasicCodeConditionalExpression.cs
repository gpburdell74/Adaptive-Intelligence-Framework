using Adaptive.BlazorBasic.LanguageService.CodeDom;
using Adaptive.BlazorBasic.Services;
using Adaptive.Intelligence.Shared;
using Adaptive.LanguageService;
using Adaptive.LanguageService.Tokenization;

namespace Adaptive.BlazorBasic.CodeDom;

/// <summary>
/// Represents a conditional expression in Blazor BASIC.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="ILanguageCodeExpression" />
public class BasicCodeConditionalExpression : BasicExpression, ILanguageCodeExpression
{
    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicCodeConditionalExpression"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public BasicCodeConditionalExpression(BlazorBasicLanguageService service) : base(service)
    {

    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicCodeConditionalExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the language service instance.
    /// </param>
    /// <param name="expression">
    /// A string containing the expression to be parsed.
    /// </param>
    public BasicCodeConditionalExpression(BlazorBasicLanguageService service, string expression) : base(service)
    {
        ParseContent(expression);
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicCodeConditionalExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the language service instance.
    /// </param>
    /// <param name="codeLine">
    /// The <see cref="ITokenizedCodeLine"/> code line instance containing the data to be parsed.
    /// </param>
    public BasicCodeConditionalExpression(BlazorBasicLanguageService service, ITokenizedCodeLine codeLine)  :base(service)
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
            LeftExpression?.Dispose();
            RightExpression?.Dispose();
        }

        LeftExpression = null;
        RightExpression = null;
        base.Dispose(disposing);
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets or sets the reference to the left expression.
    /// </summary>
    /// <value>
    /// An <see cref="ILanguageCodeExpression"/> to be evaluated as the left-side of the conditional.
    /// </value>
    public ILanguageCodeExpression? LeftExpression { get; set; }

    /// <summary>
    /// Gets or sets the comparison operator.
    /// </summary>
    /// <value>
    /// A <see cref="StandardOperatorTypes"/> enumerated value indicating the operator to use.
    /// </value>
    public StandardOperatorTypes Operator { get; set; } = StandardOperatorTypes.Comparison;

    /// <summary>
    /// Gets or sets the reference to the right expression.
    /// </summary>
    /// <value>
    /// An <see cref="ILanguageCodeExpression"/> to be evaluated as the right-side of the conditional.
    /// </value>
    public ILanguageCodeExpression? RightExpression { get; set; }
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
        // <left-side expression>[space]<operator>[space]<right-side expression>
    }

    protected override void ParseLiteralContent(string? expression)
    {
        throw new NotImplementedException();
    }

    protected override void ParseCodeLine(ITokenizedCodeLine codeLine, int startIndex, int endIndex)
    {
        throw new NotImplementedException();
    }
    #endregion
}