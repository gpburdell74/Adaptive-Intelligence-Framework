using Adaptive.Intelligence.SqlServer.Schema;

namespace Adaptive.Intelligence.SqlServer.Analysis
{
    /// <summary>
    /// Provides the signature definition for a collection implementation used to manage user-defined profiles for 
    /// specific SQL data tables in a business domain.
    /// </summary>
    /// <remarks>
    /// This interface may be implemented in a domain-specific class to provide information for the specific
    /// business domain rules for rendering SQL and code content for data tables.
    /// </remarks>
    public interface ITableProfileCollection
    {
        /// <summary>
        /// Attempts to load the local table profile file, if present, and populates the collection with 
        /// <see cref="ITableProfile"/> instances for each table in the provided collection, even if the local
        /// file does not contain an entry for each table.
        /// </summary>
        /// <param name="tableList">
        /// The <see cref="SqlTableCollection"/> containing the SQL table schema information for the tables
        /// being represented.
        /// </param>
        void CreateContentForTables(SqlTableCollection? tableList);
        /// <summary>
        /// Attempts to load the local table profile file, if present, and populates the collection with 
        /// <see cref="ITableProfile"/> instances for each table in the provided collection, even if the local
        /// file does not contain an entry for each table.
        /// </summary>
        /// <param name="tableList">
        /// The <see cref="SqlTableCollection"/> containing the SQL table schema information for the tables
        /// being represented.
        /// </param>
        Task CreateContentForTablesAsync(SqlTableCollection? tableList);
        /// <summary>
        /// Attempts to load the local table profile file.
        /// </summary>
        /// <returns>
        /// <b>true</b> if the load operation is successful; otherwise, returns <b>false</b>.
        /// </returns>
        bool Load();
        /// <summary>
        /// Attempts to load the local table profile file.
        /// </summary>
        /// <returns>
        /// <b>true</b> if the load operation is successful; otherwise, returns <b>false</b>.
        /// </returns>
        Task<bool> LoadAsync();
        /// <summary>
        /// Attempts to save the content of the collection to a local table profile file.
        /// </summary>
        /// <returns>
        /// <b>true</b> if the save operation is successful; otherwise, returns <b>false</b>.
        /// </returns>
        bool Save();
        /// <summary>
        /// Attempts to save the content of the collection to a local table profile file.
        /// </summary>
        /// <returns>
        /// <b>true</b> if the save operation is successful; otherwise, returns <b>false</b>.
        /// </returns>
        Task<bool> SaveAsync();
    }
}
