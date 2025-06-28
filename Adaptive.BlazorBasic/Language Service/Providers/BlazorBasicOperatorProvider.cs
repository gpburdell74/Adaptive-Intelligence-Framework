using Adaptive.Intelligence.LanguageService;
using Adaptive.Intelligence.LanguageService.Providers;
using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.BlazorBasic.LanguageService;

/// <summary>
/// Provides the implementation for the provider that defines the list of operators.
/// </summary>
/// <seealso cref="DisposableObjectBase"/>
/// <seealso cref="IOperatorProvider"/>
public sealed class BlazorBasicOperatorProvider : DisposableObjectBase, IOperatorProvider
{
    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicOperatorProvider"/> class.
    /// </summary>
    public BlazorBasicOperatorProvider()
    {
    }

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
    }
    #endregion

    #region Public Methods / Functions
    /// <summary>
    /// Renders the list of operator ID values for mapping.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}" /> of unique ID values used for mapping ID to text content.
    /// </returns>
    public List<int> RenderOperatorIds()
    {
        return new List<int>(25)
        {
            (int)StandardOperators.MathAdd,
            (int)StandardOperators.MathSubtract,
            (int)StandardOperators.MathMultiply,
            (int)StandardOperators.MathDivide,
            (int)StandardOperators.MathModulus,
            (int)StandardOperators.MathExponent,
            (int)StandardOperators.AssignmentEquals,
            (int)StandardOperators.BitwiseShortAnd,
            (int)StandardOperators.BitwiseLongAnd,
            (int)StandardOperators.BitwiseShortOr,
            (int)StandardOperators.BitwiseLongOr,
            (int)StandardOperators.ComparisonGreaterThan,
            (int)StandardOperators.ComparisonGreaterThanOrEqualTo,
            (int)StandardOperators.ComparisonLessThan,
            (int)StandardOperators.ComparisonLessThanOrEqualTo,
            (int)StandardOperators.ComparisonNotEqualTo,
            (int)StandardOperators.ComparisonNotEqualTo,
            (int)StandardOperators.ComparisonEqualTo,
            (int)StandardOperators.LogicalAnd,
            (int)StandardOperators.LogicalOr,
            (int)StandardOperators.LogicalNot,
            (int)StandardOperators.LogicalAndShort,
            (int)StandardOperators.LogicalOrShort,
            (int)StandardOperators.LogicalNotShort,
            (int)StandardOperators.OpsIncrement,
            (int)StandardOperators.OpsDecrement
        };
    }
    /// <summary>
    /// Renders the list of operator names for mapping.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}" /> of unique name values used for mapping text to ID values.
    /// </returns>
    public List<string> RenderOperatorNames()
    {
        return new List<string>(25)
        {
            OperatorNames.OperatorAdd,
            OperatorNames.OperatorSubtract,
            OperatorNames.OperatorMultiply,
            OperatorNames.OperatorDivide,
            OperatorNames.OperatorModulus,
            OperatorNames.OperatorExponent,
            OperatorNames.OperatorAssignment,
            OperatorNames.OperatorBitwiseAnd,
            OperatorNames.OperatorBitwiseLongAnd ,
            OperatorNames.OperatorBitwiseOr,
            OperatorNames.OperatorBitwiseLongOr,
            OperatorNames.OperatorGreaterThan,
            OperatorNames.OperatorGreaterThanOrEqualTo,
            OperatorNames.OperatorLessThan,
            OperatorNames.OperatorLessThanOrEqualTo,
            OperatorNames.OperatorNotEqualTo,
            OperatorNames.OperatorNotEqualTo2,
            OperatorNames.OperatorEqualTo,
            OperatorNames.OperatorLogicalAnd,
            OperatorNames.OperatorLogicalOr,
            OperatorNames.OperatorLogicalNot,
            OperatorNames.OperatorLogicalAndShort,
            OperatorNames.OperatorLogicalOrShort,
            OperatorNames.OperatorLogicalNotShort,
            OperatorNames.OperatorIncrement,
            OperatorNames.OperatorDecrement
        };
    }

    /// <summary>
    /// Renders the text list of assignment operators.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}"/> of <see cref="string"/> values containing the text representations.
    /// </returns>
    public List<string> RenderAssignmentOperators()
    {
        return new List<string>(1)
        {
            OperatorNames.OperatorAssignment
        };
    }

    /// <summary>
    /// Renders the text list of bitwise operators.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}"/> of <see cref="string"/> values containing the text representations.
    /// </returns>
    public List<string> RenderBitwiseOperators()
    {
        return new List<string>(4)
        {
            OperatorNames.OperatorBitwiseAnd,
            OperatorNames.OperatorBitwiseLongAnd,
            OperatorNames.OperatorBitwiseOr,
            OperatorNames.OperatorBitwiseLongOr
        };
    }

    /// <summary>
    /// Renders the text list of comparison operators.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}"/> of <see cref="string"/> values containing the text representations.
    /// </returns>
    public List<string> RenderComparisonOperators()
    {
        return new List<string>(6)
        {
            OperatorNames.OperatorGreaterThan,
            OperatorNames.OperatorGreaterThanOrEqualTo,
            OperatorNames.OperatorLessThan,
            OperatorNames.OperatorLessThanOrEqualTo,
            OperatorNames.OperatorEqualTo,
            OperatorNames.OperatorNotEqualTo
        };
    }

    /// <summary>
    /// Renders the text list of logical operators.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}"/> of <see cref="string"/> values containing the text representations.
    /// </returns>
    public List<string> RenderLogicalOperators()
    {
        return new List<string>(6)
        {
            OperatorNames.OperatorLogicalAnd,
            OperatorNames.OperatorLogicalAndShort,
            OperatorNames.OperatorLogicalOr,
            OperatorNames.OperatorLogicalOrShort,
            OperatorNames.OperatorLogicalNot,
            OperatorNames.OperatorLogicalNotShort

        };
    }

    /// <summary>
    /// Renders the text list of arithmetic operators.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}"/> of <see cref="string"/> values containing the text representations.
    /// </returns>
    public List<string> RenderArithmeticOperators()
    {
        return new List<string>(6)
        {
            OperatorNames.OperatorAdd,
            OperatorNames.OperatorSubtract,
            OperatorNames.OperatorMultiply,
            OperatorNames.OperatorDivide,
            OperatorNames.OperatorModulus,
            OperatorNames.OperatorExponent
        };
    }

    /// <summary>
    /// Renders the text list of operational / functional operators.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}"/> of <see cref="string"/> values containing the text representations.
    /// </returns>
    public List<string> RenderOperationalOperators()
    {
        return new List<string>(2)
        {
            OperatorNames.OperatorDecrement,
            OperatorNames.OperatorIncrement
        };
    }

    #endregion
}