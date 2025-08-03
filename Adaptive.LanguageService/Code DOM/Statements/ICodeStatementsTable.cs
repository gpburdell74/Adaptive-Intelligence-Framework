namespace Adaptive.Intelligence.LanguageService.CodeDom.Statements;

/// <summary>
/// Provides the signature definition for objects that contain and manage a list of Code-DOM
/// statements.
/// </summary>
/// <seealso cref="ICodeObject" />
public interface ICodeStatementsTable : ICodeObject 
{
    /// <summary>
    /// Gets the next ordinal index of an instance of the specified type.
    /// </summary>
    /// <param name="startIndex">The start index.</param>
    /// <param name="statementType">Type of the statement.</param>
    /// <returns></returns>
    int IndexOf(int startIndex, Type statementType);

    /// <summary>
    /// Finds the next end if.
    /// </summary>
    /// <param name="startIndex">
    /// AN integer specifying the ordinal index at which to start searching.
    /// </param>
    /// <returns>
    /// The ordinal index of the next end-if statement, or -1 if not found.
    /// </returns>
    int FindEndIf(int startIndex);

}
