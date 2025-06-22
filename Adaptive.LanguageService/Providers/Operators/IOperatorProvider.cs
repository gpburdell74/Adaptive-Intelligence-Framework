namespace Adaptive.LanguageService.Providers;

/// <summary>
/// Provides the signature definition for implementations that define the complete list of operators.
/// </summary>
/// <seealso cref="ICodeProvider"/>
public interface IOperatorProvider : ICodeProvider
{
    /// <summary>
    /// Gets the reference to the provider for assignment operators.
    /// </summary>
    /// <value>
    /// The <see cref="IAssignmentOperatorProvider"/> instance that provides the list of assignment operators.
    /// </value>
    IAssignmentOperatorProvider AssignmentOperators { get; }

    /// <summary>
    /// Gets the reference to the provider for bitwise operators.
    /// </summary>
    /// <value>
    /// The <see cref="IBitwiseOperatorProvider"/> instance that provides the list of assignment operators.
    /// </value>
    IBitwiseOperatorProvider BitwiseOperators { get; }

    /// <summary>
    /// Gets the reference to the provider for comparison operators.
    /// </summary>
    /// <value>
    /// The <see cref="IComparisonOperatorProvider"/> instance that provides the list of assignment operators.
    /// </value>
    IComparisonOperatorProvider ComparisonOperators { get; }

    /// <summary>
    /// Gets the reference to the provider for logical operators.
    /// </summary>
    /// <value>
    /// The <see cref="ILogicalOperatorProvider"/> instance that provides the list of assignment operators.
    /// </value>
    ILogicalOperatorProvider LogicalOperators { get; }

    /// <summary>
    /// Gets the reference to the provider for arithmetic operators.
    /// </summary>
    /// <value>
    /// The <see cref="IMathOperatorProvider"/> instance that provides the list of assignment operators.
    /// </value>
    IMathOperatorProvider MathOperators { get; }

    /// <summary>
    /// Gets the reference to the provider for operation operators.
    /// </summary>
    /// <value>
    /// The <see cref="IOperationOperatorProvider"/> instance that provides the list of assignment operators.
    /// </value>
    IOperationOperatorProvider OperationOperators { get; }

    /// <summary>
    /// Renders the list of operator ID values for mapping.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}"/> of unique ID values used for mapping ID to text content.
    /// </returns>
    List<int> RenderOperatorIds();

    /// <summary>
    /// Renders the list of operator names for mapping.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}"/> of unique name values used for mapping text to ID values.
    /// </returns>
    List<string> RenderOperatorNames();

}
