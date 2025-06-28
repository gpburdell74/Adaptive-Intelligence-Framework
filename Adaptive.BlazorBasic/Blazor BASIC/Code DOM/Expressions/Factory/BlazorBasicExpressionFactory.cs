using Adaptive.Intelligence.BlazorBasic.Parser;
using Adaptive.Intelligence.BlazorBasic.Services;
using Adaptive.Intelligence.LanguageService.Tokenization;
using System.Text;

namespace Adaptive.Intelligence.BlazorBasic.CodeDom;

/// <summary>
/// Provides static methods / functions for creating CodeDOM expression instances.
/// </summary>
public static class BlazorBasicExpressionFactory
{
    #region Public Factory Methods / Functions
    /// <summary>
    /// Creates the expression from a single token.
    /// </summary>
    /// <param name="service">
    /// The reference to the language service instance.
    /// </param>
    /// <param name="lineNumber">
    /// An integer specifying the current line number.
    /// </param>
    /// <param name="token">
    /// The reference to the <see cref="IToken"/> instance.
    /// </param>
    /// <returns>
    /// A <see cref="BlazorBasicExpression"/> instance containing the CodeDom expression.
    /// </returns>
    public static BlazorBasicExpression CreateExpressionFromSingleToken(BlazorBasicLanguageService service, int lineNumber, IToken token)
    {
        BlazorBasicExpression expression;

        ManagedTokenList singleItemList = new ManagedTokenList { token };

        switch (token.TokenType)
        {
            case TokenType.ProcedureName:
                expression = new BlazorBasicProcedureCallExpression(service, singleItemList);
                break;

            case TokenType.ReservedWord:
                expression = new BlazorBasicReservedWordExpression(service, singleItemList);
                break;

            case TokenType.ReservedFunction:
                expression = new BlazorBasicReservedFunctionExpression(service, singleItemList);
                break;

            case TokenType.FunctionName:
                expression = new BlazorBasicFunctionCallExpression(service, singleItemList);
                break;

            case TokenType.VariableName:
                expression = new BlazorBasicVariableNameExpression(service, singleItemList);
                break;

            case TokenType.UserDefinedItem:
                expression = new BlazorBasicUserDefinedItemExpression(service, singleItemList);
                break;

            case TokenType.Integer:
                expression = new BlazorBasicLiteralIntegerExpression(service, singleItemList);
                break;

            case TokenType.FloatingPoint:
                expression = new BlazorBasicLiteralFloatingPointExpression(service, singleItemList);
                break;

            default:
                throw new SyntaxErrorException(lineNumber);
        }
        return expression;
    }

    /// <summary>
    /// Creates the expression object from the provided list of tokens.
    /// </summary>
    /// <param name="service">
    /// The reference to the language service instance.
    /// </param>
    /// <param name="tokenList">
    /// The <see cref="List{T}"/> of <see cref="IToken"/> instances.
    /// </param>
    /// <returns>
    /// A <see cref="BlazorBasicExpression"/> instance.
    /// </returns>
    public static BlazorBasicExpression CreateFromTokens(BlazorBasicLanguageService service, int lineNumber, ManagedTokenList tokenList,
        int startIndex = 0)
    {
        BlazorBasicExpression newExpression;
        // Create the sub-list as specified by the starting index.
        ManagedTokenList subList = tokenList.CreateCopy(startIndex).Trim();

        // Cases: one singular token for the expression.
        if (subList.Count == 1)
        {
            newExpression = CreateExpressionFromSingleToken(service, lineNumber, subList[0]);
        }
        // Cases: a literal string such as "ABCDE"
        else if (subList.IsSingleString())
        {
            newExpression = CreateStringLiteralExpression(service, lineNumber, subList);
        }
        // Cases: a literal character such as `x`
        else if (subList.IsSingleCharList())
        {
            newExpression = CreateCharLiteralExpression(service, lineNumber, subList);
        }
        // Case: an arithmetic expression.
        else if (subList.IsArithmeticExpression())
        {
            newExpression = CreateArithmeticExpression(service, lineNumber, subList);
        }
        else
        {
            StringBuilder builder = new StringBuilder();
            for (int index = startIndex; index < tokenList.Count; index++)
            {
                builder.Append(tokenList[index].Text);
            }

            string original = builder.ToString();
            switch (tokenList[startIndex].TokenType)
            {

                case TokenType.ProcedureName:
                    newExpression = new BlazorBasicProcedureCallExpression(service, tokenList);
                    break;

                case TokenType.ReservedWord:
                    newExpression = new BlazorBasicReservedWordExpression(service, tokenList);
                    break;

                case TokenType.ReservedFunction:
                    newExpression = new BlazorBasicReservedFunctionExpression(service, tokenList);
                    break;

                case TokenType.FunctionName:
                    newExpression = new BlazorBasicFunctionCallExpression(service, tokenList);
                    break;

                case TokenType.VariableName:
                    newExpression = new BlazorBasicVariableNameExpression(service, subList);
                    break;

                case TokenType.UserDefinedItem:
                    newExpression = new BlazorBasicUserDefinedItemExpression(service, tokenList);
                    break;

                case TokenType.Integer:
                    newExpression = new BlazorBasicLiteralIntegerExpression(service, subList);
                    break;

                case TokenType.FloatingPoint:
                    newExpression = new BlazorBasicLiteralFloatingPointExpression(service, tokenList);
                    break;

                case TokenType.CharacterDelimiter:
                    IToken t = tokenList[startIndex + 1];
                    if (t.TokenType == TokenType.CharacterDelimiter)
                        newExpression = new BlazorBasicLiteralCharacterExpression(service, (char)0);
                    else
                        newExpression = new BlazorBasicLiteralCharacterExpression(service, tokenList[startIndex].Text[0]);
                    break;

                case TokenType.StringDelimiter:
                    IToken stringToken = tokenList[startIndex + 1];
                    if (stringToken.TokenType == TokenType.StringDelimiter)
                        newExpression = new BlazorBasicLiteralStringExpression(service, string.Empty);
                    else
                        newExpression = new BlazorBasicLiteralStringExpression(service, stringToken.Text);
                    break;

                default:
                    throw new SyntaxErrorException(lineNumber);
            }
        }
        return newExpression;
    }
    public static BlazorBasicExpression CreateArithmeticExpression
        (BlazorBasicLanguageService service, int lineNumber, ManagedTokenList subList)
    {
        BlazorBasicExpressionParser parser = new BlazorBasicExpressionParser(service);
        
        List<BlazorBasicExpression> list = parser.ParseExpression(subList);

        while (list.Count > 3)
        {
            BlazorBasicBasicArithmeticExpression subExpression = new BlazorBasicBasicArithmeticExpression(
                service,
                list[0],
                ((BlazorBasicArithmeticOperatorExpression)list[1]).Operator,
                list[2]);

            list.RemoveRange(0, 3);
            list.Insert(0, subExpression);
        }

        if (list.Count < 3)
            list.Add(new BlazorBasicLiteralIntegerExpression(service, 0));

        BlazorBasicBasicArithmeticExpression finalExpression = new BlazorBasicBasicArithmeticExpression(
            service,
            list[0],
            ((BlazorBasicArithmeticOperatorExpression)list[1]).Operator,
            list[2]);

        return finalExpression;
    }

