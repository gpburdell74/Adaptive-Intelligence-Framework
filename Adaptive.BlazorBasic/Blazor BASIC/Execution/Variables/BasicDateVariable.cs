using Adaptive.Intelligence.LanguageService;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Represents a variable of type "DATE".
/// </summary>
/// <seealso cref="BasicVariable{T}" />
public class BasicDateVariable : BasicVariable<DateTime>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicDateVariable"/> class.
    /// </summary>
    /// <param name="name">
    /// A string containing the name of the variable.
    /// </param>
    public BasicDateVariable(string name) : base(name, StandardDataTypes.Date, false, 0)
    {
        Value = DateTime.MinValue;
    }

    /// <summary>
    /// Gets the current date value.
    /// </summary>
    /// <value>
    /// A <see cref="DateTime"/> instance containing the current local date.
    /// </value>
    public static DateTime Now => DateTime.Now.Date;

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
        DateTime newValue = DateTime.MinValue.Date;

        if (sourceValue != null)
        {
            switch (sourceValue)
            {
                case string dateAsString:
                    newValue = DateTime.Parse(dateAsString).Date;
                    break;

                case int numberOfTicks:
                    newValue = new DateTime(numberOfTicks).Date;
                    break;

                case long fileTimeValue:
                    newValue = DateTime.FromFileTime(fileTimeValue).Date;
                    break;

                case DateTime dtValue:
                    newValue = dtValue.Date;
                    break;
            }
        }
        return newValue;
    }
    #endregion
}
