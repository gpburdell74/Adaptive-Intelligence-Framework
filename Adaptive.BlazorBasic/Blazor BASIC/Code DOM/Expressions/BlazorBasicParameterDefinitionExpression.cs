using Adaptive.Intelligence.BlazorBasic.Parser;
using Adaptive.Intelligence.BlazorBasic.Services;
using Adaptive.Intelligence.LanguageService.CodeDom;
using Adaptive.Intelligence.LanguageService.Tokenization;

namespace Adaptive.Intelligence.BlazorBasic.CodeDom;

/// <summary>
/// Represents an expression that defines a parameter definition  for a function or a procedure in Blazor BASIC.
/// </summary>
/// <remarks>
///     Expected format:
///         [name][space]AS[space][dataType]
/// </remarks>
/// <example>
///     myIntVar AS INT
///     
///     b$ AS STRING
/// </example>
/// <seealso cref="BlazorBasicExpression" />
/// <seealso cref="ILanguageCodeExpression" />
public class BlazorBasicParameterDefinitionExpression : BlazorBasicExpression, ILanguageCodeExpression
{
    #region Private Member Declarations
    /// <summary>
    /// The parameter name.
    /// </summary>
    private string? _name;
    /// <summary>
    /// The data type expression.
    /// </summary>
    private BlazorBasicDataTypeExpression? _dataType;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicParameterDefinitionExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the language service instance being injected.
    /// </param>
    public BlazorBasicParameterDefinitionExpression(BlazorBasicLanguageService service) : base(service)
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
    public BlazorBasicParameterDefinitionExpression(BlazorBasicLanguageService service, string parameterExpression) : base(service, parameterExpression)
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
    public BlazorBasicParameterDefinitionExpression(
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
    /// <param name="nameToken">
    /// The <see cref="IToken"/> instance containing the parameter name.
    /// </param>
    /// <param name="dataTypeToken">
    /// The <see cref="IToken"/> instance containing the parameter data type name.
    /// </param>
    /// <param name="isArray">
    /// <b>true</b> if the parameter represents an array; otherwise, <b>false</b>.
    /// </param>
    public BlazorBasicParameterDefinitionExpression(BlazorBasicLanguageService service, IToken nameToken, IToken dataTypeToken, bool isArray)
        :base(service)
    {
        _name = nameToken.Text;
        _dataType = new BlazorBasicDataTypeExpression(service, dataTypeToken.Text);
        _dataType.IsArray = isArray;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets the name of the parameter.
    /// </summary>
    /// <value>
    /// A string containing the parameter name.
    /// </value>
    public string? ParameterName => _name;

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
        if (!string.IsNullOrEmpty(expression))
        {
            ITokenizedCodeLine? codeLine = Service.TokenFactory.TokenizeLine(expression);
            if (codeLine == null)
                throw new SyntaxErrorException(0, "Could not parse the content.");

            // Before whitespace removal:
            //      Standard case:   <parameterName><space>AS<space><dataTypeName>
            //      Array case:      <parameterName>[]<space>AS<space><dataTypeName>

            ParseCodeLine(codeLine, 1, codeLine.Count - 1);
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
        // After whitespace removal:
        //      Standard case:   <parameterName>AS<dataTypeName>
        //      Array case:      <parameterName>[]AS<dataTypeName>

        IToken? nameToken = codeLine[startIndex];
        _name = nameToken.Text;

        IToken? dataTypeToken = null;

        if (codeLine[startIndex+2].TokenType == TokenType.ReservedWord && codeLine[startIndex + 2].Text.ToUpper() == KeywordNames.KeywordAs)
        {
            dataTypeToken = codeLine[startIndex + 4];
            _dataType = new BlazorBasicDataTypeExpression(Service, dataTypeToken.Text);
        }
        else if (codeLine[startIndex + 2].TokenType == TokenType.SizingStartDelimiter && codeLine[startIndex + 3].TokenType ==
            TokenType.SizingEndDelimiter)
        {
            dataTypeToken = codeLine[4];
            _dataType = new BlazorBasicDataTypeExpression(Service, dataTypeToken.Text);
            _dataType.IsArray = true;
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
        return _name + ParseConstants.Space + KeywordNames.KeywordAs + ParseConstants.Space + 
            _dataType.Render();
    }
    #endregion
}
