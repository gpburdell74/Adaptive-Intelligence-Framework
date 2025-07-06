namespace Adaptive.Intelligence.LanguageService.Execution;

/// <summary>
/// Provides the signature definition for storing and managing code for procedure definitions.
/// </summary>
/// <seealso cref="IDisposable" />
public interface IProcedureTable : IContainerTable 
{
    /// <summary>
    /// Gets the procedure reference for the procedure with the specified name.
    /// </summary>
    /// <param name="procedureName">
    /// A string containing the name of the procedure.
    /// </param>
    /// <returns>
    /// The reference to the <see cref="IProcedure"/> instance.
    /// </returns>
    IProcedure? GetProcedure(string? procedureName);
}
