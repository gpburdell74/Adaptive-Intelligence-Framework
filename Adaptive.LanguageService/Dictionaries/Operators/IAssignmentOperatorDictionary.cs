using Adaptive.LanguageService.Providers;

namespace Adaptive.LanguageService;

/// <summary>
/// Provides the signature definition for implementations for assignment operator dictionaries.
/// </summary>
/// <seealso cref="ICodeDictionary" />
public interface IAssignmentOperatorDictionary<DataType> : ISpecificOperatorDictionary<DataType>
    where DataType : Enum
{
    /// <summary>
    /// Populates the dictionary with the assignment operators from the specified language provider.
    /// </summary>
    /// <param name="provider">
    /// The <see cref="IAssignmentOperatorProvider" /> provider instance used to provide the list.
    /// </param>
    void InitializeDictionary(IAssignmentOperatorProvider provider);

    /// <summary>
    /// Gets a value indicating whether the specified code is a built-in assignment operator in the language being implemented.
    /// </summary>
    /// <param name="code">A string containing the code text to be evaluated.</param>
    /// <returns>
    /// <c>true</c> if the text can be matched to a known assignment operator; otherwise, <c>false</c>.
    /// </returns>
    bool IsAssignmentOperator(string? code);
}
