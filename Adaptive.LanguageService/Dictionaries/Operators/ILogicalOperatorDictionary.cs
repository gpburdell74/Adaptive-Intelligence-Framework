using Adaptive.LanguageService.Providers;

namespace Adaptive.LanguageService;

/// <summary>
/// Provides the signature definition for implementations for  logical operator dictionaries.
/// </summary>
/// <seealso cref="ICodeDictionary" />
public interface ILogicalOperatorDictionary<DataType> : ISpecificOperatorDictionary<DataType>
    where DataType : Enum
{
    /// <summary>
    /// Populates the dictionary with the logical operators from the specified language provider.
    /// </summary>
    /// <param name="provider">
    /// The <see cref="ILogicalOperatorProvider" /> provider instance used to provide the list.
    /// </param>
    void InitializeDictionary(ILogicalOperatorProvider provider);

    /// <summary>
    /// Gets a value indicating whether the specified code is a  logical operator in the language being implemented.
    /// </summary>
    /// <param name="code">A string containing the code text to be evaluated.</param>
    /// <returns>
    /// <c>true</c> if the text can be matched to a known logical operator; otherwise, <c>false</c>.
    /// </returns>
    bool IsLogicalOperator(string? code);
}
