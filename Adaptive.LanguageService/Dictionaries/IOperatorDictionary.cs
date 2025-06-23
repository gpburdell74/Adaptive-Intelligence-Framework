using Adaptive.Intelligence.LanguageService.Providers;

namespace Adaptive.Intelligence.LanguageService.Dictionaries;

/// <summary>
/// Provides the signature definition for implementations for operator dictionaries.
/// </summary>
/// <seealso cref="ICodeProvider" />
public interface IOperatorDictionary<AllOperatorsType> : ICodeProvider
    where AllOperatorsType : Enum
{
    #region Properties
    /// <summary>
    /// Gets the reference to the dictionary for assignment operators.
    /// </summary>
    /// <value>
    /// The <see cref="IOperatorDictionary{AllOperatorsType}"/> instance containing only the assignment operators.
    /// </value>
    IOperatorDictionary<AllOperatorsType> AssignmentOperators { get; }
    /// <summary>
    /// Gets the reference to the dictionary for bitwise operators.
    /// </summary>
    /// <value>
    /// The <see cref="IOperatorDictionary{AllOperatorsType}"/> instance containing only the bitwise operators.
    /// </value>
    IOperatorDictionary<AllOperatorsType> BitwiseOperators { get; }
    /// <summary>
    /// Gets the reference to the dictionary for comparison operators.
    /// </summary>
    /// <value>
    /// The <see cref="IOperatorDictionary{AllOperatorsType}"/> instance containing only the comparison operators.
    /// </value>
    IOperatorDictionary<AllOperatorsType> ComparisonOperators { get; }
    /// <summary>
    /// Gets the reference to the dictionary for logical operators.
    /// </summary>
    /// <value>
    /// The <see cref="IOperatorDictionary{AllOperatorsType}"/> instance containing only the logical operators.
    /// </value>
    IOperatorDictionary<AllOperatorsType> LogicalOperators { get; }
    /// <summary>
    /// Gets the reference to the dictionary for arithmetic operators.
    /// </summary>
    /// <value>
    /// The <see cref="IOperatorDictionary{AllOperatorsType}"/> instance containing only the arithmetic operators.
    /// </value>
    IOperatorDictionary<AllOperatorsType> MathOperators { get; }
    /// <summary>
    /// Gets the reference to the dictionary for operational/functional operators.
    /// </summary>
    /// <value>
    /// The <see cref="IOperatorDictionary{AllOperatorsType}"/> instance containing only the operational operators.
    /// </value>
    IOperatorDictionary<AllOperatorsType> OperationalOperators { get; }
    #endregion

    /// <summary>
    /// Populates the dictionary with the functions from the specified language provider.
    /// </summary>
    /// <param name="provider">
    /// The <see cref="IOperatorProvider" /> provider instance used to provide the list.
    /// </param>
    void InitializeDictionary(IOperatorProvider provider);

    /// <summary>
    /// Gets a value indicating whether the specified code is a built-in function in the language being implemented.
    /// </summary>
    /// <param name="code">A string containing the code text to be evaluated.</param>
    /// <returns>
    /// <c>true</c> if the text can be matched to a known function; otherwise, <c>false</c>.
    /// </returns>
    bool IsOperator(string? code);

    /// <summary>
    /// Adds the entry to the dictionary.
    /// </summary>
    /// <param name="operatorText">
    /// A string containing the operator text value.
    /// </param>
    /// <param name="operatorType">
    /// A <typeparamref name="AllOperatorsType"/> enumerated value indicating the type of the operator.
    /// </param>
    void AddEntry(string? operatorText, AllOperatorsType operatorType);

    /// <summary>
    /// Gets the type of the operator.
    /// </summary>
    /// <param name="operatorType">
    /// A <typeparamref name="AllOperatorsType"/> enumerated value indicating the operator.
    /// </param>
    /// <returns>
    /// A <see cref="StandardOperatorTypes"/> enumerated value indicating the type of operator.
    /// </returns>
    StandardOperatorTypes GetOperatorType(AllOperatorsType operatorType);


}
