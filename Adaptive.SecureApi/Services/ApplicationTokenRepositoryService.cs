using Adaptive.Intelligence.Shared;
using Adaptive.SecureApi.Server;
using Adaptive.SecureApi.Tokens;

namespace Adaptive.SecureApi.Services;

/// <summary>
/// Provides an injectable service for creating application authorization token repository services.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="IApplicationTokenRepositoryService" />
public class ApplicationTokenRepositoryService : DisposableObjectBase, IApplicationTokenRepositoryService
{
    #region Private mmeber Declarations

    /// <summary>
    /// Default to the in-memory (test) repository type.
    /// </summary>
    private Type _repositoryType = typeof(InMemoryApplicationTokenRepository);

    #endregion

    #region Constructors    
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationTokenRepositoryService"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public ApplicationTokenRepositoryService()
    {
        _repositoryType = typeof(InMemoryApplicationTokenRepository);
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationTokenRepositoryService"/> class.
    /// </summary>
    /// <param name="repositoryType">Type of the repository.</param>
    /// <exception cref="System.ArgumentOutOfRangeException">repositoryType</exception>
    public ApplicationTokenRepositoryService(Type repositoryType)
    {
        if (repositoryType is IApplicationTokenRepository)
            _repositoryType = repositoryType;
        else
            throw new ArgumentOutOfRangeException(nameof(repositoryType) + " is not an IApplicationTokenRepository type.");
    }
    #endregion

    #region Public Methods / Functions    
    /// <summary>
    /// Creates the new repository instance asynchronous.
    /// </summary>
    /// <returns>
    /// A new <see cref="IApplicationTokenRepository"/> instance.
    /// </returns>
    public virtual Task<IApplicationTokenRepository> CreateNewRepositoryInstanceAsync()
    {
        return Task.FromResult(
            (IApplicationTokenRepository)Activator.CreateInstance(_repositoryType));
    }

    /// <summary>
    /// Takes care of disposing and deallocating the repository instance.
    /// </summary>
    /// <param name="repository">The <see cref="IApplicationTokenRepository" /> instance to be disposed of.</param>
    public virtual Task DeleteRepositoryInstanceAsync(IApplicationTokenRepository repository)
    {
        repository.Dispose();
        repository = null;
        return Task.CompletedTask;
    }
    #endregion
}
