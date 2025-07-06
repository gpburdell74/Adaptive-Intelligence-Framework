using Adaptive.Intelligence.BlazorBasic.Services;
using Adaptive.Intelligence.LanguageService;
using Adaptive.Intelligence.LanguageService.CodeDom.Expressions;
using Adaptive.Intelligence.LanguageService.Execution;
using Adaptive.Intelligence.LanguageService.Tokenization;
using System.Text;

namespace Adaptive.Intelligence.BlazorBasic.CodeDom.Expressions;

/// <summary>
/// Represents a data type expression.
/// </summary>
/// <seealso cref="BasicExpression" />
/// <seealso cref="ICodeDataTypeExpression" />
public class BlazorBasicDataTypeExpression : BasicExpression, ICodeDataTypeExpression
{
    #region Constructor(s)
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicDataTypeExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the language service instance being injected.
    /// </param>
    public BlazorBasicDataTypeExpression(BlazorBasicLanguageService service) : base(service)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicDataTypeExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the language service instance being injected.
    /// </param>
    /// <param name="dataTypeSpec">
    /// A string containing the data type name.
    /// </param>
    public BlazorBasicDataTypeExpression(BlazorBasicLanguageService service, string dataTypeSpec) : base(service, dataTypeSpec)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicDataTypeExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the language service instance being injected.
    /// </param>
    /// <param name="dataTypeName">
    /// A string containing the data type name.
    /// </param>
    /// <param name="isArray">
    /// A value indicating whether the data type is an array.
    /// </param>
    public BlazorBasicDataTypeExpression(BlazorBasicLanguageService service, string dataTypeName, bool isArray) : base(service)
    {
        DataType = Service.DataTypes.GetDataType(dataTypeName);
        IsArray = isArray;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicDataTypeExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the language service instance being injected.
    /// </param>
    /// <param name="codeLine">A <see cref="ITokenizedCodeLine" /> containing the code tokens for the entire line of code.</param>
    /// <param name="startIndex">An integer indicating the ordinal position in <paramref name="codeLine" /> to start parsing the expression.</param>
    /// <param name="endIndex">An integer indicating the ordinal position in <paramref name="codeLine" /> to end parsing the expression.</param>
    public BlazorBasicDataTypeExpression(
        BlazorBasicLanguageService service, 
        ITokenizedCodeLine codeLine, 
        int startIndex, 
        int endIndex) : base(service, codeLine, startIndex, endIndex)
    {
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets or sets the size of the array, if <see cref="IsArray" /> is <b>true</b>.
    /// </summary>
    /// <value>
    /// An integer indicating the array size.
    /// </value>
    /// <remarks>
    /// If this is not an array, this property is ignored.
    /// </remarks>
    public int ArraySize { get; set; }

    /// <summary>
    /// Gets or sets the type of the data.
    /// </summary>
    /// <value>
    /// A <see cref="StandardDataTypes"/> enumerated value indicating the data type.
    /// </value>
    public StandardDataTypes DataType { get; set; } = StandardDataTypes.Unknown;

    /// <summary>
    /// Gets or sets a value indicating whether this instance is array.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is an array; otherwise, <c>false</c>.
    /// </value>
    public bool IsArray { get; set; }

    /// <summary>
    /// Gets or sets the size of the array, if <see cref="IsArray"/> is <b>true</b>.
    /// </summary>
    /// <remarks>
    /// If this is not an array, this property is ignored.
    /// </remarks>
    /// <value>
    /// An integer indicating the array size.
    /// </value>
    public int Size { get; set; }
    
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
        if (expression != null)
        {
            string? codeToParse = NormalizeString(expression);
            if (codeToParse != null)
            {
                string dataType;
                if (codeToParse.ToUpper().Substring(0, 3) == KeywordNames.KeywordAs + ParseConstants.Space)
                {
                    dataType = expression.Substring(3).Trim();
                }
                else
                    dataType = expression;

                DataType = Service.DataTypes.GetDataType(dataType);
            }
        }
    }

    /// <summary>
    /// Parses the code line.
    /// </summary>
    /// <param name="codeLine">
    /// A <see cref="List{T}" /> of <see cref="IToken" /> instances containing the expression to be parsed.
    /// </param>
    protected void ParseCodeLine(List<IToken> codeLine)
    {
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
        if (endIndex > startIndex + 1)
            throw new BasicSyntaxErrorException(codeLine.LineNumber);

        if (endIndex - startIndex > 0)
        {
            IToken? asToken = codeLine[startIndex];
            if (asToken == null || asToken.TokenType != TokenType.ReservedWord || asToken.Text?.ToLower() != "as")
            {
                throw new BasicInvalidArgumentException(codeLine.LineNumber, "Data type specification must start with 'as '.");
            }

            IToken? typeToken = codeLine[endIndex];
            if (typeToken == null || typeToken.TokenType != TokenType.DataTypeName)
                throw new BasicSyntaxErrorException(codeLine.LineNumber, "Unknown data type specified.");

            DataType = Service.DataTypes.GetDataType(typeToken.Text);
        }
        else
        {
            IToken? token = codeLine[startIndex];
            if (token != null)
                DataType = Service.DataTypes.GetDataType(token.Text);
        }
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
    public override object Evaluate(IExecutionEngine engine, IExecutionEnvironment environment, IScopeContainer scope)
    {
        return Render() ?? string.Empty;
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
        builder.Append(Service.DataTypes.GetDataTypeName(DataType));
        if (IsArray)
        {
            builder.Append(DelimiterNames.DelimiterOpenBracket  + Size.ToString() + DelimiterNames.DelimiterCloseBracket);
        }
        return builder.ToString();
    }
    #endregion
}
