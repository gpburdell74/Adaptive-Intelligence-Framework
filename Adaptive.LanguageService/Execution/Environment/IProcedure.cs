namespace Adaptive.Intelligence.LanguageService.Execution;

/// <summary>
/// Provides the signature definition for procedure definitions and instances.
/// </summary>
/// <seealso cref="IScopeContainer" />
public interface IProcedure : IScopeContainer, IScopedElement, IExecutableContext
{
}
