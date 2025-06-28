using Adaptive.Intelligence.BlazorBasic.Services;
using Adaptive.Intelligence.LanguageService;
using Adaptive.Intelligence.LanguageService.CodeDom;
using Adaptive.Intelligence.LanguageService.Tokenization;
using System.Linq.Expressions;
using System.Text;

namespace Adaptive.Intelligence.BlazorBasic.CodeDom;

/// <summary>
/// Represents a data type expression.
/// </summary>
/// <seealso cref="BlazorBasicExpression" />
/// <seealso cref="ILanguageCodeExpression" />
public class BlazorBasicDataTypeExpression : BlazorBasicExpression, ILanguageCodeExpression
{
    #region Constructors
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
    protected override void ParseLiteralContent(string expression)
    {
        string codeToParse = NormalizeString(expression);

        string dataType;
        if (codeToParse.ToUpper().Substring(0, 3) == KeywordNames.KeywordAs + ParseConstants.Space)
        {
            dataType = expression.Substring(3).Trim();
        }
        else
            dataType = expression;

        DataType = Service.DataTypes.GetDataType(dataType);
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
            throw new SyntaxErrorException(codeLine.LineNumber);

        if (endIndex - startIndex > 0)
        {
            IToken? asToken = codeLine[startIndex];
            if (asToken == null || asToken.TokenType != TokenType.ReservedWord || asToken.Text.ToLower() != "as")
            {
                throw new ArgumentException("Data type specification must start with 'as '.");
            }

            IToken? typeToken = codeLine[endIndex];
            if (typeToken == null || typeToken.TokenType != TokenType.DataTypeName)
                throw new SyntaxErrorException(codeLine.LineNumber, "Unknown data type specified.");

            DataType = Service.DataTypes.GetDataType(typeToken.Text);
        }
        else
        {
            DataType = Service.DataTypes.GetDataType(codeLine[startIndex].Text);
        }

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
        builder.Append(Service.DataTypes.GetDataTypeName(DataType));
        if (IsArray)
        {
            builder.Append(DelimiterNames.DelimiterOpenBracket  + Size.ToString() + DelimiterNames.DelimiterCloseBracket);
        }
        return builder.ToString();
    }
    #endregion
}
