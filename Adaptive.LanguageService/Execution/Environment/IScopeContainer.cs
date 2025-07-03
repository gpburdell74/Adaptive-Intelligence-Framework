using Adaptive.Intelligence.LanguageService.CodeDom;
using System.Xml.Linq;

namespace Adaptive.Intelligence.LanguageService.Execution;

/// <summary>
/// Provides the signature definition for an item that maintains a specific scope of execution.
/// </summary>
/// <seealso cref="IDisposable" />
public interface IScopeContainer : IDisposable
{
    /// <summary>
    /// Creates the variable within the scope.
    /// </summary>
    /// <param name="lineNumber">
    /// An integer indicating the line number of the code line being executed.
    /// </param>
    /// <param name="variableName">
    /// A string containing the name of the variable.</param>
    /// <param name="dataType">
    /// A <see cref="StandardDataTypes"/> enumerated value indicating the data type.
    /// Type of the data.</param>
    /// <param name="isArray">
    /// <b>true</b> if the data type is an array.
    /// </param>
    /// <param name="size">
    /// If <paramref name="isArray"/> is true, specifies the size of the array.
    /// </param>
    void CreateVariable(int lineNumber, string variableName, StandardDataTypes dataType, bool isArray, int size);

    /// <summary>
    /// Creates the variable within the scope from its specified parameter definition.
    /// </summary>
    /// <param name="lineNumber">
    /// An integer indicating the line number of the code line being executed.
    /// </param>
    /// <param name="variableName">
    /// A string containing the name of the variable.</param>
    /// <param name="dataType">
    /// A <see cref="StandardDataTypes"/> enumerated value indicating the data type.
    /// Type of the data.</param>
    /// <param name="isArray">
    /// <b>true</b> if the data type is an array.
    /// </param>
    /// <param name="size">
    /// If <paramref name="isArray"/> is true, specifies the size of the array.
    /// </param>
    void CreateParameterVariable(int lineNumber, string variableName, StandardDataTypes dataType, bool isArray, int size);

    /// <summary>
    /// Gets the reference to the list of CodeDOM statements to be executed.
    /// </summary>
    /// <value>
    /// A <see cref="List{T}"/> of <see cref="ILanguageCodeStatement"/> instances.
    /// </value>
    List<ILanguageCodeStatement> Code { get; }

    /// <summary>
    /// Gets the reference to the variable instance with the specified name.
    /// </summary>
    /// <param name="variableName">
    /// A string containing the name of the variable to find.
    /// </param>
    /// <returns>
    /// The <see cref="IVariable"/> instance, if found.
    /// </returns>
    IVariable GetVariable(string variableName);

    /// <summary>
    /// Gets a value indicating whether a variable with the specified name exists within this
    /// scope.
    /// </summary>
    /// <param name="variableName">
    /// A string containing the name of the variable to check for.
    /// </param>
    /// <returns>
    /// <b>true</b> if the variable with the specified name exists; otherwise, <b>false</b>.
    /// </returns>
    bool VariableExists(string? variableName);
}
