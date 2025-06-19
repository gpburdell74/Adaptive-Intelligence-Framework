using Adaptive.BlazorBasic.LanguageService;

namespace Adaptive.BlazorBasic.CodeDom;

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
    public BasicProcedureEndStatement()
    {

    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BasicProcedureEndStatement"/> class.
    /// </summary>
    /// <param name="codeLine">An <see cref="ITokenizedCodeLine" /> containing the code line to be parsed.</param>
    public BasicProcedureEndStatement(ITokenizedCodeLine codeLine) : base(codeLine)
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
}
