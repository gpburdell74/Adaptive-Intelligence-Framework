namespace Adaptive.BlazorBasic.LanguageService;

/// <summary>
/// Provides the signature definition for tokens representing user-defined token content.
/// </summary>
/// <remarks>
/// These tokens are used to mark elements of code that do not match known built-in items or types,
/// and which are most likely references to functions, procedures, variables, or parameters.
/// </remarks>
/// <seealso cref="IToken" />
public interface IUserDefinedToken : IToken
{
}
