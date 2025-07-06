namespace Adaptive.Intelligence.LanguageService.CodeDom;

/// <summary>
/// Provides the signature definition for implementations that contain the entirety of parsed code
/// and the resulting list of <see cref="ILanguageCodeStatement"/> instances.
/// </summary>
/// <seealso cref="ICodeObject" />
public interface IExecutionUnit : ICodeObject
{
    /// <summary>
    /// Gets the reference to the list of statements to be executed.
    /// </summary>
    /// <value>
    /// An <see cref="ILanguageCodeStatementsTable"/> instance containing the list of code 
    /// items to execute.
    /// </value>
    ILanguageCodeStatementsTable? Statements { get; }
}
