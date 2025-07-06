namespace Adaptive.Intelligence.LanguageService.Execution;

/// <summary>
/// Provides the signature definition for an element contained within a specific scope of execution.
/// </summary>
/// <seealso cref="IDisposable" />
public interface IScopedElement : IDisposable 
{
    /// <summary>
    /// Gets the reference to the parent scope container.
    /// </summary>
    /// <value>
    /// A reference to the parent <see cref="IScopeContainer"/> instance.
    /// </value>
    IScopeContainer? Parent { get; }
}
