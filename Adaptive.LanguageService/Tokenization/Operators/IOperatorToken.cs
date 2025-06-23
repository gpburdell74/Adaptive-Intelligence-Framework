namespace Adaptive.Intelligence.LanguageService.Tokenization;

/// <summary>
/// Provides the signature definition for tokens that represent operators in the original text.
/// </summary>
/// <seealso cref="IToken" />
public interface IOperatorToken : IToken
{
    /// <summary>
    /// Gets or sets the type of the operator.
    /// </summary>
    /// <value>
    /// A <see cref="StandardOperatorTypes"/> enumerated value indicating the type of the operator being represented.
    /// </value>
    StandardOperatorTypes OperatorType { get; set; }
}
