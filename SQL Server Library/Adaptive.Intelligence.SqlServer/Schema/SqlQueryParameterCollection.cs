namespace Adaptive.Intelligence.SqlServer.Schema
{
    /// <summary>
    /// Contains a list of <see cref="SqlQueryParameter"/> instances.
    /// </summary>
    public sealed class SqlQueryParameterCollection : List<SqlQueryParameter>
    {
        /// <summary>
        /// Gets the parameter definition for the specified table column.
        /// </summary>
        /// <param name="columnName">
        /// A string containing the name of the column.</param>
        /// <returns>
        /// The <see cref="SqlQueryParameter"/> definition for the specified column, or <b>null</b>
        /// if not present.
        /// </returns>
        public SqlQueryParameter? GetParameterForColumn(string columnName)
        {
            return (from items in this
                where (items.ColumnName == columnName)
                select items).FirstOrDefault();
        }
    }
}
