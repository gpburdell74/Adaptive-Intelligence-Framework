namespace Adaptive.Intelligence.LanguageService.Execution;

/// <summary>
/// Provides the base definition for an ID generator.
/// </summary>
/// <seealso cref="System.IDisposable" />
public interface IIdGenerator : IDisposable 
{
    /// <summary>
    /// Gets the next available ID value.
    /// </summary>
    /// <returns>
    /// An integer containing the ID value.
    /// </returns>
    int Next();

    /// <summary>
    /// Releases the specified ID value from use.
    /// </summary>
    /// <param name="id">
    /// An integer containing the ID value to be released.
    /// </param>
    void Release(int id);
}
