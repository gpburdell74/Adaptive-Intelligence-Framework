namespace Adaptive.Intelligence.LanguageService.Execution;

/// <summary>
/// Provides the signature definition for the API definitions for interacting
/// with the system environment.
/// </summary>
/// <seealso cref="IDisposable" />
public interface ISystem : IDisposable 
{
    /// <summary>
    /// Gets the reference to the file system API provider.
    /// </summary>
    /// <value>
    /// The <see cref="IFileSystemProvider"/> instance.
    /// </value>
    IFileSystemProvider FileSystem { get; }

    /// <summary>
    /// Gets the reference to the memory API provider.
    /// </summary>
    /// <value>M
    /// The <see cref="IMemoryProvider"/> instance.
    /// </value>
    IMemoryProvider Memory { get; }

    /// <summary>
    /// Gets the reference to the network API provider.
    /// </summary>
    /// <value>
    /// The <see cref="INetworkProvider"/> instance.
    /// </value>
    INetworkProvider Network { get; }

    /// <summary>
    /// Gets the reference to the operating system API provider.
    /// </summary>
    /// <value>
    /// The <see cref="IOsProvider"/> instance.
    /// </value>
    IOsProvider OS { get; }
}
