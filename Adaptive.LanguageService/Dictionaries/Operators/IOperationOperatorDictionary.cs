using Adaptive.LanguageService.Providers;

namespace Adaptive.LanguageService;

/// <summary>
/// Provides the signature definition for implementations for  operational operator dictionaries.
/// </summary>
/// <seealso cref="ICodeDictionary" />
public interface IOperationalOperatorDictionary<DataType> : ISpecificOperatorDictionary<DataType>
    where DataType : Enum
{
    /// <summary>
    /// Populates the dictionary with the operational operators from the specified language provider.
    /// </summary>
    /// <param name="provider">
    /// The <see cref="IOperationOperatorProvider" /> provider instance used to provide the list.
    /// </param>
    void InitializeDictionary(IOperationOperatorProvider provider);

    /// <summary>
    /// Gets a value indicating whether the specified code is a  operational operator in the language being implemented.
    /// </summary>
    /// <param name="code">A string containing the code text to be evaluated.</param>
    /// <returns>
    /// <c>true</c> if the text can be matched to a known operational operator; otherwise, <c>false</c>.
    /// </returns>
    bool IsOperationalOperator(string? code);
}
