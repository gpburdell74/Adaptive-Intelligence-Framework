namespace Adaptive.Intelligence.SqlServer.Maintenance
{
    /// <summary>
    /// Contains a list of <see cref="IndexStatisticsInfo"/> instances.
    /// </summary>
    /// <seealso cref="List{T}" />
    /// <seealso cref="IndexStatisticsInfo"/>
    public sealed class IndexStatisticsInfoCollection : List<IndexStatisticsInfo>
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="IndexStatisticsInfoCollection"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public IndexStatisticsInfoCollection()
        {
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the <see cref="IndexStatisticsInfo"/> with the specified name.
        /// </summary>
        /// <value>
        /// The <see cref="IndexStatisticsInfo"/> instance with the specified name, or <b>null</b> if not
        /// present in the collection.
        /// </value>
        /// <param name="name">
        /// A string containing the name of the index to search for.
        /// </param>
        public IndexStatisticsInfo? this[string name] => GetByName(name);
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Gets the index by its ordinal ID value.
        /// </summary>
        /// <param name="indexId">
        /// An integer specifying the index ID value to search for.
        /// </param>
        /// <returns>
        /// The <see cref="IndexStatisticsInfo"/> instance with the specified ID, or <b>null</b> if not
        /// present in the collection.
        /// </returns>
        public IndexStatisticsInfo? GetByIndexId(int indexId)
        {
            return this.FirstOrDefault(x => x.IndexId == indexId);
        }
        /// <summary>
        /// Gets the index by its specified name.
        /// </summary>
        /// <param name="indexName">
        /// A string containing the name of the index to search for.
        /// </param>
        /// <returns>
        /// The <see cref="IndexStatisticsInfo"/> instance with the specified name, or <b>null</b> if not
        /// present in the collection.
        /// </returns>
        public IndexStatisticsInfo? GetByName(string indexName)
        {
            return this.FirstOrDefault(x => x.Name == indexName);
        }
        /// <summary>
        /// Determines whether the table has indexes that need rebuilding.
        /// </summary>
        /// <returns>
        /// <b>true</b> if the table has indexes that need rebuilding; otherwise, returns <b>false</b>.
        /// </returns>
        public bool HasIndexesToRebuild()
        {
            int count = 0;
            foreach (IndexStatisticsInfo item in this)
            {
                if (item.NeedsToRebuild)
                    count++;
            }
            return (count > 0);
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

            foreach (IndexStatisticsInfo item in this)
            {
                if (item.NeedsToRebuild)
                    list.Add(item);
            }
            return list;
        }
        #endregion
    }
}
