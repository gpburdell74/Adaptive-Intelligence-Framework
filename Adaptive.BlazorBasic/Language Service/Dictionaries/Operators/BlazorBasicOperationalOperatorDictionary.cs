using Adaptive.LanguageService;
using Adaptive.LanguageService.Providers;

namespace Adaptive.BlazorBasic.LanguageService;

/// <summary>
/// Provides a two-way look-up dictionary for operational / functional operators.
/// </summary>
/// <seealso cref="TwoWayDictionaryBase{PrimaryValue, SecondaryValue}" />
/// <seealso cref="IOperationalOperatorDictionary{T}" />
public sealed class BlazorBasicOperationalOperatorDictionary : TwoWayDictionaryBase<string, BlazorBasicOperationalOperators>, IOperationalOperatorDictionary<BlazorBasicOperationalOperators>
{
    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicOperationalOperatorDictionary"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public BlazorBasicOperationalOperatorDictionary()
    {
    }
    #endregion

    #region Public Methods / DataTypes
    /// <summary>
    /// Gets the operator as specified by the provided text.
    /// </summary>
    /// <param name="operatorText">
    /// A string containing the operator text.
    /// </param>
    /// <returns>
    /// A <see cref="BlazorBasicOperationalOperators" /> enumerated value indicating the operator.
    /// </returns>
    public BlazorBasicOperationalOperators GetOperator(string? operatorText)
    {
        if (operatorText == null)
            throw new Exception();

        return Get(NormalizeKeyValue(operatorText));
    }

    /// <summary>
    /// Gets the text representation of the operator.
    /// </summary>
    /// <param name="operatorId">
    /// A <see cref="BlazorBasicOperationalOperators" /> enumerated value indicating the operator.
    /// </param>
    /// <returns>
    /// A string containing the standard code/text for the specified operator.
    /// </returns>
    public string? GetOperatorText(BlazorBasicOperationalOperators operatorId)
    {
        return ReverseGet(operatorId);
    }

    /// <summary>
    /// Populates the dictionary with the operators from the specified language provider.
    /// </summary>
    /// <param name="provider">The <see cref="IOperationOperatorProvider" /> provider instance used to provide the list.
    /// </param>
    public void InitializeDictionary(IOperationOperatorProvider provider)
    {
        // Get the list of data type names.
        List<string> names = provider.RenderOperatorNames();

        // Get the list of ID values and translate to the appropriate list of enumerations.
        List<int> idValues = provider.RenderOperatorIds();
        List<BlazorBasicOperationalOperators> typeList = IdsToEnum<BlazorBasicOperationalOperators>(idValues);
        idValues.Clear();

        BaseInitialize(names, typeList);

        typeList.Clear();
        names.Clear();
    }

    /// <summary>
    /// Gets a value indicating whether the specified code is a built-in data type in the language being implemented.
    /// </summary>
    /// <param name="code">A string containing the code text to be evaluated.</param>
    /// <returns>
    /// <c>true</c> if the text can be matched to a known data type; otherwise, <c>false</c>.
    /// </returns>
    public bool IsOperationalOperator(string? code)
    {
        return IsInDictionary(code);
    }

    #endregion

    #region Protected Method Overrides    
    /// <summary>
    /// Normalizes the specified value for use in a dictionary as a key value.
    /// </summary>
    /// <param name="value">
    /// A <see cref="string"/> variable containing the value.
    /// </param>
    /// <returns>
    /// The normalized version of the value for use in a dictionary as a key.
    /// </returns>
    protected override string NormalizeKeyValue(string value)
    {
        return value.ToLower().Trim();
    }
    #endregion
}
