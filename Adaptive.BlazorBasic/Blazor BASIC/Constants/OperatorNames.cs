namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Provides the static constants definitions for the operators.
/// </summary>

internal static class OperatorNames
{
    #region Arithmetic Operators
    /// <summary>
    /// The operator for addition.
    /// </summary>
    public const string OperatorAdd = "+";
    /// <summary>
    /// The operator for subtraction.
    /// </summary>
    public const string OperatorSubtract = "-";
    /// <summary>
    /// The operator for multiplication.
    /// </summary>
    public const string OperatorMultiply = "*";
    /// <summary>
    /// The operator for division.
    /// </summary>
    public const string OperatorDivide = "/";
    /// <summary>
    /// The operator for modulus.
    /// </summary>
    public const string OperatorModulus = "%";
    /// <summary>
    /// The operator for an exponent e.g. x ^ y (x raised to the power of y).
    /// </summary>
    public const string OperatorExponent = "^";
    #endregion

    #region Assignment Operators
    /// <summary>
    /// The assignment operator.
    /// </summary>
    public const string OperatorAssignment = "=";
    #endregion

    #region Bitwise Operators
    /// <summary>
    /// The operator for a bitwise and.
    /// </summary>
    public const string OperatorBitwiseAnd = "&";
    /// <summary>
    /// The operator for a bitwise and.
    /// </summary>
    public const string OperatorBitwiseLongAnd = "AND";
    /// <summary>
    /// The operator for a bitwise or.
    /// </summary>
    public const string OperatorBitwiseOr = "|";
    /// <summary>
    /// The operator for a bitwise or.
    /// </summary>
    public const string OperatorBitwiseLongOr = "OR";
    #endregion

    #region Comparison Operators
    /// <summary>
    /// The operator for greater than.
    /// </summary>
    public const string OperatorGreaterThan = ">";
    /// <summary>
    /// The operator for greater than or equal to.
    /// </summary>
    public const string OperatorGreaterThanOrEqualTo = ">=";
    /// <summary>
    /// The operator for less than.
    /// </summary>
    public const string OperatorLessThan = "<";
    /// <summary>
    /// The operator for less than or equal to.
    /// </summary>
    public const string OperatorLessThanOrEqualTo = "<=";
    /// <summary>
    /// The operator for not equal to.
    /// </summary>
    public const string OperatorNotEqualTo = "!=";
    /// <summary>
    /// The operator for equal to.
    /// </summary>
    public const string OperatorEqualTo = "==";
    #endregion

    #region Logical Operators
    /// <summary>
    /// The operator for a logical AND operation.
    /// </summary>
    public const string OperatorLogicalAnd = "AND";
    /// <summary>
    /// The operator for a logical OR operation.
    /// </summary>
    public const string OperatorLogicalOr = "OR";
    /// <summary>
    /// The operator for a logical NOT operation.
    /// </summary>
    public const string OperatorLogicalNot = "NOT";
    /// <summary>
    /// The (short) operator for a logical AND operation. (&amp;&amp;)
    /// </summary>
    public const string OperatorLogicalAndShort = "&&";
    /// <summary>
    /// The (short) operator for a logical OR operation. (||)
    /// </summary>
    public const string OperatorLogicalOrShort = "||";
    /// <summary>
    /// The (short) operator for a logical NOT operation. (!)
    /// </summary>
    public const string OperatorLogicalNotShort = "!";
    #endregion

    #region Increment/Decrement
    /// <summary>
    /// The increment by one operator.
    /// </summary>
    public const string OperatorIncrement = "++";
    /// <summary>
    /// The increment by one operator.
    /// </summary>
    public const string OperatorDecrement = "--";
    #endregion

}
