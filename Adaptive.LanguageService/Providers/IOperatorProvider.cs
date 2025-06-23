namespace Adaptive.Intelligence.LanguageService.Providers;

/// <summary>
/// Provides the signature definition for implementations that define the complete list of operators.
/// </summary>
/// <seealso cref="ICodeProvider"/>
public interface IOperatorProvider : ICodeProvider
{

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

    /// <summary>
    /// Renders the text list of assignment operators.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}"/> of <see cref="string"/> values containing the text representations.
    /// </returns>
    List<string> RenderAssignmentOperators();

    /// <summary>
    /// Renders the text list of bitwise operators.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}"/> of <see cref="string"/> values containing the text representations.
    /// </returns>
    List<string> RenderBitwiseOperators();

    /// <summary>
    /// Renders the text list of comparison operators.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}"/> of <see cref="string"/> values containing the text representations.
    /// </returns>
    List<string> RenderComparisonOperators();

    /// <summary>
    /// Renders the text list of logical operators.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}"/> of <see cref="string"/> values containing the text representations.
    /// </returns>
    List<string> RenderLogicalOperators();

    /// <summary>
    /// Renders the text list of arithmetic operators.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}"/> of <see cref="string"/> values containing the text representations.
    /// </returns>
    List<string> RenderArithmeticOperators();

    /// <summary>
    /// Renders the text list of operational / functional operators.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}"/> of <see cref="string"/> values containing the text representations.
    /// </returns>
    List<string> RenderOperationalOperators();
}
