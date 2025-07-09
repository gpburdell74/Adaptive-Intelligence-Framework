using Adaptive.Intelligence.BlazorBasic.CodeDom;
using Adaptive.Intelligence.BlazorBasic.CodeDom.Expressions;
using Adaptive.Intelligence.BlazorBasic.CodeDom.Statements;
using Adaptive.Intelligence.BlazorBasic.Execution;
using Adaptive.Intelligence.LanguageService.CodeDom;
using Adaptive.Intelligence.LanguageService.CodeDom.Statements;
using Adaptive.Intelligence.LanguageService.Execution;
using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.Shared.IO;
using System;

namespace Adaptive.Intelligence.BlazorBasic;

public class BlazorBasicStatementExecutor : DisposableObjectBase
{
    private int _statementIndex = 0;

    public bool ExecuteNextStatement(BlazorBasicExecutionEngine engine,
        BlazorBasicExecutionEnvironment environment,
        IScopeContainer scopeContainer)
    {
        ExecuteStatement(engine, environment, scopeContainer, scopeContainer.Code[_statementIndex]);
        return _statementIndex >= scopeContainer.Code.Count;

    }

    public void ExecuteStatement(
        BlazorBasicExecutionEngine engine,
        BlazorBasicExecutionEnvironment environment,
        IScopeContainer scopeContainer,
        ICodeStatement statement)
    {
        switch (statement)
        {
            case BasicClsStatement clsStatement:
                ExecuteCls(engine, environment, clsStatement);
                _statementIndex++;
                break;

            case BasicCloseStatement closeStatement:
                ExecuteCloseStatement(engine, environment, scopeContainer, closeStatement);
                _statementIndex++;
                break;

            case BasicOpenStatement openStatement:
                ExecuteFileOpen(engine, environment, openStatement);
                _statementIndex++;
                break;

            case BasicVariableDeclarationStatement dimStatement:
                DeclareVariableInScope(engine, environment, scopeContainer, dimStatement);
                _statementIndex++;
                break;

            case BasicProcedureStartStatement procStatement:
                CreateParametersAsVariablesInScope(engine, environment, scopeContainer, procStatement);
                _statementIndex++;
                break;

            case BasicVariableAssignmentStatement assignStatement:
                AssignValueToVariable(engine, environment, scopeContainer, assignStatement);
                _statementIndex++;
                break;

            case BasicDoStatement doStatement:
                ExecuteDoLoop(engine, environment, scopeContainer, doStatement);
                break;

            case BasicInputStatement inputStatement:
                PerformInput(engine, environment, scopeContainer, inputStatement);
                _statementIndex++;
                break;

            case BasicIfStatement ifStatement:
                ExecuteIfElseBlock(engine, environment, scopeContainer, ifStatement);
                break;

            case BasicWriteStatement writeStatement:
                ExecuteWriteStatement(engine, environment, scopeContainer, writeStatement);
                _statementIndex++;
                break;

            case BasicProcedureCallStatement procCallStatement:
                ExecuteProcedureCall(engine, environment, scopeContainer, procCallStatement);
                break;

            case BasicCommentStatement:
            case BlazorBasicEmptyStatement:
            case BasicEndStatement:
            case BasicLoopStatement:
            case BasicProcedureEndStatement:
            case BasicFunctionEndStatement:
                _statementIndex++;
                break;

            default:
                throw new NotSupportedException($"Statement type {statement.GetType().Name} is not supported yet.");
        }
    }

    public void ExecuteCls(BlazorBasicExecutionEngine engine, BlazorBasicExecutionEnvironment environment,
        BasicClsStatement statement)
    {
        environment.Console.Cls();

    }
    public void ExecuteFileOpen(BlazorBasicExecutionEngine engine, BlazorBasicExecutionEnvironment environment,
        BasicOpenStatement statement)
    {
        int handle = statement.FileNumber.FileNumber;

        // Open the file.
        environment.OpenFile(statement.LineNumber, handle, statement.FileName.FileName,
            statement.FileDirection.Mode, statement.FileDirection.Access);
    }

