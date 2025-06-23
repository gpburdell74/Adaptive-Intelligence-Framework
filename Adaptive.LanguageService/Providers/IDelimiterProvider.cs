namespace Adaptive.Intelligence.LanguageService.Providers;

/// <summary>
/// Provides the signature definition for implementations that define the list of delimiters.
/// </summary>
/// <seealso cref="ICodeProvider"/>
public interface IDelimiterProvider
{
    /// <summary>
    /// Renders the list of delimiter ID values for mapping.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}"/> of unique ID values used for mapping ID to text content.
    /// </returns>
    List<int> RenderDelimiterIds();

    /// <summary>
    /// Renders the list of delimiter names / text for mapping.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}"/> of unique name values used for mapping text to ID values.
    /// </returns>
    List<string> RenderDelimiterNames();
}
