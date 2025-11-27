using Adaptive.Intelligence.LanguageService.CodeDom;
using System;

namespace Adaptive.Intelligence.LanguageService.Execution;

/// <summary>
/// Provides the signature definition for the global environment container in which the 
/// language code is executed.
/// </summary>
/// <seealso cref="IDisposable" />
public interface IExecutionEnvironment : IScopeContainer
{
    #region Properties
    /// <summary>
    /// Gets the reference to the table containing the defined function instances.
    /// </summary>
    /// <value>
    /// An <see cref="IFunctionTable"/> instance containing the function instances.
    /// </value>
    IFunctionTable? Functions { get; }

    /// <summary>
    /// Gets the reference to the list of global variables.
    /// </summary>
    /// <value>
    /// An <see cref="IVariableTable"/> instance containing the global variables.
    /// </value>
    IVariableTable? GlobalVaraibles { get; }

    /// <summary>
    /// Gets the reference to the main procedure.
    /// </summary>
    /// <value>
    /// The <see cref="IProcedure"/> instance representing the main procedure.
    /// </value>
    IProcedure? MainProcedure { get; }

    /// <summary>
    /// Gets the reference to the table containing the defined procedure instances.
    /// </summary>
    /// <value>
    /// An <see cref="IProcedureTable"/> instance containing the procedure instances.
    /// </value>
    IProcedureTable? Procedures { get; }

    /// <summary>
    /// Gets the reference to the standard output device or mechanism.
    /// </summary>
    /// <value>
    /// The <see cref="IStandardOutput"/> instance used for output operations.
    /// </value>
    IStandardOutput? StandardOutput { get; }

    /// <summary>
    /// Gets the reference to the system API implementation.
    /// </summary>
    /// <value>
    /// The <see cref="ISystem"/> instance providing access to system-level operations."/>
    /// </value>
    ISystem? System { get; }
    #endregion

    #region Methods    
    /// <summary>
    /// Closes the file.
    /// </summary>
    /// <param name="lineNumber">The line number.</param>
    /// <param name="fileHandle">The file handle.</param>
    void CloseFile(int lineNumber, int fileHandle);

    /// <summary>
    /// Gets the file stream reference.
    /// </summary>
    /// <param name="lineNumber">
    /// An integer specifying the current line number.
    /// </param>
    /// <param name="fileHandle">
    /// An integer specifying the file handle.
    /// </param>
    /// <returns>
    /// The <see cref="FileStream"/> instance, or <b>null</b> if not found.
    /// </returns>
    FileStream? GetFileStream(int lineNumber, int fileHandle);

    /// <summary>
    /// Determines whether the specified file is opened for binary (rather than text).
    /// </summary>
    /// <param name="lineNumber">
    /// An integer specifying the current line number.
    /// </param>
    /// <param name="fileHandle">
    /// An integer specifying the file handle.
    /// </param>
    /// <returns>
    ///   <c>true</c> if the specified file is opened for binary; otherwise, <c>false</c>.
    /// </returns>
    bool IsBinaryFile(int lineNumber, int fileHandle);

    /// <summary>
    /// Loads the source into memory as an interpreter unit and prepares the environment for execution.
    /// </summary>
    /// <param name="interpreterUnit">
    /// The <see cref="ICodeInterpreterUnit"/> instance containing the loaded source code.
    /// </param>
    void LoadUnit(ICodeInterpreterUnit interpreterUnit);

    /// <summary>
    /// Registers the file handle with the specified stream instance.
    /// </summary>
    /// <param name="lineNumber">
    /// An integer indicating the current line number in use.
    /// </param>
    /// <param name="fileHandle">
    /// An integer containing the file handle value.
    /// </param>
    /// <param name="stream">
    /// The <see cref="FileStream"/> instance that was opened.
    /// </param>
    void RegisterFileHandle(int lineNumber, int fileHandle, FileStream stream);

    /// <summary>
    /// Unloads the <see cref="ICodeInterpreterUnit"/> loaded into memory and clears all 
    /// environmental variables, lists, and closes all files.
    /// </summary>
    void UnloadUnit();
    #endregion
}
