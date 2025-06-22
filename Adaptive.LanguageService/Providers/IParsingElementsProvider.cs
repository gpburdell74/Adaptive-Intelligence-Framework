namespace Adaptive.LanguageService.Providers;

/// <summary>
/// Provides the signature definition for implementations that define the list of parsing elements.
/// </summary>
/// <seealso cref="ICodeProvider"/>
public interface IParsingElementsProvider : ICodeProvider
{
    /// <summary>
    /// Renders the list of parsing element ID values for mapping.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}"/> of unique ID values used for mapping ID to text content.
    /// </returns>
    List<int> RenderParsingElementIds();

    /// <summary>
    /// Renders the list of parsing elements for mapping.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}"/> of unique name values used for mapping text to ID values.
    /// </returns>
    List<string> RenderParsingElements();
}
