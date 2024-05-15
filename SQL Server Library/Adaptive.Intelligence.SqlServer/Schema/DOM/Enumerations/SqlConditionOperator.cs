namespace Adaptive.Intelligence.SqlServer.CodeDom
{
    /// <summary>
    /// Lists the SQL boolean comparison operators that may be rendered.
    /// </summary>
    public enum SqlConditionOperator
    {
        /// <summary>
        /// Indicates no operator was specified.
        /// </summary>
        NotSpecified = 0,
        /// <summary>
        /// Indicates the AND operator.
        /// </summary>
        And = 1,
        /// <summary>
        /// Indicates the OR operator.
        /// </summary>
        Or = 2
    }
}
