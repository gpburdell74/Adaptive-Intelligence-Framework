using Adaptive.BlazorBasic.LanguageService.CodeDom;
using Adaptive.BlazorBasic.Services;
using Adaptive.Intelligence.Shared;
using Adaptive.LanguageService.Tokenization;

namespace Adaptive.BlazorBasic.CodeDom;

/// <summary>
/// Represents a string literal.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="ILanguageCodeExpression" />
public class BasicLiteralStringExpression : BasicExpression, ILanguageCodeExpression
{
    #region Private Member Declarations    
    /// <summary>
    /// The literal string value.
    /// </summary>
    private string? _value;
    #endregion

    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicLiteralStringExpression"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public BasicLiteralStringExpression(BlazorBasicLanguageService service) : base(service)
    {
        _value = string.Empty;
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicLiteralStringExpression"/> class.
    /// </summary>
    /// <param name="stringContent">
    /// A string containing the data type name.
    /// </param>
    public BasicLiteralStringExpression(BlazorBasicLanguageService service, string stringContent) : base(service)
    {
        _value = stringContent;
    }
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        _value = null;
        base.Dispose(disposing);
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

    #region Public Properties    
    /// <summary>
    /// Gets the string literal being represented.
    /// </summary>
    /// <value>
    /// A string containing the literal value.
    /// </value>
    public string? Content => _value;
    #endregion
}
