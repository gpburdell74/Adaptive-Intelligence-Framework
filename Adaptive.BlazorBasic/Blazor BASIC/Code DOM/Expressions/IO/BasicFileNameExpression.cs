using Adaptive.Intelligence.BlazorBasic.Services;
using Adaptive.Intelligence.LanguageService.CodeDom;
using Adaptive.Intelligence.LanguageService.Tokenization;

namespace Adaptive.Intelligence.BlazorBasic.CodeDom;

/// <summary>
/// Represents an expression that renders a file path and name value.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="ILanguageCodeExpression" />
public class BasicFileNameExpression : BasicExpression, ILanguageCodeExpression
{
    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicFileNameExpression"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public BasicFileNameExpression(BlazorBasicLanguageService service) : base(service)
    {

    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BasicFileNameExpression"/> class.
    /// </summary>
    /// <param name="pathAndFileName">
    /// A string containing the fully-qualified path and name of the file.
    /// </param>
    public BasicFileNameExpression(BlazorBasicLanguageService service, string pathAndFileName) : base(service)
    {
        FileName = pathAndFileName;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BasicFileNameExpression"/> class.
    /// </summary>
    /// <param name="token">
    /// The <see cref="IToken"/> token instance to be parsed into a file path and name.
    /// </param>
    public BasicFileNameExpression(BlazorBasicLanguageService service, IToken token) : base(service)
    {

    }

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        FileName = null;
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
    /// Gets or sets the path and name of the file.
    /// </summary>
    /// <value>
    /// A string containing the fully-qualified path and name of the file, or <b>null</b> if not specified.
    /// </value>
    public string? FileName { get; set; }
    #endregion

}
