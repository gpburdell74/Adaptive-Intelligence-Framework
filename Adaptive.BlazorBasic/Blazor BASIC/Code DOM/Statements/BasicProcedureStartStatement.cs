using Adaptive.BlazorBasic.LanguageService;

namespace Adaptive.BlazorBasic.CodeDom;

/// <summary>
/// Represents the declaration of a procedure (PROCEDURE [Name](...) ).
/// </summary>
/// <seealso cref="BasicCodeStatement" />
public class BasicProcedureStartStatement : BasicCodeStatement
{
    #region Private Member Declarations
    /// <summary>
    /// The procedure name.
    /// </summary>
    private string? _procedureName;
    #endregion

    #region Constructor / Dispose Methods    
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicProcedureStartStatement"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public BasicProcedureStartStatement() : base()
    {

    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicProcedureStartStatement"/> class.
    /// </summary>
    /// <param name="codeLine">An <see cref="ITokenizedCodeLine" /> containing the code line to be parsed.</param>
    public BasicProcedureStartStatement(ITokenizedCodeLine codeLine) : base(codeLine)
    {
    }
    #endregion

    #region Public Properties    
    /// <summary>
    /// Gets the name of the procedure.
    /// </summary>
    /// <value>
    /// A string containing the name of the procedure.
    /// </value>
    public string ProcedureName => _procedureName;
    #endregion

    #region Protected Method Overrides    
    /// <summary>
    /// Parses the specified code content.
    /// </summary>
    /// <param name="codeLine">
    /// The <see cref="ITokenizedCodeLine"/> instance to be parsed into its component expressions.
    /// </param>
    protected override void ParseIntoExpressions(ITokenizedCodeLine codeLine)
    {
        // Expected format:
        //
        // PROCEDURE <name> ([parameters])
        if (codeLine[1].TokenType == TokenType.SeparatorDelimiter)
        {
            // Procedure name should be token 2:
            if (codeLine.Count < 3 || codeLine[2].TokenType != TokenType.ProcedureName)
            {
                throw new Exception("Procedure name is missing");
            }
            else
            {
                _procedureName = codeLine[2].Text;
                int start = 2;
                while (codeLine[start].TokenType != TokenType.ExpressionStartDelimiter)
                {
                    start++;
                    if (start >= codeLine.Count)
                    {
                        throw new Exception("Error in declaring procedure: missing opening parenthesis");
                    }
                }
                ProcessParameterExpressions(codeLine, start);
            }
        }
        else
        {
            throw new Exception("Error in declaring procedure");
        }
    }

    private void ProcessParameterExpressions(ITokenizedCodeLine codeLine, int startIndex)
    {
        bool endFound = false;
        int nextToken = startIndex + 1;
        do
        {
            IToken token = codeLine[nextToken];
            endFound = (token.TokenType == TokenType.ExpressionEndDelimiter);
            if (!endFound)
            {
                // Todo:!
                int s = 1;
            }
        } while (!endFound);


    }
    #endregion

}
