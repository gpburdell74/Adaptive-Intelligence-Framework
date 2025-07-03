namespace Adaptive.Intelligence.LanguageService.Execution;

/// <summary>
/// Provides the signature definition for an instance of a code item, such as a function, procedure,
/// variable, etc that is managed in the global environment.
/// </summary>
/// <seealso cref="IDisposable" />
public interface ICodeItemInstance : IDisposable 
{
    /// <summary>
    /// Gets the unique ID value assigned to the instance.
    /// </summary>
    /// <value>
    /// An integer containing the ID value.
    /// </value>
    int Id { get; }

    /// <summary>
    /// Gets the name of the item.
    /// </summary>
    /// <value>
    /// A string containing the instance's name.
    /// </value>
    string? Name { get; }
}
