using Adaptive.BlazorBasic.LanguageService.CodeDom;
using Adaptive.BlazorBasic.Services;
using Adaptive.Intelligence.Shared;
using Adaptive.LanguageService.Tokenization;

namespace Adaptive.BlazorBasic.CodeDom;

/// <summary>
/// Represents a parameter definition in Blazor BASIC.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="ILanguageCodeExpression" />
public class BasicParameterDefinitionExpression : BasicExpression, ILanguageCodeExpression
{
    #region Constructor / Dispose Methods    
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicParameterDefinitionExpression"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public BasicParameterDefinitionExpression(BlazorBasicLanguageService service) : base(service)
    {

    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicParameterDefinitionExpression"/> class.
    /// </summary>
    /// <param name="expression">
    /// A string containing the expression to be parsed.
    /// </param>
    public BasicParameterDefinitionExpression(BlazorBasicLanguageService service, string expression) : base(service)
    {
        ParseContent(expression);
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
            DataType?.Dispose();
        }

        DataType = null;
        ParameterName = null;
        base.Dispose(disposing);
    }
    #endregion

    #region Public Properties    
    /// <summary>
    /// Gets or sets the reference to the data type of the parameter.
    /// </summary>
    /// <value>
    /// A <see cref="BasicDataTypeExpression"/> indicating the data type.
    /// </value>
    public BasicDataTypeExpression? DataType { get; set; }

    /// <summary>
    /// Gets or sets the name of the parameter.
    /// </summary>
    /// <value>
    /// A string containing the name of the parameter.
    /// </value>
    public string? ParameterName { get; set; }
    #endregion

    #region Private Methods / Functions
    /// <summary>
    /// Parses the content expression into a parameter definition.
    /// </summary>
    /// <param name="expression">
    /// A string containing the expression to be parsed.
    /// </param>
    /// <exception cref="Adaptive.Intelligence.Shared.ExceptionEventArgs.Exception"></exception>
    private void ParseContent(string expression)
    {
        expression = expression.Trim();
        // Expected format:
        //
        //  [ParameterName][space][AS][space][DataTypeName]

        int index = expression.IndexOf(' ');
        if (index == -1)
        {
            throw new Exception();
        }
        else
        {
            // Extract the name.
            ParameterName = expression.Substring(0, index);

            // Parse the data type.
            DataType = new BasicDataTypeExpression(Service, expression.Substring(index, expression.Length - index));
        }
    }

    protected override void ParseLiteralContent(string? expression)
    {
        throw new NotImplementedException();
    }

    protected override void ParseCodeLine(ITokenizedCodeLine codeLine, int startIndex, int endIndex)
    {
        throw new NotImplementedException();
    }
    #endregion
}
