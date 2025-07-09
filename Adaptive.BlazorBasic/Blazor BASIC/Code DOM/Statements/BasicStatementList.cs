using Adaptive.Intelligence.BlazorBasic.CodeDom.Statements;
using Adaptive.Intelligence.LanguageService;
using Adaptive.Intelligence.LanguageService.CodeDom.Statements;
using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.BlazorBasic.CodeDom;

/// <summary>
/// Manages and contains a list of <see cref="BasicCodeStatement"/> instances for execution.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="ICodeStatement" />
public class BasicCodeStatementList : List<ICodeStatement>
{
    /// <summary>
    /// Gets the next ordinal index of an instance of the specified type.
    /// </summary>
    /// <param name="startIndex">The start index.</param>
    /// <param name="statementType">Type of the statement.</param>
    /// <returns></returns>
    public int IndexOf(int startIndex, Type statementType)
    {
        int returnIndex = -1;
        int pos = startIndex;
        int length =Count ;
        do
        {
            if (this[pos].GetType() ==  statementType)
            {   
                returnIndex = pos;
            }
            pos++;
        } while (pos < length && returnIndex == -1);

        return returnIndex;
    }
    public int FindEndIf(int startIndex)
    {
        int returnIndex = -1;
        int pos = startIndex;
        int length = Count;
        do
        {
            BasicCodeStatement statement = (BasicCodeStatement)this[pos];
            if (statement is BasicEndStatement endStatement)
            {
                if (endStatement.BlockType == CodeBlockType.If)
                    returnIndex = pos;
            }
            pos++;
        } while (pos < length && returnIndex == -1);

        return returnIndex;
    }
}
