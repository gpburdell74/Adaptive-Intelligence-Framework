using Adaptive.Intelligence.BlazorBasic.CodeDom.Expressions;
using Adaptive.Intelligence.BlazorBasic.Parser;
using Adaptive.Intelligence.BlazorBasic.Services;
using Adaptive.Intelligence.LanguageService.CodeDom.Expressions;
using Adaptive.Intelligence.LanguageService.Services;
using Adaptive.Intelligence.LanguageService.Tokenization;
using System.Text;

namespace Adaptive.Intelligence.BlazorBasic.CodeDom;

/// <summary>
/// Provides static methods / functions for creating CodeDOM expression instances.
/// </summary>
public class BlazorBasicExpressionFactory : ICodeExpressionFactory
{
    private readonly static BlazorBasicExpressionFactory _factory = new BlazorBasicExpressionFactory();

    public static BlazorBasicExpressionFactory Instance => _factory;

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
    /// A <see cref="BasicExpression"/> instance containing the CodeDom expression.
    /// </returns>
    public static BasicExpression CreateExpressionFromSingleToken(BlazorBasicLanguageService service, int lineNumber, IToken token)
    {
        BasicExpression expression;

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
                expression = new BasicVariableNameExpression(service, singleItemList);
                break;

            case TokenType.UserDefinedItem:
                expression = new BlazorBasicUserDefinedItemExpression(service, singleItemList);
                break;

            case TokenType.Integer:
                expression = new BasicLiteralIntegerExpression(service, singleItemList);
                break;

            case TokenType.FloatingPoint:
                expression = new BlazorBasicLiteralFloatingPointExpression(service, singleItemList);
                break;

            default:
                throw new BasicSyntaxErrorException(lineNumber);
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
    /// A <see cref="BasicExpression"/> instance.
    /// </returns>
    public static BasicExpression CreateFromTokens(BlazorBasicLanguageService service, int lineNumber, ManagedTokenList tokenList,
        int startIndex = 0)
    {
        BasicExpression newExpression;
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
                    newExpression = new BasicVariableNameExpression(service, subList);
                    break;

                case TokenType.UserDefinedItem:
                    newExpression = new BlazorBasicUserDefinedItemExpression(service, tokenList);
                    break;

                case TokenType.Integer:
                    newExpression = new BasicLiteralIntegerExpression(service, subList);
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
                        newExpression = new BasicLiteralStringExpression(service, string.Empty);
                    else
                        newExpression = new BasicLiteralStringExpression(service, stringToken.Text);
                    break;

                default:
                    throw new BasicSyntaxErrorException(lineNumber);
            }
        }
        return newExpression;
    }
    public static BasicExpression CreateArithmeticExpression
        (BlazorBasicLanguageService service, int lineNumber, ManagedTokenList subList)
    {
        BlazorBasicExpressionParser parser = new BlazorBasicExpressionParser(service);
        
        List<BasicExpression> list = parser.ParseExpression(subList);

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
            list.Add(new BasicLiteralIntegerExpression(service, 0));

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

    public static ExpressionNodeTree xreateExpressionTree(BlazorBasicLanguageService service, ManagedTokenList list)
    {
        ExpressionNodeTree tree = new ExpressionNodeTree();
        ManagedTokenList simpleList = list.RemoveSeparators();

        List<BasicExpression> expressionList = new List<BasicExpression>();
        int index = 0;
        while (index < simpleList.Count)
        {
            List<BasicExpression> subList = xParseExpression(service, simpleList, ref index, tree);
            BlazorBasicComplexExpression exp = new BlazorBasicComplexExpression(service);
            exp.Expressions = subList;
            expressionList.Add(exp);
        }

        return tree;
    }

    public static BasicLiteralStringExpression CreateStringLiteralExpression
       (BlazorBasicLanguageService service, int lineNumber, ManagedTokenList subList)
    {
        StringBuilder builder = new StringBuilder();

        for (int index = 1; index < subList.Count - 2; index++)
        {
            builder.Append(subList[index].Text);
        }
        return new BasicLiteralStringExpression(service, builder.ToString());
    }

    
    public static List<BasicExpression> xParseExpression(BlazorBasicLanguageService service, ManagedTokenList list, ref int index, ExpressionNodeTree tree)
    {
        List<BasicExpression> subExpressionList = new List<BasicExpression>();
        int length = list.Count;
        bool done = false;
        if (index < list.Count)
        {
            do
            {
                IToken token = list[index];
                BasicExpression? expression = null;

                switch (token.TokenType)
                {
                    case TokenType.ExpressionStartDelimiter:
                        index++;
                        List<BasicExpression> expList = xParseExpression(service, list, ref index, tree);
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
                        expression = new BasicVariableNameExpression(service, new ManagedTokenList { token });
                        subExpressionList.Add(expression);
                        break;

                    case TokenType.FloatingPoint:
                        index++;
                        expression = new BlazorBasicLiteralFloatingPointExpression(service, new ManagedTokenList { token });
                        subExpressionList.Add(expression);
                        break;

                    case TokenType.Integer:
                        index++;
                        expression = new BasicLiteralIntegerExpression(service, new ManagedTokenList { token });
                        subExpressionList.Add(expression);
                        break;

                    default:
                        break;
                }
                
            } while (index < length && !done);
        }
        
        return subExpressionList;
    }

    public ICodeExpression CreateExpressionFromSingleToken(ILanguageService service, int lineNumber, IToken token)
    {
        throw new NotImplementedException();
    }

    public ICodeExpression CreateFromTokens(ILanguageService service, int lineNumber, ManagedTokenList tokenList, int startIndex = 0)
    {
        throw new NotImplementedException();
    }

    public ICodeExpression CreateArithmeticExpression(ILanguageService service, int lineNumber, ManagedTokenList subList)
    {
        throw new NotImplementedException();
    }

    public ICodeLiteralCharExpression CreateLiteralCharExpression(ILanguageService service, int lineNumber, ManagedTokenList subList)
    {
        throw new NotImplementedException();
    }

    public ICodeLiteralStringExpression CreateStringLiteralExpression(ILanguageService service, int lineNumber, ManagedTokenList subList)
    {
        throw new NotImplementedException();
    }
    #endregion
}
