using Adaptive.Intelligence.BlazorBasic.Services;
using Adaptive.Intelligence.LanguageService.CodeDom;

namespace Adaptive.Intelligence.BlazorBasic.CodeDom;

/// <summary>
/// Represents a literal value expression.
/// </summary>
/// <seealso cref="BlazorBasicExpression" />
/// <seealso cref="ILanguageCodeExpression" />
/// <typeparam name="T">
/// The data type of the literal value.
/// </typeparam>
public abstract class BlazorBasicLiteralExpression<T> : BlazorBasicExpression, ILanguageCodeExpression
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicLiteralExpression{T}"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the language service instance being injected.
    /// </param>
    public BlazorBasicLiteralExpression(BlazorBasicLanguageService service) : base(service)
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicLiteralExpression{T}"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the language service instance being injected.
    /// </param>
    /// <param name="literalValue">
    /// The literal value being represented.
    /// </param>
    public BlazorBasicLiteralExpression(BlazorBasicLanguageService service, T literalValue) : base(service)
    {
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets or sets the literal value.
    /// </summary>
    /// <value>
    /// A <typeparamref name="T"/> containing the literal value being represented by this expression.
    /// </value>
    public T Value { get; set; } = default;
    #endregion

}
