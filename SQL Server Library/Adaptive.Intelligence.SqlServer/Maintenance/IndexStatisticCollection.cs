namespace Adaptive.Intelligence.SqlServer.Maintenance
{
    /// <summary>
    /// Contains a list of <see cref="IndexStatistic"/> structures.
    /// </summary>
    /// <seealso cref="List{T}" />
    /// <seealso cref="IndexStatistic"/>
    public sealed class IndexStatisticCollection : List<IndexStatistic>
    {
        #region Constructor        
        /// <summary>
        /// Initializes a new instance of the <see cref="IndexStatisticCollection"/> class.
        /// </summary>
        public IndexStatisticCollection()
        {
        }
        #endregion

        #region Public Properties        
        /// <summary>
        /// Gets a value indicating whether the collection is empty.
        /// </summary>
        /// <remarks>
        /// This property should be checked before reading the <see cref="Last"/> property.
        /// </remarks>
        /// <value>
        ///   <b>true</b> if the collection is empty; otherwise, <b>false</b>.
        /// </value>
        public bool IsEmpty => Count == 0;
        /// <summary>
        /// Gets the reference to the last index statistic structure in the list.
        /// </summary>
        /// <value>
        /// The <see cref="IndexStatistic"/> in the list.
        /// </value>
        public IndexStatistic Last => this[Count - 1];
        #endregion
    }
}
