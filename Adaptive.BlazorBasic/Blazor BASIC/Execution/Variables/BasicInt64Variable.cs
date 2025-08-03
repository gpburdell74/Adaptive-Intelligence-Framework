using Adaptive.Intelligence.LanguageService;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Represents a variable of type "LONG" or "INT64".
/// </summary>
/// <seealso cref="BasicVariable{T}" />
public class BasicInt64Variable : BasicVariable<long>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicInt64Variable"/> class.
    /// </summary>
    /// <param name="name">
    /// A string containing the name of the variable.
    /// </param>
    public BasicInt64Variable(string name) : base(name, StandardDataTypes.LongInteger, false, 0)
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
    public override long Convert(object? sourceValue)
    {
        return DynamicTypeConverter.ToInt64(sourceValue);
    }
    #endregion
}
