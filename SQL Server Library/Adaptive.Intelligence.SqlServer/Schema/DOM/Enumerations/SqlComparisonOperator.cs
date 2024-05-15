namespace Adaptive.Intelligence.SqlServer.CodeDom
{
    /// <summary>
    /// Lists the SQL comparison operators that may be rendered.
    /// </summary>
    public enum SqlComparisonOperator
    {
        /// <summary>
        /// Indicates no operator was specified.
        /// </summary>
        NotSpecified = 0,
        /// <summary>
        /// Indicates the equal to ( = ) operator.
        /// </summary>
        EqualTo = 1,
        /// <summary>
        /// Indicates the not equal to ( != ) operator.
        /// </summary>
        NotEqualTo = 2,
        /// <summary>
        /// Indicates the less than ( &lt; ) operator.
        /// </summary>
        LessThan = 3,
        /// <summary>
        /// Indicates the less than or equal to ( &lt;= ) operator.
        /// </summary>
        LessThanOrEqualTo = 4,
        /// <summary>
        /// Indicates the greater than to ( &gt; ) operator.
        /// </summary>
        GreaterThan = 5,
        /// <summary>
        /// Indicates the greater than or equal to ( &gt;= ) operator.
        /// </summary>
        GreaterThanOrEqualTo = 6,
        /// <summary>
        /// Indicates the not ( NOT ) operator.
        /// </summary>
        Not = 7,
        /// <summary>
        /// Indicates the not in ( NOT IN ) operator.
        /// </summary>
        NotIn = 8,
        /// <summary>
        /// Indicates the is null ( IS NULL ) operator.
        /// </summary>
        IsNull = 9,
        /// <summary>
        /// Indicates the is not null ( IS NOT NULL ) operator.
        /// </summary>
        IsNotNull = 10
    }
}
