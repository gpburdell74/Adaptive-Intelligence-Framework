using Adaptive.BlazorBasic.LanguageService;
using Adaptive.Intelligence.Shared;
using Adaptive.LanguageService;
using Adaptive.LanguageService.Providers;
using Adaptive.LanguageService.Services;

namespace Adaptive.BlazorBasic.Services;

/// <summary>
/// Provides the service for creating dictionaries and language provider instances for Blazor BASIC.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
public sealed class BlazorBasicProviderService : DisposableObjectBase, 
    ILanguageProviderService<
        BlazorBasicDelimiters, 
        BlazorBasicErrorCodes,
        BlazorBasicFunctions, 
        BlazorBasicKeywords, 
        StandardOperators,
        BlazorBasicAssignmentOperators, 
        BlazorBasicBitwiseOperators,
        BlazorBasicComparisonOperators,
        BlazorBasicLogicalOperators,
        BlazorBasicMathOperators,
        BlazorBasicOperationalOperators>
{
    #region Private Member Declarations
    /// <summary>
    /// The data type provider.
    /// </summary>
    private IDataTypeProvider? _dataTypeProvider;

    /// <summary>
    /// The delimiter provider.
    /// </summary>
    private IDelimiterProvider? _delimiterProvider;

    /// <summary>
    /// The error provider.
    /// </summary>
    private IErrorProvider? _errorProvider;

    /// <summary>
    /// The function provider.
    /// </summary>
    private IBuiltInFunctionProvider? _functionProvider;

    /// <summary>
    /// The keyword provider.
    /// </summary>
    private IKeywordProvider? _keywordProvider;

    /// <summary>
    /// The operator provider.
    /// </summary>
    private IOperatorProvider? _operatorProvider;
    #endregion

    #region Constructor / Dispose Methods    
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicProviderService"/> class.
    /// </summary>
    /// <param name="dataTypeProvider">
    /// The <see cref="IDataTypeProvider"/> implementation instance to use.
    /// </param>
    /// <param name="delimiterProvider">
    /// The <see cref="IDelimiterProvider"/> implementation instance to use.
    /// </param>
    /// <param name="errorProvider">
    /// The <see cref="IErrorProvider"/> implementation instance to use.
    /// </param>
    /// <param name="functionProvider">
    /// The <see cref="IBuiltInFunctionProvider"/> implementation instance to use.
    /// </param>
    /// <param name="keywordProvider">
    /// The <see cref="IKeywordProvider"/> implementation instance to use.
    /// </param>
    /// <param name="operatorProvider">
    /// The <see cref="IOperatorProvider"/> implementation instance to use.
    /// </param>
    public BlazorBasicProviderService(
        IDataTypeProvider dataTypeProvider,
        IDelimiterProvider delimiterProvider,
        IErrorProvider errorProvider,
        IBuiltInFunctionProvider functionProvider,
        IKeywordProvider keywordProvider,
        IOperatorProvider operatorProvider)
    {
        _dataTypeProvider = dataTypeProvider;
        _delimiterProvider = delimiterProvider;
        _errorProvider = errorProvider;
        _functionProvider = functionProvider;
        _keywordProvider = keywordProvider;
        _operatorProvider = operatorProvider;
    }
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        _delimiterProvider = null;
        _dataTypeProvider = null;
        _errorProvider = null;
        _functionProvider = null;
        _keywordProvider = null;
        _operatorProvider = null;

        base.Dispose(disposing);
    }
    #endregion

    #region Public Methods / Functions
    /// <summary>
    /// Creates the data type dictionary.
    /// </summary>
    /// <returns>
    /// An <see cref="IDataTypeDictionary"/> that contains the data types used in the language.
    /// </returns>
    public BlazorBasicDataTypeDictionary CreateDataTypeDictionary()
    {
        if (_dataTypeProvider == null)
            throw new Exception();

        BlazorBasicDataTypeDictionary dictionary = new BlazorBasicDataTypeDictionary();
        dictionary.InitializeDictionary(_dataTypeProvider);
        return dictionary;
    }
    /// <summary>
    /// Creates the delimiter dictionary.
    /// </summary>
    /// <returns>
    /// An <see cref="IDataTypeDictionary"/> that contains the delimiters used in the language.
    /// </returns>
    public BlazorBasicDelimiterDictionary CreateDelimiterDictionary()
    {
        if (_delimiterProvider == null)
            throw new Exception();

        BlazorBasicDelimiterDictionary dictionary = new BlazorBasicDelimiterDictionary();
        dictionary.InitializeDictionary(_delimiterProvider);
        return dictionary;

    }
    /// <summary>
    /// Creates the errors dictionary.
    /// </summary>
    /// <returns>
    /// An <see cref="IErrorDictionary{T}"/> that contains the error types used in the language.
    /// </returns>
    public BlazorBasicErrorDictionary CreateErrorDictionary()
    {
        if (_errorProvider == null)
            throw new Exception();

        BlazorBasicErrorDictionary dictionary = new BlazorBasicErrorDictionary();
        dictionary.InitializeDictionary(_errorProvider);
        return dictionary;

    }
    /// <summary>
    /// Creates the built-in functions dictionary.
    /// </summary>
    /// <returns>
    /// An <see cref="IDataTypeDictionary"/> that contains the built in functions used in the language.
    /// </returns>
    public BlazorBasicFunctionDictionary CreateFunctionsDictionary()
    {
        if (_functionProvider == null)
            throw new Exception();

        BlazorBasicFunctionDictionary dictionary = new BlazorBasicFunctionDictionary();
        dictionary.InitializeDictionary(_functionProvider);
        return dictionary;
    }
    /// <summary>
    /// Creates the keyword / reserved word dictionary.
    /// </summary>
    /// <returns>
    /// An <see cref="IDataTypeDictionary"/> that contains the keywords used in the language.
    /// </returns>
    public BlazorBasicKeywordsDictionary CreateKeywordsDictionary()
    {
        if (_keywordProvider == null)
            throw new Exception();

        BlazorBasicKeywordsDictionary dictionary = new BlazorBasicKeywordsDictionary();
        dictionary.InitializeDictionary(_keywordProvider);
        return dictionary;
    }
    /// <summary>
    /// Creates the operators dictionary and sub-dictionaries.
    /// </summary>
    /// <returns>
    /// An <see cref="IOperatorDictionary{A,B,C,D,E,F,G}"/> that contains the operators used in the language.
    /// </returns>
    public BlazorBasicOperatorDictionary CreateOperatorDictionary()
    {
        if (_operatorProvider == null)
            throw new Exception();

        BlazorBasicOperatorDictionary dictionary = new BlazorBasicOperatorDictionary();
        dictionary.InitializeDictionary(_operatorProvider);
        return dictionary;
    }
    /// <summary>
    /// Creates the list of single-character delimiters.
    /// </summary>
    /// <remarks>
    /// This is used when separator delimiters are optional around the particular 1-character token, such as 
    /// parsing: 1+1 or myVariable=3.
    /// </remarks>
    /// <returns>
    /// An <see cref="IDataTypeDictionary"/> that contains the list of delimiters that are single-character length.
    /// </returns>
    public List<string> GetSingleCharacterDelimiters()
    {
        if (_delimiterProvider == null)
            throw new Exception();

        List<string> itemList = new List<string>();
        List<string> fullList = _delimiterProvider.RenderDelimiterNames();
        foreach (string delimiter in fullList)
        {
            if (delimiter.Length == 1)
                itemList.Add(delimiter);
        }
        fullList.Clear();
        return itemList;
    }
    #endregion

    #region Explicit Interface Implementation Methods
    /// <summary>
    /// Creates the data type dictionary.
    /// </summary>
    /// <returns>
    /// An <see cref="IDataTypeDictionary"/> that contains the data types used in the language.
    /// </returns>
    IDataTypeDictionary ILanguageProviderService<
        BlazorBasicDelimiters,
        BlazorBasicErrorCodes,
        BlazorBasicFunctions,
        BlazorBasicKeywords,
        StandardOperators,
        BlazorBasicAssignmentOperators,
        BlazorBasicBitwiseOperators,
        BlazorBasicComparisonOperators,
        BlazorBasicLogicalOperators,
        BlazorBasicMathOperators,
        BlazorBasicOperationalOperators>.CreateDataTypeDictionary()
    {
        return CreateDataTypeDictionary();
    }

    /// <summary>
    /// Creates the delimiter dictionary.
    /// </summary>
    /// <returns>
    /// An <see cref="IDelimiterDictionary{T}" /> that contains the delimiters used in the language.
    /// </returns>
    IDelimiterDictionary<BlazorBasicDelimiters> 
    ILanguageProviderService<
        BlazorBasicDelimiters, 
        BlazorBasicErrorCodes, 
        BlazorBasicFunctions, 
        BlazorBasicKeywords, 
        StandardOperators, 
        BlazorBasicAssignmentOperators, 
        BlazorBasicBitwiseOperators, 
        BlazorBasicComparisonOperators, 
        BlazorBasicLogicalOperators, 
        BlazorBasicMathOperators, 
        BlazorBasicOperationalOperators>.CreateDelimiterDictionary()
    {
        return CreateDelimiterDictionary();
    }

    /// <summary>
    /// Creates the errors dictionary.
    /// </summary>
    /// <returns>
    /// An <see cref="IErrorDictionary" /> that contains the error types used in the language.
    /// </returns>
    IErrorDictionary<BlazorBasicErrorCodes>
    ILanguageProviderService<
        BlazorBasicDelimiters,
        BlazorBasicErrorCodes,
        BlazorBasicFunctions,
        BlazorBasicKeywords,
        StandardOperators,
        BlazorBasicAssignmentOperators,
        BlazorBasicBitwiseOperators,
        BlazorBasicComparisonOperators,
        BlazorBasicLogicalOperators,
        BlazorBasicMathOperators,
        BlazorBasicOperationalOperators>.CreateErrorDictionary()
    {
        return CreateErrorDictionary();
    }

    IBuiltInFunctionDictionary<BlazorBasicFunctions> ILanguageProviderService<BlazorBasicDelimiters, BlazorBasicErrorCodes, BlazorBasicFunctions, BlazorBasicKeywords, StandardOperators, BlazorBasicAssignmentOperators, BlazorBasicBitwiseOperators, BlazorBasicComparisonOperators, BlazorBasicLogicalOperators, BlazorBasicMathOperators, BlazorBasicOperationalOperators>.CreateFunctionsDictionary()
    {
        return CreateFunctionsDictionary();
    }

    IKeywordDictionary<BlazorBasicKeywords> ILanguageProviderService<BlazorBasicDelimiters, BlazorBasicErrorCodes, BlazorBasicFunctions, BlazorBasicKeywords, StandardOperators, BlazorBasicAssignmentOperators, BlazorBasicBitwiseOperators, BlazorBasicComparisonOperators, BlazorBasicLogicalOperators, BlazorBasicMathOperators, BlazorBasicOperationalOperators>.CreateKeywordsDictionary()
    {
        return CreateKeywordsDictionary();
    }

    IOperatorDictionary<StandardOperators, BlazorBasicAssignmentOperators, BlazorBasicBitwiseOperators, BlazorBasicComparisonOperators, BlazorBasicLogicalOperators, BlazorBasicMathOperators, BlazorBasicOperationalOperators> 
        ILanguageProviderService<BlazorBasicDelimiters, BlazorBasicErrorCodes, BlazorBasicFunctions, BlazorBasicKeywords, StandardOperators, BlazorBasicAssignmentOperators, BlazorBasicBitwiseOperators, BlazorBasicComparisonOperators, BlazorBasicLogicalOperators, BlazorBasicMathOperators, BlazorBasicOperationalOperators>.CreateOperatorDictionary()
    {
        return CreateOperatorDictionary();
    }
    #endregion
}
