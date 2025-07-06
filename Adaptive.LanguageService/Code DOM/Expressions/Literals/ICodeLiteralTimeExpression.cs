using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.LanguageService.CodeDom.Expressions;

/// <summary>
/// Provides the signature definition for expressions that represents a time value literal.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="ICodeExpression" />
public interface ICodeLiteralTimeExpression : ICodeLiteralExpression<Time>
{
}
