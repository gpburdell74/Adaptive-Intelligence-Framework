using Adaptive.Intelligence.BlazorBasic.CodeDom.Statements;
using Adaptive.Intelligence.BlazorBasic.Services;
using Adaptive.Intelligence.LanguageService.CodeDom;
using Adaptive.Intelligence.LanguageService.Tokenization;

namespace Adaptive.Intelligence.BlazorBasic.CodeDom;

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
    public BasicDoStatement(BlazorBasicLanguageService service) : base(service)
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicDoStatement"/> class.
    /// </summary>
    /// <param name="codeLine">An <see cref="ITokenizedCodeLine" /> containing the code line to be parsed.</param>
    public BasicDoStatement(BlazorBasicLanguageService service, ITokenizedCodeLine codeLine) : base(service,codeLine)
    {
    }
    #endregion

    public override RenderTabState TabModification => RenderTabState.IndentAfter;


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

    #region Public Methods / Functions    
    /// <summary>
    /// Renders the content of the expression into a string.
    /// </summary>
    /// <returns>
    /// A string containing the expression rendered into Blazor BASIC code.
    /// </returns>
    public override string? Render()
    {
        return KeywordNames.CommandDo;
    }
    #endregion
}
