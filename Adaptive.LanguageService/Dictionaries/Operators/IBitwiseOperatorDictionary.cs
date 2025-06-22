using Adaptive.LanguageService.Providers;

namespace Adaptive.LanguageService;

/// <summary>
/// Provides the signature definition for implementations for  bitwise operator dictionaries.
/// </summary>
/// <seealso cref="ICodeDictionary" />
public interface IBitwiseOperatorDictionary<DataType> : ISpecificOperatorDictionary<DataType>
    where DataType : Enum
{
    /// <summary>
    /// Populates the dictionary with the bitwise operators from the specified language provider.
    /// </summary>
    /// <param name="provider">
    /// The <see cref="IBitwiseOperatorProvider" /> provider instance used to provide the list.
    /// </param>
    void InitializeDictionary(IBitwiseOperatorProvider provider);

    /// <summary>
    /// Gets a value indicating whether the specified code is a bitwise operator in the language being implemented.
    /// </summary>
    /// <param name="code">A string containing the code text to be evaluated.</param>
    /// <returns>
    /// <c>true</c> if the text can be matched to a known bitwise operator; otherwise, <c>false</c>.
    /// </returns>
    bool IsBitwiseOperator(string? code);
}
