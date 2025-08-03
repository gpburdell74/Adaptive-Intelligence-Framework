using Adaptive.Intelligence.LanguageService.Providers;

namespace Adaptive.Intelligence.LanguageService.Dictionaries;

/// <summary>
/// Provides the signature definition for implementations for built-in function dictionaries.
/// </summary>
/// <seealso cref="ICodeDictionary" />
public interface IBuiltInFunctionDictionary<FunctionType> : ICodeDictionary
    where FunctionType : Enum
{
    /// <summary>
    /// Gets the function as specified by the provided text.
    /// </summary>
    /// <param name="functionText">
    /// A string containing the name of the built-in function.
    /// </param>
    /// <returns>
    /// A <typeparamref name="FunctionType"/> enumerated value containing the unique ID value identifying the function.
    /// </returns>
    FunctionType GetBuiltInFunctionType(string? functionText);

    /// <summary>
    /// Gets the text/name for the built-in function.
    /// </summary>
    /// <param name="functionId">
    /// A <typeparamref name="FunctionType"/> containing the unique ID value identifying the function.
    /// </param>
    /// <returns>
    /// A string containing the standard code/text for the specified function in the language being implemented,
    /// or <b>null</b> if an invalid function name is specified.
    /// </returns>
    string? GetBuiltInFunctionText(FunctionType functionId);

    /// <summary>
    /// Populates the dictionary with the functions from the specified language provider.
    /// </summary>
    /// <param name="provider">
    /// The <see cref="IBuiltInFunctionProvider" /> provider instance used to provide the list.
    /// </param>
    void InitializeDictionary(IBuiltInFunctionProvider provider);

    /// <summary>
    /// Gets a value indicating whether the specified code is a built-in function in the language being implemented.
    /// </summary>
    /// <param name="code">A string containing the code text to be evaluated.</param>
    /// <returns>
    /// <c>true</c> if the text can be matched to a known function; otherwise, <c>false</c>.
    /// </returns>
    bool IsBuiltInFunction(string? code);
}
