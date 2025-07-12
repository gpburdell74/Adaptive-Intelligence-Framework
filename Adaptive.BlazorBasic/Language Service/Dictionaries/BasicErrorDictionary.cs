using Adaptive.Intelligence.LanguageService.Dictionaries;
using Adaptive.Intelligence.LanguageService.Providers;

namespace Adaptive.Intelligence.BlazorBasic.LanguageService;

/// <summary>
/// Provides the delimiter dictionary for the Blazor BASIC language.
/// </summary>
/// <seealso cref="TwoWayDictionaryBase{K, V}" />
/// <seealso cref="IDelimiterDictionary{T}" />
public sealed class BasicErrorDictionary : TwoWayDictionaryBase<string, BlazorBasicErrorCodes>, IErrorDictionary<BlazorBasicErrorCodes>
{
    /// <summary>
    /// Gets the text/name for the built-in error.
    /// </summary>
    /// <param name="errorCode">
    /// A <see cref="BlazorBasicErrorCodes" /> enumerated value containing the unique ID value identifying the error. 
    /// This can double as the error code.
    /// </param>
    /// <returns>
    /// A string containing the standard code/text for the specified error in the language being implemented,
    /// or <b>null</b> if an invalid error name is specified.
    /// </returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public string? GetErrorText(BlazorBasicErrorCodes errorCode)
    {
        return ReverseGet(errorCode);
    }
    /// <summary>
    /// Gets the error as specified by the provided text.
    /// </summary>
    /// <param name="errorText">A string containing the name of the built-in error.</param>
    /// <returns>
    /// A <see cref="BlazorBasicErrorCodes"/> enumerated value the unique ID value identifying the error.
    /// </returns>
    public BlazorBasicErrorCodes GetErrorType(string? errorText)
    {
        return Get(errorText);
    }
    /// <summary>
    /// Populates the dictionary with the errors from the specified language provider.
    /// </summary>
    /// <param name="provider">
    /// The <see cref="IErrorProvider" /> provider instance used to provide the list.
    /// </param>
    public void InitializeDictionary(IErrorProvider provider)
    {
        // Get the list of delimiters.
        List<string> names = provider.RenderErrorNames();

        // Get the list of ID values and translate to the appropriate list of enumerations.
        List<int> idValues = provider.RenderErrorIds();
        List<BlazorBasicErrorCodes> typeList = IdsToEnum<BlazorBasicErrorCodes>(idValues);
        idValues.Clear();

        BaseInitialize(names, typeList);

        typeList.Clear();
        names.Clear();
    }
    /// <summary>
    /// Gets a value indicating whether the specified code is a built-in error in the language being implemented.
    /// </summary>
    /// <param name="code">A string containing the code text to be evaluated.</param>
    /// <returns>
    /// <c>true</c> if the text can be matched to a known error; otherwise, <c>false</c>.
    /// </returns>
    public bool IsError(string? code)
    {
        return IsInDictionary(code);
    }
    /// <summary>
    /// Normalizes the specified value for use in a dictionary as a key value.
    /// </summary>
    /// <param name="value">
    /// A string variable containing the value.</param>
    /// <returns>
    /// The normalized version of the value for use in a dictionary as a key.
    /// </returns>
    protected override string NormalizeKeyValue(string value)
    {
        return value.ToUpper().Trim();
    }
}
