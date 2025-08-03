using Adaptive.Intelligence.LanguageService;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Represents a variable of type "STRING".
/// </summary>
/// <seealso cref="BasicVariable{T}" />
public sealed class BasicStringVariable : BasicVariable<string>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicStringVariable"/> class.
    /// </summary>
    /// <param name="name">
    /// A string containing the name of the variable.
    /// </param>
    public BasicStringVariable(string name) : base(name, StandardDataTypes.String, false, 0)
    {
        Value = string.Empty;
    }

    #region Protected Method Overrides
    /// <summary>
    /// Performs the appropriate conversion of the source value to the variable's type.
    /// </summary>
    /// <param name="sourceValue">The source value to be converted.</param>
    /// <returns>
    /// The value of <paramref name="sourceValue" /> converted to the variable's type, or null if the conversion is not possible.
    /// </returns>
    public override string Convert(object? sourceValue)
    {
        return DynamicTypeConverter.ToString(sourceValue);
    }
    #endregion
}
