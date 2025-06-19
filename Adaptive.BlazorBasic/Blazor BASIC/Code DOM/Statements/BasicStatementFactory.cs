using Adaptive.BlazorBasic.LanguageService;
using Adaptive.BlazorBasic.LanguageService.CodeDom;

namespace Adaptive.BlazorBasic.CodeDom;

/// <summary>
/// Provides static methods / functions for creating <see cref="BasicCodeStatement"/> derived instances.
/// </summary>
public static class BasicStatementFactory
{
    #region Public Static Methods / Functions
    /// <summary>
    /// Creates the statement object based on the provide code line and the starting command token.
    /// </summary>
    /// <param name="codeLine">
    /// An <see cref="ITokenizedCodeLine"/> instance containing the content to be transformed.
    /// </param>
    /// <returns>
    /// An <see cref="ILanguageCodeStatement"/> instance representing the code statement created from the provided code line.
    /// </returns>
    public static ILanguageCodeStatement? CreateStatementByCommand(ILanguageService<BlazorBasicFunctions, BlazorBasicKeywords> service, ITokenizedCodeLine codeLine)
    {
        ILanguageCodeStatement? newStatement = null;

        IToken commandToken = codeLine[0];
        TokenType type = commandToken.TokenType;

        switch (type)
        {
            case TokenType.FunctionName:
                break;

            case TokenType.ProcedureName:
                break;

            case TokenType.ReservedFunction:
                break;

            case TokenType.ReservedWord:
                newStatement = CreateStatementByReservedWord(service, codeLine);
                break;

            case TokenType.VariableName:
                break;

            default:
                throw new Exception();
                break;
        }

        return newStatement;
    }
    #endregion

    #region Private Static Methods / Functions

    /// <summary>
    /// Creates the statement object for the specified reserved word value.
    /// </summary>
    /// <param name="service">
    /// The reference to the <see cref="ILanguageService{FunctionsEnum, KeywordsEnum}"/> instance.
    /// </param>
    /// <param name="codeLine">
    /// A string containing the code line to be parsed.
    /// </param>
    /// <returns>
    /// The <see cref="ILanguageCodeStatement"/> instance, or <b>null</b> if the operation fails.
    /// </returns>
    private static ILanguageCodeStatement? CreateStatementByReservedWord(ILanguageService<BlazorBasicFunctions, BlazorBasicKeywords> service, ITokenizedCodeLine codeLine)
    {
        ILanguageCodeStatement? newStatement = null;

        string command = codeLine[0].Text.ToLower().Trim();
        BlazorBasicKeywords keyword = service.Keywords.GetKeywordType(command);

        switch (keyword)
        {
            case BlazorBasicKeywords.Close:
                newStatement = new BasicCloseStatement(codeLine);
                break;

            case BlazorBasicKeywords.Cls:
                newStatement = new BasicClsStatement(codeLine);
                break;

            case BlazorBasicKeywords.Comment:
                newStatement = new BasicCommentStatement(codeLine);
                break;

            case BlazorBasicKeywords.Open:
                newStatement = new BasicOpenStatement(codeLine);
                break;

            case BlazorBasicKeywords.Procedure:
                newStatement = new BasicProcedureStartStatement(codeLine);
                break;

            default:
                throw new Exception("Not yet implemented.");
        }

        return newStatement;
    }
    #endregion

}
