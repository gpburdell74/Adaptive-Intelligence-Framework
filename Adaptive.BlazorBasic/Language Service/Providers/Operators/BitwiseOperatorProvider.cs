using Adaptive.Intelligence.Shared;
using Adaptive.LanguageService.Providers;

namespace Adaptive.BlazorBasic.LanguageService;

/// <summary>
/// Provides the implementation for the provider that defines the list of bitwise operators.
/// </summary>
/// <seealso cref="DisposableObjectBase"/>
/// <seealso cref="IBitwiseOperatorProvider"/>
public sealed class BitwiseOperatorProvider : DisposableObjectBase, IBitwiseOperatorProvider
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
            (int)BlazorBasicBitwiseOperators.ShortAnd,
            (int)BlazorBasicBitwiseOperators.LongAnd,
            (int)BlazorBasicBitwiseOperators.ShortOr,
            (int)BlazorBasicBitwiseOperators.LongOr
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
            OperatorNames.OperatorBitwiseAnd,
            OperatorNames.OperatorBitwiseLongAnd,
            OperatorNames.OperatorBitwiseOr,
            OperatorNames.OperatorBitwiseLongOr
        };
    }
}
