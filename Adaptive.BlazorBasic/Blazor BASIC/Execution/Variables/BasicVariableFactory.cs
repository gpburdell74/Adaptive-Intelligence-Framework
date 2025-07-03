using Adaptive.Intelligence.LanguageService;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Provides static methods / functions for creating variable objects.
/// </summary>
public static class BasicVariableFactory
{
    /// <summary>
    /// Creates the variable with the specified name and specified data type.
    /// </summary>
    /// <param name="variableName">
    /// A string containing the name of the variable.
    /// </param>
    /// <param name="dataType">
    /// A <see cref="StandardDataTypes"/> enumerated value indicating the variable's data type.
    /// </param>
    /// <param name="isArray">
    /// <b>true</b> if th variable represents an array.
    /// </param>
    /// <param name="size">
    /// The size of the array, if <paramref name="isArray"/> is <b>true</b>.
    /// </param>
    /// <returns>
    /// A <see cref="BlazorBasicVariable"/> instance representing the variable with the specified name and data type.
    /// </returns>
    /// <exception cref="Exception">Invalid data type specified.</exception>
    public static BlazorBasicVariable? CreateByType(string variableName, StandardDataTypes dataType, 
        bool isArray, int size)
    {
        BlazorBasicVariable? variable = null;

        switch(dataType)
        {
            case StandardDataTypes.String:
                variable = new BasicStringVariable(variableName);
                break;

            case StandardDataTypes.Boolean:
                variable = new BasicBooleanVariable(variableName);
                break;

            case StandardDataTypes.Byte:
                variable = new BasicByteVariable(variableName);
                break;

            case StandardDataTypes.Char:
                variable = new BasicCharVariable(variableName);
                break;

            case StandardDataTypes.Date:
                variable = new BasicDateVariable(variableName);
                break;

            case StandardDataTypes.DateTime:
                variable = new BasicDateTimeVariable(variableName);
                break;

            case StandardDataTypes.Double:
                variable = new BasicDoubleVariable(variableName);
                break;

            case StandardDataTypes.Float:
                variable = new BasicFloatVariable(variableName);
                break;

            case StandardDataTypes.Integer:
                variable = new BasicInt32Variable(variableName);
                break;

            case StandardDataTypes.LongInteger:
                variable = new BasicInt64Variable(variableName);
                break;

            case StandardDataTypes.ShortInteger:
                variable = new BasicInt16Variable(variableName);
                break;

            case StandardDataTypes.Time:
                variable = new BasicTimeVariable(variableName);
                break;

            case StandardDataTypes.Object:
            case StandardDataTypes.Unknown:
            default:
                throw new Exception("Invalid data type specified.");
        }

        return variable;
    }
}
