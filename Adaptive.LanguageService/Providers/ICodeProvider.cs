namespace Adaptive.LanguageService.Providers;

/// <summary>
/// Provides the signature definition for implementations that provide static code elements.
/// </summary>
/// <seealso cref="IDisposable" />
public interface ICodeProvider : IDisposable 
{
    /// <summary>
    /// Initializes the content of the provider.
    /// </summary>
    void Initialize();
}
