using Adaptive.Intelligence.LanguageService;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Represents a variable of type "LONG" or "INT64".
/// </summary>
/// <seealso cref="BlazorBasicVariable{T}" />
public class BasicInt64Variable : BlazorBasicVariable<long>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicInt64Variable"/> class.
    /// </summary>
    /// <param name="name">
    /// A string containing the name of the variable.
    /// </param>
    public BasicInt64Variable(string name) : base(name, StandardDataTypes.LongInteger, false, 0)
    {
        Value = 0;
    }
}
