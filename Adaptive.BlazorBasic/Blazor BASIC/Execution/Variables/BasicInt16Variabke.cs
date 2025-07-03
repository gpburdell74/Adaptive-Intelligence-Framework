using Adaptive.Intelligence.LanguageService;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Represents a variable of type "SHORT" or "INT16".
/// </summary>
/// <seealso cref="BlazorBasicVariable{T}" />
public class BasicInt16Variable : BlazorBasicVariable<short>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicInt16Variable"/> class.
    /// </summary>
    /// <param name="name">
    /// A string containing the name of the variable.
    /// </param>
    public BasicInt16Variable(string name) : base(name, StandardDataTypes.ShortInteger, false, 0)
    {
        Value = 0;
    }
}
