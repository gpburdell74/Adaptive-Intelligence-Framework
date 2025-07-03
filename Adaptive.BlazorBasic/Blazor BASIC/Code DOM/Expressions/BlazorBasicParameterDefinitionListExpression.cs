using Adaptive.Intelligence.BlazorBasic.Parser;
using Adaptive.Intelligence.BlazorBasic.Services;
using Adaptive.Intelligence.LanguageService.CodeDom;
using Adaptive.Intelligence.LanguageService.Execution;
using Adaptive.Intelligence.LanguageService.Tokenization;
using System.Text;

namespace Adaptive.Intelligence.BlazorBasic.CodeDom;

/// <summary>
/// Represents a list of expressions that define a parameter definition for a function or a procedure in Blazor BASIC.
/// </summary>
/// <remarks>
///     Expected format:
///         [name][space]AS[space][dataType], [name][space]AS[space][dataType],  etc.
/// </remarks>
/// <example>
///     myIntVar AS INT, f AS FLOAT, d as STRING
/// </example>
/// <seealso cref="BlazorBasicExpression" />
/// <seealso cref="ILanguageCodeExpression" />
public class BlazorBasicParameterDefinitionListExpression : BlazorBasicExpression, ILanguageCodeExpression
{
    #region Private Member Declarations
    /// <summary>
    /// The list of expressions.
    /// </summary>
    private List<BlazorBasicParameterDefinitionExpression>? _parameters;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicParameterDefinitionListExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the language service instance being injected.
    /// </param>
    public BlazorBasicParameterDefinitionListExpression(BlazorBasicLanguageService service) : base(service)
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicParameterDefinitionExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the language service instance being injected.
    /// </param>
    /// <param name="parameterExpression">
    /// A string containing the parameter definition expression.
    /// </param>
    public BlazorBasicParameterDefinitionListExpression(BlazorBasicLanguageService service, string parameterExpression) : base(service, parameterExpression)
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicParameterDefinitionExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the language service instance being injected.
    /// </param>
    /// <param name="codeLine">A <see cref="ITokenizedCodeLine" /> containing the code tokens for the entire line of code.</param>
    /// <param name="startIndex">An integer indicating the ordinal position in <paramref name="codeLine" /> to start parsing the expression.</param>
    /// <param name="endIndex">An integer indicating the ordinal position in <paramref name="codeLine" /> to end parsing the expression.</param>
    public BlazorBasicParameterDefinitionListExpression(
        BlazorBasicLanguageService service,
        ITokenizedCodeLine codeLine,
        int startIndex,
        int endIndex) : base(service, codeLine, startIndex, endIndex)
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicParameterDefinitionExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the language service instance being injected.
    /// </param>
    /// <param name="codeLine">A <see cref="ITokenizedCodeLine" /> containing the code tokens for the entire line of code.</param>
    /// <param name="startIndex">An integer indicating the ordinal position in <paramref name="codeLine" /> to start parsing the expression.</param>
    /// <param name="endIndex">An integer indicating the ordinal position in <paramref name="codeLine" /> to end parsing the expression.</param>
    public BlazorBasicParameterDefinitionListExpression(
        BlazorBasicLanguageService service,
        ManagedTokenList codeLine,
        int startIndex,
        int endIndex) : base(service)
    {
        ParseManagedList(codeLine, startIndex, endIndex);
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicParameterDefinitionExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the language service instance being injected.
    /// </param>
    /// <param name="list">
    /// The <see cref="List{T}"/> of <see cref="BlazorBasicParameterDefinitionExpression"/> instances.
    /// </param>
    public BlazorBasicParameterDefinitionListExpression(BlazorBasicLanguageService service, List<BlazorBasicParameterDefinitionExpression> list)
        : base(service)
    {
        _parameters = list;
    }
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        _parameters?.Clear();

        _parameters = null; 
        base.Dispose(disposing);
    }
    #endregion

    #region Public Properties    
    /// <summary>
    /// Gets the number of parameter expressions.
    /// </summary>
    /// <value>
    /// An integer containing the number of expressions.
    /// </value>
    public int Count
    {
        get
        {
            if (_parameters == null)
                return 0;
            else
                return _parameters.Count;
        }
    }
    /// <summary>
    /// Gets the reference to the list of parameter definitions.
    /// </summary>
    /// <value>
    /// A <see cref="List{T}"/> of <see cref="BlazorBasicParameterDefinitionExpression"/> instances.
    /// </value>
    public List<BlazorBasicParameterDefinitionExpression>? ParameterList => _parameters;
    #endregion

    #region Protected Methods    
    /// <summary>
    /// Parses the content expression into a parameter definition.
    /// </summary>
    /// <param name="expression">
    /// A string containing the expression to be parsed.
    /// </param>
    protected override void ParseLiteralContent(string? expression)
    {
        if (!string.IsNullOrEmpty(expression))
        {
            ITokenizedCodeLine? codeLine = Service.TokenFactory.TokenizeLine(expression);
            if (codeLine == null)
                throw new BasicSyntaxErrorException(0);

            ParseCodeLine(codeLine, 0, codeLine.Count-1);
        }
    }

