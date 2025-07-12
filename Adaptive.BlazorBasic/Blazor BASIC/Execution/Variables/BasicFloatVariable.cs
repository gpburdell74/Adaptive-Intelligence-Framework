using Adaptive.Intelligence.LanguageService;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Represents a variable of type "FLOAT".
/// </summary>
/// <seealso cref="BasicVariable{T}" />
public class BasicFloatVariable : BasicVariable<float>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicFloatVariable"/> class.
    /// </summary>
    /// <param name="name">
    /// A string containing the name of the variable.
    /// </param>
    public BasicFloatVariable(string name) : base(name, StandardDataTypes.Float, false, 0)
    {
        Value = 0;
    }

    #region Protected Method Overrides    
    /// <summary>
    /// Performs the appropriate conversion of the source value to the variable's type.
    /// </summary>
    /// <param name="sourceValue">The source value to be converted.</param>
    /// <returns>
    /// The value of <paramref name="sourceValue" /> converted to the variable's type, or null if the conversion is not possible.
    /// </returns>
    public override float Convert(object? sourceValue)
    {
        return DynamicTypeConverter.ToSingle(sourceValue);
    }
    #endregion
}
