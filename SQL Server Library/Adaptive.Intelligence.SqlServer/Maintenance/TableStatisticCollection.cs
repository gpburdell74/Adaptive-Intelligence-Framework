namespace Adaptive.Intelligence.SqlServer.Maintenance
{
    /// <summary>
    /// Contains a list of <see cref="TableStatistic"/> instances.
    /// </summary>
    /// <seealso cref="List{T}" />
    /// <seealso cref="TableStatistic"/>
    public sealed class TableStatisticCollection : List<TableStatistic>
    {
        #region Constructor        
        /// <summary>
        /// Initializes a new instance of the <see cref="TableStatisticCollection"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public TableStatisticCollection()
        {
        }
        #endregion

        #region Public Properties        
        /// <summary>
        /// Gets the <see cref="TableStatistic"/> with the specified name.
        /// </summary>
        /// <value>
        /// The <see cref="TableStatistic"/> instance with the specified name, or <b>null</b> if not
        /// present in the collection.
        /// </value>
        /// <param name="name">
        /// A string containing the name of the table to search for.
        /// </param>
        public TableStatistic? this[string name] => GetByName(name);
        #endregion

        #region Public Methods / Functions        
        /// <summary>
        /// Gets the table by its object ID value.
        /// </summary>
        /// <param name="objectId">
        /// An integer specifying the object ID value to search for.
        /// </param>
        /// <returns>
        /// The <see cref="TableStatistic"/> instance with the specified ID, or <b>null</b> if not
        /// present in the collection.
        /// </returns>
        public TableStatistic? GetByObjectId(int objectId)
        {
            return this.FirstOrDefault(x => x.ObjectId == objectId);
        }
        /// <summary>
        /// Gets the table by its specified name.
        /// </summary>
        /// <param name="tableName">
        /// A string containing the name of the table to search for.
        /// </param>
        /// <returns>
        /// The <see cref="TableStatistic"/> instance with the specified name, or <b>null</b> if not
        /// present in the collection.
        /// </returns>
        public TableStatistic? GetByName(string tableName)
        {
            return this.FirstOrDefault(x => x.Name == tableName);
        }
        /// <summary>
        /// Gets the list of indexes in the collection that need to be re-build based 
        /// on their last statistic information.
        /// </summary>
        /// <returns>
        /// A <see cref="List{T}"/> of <see cref="IndexStatisticsInfo"/> instances.
        /// </returns>
        public List<IndexStatisticsInfo> GetIndexesToRebuild()
        {
            List<IndexStatisticsInfo> list = new List<IndexStatisticsInfo>();

            foreach (TableStatistic table in this)
            {
                if (table.Indexes != null)
                {
                    List<IndexStatisticsInfo> subList = table.Indexes.GetIndexesToRebuild();
                    if (subList.Count > 0)
                    {
                        list.AddRange(subList);
                    }
                }
            }
            return list;
        }
        #endregion
    }
}
