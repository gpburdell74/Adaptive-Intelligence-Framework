namespace Adaptive.Intelligence.LanguageService.Providers;

/// <summary>
/// Provides the signature definition for implementations that define the list of errors.
/// </summary>
/// <seealso cref="ICodeProvider"/>
public interface IErrorProvider
{
    /// <summary>
    /// Renders the list of error ID values (e.g. error codes) for mapping.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}"/> of unique ID values used for mapping ID to text content.
    /// </returns>
    List<int> RenderErrorIds();

    /// <summary>
    /// Renders the list of error names for mapping.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}"/> of unique name values used for mapping text to ID values.
    /// </returns>
    List<string> RenderErrorNames();
}
