using Adaptive.Intelligence.BlazorBasic.CodeDom;
using Adaptive.Intelligence.LanguageService.CodeDom;
using Adaptive.Intelligence.LanguageService.CodeDom.Statements;
using Adaptive.Intelligence.LanguageService.Execution;
using Adaptive.Intelligence.Shared;
using System;
using System.Security;

namespace Adaptive.Intelligence.BlazorBasic.Execution;

public class BlazorBasicExecutionEnvironment : DisposableObjectBase, IExecutionEnvironment
{
    private BLazorBasicIdGenerator _idGenerator;
    private BlazorBasicFunctionTable _functions;
    private BlazorBasicProcedureTable _procedures;
    private BlazorBasicProcedure? _mainProc;
    private BlazorBasicConsole? _console;

    private List<int>? _fileHandles;
    private Dictionary<int, FileStream> _files;

    public BlazorBasicExecutionEnvironment()
    {
        _idGenerator = new BLazorBasicIdGenerator();
        _functions = new BlazorBasicFunctionTable();
        _procedures = new BlazorBasicProcedureTable();
        _console = new BlazorBasicConsole();
        _fileHandles = new List<int>();
        _files = new Dictionary<int, FileStream>();
    }
    protected override void Dispose(bool disposing)
    {
        if (!IsDisposed && disposing)
        {
            _idGenerator?.Dispose();
            _functions?.Dispose();
            _procedures?.Dispose();
            _console?.Dispose();
        }

        _console = null;
        _functions = null;
        _procedures = null;
        _idGenerator = null;
        base.Dispose(disposing);
    }

    public BlazorBasicConsole? Console => _console;

    public IIdGenerator IdGenerator => _idGenerator;
    public IProcedure MainProc => _mainProc;

    public IFunctionTable? Functions => _functions;
    public IProcedureTable? Procedures => _procedures;

    public IVariableTable? GlobalVaraibles { get; }
    public IProcedure? MainProcedure { get; }
    public IStandardOutput? StandardOutput { get; }
    public ISystem System { get; }

    public void CloseFile(int lineNumber, int fileHandle)
    {
        if (!_fileHandles.Contains(fileHandle))
            throw new BasicIOException(lineNumber, "Invalid file number.");
        else
        {
            FileStream fs = _files[fileHandle];
            _files.Remove(fileHandle);
            _fileHandles.Remove(fileHandle);

            fs.Close();
            fs.Dispose();
        }
    }
    public int OpenFile(int lineNumber, int requestedHandle, string fileName, FileMode mode, FileAccess access)
    {
        if (_fileHandles.Contains(requestedHandle))
            throw new BasicHandleInUseException(lineNumber, $"File handle {requestedHandle} is already allocated.");

        FileStream? fs = null;
        try
        {
            fs = new FileStream(fileName, mode, access);
            fs.Seek(0, SeekOrigin.Begin);
            _fileHandles.Add(requestedHandle);
            _files.Add(requestedHandle, fs);
        }
        catch(FileNotFoundException fileNotFoundEx)
        {
            throw new BasicFileNotFoundException(lineNumber, fileNotFoundEx.Message);

        }
        catch(ArgumentException argumentEx)
        {
            throw new BasicInvalidArgumentException(lineNumber, argumentEx.Message);
        }
        catch (PathTooLongException pathTooLongEx)
        {
            throw new BasicInvalidArgumentException(lineNumber, pathTooLongEx.Message);
        }
        catch (IOException ioEx)
        {
            throw new BasicIOException(lineNumber, ioEx.Message);
        }
        catch (NotSupportedException noSupportEx)
        {
            throw new BasicNotSupportedException(lineNumber, noSupportEx.Message);
        }
        catch (SecurityException securityEx)
        {
            throw new BasicSecurityException(lineNumber, securityEx.Message);
        }
        catch (UnauthorizedAccessException unAuthorized)
        {
            throw new BasicSecurityException(lineNumber, unAuthorized.Message);
        }
        return requestedHandle;
    }

    public void Initialize(ICodeInterpreterUnit codeUnit)
    {
        CreateProceduresAndFunctions(codeUnit);
    }


    private void CreateProceduresAndFunctions(ICodeInterpreterUnit codeUnit)
    {
        BlazorBasicCodeStatementsTable statements = (BlazorBasicCodeStatementsTable) codeUnit.Statements;
        int length = statements.Count;
        int index = 0;

        do
        {
            ICodeStatement statement = statements[index];

            if (statement is BasicProcedureStartStatement procStart)
            {
                index = AddProcedure(statements, index);
            }
            else if (statement is BasicFunctionStartStatement functionStart)
            {
                index = AddFunction(statements, index);
            }
            else
                index++;
        } while (index < length);
    }
    private int AddProcedure(BlazorBasicCodeStatementsTable statements, int currentIndex)
    {
        int returnIndex = -1;
        int length = statements.Count;
        int index = currentIndex;

        List<ICodeStatement> procCodeList = new List<ICodeStatement>();
        do
        {
            ICodeStatement statement = statements[index];
            procCodeList.Add(statement);
            
            if (statement is BasicProcedureEndStatement endStatement)
            {
                returnIndex = index + 1;
            }
            index++;
        } while (index < length && returnIndex == -1);

        BlazorBasicProcedure newProcedure = new BlazorBasicProcedure(
            _idGenerator.Next(),
            procCodeList);

        _procedures.Add(newProcedure);
        if (newProcedure.Name.Trim().ToLower() == "main")
        {
            _mainProc = newProcedure;
        }
        return index;

    }

    private int AddFunction(BlazorBasicCodeStatementsTable statements, int currentIndex)
    {
        int returnIndex = -1;
        int length = statements.Count;
        int index = currentIndex;

        List<ICodeStatement> procCodeList = new List<ICodeStatement>();
        do
        {
            ICodeStatement statement = statements[index];
            procCodeList.Add(statement);

            if (statement is BasicFunctionEndStatement endStatement)
            {
                returnIndex = index + 1;
            }
            index++;
        } while (index < length && returnIndex == -1);

        BlazorBasicFunction newFunction = new BlazorBasicFunction(
            _idGenerator.Next(),
            procCodeList);

        _functions.Add(newFunction);

        return index;
    }

    public FileStream GetFileStream(int fileNumber)
    {
        if (!_files.ContainsKey(fileNumber))
            throw new BasicIOException(0, "Invalid file number.");

        return _files[fileNumber];
    }
    public bool IsBinaryFile(int fileNumber)
    {
        if (!_files.ContainsKey(fileNumber))
            throw new BasicIOException(0, "Invalid file number.");

        FileStream fs = _files[fileNumber];

        return false;
    }

    public T? CallFunction<T>(int currentLineNumber, IScopeContainer? scope, string functionName, List<object> parameterValues)
    {
        throw new NotImplementedException();
    }

    public void CallProcedure(int currentLineNumber, IScopeContainer? scope, string procedureName, List<object> parameterValues)
    {
        throw new NotImplementedException();
    }

    public void Execute()
    {
        throw new NotImplementedException();
    }

    public void LoadUnit(ICodeInterpreterUnit interpreterUnit)
    {
        throw new NotImplementedException();
    }

    public void UnloadUnit()
    {
        throw new NotImplementedException();
    }
}