    public void DeclareVariableInScope(
        BlazorBasicExecutionEngine engine,
        BlazorBasicExecutionEnvironment environment,
        IScopeContainer scopeContainer,
        BasicVariableDeclarationStatement statement)
    {
        string name = statement.VariableReference.VariableName;

        // Check if the variable is already defined.
        if (scopeContainer.VariableExists(name))
            throw new BasicDuplicateDefinitionException(statement.LineNumber, name);

        try
        {
            scopeContainer.CreateVariable(
                statement.LineNumber,
                name,
                statement.DataType.DataType,
                statement.DataType.IsArray,
                statement.DataType.Size);
        }
        catch
        {
            throw new BasicBadDataTypeException(statement.LineNumber, statement.OriginalCode);
        }

    }
    public void CreateParametersAsVariablesInScope(
         BlazorBasicExecutionEngine engine,
        BlazorBasicExecutionEnvironment environment,
        IScopeContainer scopeContainer,
        BasicProcedureStartStatement statement)
    {

        if (statement.Parameters != null && statement.Parameters.ParameterList != null)
        {
            foreach (var paramExpression in statement.Parameters.ParameterList)
            {
                scopeContainer.CreateParameterVariable(statement.LineNumber,
                    paramExpression.ParameterName, paramExpression.DataType.DataType,
                    paramExpression.DataType.IsArray, paramExpression.DataType.Size);

            }
        }
    }

    public void AssignValueToVariable(BlazorBasicExecutionEngine engine,
        BlazorBasicExecutionEnvironment environment,
        IScopeContainer scopeContainer,
        BasicVariableAssignmentStatement assignStatement)
    {
        string variableToAssign = assignStatement.VariableReference.VariableName;
        if (!scopeContainer.VariableExists(variableToAssign))
            throw new BasicVariableNotFoundException(assignStatement.LineNumber, variableToAssign);

        IVariable variable = scopeContainer.GetVariable(variableToAssign);

        try
        {
            variable.SetValue(PerformEval(engine, environment, scopeContainer, assignStatement.Expression));
        }
        catch (Exception ex)
        {
            throw new BasicTypeMismatchException(assignStatement.LineNumber);

        }
    }

    public bool PerformEvalAsBoolean(BlazorBasicExecutionEngine engine,
        BlazorBasicExecutionEnvironment environment,
        IScopeContainer scopeContainer,
        BasicExpression expression)
    {
        object result = PerformEval(engine, environment, scopeContainer, expression);
        if (result is bool boolResult)
            return boolResult;
        else
            throw new BasicTypeMismatchException(0);
    }

    public object PerformEval(BlazorBasicExecutionEngine engine,
        BlazorBasicExecutionEnvironment environment,
        IScopeContainer scopeContainer,
        BasicExpression expression)
    {
        object data = null;

        switch (expression)
        {
            case BlazorBasicCodeConditionalExpression booleanConditional:
                data = booleanConditional.Evaluate(engine, environment, scopeContainer);
                break;

            case BasicLiteralStringExpression literalString:
                data = (string)literalString.Evaluate(engine, environment, scopeContainer);
                break;

            case BlazorBasicLiteralCharacterExpression literalChar:
                data = literalChar.Value;
                break;

            case BasicLiteralIntegerExpression literalInt32:
                data = literalInt32.Value;
                break;

            case BlazorBasicLiteralFloatingPointExpression literalFloat:
                data = literalFloat.Value;
                break;

            case BasicVariableReferenceExpression variableReference:
                data = variableReference.VariableName;
                break;

            case BlazorBasicVariableNameExpression variableName:
                data = scopeContainer.GetVariable(variableName.VariableName).GetValue();
                break;

            case BlazorBasicBasicArithmeticExpression mathExpression:
                data = mathExpression.Evaluate(engine, environment, scopeContainer);
                break;

            default:
                throw new NotSupportedException($"Expression type {expression.GetType().Name} is not supported yet.");
        }
        return data;
    }

    private void ExecuteDoLoop(BlazorBasicExecutionEngine engine,
        BlazorBasicExecutionEnvironment environment,
        IScopeContainer scopeContainer,
        BasicDoStatement statement)
    {
        int loopIndex = ((BasicCodeStatementList)scopeContainer.Code).IndexOf(_statementIndex, typeof(BasicLoopStatement));
        bool done = false;
        _statementIndex++;
        int startIndex = _statementIndex;
        do
        {
            BasicCodeStatement nextStatement = (BasicCodeStatement)scopeContainer.Code[_statementIndex];
            if (_statementIndex < loopIndex)
            {
                ExecuteStatement(engine, environment, scopeContainer, nextStatement);
            }
            
            if (_statementIndex == loopIndex)
            {
                BasicLoopStatement loopStatement = ((BasicLoopStatement)scopeContainer.Code[_statementIndex]);
                object result = PerformEval(engine, environment, scopeContainer, loopStatement.ConditionExpression);
                if (result is bool condition)
                {
                    done = condition;
                    if (done)
                        _statementIndex++;
                    else
                        _statementIndex = startIndex;
                }

            }
        } while (_statementIndex <= loopIndex && !done);

        _statementIndex = loopIndex + 1;

    }

