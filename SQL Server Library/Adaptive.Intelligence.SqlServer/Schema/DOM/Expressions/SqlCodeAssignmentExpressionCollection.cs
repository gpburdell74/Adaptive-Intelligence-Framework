namespace Adaptive.Intelligence.SqlServer.CodeDom
{
    /// <summary>
    /// Contains a list of <see cref="SqlCodeAssignmentExpression"/> instances.
    /// </summary>
    /// <seealso cref="List{T}" />
    public sealed class SqlCodeAssignmentExpressionCollection : List<SqlCodeAssignmentExpression>
    {
        /// <summary>
        /// Creates and adds the assignment expression to the collection with the specified values.
        /// </summary>
        /// <param name="assignedToExpression">
        /// A <see cref="SqlCodeExpression"/>-derived instance describing the item being assigned to.
        /// </param>
        /// <param name="valueExpression">
        /// A <see cref="SqlCodeExpression"/>-derived instance describing the value.
        /// </param>
        public void Add(SqlCodeExpression assignedToExpression, SqlCodeExpression valueExpression)
        {
            Add(new SqlCodeAssignmentExpression(assignedToExpression, valueExpression));
        }
    }
}
