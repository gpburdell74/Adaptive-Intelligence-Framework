namespace Adaptive.Intelligence.LanguageService.Tokenization;

/// <summary>
/// Provides the signature definition for tokens representing the content of a specific data type.
/// </summary>
/// <remarks>
/// These tokens are used to differentiate between integer type (such as 1234), floating-point types,
/// such as (3.14159265), and others.
/// </remarks>
/// <seealso cref="IToken" />
/// <typeparam name="BasicDataType">
/// The basic underlying data type being represented.
/// </typeparam>
public interface IDataTypeToken<BasicDataType> : IToken
{
    /// <summary>
    /// Gets the value being represented.
    /// </summary>
    /// <returns>
    /// The <typeparamref name="BasicDataType"/> value, if it can be provided/parsed.
    /// </returns>
    BasicDataType GetValue();
}
