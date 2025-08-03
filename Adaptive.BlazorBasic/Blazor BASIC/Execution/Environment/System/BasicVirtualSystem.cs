using Adaptive.Intelligence.LanguageService.Execution;
using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.BlazorBasic.Execution;

/// <summary>
/// Provides the implementation of the general system API for the language interpreter.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="ISystem" />
public sealed class BasicVirtualSystem : DisposableObjectBase, ISystem
{
    #region Private Member Declarations
    /// <summary>
    /// The file system API provider.
    /// </summary>
    private IFileSystemProvider? _fileSystem;
    /// <summary>
    /// The memory  API provider.
    /// </summary>
    private IMemoryProvider? _memory;
    /// <summary>
    /// The network API provider.
    /// </summary>
    private INetworkProvider? _network;
    /// <summary>
    /// The operating system API provider.
    /// </summary>
    private IOsProvider? _os;
    #endregion

    #region Constructor / Dispose Methods    
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicVirtualSystem"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public BasicVirtualSystem()
    {
        _fileSystem = new BasicFileSystemApiProvider();
        _memory = new BasicMemoryApiProvider();
        _network = new BasicNetworkApiProvider();
        _os = new BasicOsApiProvider();
    }
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        if (!IsDisposed && disposing)
        {
            _fileSystem?.Dispose();
            _memory?.Dispose();
            _network?.Dispose();
            _os?.Dispose();
        }

        _fileSystem = null;
        _memory = null;
        _network = null;
        _os?.Dispose();

        base.Dispose(disposing);
    }

    #endregion

    #region Public Properties    
    /// <summary>
    /// Gets the reference to the file system API provider.
    /// </summary>
    /// <value>
    /// The <see cref="IFileSystemProvider" /> instance.
    /// </value>
    public IFileSystemProvider? FileSystem => _fileSystem;

    /// <summary>
    /// Gets the reference to the memory API provider.
    /// </summary>
    /// <value>
    /// M
    /// The <see cref="IMemoryProvider" /> instance.
    /// </value>
    public IMemoryProvider? Memory => _memory;

    /// <summary>
    /// Gets the reference to the network API provider.
    /// </summary>
    /// <value>
    /// The <see cref="INetworkProvider" /> instance.
    /// </value>
    public INetworkProvider? Network => _network;

    /// <summary>
    /// Gets the reference to the operating system API provider.
    /// </summary>
    /// <value>
    /// The <see cref="IOsProvider" /> instance.
    /// </value>
    public IOsProvider? OS => _os;
    #endregion

    /// <summary>
    /// Resets this instance to its initial state.
    /// </summary>
    public void Reset()
    {
    }

}
