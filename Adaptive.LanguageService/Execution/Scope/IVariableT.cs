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

    /// <summary>
    /// Performs the apprpopriate conversion of the source value to the variable's type.
    /// </summary>
    /// <param name="sourceValue">The source value to be converted.
    /// </param>
    /// <returns>
    /// The value of <paramref name="sourceValue"/> converted to the variable's type, or null if the conversion is not possible.
    /// </returns>
    T? Convert(object? sourceValue);

    /// <summary>
    /// Gets the value of the variable.
    /// </summary>
    /// <returns>
    /// The value of the variable, or null if it is not set.
    /// </returns>
    T? GetValue();

    /// <summary>
    /// Sets the value of the variable.
    /// </summary>
    /// <param name="value">
    /// The value to be stored in the variable.
    /// </param>
    void SetValue(T? value);
}
