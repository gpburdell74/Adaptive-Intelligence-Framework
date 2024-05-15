using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.SqlServer.Maintenance
{
    /// <summary>
    /// Contains summary information about index fragmentation.
    /// </summary>
    /// <seealso cref="DisposableObjectBase" />
    public sealed class SummaryIndexRecord : DisposableObjectBase
    {
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <b>true</b> to release both managed and unmanaged resources;
        /// <b>false</b> to release only unmanaged resources.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            AverageFragmentationPercent = 0;
            IndexName = null;
            TableName = null;
            base.Dispose(disposing);
        }
        /// <summary>
        /// Gets or sets the average fragmentation percent.
        /// </summary>
        /// <value>
        /// A <see cref="float"/> specifying the average fragmentation percent.
        /// </value>
        public float AverageFragmentationPercent { get; set; }
        /// <summary>
        /// Gets or sets the fragment count.
        /// </summary>
        /// <value>
        /// An integer specifying the number of fragments.
        /// </value>
        public int FragmentCount { get; set; }
        /// <summary>
        /// Gets or sets the name of the index.
        /// </summary>
        /// <value>
        /// A string specifying the name of the index.
        /// </value>
        public string? IndexName { get; set; }
        /// <summary>
        /// Gets or sets the name of the table.
        /// </summary>
        /// <value>
        /// A string specifying the name of the table.
        /// </value>
        public string? TableName { get; set; }
    }
}
