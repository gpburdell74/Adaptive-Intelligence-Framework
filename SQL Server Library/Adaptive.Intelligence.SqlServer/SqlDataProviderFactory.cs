using Microsoft.Data.SqlClient;

namespace Adaptive.SqlServer.Client
{
    /// <summary>
    /// Provides static methods and functions for creating <see cref="SqlDataProvider"/> instances.
    /// </summary>
    public static class SqlDataProviderFactory
    {
        /// <summary>
        /// Creates the SQL data provider instance to use the specified connection string.
        /// </summary>
        /// <param name="connectionString">
        /// A string containing the SQL Server connection parameters.
        /// </param>
        /// <returns>
        /// The <see cref="SqlDataProvider"/> instance.
        /// </returns>
        public static SqlDataProvider CreateProvider(string connectionString)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
            SqlDataProvider provider = CreateProvider(builder);
            builder.Clear();
            return provider;
        }
        /// <summary>
        /// Creates the SQL data provider instance to use the specified connection string.
        /// </summary>
        /// <param name="connectionString">
        /// A <see cref="SqlDataProvider"/> containing the SQL Server connection parameters.
        /// </param>
        /// <returns>
        /// The <see cref="SqlDataProvider"/> instance.
        /// </returns>
        public static SqlDataProvider CreateProvider(SqlConnectionStringBuilder connectionString)
        {
            return new SqlDataProvider(connectionString);
        }


    }
}
