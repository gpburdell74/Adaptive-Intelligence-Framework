namespace Adaptive.Intelligence.SqlServer.Maintenance
{
    /// <summary>
    /// Provides a data container for representing and recording statistics for a SQL Index
    /// on a SQL Server table.
    /// </summary>
    public struct IndexStatistic
    {
        /// <summary>
        /// Gets or sets the average fragmentation percent.
        /// </summary>
        /// <remarks>
        /// This corresponds to the avg_fragmentation_in_percent column returned from the
        /// sys.dm_db_index_physical_stats function.
        ///
        /// This value indicates the logical fragmentation for indexes, or extent fragmentation for
        /// heaps in the IN_ROW_DATA allocation unit.  The value is measured as a percentage and takes
        /// into account multiple files.
        /// </remarks>
        /// <value>
        /// A <see cref="double"/> value containing the average fragmentation percent for the
        /// related index.
        /// This is <b>float</b> in SQL.
        /// </value>
        public double AverageFragmentationPercent { get; set; }
        /// <summary>
        /// Gets or sets the ID of the containing database object.
        /// </summary>
        /// <remarks>
        /// This corresponds to the database_id column returned from the
        /// sys.dm_db_index_physical_stats function.
        /// </remarks>
        /// <value>
        /// A <see cref="short"/> integer specifying the ID of the containing database object.
        /// This is <b>smallint</b> in SQL.
        /// </value>
        public short DatabaseId { get; set; }
        /// <summary>
        /// Gets or sets the number of fragments in the leaf level of an IN_ROW_DATA allocation unit.
        /// </summary>
        /// <remarks>
        /// This corresponds to the fragment_count column returned from the
        /// sys.dm_db_index_physical_stats function.
        /// </remarks>
        /// <value>
        /// A <see cref="long"/> integer specifying the number of fragments the parent index is split into.
        /// This is <b>bigint</b> in SQL.
        /// </value>
        public long FragmentCount { get; set; }
        /// <summary>
        /// Gets or sets the index ID value.
        /// </summary>
        /// <remarks>
        /// This corresponds to the index_id column returned from the
        /// sys.dm_db_index_physical_stats function.
        /// </remarks>
        /// <value>
        /// An <see cref="int"/> specifying the table-scoped, ordinal ID of the parent index.
        /// This is <b>int</b> in SQL.
        /// </value>
        public int IndexId { get; set; }
        /// <summary>
        /// Gets or sets the ID of the parent object of the index, usually a table.
        /// </summary>
        /// <remarks>
        /// This corresponds to the object_id column returned from the
        /// sys.dm_db_index_physical_stats function.
        /// </remarks>
        /// <value>
        /// An <see cref="int"/> specifying the ID of the parent table the index is defined on.
        /// This is <b>int</b> in SQL.
        /// </value>
        public int ObjectId { get; set; }
        /// <summary>
        /// Gets or sets the total number of index or data pages.
        /// </summary>
        /// <remarks>
        /// This corresponds to the page_count column returned from the
        /// sys.dm_db_index_physical_stats function.
        ///
        /// For an index, the total number of index pages in the current level of the
        /// b-tree in the IN_ROW_DATA allocation unit.
        ///
        /// For a heap, the total number of data pages in the IN_ROW_DATA allocation unit.
        ///
        /// For LOB_DATA or ROW_OVERFLOW_DATA allocation units, total number of pages in the
        /// allocation unit.
        /// </remarks>
        /// <value>
        /// A <see cref="long"/> integer specifying the total number of index or data pages.
        /// This is <b>bigint</b> in SQL.
        /// </value>
        public long PageCount { get; set; }
    }
}
