using Adaptive.Intelligence.BlazorBasic.Services;
using Adaptive.Intelligence.LanguageService.CodeDom;
using Adaptive.Intelligence.LanguageService.Tokenization;

namespace Adaptive.Intelligence.BlazorBasic.CodeDom;

/// <summary>
/// Represents the code statement marking the end of a procedure (END PROCEDURE)
/// </summary>
/// <seealso cref="BasicCodeStatement" />
public class BasicProcedureEndStatement : BasicCodeStatement
{
    #region Constructors    
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicProcedureEndStatement"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public BasicProcedureEndStatement(BlazorBasicLanguageService service) : base(service)
    {

    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BasicProcedureEndStatement"/> class.
    /// </summary>
    /// <param name="codeLine">An <see cref="ITokenizedCodeLine" /> containing the code line to be parsed.</param>
    public BasicProcedureEndStatement(BlazorBasicLanguageService service, ITokenizedCodeLine codeLine) : base(service, codeLine)
    {

    }
    #endregion

    #region Protected Method Overrides    
    /// <summary>
    /// Parses the specified code content.
    /// </summary>
    /// <param name="codeLine">A string containing the code to be parsed.</param>
    protected override void ParseIntoExpressions(ITokenizedCodeLine codeLine)
    {

    }
    #endregion


    /// <summary>
    /// Gets the value of how the current number of tabs being printed is to be modified.
    /// </summary>
    /// <value>
    /// The tab modification.
    /// </value>
    public override RenderTabState TabModification => RenderTabState.Exdent;

    #region Public Methods / Functions    
    /// <summary>
    /// Renders the content of the expression into a string.
    /// </summary>
    /// <returns>
    /// A string containing the expression rendered into Blazor BASIC code.
    /// </returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public override string? Render()
    {
        throw new NotImplementedException();
    }
    #endregion
}
