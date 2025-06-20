using Adaptive.BlazorBasic.LanguageService;

namespace Adaptive.BlazorBasic.CodeDom;

/// <summary>
/// Represents the command to start a DO ... loop.
/// </summary>
/// <example>
///     DO
/// </example>
/// <seealso cref="BasicCodeStatement" />
public class BasicDoStatement : BasicCodeStatement 
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicDoStatement"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public BasicDoStatement()
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicDoStatement"/> class.
    /// </summary>
    /// <param name="codeLine">An <see cref="ITokenizedCodeLine" /> containing the code line to be parsed.</param>
    public BasicDoStatement(ITokenizedCodeLine codeLine) : base(codeLine)
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
        if (codeLine.Count > 1)
            throw new Exception("?SYNTAX ERROR");
    }
    #endregion
}
