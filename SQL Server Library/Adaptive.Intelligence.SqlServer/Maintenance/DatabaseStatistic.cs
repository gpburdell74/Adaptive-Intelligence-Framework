using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.SqlServer.Maintenance
{
    /// <summary>
    /// Provides a basic SQL Server database information container used in maintenance operations
    /// and used to store statistics on the indexes of the tables in the database.
    /// </summary>
    /// <seealso cref="DisposableObjectBase"/>
    /// <seealso cref="TableStatistic"/>
    /// <seealso cref="IndexStatistic"/>
    /// <seealso cref="IndexStatisticsInfo"/>
    public sealed class DatabaseStatistic : DisposableObjectBase
    {
        #region Private Member Declarations
        /// <summary>
        /// The list of tables.
        /// </summary>
        private TableStatisticCollection? _tables;
        #endregion

        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseStatistic"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public DatabaseStatistic()
        {
            _tables = new TableStatisticCollection();
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
        /// <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed && disposing)
            {
                _tables?.Clear();
            }
            _tables = null;
            Name = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the references to the list of tables.
        /// </summary>
        /// <value>
        /// The <see cref="TableStatisticCollection"/> instance.
        /// </value>
        public TableStatisticCollection? Tables => _tables;
        /// <summary>
        /// Gets or sets the object ID value for the database.
        /// </summary>
        /// <remarks>
        /// This correlates to the DB_ID() function result.
        /// </remarks>
        /// <value>
        /// An integer indicating the object ID value for the database.
        /// </value>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the name of the database.
        /// </summary>
        /// <remarks>
        /// This correlates to the DB_NAME(DB_ID()) function result.
        /// </remarks>
        /// <value>
        /// A string containing the name of the database.
        /// </value>
        public string? Name { get; set; }
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Adds the table to the database statistics listing.
        /// </summary>
        /// <param name="table">
        /// The <see cref="TableStatistic"/> instance to be added.
        /// </param>
        public void AddTable(TableStatistic table)
        {
            _tables?.Add(table);
        }
        /// <summary>
        /// Adds the supplied index information stats to the table with the specified object ID value.
        /// </summary>
        /// <param name="tableId">
        /// An integer containing the ID of the table.
        /// </param>
        /// <param name="newIndex">
        /// The <see cref="IndexStatisticsInfo"/> instance containing the index statistics.
        /// </param>
        public void AddIndexToTable(int tableId, IndexStatisticsInfo newIndex)
        {
            TableStatistic? table = _tables?.GetByObjectId(tableId);
            if (table != null)
            {
                table.AddIndex(newIndex);
            }
        }
        /// <summary>
        /// Gets the list of names of the table that need index rebuilding.
        /// </summary>
        public List<string> GetTablesForIndexRebuild()
        {
            List<string> list = new List<string>();

            if (_tables != null)
            {
                foreach (TableStatistic table in _tables)
                {
                    if (table.HasIndexesToRebuild() && table.Name != null)
                    {
                        list.Add(table.Name);
                    }
                }
            }
            return list;
        }
        #endregion
    }
}
