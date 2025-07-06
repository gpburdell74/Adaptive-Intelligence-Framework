using System.Text;

namespace Adaptive.Intelligence.LanguageService.Tokenization;

/// <summary>
/// Provides additional methods and functions for managing and querying lists of <see cref="IToken"/>
/// instances.
/// </summary>
/// <seealso cref="List{T}" />
/// <seealso cref="IToken"/>
public class ManagedTokenList : List<IToken>
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="ManagedTokenList"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public ManagedTokenList()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ManagedTokenList"/> class.
    /// </summary>
    /// <param name="capacity">
    /// An integer specifying the number of elements that the new list can initially store.
    /// </param>
    public ManagedTokenList(int capacity) : base(capacity)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ManagedTokenList"/> class.
    /// </summary>
    /// <param name="tokenList">
    /// An <see cref="IEnumerable{T}"/> list of <see cref="IToken"/> instances.
    /// </param>
    public ManagedTokenList(IEnumerable<IToken>? tokenList)
    {
        if (tokenList != null)
            AddRange(tokenList);
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="ManagedTokenList"/> class.
    /// </summary>
    /// <param name="tokenList">
    /// An <see cref="IEnumerable{T}"/> list of <see cref="IToken"/> instances.
    /// </param>
    /// <param name="trim">
    /// A value indicating whether to trim the list of tokens by removing any leading or trailing whitespace
    /// tokens.
    /// </param>
    public ManagedTokenList(IEnumerable<IToken>? tokenList, bool trim)
    {
        if (tokenList != null && tokenList.Count() > 0)
        {
            if (!trim)
                AddRange(tokenList);
            else
            {
                List<IToken> list = tokenList.ToList();
                CopyFromSourceAndTrim(list);
                list.Clear();
            }
        }
    }
    #endregion

    #region Public Methods / Functions
    /// <summary>
    /// Copies the content from the source list, removes the leading and trailing whitespace tokens,
    /// and adds the remainder to the current list.
    /// </summary>
    /// <param name="sourceList">
    /// The source <see cref="List{T}"/> of <see cref="IToken"/> to copy the content from.
    /// </param>
    public void CopyFromSourceAndTrim(List<IToken> sourceList)
    {
        Clear();

        bool start = false;
        int index = 0;
        int length = sourceList.Count;

        do
        {
            IToken token = sourceList[index];
            if (token.TokenType != TokenType.SeparatorDelimiter)
            {
                start = true;
            }
            if (start)
                Add(token);
            index++;
        } while (index < length);

        if (Count > 0)
        {
            int lastIndex = Count - 1;
            while (this[lastIndex].TokenType == TokenType.SeparatorDelimiter)
            {
                index = lastIndex;
                RemoveAt(index);
            }
        }
    }

    /// <summary>
    /// Creates a copy of the current list from the original, starting at the specified index.
    /// </summary>
    /// <param name="startIndex">
    /// An integer specifying the start index.  The optional value defaults to zero (0).
    /// </param>
    /// <returns>
    /// A new <see cref="ManagedTokenList"/> containing the references tot he instance in this
    /// list, starting from the specified index.
    /// </returns>
    public ManagedTokenList CreateCopy(int startIndex = 0)
    {
        ManagedTokenList newList = new ManagedTokenList(Count - startIndex);

        for (int index = startIndex; index < Count; index++)
            newList.Add(this[index]);

        return newList;
    }

    /// <summary>
    /// Creates a copy of the current list from the original, starting at the specified index.
    /// </summary>
    /// <param name="startIndex">
    /// An integer specifying the start index.
    /// </param>
    /// <param name="endIndex">
    /// An integer specifying the end index.
    /// </param>
    /// <returns>
    /// A new <see cref="ManagedTokenList"/> containing the references tot he instance in this
    /// list, starting from the specified index.
    /// </returns>
    public ManagedTokenList CreateCopy(int startIndex, int endIndex)
    {
        int length = endIndex - startIndex;
        ManagedTokenList newList = new ManagedTokenList(length);

        for (int index = startIndex; index <= endIndex; index++)
            newList.Add(this[index]);

        return newList;
    }

    /// <summary>
    /// Finds the first token in the current list of the specified type.
    /// </summary>
    /// <param name="tokenType">
    /// A <see cref="TokenType"/> enumerated value indicating the type of token.
    /// </param>
    /// <returns>
    /// The ordinal index of the first token of the specified type, or -1 if not found.
    /// </returns>
    public int FindFirstToken(TokenType tokenType)
    {
        int tokenIndex = -1;
        int index = 0;
        int length = Count;

        while (index < length && tokenIndex == -1)
        {
            if (this[index].TokenType == tokenType)
                tokenIndex = index;
            index++;
        }

        return tokenIndex;
    }

    /// <summary>
    /// Finds the last token in the current list of the specified type.
    /// </summary>
    /// <param name="tokenType">
    /// A <see cref="TokenType"/> enumerated value indicating the type of token.
    /// </param>
    /// <returns>
    /// The ordinal index of the last token of the specified type, or -1 if not found.
    /// </returns>
    public int FindLastToken(TokenType tokenType)
    {
        int tokenIndex = -1;
        int index = Count - 1;

        while (index > -1 && tokenIndex == -1)
        {
            if (this[index].TokenType == tokenType)
                tokenIndex = index;
            index--;
        }

        return tokenIndex;
    }

    /// <summary>
    /// Finds the next token in the current list of the specified type.
    /// </summary>
    /// <param name="startIndex">
    /// An integer specifying the ordinal index at which to start searching.
    /// </param>
    /// <param name="tokenType">
    /// A <see cref="TokenType"/> enumerated value indicating the type of token.
    /// </param>
    /// <returns>
    /// The ordinal index of the first token of the specified type, or -1 if not found.
    /// </returns>
    public int FindNextToken(int startIndex, TokenType tokenType)
    {
        int tokenIndex = -1;
        int index = startIndex;
        int length = Count;

        while (index < length && tokenIndex == -1)
        {
            if (this[index].TokenType == tokenType)
                tokenIndex = index;
            index++;
        }

        return tokenIndex;
    }

    /// <summary>
    /// Gets the indices of each token that has the specified token type.
    /// </summary>
    /// <param name="tokenType">
    /// A <see cref="TokenType"/> enumerated value indicating the type of token to search for.
    /// </param>
    /// <returns>
    /// A <see cref="List{T}"/> of <see cref="int"/> containing the index values.
    /// </returns>
    public List<int> GetIndicesOfTokenTypes(TokenType tokenType)
    {
        List<int> indices = new List<int>();

        int length = Count - 1;
        for (int count = 0; count < length; count++)
        {
            if (this[count].TokenType == tokenType)
                indices.Add(count);
        }
        return indices;
    }

    /// <summary>
    /// Gets the string value between two string delimiter tokens.
    /// </summary>
    /// <param name="startIndex">
    /// The start index of the first string delimiter token.
    /// </param>
    /// <returns>
    /// A string value containing the combined literal content of the tokens after the
    /// first string delimiter and the next string delimiter.
    /// </returns>
    public string? GetString(int startIndex)
    {
        StringBuilder builder = new StringBuilder();
        if (this[startIndex].TokenType == TokenType.StringDelimiter)
        {
            startIndex++;
            while (startIndex < Count && this[startIndex].TokenType != TokenType.StringDelimiter)
            {
                builder.Append(this[startIndex].Text);
                startIndex++;
            }
        }
        return builder.ToString();
    }

    /// <summary>
    /// Determines whether the list of tokens contains one or more arithmetic operators.
    /// </summary>
    /// <returns>
    ///   <c>true</c> if the list of tokens contains one or more arithmetic operators; otherwise, <c>false</c>.
    /// </returns>
    public bool IsArithmeticExpression()
    {
        bool hasMathOperator = false;
        int index = 0;
        int length = Count;

        while (index < length && !hasMathOperator)
        {
            if (this[index].TokenType == TokenType.ArithmeticOperator)
                hasMathOperator = true;
            index++;
        }
        return hasMathOperator;
    }

    /// <summary>
    /// Determines whether if this list of tokens represents a literal character definition.
    /// </summary>
    /// <returns>
    ///   <c>true</c> if list of tokens represents a literal character definition; otherwise, <c>false</c>.
    /// </returns>
    public bool IsSingleCharList()
    {
        return (
            (FindFirstToken(TokenType.CharacterDelimiter) == 0) &&
            (FindLastToken(TokenType.CharacterDelimiter) == Count - 1));
    }

    /// <summary>
    /// Determines whether if this list of tokens represents a literal string definition.
    /// </summary>
    /// <returns>
    ///   <c>true</c> if list of tokens represents a literal string definition; otherwise, <c>false</c>.
    /// </returns>
    public bool IsSingleString()
    {
        return (
            (FindFirstToken(TokenType.StringDelimiter) == 0) &&
            (FindLastToken(TokenType.StringDelimiter) == Count - 1));
    }

    /// <summary>
    /// Removes the separator delimiter tokens from the current list.
    /// </summary>
    /// <returns>
    /// A new <see cref="ManagedTokenList"/> with the copies of the current content minus
    /// the separator tokens.
    /// </returns>
    public ManagedTokenList RemoveSeparators()
    {
        ManagedTokenList newList = new ManagedTokenList(Count);

        int length = Count;
        for (int count = 0; count < length; count++)
        {
            IToken token = this[count];
            if (token.TokenType != TokenType.SeparatorDelimiter)
            {
                newList.Add(token);
            }
        }

        return newList;
    }

    /// <summary>
    /// Removes any whitespace indicators from the start and end of the list.
    /// </summary>
    public ManagedTokenList Trim()
    {
        ManagedTokenList newList = new ManagedTokenList();
        int length = Count;
        if (length > 0)
        {
            bool start = false;
            int index = 0;
            do
            {
                IToken token = this[index];
                if (token.TokenType != TokenType.SeparatorDelimiter)
                {
                    start = true;
                }
                if (start)
                    newList.Add(token);
                index++;
            } while (index < length);

            if (newList.Count > 0)
            {
                while (newList[newList.Count - 1].TokenType == TokenType.SeparatorDelimiter)
                {
                    index = newList.Count - 1;
                    newList.RemoveAt(index);
                }
            }
        }
        return newList;
    }
    #endregion
}
