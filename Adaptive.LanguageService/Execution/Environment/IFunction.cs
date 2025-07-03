namespace Adaptive.Intelligence.LanguageService.Execution;

/// <summary>
/// Provides the signature definition to represent a function definition and its code.
/// </summary>
/// <seealso cref="IScopeContainer" />
public interface IFunction : IProcedure
{
    /// <summary>
    /// Gets the data type of the return value for the function instance.
    /// </summary>
    /// <value>
    /// The data <see cref="Type"/> of the expected return value.
    /// </value>
    Type ReturnType { get; }
}
