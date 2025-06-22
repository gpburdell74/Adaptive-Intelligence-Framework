namespace Adaptive.LanguageService.Tokenization;

/// <summary>
/// Provides the signature definition for a token to represent a number value with a decimal point.
/// </summary>
/// <seealso cref="IToken" />
/// <seealso cref="IDataTypeToken{T}" />
public interface IFloatingPointToken : IDataTypeToken<double>
{
}
