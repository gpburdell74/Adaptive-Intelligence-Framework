using Adaptive.Intelligence.LanguageService.Services;

namespace Adaptive.Intelligence.LanguageService.Tokenization;

/// <summary>
/// Provides the signature definition for implementing factory methods for creating tokens.
/// </summary>
/// <seealso cref="IDisposable" />
public interface ITokenFactory<DelimiterType, ErrorType, FunctionType, KeywordType, OperatorType> : IDisposable
    where DelimiterType : Enum
    where ErrorType : Enum
    where FunctionType : Enum
    where KeywordType : Enum
    where OperatorType : Enum
{
    /// <summary>
    /// Determines whether the provided text matches a single-length character token.
    /// </summary>
    /// <remarks>
    /// This is used in the parsing process since some single-character values in the code may or may not be preceded or followed
    /// by separator delimiters.
    /// </remarks>
    /// <param name="character">
    /// A string containing the character to be checked.
    /// </param>
    /// <returns>
    ///   <c>true</c> the provided character represents a known token or token type; otherwise, <c>false</c>.
    /// </returns>
    bool IsSingleCharToken(string character);

    /// <summary>
    /// Creates a token instance from the provided text.
    /// </summary>
    /// <param name="originalCode">
    /// A string containing the original code to be parsed.
    /// </param>
    /// <returns>
    /// An <see cref="IToken"/> implementation.
    /// </returns>
    IToken CreateToken(string originalCode);

    /// <summary>
    /// Determines the type of the token being represented by the provided text.
    /// </summary>
    /// <param name="originalCode">
    /// A string containing the original code to be parsed.
    /// </param>
    /// <returns>
    /// A <see cref="TokenType"/> enumerated value indicating the token type to create.
    /// </returns>
    TokenType DetermineTokenType(string originalCode);

    /// <summary>
    /// Determines whether the specified original code is numeric.
    /// </summary>
    /// <remarks>
    /// An implementation of this method should check for leading sign values (+ or -), as well as zero or one
    /// decimal points.
    /// </remarks>
    /// <param name="originalCode">
    /// A string containing the original code to be parsed.
    /// </param>
    /// <returns>
    ///   <c>true</c> if the specified original code is numeric; otherwise, <c>false</c>.
    /// </returns>
    bool IsNumeric(string originalCode);

    /// <summary>
    /// Initializes the factory instance using the specified language service reference.
    /// </summary>
    /// <param name="service">
    /// The <see cref="ILanguageService{D, E, F, K, O}"/> instance to use to initialize the factory.
    /// </param>
    void Initialize(ILanguageService<DelimiterType, ErrorType, FunctionType, KeywordType, OperatorType> service);
}