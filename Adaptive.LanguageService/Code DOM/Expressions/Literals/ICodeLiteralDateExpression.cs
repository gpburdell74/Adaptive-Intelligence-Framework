using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.LanguageService.CodeDom.Expressions;

/// <summary>
/// Provides the signature definition for expressions that represents a Date/Time literal
/// that only includes the date value.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="ICodeExpression" />
public interface ICodeLiteralDateExpression : ICodeLiteralExpression<DateTime>
{
}
