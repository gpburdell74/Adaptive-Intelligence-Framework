using Adaptive.Intelligence.LanguageService;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Represents a variable of type "DATE".
/// </summary>
/// <seealso cref="BlazorBasicVariable{T}" />
public class BasicDateVariable : BlazorBasicVariable<DateTime>
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
}
