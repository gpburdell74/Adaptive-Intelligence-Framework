using Adaptive.Intelligence.SqlServer.Schema;

namespace Adaptive.Intelligence.SqlServer.UI
{
    /// <summary>
    /// Defines the arguments for events that use <see cref="SqlStoredProcedure"/> instances as event data.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public sealed class SqlStoredProcedureEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlStoredProcedureEventArgs"/> class.
        /// </summary>
        public SqlStoredProcedureEventArgs()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlStoredProcedureEventArgs"/> class.
        /// </summary>
        /// <param name="procedure">
        /// The <see cref="SqlStoredProcedure"/> instance being referenced in the event.
        /// </param>
        public SqlStoredProcedureEventArgs(SqlStoredProcedure? procedure)
        {
            Procedure = procedure;
        }
        /// <summary>
        /// Finalizes an instance of the <see cref="SqlStoredProcedureEventArgs"/> class.
        /// </summary>
        ~SqlStoredProcedureEventArgs()
        {
            Procedure = null;
        }

        /// <summary>
        /// Gets or sets the reference to the <see cref="SqlStoredProcedure"/> instance being referenced in the event.
        /// </summary>
        /// <value>
        /// The <see cref="SqlStoredProcedure"/> instance being referenced in the event, or <b>null</b>.
        /// </value>
        public SqlStoredProcedure? Procedure
        {
            get; set;
        }
    }
}