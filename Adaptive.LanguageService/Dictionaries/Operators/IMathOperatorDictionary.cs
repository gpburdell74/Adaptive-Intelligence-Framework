using Adaptive.LanguageService.Providers;

namespace Adaptive.LanguageService;

/// <summary>
/// Provides the signature definition for implementations for  arithmetic operator dictionaries.
/// </summary>
/// <seealso cref="ICodeDictionary" />
public interface IMathOperatorDictionary<DataType> : ISpecificOperatorDictionary<DataType>
    where DataType : Enum
{
    /// <summary>
    /// Populates the dictionary with the arithmetic operators from the specified language provider.
    /// </summary>
    /// <param name="provider">
    /// The <see cref="IMathOperatorProvider" /> provider instance used to provide the list.
    /// </param>
    void InitializeDictionary(IMathOperatorProvider provider);

    /// <summary>
    /// Gets a value indicating whether the specified code is a  arithmetic operator in the language being implemented.
    /// </summary>
    /// <param name="code">A string containing the code text to be evaluated.</param>
    /// <returns>
    /// <c>true</c> if the text can be matched to a known arithmetic operator; otherwise, <c>false</c>.
    /// </returns>
    bool IsMathOperator(string? code);
}
