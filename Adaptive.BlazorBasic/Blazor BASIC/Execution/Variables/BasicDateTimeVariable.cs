using Adaptive.Intelligence.LanguageService;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Represents a variable of type "DATETIME".
/// </summary>
/// <seealso cref="BlazorBasicVariable{T}" />
public class BasicDateTimeVariable : BlazorBasicVariable<DateTime>
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
}
