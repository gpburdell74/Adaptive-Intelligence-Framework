using Adaptive.Intelligence.Shared;
using Adaptive.LanguageService.Providers;

namespace Adaptive.BlazorBasic.LanguageService;

/// <summary>
/// Provides the implementation for the provider that defines the list of logical operators.
/// </summary>
/// <seealso cref="DisposableObjectBase"/>
/// <seealso cref="ILogicalOperatorProvider"/>
public sealed class LogicalOperatorProvider : DisposableObjectBase, ILogicalOperatorProvider
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
            (int)BlazorBasicLogicalOperators.And,
            (int)BlazorBasicLogicalOperators.AndShort,
            (int)BlazorBasicLogicalOperators.Or,
            (int)BlazorBasicLogicalOperators.OrShort,
            (int)BlazorBasicLogicalOperators.Not,
            (int)BlazorBasicLogicalOperators.NotShort
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
            OperatorNames.OperatorLogicalAnd,
            OperatorNames.OperatorLogicalAndShort,
            OperatorNames.OperatorLogicalOr,
            OperatorNames.OperatorLogicalOrShort,
            OperatorNames.OperatorLogicalNot,
            OperatorNames.OperatorLogicalNotShort
        };
    }

}
