namespace Adaptive.Intelligence.SqlServer.Schema
{
    /// <summary>
    /// Contains a list of <see cref="SqlStoredProcedure"/> instances.
    /// </summary>
    public sealed class SqlStoredProcedureCollection : List<SqlStoredProcedure>
    {
        #region Destructor
        /// <summary>
        /// Finalizes an instance of the <see cref="SqlStoredProcedureCollection"/> class.
        /// </summary>
        ~SqlStoredProcedureCollection()
        {
            RelatedTable = null;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the reference to a related table.
        /// </summary>
        /// <value>
        /// The <see cref="SqlTable"/> instance the procedures in the list are related to, or 
        /// <b>null</b>.
        /// </value>
        public SqlTable? RelatedTable { get; set; }
        #endregion

        #region Public Methods / Functions        
        /// <summary>
        /// Gets the reference to the stored procedure by its function.
        /// </summary>
        /// <param name="function">
        /// A string containing the part of the stored procedure name that describes its function,
        /// such as: Insert, Update, Delete, GetAll, GetById, or GetForCustomer.
        /// </param>
        /// <returns>
        /// The first matching <see cref="SqlStoredProcedure"/> instance that was found, or <b>null</b>.
        /// </returns>
        public SqlStoredProcedure? GetItemByFunction(string function)
        {
            SqlStoredProcedure? proc;
            if (RelatedTable != null)
            {
                string name = RelatedTable.TableName + function;
                proc =
                (from items in this where items.Name.Contains(name) select items).FirstOrDefault();
            }
            else
            {
                proc = (from items in this where items.Name.Contains(function) select items).FirstOrDefault();
            }

            return proc;
        }
        #endregion
    }
}
