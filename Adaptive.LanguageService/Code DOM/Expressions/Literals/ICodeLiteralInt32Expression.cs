using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.LanguageService.CodeDom.Expressions;

/// <summary>
/// Provides the signature definition for expressions that represents a n integer literal.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="ICodeExpression" />
public interface ICodeLiteralInt32Expression : ICodeLiteralExpression<int>
{
}
