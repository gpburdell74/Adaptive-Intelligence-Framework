namespace Adaptive.Intelligence.SqlServer.Analysis
{
    /// <summary>
    /// Contains and manages a list of <see cref="ReferencedTableJoin"/> instances.
    /// </summary>
    /// <seealso cref="List{T}" />
    public sealed class ReferencedTableJoinCollection : List<ReferencedTableJoin>
    {
        #region Constructors        
        /// <summary>
        /// Initializes a new instance of the <see cref="ReferencedTableJoinCollection"/> class.
        /// </summary>
        public ReferencedTableJoinCollection()
        {
        }
        #endregion

        #region Public Methods / Functions        
        /// <summary>
        /// Re-orders the content of the collection based on applicable INNER / LEFT join operations, and adds alias
        /// names for tables that might be used more than once.
        /// </summary>
        public void Organize()
        {
            // For SQL performance purposes, we need the INNER JOIN instances to come before the LEFT JOIN instances.
            //
            // Separate the lists...
            List<ReferencedTableJoin> innerList = (from items in this where !items.UsesLeftJoin select items).ToList();
            List<ReferencedTableJoin> leftList = (from items in this where items.UsesLeftJoin select items).ToList();

            // Re-add the lists.
            Clear();
            AddRange(innerList);
            AddRange(leftList);

            // Add table aliases, if needed.
            Dictionary<string, int> tableUseList = new Dictionary<string, int>();
            foreach (ReferencedTableJoin item in this)
            {
                if (item.ReferencedTable != null && item.ReferencedTable.TableName != null)
                {
                    // If not in the list, add it to the dictionary.
                    if (!tableUseList.ContainsKey(item.ReferencedTable.TableName))
                    {
                        tableUseList.Add(item.ReferencedTable.TableName, 1);
                    }
                    else
                    {
                        // We have seen this table before... Create a new alias and increment the numeric value.
                        int value = tableUseList[item.ReferencedTable.TableName];
                        tableUseList[item.ReferencedTable.TableName]++;
                        item.TableAlias = item.ReferencedTable.TableName + value.ToString();
                    }
                }
            }
        }
        #endregion
    }
}
