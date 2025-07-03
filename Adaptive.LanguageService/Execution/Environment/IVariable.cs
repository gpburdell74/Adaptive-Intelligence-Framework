namespace Adaptive.Intelligence.LanguageService.Execution;

/// <summary>
/// Provides the signature definition for a variable instance within the scope of a procedure or function.
/// </summary>
/// <seealso cref="IScopedElement" />
public interface IVariable : IScopedElement, ICodeItemInstance
{
    /// <summary>
    /// Gets or sets a unique Id value for the variable within the scope.
    /// </summary>
    /// <value>
    /// An integer indicating the ID of the instance.
    /// </value>
    int Id { get; set; }

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

    object? GetValue();
    void SetValue(object value);
}
