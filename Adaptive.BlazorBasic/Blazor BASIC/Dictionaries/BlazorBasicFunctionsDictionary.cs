using Adaptive.BlazorBasic.LanguageService;
using Adaptive.Intelligence.Shared;

namespace Adaptive.BlazorBasic;

/// <summary>
/// Provides the built-in functions dictionary for the Blazor BASIC language.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="TwoWayDictionaryBase{PrimaryValue, SecondaryValue}" />
/// <seealso cref="BlazorBasicFunctions"/>
public class BlazorBasicFunctionsDictionary : TwoWayDictionaryBase<string, BlazorBasicFunctions>, IFunctionDictionary<BlazorBasicFunctions, BlazorBasicKeywords>
{
    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicFunctionsDictionary"/> class.
    /// </summary>
    public BlazorBasicFunctionsDictionary()
    {

    }
    #endregion

    #region Public Methods / Functions    
    /// <summary>
    /// Gets the function as specified by the provided text.
    /// </summary>
    /// <param name="functionName">
    /// A string containing the name of the built-in function.
    /// </param>
    /// <returns>
    /// A <see cref="BlazorBasicFunctions"/> enumerated value indicating the function.
    /// </returns>
    public BlazorBasicFunctions GetFunctionType(string? functionName)
    {
        return Get(NormalizeString(functionName));
    }

    /// <summary>
    /// Gets the text/name for the built-in function.
    /// </summary>
    /// <param name="function">
    /// A <see cref="BlazorBasicFunctions"/> enumerated value indicating the function.
    /// </param>
    /// <returns>
    /// A string containing the standard code/text for the specified function in the language being implemented,
    /// or <b>null</b> if an invalid function name is specified.
    /// </returns>
    public string? GetFunctionText(BlazorBasicFunctions function)
    {
        return ReverseGet(function);
    }

    /// <summary>
    /// Populates the dictionary with the functions from the specified language provider.
    /// </summary>
    /// <param name="service">
    /// The <see cref="ILanguageService{F, K}" /> service instance used to provide the list of built-in functions.
    /// </param>
    public void InitializeDictionary(ILanguageService<BlazorBasicFunctions, BlazorBasicKeywords> service)
    {
        List<string> functionList = service.RenderBuiltInFunctions();

        foreach (string functionName in functionList)
        {
            AddEntry(functionName, NormalizeString(functionName), service.MapFunction(functionName));
        }
    }

    /// <summary>
    /// Gets a value indicating whether the specified code is a built-in function in the language being implemented.
    /// </summary>
    /// <param name="code">A string containing the code text to be evaluated.</param>
    /// <returns>
    /// <c>true</c> if the text can be matched to a known function; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public bool IsFunction(string? code)
    {
        return IsInDictionary(NormalizeString(code));
    }
    #endregion
}