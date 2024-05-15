using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.SqlServer.Maintenance
{
    /// <summary>
    /// Provides a basic SQL Index information container used in maintenance operations
    /// and used to store statistics on the index.
    /// </summary>
    /// <seealso cref="DisposableObjectBase" />
    public sealed class IndexStatisticsInfo : DisposableObjectBase
    {
        #region Private Member Declarations        
        /// <summary>
        /// The list of statistics for each pass.
        /// </summary>
        private IndexStatisticCollection? _statistics;
        #endregion

        #region Constructor / Dispose Methods        
        /// <summary>
        /// Initializes a new instance of the <see cref="IndexStatisticsInfo"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public IndexStatisticsInfo()
        {
            _statistics = new IndexStatisticCollection();
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
                _statistics?.Clear();
            }

            _statistics = null;
            Name = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Meta-data Properties        
        /// <summary>
        /// Gets or sets the ordinal ID value of the index.
        /// </summary>
        /// <remarks>
        /// This value correlates to the [sys].[indexes].[index_id] column.
        /// </remarks>
        /// <value>
        /// An integer specifying the ordinal ID value of the index.
        /// </value>
        public int IndexId { get; set; }
        /// <summary>
        /// Gets or sets the object ID value of the parent table.
        /// </summary>
        /// <remarks>
        /// This value correlates to the [sys].[indexes].[object_id] column.
        /// </remarks>
        /// <value>
        /// A byte value specifying the object ID value of the parent table.
        /// </value>
        public byte IndexType { get; set; }
        /// <summary>
        /// Gets the reference to the last statistic entry added for the index.
        /// </summary>
        /// <value>
        /// The reference to the last <see cref="IndexStatistic"/> recorded for the index.
        /// </value>
        public IndexStatistic LastStatistic
        {
            get
            {
                if (_statistics == null || _statistics.Count == 0)
                    return new IndexStatistic();
                else
                    return _statistics.Last;
            }
        }
        /// <summary>
        /// Gets or sets the name of the index.
        /// </summary>
        /// <remarks>
        /// This value correlates to the [sys].[indexes].[name] column.
        /// </remarks>
        /// <value>
        /// A string containing the name of the index.
        /// </value>
        public string? Name { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the index is the primary key for the table.
        /// </summary>
        /// <remarks>
        /// This value correlates to the [sys].[indexes].[is_primary_key] column.
        /// </remarks>
        /// <value>
        ///   <b>true</b> the index is the primary key for the table; otherwise, <b>false</b>.
        /// </value>
        public bool PrimaryKey { get; set; }
        /// <summary>
        /// Gets the reference to the list of statistics for the index.
        /// </summary>
        /// <value>
        /// The <see cref="List{T}"/> of <see cref="IndexStatistic"/> instances for the index.
        /// </value>
        public List<IndexStatistic>? Statistics => _statistics;
        /// <summary>
        /// Gets or sets the type of the index.
        /// </summary>
        /// <remarks>
        /// This value correlates to the [sys].[indexes].[type] column.
        /// </remarks>
        /// <value>
        /// An integer specifying the type of index.
        /// </value>
        public int TableId { get; set; }
        #endregion

        #region Public Processing Properties        
        /// <summary>
        /// Gets a value indicating whether the index needs to be rebuilt.
        /// </summary>
        /// <value>
        ///   <b>true</b> if the index needs to be rebuilt; otherwise, <b>false</b>.
        /// </value>
        public bool NeedsToRebuild
        {
            get
            {
                if (_statistics == null || _statistics.Count == 0)
                    return false;
                else
                {
                    IndexStatistic stat = LastStatistic;
                    return (stat.PageCount > 1000 &&
                           (stat.FragmentCount > 4 || stat.AverageFragmentationPercent > 30));
                }
            }
        }
        #endregion
    }
}