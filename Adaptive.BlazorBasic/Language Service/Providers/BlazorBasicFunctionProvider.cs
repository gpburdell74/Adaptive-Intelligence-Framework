using Adaptive.Intelligence.LanguageService.Providers;
using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.BlazorBasic.LanguageService;

/// <summary>
/// Provides the implementation for the provider that defines the list of built-in functions.
/// </summary>
public sealed class BlazorBasicFunctionProvider : DisposableObjectBase, IBuiltInFunctionProvider
{
    /// <summary>
    /// Initializes the content of the provider.
    /// </summary>
    public void Initialize()
    {
    }

    /// <summary>
    /// Renders the list of function ID values for mapping.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}" /> of unique ID values used for mapping ID to text content.
    /// </returns>
    public List<int> RenderFunctionIds()
    {
        return new List<int>
        {
            (int)BlazorBasicFunctions.Abs,
            (int)BlazorBasicFunctions.Asc,
            (int)BlazorBasicFunctions.Chr,
            (int)BlazorBasicFunctions.Cos,
            (int)BlazorBasicFunctions.LTrim,
            (int)BlazorBasicFunctions.RTrim,
            (int)BlazorBasicFunctions.Sin,
            (int)BlazorBasicFunctions.Trim,
            (int)BlazorBasicFunctions.Ver,
        };
    }

    /// <summary>
    /// Renders the list of function names for mapping.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}" /> of unique function name values used for mapping text to ID values.
    /// </returns>
    public List<string> RenderFunctionNames()
    {
        return new List<string>
        {
            FunctionNames.FunctionAbs,
            FunctionNames.FunctionAsc,
            FunctionNames.FunctionChr,
            FunctionNames.FunctionCos,
            FunctionNames.FunctionLTrim,
            FunctionNames.FunctionRTrim,
            FunctionNames.FunctionSin,
            FunctionNames.FunctionTrim,
            FunctionNames.FunctionVer
        };
    }
}
