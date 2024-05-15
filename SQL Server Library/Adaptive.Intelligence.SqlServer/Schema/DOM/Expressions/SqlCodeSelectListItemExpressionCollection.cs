namespace Adaptive.Intelligence.SqlServer.CodeDom
{
    /// <summary>
    /// Contains a list of <see cref="SqlCodeSelectListItemExpression"/> instances.
    /// </summary>
    /// <seealso cref="SqlCodeSelectListItemExpression" />
    public sealed class SqlCodeSelectListItemExpressionCollection : List<SqlCodeSelectListItemExpression>
    {
        /// <summary>
        /// Adds the SQL Expression to the list of items to be selected.
        /// </summary>
        /// <param name="selectItemExpression">
        /// A <see cref="SqlCodeExpression"/> representing the item to be selected.
        /// </param>
        public void AddExpression(SqlCodeExpression? selectItemExpression)
        {
            if (selectItemExpression != null)
                Add(new SqlCodeSelectListItemExpression(selectItemExpression));
        }
        /// <summary>
        /// Adds the <see cref="SqlCodeTableColumnReferenceExpression"/> expression to the list of items to be selected.
        /// </summary>
        /// <param name="tableName">A string specifying the name of the table.</param>
        /// <param name="columnName">A string specifying the name of the column.</param>
        public void AddExpression(string? tableName, string? columnName)
        {
            if (tableName != null && columnName != null) 
                Add(new SqlCodeSelectListItemExpression(new SqlCodeTableColumnReferenceExpression(tableName, columnName)));
        }
        /// <summary>
        /// Adds the select all expression to the collection.
        /// </summary>
        /// <remarks>
        /// This method will remove all other expressions from the collection.
        /// </remarks>
        public void AddSelectAllExpression()
        {
            Clear();
            Add(new SqlCodeSelectListItemExpression(new SqlCodeLiteralExpression(TSqlConstants.Wildcard)));
        }
        /// <summary>
        /// Adds the select row unique identifier expression to the collection.
        /// </summary>
        public void AddSelectRowGuidExpression()
        {
            Add(new SqlCodeSelectListItemExpression(new SqlCodeLiteralExpression(TSqlConstants.SqlRowGuid)));
        }
        /// <summary>
        /// Finds the  index of the last expression in the list that is not whitespace or a comment.
        /// </summary>
        /// <returns>
        /// An integer indicating the index value within the collection, or -1 if no items in the collection
        /// are non-whitespace items or the collection is empty.
        /// </returns>
        public int FindLastNonWhiteSpaceItem()
        {
            int index = -1;
            int pos = Count - 1;

            while (index == -1 && pos > 0)
            {
                SqlCodeSelectListItemExpression item = this[pos];

                switch (item.Expression)
                {
                    case SqlCodeLiteralExpression literalExpression:
                        if (!string.IsNullOrEmpty(literalExpression.Expression))
                            index = pos;
                        break;

                    default:
                        index = pos;
                        break;
                }
                pos--;
            }
            return index;
        }
    }
}
