namespace Adaptive.Intelligence.LanguageService.Execution;

/// <summary>
/// Contains the table for the variable definitions and instances within a scoped item such as a 
/// procedure or function.
/// </summary>
/// <seealso cref="IScopedElement" />
public interface IVariableTable : IContainerTable
{
    /// <summary>
    /// Gets the variable by name value.
    /// </summary>
    /// <param name="name">
    /// A string containing the name value.
    /// </param>
    /// <returns>
    /// The reference to the <see cref="IVariable"/> instance.
    /// </returns>
    IVariable? GetVariable(string? name);
}
