using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.SqlServer.Maintenance
{
    /// <summary>
    /// Provides a basic SQL Table information container used in maintenance operations
    /// and used to store statistics on the indexes of the table.
    /// </summary>
    /// <seealso cref="DisposableObjectBase"/>
    /// <seealso cref="IndexStatisticsInfo"/>
    /// <seealso cref="IndexStatistic"/>
    public sealed class TableStatistic : DisposableObjectBase
    {
        #region Private Member Declarations        
        /// <summary>
        /// The indexes for the table.
        /// </summary>
        private IndexStatisticsInfoCollection? _indexes;
        #endregion

        #region Constructor / Dispose Methods        
        /// <summary>
        /// Initializes a new instance of the <see cref="TableStatistic"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public TableStatistic()
        {
            _indexes = new IndexStatisticsInfoCollection();
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
                _indexes?.Clear();
            }

            _indexes = null;
            Name = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties        
        /// <summary>
        /// Gets the references to the list of indexes for the table.
        /// </summary>
        /// <value>
        /// The <see cref="IndexStatisticsInfoCollection"/> instance.
        /// </value>
        public IndexStatisticsInfoCollection? Indexes => _indexes;
        /// <summary>
        /// Gets or sets the object ID value for the table.
        /// </summary>
        /// <remarks>
        /// This correlates to the [sys].[tables].[object_id] column.
        /// </remarks>
        /// <value>
        /// An integer indicating the object ID value for the table.
        /// </value>
        public int ObjectId { get; set; }
        /// <summary>
        /// Gets or sets the name of the table.
        /// </summary>
        /// <remarks>
        /// This correlates to the [sys].[tables].[name] column.
        /// </remarks>
        /// <value>
        /// A string containing the name of the table.
        /// </value>
        public string? Name { get; set; }
        #endregion

        #region Public Methods / Functions        
        /// <summary>
        /// Adds the index to the table index listing.
        /// </summary>
        /// <param name="index">
        /// The <see cref="IndexStatisticsInfo"/> instance to be added.
        /// </param>
        public void AddIndex(IndexStatisticsInfo index)
        {
            if (_indexes != null)
                _indexes.Add(index);
        }
        /// <summary>
        /// Determines whether the table has indexes that need rebuilding.
        /// </summary>
        /// <returns>
        /// <b>true</b> if the table has indexes that need rebuilding; otherwise, returns <b>false</b>.
        /// </returns>
        public bool HasIndexesToRebuild()
        {
            if (_indexes == null)
                return false;

            return _indexes.HasIndexesToRebuild();
        }
        #endregion
    }
}
