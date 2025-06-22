using Adaptive.Intelligence.Shared;
using Adaptive.LanguageService;
using Adaptive.LanguageService.Providers;

namespace Adaptive.BlazorBasic.LanguageService;

/// <summary>
/// Provides the implementation for the provider that defines the list of operational / functional operators.
/// </summary>
/// <seealso cref="DisposableObjectBase"/>
/// <seealso cref="IOperationOperatorProvider"/>
public sealed class OperationalOperatorProvider : DisposableObjectBase, IOperationOperatorProvider
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
            (int)BlazorBasicOperationalOperators.Increment,
            (int)BlazorBasicOperationalOperators.Decrement
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
            OperatorNames.OperatorIncrement,
            OperatorNames.OperatorDecrement
        };
    }
}
