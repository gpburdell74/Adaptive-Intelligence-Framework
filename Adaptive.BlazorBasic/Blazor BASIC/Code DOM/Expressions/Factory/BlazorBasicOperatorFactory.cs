namespace Adaptive.Intelligence.BlazorBasic.CodeDom;

/// <summary>
/// Provides static methods / functions for creating CodeDOM expression instances.
/// </summary>
public static class BlazorBasicOperatorFactory
{
    public static BlazorBasicMathOperators GetMathOperator(string text)
    {
        BlazorBasicMathOperators mathOperator = BlazorBasicMathOperators.Unknown;
        text = text.Trim().ToLowerInvariant();
        switch (text)
        {
            case OperatorNames.OperatorAdd:
                mathOperator = BlazorBasicMathOperators.Add;
                break;

            case OperatorNames.OperatorSubtract:
                mathOperator = BlazorBasicMathOperators.Subtract;
                break;

            case OperatorNames.OperatorMultiply:
                mathOperator = BlazorBasicMathOperators.Multiply;
                break;

            case OperatorNames.OperatorDivide:
                mathOperator = BlazorBasicMathOperators.Divide;
                break;

            case OperatorNames.OperatorModulus:
                mathOperator = BlazorBasicMathOperators.Modulus;
                break;

            case OperatorNames.OperatorExponent:
                mathOperator = BlazorBasicMathOperators.Exponent;
                break;

            default:
                mathOperator = BlazorBasicMathOperators.Unknown;
                break;
        }
        return mathOperator;
    }

    public static string GetOperatorText(BlazorBasicMathOperators operatorItem)
    {
        string text = string.Empty;

        switch(operatorItem)
        {
            case BlazorBasicMathOperators.Add:
                text = OperatorNames.OperatorAdd;
                break;

            case BlazorBasicMathOperators.Subtract:
                text = OperatorNames.OperatorSubtract;
                break;

            case BlazorBasicMathOperators.Multiply:
                text = OperatorNames.OperatorMultiply;
                break;

            case BlazorBasicMathOperators.Divide:
                text = OperatorNames.OperatorDivide;
                break;

            case BlazorBasicMathOperators.Modulus:
                text = OperatorNames.OperatorModulus;
                break;

            case BlazorBasicMathOperators.Exponent:
                text = OperatorNames.OperatorExponent;
                break;

        }
        return text;
    }
}
