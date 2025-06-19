using Adaptive.BlazorBasic.LanguageService;
using Adaptive.Intelligence.Shared;

namespace Adaptive.BlazorBasic.Parser;

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
    private ILanguageService<BlazorBasicFunctions, BlazorBasicKeywords>? _service;

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
    /// The reference to the language service instance.
    /// </param>
    public TokenizedCodeLine(ILanguageService<BlazorBasicFunctions, BlazorBasicKeywords> service)
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
        if (_tokens != null && index > 0 && index < _tokens!.Count)
        {
            // Capture the original and substitute the new instance.
            original = _tokens[index];
            _tokens[index] = newToken;
        }
        return original;
    }
    #endregion
}

