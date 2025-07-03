namespace Adaptive.Intelligence.LanguageService.Execution;

/// <summary>
/// Provides the signature definition for storing and managing code for function definitions.
/// </summary>
/// <seealso cref="IDisposable" />
public interface IFunctionTable : IContainerTable
{
    /// <summary>
    /// Gets the function reference for the specified ID value.
    /// </summary>
    /// <param name="id">
    /// An integer containing the unique ID value assigned to the function instance.
    /// </param>
    /// <returns>
    /// The reference to the <see cref="IFunction"/> instance.
    /// </returns>
    IFunction? GetFunction(int id);

    /// <summary>
    /// Gets the function reference for the function with the specified name.
    /// </summary>
    /// <param name="functionName">
    /// A string containing the name of the function.
    /// </param>
    /// <returns>
    /// The reference to the <see cref="IFunction"/> instance.
    /// </returns>
    IFunction? GetFunctionByName(string functionName);
}
