using Adaptive.Intelligence.LanguageService;
using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Represents a variable of type "BOOL".
/// </summary>
/// <seealso cref="BasicVariable{T}" />
public sealed class BasicBooleanVariable : BasicVariable<bool>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicBooleanVariable"/> class.
    /// </summary>
    /// <param name="name">
    /// A string containing the name of the variable.
    /// </param>
    public BasicBooleanVariable(string name) : base(name, StandardDataTypes.Boolean, false, 0)
    {
        Value = false;
    }

    #region Protected Method Overrides    
    /// <summary>
    /// Performs the appropriate conversion of the source value to the variable's type.
    /// </summary>
    /// <param name="sourceValue">The source value to be converted.</param>
    /// <returns>
    /// The value of <paramref name="sourceValue" /> converted to the variable's type, or null if the conversion is not possible.
    /// </returns>
    public override bool Convert(object? sourceValue)
    {
        bool newValue = false;

        if (sourceValue != null)
        {
            switch (sourceValue)
            {
                case string boolAsString:
                    newValue = bool.Parse(boolAsString);
                    break;

                case int boolAsInt:
                    newValue = (boolAsInt != 0);
                    break;

                case bool boolValue:
                    newValue = boolValue;
                    break;
            }
        }
        return newValue;
    }
    #endregion

}
