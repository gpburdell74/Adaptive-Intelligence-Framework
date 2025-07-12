using Adaptive.Intelligence.LanguageService;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Represents a variable of type "DATETIME".
/// </summary>
/// <seealso cref="BasicVariable{T}" />
public sealed class BasicDateTimeVariable : BasicVariable<DateTime>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicDateTimeVariable"/> class.
    /// </summary>
    /// <param name="name">
    /// A string containing the name of the variable.
    /// </param>
    public BasicDateTimeVariable(string name) : base(name, StandardDataTypes.DateTime, false, 0)
    {
        Value = DateTime.MinValue;
    }

    /// <summary>
    /// Gets the current date/time value.
    /// </summary>
    /// <value>
    /// A <see cref="DateTime"/> instance containing the current local date and time.
    /// </value>
    public static DateTime Now => DateTime.Now;

    #region Protected Method Overrides
    /// <summary>
    /// Performs the appropriate conversion of the source value to the variable's type.
    /// </summary>
    /// <param name="sourceValue">The source value to be converted.</param>
    /// <returns>
    /// The value of <paramref name="sourceValue" /> converted to the variable's type, or null if the conversion is not possible.
    /// </returns>
    public override DateTime Convert(object? sourceValue)
    {
        DateTime newValue = DateTime.MinValue;

        if (sourceValue != null)
        {
            switch (sourceValue)
            {
                case string dateAsString:
                    newValue = DateTime.Parse(dateAsString);
                    break;

                case int numberOfTicks:
                    newValue = new DateTime(numberOfTicks);
                    break;

                case long fileTimeValue:
                    newValue = DateTime.FromFileTime(fileTimeValue);
                    break;

                case DateTime dtValue:
                    newValue = dtValue;
                    break;
            }
        }
        return newValue;
    }
    #endregion
}
