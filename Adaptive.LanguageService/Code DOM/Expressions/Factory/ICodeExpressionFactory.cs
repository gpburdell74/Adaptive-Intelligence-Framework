using Adaptive.Intelligence.LanguageService.Services;
using Adaptive.Intelligence.LanguageService.Tokenization;

namespace Adaptive.Intelligence.LanguageService.CodeDom.Expressions;

/// <summary>
/// Provides the signature definition for a Code DOM Expression factory.
/// </summary>
public interface ICodeExpressionFactory
{
    #region Public Factory Methods / Functions
    /// <summary>
    /// Creates the expression from a single token.
    /// </summary>
    /// <param name="service">
    /// The reference to the <see cref="ILanguageService"/> implementation instance.
    /// </param>
    /// <param name="lineNumber">
    /// An integer specifying the current line number.
    /// </param>
    /// <param name="token">
    /// The reference to the <see cref="IToken"/> instance.
    /// </param>
    /// <returns>
    /// An <see cref="ICodeExpression"/> instance containing the CodeDom expression.
    /// </returns>
    public ICodeExpression CreateExpressionFromSingleToken(ILanguageService service, int lineNumber, IToken token);

    /// <summary>
    /// Creates the expression object from the provided list of tokens.
    /// </summary>
    /// <param name="service">
    /// The reference to the <see cref="ILanguageService"/> implementation instance.
    /// </param>
    /// <param name="lineNumber">
    /// An integer specifying the current line number.
    /// </param>
    /// <param name="tokenList">
    /// The <see cref="List{T}"/> of <see cref="IToken"/> instances.
    /// </param>
    /// <param name="startIndex">
    /// An optional integer indicating the ordinal index at which to start processing 
    /// <paramref name="tokenList"/>.
    /// </param>
    /// <returns>
    /// An <see cref="ICodeExpression"/> instance representing the expression defined
    /// by the provided list of parsed tokens.
    /// </returns>
    public ICodeExpression CreateFromTokens(ILanguageService service, int lineNumber,
        ManagedTokenList tokenList, int startIndex = 0);

    /// <summary>
    /// Creates the arithmetic expression.
    /// </summary>
    /// <param name="service">
    /// The reference to the <see cref="ILanguageService"/> implementation instance.
    /// </param>
    /// <param name="lineNumber">
    /// An integer specifying the current line number.
    /// </param>
    /// <param name="subList">
    /// The <see cref="List{T}"/> of <see cref="IToken"/> instances.
    /// </param>
    /// <returns>
    /// An <see cref="ICodeExpression"/> instance representing the expression defined
    /// by the provided list of parsed tokens.
    /// </returns>
    public ICodeExpression CreateArithmeticExpression(ILanguageService service, int lineNumber, ManagedTokenList subList);

    /// <summary>
    /// Creates the an expression representing a character literal value.
    /// </summary>
    /// <param name="service">
    /// The reference to the <see cref="ILanguageService"/> implementation instance.
    /// </param>
    /// <param name="lineNumber">
    /// An integer specifying the current line number.
    /// </param>
    /// <param name="subList">
    /// The <see cref="List{T}"/> of <see cref="IToken"/> instances.
    /// </param>
    /// <returns>
    /// An <see cref="ICodeLiteralCharExpression"/> instance representing the literal value.
    /// </returns>
    public ICodeLiteralCharExpression CreateLiteralCharExpression(ILanguageService service, int lineNumber, ManagedTokenList subList);

    /// <summary>
    /// Creates the an expression representing a string literal value.
    /// </summary>
    /// <param name="service">
    /// The reference to the <see cref="ILanguageService"/> implementation instance.
    /// </param>
    /// <param name="lineNumber">
    /// An integer specifying the current line number.
    /// </param>
    /// <param name="subList">
    /// The <see cref="List{T}"/> of <see cref="IToken"/> instances.
    /// </param>
    /// <returns>
    /// An <see cref="ICodeLiteralCharExpression"/> instance representing the literal value.
    /// </returns>
    public ICodeLiteralStringExpression CreateStringLiteralExpression(ILanguageService service, int lineNumber, ManagedTokenList subList);
    #endregion
}