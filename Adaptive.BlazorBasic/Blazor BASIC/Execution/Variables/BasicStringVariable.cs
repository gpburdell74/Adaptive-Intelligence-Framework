using Adaptive.Intelligence.LanguageService;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Represents a variable of type "STRING".
/// </summary>
/// <seealso cref="BlazorBasicVariable{T}" />
public class BasicStringVariable : BlazorBasicVariable<string>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicStringVariable"/> class.
    /// </summary>
    /// <param name="name">
    /// A string containing the name of the variable.
    /// </param>
    public BasicStringVariable(string name) : base(name, StandardDataTypes.String, false, 0)
    {
        Value = string.Empty;
    }
}
