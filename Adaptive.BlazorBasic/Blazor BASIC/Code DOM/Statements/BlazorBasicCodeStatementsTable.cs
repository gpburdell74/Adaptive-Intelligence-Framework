using Adaptive.Intelligence.BlazorBasic.CodeDom;
using Adaptive.Intelligence.BlazorBasic.CodeDom.Statements;
using Adaptive.Intelligence.LanguageService.CodeDom.Statements;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Provides the methods and functions for containing and querying a list of <see cref="BasicCodeStatement"/> instances.
/// </summary>
/// <seealso cref="List{T}" />
/// <see cref="BasicCodeStatement"/>
public class BlazorBasicCodeStatementsTable : List<BasicCodeStatement>, ICodeStatementsTable
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicCodeStatementsTable"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public BlazorBasicCodeStatementsTable() : base()
    {

    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicCodeStatementsTable"/> class.
    /// </summary>
    /// <param name="capacity">The number of elements that the new list can initially store.</param>
    public BlazorBasicCodeStatementsTable(int capacity) : base(capacity)
    {

    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicCodeStatementsTable"/> class.
    /// </summary>
    /// <param name="statementList">The statement list.</param>
    public BlazorBasicCodeStatementsTable(IEnumerable<BasicCodeStatement> statementList)
    {
        AddRange(statementList);
    }
    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        Clear();
    }
    #endregion

    #region Public Methods / Functions
    /// <summary>
    /// Adds the specified statement.
    /// </summary>
    /// <param name="statement">
    /// The <see cref="ICodeStatement"/> instance to be added to the list.
    /// </param>
    public void Add(ICodeStatement statement)
    {
        Add((BasicCodeStatement)statement);
    }
    /// <summary>
    /// Adds the specified statement.
    /// </summary>
    /// <param name="statement">
    /// The <see cref="ICodeStatement"/> instance to be added to the list.
    /// </param>
    public void Add(BasicCodeStatement statement)
    {
        base.Add(statement);
    }

    /// <summary>
    /// Finds the next end if.
    /// </summary>
    /// <param name="startIndex">AN integer specifying the ordinal index at which to start searching.</param>
    /// <returns>
    /// The ordinal index of the next end-if statement, or -1 if not found.
    /// </returns>
    public int FindEndIf(int startIndex)
    {
        int returnIndex = -1;
        int index = startIndex;
        int length = Count;

        while (index < length && returnIndex == -1)
        {
            if (this[index] is BasicEndStatement endStatement)
            {
                if (endStatement.BlockType == Intelligence.LanguageService.CodeBlockType.If)
                    returnIndex = index;
            }
            index++;
        }

        return returnIndex;
    }

    /// <summary>
    /// Finds the procedure definition.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <returns></returns>
    public BasicProcedureStartStatement? FindProcedureDefinition(string name)
    {
        BasicProcedureStartStatement? definition = null;

        foreach (BasicCodeStatement codeStatement in this)
        {
            if (codeStatement is BasicProcedureStartStatement procStartStatement)
            {
                if (procStartStatement.ProcedureName == name)
                    definition = procStartStatement;
            }
        }
        return definition;
    }

    /// <summary>
    /// Gets the next ordinal index of an instance of the specified type.
    /// </summary>
    /// <param name="startIndex">The start index.</param>
    /// <param name="statementType">Type of the statement.</param>
    /// <returns></returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public int IndexOf(int startIndex, Type statementType)
    {
        int returnIndex = -1;
        int index = startIndex;
        int length = Count;

        while (index < length && returnIndex == -1)
        {
            if (this[index].GetType() == statementType)
            {
                returnIndex = index;
            }
            index++;
        }

        return returnIndex;
    }
    #endregion
}
