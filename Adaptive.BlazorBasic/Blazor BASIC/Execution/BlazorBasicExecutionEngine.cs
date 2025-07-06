using Adaptive.Intelligence.LanguageService.CodeDom;
using Adaptive.Intelligence.LanguageService.Execution;
using Adaptive.Intelligence.Shared;
using System;

namespace Adaptive.Intelligence.BlazorBasic.Execution;

public class BlazorBasicExecutionEngine : DisposableObjectBase, IExecutionEngine
{
    private BlazorBasicExecutionEnvironment _environment;
    private BlazorBasicStatementExecutor _exec;

    private IExecutionUnit _codeUnit;

    public T? CallFunction<T>(IExecutionEnvironment environment, IScopeContainer scope, string procedureName,
        List<object> parameterValues)
    {
        return default(T);
    }

    public void CallProcedure(IExecutionEnvironment environment, IScopeContainer scope, string procedureName,
        List<object> parameterValues)
    {
        BlazorBasicProcedure? proc = (BlazorBasicProcedure)_environment.Procedures.GetProcedureByName(procedureName);
        //proc.SetParameterVariablesInOrder(parameterValues);
        ExecuteProcedure(proc);
    }

    public void Execute()
    {
        _environment.Console.Color(7, 0);
        _environment.Console.PrintLine("Blazor BASIC Version 0.0.1.0");
        _environment.Console.PrintLine("Copyright (c) 2025 Adaptive Intelligence, Inc. All rights reserved.");
        _environment.Console.PrintLine("This is a pre-release version of Blazor BASIC. Use at your own risk.");
        _environment.Console.PrintLine("");


        // Start at MAIN();
        IProcedure ? proc = _environment.MainProc;
        if (proc == null)
        {

        }
        else
        {
            _exec = new BlazorBasicStatementExecutor();
            try
            {
                ExecuteProcedure(proc);
            }
            catch (Exception ex)
            {

            }
        }
    }

    public void Load(IExecutionUnit codeUnit)
    {
        _codeUnit = codeUnit;
        _environment = new BlazorBasicExecutionEnvironment();
        _environment.Initialize(codeUnit);
    }

    private void ExecuteProcedure(IProcedure procedure)
    {
        int length = procedure.Code.Count;
        int index = 0;
        bool done = false;
        do
        {
            done = _exec.ExecuteNextStatement(this, _environment, procedure);
        } while (!done);
    }

}