    public static BlazorBasicLiteralCharacterExpression CreateCharLiteralExpression
        (BlazorBasicLanguageService service, int lineNumber, ManagedTokenList subList)
    {
        return new BlazorBasicLiteralCharacterExpression(service, subList[1].Text[0]);
    }

    public static ExpressionNodeTree CreateExpressionTree(BlazorBasicLanguageService service, ManagedTokenList list)
    {
        ExpressionNodeTree tree = new ExpressionNodeTree();
        ManagedTokenList simpleList = list.RemoveSeparators();

        List<BlazorBasicExpression> expressionList = new List<BlazorBasicExpression>();
        int index = 0;
        while (index < simpleList.Count)
        {
            List<BlazorBasicExpression> subList = ParseExpression(service, simpleList, ref index, tree);
            BlazorBasicComplexExpression exp = new BlazorBasicComplexExpression(service);
            exp.Expressions = subList;
            expressionList.Add(exp);
        }

        return tree;
    }

    public static BlazorBasicLiteralStringExpression CreateStringLiteralExpression
       (BlazorBasicLanguageService service, int lineNumber, ManagedTokenList subList)
    {
        StringBuilder builder = new StringBuilder();

        for (int index = 1; index < subList.Count - 2; index++)
        {
            builder.Append(subList[index].Text);
        }
        return new BlazorBasicLiteralStringExpression(service, builder.ToString());
    }

    
    public static List<BlazorBasicExpression> ParseExpression(BlazorBasicLanguageService service, ManagedTokenList list, ref int index, ExpressionNodeTree tree)
    {
        List<BlazorBasicExpression> subExpressionList = new List<BlazorBasicExpression>();
        int length = list.Count;
        bool done = false;
        if (index < list.Count)
        {
            do
            {
                IToken token = list[index];
                BlazorBasicExpression? expression = null;

                switch (token.TokenType)
                {
                    case TokenType.ExpressionStartDelimiter:
                        index++;
                        List<BlazorBasicExpression> expList = ParseExpression(service, list, ref index, tree);
                        expression = new BlazorBasicComplexExpression(service);
                        ((BlazorBasicComplexExpression)expression).Expressions = expList;
                        subExpressionList.Add(expression);
                        break;

                    case TokenType.ExpressionEndDelimiter:
                        index++;
                        done = true;
                        break;

                    case TokenType.ArithmeticOperator:
                        index++;
                        expression = new BlazorBasicArithmeticOperatorExpression(service, token.Text);
                        subExpressionList.Add(expression);
                        break;

                    case TokenType.VariableName:
                        index++;
                        expression = new BlazorBasicVariableNameExpression(service, new ManagedTokenList { token });
                        subExpressionList.Add(expression);
                        break;

                    case TokenType.FloatingPoint:
                        index++;
                        expression = new BlazorBasicLiteralFloatingPointExpression(service, new ManagedTokenList { token });
                        subExpressionList.Add(expression);
                        break;

                    case TokenType.Integer:
                        index++;
                        expression = new BlazorBasicLiteralIntegerExpression(service, new ManagedTokenList { token });
                        subExpressionList.Add(expression);
                        break;

                    default:
                        break;
                }
                
            } while (index < length && !done);
        }
        
        return subExpressionList;
    }
    #endregion
}
