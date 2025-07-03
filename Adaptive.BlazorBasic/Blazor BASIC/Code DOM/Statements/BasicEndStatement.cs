using Adaptive.Intelligence.BlazorBasic.Services;
using Adaptive.Intelligence.LanguageService.CodeDom;
using Adaptive.Intelligence.LanguageService.Tokenization;
using System.Text;

namespace Adaptive.Intelligence.BlazorBasic.CodeDom;

/// <summary>
/// Represents the command to END a block section, such as IF, or PROCEDURE or FUNCTION.
/// </summary>
/// <example>
///     END IF
///     
///     END PROCEDURE
///     
///     END FUNCTION
///     
/// </example>
/// <seealso cref="BasicCodeStatement" />
public class BasicEndStatement : BasicCodeStatement
{
    #region Private Member Declarations
    /// <summary>
    /// The expression representing the block type.
    /// </summary>
    private BlazorBasicReservedWordExpression _expression;
    /// <summary>
    /// The block type.
    /// </summary>
    private BlockType _blockType;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicEndStatement"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public BasicEndStatement(BlazorBasicLanguageService service) : base(service)
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicEndStatement"/> class.
    /// </summary>
    /// <param name="codeLine">An <see cref="ITokenizedCodeLine" /> containing the code line to be parsed.</param>
    public BasicEndStatement(BlazorBasicLanguageService service, ITokenizedCodeLine codeLine) : base(service, codeLine)
    {
    }
    #endregion

    /// <summary>
    /// Gets the type of the block.
    /// </summary>
    /// <value>
    /// The type of the block.
    /// </value>
    public BlockType BlockType => _blockType;
    /// <summary>
    /// Gets the value of how the current number of tabs being printed is to be modified.
    /// </summary>
    /// <value>
    /// The tab modification.
    /// </value>
    public override RenderTabState TabModification => RenderTabState.Exdent;

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
        if (codeLine.Count < 3)
            throw new Exception("?SYNTAX ERROR");

        IToken token = codeLine[2];
        _blockType = Service.DetermineBlockType(token.Text);
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
        StringBuilder builder = new StringBuilder();
        builder.Append(KeywordNames.CommandEnd);
        builder.Append(ParseConstants.Space);
        switch (_blockType)
        {
            case BlockType.If:
                builder.Append(KeywordNames.CommandIf);
                break;

            case BlockType.Procedure:
                builder.Append(KeywordNames.CommandProcedure);
                break;

            case BlockType.Function:
                builder.Append(KeywordNames.CommandFunction);
                break;
        }
        return builder.ToString();
    }
    #endregion
}
