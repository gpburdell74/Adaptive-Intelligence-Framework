using Adaptive.Intelligence.LanguageService;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Represents a variable of type "FLOAT".
/// </summary>
/// <seealso cref="BlazorBasicVariable{T}" />
public class BasicFloatVariable : BlazorBasicVariable<float>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicFloatVariable"/> class.
    /// </summary>
    /// <param name="name">
    /// A string containing the name of the variable.
    /// </param>
    public BasicFloatVariable(string name) : base(name, StandardDataTypes.Float, false, 0)
    {
        Value = 0;
    }
}
