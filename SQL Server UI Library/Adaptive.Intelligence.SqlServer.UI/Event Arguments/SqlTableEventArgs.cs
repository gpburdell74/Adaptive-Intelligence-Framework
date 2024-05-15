using Adaptive.Intelligence.SqlServer.Schema;

namespace Adaptive.Intelligence.SqlServer.UI
{
    /// <summary>
    /// Defines the arguments for events that use <see cref="SqlTable"/> instances as event data.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public sealed class SqlTableEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlTableEventArgs"/> class.
        /// </summary>
        public SqlTableEventArgs()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlTableEventArgs"/> class.
        /// </summary>
        /// <param name="table">
        /// The <see cref="SqlTable"/> instance being referenced in the event.
        /// </param>
        public SqlTableEventArgs(SqlTable? table)
        {
            Table = table;
        }
        /// <summary>
        /// Finalizes an instance of the <see cref="SqlTableEventArgs"/> class.
        /// </summary>
        ~SqlTableEventArgs()
        {
            Table = null;
        }

        /// <summary>
        /// Gets or sets the reference to the <see cref="SqlTable"/> instance being referenced in the event.
        /// </summary>
        /// <value>
        /// The <see cref="SqlTable"/> instance being referenced in the event, or <b>null</b>.
        /// </value>
        public SqlTable? Table
        {
            get; set;
        }
    }
}