    /// <summary>
    /// Parses the code line.
    /// </summary>
    /// <param name="codeLine">
    /// A <see cref="ITokenizedCodeLine" /> containing the code tokens for the entire line of code.
    /// </param>
    /// <param name="startIndex">
    /// An integer indicating the ordinal position in <paramref name="codeLine" /> to start parsing the expression.
    /// </param>
    /// <param name="endIndex">
    /// An integer indicating the ordinal position in <paramref name="codeLine" /> to end parsing the expression.
    /// </param>
    protected override void ParseCodeLine(ITokenizedCodeLine codeLine, int startIndex, int endIndex)
    {
        // Remove extra whitespace tokens.
        ManagedTokenList managedList = new ManagedTokenList(codeLine.TokenList);
        ManagedTokenList trimmedList = managedList.Trim();

        List<BlazorBasicParameterDefinitionExpression> list = SplitIntoParameters(trimmedList);
        _parameters = list;

        trimmedList.Clear();
        managedList.Clear();
    }
    #endregion

    #region Public Methods / Functions
    /// <summary>
    /// Evaluates the expression.
    /// </summary>
    /// <param name="engine">
    /// The reference to the execution engine instance.
    /// </param>
    /// <param name="environment">
    /// The reference to the execution environment instance.
    /// </param>
    /// <param name="scope">
    /// The <see cref="IScopeContainer" /> instance, such as a procedure or function, in which scoped
    /// variables are declared.</param>
    /// <returns>
    /// A string containing the user-defined text value.
    /// </returns>
    public string? Evaluate(IExecutionEngine engine, IExecutionEnvironment environment, IScopeContainer scope)
    {
        return "";
    }

    /// <summary>
    /// Evaluates the expression.
    /// </summary>
    /// <param name="engine">
    /// The reference to the execution engine instance.
    /// </param>
    /// <param name="environment">
    /// The reference to the execution environment instance.
    /// </param>
    /// <param name="scope">
    /// The <see cref="IScopeContainer" /> instance, such as a procedure or function, in which scoped
    /// variables are declared.</param>
    /// <returns>
    /// A string containing the user-defined text value.
    /// </returns>
    public override T? Evaluate<T>(IExecutionEngine engine, IExecutionEnvironment environment, IScopeContainer scope) where T : default
    {
        return (T?)(object?)"";
    }
    /// <summary>
    /// Renders the content of the expression into a string.
    /// </summary>
    /// <returns>
    /// A string containing the expression rendered into Blazor BASIC code.
    /// </returns>
    public override string? Render()
    {
        StringBuilder builder = new StringBuilder();
        if (_parameters != null && _parameters.Count > 0)
        {
            int length = _parameters.Count - 1;
            int index = 0;

            do
            {
                builder.Append(_parameters[index].Render());
                if (index < length)
                    builder.Append(", ");
                index++;
            } while (index <= length);
        }
        return builder.ToString();
    }
    #endregion

    #region Private Methods / Functions


    /// <summary>
    /// Parses the code line.
    /// </summary>
    /// <param name="managedList">
    /// A <see cref="ManagedTokenList" /> containing the code tokens for the entire line of code.
    /// </param>
    /// <param name="startIndex">
    /// An integer indicating the ordinal position in <paramref name="managedList" /> to start parsing the expression.
    /// </param>
    /// <param name="endIndex">
    /// An integer indicating the ordinal position in <paramref name="managedList" /> to end parsing the expression.
    /// </param>
    private void ParseManagedList(ManagedTokenList managedList, int startIndex, int endIndex)
    {
        // Remove extra whitespace tokens.
        ManagedTokenList trimmedList = managedList.Trim();

        List<BlazorBasicParameterDefinitionExpression> list = SplitIntoParameters(trimmedList);
        _parameters = list;

        trimmedList.Clear();
        managedList.Clear();
    }

    /// <summary>
    /// Splits the list of tokens into individual parameter definitions.
    /// </summary>
    /// <param name="tokenList">
    /// The <see cref="ManagedTokenList"/> instance to read the tokens from.
    /// </param>
    /// <returns>
    /// A <see cref="List{T}"/> of <see cref="BlazorBasicParameterDefinitionExpression"/> instances.
    /// </returns>
    private List<BlazorBasicParameterDefinitionExpression> SplitIntoParameters(ManagedTokenList tokenList)
    {
        int startIndex = tokenList.FindFirstToken(TokenType.ExpressionStartDelimiter);
        int endIndex = tokenList.FindLastToken(TokenType.ExpressionEndDelimiter);

        ManagedTokenList tokenSubList = tokenList.CreateCopy(startIndex+1, endIndex-1).RemoveSeparators();

        List<BlazorBasicParameterDefinitionExpression> parameterList = new List<BlazorBasicParameterDefinitionExpression>();

        int pos = 0;

        do
        {
            bool isArray = false;
            IToken nameToken = tokenSubList[pos];
            IToken dataTypeToken;
            pos++;

            if (tokenSubList[pos].TokenType == TokenType.SizingStartDelimiter &&
                tokenSubList[pos + 1].TokenType == TokenType.SizingStartDelimiter)
            {
                isArray = true;
                pos += 2;

            }
            else
                pos++;

            dataTypeToken = tokenSubList[pos];
            parameterList.Add(
                new BlazorBasicParameterDefinitionExpression(Service, nameToken, dataTypeToken, isArray));
            pos++;

        } while (pos < tokenSubList.Count);

        return parameterList;
    }
    #endregion
}
