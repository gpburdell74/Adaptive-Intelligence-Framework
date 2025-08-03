using Adaptive.Intelligence.LanguageService;
using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Represents a variable of type "TIME".
/// </summary>
/// <seealso cref="BasicVariable{T}" />
public sealed class BasicTimeVariable : BasicVariable<Time>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicTimeVariable"/> class.
    /// </summary>
    /// <param name="name">
    /// A string containing the name of the variable.
    /// </param>
    public BasicTimeVariable(string name) : base(name, StandardDataTypes.Time, false, 0)
    {
        Value = Time.MinValue;
    }

    /// <summary>
    /// Gets the current time value.
    /// </summary>
    /// <value>
    /// A <see cref="Time"/> instance containing the current local time.
    /// </value>
    public static Time Now => Time.Now;

    #region Protected Method Overrides    
    /// <summary>
    /// Performs the appropriate conversion of the source value to the variable's type.
    /// </summary>
    /// <param name="sourceValue">The source value to be converted.</param>
    /// <returns>
    /// The value of <paramref name="sourceValue" /> converted to the variable's type, or null if the conversion is not possible.
    /// </returns>
    public override Time Convert(object? sourceValue)
    {
        Time newValue = Time.MinValue;

        if (sourceValue != null)
        {
            switch (sourceValue)
            {
                case string timeAsString:
                    newValue = Time.Parse(timeAsString);
                    break;

                case int totalSeconds:
                    newValue = new Time(totalSeconds);
                    break;

                case Time timeValue:
                    newValue = new Time(timeValue.TotalSeconds);
                    break;

            }
        }
        return newValue;
    }
    #endregion
}
