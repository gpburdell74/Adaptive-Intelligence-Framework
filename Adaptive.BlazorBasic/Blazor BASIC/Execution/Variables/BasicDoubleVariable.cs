using Adaptive.Intelligence.LanguageService;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Represents a variable of type "DOUBLE".
/// </summary>
/// <seealso cref="BlazorBasicVariable{T}" />
public class BasicDoubleVariable : BlazorBasicVariable<short>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicDoubleVariable"/> class.
    /// </summary>
    /// <param name="name">
    /// A string containing the name of the variable.
    /// </param>
    public BasicDoubleVariable(string name) : base(name, StandardDataTypes.Double, false, 0)
    {
        Value = 0;
    }
}
