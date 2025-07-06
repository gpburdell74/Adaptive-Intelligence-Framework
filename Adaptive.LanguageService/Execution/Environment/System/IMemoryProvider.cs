namespace Adaptive.Intelligence.LanguageService.Execution;

/// <summary>
/// Provides the signature definition for the API for accessing memory.
/// </summary>
/// <seealso cref="IDisposable" />
public interface IMemoryProvider : IDisposable 
{
    /// <summary>
    /// Allocates a sectioon of memory of the specified size.
    /// </summary>
    /// <param name="size">
    /// An integer specifying the size of memory to allocate.
    /// </param>
    /// <returns>
    /// A byte array containing the allocated memory, or null if the allocation fails.
    /// </returns>
    byte[]? Allocate(int size);

    /// <summary>
    /// Invokes garbage collection.
    /// </summary>
    void Collect();

    /// <summary>
    /// Frees the specified memory.
    /// </summary>
    /// <param name="memory">
    /// The reference to the memory content to free.
    /// </param>
    void Free(byte[]? memory);
}