    private void PerformInput(BlazorBasicExecutionEngine engine,
        BlazorBasicExecutionEnvironment environment,
        IScopeContainer scopeContainer,
        BasicInputStatement inputStatement)
    {
        if (inputStatement.PromptExpression != null)
        {
            object data = PerformEval(engine, environment, scopeContainer, (BasicExpression)inputStatement.PromptExpression);
            if (data is string stringData)
            {
                Console.Write(data);
            }

            string userData = Console.ReadLine();
            if (inputStatement.VariableReferenceExpression != null)
            {
                string variableName = inputStatement.VariableReferenceExpression.VariableName;
                if (!scopeContainer.VariableExists(variableName))
                    throw new BasicVariableNotFoundException(inputStatement.LineNumber, variableName);
                IVariable variable = scopeContainer.GetVariable(variableName);
                try
                {
                    variable.SetValue(userData);
                }
                catch (Exception ex)
                {
                    throw new BasicTypeMismatchException(inputStatement.LineNumber);
                }
            }
        }
    }

    private void ExecuteIfElseBlock(BlazorBasicExecutionEngine engine,
        BlazorBasicExecutionEnvironment environment,
        IScopeContainer scopeContainer,
        BasicIfStatement ifStatement)
    {
        int currentIndex = scopeContainer.Code.IndexOf(ifStatement);
        int endIfIndex = ((BasicCodeStatementList)scopeContainer.Code).FindEndIf(currentIndex);
        bool done = false;

        bool conditionIsTrue = PerformEvalAsBoolean(engine, environment, scopeContainer, ifStatement.ConditionalExpression);
        if (conditionIsTrue)
        {
            for (int startIndex = currentIndex + 1; startIndex < endIfIndex; startIndex++)
            {
                ExecuteStatement(engine, environment, scopeContainer, scopeContainer.Code[startIndex]);
            }
        }
        else
        {

        }
        _statementIndex = endIfIndex + 1;
    }
    private void ExecuteWriteStatement(BlazorBasicExecutionEngine engine,
        BlazorBasicExecutionEnvironment environment,
        IScopeContainer scopeContainer,
        BasicWriteStatement writeStatement)
    {
        FileStream fs = environment.GetFileStream(writeStatement.FileNumber.FileNumber);
        bool isBinary = environment.IsBinaryFile(writeStatement.FileNumber.FileNumber);

        SafeBinaryWriter binWriter = null;
        StreamWriter streamWriter = null;

        if (isBinary)
            binWriter = new SafeBinaryWriter(fs);
        else
            streamWriter = new StreamWriter(fs);

        foreach (BasicExpression expression in writeStatement.Expressions)
        {
            object data = PerformEval(engine, environment, scopeContainer, expression);
            if (isBinary)
            {
//                binWriter.Write(data);
            }
            else
            {
                streamWriter.Write(data);
            }
        }
        if (isBinary)
            binWriter.Flush();
        else
            streamWriter.Flush();
        fs.Flush();
        fs = null;
    }
    private void ExecuteCloseStatement(BlazorBasicExecutionEngine engine,
        BlazorBasicExecutionEnvironment environment,
        IScopeContainer scopeContainer,
        BasicCloseStatement closeStatement)
    {
        environment.CloseFile(0, closeStatement.FileNumberExpression.FileNumber);
    }

    private void ExecuteProcedureCall(BlazorBasicExecutionEngine engine,
        BlazorBasicExecutionEnvironment environment,
        IScopeContainer scopeContainer,
        BasicProcedureCallStatement procCallStatement)
    {
        
        List<object> paramValueList = new List<object>();
            
        foreach(BlazorBasicParameterValueExpression exp in procCallStatement.Parameters)
        {
            object a = exp.Evaluate(engine, environment, scopeContainer);
            paramValueList.Add(a);
        }
        engine.CallProcedure(environment, scopeContainer, procCallStatement.ProcedureName,
            paramValueList);
    }
}
