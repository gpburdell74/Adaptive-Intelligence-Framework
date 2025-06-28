using Adaptive.Intelligence.LanguageService.Dictionaries;
using Adaptive.Intelligence.LanguageService.Tokenization;
using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.BlazorBasic.Parser;

/// <summary>
/// Stores and manages a list of user-defined items that may refer to procedures, functions, or variables.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="IUserReferenceTable" />
public sealed class UserReferenceTable : DisposableObjectBase, IUserReferenceTable
{
    #region Private Member Declarations
    /// <summary>
    /// The procedure name references list.
    /// </summary>
    private Dictionary<string, UserReferenceRecord>? _procedures;
    /// <summary>
    /// The function name references list.
    /// </summary>
    private Dictionary<string, UserReferenceRecord>? _functions;
    /// <summary>
    /// The variable name references list.
    /// </summary>
    private Dictionary<string, UserReferenceRecord>? _variables;
    #endregion

    #region Constructor / Dispose Methods    
    /// <summary>
    /// Initializes a new instance of the <see cref="UserReferenceTable"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public UserReferenceTable()
    {
        _procedures = new Dictionary<string, UserReferenceRecord>();
        _functions = new Dictionary<string, UserReferenceRecord>();
        _variables = new Dictionary<string, UserReferenceRecord>();
    }
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        if (!IsDisposed && disposing)
        {
            _procedures?.Clear();
            _functions?.Clear();
            _variables?.Clear();
        }

        _procedures = null;
        _functions = null;
        _variables = null;
        base.Dispose(disposing);
    }
    #endregion

    #region Public Methods / Functions    
    /// <summary>
    /// Adds the user function declaration to the table.
    /// </summary>
    /// <param name="lineNumber">An integer specifying the line number.</param>
    /// <param name="functionName">A string containing the unique name of the function.</param>
    /// <param name="codeLine">An <see cref="ITokenizedCodeLine" /> containing the tokens for the line of code.</param>
    public void AddUserFunctionDeclaration(int lineNumber, string functionName, ITokenizedCodeLine codeLine)
    {
        UserReferenceRecord record = new UserReferenceRecord();
        record.LineNumber = lineNumber;
        record.NormalizedName = ParseUtils.NormalizeAsKey(functionName);
        record.Name = functionName;
        record.CodeLine = codeLine;
        _functions!.Add(functionName, record);
    }

    /// <summary>
    /// Adds the user procedure declaration to the table.
    /// </summary>
    /// <param name="lineNumber">An integer specifying the line number.</param>
    /// <param name="procedureName">A string containing the unique name of the procedure.</param>
    /// <param name="codeLine">An <see cref="ITokenizedCodeLine" /> containing the tokens for the line of code.</param>
    public void AddUserProcedureDeclaration(int lineNumber, string procedureName, ITokenizedCodeLine codeLine)
    {
        UserReferenceRecord record = new UserReferenceRecord();
        record.LineNumber = lineNumber;
        record.NormalizedName = ParseUtils.NormalizeAsKey(procedureName);
        record.Name = procedureName;
        record.CodeLine = codeLine;
        _procedures!.Add(procedureName, record);
    }

    /// <summary>
    /// Adds the user variable declaration to the table.
    /// </summary>
    /// <param name="lineNumber">An integer specifying the line number.</param>
    /// <param name="variableName">A string containing the unique (within scope) name of the variable.</param>
    /// <param name="codeLine">An <see cref="ITokenizedCodeLine" /> containing the tokens for the line of code.</param>
    public void AddUserVariableDeclaration(int lineNumber, string variableName, ITokenizedCodeLine codeLine)
    {
        UserReferenceRecord record = new UserReferenceRecord();
        record.LineNumber = lineNumber;
        // This is different because variables may be named within different procedures and functions.
        record.NormalizedName = lineNumber.ToString("0000000") + ParseUtils.NormalizeAsKey(variableName);
        record.Name = variableName;
        record.CodeLine = codeLine;
        _variables!.Add(record.NormalizedName, record);
    }

    public bool IsFunction(IToken token)
    {
        return _functions.ContainsKey(token.Text.ToUpper().Trim());
    }
    public bool IsProcedure(IToken token)
    {
        return _procedures.ContainsKey(token.Text.ToUpper().Trim());
    }

    public bool IsVariable(int lineNumber, IToken token)
    {
        bool isvar = false;

        foreach(string k in _variables.Keys)
        {
            UserReferenceRecord record = _variables[k];
            if (record.Name.ToUpper().Trim() == token.Text.ToUpper().Trim())
                isvar = true;
        }
        return isvar;
    }

    #endregion
}
