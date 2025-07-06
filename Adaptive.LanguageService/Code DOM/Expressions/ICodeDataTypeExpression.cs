namespace Adaptive.Intelligence.LanguageService.CodeDom.Expressions;

/// <summary>
/// Provides the signature definition for expressions that define or reference data types.
/// </summary>
/// <seealso cref="ICodeExpression" />
public interface ICodeDataTypeExpression : ICodeExpression
{
    /// <summary>
    /// Gets or sets the size of the array, if <see cref="IsArray"/> is <b>true</b>.
    /// </summary>
    /// <remarks>
    /// If this is not an array, this property is ignored.
    /// </remarks>
    /// <value>
    /// An integer indicating the array size.
    /// </value>
    int ArraySize { get; set; }

    /// <summary>
    /// Gets or sets the type of the data.
    /// </summary>
    /// <value>
    /// A <see cref="StandardDataTypes"/> enumerated value indicating the data type.
    /// </value>
    StandardDataTypes DataType { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is array.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is an array; otherwise, <c>false</c>.
    /// </value>
    bool IsArray { get; set; }
}
