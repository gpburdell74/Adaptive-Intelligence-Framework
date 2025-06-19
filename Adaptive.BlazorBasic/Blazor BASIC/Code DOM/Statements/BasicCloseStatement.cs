using Adaptive.BlazorBasic.LanguageService;

namespace Adaptive.BlazorBasic.CodeDom;

/// <summary>
/// Represents a CLOSE statement.
/// </summary>
/// <example>
/// 
///     CLOSE #42
///     
/// </example>
/// <seealso cref="BasicCodeStatement" />
public class BasicCloseStatement : BasicCodeStatement
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicCloseStatement"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public BasicCloseStatement()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BasicCloseStatement"/> class.
    /// </summary>
    /// <param name="codeLine">An <see cref="ITokenizedCodeLine" /> containing the code line to be parsed.</param>
    public BasicCloseStatement(ITokenizedCodeLine codeLine) : base(codeLine)
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
