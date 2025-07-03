namespace Adaptive.Intelligence.LanguageService.Execution;

/// <summary>
/// Provides the signature definition for a variable of a specific value type.
/// </summary>
/// <typeparam name="T">
/// The data type of the variable.
/// </typeparam>
/// <seealso cref="IVariable" />
public interface IVariable<T> : IVariable
{
    /// <summary>
    /// Gets or sets the value of the variable.
    /// </summary>
    /// <value>
    /// A <typeparamref name="T"/> value for the variable.
    /// </value>
    T? Value { get; set; }
}
