using Adaptive.BlazorBasic.LanguageService;
using Adaptive.Intelligence.Shared;

namespace Adaptive.BlazorBasic;

/// <summary>
/// Provides the operator dictionary for the Blazor BASIC language.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="IOperatorDictionary" />
public class BlazorBasicOperatorDictionary : TwoWayDictionaryBase<string, StandardOperatorTypes>, IOperatorDictionary
{
    #region Constructor 
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicOperatorDictionary"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public BlazorBasicOperatorDictionary()
    {
    }
    #endregion

    #region Public Methods / Functions
    /// <summary>
    /// Gets the operator as specified by the provided text.
    /// </summary>
    /// <param name="operatorText">
    /// A string containing the code representation of the operator.
    /// </param>
    /// <returns>
    /// A <see cref="StandardOperatorTypes" /> enumerated value indicating the operator.
    /// </returns>
    public StandardOperatorTypes GetOperator(string? operatorText)
    {
        return GetOperator(NormalizeString(operatorText));
    }

    /// <summary>
    /// Gets the name of the operator.
    /// </summary>
    /// <param name="operatorType">A <see cref="!:StandardOperators" /> enumerated value indicating the operator.</param>
    /// <returns>
    /// A string containing the standard code/text for the specified operator in the language being implemented,
    /// or <b>null</b> if an invalid operator is specified.
    /// </returns>
    public string? GetOperatorName(StandardOperatorTypes operatorType)
    {
        return ReverseGet(operatorType);
    }

    /// <summary>
    /// Populates the dictionary with the operators from the specified language provider.
    /// </summary>
    /// <param name="provider">The <see cref="ILanguageService" /> provider instance used to provide the list.</param>
    public void InitializeDictionary(ILanguageService provider)
    {
        List<string> operatorList = provider.RenderOperators();

        foreach(string operatorCode in operatorList)
        {
            string? normalized = NormalizeString(operatorCode);
            if (normalized != null)
                AddEntry(operatorCode, normalized, provider.MapOperator(operatorCode));
        }
        operatorList.Clear();
    }

    /// <summary>
    /// Gets a value indicating whether the specified code is an operator in the language being implemented.
    /// </summary>
    /// <param name="code">A string containing the code text to be evaluated.</param>
    /// <returns>
    /// <c>true</c> if the text can be matched to a known operator; otherwise, <c>false</c>.
    /// </returns>
    public bool IsOperator(string? code)
    {
        return IsInDictionary(code);
    }
    #endregion
}

