using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.LanguageService.CodeDom.Expressions;

/// <summary>
/// Provides the signature definition for expressions that represents a Date/Time literal.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="ICodeExpression" />
public interface ICodeLiteralDateTimeExpression : ICodeLiteralExpression<DateTime>
{
}
