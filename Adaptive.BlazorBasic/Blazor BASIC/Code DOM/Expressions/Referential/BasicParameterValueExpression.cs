using Adaptive.Intelligence.BlazorBasic.CodeDom.Expressions;
using Adaptive.Intelligence.BlazorBasic.Services;
using Adaptive.Intelligence.LanguageService;
using Adaptive.Intelligence.LanguageService.CodeDom.Expressions;
using Adaptive.Intelligence.LanguageService.Execution;
using Adaptive.Intelligence.LanguageService.Tokenization;

namespace Adaptive.Intelligence.BlazorBasic.CodeDom;

/// <summary>
/// Represents a value to be assigned to a parameter when a function or procedure is called.
/// </summary>
/// <seealso cref="BasicExpression" />
/// <seealso cref="ICodeExpression" />
public class BlazorBasicParameterValueExpression : BasicExpression, ICodeParameterValueExpression
{
    #region Private Member Declarations
    /// <summary>
    /// The data type expression.
    /// </summary>
    private BlazorBasicDataTypeExpression? _dataType;

    /// <summary>
    /// The parameter name.
    /// </summary>
    private string? _name;

    /// <summary>
    /// The original data value.
    /// </summary>
    private object? _originalData;

    /// <summary>
    /// The value expression instance.
    /// </summary>
    private BasicExpression? _valueExpression;
    #endregion

    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicParameterValueExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the language service instance being injected.
    /// </param>
    public BlazorBasicParameterValueExpression(BlazorBasicLanguageService service) : base(service)
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicParameterValueExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the language service instance being injected.
    /// </param>
    /// <param name="parameterExpression">
    /// A string containing the parameter definition expression.
    /// </param>
    public BlazorBasicParameterValueExpression(BlazorBasicLanguageService service, string parameterExpression) : base(service, parameterExpression)
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicParameterValueExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the language service instance being injected.
    /// </param>
    /// <param name="codeLine">A <see cref="ITokenizedCodeLine" /> containing the code tokens for the entire line of code.</param>
    /// <param name="startIndex">An integer indicating the ordinal position in <paramref name="codeLine" /> to start parsing the expression.</param>
    /// <param name="endIndex">An integer indicating the ordinal position in <paramref name="codeLine" /> to end parsing the expression.</param>
    public BlazorBasicParameterValueExpression(
        BlazorBasicLanguageService service,
        ITokenizedCodeLine codeLine,
        int startIndex,
        int endIndex) : base(service, codeLine, startIndex, endIndex)
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicParameterValueExpression"/> class.
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
    public BlazorBasicParameterValueExpression(BlazorBasicLanguageService service, IToken nameToken, IToken dataTypeToken, bool isArray)
        : base(service)
    {
        _name = nameToken.Text;
        _dataType = new BlazorBasicDataTypeExpression(service, dataTypeToken.Text ?? string.Empty);
        _dataType.IsArray = isArray;
    }
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        if (!IsDisposed && disposing)
        {
            _dataType?.Dispose();
            _valueExpression?.Dispose();
        }

        _name = null;
        _originalData = null;
        _dataType = null;
        _valueExpression = null;

        base.Dispose(disposing);
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets the size of the array, if <see cref="P:Adaptive.Intelligence.LanguageService.CodeDom.Expressions.ICodeParameterDefinitionExpression.IsArray" /> is <b>true</b>.
    /// </summary>
    /// <value>
    /// An integer indicating the array size.
    /// </value>
    /// <remarks>
    /// If this is not an array, this property is ignored.
    /// </remarks>
    public int ArraySize { get; }

    /// <summary>
    /// Gets the data type of the parameter variable.
    /// </summary>
    /// <value>
    /// A <see cref="BlazorBasicDataTypeExpression"/> representing the type of data.
    /// </value>
    public BlazorBasicDataTypeExpression? DataType
    {
        get => _dataType;
        set
        {
            _dataType = value;
            SetValueExpression(value);
        }
    }
    ICodeDataTypeExpression? ICodeParameterDefinitionExpression.DataType => _dataType;

    /// <summary>
    /// Gets or sets a value indicating whether this instance is array.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is an array; otherwise, <c>false</c>.
    /// </value>
    public bool IsArray { get; set; }

