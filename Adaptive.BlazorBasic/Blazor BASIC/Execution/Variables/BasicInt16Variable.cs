using Adaptive.Intelligence.LanguageService;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Represents a variable of type "SHORT" or "INT16".
/// </summary>
/// <seealso cref="BasicVariable{T}" />
public class BasicInt16Variable : BasicVariable<short>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicInt16Variable"/> class.
    /// </summary>
    /// <param name="name">
    /// A string containing the name of the variable.
    /// </param>
    public BasicInt16Variable(string name) : base(name, StandardDataTypes.ShortInteger, false, 0)
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
    public override short Convert(object? sourceValue)
    {
        return DynamicTypeConverter.ToInt16(sourceValue);
    }
    #endregion
}
