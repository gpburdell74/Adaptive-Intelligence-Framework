namespace Adaptive.Intelligence.LanguageService.Providers;

/// <summary>
/// Provides the signature definition for a code provider for built-in functions.
/// </summary>
/// <seealso cref="ICodeProvider" />
public interface IBuiltInFunctionProvider : ICodeProvider
{
    /// <summary>
    /// Renders the list of function ID values for mapping.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}"/> of unique ID values used for mapping ID to text content.
    /// </returns>
    List<int> RenderFunctionIds();

    /// <summary>
    /// Renders the list of function names for mapping.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}"/> of unique function name values used for mapping text to ID values.
    /// </returns>
    List<string> RenderFunctionNames();
}
