namespace Adaptive.Intelligence.LanguageService.CodeDom;

/// <summary>
/// Provides the signature definition for implementations that contain the entirety of parsed code
/// and the resulting list of <see cref="ILanguageCodeStatement"/> instances.
/// </summary>
/// <seealso cref="ILanguageCodeObject" />
public interface IExecutionUnit : ILanguageCodeObject
{
    /// <summary>
    /// Gets the reference to the list of statements to be executed.
    /// </summary>
    /// <value>
    /// A <see cref="List{T}"/> of <see cref="ILanguageCodeStatement"/> instances.
    /// </value>
    List<ILanguageCodeStatement>? Statements { get; }
}
