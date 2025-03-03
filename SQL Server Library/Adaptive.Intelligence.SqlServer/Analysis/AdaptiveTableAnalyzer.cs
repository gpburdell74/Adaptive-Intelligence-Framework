using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.Shared.Logging;
using Adaptive.Intelligence.SqlServer.Schema;
using Adaptive.SqlServer.Client;

namespace Adaptive.Intelligence.SqlServer.Analysis
{
    /// <summary>
    /// Provides an instance for analyzing and storing information for the provided Adaptive database table schemas
    /// that can be used to render SQL and other code.
    /// </summary>
    /// <seealso cref="DisposableObjectBase" />
    public sealed class AdaptiveTableAnalyzer : DisposableObjectBase, ITableAnalyzer<AdaptiveTableProfileCollection>
    {
        #region Public Events
        /// <summary>
        /// Occurs when a progress update is communicated to the UI.
        /// </summary>
        public event ProgressUpdateEventHandler? ProgressUpdate;
        #endregion

        #region Private Member Declarations
        /// <summary>
        /// The source database instance containing the schema.
        /// </summary>
        private SqlDatabase? _sourceDatabase;
        /// <summary>
        /// The tables list.
        /// </summary>
        private SqlTableCollection? _tables;
        /// <summary>
        /// The profiles data.
        /// </summary>
        private AdaptiveTableProfileCollection? _profiles;
        #endregion

        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="AdaptiveTableAnalyzer"/> class.
        /// </summary>
        /// <param name="sourceDatabase">
        /// The source <see cref="SqlDatabase"/> instance that contains all the schema data for the database.
        /// </param>
        /// <param name="profileList">
        /// The <see cref="AdaptiveTableProfileCollection"/> instance containing the profile list for the tables.
        /// </param>
        public AdaptiveTableAnalyzer(SqlDatabase sourceDatabase, AdaptiveTableProfileCollection profileList)
        {
            _sourceDatabase = sourceDatabase;
            _tables = sourceDatabase.Tables;
            _profiles = profileList;
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
        /// <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            _sourceDatabase = null;
            _tables = null;
            _profiles = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Analyzes the tables for query creation.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="SqlDataProvider"/> instance to use to communicate with the database.
        /// </param>
        public void AnalyzeTablesForQueryCreation(SqlDataProvider provider)
        {
            if (_tables != null)
            {
                int count = 1;
                int len = _tables.Count;
                foreach (SqlTable table in _tables)
                {
                    ProgressUpdate?.BeginInvoke(this, new
                        ProgressUpdateEventArgs("Analyzing: " + table.TableName,
                            Adaptive.Math.Percent(count, len)), null, null);
                    AnalyzeTableForQueryCreation(provider, table);
                    count++;
                }
            }
        }
        /// <summary>
        /// Analyzes the tables for query creation.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="SqlDataProvider"/> instance to use to communicate with the database.
        /// </param>
        public async Task AnalyzeTablesForQueryCreationAsync(SqlDataProvider provider)
        {
            if (_tables != null)
            {
                int count = 1;
                int len = _tables.Count;
                foreach (SqlTable table in _tables)
                {
                    ProgressUpdate?.Invoke(
                        this,
                        new ProgressUpdateEventArgs(
                            table.TableName, string.Empty,
                            Adaptive.Math.Percent(count, len)));

                    await AnalyzeTableForQueryCreationAsync(provider, table).ConfigureAwait(false);
                    count++;
                }
            }
        }
        /// <summary>
        /// Analyzes the tables for query creation.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="SqlDataProvider"/> instance to use to communicate with the database.
        /// </param>
        /// <param name="table">
        /// The <see cref="SqlTable"/> object being analyzed.
        /// </param>
        public void AnalyzeTableForQueryCreation(SqlDataProvider provider, SqlTable table)
        {
            AdaptiveTableProfile profile = _profiles![table.TableName!]!;
            profile.TableReference = table;

            string schema;
            if (string.IsNullOrEmpty(table.Schema))
                schema = TSqlConstants.DefaultDatabaseOwner;
            else
                schema = table.Schema;

            profile.QualifiedName = TSqlConstants.RenderSchemaAndTableName(schema, table.TableName);

            // Clear.
            profile.KeyFieldNames?.Clear();
            profile.ReferencedTableJoins?.Clear();
            profile.QueryParameters?.Clear();

            // Populate the "Key" column fields based on data type and Adaptive naming conventions.
            foreach (SqlColumn col in table.Columns)
            {
                if (!string.IsNullOrEmpty(col.ColumnName) && 
                    col.ColumnName != "Id" && 
                    col.TypeId == (int)SqlDataTypes.NVarCharOrSysName && col.MaxLength == 256)
                {
                    profile.KeyFieldNames?.Add(col.ColumnName);
                }
            }

            // Match the tables with the specified key fields based on Adaptive naming conventions.
            if (profile.KeyFieldNames != null && _sourceDatabase != null && _sourceDatabase.Tables != null)
            {
                foreach (string name in profile.KeyFieldNames)
                {
                    string subName = name.ToLower().Replace("key", "").Replace("id", "");

                    SqlTable? tableRef = _sourceDatabase.Tables.HeuristicFind(subName);
                    if (tableRef == null)
                    {
                        if (subName.EndsWith("y"))
                            subName = subName.Substring(0, subName.Length - 1) + "ies";
                        tableRef = _sourceDatabase.Tables.HeuristicFind(subName);
                    }

                    if (tableRef != null)
                    {
                        ReferencedTableJoin newItem = new ReferencedTableJoin
                        {
                            ReferencedTable = tableRef,
                            ReferencedTableField = "Id",
                            KeyField = name,
                        };
                        if (table.Columns[name]!.IsNullable)
                            newItem.UsesLeftJoin = true;
                        else
                            newItem.UsesLeftJoin = false;

                        AdaptiveTableProfile? subProfile = _profiles[tableRef.TableName!];
                        profile.ReferencedTableJoins?.Add(newItem);
                    }
                }
            }

            // Create the parameter definitions for update and insert statements.
            profile.CreateColumnParameters();

            // Find and load the existing standard CRUD stored procedures that may have been defined.
            var proceduresList = SqlDatabase.GetStoredProceduresForTable(provider, table.TableName!);
            if (proceduresList != null)
                profile.StandardStoredProcedures?.AddRange(proceduresList);
        }

        /// <summary>
        /// Analyzes the tables for query creation.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="SqlDataProvider"/> instance to use to communicate with the database.
        /// </param>
        /// <param name="table">
        /// The <see cref="SqlTable"/> instance being analyzed.
        /// </param>
        public async Task AnalyzeTableForQueryCreationAsync(SqlDataProvider provider, SqlTable table)
        {
            await Task.Yield();

            try
            {
                AdaptiveTableProfile? profile = _profiles![table.TableName!];
                if (profile != null)
                {
                    profile.TableReference = table;

                    string schema;
                    if (string.IsNullOrEmpty(table.Schema))
                        schema = TSqlConstants.DefaultDatabaseOwner;
                    else
                        schema = table.Schema;

                    profile.QualifiedName = TSqlConstants.RenderSchemaAndTableName(schema, table.TableName);


                    // Clear.
                    profile.KeyFieldNames?.Clear();
                    profile.ReferencedTableJoins?.Clear();
                    profile.QueryParameters?.Clear();

                    // Populate the "Key" column fields based on data type and Adaptive naming conventions.
                    foreach (SqlColumn col in table.Columns)
                    {
                        if (col.ColumnName != "Id" && col.TypeId == (int)SqlDataTypes.NVarCharOrSysName &&
                            col.MaxLength == 256)
                        {
                            profile.KeyFieldNames!.Add(col.ColumnName ?? string.Empty);
                        }
                    }

                    // Match the tables with the specified key fields based on Adaptive naming conventions.
                    if (profile != null && profile.KeyFieldNames != null)
                    {
                        foreach (string name in profile.KeyFieldNames)
                        {
                            string subName = name.ToLower().Replace("key", "").Replace("id", "");

                            SqlTable? tableRef = _sourceDatabase?.Tables?.HeuristicFind(subName);
                            if (tableRef == null)
                            {
                                if (subName.EndsWith("y"))
                                    subName = subName.Substring(0, subName.Length - 1) + "ies";
                                tableRef = _sourceDatabase?.Tables?.HeuristicFind(subName);
                            }

                            if (tableRef != null)
                            {
                                ReferencedTableJoin newItem = new ReferencedTableJoin
                                {
                                    ReferencedTable = tableRef,
                                    ReferencedTableField = "Id",
                                    KeyField = name,
                                };
                                if (table.Columns[name]!.IsNullable)
                                    newItem.UsesLeftJoin = true;
                                else
                                    newItem.UsesLeftJoin = false;

                                AdaptiveTableProfile? subProfile = _profiles[tableRef.TableName!];
                                profile?.ReferencedTableJoins?.Add(newItem);
                            }
                        }
                    }

                    // Create the parameter definitions for update and insert statements.
                    profile?.CreateColumnParameters();

                    // Find and load the existing standard CRUD stored procedures that may have been defined.
                    if (_sourceDatabase != null && provider != null)
                    {
                        SqlStoredProcedureCollection? list = await _sourceDatabase
                            .GetStoredProceduresForTableAsync(
                                provider.ConnectionString ?? string.Empty,
                                table.TableName ?? string.Empty)
                            .ConfigureAwait(false);
                        if (list != null)
                            profile?.StandardStoredProcedures?.AddRange(list);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLog.LogException(ex);
            }
        }
        /// <summary>
        /// Loads the table profiles.
        /// </summary>
        /// <param name="tableList">
        /// A <see cref="SqlTableCollection"/> instance containing the list of tables.
        /// </param>
        /// <returns>
        /// An <see cref="AdaptiveTableProfileCollection"/> containing the list of profile definitions
        /// for each table in the provided <i>tableList</i>.
        /// </returns>
        public AdaptiveTableProfileCollection LoadTableProfiles(SqlTableCollection tableList)
        {
            AdaptiveTableProfileCollection list = new AdaptiveTableProfileCollection();
            list.CreateContentForTables(tableList);
            return list;
        }
        #endregion
    }
}
