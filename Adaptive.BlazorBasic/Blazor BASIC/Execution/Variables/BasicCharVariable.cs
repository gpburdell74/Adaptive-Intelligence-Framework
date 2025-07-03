using Adaptive.Intelligence.LanguageService;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Represents a variable of type "CHAR".
/// </summary>
/// <seealso cref="BlazorBasicVariable{T}" />
public class BasicCharVariable : BlazorBasicVariable<byte>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicCharVariable"/> class.
    /// </summary>
    /// <param name="name">
    /// A string containing the name of the variable.
    /// </param>
    public BasicCharVariable(string name) : base(name, StandardDataTypes.Char, false, 0)
    {
        Value = 0;
    }
}
