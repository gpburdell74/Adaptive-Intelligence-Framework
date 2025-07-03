using Adaptive.Intelligence.LanguageService;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Represents a variable of type "BOOL".
/// </summary>
/// <seealso cref="BlazorBasicVariable{T}" />
public class BasicBooleanVariable : BlazorBasicVariable<bool>
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
}