    /// <summary>
    /// Gets or sets the original data.
    /// </summary>
    /// <value>
    /// The original data that was parsed from the source file.
    /// </value>
    public object? OriginalData
    {
        get => _originalData;
        set => _originalData = value;
    }

    /// <summary>
    /// Gets the name of the parameter.
    /// </summary>
    /// <value>
    /// A string containing the parameter name.
    /// </value>
    public string? ParameterName { get => _name; set => _name = value; }

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

    /// <summary>
    /// Gets the reference to the expression that renders / contains the value to be passed
    /// to the parameter.
    /// </summary>
    /// <value>
    /// An <see cref="BasicExpression" /> instance containing the value to be passed to the parameter.
    /// </value>
    public BasicExpression? ValueExpression => _valueExpression;
    ICodeExpression? ICodeParameterValueExpression.ValueExpression => _valueExpression;
    #endregion

    #region Protected Methods
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
        if (codeLine.Count > 0)
        {
            IToken? token = codeLine[0];
            if (token != null)
                ParseLiteralContent(codeLine[0].Text);
        }
    }

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
                throw new BasicSyntaxErrorException(0, "Could not parse the content.");

            // Before whitespace removal:
            //      Standard case:   <parameterName><space>AS<space><dataTypeName>
            //      Array case:      <parameterName>[]<space>AS<space><dataTypeName>
            if (codeLine.Count == 1)
            {
                _originalData = (object)expression;
            }
            else
                ParseCodeLine(codeLine, 1, codeLine.Count - 1);
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
    public override object? Evaluate(IExecutionEngine engine, IExecutionEnvironment environment, IScopeContainer scope)
    {
        return _originalData;
    }
    /// <summary>
    /// Renders the content of the expression into a string.
    /// </summary>
    /// <returns>
    /// A string containing the expression rendered into Blazor BASIC code.
    /// </returns>
    public override string? Render()
    {
        return _name + ParseConstants.Space + KeywordNames.KeywordAs + ParseConstants.Space +
            _dataType?.Render();
    }
    /// <summary>
    /// Sets the internal values based on the provided parent parameter definition.
    /// </summary>
    /// <param name="definitionExpression">
    /// The <see cref="ICodeParameterDefinitionExpression" /> instance defining the data type
    /// and parameter the value of the current instance belongs to.
    /// </param>
    public void SetFromParameterDefinition(ICodeParameterDefinitionExpression definitionExpression)
    {
        if (definitionExpression.DataType != null)
            _dataType = (BlazorBasicDataTypeExpression)definitionExpression.DataType;

        _name = definitionExpression.ParameterName;
    }
    /// <summary>
    /// Sets the value expression from the provided data.
    /// </summary>
    /// <param name="originalData">The original data that was parsed from the source file.</param>
    public void SetValueExpression(object originalData)
    {
        if (_dataType != null)
        { 
        StandardDataTypes dt = _dataType.DataType;
            switch (dt)
            {
                case StandardDataTypes.Boolean:
                    _valueExpression = new BasicLiteralBooleanExpression(Service, (bool)_originalData);
                    break;

                case StandardDataTypes.Byte:
                    break;

                case StandardDataTypes.Char:
                    _valueExpression = new BlazorBasicLiteralCharacterExpression(Service, (char)_originalData);
                    break;

                case StandardDataTypes.Date:
                    break;

                case StandardDataTypes.DateTime:
                    break;

                case StandardDataTypes.Float:
                    _valueExpression = new BlazorBasicLiteralFloatingPointExpression(Service, (float)_originalData);
                    break;

                case StandardDataTypes.Time:
                    break;
                case StandardDataTypes.Object:
                    break;
                case StandardDataTypes.Integer:
                    _valueExpression = new BasicLiteralIntegerExpression(Service, Convert.ToInt32(_originalData));
                    break;

                case StandardDataTypes.LongInteger:
                    break;
                case StandardDataTypes.ShortInteger:
                    break;
                case StandardDataTypes.String:
                    _valueExpression = new BasicLiteralStringExpression(Service, (string)_originalData);
                    break;
            }
        }
    }
    #endregion
}
