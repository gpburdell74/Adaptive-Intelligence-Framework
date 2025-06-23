using Adaptive.Intelligence.BlazorBasic.Services;
using Adaptive.Intelligence.LanguageService.Tokenization;

namespace Adaptive.Intelligence.BlazorBasic.CodeDom;

/// <summary>
/// Represents a (clear screen) command statement.
/// </summary>
/// <example>
///     CLS
/// </example>
/// <seealso cref="BasicCodeStatement" />
public class BasicClsStatement : BasicCodeStatement
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicClsStatement"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public BasicClsStatement(BlazorBasicLanguageService service) : base(service)
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicClsStatement"/> class.
    /// </summary>
    /// <param name="codeLine">An <see cref="ITokenizedCodeLine" /> containing the code line to be parsed.</param>
    public BasicClsStatement(BlazorBasicLanguageService service, ITokenizedCodeLine codeLine) : base(service,codeLine)
    {
    }
    #endregion

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
    }
    #endregion
}
