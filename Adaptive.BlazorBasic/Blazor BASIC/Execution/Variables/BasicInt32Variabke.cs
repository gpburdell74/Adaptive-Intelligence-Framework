using Adaptive.Intelligence.LanguageService;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Represents a variable of type "INT".
/// </summary>
/// <seealso cref="BlazorBasicVariable{T}" />
public class BasicInt32Variable : BlazorBasicVariable<int>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicInt32Variable"/> class.
    /// </summary>
    /// <param name="name">
    /// A string containing the name of the variable.
    /// </param>
    public BasicInt32Variable(string name) : base(name, StandardDataTypes.Integer, false, 0)
    {
        Value = 0;
    }

    public override void SetValue(object value)
    {
        Value = Convert.ToInt32(value);
    }
}
