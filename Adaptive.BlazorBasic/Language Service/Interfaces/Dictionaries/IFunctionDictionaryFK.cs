namespace Adaptive.BlazorBasic.LanguageService;

/// <summary>
/// Provides the signature definition for implementing a data type dictionary for language service.
/// </summary>
/// <seealso cref="IDisposable" />
/// <typeparam name="FunctionsEnum">
/// The data type of the numeration defining the list of supported functions.
/// </typeparam>
/// <typeparam name="KeywordsEnum">
/// The data type of the numeration defining the list of supported keywords.
/// </typeparam>
public interface IFunctionDictionary<FunctionsEnum, KeywordsEnum> : ILanguageDefinitionDictionary
    where FunctionsEnum : Enum
    where KeywordsEnum : Enum
{
    /// <summary>
    /// Gets the function as specified by the provided text.
    /// </summary>
    /// <param name="functionName">
    /// A string containing the name of the built-in function.
    /// </param>
    /// <returns>
    /// A <typeparamref name="T"/> enumerated value indicating the function.
    /// </returns>
    FunctionsEnum GetFunctionType(string? functionName);

    /// <summary>
    /// Gets the text/name for the built-in function.
    /// </summary>
    /// <param name="function">
    /// A <typeparamref name="T"/> enumerated value indicating the function.
    /// </param>
    /// <returns>
    /// A string containing the standard code/text for the specified function in the language being implemented,
    /// or <b>null</b> if an invalid function name is specified.
    /// </returns>
    string? GetFunctionText(FunctionsEnum function);

    /// <summary>
    /// Populates the dictionary with the functions from the specified language provider.
    /// </summary>
    /// <param name="service">
    /// The <see cref="ILanguageService{F, K}"/> service instance used to provide the list of built-in functions.
    /// </param>
    void InitializeDictionary(ILanguageService<FunctionsEnum, KeywordsEnum> service);

    /// <summary>
    /// Gets a value indicating whether the specified code is a built-in function in the language being implemented.
    /// </summary>
    /// <param name="code">
    /// A string containing the code text to be evaluated.
    /// </param>
    /// <returns>
    ///   <c>true</c> if the text can be matched to a known function; otherwise, <c>false</c>.
    /// </returns>
    bool IsFunction(string? code);
}
