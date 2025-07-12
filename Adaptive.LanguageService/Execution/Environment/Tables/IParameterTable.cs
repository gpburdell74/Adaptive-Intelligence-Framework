namespace Adaptive.Intelligence.LanguageService.Execution;

/// <summary>
/// Contains the table for the variable definitions and instances within a scoped item such as a 
/// procedure or function.
/// </summary>
/// <seealso cref="IScopedElement" />
public interface IParameterTable : IScopedElement, IContainerTable
{

    /// <summary>
    /// Creates the parameter object and adds to the table with the specified name and data type.
    /// </summary>
    /// <param name="scope">
    /// The <see cref="IScopeContainer"/> instance the parameter is defined for.
    /// </param>
    /// <param name="parameterName">
    /// A string containing the name of the parameter.
    /// </param>
    /// <param name="dataType">
    /// A <see cref="StandardDataTypes"/> enumerated value indicating the type of the data.
    /// </param>
    void Add(IScopeContainer scope, string parameterName, StandardDataTypes dataType);

    /// <summary>
    /// Gets the parameter by name value.
    /// </summary>
    /// <param name="name">
    /// A string containing the name value.
    /// </param>
    /// <returns>
    /// The reference to the <see cref="IParameter"/> instance.
    /// </returns>
    IParameter? GetParameter(string? name);
}
