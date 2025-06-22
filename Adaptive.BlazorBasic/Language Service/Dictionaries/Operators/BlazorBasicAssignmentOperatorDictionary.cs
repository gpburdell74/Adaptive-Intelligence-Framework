using Adaptive.LanguageService;
using Adaptive.LanguageService.Providers;

namespace Adaptive.BlazorBasic.LanguageService;

/// <summary>
/// Provides a two-way look-up dictionary for assignment operators.
/// </summary>
/// <seealso cref="TwoWayDictionaryBase{PrimaryValue, SecondaryValue}" />
/// <seealso cref="IAssignmentOperatorDictionary{T}" />
public sealed class BlazorBasicAssignmentOperatorDictionary : TwoWayDictionaryBase<string, BlazorBasicAssignmentOperators>, IAssignmentOperatorDictionary<BlazorBasicAssignmentOperators>
{
    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicAssignmentOperatorDictionary"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public BlazorBasicAssignmentOperatorDictionary()
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
    /// A <see cref="BlazorBasicAssignmentOperators" /> enumerated value indicating the operator.
    /// </returns>
    public BlazorBasicAssignmentOperators GetOperator(string? operatorText)
    {
        if (operatorText == null)
            throw new Exception();

        return Get(NormalizeKeyValue(operatorText));
    }

    /// <summary>
    /// Gets the text representation of the operator.
    /// </summary>
    /// <param name="operatorId">
    /// A <see cref="BlazorBasicAssignmentOperators" /> enumerated value indicating the data type.</param>
    /// <returns>
    /// A string containing the standard code/text for the specified operator.
    /// </returns>
    public string? GetOperatorText(BlazorBasicAssignmentOperators operatorId)
    {
        return ReverseGet(operatorId);
    }

    /// <summary>
    /// Populates the dictionary with the operators from the specified language provider.
    /// </summary>
    /// <param name="provider">The <see cref="IAssignmentOperatorProvider" /> provider instance used to provide the list.</param>
    public void InitializeDictionary(IAssignmentOperatorProvider provider)
    {
        // Get the list of data type names.
        List<string> names = provider.RenderOperatorNames();

        // Get the list of ID values and translate to the appropriate list of enumerations.
        List<int> idValues = provider.RenderOperatorIds();
        List<BlazorBasicAssignmentOperators> typeList = IdsToEnum<BlazorBasicAssignmentOperators>(idValues);
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
    public bool IsAssignmentOperator(string? code)
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
