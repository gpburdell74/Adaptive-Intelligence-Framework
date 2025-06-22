namespace Adaptive.LanguageService.Providers;

/// <summary>
/// Provides the signature definition for implementations that define the list of bitwise operators.
/// </summary>
/// <seealso cref="ICodeProvider"/>
public interface IBitwiseOperatorProvider : ICodeProvider
{
    /// <summary>
    /// Renders the list of operator ID values for mapping.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}"/> of unique ID values used for mapping ID to text content.
    /// </returns>
    List<int> RenderOperatorIds();

    /// <summary>
    /// Renders the list of operator names for mapping.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}"/> of unique name values used for mapping text to ID values.
    /// </returns>
    List<string> RenderOperatorNames();
}
