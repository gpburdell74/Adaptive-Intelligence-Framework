namespace Adaptive.Intelligence.LanguageService.Execution;

/// <summary>
/// Provides the signature definition for a variable instance within the scope of a procedure or function.
/// </summary>
/// <seealso cref="IScopedElement" />
public interface IVariable : IScopedElement
{
    /// <summary>
    /// Gets a value indicating whether this variable is defined and populated from a procedure or 
    /// function parameter.
    /// </summary>
    /// <value>
    ///   <c>true</c> if variable is defined and populated from a procedure or 
    /// function parameter; otherwise, <c>false</c>.
    /// </value>
    bool IsParameter { get; set; }

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
