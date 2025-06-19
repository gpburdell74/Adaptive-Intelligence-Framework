namespace Adaptive.BlazorBasic.LanguageService;

/// <summary>
/// Provides the signature definition for implementing an operator dictionary for a language service.
/// </summary>
/// <seealso cref="IDisposable" />
public interface IOperatorDictionary : ILanguageDefinitionDictionary
{
    /// <summary>
    /// Gets the operator as specified by the provided text.
    /// </summary>
    /// <param name="operatorText">
    /// A string containing the code representation of the operator.
    /// </param>
    /// <returns>
    /// A <see cref="StandardOperatorTypes"/> enumerated value indicating the operator.
    /// </returns>
    StandardOperatorTypes GetOperator(string? operatorText);

    /// <summary>
    /// Gets the name of the operator.
    /// </summary>
    /// <param name="operatorType">
    /// A <see cref="StandardOperatorTypes"/> enumerated value indicating the operator.
    /// </param>
    /// <returns>
    /// A string containing the standard code/text for the specified operator in the language being implemented,
    /// or <b>null</b> if an invalid operator is specified.
    /// </returns>
    string? GetOperatorName(StandardOperatorTypes operatorType);

    /// <summary>
    /// Populates the dictionary with the operators from the specified language service.
    /// </summary>
    /// <param name="service">
    /// The <see cref="ILanguageService"/> provider instance used to provide the list.
    /// </param>
    void InitializeDictionary(ILanguageService service);

    /// <summary>
    /// Gets a value indicating whether the specified code is an operator in the language being implemented.
    /// </summary>
    /// <param name="code">
    /// A string containing the code text to be evaluated.
    /// </param>
    /// <returns>
    ///   <c>true</c> if the text can be matched to a known operator; otherwise, <c>false</c>.
    /// </returns>
    bool IsOperator(string? code);
}
