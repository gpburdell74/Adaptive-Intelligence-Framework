using Adaptive.Intelligence.BlazorBasic.Services;
using Adaptive.Intelligence.LanguageService.CodeDom;
using Adaptive.Intelligence.LanguageService.Tokenization;

namespace Adaptive.Intelligence.BlazorBasic.CodeDom;

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
    public static ILanguageCodeStatement? CreateStatementByCommand(BlazorBasicLanguageService service, ITokenizedCodeLine codeLine)
    {
        ILanguageCodeStatement? newStatement = null;

        if (codeLine.Count == 0)
            newStatement = new BlazorBasicEmptyStatement(service, codeLine);
        else
        {
            IToken? commandToken = codeLine[0];
            if (commandToken != null)
            {
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
                        if (codeLine[2].TokenType == TokenType.AssignmentOperator)
                        {
                            newStatement = CreateStatementByReservedWord(service, codeLine);
                        }
                        break;

                    default:
                        throw new Exception();
                }
            }
        }
        return newStatement;
    }
    #endregion

    #region Private Static Methods / Functions

    /// <summary>
    /// Creates the statement object for the specified reserved word value.
    /// </summary>
    /// <param name="service">
    /// The reference to the <see cref="BlazorBasicLanguageService"/> instance.
    /// </param>
    /// <param name="codeLine">
    /// A string containing the code line to be parsed.
    /// </param>
    /// <returns>
    /// The <see cref="ILanguageCodeStatement"/> instance, or <b>null</b> if the operation fails.
    /// </returns>
    private static ILanguageCodeStatement? CreateStatementByReservedWord(BlazorBasicLanguageService service, ITokenizedCodeLine codeLine)
    {
        ILanguageCodeStatement? newStatement = null;

        string command = codeLine[0].Text.ToLower().Trim();
        BlazorBasicKeywords keyword = service.Keywords.GetKeywordType(command);

        switch (keyword)
        {
            case BlazorBasicKeywords.Close:
                newStatement = new BasicCloseStatement(service, codeLine);
                break;

            case BlazorBasicKeywords.Cls:
                newStatement = new BasicClsStatement(service, codeLine);
                break;

            case BlazorBasicKeywords.Comment:
                newStatement = new BasicCommentStatement(service, codeLine);
                break;

            case BlazorBasicKeywords.Dim:
                newStatement = new BasicVariableDeclarationStatement(service, codeLine);
                break;

            case BlazorBasicKeywords.Do:
                newStatement = new BasicDoStatement(service, codeLine);
                break;

            case BlazorBasicKeywords.End:
                newStatement = new BasicEndStatement(service, codeLine);
                break;

            case BlazorBasicKeywords.If:
                newStatement = new BasicIfStatement(service, codeLine);
                break;

            case BlazorBasicKeywords.Function:
                newStatement = new BasicFunctionStartStatement(service, codeLine);
                break;

            case BlazorBasicKeywords.Input:
                newStatement = new BasicInputStatement(service, codeLine);
                break;

            case BlazorBasicKeywords.Let:
                newStatement = new BasicVariableAssignmentExpression(service, codeLine);
                break;

            case BlazorBasicKeywords.Loop:
                newStatement = new BasicLoopStatement(service, codeLine);
                break;

            case BlazorBasicKeywords.Open:
                newStatement = new BasicOpenStatement(service, codeLine);
                break;

            case BlazorBasicKeywords.Print:
                newStatement = new BasicPrintStatement(service, codeLine);
                break;

            case BlazorBasicKeywords.Procedure:
                newStatement = new BasicProcedureStartStatement(service, codeLine);
                break;

            case BlazorBasicKeywords.Return:
                newStatement = new BasicReturnStatement(service, codeLine);
                break;

            case BlazorBasicKeywords.Write:
                newStatement = new BasicWriteStatement(service, codeLine);
                break;

            default:
                if (codeLine[0].TokenType == TokenType.VariableName)
                {
                    newStatement = new BasicVariableAssignmentExpression(service, codeLine);
                }
                else
                    throw new Exception("Not yet implemented.");
                break;
        }

        return newStatement;
    }
    #endregion

}
