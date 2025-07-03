namespace Adaptive.Intelligence.LanguageService.Execution;

/// <summary>
/// Provides the signature definition for the global environment container in which the 
/// language code is executed.
/// </summary>
/// <seealso cref="IDisposable" />
public interface IExecutionEnvironment : IDisposable 
{
    IIdGenerator IdGenerator { get; }

    #region Functions
    /// <summary>
    /// Gets the reference to the table containing the defined function instances.
    /// </summary>
    /// <value>
    /// An <see cref="IFunctionTable"/> instance containing the function instances.
    /// </value>
    IFunctionTable? Functions { get; }
    #endregion

    #region Procedures
    /// <summary>
    /// Gets the reference to the table containing the defined procedure instances.
    /// </summary>
    /// <value>
    /// An <see cref="IProcedureTable"/> instance containing the procedure instances.
    /// </value>
    IProcedureTable? Procedures { get; }
    #endregion

    #region Memory
    #endregion

    #region File System
    #endregion
}
