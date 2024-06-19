using Adaptive.Intelligence.Shared;
using Adaptive.SqlServer.Client;

namespace Adaptive.Intelligence.SqlServer.Schema
{
    /// <summary>
    /// Provides the data access methods for retrieving schema information from a SQL Server database.
    /// </summary>
    /// <seealso cref="DataAccessBase" />
    public sealed class SqlSchemaDataAccess : SqlDataAccessBase
    {
        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlSchemaDataAccess"/> class.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string used to connect to SQL Server.
        /// </param>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public SqlSchemaDataAccess(string connectionString) : base(connectionString)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlSchemaDataAccess"/> class.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="SqlDataProvider"/> instance to be used for SQL queries.
        /// </param>
        public SqlSchemaDataAccess(SqlDataProvider provider) : base(provider)
        {
        }
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Gets the list of foreign key definitions from the database.
        /// </summary>
        /// <returns>
        /// A <see cref="SqlForeignKeyCollection"/> containing the foreign key definition instances.
        /// </returns>
        public SqlForeignKeyCollection? GetForeignKeys()
        {
            SqlForeignKeyCollection? list = null;

            ISafeSqlDataReader? reader = GetReaderForParameterizedCommandText(SchemaQueryConstants.GetForeignKeysSql, 
                null);
            if (reader != null)
            {
                list = new SqlForeignKeyCollection();
                while (reader.Read())
                {
                    list.Add(new SqlForeignKey(reader));
                }
                reader.Dispose();
            }

            return list;
        }
        /// <summary>
        /// Gets the list of foreign key definitions from the database.
        /// </summary>
        /// <returns>
        /// A <see cref="SqlForeignKeyCollection"/> containing the foreign key definition instances.
        /// </returns>
        public async Task<SqlForeignKeyCollection?> GetForeignKeysAsync()
        {
            SqlForeignKeyCollection? list = null;

            ISafeSqlDataReader? reader = await GetReaderForParameterizedCommandTextAsync(SchemaQueryConstants.GetForeignKeysSql, 
                null);
            if (reader != null)
            {
                list = new SqlForeignKeyCollection();
                while (reader.Read())
                {
                    list.Add(new SqlForeignKey(reader));
                }
                reader.Dispose();
            }

            return list;
        }
        /// <summary>
        /// Gets the list of table index definitions from the database.
        /// </summary>
        /// <returns>
        /// A <see cref="SqlIndexCollection"/> containing the index definition instances.
        /// </returns>
        public SqlIndexCollection? GetIndexes()
        {
            SqlIndexCollection? list = null;

            ISafeSqlDataReader? reader = GetReaderForParameterizedCommandText(SchemaQueryConstants.GetIndexesSql, null);
            if (reader != null)
            {
                list = new SqlIndexCollection();
                while (reader.Read())
                {
                    list.Add(new SqlIndex(reader));
                }
                reader.Dispose();
            }

            return list;
        }
        /// <summary>
        /// Gets the list of table index definitions from the database.
        /// </summary>
        /// <returns>
        /// A <see cref="SqlIndexCollection"/> containing the index definition instances.
        /// </returns>
        public async Task<SqlIndexCollection?> GetIndexesAsync()
        {
            SqlIndexCollection? list = null;

            ISafeSqlDataReader? reader = await GetReaderForParameterizedCommandTextAsync(SchemaQueryConstants.GetIndexesSql, null);
            if (reader != null)
            {
                list = new SqlIndexCollection();
                while (reader.Read())
                {
                    list.Add(new SqlIndex(reader));
                }
                reader.Dispose();
            }

            return list;
        }
        /// <summary>
        /// Gets the list of stored procedures from the database.
        /// </summary>
        /// <returns>
        /// A <see cref="SqlStoredProcedureCollection"/> containing the list.
        /// </returns>
        public SqlStoredProcedureCollection? GetProcedures()
        {
            SqlStoredProcedureCollection? list = null;

            ISafeSqlDataReader? reader = GetReaderForParameterizedCommandText(SchemaQueryConstants.GetProceduresSql, null);
            if (reader != null)
            {
                list = new SqlStoredProcedureCollection();
                while (reader.Read())
                {
                    list.Add(new SqlStoredProcedure(reader));
                }
                reader.Dispose();
            }

            return list;
        }
        /// <summary>
        /// Gets the list of stored procedures from the database.
        /// </summary>
        /// <returns>
        /// A <see cref="SqlStoredProcedureCollection"/> containing the list.
        /// </returns>
        public async Task<SqlStoredProcedureCollection?> GetProceduresAsync()
        {
            SqlStoredProcedureCollection? list = null;

            ISafeSqlDataReader? reader = await GetReaderForParameterizedCommandTextAsync(SchemaQueryConstants.GetProceduresSql, null);
            if (reader != null)
            {
                list = new SqlStoredProcedureCollection();
                while (reader.Read())
                {
                    list.Add(new SqlStoredProcedure(reader));
                }
                reader.Dispose();
            }

            return list;

        }
        /// <summary>
        /// Gets the stored procedure definition by name.
        /// </summary>
        /// <param name="procedureName">
        /// A string containing the name of the stored procedure to retrieve.
        /// </param>
        /// <returns>
        /// A <see cref="SqlStoredProcedure"/> instance containing the definition, or
        /// <b>null</b> if not found.
        /// </returns>
        public SqlStoredProcedure? GetProcedureByName(string procedureName)
        {
            SqlStoredProcedure? procedure = null;

            ISafeSqlDataReader? reader = GetReaderForParameterizedCommandText(SchemaQueryConstants.GetProcedureByNameSql,
                CreateParameterList(SqlParam.ProcedureName, procedureName));

            if (reader != null)
            {
                if (reader.Read())
                {
                    procedure = new SqlStoredProcedure(reader);
                }
                reader.Dispose();
            }

            return procedure;
        }
        /// <summary>
        /// Gets the stored procedure definition by name.
        /// </summary>
        /// <param name="procedureName">
        /// A string containing the name of the stored procedure to retrieve.
        /// </param>
        /// <returns>
        /// A <see cref="SqlStoredProcedure"/> instance containing the definition, or
        /// <b>null</b> if not found.
        /// </returns>
        public async Task<SqlStoredProcedure?> GetProcedureByNameAsync(string procedureName)
        {
            SqlStoredProcedure? procedure = null;

            ISafeSqlDataReader? reader = await GetReaderForParameterizedCommandTextAsync(SchemaQueryConstants.GetProcedureByNameSql,
                CreateParameterList(SqlParam.ProcedureName, procedureName));
            if (reader != null)
            {
                while (reader.Read())
                {
                    procedure = new SqlStoredProcedure(reader);
                }
                reader.Dispose();
            }

            return procedure;
        }
        /// <summary>
        /// Gets the list of stored procedures that are named after the specified table.
        /// </summary>
        /// <param name="tableName">
        /// A string containing the name of the table.
        /// </param>
        /// <returns>
        /// A <see cref="SqlStoredProcedureCollection"/> instance containing the list.
        /// </returns>
        public SqlStoredProcedureCollection? GetStandardProceduresForTable(string tableName)
        {
            SqlStoredProcedureCollection? list = null;

            ISafeSqlDataReader? reader = GetReaderForParameterizedCommandText(SchemaQueryConstants.GetProceduresByPartialNameSql,
                CreateParameterList(SqlParam.TableName, tableName));
            if (reader != null)
            {
                list = new SqlStoredProcedureCollection();
                while (reader.Read())
                {
                    list.Add(new SqlStoredProcedure(reader));
                }
                reader.Dispose();
            }

            return list;
        }
        /// <summary>
        /// Gets the list of stored procedures that are named after the specified table.
        /// </summary>
        /// <param name="tableName">
        /// A string containing the name of the table.
        /// </param>
        /// <returns>
        /// A <see cref="SqlStoredProcedureCollection"/> instance containing the list.
        /// </returns>
        public async Task<SqlStoredProcedureCollection?> GetStandardProceduresForTableAsync(string tableName)
        {
            SqlStoredProcedureCollection? list = null;

            ISafeSqlDataReader? reader = await GetReaderForParameterizedCommandTextAsync(SchemaQueryConstants.GetProceduresByPartialNameSql,
                CreateParameterList(SqlParam.TableName, tableName));
            if (reader != null)
            {
                list = new SqlStoredProcedureCollection();
                while (reader.Read())
                {
                    list.Add(new SqlStoredProcedure(reader));
                }
                reader.Dispose();
            }

            return list;
        }
        /// <summary>
        /// Gets the list of SQL data type definitions from the database.
        /// </summary>
        /// <returns>
        /// A <see cref="SqlDataTypeCollection"/> containing the SQL data type definition instances.
        /// </returns>
        public SqlDataTypeCollection? GetSqlDataTypes()
        {
            SqlDataTypeCollection? list = null;

            ISafeSqlDataReader? reader = GetReaderForParameterizedCommandText(SchemaQueryConstants.GetSqlDataTypesSql, null);
            if (reader != null)
            {
                list = new SqlDataTypeCollection();
                while (reader.Read())
                {
                    list.Add(new SqlDataType(reader));
                }
                reader.Dispose();
            }

            return list;
        }
        /// <summary>
        /// Gets the list of SQL data type definitions from the database.
        /// </summary>
        /// <returns>
        /// A <see cref="SqlDataTypeCollection"/> containing the SQL data type definition instances.
        /// </returns>
        public async Task<SqlDataTypeCollection?> GetSqlDataTypesAsync()
        {
            SqlDataTypeCollection? list = null;

            ISafeSqlDataReader? reader = await GetReaderForParameterizedCommandTextAsync(SchemaQueryConstants.GetSqlDataTypesSql, null);
            if (reader != null)
            {
                list = new SqlDataTypeCollection();
                while (reader.Read())
                {
                    list.Add(new SqlDataType(reader));
                }
                reader.Dispose();
            }

            return list;
        }
        /// <summary>
        /// Gets the list of SQL table definitions from the database.
        /// </summary>
        /// <returns>
        /// A <see cref="SqlTableCollection"/> containing the table instances with columns.
        /// </returns>
        public SqlTableCollection? GetTables()
        {
            SqlTableCollection? list = null;

            ISafeSqlDataReader? reader = GetReaderForParameterizedCommandText(SchemaQueryConstants.GetTablesAndColumnsSql, null);
            if (reader != null)
            {
                list = new SqlTableCollection();
                while (reader.Read())
                {
                    // Read the raw data.
                    SqlTableColumnRecord record = new SqlTableColumnRecord();
                    int index = 0;
                    record.TableObjectId = reader.GetInt32(index++);
                    record.TableName = reader.GetString(index++);
                    record.ColumnId = reader.GetInt32(index++);
                    record.ColumnName = reader.GetString(index++);
                    record.TypeId = reader.GetInt32(index++);
                    record.MaxLength = reader.GetInt16(index++);
                    record.Precision = reader.GetByte(index++);
                    record.Scale = reader.GetByte(index++);
                    record.IsNullable = reader.GetBoolean(index++);
                    record.IsIdentity = reader.GetBoolean(index++);
                    record.IsComputed = reader.GetBoolean(index++);
                    record.IsPadded = reader.GetBoolean(index);

                    // Find the table object, or create it if needed.
                    SqlTable? table = list[record.TableName!];
                    if (table == null)
                    {
                        table = new SqlTable
                        {
                            TableName = record.TableName,
                            TableObjectId = record.TableObjectId
                        };
                        list.Add(table);
                    }

                    // Create and add the column.
                    SqlColumn newColumn = new SqlColumn
                    {
                        ColumnId = record.ColumnId,
                        ColumnName = record.ColumnName,
                        IsComputed = record.IsComputed,
                        IsIdentity = record.IsIdentity,
                        IsNullable = record.IsNullable,
                        MaxLength = record.MaxLength,
                        Precision = record.Precision,
                        Scale = record.Scale,
                        TypeId = record.TypeId,
                        IsAnsiPadded = record.IsPadded
                    };
                    if (table.Columns != null)
                        table.Columns.Add(newColumn);
                }
                reader.Dispose();
            }

            return list;
        }
        /// <summary>
        /// Gets the list of SQL table definitions from the database.
        /// </summary>
        /// <returns>
        /// A <see cref="SqlTableCollection"/> containing the table instances with columns.
        /// </returns>
        public async Task<SqlTableCollection?> GetTablesAsync()
        {
            SqlTableCollection? list = null;

            ISafeSqlDataReader? reader = await GetReaderForQueryAsync(SchemaQueryConstants.GetTablesAndColumnsSql);
            if (reader != null)
            {
                list = new SqlTableCollection();
                while (reader.Read())
                {
                    // Read the raw data.
                    SqlTableColumnRecord record = new SqlTableColumnRecord();
                    int index = 0;
                    record.TableObjectId = reader.GetInt32(index++);
                    record.SchemaName = reader.GetString(index++);
                    record.TableName = reader.GetString(index++);
                    record.ColumnId = reader.GetInt32(index++);
                    record.ColumnName = reader.GetString(index++);
                    record.TypeId = reader.GetInt32(index++);
                    record.MaxLength = reader.GetInt16(index++);
                    record.Precision = reader.GetByte(index++);
                    record.Scale = reader.GetByte(index++);
                    record.IsNullable = reader.GetBoolean(index++);
                    record.IsIdentity = reader.GetBoolean(index++);
                    record.IsComputed = reader.GetBoolean(index++);
                    record.IsPadded = reader.GetBoolean(index);

                    // Find the table object, or create it if needed.
                    SqlTable? table = list[record.TableName!];

                    if (table == null)
                    {
                        table = new SqlTable
                        {
                            Schema = record.SchemaName,
                            TableName = record.TableName,
                            TableObjectId = record.TableObjectId
                        };
                        list.Add(table);
                    }

                    // Create and add the column.
                    SqlColumn newColumn = new SqlColumn
                    {
                        ColumnId = record.ColumnId,
                        ColumnName = record.ColumnName,
                        IsComputed = record.IsComputed,
                        IsIdentity = record.IsIdentity,
                        IsNullable = record.IsNullable,
                        MaxLength = record.MaxLength,
                        Precision = record.Precision,
                        Scale = record.Scale,
                        TypeId = record.TypeId,
                        IsAnsiPadded = record.IsPadded
                    };
                    if (table.Columns != null)
                        table.Columns.Add(newColumn);
                }
                reader.Dispose();
            }

            return list;
        }
        /// <summary>
        /// Gets the list of SQL data type definitions from the database.
        /// </summary>
        /// <returns>
        /// A <see cref="SqlTableTypeCollection"/> containing the SQL user-defined table definition instances.
        /// </returns>
        public SqlTableTypeCollection? GetTableTypes()
        {
            SqlTableTypeCollection? list = null;

            ISafeSqlDataReader? reader = GetReaderForParameterizedCommandText(SchemaQueryConstants.GetTableTypesSql, null);
            if (reader != null)
            {
                list = new SqlTableTypeCollection();
                while (reader.Read())
                {
                    list.Add(new SqlTableType(reader));
                }
                reader.Dispose();
            }

            return list;
        }
        /// <summary>
        /// Gets the list of SQL data type definitions from the database.
        /// </summary>
        /// <returns>
        /// A <see cref="SqlTableTypeCollection"/> containing the SQL user-defined table definition instances.
        /// </returns>
        public async Task<SqlTableTypeCollection?> GetTableTypesAsync()
        {
            SqlTableTypeCollection? list = null;

            ISafeSqlDataReader? reader = await GetReaderForParameterizedCommandTextAsync(SchemaQueryConstants.GetTableTypesSql, null);
            if (reader != null)
            {
                list = new SqlTableTypeCollection();
                while (reader.Read())
                {
                    list.Add(new SqlTableType(reader));
                }
                reader.Dispose();
            }

            return list;
        }
        #endregion
    }
}