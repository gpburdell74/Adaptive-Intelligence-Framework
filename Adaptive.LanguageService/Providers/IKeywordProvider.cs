namespace Adaptive.LanguageService.Providers;

/// <summary>
/// Provides the signature definition for implementations that define the list of keywords.
/// </summary>
/// <seealso cref="ICodeProvider"/>
public interface IKeywordProvider
{
    /// <summary>
    /// Renders the list of keyword ID values for mapping.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}"/> of unique ID values used for mapping ID to text content.
    /// </returns>
    List<int> RenderKeywordIds();

    /// <summary>
    /// Renders the list of keyword names for mapping.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}"/> of unique name values used for mapping text to ID values.
    /// </returns>
    List<string> RenderKeywordNames();
}
