using Adaptive.Intelligence.LanguageService.CodeDom;
using System;

namespace Adaptive.Intelligence.LanguageService.Execution;

/// <summary>
/// Provides the signature definition for a language interpreter execution engine.
/// </summary>
public interface IExecutionEngine : IDisposable 
{
    T? CallFunction<T>(IExecutionEnvironment environment, IScopeContainer scope, string procedureName,
        List<object> parameterValues);
    void CallProcedure(IExecutionEnvironment environment, IScopeContainer scope, string procedureName,
        List<object> parameterValues);

    /// <summary>
    /// Executes the loaded code.
    /// </summary>
    void Execute();

    /// <summary>
    /// Loads the specified code unit and prepares the environment for use.
    /// </summary>
    /// <param name="codeUnit">
    /// The <see cref="IExecutionUnit"/> instance containing the code to execute.
    /// </param>
    void Load(IExecutionUnit codeUnit);
}
