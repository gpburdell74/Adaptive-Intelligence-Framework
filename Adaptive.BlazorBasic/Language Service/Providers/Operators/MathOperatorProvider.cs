using Adaptive.Intelligence.Shared;
using Adaptive.LanguageService.Providers;

namespace Adaptive.BlazorBasic.LanguageService;

/// <summary>
/// Provides the implementation for the provider that defines the list of arithmetic operators.
/// </summary>
/// <seealso cref="DisposableObjectBase"/>
/// <seealso cref="IComparisonOperatorProvider"/>
public sealed class MathOperatorProvider : DisposableObjectBase, IMathOperatorProvider
{
    /// <summary>
    /// Initializes the content of the provider.
    /// </summary>
    public void Initialize()
    {
    }

    /// <summary>
    /// Renders the list of operator ID values for mapping.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}" /> of unique ID values used for mapping ID to text content.
    /// </returns>
    public List<int> RenderOperatorIds()
    {
        return new List<int>()
        {
            (int)BlazorBasicMathOperators.Add,
            (int)BlazorBasicMathOperators.Subtract,
            (int)BlazorBasicMathOperators.Multiply,
            (int)BlazorBasicMathOperators.Divide,
            (int)BlazorBasicMathOperators.Exponent,
            (int)BlazorBasicMathOperators.Modulus
        };
    }

    /// <summary>
    /// Renders the list of operator names for mapping.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}" /> of unique name values used for mapping text to ID values.
    /// </returns>
    public List<string> RenderOperatorNames()
    {
        return new List<string>
        {
            OperatorNames.OperatorAdd,
            OperatorNames.OperatorSubtract,
            OperatorNames.OperatorMultiply,
            OperatorNames.OperatorDivide,
            OperatorNames.OperatorExponent,
            OperatorNames.OperatorModulus
        };
    }

}
