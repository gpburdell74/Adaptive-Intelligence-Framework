namespace Adaptive.BlazorBasic.LanguageService;

/// <summary>
/// Provides the signature definition for implementing dictionary that contain a unique list of language-specific items.
/// </summary>
/// <seealso cref="System.IDisposable" />
public interface ILanguageDefinitionDictionary : IDisposable 
{
    /// <summary>
    /// Renders a list of the unique keys in the dictionary.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}"/> of <see cref="string"/> values containing the unique list of 
    /// text items stored in the dictionary.
    /// </returns>
    List<string> RenderUniqueKeys();
}
