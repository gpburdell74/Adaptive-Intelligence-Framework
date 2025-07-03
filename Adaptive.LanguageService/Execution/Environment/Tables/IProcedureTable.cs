namespace Adaptive.Intelligence.LanguageService.Execution;

/// <summary>
/// Provides the signature definition for storing and managing code for procedure definitions.
/// </summary>
/// <seealso cref="IDisposable" />
public interface IProcedureTable : IDisposable 
{
    /// <summary>
    /// Gets the number of procedures in the table.
    /// </summary>
    /// <value>
    /// An integer containing the count of procedures.
    /// </value>
    int Count { get; }

    /// <summary>
    /// Gets the procedure reference for the specified ID value.
    /// </summary>
    /// <param name="id">
    /// An integer containing the unique ID value assigned to the procedure instance.</param>
    /// <returns>
    /// The reference to the <see cref="IProcedure"/> instance.
    /// </returns>
    IProcedure? GetProcedure(int id);

    /// <summary>
    /// Gets the procedure reference for the procedure with the specified name.
    /// </summary>
    /// <param name="procedureName">
    /// A string containing the name of the procedure.
    /// </param>
    /// <returns>
    /// The reference to the <see cref="IProcedure"/> instance.
    /// </returns>
    IProcedure? GetProcedureByName(string procedureName);
}
