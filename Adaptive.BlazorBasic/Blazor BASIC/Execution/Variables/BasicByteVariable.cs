using Adaptive.Intelligence.LanguageService;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Represents a variable of type "BYTE".
/// </summary>
/// <seealso cref="BlazorBasicVariable{T}" />
public class BasicByteVariable : BlazorBasicVariable<byte>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicByteVariable"/> class.
    /// </summary>
    /// <param name="name">
    /// A string containing the name of the variable.
    /// </param>
    public BasicByteVariable(string name) : base(name, StandardDataTypes.Byte, false, 0)
    {
        Value = 0;
    }
}
