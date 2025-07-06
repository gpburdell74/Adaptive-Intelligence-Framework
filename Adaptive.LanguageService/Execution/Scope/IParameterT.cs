namespace Adaptive.Intelligence.LanguageService.Execution;

/// <summary>
/// Provides the signature definition for a Parameter of a specific value type.
/// </summary>
/// <typeparam name="T">
/// The data type of the Parameter.
/// </typeparam>
/// <seealso cref="IParameter" />
public interface IParameter<T> : IParameter
{
    /// <summary>
    /// Gets or sets the value of the Parameter.
    /// </summary>
    /// <value>
    /// A <typeparamref name="T"/> value for the Parameter.
    /// </value>
    T? Value { get; set; }

    /// <summary>
    /// Performs the apprpopriate conversion of the source value to the Parameter's type.
    /// </summary>
    /// <param name="sourceValue">The source value to be converted.
    /// </param>
    /// <returns>
    /// The value of <paramref name="sourceValue"/> converted to the Parameter's type, or null if the conversion is not possible.
    /// </returns>
    T? Convert(object? sourceValue);

    /// <summary>
    /// Gets the value of the Parameter.
    /// </summary>
    /// <returns>
    /// The value of the Parameter, or null if it is not set.
    /// </returns>
    T? GetValue();

    /// <summary>
    /// Sets the value of the Parameter.
    /// </summary>
    /// <param name="value">
    /// The value to be stored in the Parameter.
    /// </param>
    void SetValue(T? value);
}
