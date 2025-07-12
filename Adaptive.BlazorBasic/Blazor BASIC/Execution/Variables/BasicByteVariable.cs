using Adaptive.Intelligence.LanguageService;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Represents a variable of type "BYTE".
/// </summary>
/// <seealso cref="BasicVariable{T}" />
public class BasicByteVariable : BasicVariable<byte>
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
    #region Protected Method Overrides
    /// <summary>
    /// Performs the appropriate conversion of the source value to the variable's type.
    /// </summary>
    /// <param name="sourceValue">The source value to be converted.</param>
    /// <returns>
    /// The value of <paramref name="sourceValue" /> converted to the variable's type, or null if the conversion is not possible.
    /// </returns>
    public override byte Convert(object? sourceValue)
    {
        byte newValue = 0;

        if (sourceValue != null)
        {
            switch (sourceValue)
            {
                case string boolAsString:
                    newValue = byte.Parse(boolAsString);
                    break;

                case short byteAsShort:
                    newValue = System.Convert.ToByte(byteAsShort);
                    break;

                case int byteAsInt:
                    newValue = System.Convert.ToByte(byteAsInt);
                    break;

                case bool byteAsBool:
                    newValue = System.Convert.ToByte(byteAsBool);
                    break;

                case long byteAsLong:
                    newValue = System.Convert.ToByte(byteAsLong);
                    break;

                case byte byteValue:
                    newValue = byteValue;
                    break;
            }
        }
        return newValue;
    }
    #endregion
}
