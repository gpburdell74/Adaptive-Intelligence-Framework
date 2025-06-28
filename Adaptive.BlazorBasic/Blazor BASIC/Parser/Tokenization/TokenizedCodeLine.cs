using Adaptive.Intelligence.BlazorBasic.Services;
using Adaptive.Intelligence.LanguageService.Tokenization;
using Adaptive.Intelligence.Shared;
using System.Text;

namespace Adaptive.Intelligence.BlazorBasic.Parser;

/// <summary>
/// Represents and manages the tokens for a line of code.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="ITokenizedCodeLine" />
public class TokenizedCodeLine : DisposableObjectBase, ITokenizedCodeLine
{
    #region Private Member Declarations    
    /// <summary>
    /// The service reference.
    /// </summary>
    private BlazorBasicLanguageService? _service;

    /// <summary>
    /// The list of tokens.
    /// </summary>
    private List<IToken>? _tokens;
    #endregion

    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="TokenizedCodeLine"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the <see cref="BlazorBasicLanguageService"/> instance.
    /// </param>
    public TokenizedCodeLine(BlazorBasicLanguageService service)
    {
        _service = service;
        _tokens = new List<IToken>();
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
            _tokens?.Clear();
        }

        _tokens = null;
        _service = null;
        base.Dispose(disposing);
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets the number of tokens in the code line.
    /// </summary>
    /// <value>
    /// An integer indicating the number of tokens.
    /// </value>
    public int Count
    {
        get
        {
            if (_tokens == null)
                return 0;
            else
                return _tokens.Count;
        }
    }
    /// <summary>
    /// Gets or sets the line number.
    /// </summary>
    /// <value>
    /// The line number specified when parsing.
    /// </value>
    public int LineNumber { get; set; }

    /// <summary>
    /// Gets the <see cref="IToken"/> at the specified index.
    /// </summary>
    /// <value>
    /// The <see cref="IToken"/>.
    /// </value>
    /// <param name="index">The index.</param>
    /// <returns></returns>
    public IToken? this[int index]
    {
        get
        {
            if (_tokens != null)
                return _tokens[index];
            else
                return null;
        }
    }
    /// <summary>
    /// Gets the reference to the list of tokens for a line of code.
    /// </summary>
    /// <value>
    /// A <see cref="List{T}" /> of <see cref="IToken" /> instances.
    /// </value>
    public List<IToken>? TokenList => _tokens;
    #endregion

    #region Public Methods / Functions
    /// <summary>
    /// Adds the specified token to the list.
    /// </summary>
    /// <param name="token">
    /// The <see cref="IToken"/> instance to add.
    /// </param>
    public void Add(IToken token)
    {
        if (_tokens != null)
            _tokens.Add(token);
    }
    /// <summary>
    /// Combines the values of each of the tokens into a single string.
    /// </summary>
    /// <param name="startIndex">The ordinal index of the first token.</param>
    /// <param name="endIndex">The ordinal index of the last token.</param>
    /// <returns>
    /// A string containing the combined text values.
    /// </returns>
    public string CombineValues(int startIndex, int endIndex)
    {
        StringBuilder builder = new StringBuilder();

        for (int index = startIndex; index <= endIndex; index++)
            builder.Append(this[index]!.Text);

        return builder.ToString();
    }

    /// <summary>
    /// Finds the index of the first token of the specified type in the current instance.
    /// </summary>
    /// <param name="tokenType">
    /// A <see cref="TokenType"/> enumerated value indicating the type of token to find.
    /// </param>
    /// <returns>
    /// An integer specifying the ordinal index of the specified token, or -1 if not found.
    /// </returns>
    public int IndexOf(TokenType tokenType)
    {
        int ordinalIndex = -1;

        if (Count > 0)
        {
            int searchIndex = 0;
            do
            {
                IToken? current = this[searchIndex];
                if (current != null && current.TokenType == tokenType)
                    ordinalIndex = searchIndex;
                searchIndex++;
            } while (searchIndex < Count && ordinalIndex == -1);
        }

        return ordinalIndex;
    }

    /// <summary>
    /// Finds the index of the first token of the specified type in the current instance that
    /// matches the specified text.
    /// </summary>
    /// <param name="tokenType">
    /// A <see cref="TokenType"/> enumerated value indicating the type of token to find.
    /// </param>
    /// <param name="textMatch">
    /// A string containing the text value to match on.
    /// </param>
    /// <returns>
    /// An integer specifying the ordinal index of the specified token, or -1 if not found.
    /// </returns>
    public int IndexOf(TokenType tokenType, string textMatch)
    {
        int ordinalIndex = -1;

        if (Count > 0)
        {
            int searchIndex = 0;
            do
            {
                IToken? current = this[searchIndex];
                if (current != null && current.TokenType == tokenType &&
                    string.Compare(current.Text, textMatch, true) == 0)

                    ordinalIndex = searchIndex;
                searchIndex++;
            } while (searchIndex < Count && ordinalIndex == -1);
        }

        return ordinalIndex;
    }

    /// <summary>
    /// Finds the index of the last token of the specified type in the current instance.
    /// </summary>
    /// <param name="tokenType">
    /// A <see cref="TokenType"/> enumerated value indicating the type of token to find.
    /// </param>
    /// <returns>
    /// An integer specifying the ordinal index of the specified token, or -1 if not found.
    /// </returns>
    public int LastIndexOf(TokenType tokenType)
    {
        int ordinalIndex = -1;

        if (Count > 0)
        {
            int searchIndex = Count - 1;
            do
            {
                IToken? current = this[searchIndex];
                if (current != null && current.TokenType == tokenType)
                    ordinalIndex = searchIndex;
                searchIndex--;
            } while (searchIndex >= 0 && ordinalIndex == -1);
        }

        return ordinalIndex;
    }


    /// <summary>
    /// Extracts a list of the tokens in the current instance starting at the specified index
    /// and ending at the specified index.
    /// </summary>
    /// <param name="leftIndex">
    /// An integer containing the ordinal index at which to start.
    /// </param>
    /// <param name="rightIndex">
    /// An integer containing the ordinal index at which to end.
    /// </param>
    /// <returns>
    /// A <see cref="List{T}"/> containing the sub-list of <see cref="IToken"/> instances, if successful;
    /// otherwise returns an empty list.
    /// </returns>
    public List<IToken> SubExpression(int leftIndex, int rightIndex)
    {
        int length = rightIndex - leftIndex;
        if (length < 0)
            throw new SyntaxErrorException(LineNumber);

        if (leftIndex < 0 || rightIndex > Count-1)
            throw new SyntaxErrorException(LineNumber);

        length++;
        List<IToken> returnList = new List<IToken>(length);
        for(int index = leftIndex; index <= rightIndex; index++)
        {
            IToken? token = this[index];
            if (token != null)
                returnList.Add(token);
        }
        return returnList;
    }

    /// <summary>
    /// Substitutes the new token for the token at the specified index.
    /// </summary>
    /// <param name="index">An integer containing the ordinal index.</param>
    /// <param name="newToken">An <see cref="IToken" /> containing the new token instance.</param>
    /// <returns>
    /// The reference to the <see cref="IToken"/> that was removed from the list.
    /// </returns>
    public IToken? Substitute(int index, IToken newToken)
    {
        IToken? original = null;
        if (_tokens != null && index > -1 && index < _tokens!.Count)
        {
            // Capture the original and substitute the new instance.
            original = _tokens[index];
            _tokens[index] = newToken;
        }
        return original;
    }
    #endregion
}

