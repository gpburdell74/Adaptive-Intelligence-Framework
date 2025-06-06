using Adaptive.Intelligence.SecureApi.Server;
using Adaptive.Intelligence.SecureApi.Sessions;
using Adaptive.SecureApi.Server;

namespace Adaptive.Intelligence.SecureApi.Services;

/// <summary>
/// Provides the session repository service implementation for the secure API server.
/// </summary>
/// <seealso cref="ISessionRepositoryService" />
public class SesssionRepositoryService : ISessionRepositoryService
{
    public Task<ISessionRepository> CreatNewRepositoryInstanceAsync()
    {
        return Task.FromResult((ISessionRepository)new InMemorySessionRepository());
    }

    public Task DeleteRepositoryInstanceAsync(ISessionRepository repository)
    {
        repository.Dispose();
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        
    }
}
