namespace Adaptive.LanguageService;

/// <summary>
/// Provides the signature definition for implementations for dictionaries for specific types of operators.
/// </summary>
/// <seealso cref="ICodeDictionary" />
public interface ISpecificOperatorDictionary<DataType> : ICodeDictionary
    where DataType : Enum
{
    /// <summary>
    /// Gets the operator as specified by the provided text.
    /// </summary>
    /// <param name="operatorText">
    /// A string containing the name / text representation. of the operator.
    /// </param>
    /// <returns>
    /// A <typeparamref name="DataType"/> enumerated value containing the unique ID value identifying the operator.
    /// </returns>
    DataType GetOperator(string? operatorText);

    /// <summary>
    /// Gets the name or text representation for the operator.
    /// </summary>
    /// <param name="operatorId">
    /// A <typeparamref name="DataType"/> enumerated value containing the unique ID value identifying the operator.
    /// </param>
    /// <returns>
    /// A string containing the standard code/text for the specified operator in the language being implemented.
    /// </returns>
    string? GetOperatorText(DataType operatorId);
}
