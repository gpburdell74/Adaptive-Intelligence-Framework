using Adaptive.Intelligence.LanguageService.Tokenization;

namespace Adaptive.Intelligence.LanguageService.Dictionaries;

/// <summary>
/// Provides the signature definition for storing and referencing user-defined elements, such as procedures, functions, and variables.
/// </summary>
/// <seealso cref="IDisposable" />
public interface IUserReferenceTable : IDisposable
{
    /// <summary>
    /// Adds the user function declaration to the table.
    /// </summary>
    /// <param name="lineNumber">
    /// An integer specifying the line number.
    /// </param>
    /// <param name="functionName">
    /// A string containing the unique name of the function.
    /// </param>
    /// <param name="codeLine">
    /// An <see cref="ITokenizedCodeLine"/> containing the tokens for the line of code.
    /// </param>
    void AddUserFunctionDeclaration(int lineNumber, string functionName, ITokenizedCodeLine codeLine);

    /// <summary>
    /// Adds the user procedure declaration to the table.
    /// </summary>
    /// <param name="lineNumber">
    /// An integer specifying the line number.
    /// </param>
    /// <param name="procedureName">
    /// A string containing the unique name of the procedure.
    /// </param>
    /// <param name="codeLine">
    /// An <see cref="ITokenizedCodeLine"/> containing the tokens for the line of code.
    /// </param>
    void AddUserProcedureDeclaration(int lineNumber, string procedureName, ITokenizedCodeLine codeLine);

    /// <summary>
    /// Adds the user variable declaration to the table.
    /// </summary>
    /// <param name="lineNumber">
    /// An integer specifying the line number.
    /// </param>
    /// <param name="variableName">
    /// A string containing the unique (within scope) name of the variable.
    /// </param>
    /// <param name="codeLine">
    /// An <see cref="ITokenizedCodeLine"/> containing the tokens for the line of code.
    /// </param>
    void AddUserVariableDeclaration(int lineNumber, string variableName, ITokenizedCodeLine codeLine);
}
