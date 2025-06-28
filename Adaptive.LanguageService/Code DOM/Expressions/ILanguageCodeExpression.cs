namespace Adaptive.Intelligence.LanguageService.CodeDom;

/// <summary>
/// Provides the signature definition for instances that represent generic code expressions.
/// </summary>
/// <seealso cref="IDisposable" />
public interface ILanguageCodeExpression : ILanguageCodeObject
{
    /// <summary>
    /// Renders the content of the expression into a string.
    /// </summary>
    /// <returns>
    /// A string containing the expression rendered into Blazor BASIC code.
    /// </returns>
    string? Render();
}
