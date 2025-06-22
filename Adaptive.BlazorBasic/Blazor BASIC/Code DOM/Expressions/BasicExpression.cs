using Adaptive.BlazorBasic.LanguageService.CodeDom;
using Adaptive.BlazorBasic.Services;
using Adaptive.Intelligence.Shared;
using Adaptive.LanguageService.Tokenization;

namespace Adaptive.BlazorBasic.CodeDom;

/// <summary>
/// Provides the signature definition and base implementation for Blazor BASIC code expressions.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="ILanguageCodeExpression" />
public abstract class BasicExpression : DisposableObjectBase, ILanguageCodeExpression
{
    #region Private Member Declarations
    /// <summary>
    /// The reference to the language service.
    /// </summary>
    private BlazorBasicLanguageService? _service;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the <see cref="BlazorBasicLanguageService"/> to use to find and compare
    /// text to language reserved words and operators and other items.
    /// </param>
    protected BasicExpression(BlazorBasicLanguageService service)
    {
        _service = service;
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the <see cref="BlazorBasicLanguageService"/> to use to find and compare
    /// text to language reserved words and operators and other items.
    /// </param>
    /// <param name="expression">
    /// A string containing the expression to be parsed.
    /// </param>
    protected BasicExpression(BlazorBasicLanguageService service, string expression) : base()
    {
        _service = service;
        ParseLiteralContent(expression);
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicCodeConditionalExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the <see cref="BlazorBasicLanguageService"/> to use to find and compare
    /// text to language reserved words and operators and other items.
    /// </param>
    /// <param name="codeLine">
    /// A <see cref="ITokenizedCodeLine"/> containing the code tokens for the entire line of code.
    /// </param>
    /// <param name="startIndex">
    /// An integer indicating the ordinal position in <paramref name="codeLine"/> to start parsing the expression.
    /// </param>
    /// <param name="endIndex">
    /// An integer indicating the ordinal position in <paramref name="codeLine"/> to end parsing the expression.
    /// </param>
    protected BasicExpression(BlazorBasicLanguageService service, ITokenizedCodeLine codeLine, int startIndex, int endIndex) : base()
    {
        _service = service;
        ParseCodeLine(codeLine, startIndex, endIndex);
    }
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        _service = null;
        base.Dispose(disposing);
    }
    #endregion

    #region Protected Properties
    /// <summary>
    /// Gets the reference to the language service.
    /// </summary>
    /// <value>
    /// The reference to the <see cref="BlazorBasicLanguageService"/> to use to find and compare
    /// text to language reserved words and operators and other items.
    /// </value>
    protected BlazorBasicLanguageService Service
    {
        get
        {
            if (_service == null)
                throw new Exception("Engine Error!");
            return _service;
        }
    }
    #endregion

    #region Protected Abstract Methods
    /// <summary>
    /// Parses the content expression into a parameter definition.
    /// </summary>
    /// <param name="expression">
    /// A string containing the expression to be parsed.
    /// </param>
    protected abstract void ParseLiteralContent(string? expression);
    /// <summary>
    /// Parses the code line.
    /// </summary>
    /// <param name="codeLine">
    /// A <see cref="ITokenizedCodeLine"/> containing the code tokens for the entire line of code.
    /// </param>
    /// <param name="startIndex">
    /// An integer indicating the ordinal position in <paramref name="codeLine"/> to start parsing the expression.
    /// </param>
    /// <param name="endIndex">
    /// An integer indicating the ordinal position in <paramref name="codeLine"/> to end parsing the expression.
    /// </param>
    protected abstract void ParseCodeLine(ITokenizedCodeLine codeLine, int startIndex, int endIndex);
    #endregion

    #region Protected Methods / Functions    
    /// <summary>
    /// Normalizes the string for parsing.
    /// </summary>
    /// <param name="expression">
    /// A string containing the expression to be parsed.
    /// </param>
    /// <returns>
    /// The modified string.
    /// </returns>
    protected string? NormalizeString(string? expression)
    {
        if (expression == null)
            return null;

        return expression.Trim();
    }
    #endregion

}