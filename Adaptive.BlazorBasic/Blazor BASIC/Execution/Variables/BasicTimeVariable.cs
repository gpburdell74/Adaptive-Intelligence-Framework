using Adaptive.Intelligence.LanguageService;
using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Represents a variable of type "TIME".
/// </summary>
/// <seealso cref="BlazorBasicVariable{T}" />
public class BasicTimeVariable : BlazorBasicVariable<Time>
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
}
