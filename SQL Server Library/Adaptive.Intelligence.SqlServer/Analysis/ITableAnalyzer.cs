using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.SqlServer.Schema;
using Adaptive.SqlServer.Client;

namespace Adaptive.Intelligence.SqlServer.Analysis
{
    /// <summary>
    /// Provides signature definition for implementing classes that analyze and store information for the provided 
    /// SQL table schemas that can be used to render SQL and other code.
    /// </summary>
    /// <typeparam name="T">
    /// The data type for the data being stored in the table.
    /// </typeparam>
    /// <seealso cref="DisposableObjectBase" />
    public interface ITableAnalyzer<T> where T : ITableProfileCollection
    {
        #region Methods / Functions         
        /// <summary>
        /// Analyzes the tables for query creation.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="SqlDataProvider"/> instance to use to communicate with the database.
        /// </param>
        void AnalyzeTablesForQueryCreation(SqlDataProvider provider);
        /// <summary>
        /// Analyzes the tables for query creation.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="SqlDataProvider"/> instance used to communicate with the database.
        /// </param>
        /// <param name="table">
        /// The <see cref="SqlTable"/> instance being analyzed.
        /// </param>
        void AnalyzeTableForQueryCreation(SqlDataProvider provider, SqlTable table);
        /// <summary>
        /// Loads the table profiles.
        /// </summary>
        /// <param name="tableList">
        /// A <see cref="SqlTableCollection"/> instance containing the list of tables.
        /// </param>
        /// <returns>
        /// An implementation of <typeparamref name="T"/> containing the list of profile definitions
        /// for each table in the provided <i>tableList</i>.
        /// </returns>
        T LoadTableProfiles(SqlTableCollection tableList);
        #endregion
    }
}