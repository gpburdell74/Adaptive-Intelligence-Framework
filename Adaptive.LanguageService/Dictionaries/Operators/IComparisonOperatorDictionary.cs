using Adaptive.LanguageService.Providers;

namespace Adaptive.LanguageService;

/// <summary>
/// Provides the signature definition for implementations for  comparison operator dictionaries.
/// </summary>
/// <seealso cref="ICodeDictionary" />
public interface IComparisonOperatorDictionary<DataType> : ISpecificOperatorDictionary<DataType>
    where DataType : Enum
{
    /// <summary>
    /// Populates the dictionary with the comparison operators from the specified language provider.
    /// </summary>
    /// <param name="provider">
    /// The <see cref="IComparisonOperatorProvider" /> provider instance used to provide the list.
    /// </param>
    void InitializeDictionary(IComparisonOperatorProvider provider);

    /// <summary>
    /// Gets a value indicating whether the specified code is a  comparison operator in the language being implemented.
    /// </summary>
    /// <param name="code">A string containing the code text to be evaluated.</param>
    /// <returns>
    /// <c>true</c> if the text can be matched to a known comparison operator; otherwise, <c>false</c>.
    /// </returns>
    bool IsComparisonOperator(string? code);
}
