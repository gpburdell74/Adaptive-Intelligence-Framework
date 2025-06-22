using Adaptive.LanguageService;
using Adaptive.LanguageService.Providers;

namespace Adaptive.BlazorBasic.LanguageService;

/// <summary>
/// Provides a two-way look-up dictionary for arithmetic operators.
/// </summary>
/// <seealso cref="TwoWayDictionaryBase{PrimaryValue, SecondaryValue}" />
/// <seealso cref="IMathOperatorDictionary{T}" />
public sealed class BlazorBasicMathOperatorDictionary : TwoWayDictionaryBase<string, BlazorBasicMathOperators>, IMathOperatorDictionary<BlazorBasicMathOperators>
{
    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicMathOperatorDictionary"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public BlazorBasicMathOperatorDictionary()
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
    /// A <see cref="BlazorBasicMathOperators" /> enumerated value indicating the operator.
    /// </returns>
    public BlazorBasicMathOperators GetOperator(string? operatorText)
    {
        if (operatorText == null)
            throw new Exception();

        return Get(NormalizeKeyValue(operatorText));
    }

    /// <summary>
    /// Gets the text representation of the operator.
    /// </summary>
    /// <param name="operatorId">
    /// A <see cref="BlazorBasicMathOperators" /> enumerated value indicating the operator.
    /// </param>
    /// <returns>
    /// A string containing the standard code/text for the specified operator.
    /// </returns>
    public string? GetOperatorText(BlazorBasicMathOperators operatorId)
    {
        return ReverseGet(operatorId);
    }

    /// <summary>
    /// Populates the dictionary with the operators from the specified language provider.
    /// </summary>
    /// <param name="provider">The <see cref="IMathOperatorProvider" /> provider instance used to provide the list.
    /// </param>
    public void InitializeDictionary(IMathOperatorProvider provider)
    {
        // Get the list of data type names.
        List<string> names = provider.RenderOperatorNames();

        // Get the list of ID values and translate to the appropriate list of enumerations.
        List<int> idValues = provider.RenderOperatorIds();
        List<BlazorBasicMathOperators> typeList = IdsToEnum<BlazorBasicMathOperators>(idValues);
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
    public bool IsMathOperator(string? code)
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
