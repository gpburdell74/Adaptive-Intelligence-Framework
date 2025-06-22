using Adaptive.LanguageService.Providers;

namespace Adaptive.LanguageService;

/// <summary>
/// Provides the signature definition for implementations for operator dictionaries.
/// </summary>
/// <seealso cref="ISpecificOperatorDictionary" />
public interface IOperatorDictionary<AllOperatorsType, AssignmentType, BitwiseType, ComparisonType, LogicalType, MathType, OperationalType> 
    : ISpecificOperatorDictionary<AllOperatorsType>
    where AllOperatorsType : Enum
    where AssignmentType : Enum
    where BitwiseType : Enum
    where ComparisonType : Enum
    where LogicalType : Enum
    where MathType : Enum
    where OperationalType : Enum
{
    #region Properties
    /// <summary>
    /// Gets the reference to the dictionary for assignment operators.
    /// </summary>
    /// <value>
    /// The <see cref="IAssignmentOperatorDictionary{AssignmentType}"/> instance.
    /// </value>
    IAssignmentOperatorDictionary<AssignmentType> AssignmentOperators { get;  }
    /// <summary>
    /// Gets the reference to the dictionary for bitwise operators.
    /// </summary>
    /// <value>
    /// The <see cref="IBitwiseOperatorDictionary{BitwiseType}"/> instance.
    /// </value>
    IBitwiseOperatorDictionary<BitwiseType> BitwiseOperators { get; }
    /// <summary>
    /// Gets the reference to the dictionary for comparison operators.
    /// </summary>
    /// <value>
    /// The <see cref="IComparisonOperatorDictionary{ComparisonType}"/> instance.
    /// </value>
    IComparisonOperatorDictionary<ComparisonType> ComparisonOperators { get; }
    /// <summary>
    /// Gets the reference to the dictionary for logical operators.
    /// </summary>
    /// <value>
    /// The <see cref="ILogicalOperatorDictionary{LogicalType}"/> instance.
    /// </value>
    ILogicalOperatorDictionary<LogicalType> LogicalOperators { get;  }
    /// <summary>
    /// Gets the reference to the dictionary for arithmetic operators.
    /// </summary>
    /// <value>
    /// The <see cref="IMathOperatorDictionary{MathType}"/> instance.
    /// </value>
    IMathOperatorDictionary<MathType> MathOperators { get; }
    /// <summary>
    /// Gets the reference to the dictionary for operational/functional operators.
    /// </summary>
    /// <value>
    /// The <see cref="IOperationalOperatorDictionary{OperationalType}"/> instance.
    /// </value>
    IOperationalOperatorDictionary<OperationalType> OperationalOperators { get; }
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
}
