namespace Adaptive.Intelligence.LanguageService.CodeDom.Expressions;

/// <summary>
/// Provides the signature definition for expressions that contain and render function or 
/// procedure parameter definitions.
/// </summary>
/// <seealso cref="ICodeExpression" />
public interface ICodeParameterDefinitionExpression : ICodeExpression
{
    /// <summary>
    /// Gets the size of the array, if <see cref="IsArray"/> is <b>true</b>.
    /// </summary>
    /// <remarks>
    /// If this is not an array, this property is ignored.
    /// </remarks>
    /// <value>
    /// An integer indicating the array size.
    /// </value>
    public int ArraySize { get; }

    /// <summary>
    /// Gets the data type of the parameter variable.
    /// </summary>
    /// <value>
    /// A <see cref="ICodeDataTypeExpression"/> representing the type of data.
    /// </value>
    ICodeDataTypeExpression? DataType { get; }

    /// <summary>
    /// Gets a value indicating whether this instance is array.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is an array; otherwise, <c>false</c>.
    /// </value>
    public bool IsArray { get; }

    /// <summary>
    /// Gets the name of the parameter.
    /// </summary>
    /// <value>
    /// A string containing the parameter name.
    /// </value>
    string? ParameterName { get; }
}