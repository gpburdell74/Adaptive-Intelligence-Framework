using Adaptive.Intelligence.Shared;
using Microsoft.Data.SqlClient;

namespace Adaptive.SqlServer.Client
{
    /// <summary>
    /// Provides the signature definition for a wrapper implementation for the <see cref="SqlDataReader"/> class.
    /// These reader style instances are used to simplify the reading of data fields.
    /// </summary>
    /// <remarks>
    /// Provides a way of reading a forward-only stream of rows from a SQL Server database.
    /// </remarks>
    public interface ISafeSqlDataReader : IRepositoryReader, IExceptionTracking
    {
        #region Properties
        /// <summary>
        /// Gets a value that indicates whether the <see cref="ISafeSqlDataReader"/>
        /// contains one or more rows.
        /// </summary>
        /// <value>
        /// <b>true</b> if the <see cref="ISafeSqlDataReader"/> contains one or more rows;
        /// otherwise <b>false</b>.
        /// </value>
        bool HasRows { get; }
        /// <summary>
        /// Gets the value of the specified column in its native format given the column name.
        /// </summary>
        /// <param name="name">
        /// A string containing the column name.
        /// </param>
        /// <returns>
        /// The value of the specified column in its native format, or <b>null</b>.
        /// </returns>
        object? this[string name] { get; }
        #endregion

        #region Methods / Functions
        /// <summary>
        /// Closes the <see cref="ISafeSqlDataReader"/> object.
        /// </summary>
        /// <remarks>
        /// You must explicitly call the <see cref="Close"/> method when you are through using
        /// the <see cref="ISafeSqlDataReader"/> to use the associated <see cref="SqlConnection"/>
        /// for any other purpose.
        ///
        /// The <b>Close</b> method disposes of the underlying
        /// <see cref="ISafeSqlDataReader"/> instance.
        /// </remarks>
        void Close();
        /// <summary>
        /// Gets the name of the column at the specified index.
        /// </summary>
        /// <param name="index">The ordinal index of the column.</param>
        /// <returns>
        /// The name of the column, or <b>null</b>.
        /// </returns>
        string? GetColumnName(int index);
        /// <summary>
        /// Gets the list of field names in sequential order.
        /// </summary>
        /// <returns>
        /// A <see cref="List{T}"/> of <see cref="string"/> containing the field names.
        /// </returns>
        List<string> GetFieldNames();
        /// <summary>
        /// Gets the value of the specified column as a string.
        /// </summary>
        /// <param name="index">
        /// The zero-based column ordinal value.
        /// </param>
        /// <returns>
        /// The value of the column as a string, or <b>null</b> if the
        /// operation fails.
        /// </returns>
        string? GetStringUnsafe(int index);
        /// <summary>
        /// Gets a value that indicates whether the column contains non-existent or
        /// missing values.
        /// </summary>
        /// <param name="index">
        /// The zero-based column ordinal.
        /// </param>
        /// <returns>
        /// <b>true</b> if the specified column value is equivalent to DBNull;
        /// otherwise <b>false</b>.
        /// </returns>
        bool IsDBNull(int index);
        /// <summary>
        /// Gets a value that indicates whether the column contains non-existent or
        /// missing values.
        /// </summary>
        /// <param name="columnName">
        /// The name of the column.
        /// </param>
        /// <returns>
        /// <b>true</b> if the specified column value is equivalent to DBNull;
        /// otherwise <b>false</b>.
        /// </returns>
        bool IsDBNull(string? columnName);
        /// <summary>
        ///  Advances the reader to the next result when reading the results of a batch of
        ///  statements.
        /// </summary>
        /// <returns>
        /// <b>true</b> if there are more result sets; otherwise, <b>false</b>.
        /// </returns>
        bool NextResult();
        /// <summary>
        /// Advances the <see cref="ISafeSqlDataReader"/> to the next record.
        /// </summary>
        /// <returns>
        /// <b>true</b> if there are more rows; otherwise <b>false</b>.
        /// </returns>
        bool Read();
        /// <summary>
        /// An asynchronous version of <see cref="Read"/>, which advances the reader to the next
        /// record in a result set. This method invokes ReadAsync
        /// </summary>
        /// <returns>
        /// <b>true</b> if there are more rows; otherwise <b>false</b>.
        /// </returns>
        Task<bool> ReadAsync();
        #endregion
    }
}
