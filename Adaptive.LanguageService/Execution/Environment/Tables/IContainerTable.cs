namespace Adaptive.Intelligence.LanguageService.Execution;

/// <summary>
/// Provides the signature definition for any basic table storing code items in the global environment.
/// </summary>
/// <seealso cref="IDisposable" />
public interface IContainerTable : IDisposable
{
    /// <summary>
    /// Gets the number of items in the table.
    /// </summary>
    /// <value>
    /// An integer containing the count of items.
    /// </value>
    int Count { get; }

    /// <summary>
    /// Determines if an item with the specified name in the table exists.
    /// </summary>
    /// <param name="name">
    /// A string containing the name to query for.
    /// </param>
    /// <returns>
    /// <b>true</b> if an entry with the specified name exists; otherwise,
    /// returns <b>false</b>/
    /// </returns>
    bool Exists(string name);
}
