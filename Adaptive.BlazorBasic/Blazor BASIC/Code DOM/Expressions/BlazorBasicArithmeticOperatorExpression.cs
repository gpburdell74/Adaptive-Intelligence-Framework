using Adaptive.Intelligence.BlazorBasic.Services;
using Adaptive.Intelligence.LanguageService.CodeDom;
using Adaptive.Intelligence.LanguageService.Tokenization;

namespace Adaptive.Intelligence.BlazorBasic.CodeDom;

/// <summary>
/// Represents a data type expression.
/// </summary>
/// <seealso cref="BlazorBasicExpression" />
/// <seealso cref="ILanguageCodeExpression" />
public class BlazorBasicArithmeticOperatorExpression : BlazorBasicExpression, ILanguageCodeExpression
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicArithmeticOperatorExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the language service instance being injected.
    /// </param>
    public BlazorBasicArithmeticOperatorExpression(BlazorBasicLanguageService service) : base(service)
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicArithmeticOperatorExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the language service instance being injected.
    /// </param>
    /// <param name="dataTypeSpec">
    /// A string containing the data type name.
    /// </param>
    public BlazorBasicArithmeticOperatorExpression(BlazorBasicLanguageService service, string expression) : base(service, expression)
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicArithmeticOperatorExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the language service instance being injected.
    /// </param>
    /// <param name="codeLine">A <see cref="ITokenizedCodeLine" /> containing the code tokens for the entire line of code.</param>
    /// <param name="startIndex">An integer indicating the ordinal position in <paramref name="codeLine" /> to start parsing the expression.</param>
    /// <param name="endIndex">An integer indicating the ordinal position in <paramref name="codeLine" /> to end parsing the expression.</param>
    public BlazorBasicArithmeticOperatorExpression(
        BlazorBasicLanguageService service,
        ITokenizedCodeLine codeLine,
        int startIndex,
        int endIndex) : base(service, codeLine, startIndex, endIndex)
    {
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets or sets the type of the data.
    /// </summary>
    /// <value>
    /// A <see cref="BlazorBasicMathOperators"/> enumerated value indicating the data type.
    /// </value>
    public BlazorBasicMathOperators Operator { get; set; } = BlazorBasicMathOperators.Unknown;
    #endregion

    #region Protected Methods    
    /// <summary>
    /// Parses the content expression into a parameter definition.
    /// </summary>
    /// <param name="expression">
    /// A string containing the expression to be parsed.
    /// </param>
    protected override void ParseLiteralContent(string expression)
    {
        Operator = BlazorBasicOperatorFactory.GetMathOperator(expression);
    }

    /// <summary>
    /// Parses the code line.
    /// </summary>
    /// <param name="codeLine">
    /// A <see cref="ITokenizedCodeLine" /> containing the code tokens for the entire line of code.
    /// </param>
    /// <param name="startIndex">
    /// An integer indicating the ordinal position in <paramref name="codeLine" /> to start parsing the expression.
    /// </param>
    /// <param name="endIndex">
    /// An integer indicating the ordinal position in <paramref name="codeLine" /> to end parsing the expression.
    /// </param>
    protected override void ParseCodeLine(ITokenizedCodeLine codeLine, int startIndex, int endIndex)
    {
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
        return BlazorBasicOperatorFactory.GetOperatorText(Operator);
    }
    #endregion
}
