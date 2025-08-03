namespace Adaptive.Intelligence.LanguageService.Execution;

/// <summary>
/// Provides the signature definition for a parameter instance within the scope of a procedure or function.
/// </summary>
/// <seealso cref="IScopedElement" />
public interface IParameter : IScopedElement
{
    /// <summary>
    /// Gets the data type of the value in the parameter.
    /// </summary>
    /// <value>
    /// A <see cref="StandardDataTypes"/> enumerated value indicating the data type.
    /// </value>
    StandardDataTypes DataType { get; }

    /// <summary>
    /// Gets the name of the variable instance.
    /// </summary>
    /// <value>
    /// A string containing the name of the variable.
    /// </value>
    string? Name { get; }

    /// <summary>
    /// Gets the value stored in the variable.
    /// </summary>
    /// <returns>
    /// The content of the variable, or <c>null</c> if the variable is not set.
    /// </returns>
    object? GetValue();

    /// <summary>
    /// Sets the value of the variable instance. This method allows for updating the variable's content.
    /// </summary>
    /// <param name="value">
    /// The value to be stored in the variable.</param>
    void SetValue(object value);
}
