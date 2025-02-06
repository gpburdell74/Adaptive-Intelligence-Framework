using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.Shared.Logging;
using Adaptive.Intelligence.SqlServer.Data_Access;
using Adaptive.Intelligence.SqlServer.Maintenance;
using Adaptive.SqlServer.Client;

namespace Adaptive.Intelligence.SqlServer
{
    /// <summary>
    /// Provides methods to perform general maintenance activity for table indexes.
    /// </summary>
    /// <seealso cref="DisposableObjectBase" />
    public sealed class GeneralMaintenanceProcessor : DisposableObjectBase
    {
        #region Public Events        
        /// <summary>
        /// Occurs when the operation is completed.
        /// </summary>
        public event EventHandler? OperationComplete;
        /// <summary>
        /// Occurs when the status is being updated.
        /// </summary>
        public event ProgressUpdateEventHandler? StatusUpdate;
        #endregion

        #region Thread Synchronization and Cancellation
        /// <summary>
        /// The thread synchronization root instance.
        /// </summary>
        private static readonly object _syncRoot = new object();
        #endregion

        #region Private Member Declarations
        /// <summary>
        /// The data access instance to use.
        /// </summary>
        private SqlMaintenanceDataAccess? _dataAccess;
        /// <summary>
        /// The database information container.
        /// </summary>
        private DatabaseStatistic? _db;
        /// <summary>
        /// The connection string
        /// </summary>
        private string? _connectionString;
        /// <summary>
        /// The number of rebuild passes to execute.
        /// </summary>
        private int _passCount = 1;
        /// <summary>
        /// The cancel operation flag.
        /// </summary>
        private bool _cancelOperation;
        /// <summary>
        /// The is executing flag.
        /// </summary>
        private bool _executing;
        #endregion

        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralMaintenanceProcessor"/> class.
        /// </summary>
        /// <param name="connectionString">
        /// A string containing the connection information for SQL Server.
        /// </param>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public GeneralMaintenanceProcessor(string connectionString)
        {
            try
            {
                _connectionString = connectionString;
                _dataAccess = new SqlMaintenanceDataAccess(connectionString);
            }
            catch (Exception ex)
            {
                ExceptionLog.LogException(ex);
                _dataAccess = null;
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralMaintenanceProcessor"/> class.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="SqlDataProvider"/> instance to use.
        /// </param>
        public GeneralMaintenanceProcessor(SqlDataProvider provider)
        {
            try
            {
                _connectionString = provider.ConnectionString;
                _dataAccess = new SqlMaintenanceDataAccess(_connectionString!);
            }
            catch (Exception ex)
            {
                ExceptionLog.LogException(ex);
                _dataAccess = null;
            }
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
        /// <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed && disposing)
            {
                _db?.Dispose();
                _dataAccess?.Dispose();
            }

            _db = null;
            _connectionString = null;
            _dataAccess = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets a value indicating whether to cancel the current ongoing operation.
        /// </summary>
        /// <value>
        ///   <b>true</b> to cancel the current ongoing operation; otherwise, <b>false</b>.
        /// </value>
        public bool Cancel
        {
            get => _cancelOperation;
            set
            {
                lock (_syncRoot)
                {
                    _cancelOperation = value;
                }
            }
        }
        /// <summary>
        /// Gets a value indicating whether this instance is executing.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is executing; otherwise, <c>false</c>.
        /// </value>
        public bool IsExecuting => _executing;
        /// <summary>
        /// Gets or sets the number of passes the operation makes in the smart rebuild process.
        /// </summary>
        /// <value>
        /// An integer specifying the number of passes.
        /// </value>
        public int NumberOfPasses
        {
            get => _passCount;
            set
            {
                if (value < 1)
                    value = 1;
                _passCount = value;
            }
        }
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Performs the database maintenance functions.
        /// </summary>
        public void PerformDatabaseMaintenance()
        {
            _cancelOperation = false;
            if (_dataAccess != null)
            {
                // Test connectivity first. Exit if cannot connect to database.
                PerformStatusUpdate(
                    Properties.Resources.StatusInitializing,
                    Properties.Resources.SubStatusTestingConnection,
                    0);

                bool canConnect = SqlMaintenanceDataAccess.TestConnection(_connectionString!);
                if (canConnect && !_cancelOperation)
                {
                    // Load the list of tables and indexes.
                    PerformStatusUpdate(
                        Properties.Resources.StatusInitializing,
                        Properties.Resources.SubStatusReadingSchema,
                        0);

                    _db = _dataAccess.ReadBasicDatabaseSchema();
                    if (_db != null && !_cancelOperation)
                    {
                        // If successful, perform maintenance operations.
                        PerformStatusUpdate(
                            Properties.Resources.StatusConnected,
                            Properties.Resources.SubStatusStartOps,
                            0);

                        PerformMaintenanceOperations();
                    }
                }
                OnOperationComplete(EventArgs.Empty);
            }
        }
        /// <summary>
        /// Performs the database maintenance functions.
        /// </summary>+
        public async Task PerformDatabaseMaintenanceAsync()
        {
            _cancelOperation = false;
            if (_dataAccess != null)
            {
                _executing = true;

                // Test connectivity first. Exit if cannot connect to database.
                PerformStatusUpdate(
                    Properties.Resources.StatusInitializing,
                    Properties.Resources.SubStatusTestingConnection,
                    0);
                bool canConnect = await _dataAccess.TestConnectionAsync(_connectionString).ConfigureAwait(false);
                if (canConnect && !_cancelOperation)
                {
                    // Load the list of tables and indexes.
                    PerformStatusUpdate(
                        Properties.Resources.StatusInitializing,
                        Properties.Resources.SubStatusReadingSchema,
                        0);
                    _db = await _dataAccess.ReadBasicDatabaseSchemaAsync().ConfigureAwait(false);

                    if (_db != null && !_cancelOperation)
                    {
                        // If successful, perform maintenance operations.
                        PerformStatusUpdate(
                            Properties.Resources.StatusConnected,
                            Properties.Resources.SubStatusStartOps,
                            0);

                        await PerformMaintenanceOperationsAsync().ConfigureAwait(false);
                    }
                }
                OnOperationComplete(EventArgs.Empty);
            }
            _executing = false;
        }
        #endregion

        #region Private Operational Methods / Functions
        /// <summary>
        /// Performs the maintenance operations.
        /// </summary>
        /// <remarks>
        /// This method updates the table statistics, reads the index statistics,
        /// rebuild indexes as needed, and sets objects for recompilation.
        /// </remarks>
        private void PerformMaintenanceOperations()
        {
            // Attempt to perform the update of the table statistics.
            if (!_cancelOperation)
            {
                bool canContinue = UpdateStatistics();

                if (canContinue && !_cancelOperation)
                {
                    // Collect the starting statistics.
                    CollectFragmentationStatistics();

                    // Perform Smart Operation.
                    if (!_cancelOperation)
                        PerformSmartRebuild();

                    // Set objects for re-compilation.
                    if (!_cancelOperation)
                        SetForRecompile();
                }
            }
        }
        /// <summary>
        /// Performs the maintenance operations.
        /// </summary>
        /// <remarks>
        /// This method updates the table statistics, reads the index statistics,
        /// rebuild indexes as needed, and sets objects for recompilation.
        /// </remarks>
        private async Task PerformMaintenanceOperationsAsync()
        {

            // Attempt to perform the update of the table statistics.
            if (!_cancelOperation)
            {
                bool canContinue = await UpdateStatisticsAsync();

                if (canContinue && !_cancelOperation)
                {
                    // Collect the starting statistics.
                    await CollectFragmentationStatisticsAsync();

                    // Perform Smart Operation.
                    if (!_cancelOperation)
                        await PerformSmartRebuildAsync();

                    // Set objects for re-compilation.
                    if (!_cancelOperation)
                        await SetForRecompileAsync();
                }
            }
        }
        /// <summary>
        /// Updates the statistics on each table in the database.
        /// </summary>
        /// <returns>
        /// <b>true</b> if the operation is successful; otherwise, returns <b>false</b>.
        /// </returns>
        private bool UpdateStatistics()
        {
            bool success = true;

            if (_db != null && _db.Tables != null && _dataAccess != null)
            {
                int count = 0;
                int length = _db.Tables.Count;
                PerformTableStatsUpdate(null, 0);
                do
                {
                    TableStatistic table = _db.Tables[count];
                    if (!_cancelOperation)
                    {
                        // UPDATE STATISTICS [dbo].[Table Name]
                        PerformTableStatsUpdate(table.Name, count + 1, length);
                        success = _dataAccess.UpdateStatisticsForTable(table.Name);
                    }
                    count++;

                } while (success && (count < length) && !_cancelOperation);
            }

            return success;
        }
        /// <summary>
        /// Updates the statistics on the specified tables.
        /// </summary>
        /// <param name="tableList">
        /// A <see cref="List{T}"/> of strings containing the names of the tables that need
        /// to be updated.
        /// </param>
        private void UpdateStatistics(List<string> tableList)
        {
            bool success = true;
            int count = 0;
            int length = tableList.Count;

            if (_dataAccess != null)
            {
                PerformTableStatsUpdate(null, 0);

                do
                {
                    string tableName = tableList[count];
                    if (!_cancelOperation)
                    {
                        PerformTableStatsUpdate(tableName, count, length);
                        success = _dataAccess.UpdateStatisticsForTable(tableList[count]);
                    }
                    count++;
                } while (success && (count < length) && (!_cancelOperation));
            }
        }
        /// <summary>
        /// Updates the statistics on each table in the database.
        /// </summary>
        /// <returns>
        /// <b>true</b> if the operation is successful; otherwise, returns <b>false</b>.
        /// </returns>
        private async Task<bool> UpdateStatisticsAsync()
        {
            bool success = true;
            int count = 0;

            if (_db != null && _db.Tables != null && _dataAccess != null)
            {
                int length = _db.Tables.Count;

                PerformTableStatsUpdate(null, 0);
                if (length > 0)
                {
                    do
                    {

                        TableStatistic table = _db.Tables[count];
                        if (!_cancelOperation)
                        {
                            PerformTableStatsUpdate(table.Name, count + 1, length);
                            string tableName = TSqlConstants.RenderSchemaAndTableName(table.Schema, table.Name);
                            if (!string.IsNullOrEmpty(tableName))
                                success = await _dataAccess.UpdateStatisticsForTableAsync(tableName).ConfigureAwait(false);

                        }
                        count++;
                    } while (success && (count < length) && (!_cancelOperation));
                }
                else
                    success = false;
            }

            return success;
        }
        /// <summary>
        /// Updates the statistics on the specified tables.
        /// </summary>
        /// <param name="tableList">
        /// A <see cref="List{T}"/> of strings containing the names of the tables that need
        /// to be updated.
        /// </param>
        private async Task UpdateStatisticsAsync(List<string> tableList)
        {
            bool success = true;
            int count = 0;
            int length = tableList.Count;

            if (_dataAccess != null)
            {
                PerformTableStatsUpdate(null, 0);

                if (length > 0)
                {
                    do
                    {
                        string tableName = tableList[count];
                        if (!_cancelOperation)
                        {
                            PerformTableStatsUpdate(tableName, count, length);
                            success = await _dataAccess.UpdateStatisticsForTableAsync(tableList[count]).ConfigureAwait(false);
                        }
                        count++;
                    } while (success && (count < length) && (!_cancelOperation));
                }
            }
        }
        /// <summary>
        /// Collects the fragmentation statistics for the indexes on each table in the database.
        /// </summary>
        private void CollectFragmentationStatistics()
        {
            if (_db != null && _db.Tables != null && _dataAccess != null)
            {
                PerformFragStatsUpdate(null, 0);

                TableStatisticCollection tableList = _db.Tables;
                if (tableList != null && !_cancelOperation)
                {
                    int length = tableList.Count;
                    int count = 1;
                    foreach (TableStatistic table in tableList)
                    {
                        PerformFragStatsUpdate(table.Name, count, length);
                        if (!_cancelOperation)
                        {
                            List<IndexStatistic>? list = _dataAccess.ReadIndexFragmentationStatistics(table.ObjectId);
                            if (list != null)
                            {
                                foreach (IndexStatistic stat in list)
                                {
                                    if (!_cancelOperation)
                                    {
                                        if (table.Indexes != null)
                                        {
                                            IndexStatisticsInfo? index = table.Indexes.GetByIndexId(stat.IndexId);
                                            if (index != null && index.Statistics != null)
                                            {
                                                index.Statistics.Add(stat);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Collects the fragmentation statistics for the indexes on each table in the database.
        /// </summary>
        private async Task CollectFragmentationStatisticsAsync()
        {
            if (_db != null && _db.Tables != null && _dataAccess != null)
            {
                PerformFragStatsUpdate(null, 0);

                TableStatisticCollection tableList = _db.Tables;
                if (tableList != null && !_cancelOperation)
                {
                    int length = tableList.Count;
                    int count = 1;
                    foreach (TableStatistic table in tableList)
                    {
                        PerformFragStatsUpdate(table.Name, count, length);
                        if (!_cancelOperation)
                        {
                            List<IndexStatistic>? list = await _dataAccess.ReadIndexFragmentationStatisticsAsync(table.ObjectId).ConfigureAwait(false);
                            if (!_cancelOperation && list != null)
                            {
                                foreach (IndexStatistic stat in list)
                                {
                                    if (!_cancelOperation)
                                    {
                                        if (table.Indexes != null)
                                        {
                                            IndexStatisticsInfo? index = table.Indexes.GetByIndexId(stat.IndexId);
                                            if (index != null && index.Statistics != null)
                                                index.Statistics.Add(stat);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Performs the process of rebuilding indexes that are fragmented with more than 1000 pages,
        /// as needed, in three passes.
        /// </summary>
        private void PerformSmartRebuild()
        {
            if (!_cancelOperation && _db != null)
            {
                PerformRebuildStatusUpdate(null, 0);
                for (int count = 1; count <= _passCount; count++)
                {
                    PerformRebuildStatusUpdate(count, 0);

                    // Determine the tables whose indexes are to be rebuilt.
                    if (!_cancelOperation)
                    {
                        List<string> tableNames = _db.GetTablesForIndexRebuild();

                        // Create and execute the SQL statements to rebuild the indexes.
                        if (!_cancelOperation)
                        {
                            List<string> statements = CreateExecStatements();
                            ExecuteStatements(statements);

                            // Re-update the statistics on the affected tables.
                            if (!_cancelOperation)
                            {
                                UpdateStatistics(tableNames);

                                // Re-query for the new fragmentation statistics.
                                if (!_cancelOperation)
                                {
                                    CollectFragmentationStatistics();
                                }
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Performs the process of rebuilding indexes that are fragmented with more than 1000 pages,
        /// as needed, in three passes.
        /// </summary>
        private async Task PerformSmartRebuildAsync()
        {
            if (!_cancelOperation && _db != null)
            {
                PerformRebuildStatusUpdate(null, 0);
                for (int count = 1; count <= _passCount; count++)
                {
                    PerformRebuildStatusUpdate("Pass #" + count, count, _passCount);

                    // Determine the tables whose indexes are to be rebuilt.
                    if (!_cancelOperation)
                    {
                        List<string> tableNames = _db.GetTablesForIndexRebuild();

                        // Create and execute the SQL statements to rebuild the indexes.
                        if (!_cancelOperation)
                        {
                            List<string> statements = CreateExecStatements();
                            if (!_cancelOperation)
                            {
                                await ExecuteStatementsAsync(statements).ConfigureAwait(false);

                                // Re-update the statistics on the affected tables.
                                if (!_cancelOperation)
                                {
                                    await UpdateStatisticsAsync(tableNames).ConfigureAwait(false);

                                    // Re-query for the new fragmentation statistics.
                                    if (!_cancelOperation)
                                    {
                                        await CollectFragmentationStatisticsAsync().ConfigureAwait(false);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Creates the SQL execution statements needed to rebuild the indexes.
        /// </summary>
        /// <returns>
        /// A <see cref="List{T}"/> of <see cref="string"/> instances containing each SQL query
        /// to execute.
        /// </returns>
        private List<string> CreateExecStatements()
        {
            List<string> statements = new List<string>();

            // Get the list of indexes that need to be rebuilt.
            if (!_cancelOperation && _db != null && _db.Tables != null)
            {
                PerformStatusUpdate(
                Properties.Resources.StatusPerformSmartRebuild,
                Properties.Resources.SubStatusCreatingStatements, 0);

                List<IndexStatisticsInfo> indexList = _db.Tables.GetIndexesToRebuild();
                foreach (IndexStatisticsInfo index in indexList)
                {
                    if (_cancelOperation)
                        break;
                    // Find the parent table.
                    TableStatistic? table = _db.Tables.GetByObjectId(index.TableId);

                    // Create the SQL statement.
                    if (table != null)
                    {
                        statements.Add(
                            $"ALTER INDEX [{index.Name}] ON [{table.Name}] REBUILD WITH (ONLINE=ON,MAXDOP=2,SORT_IN_TEMPDB=OFF, PAD_INDEX=ON, FILLFACTOR=50)");
                    }
                }
            }

            // Ensure each command is created only once.
            List<string> finalList = statements.Distinct().ToList();
            statements.Clear();
            return finalList;
        }
        /// <summary>
        /// Executes the statements.
        /// </summary>
        /// <param name="statements">
        /// A <see cref="List{T}"/> of <see cref="string"/> instances containing each SQL query
        /// to execute.
        /// </param>
        private void ExecuteStatements(List<string> statements)
        {
            int index = 1;
            int length = statements.Count;

            if (_dataAccess != null)
            {
                PerformRebuildStatusUpdate(Properties.Resources.SubStatusBeginRebuildExec, 0);

                // Execute each statement.
                if (!_cancelOperation)
                {
                    foreach (string sql in statements)
                    {
                        // Status update.
                        PerformRebuildStatusUpdate(Properties.Resources.SubStatusBeginRebuildExec, index, length);
                        index++;

                        // Perform the execution and pause to not overload the server.
                        if (!_cancelOperation)
                        {
                            _dataAccess.ExecuteSql(sql);
                            if (!_cancelOperation)
                            {
                                Thread.Sleep(250);
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Executes the statements.
        /// </summary>
        /// <param name="statements">
        /// A <see cref="List{T}"/> of <see cref="string"/> instances containing each SQL query
        /// to execute.
        /// </param>
        private async Task ExecuteStatementsAsync(List<string> statements)
        {
            int index = 1;
            int length = statements.Count;

            if (_dataAccess != null)
            {
                PerformRebuildStatusUpdate(Properties.Resources.SubStatusBeginRebuildExec, 0);

                // Execute each statement.
                if (!_cancelOperation)
                {
                    foreach (string sql in statements)
                    {
                        // Status update.
                        PerformRebuildStatusUpdate($"Executing Index Rebuilds...{index} of {length}", index, length);
                        index++;

                        // Perform the execution and pause to not overload the server.
                        if (!_cancelOperation)
                        {
                            await _dataAccess.ExecuteSqlAsync(sql).ConfigureAwait(false);
                            if (_dataAccess.HasExceptions)
                            {
                                _dataAccess.Dispose();
                                _dataAccess = new SqlMaintenanceDataAccess(_connectionString);
                            }
                            if (!_cancelOperation)
                            {
                                await Task.Delay(150).ConfigureAwait(false);
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// To ensure fresh query plans that match the new statistics and indexes, this method
        /// sets the "recompile" for each table.
        /// </summary>
        private void SetForRecompile()
        {
            if (_db != null && _db.Tables != null && _dataAccess != null)
            {
                PerformStatusUpdate(Properties.Resources.StatusSetForRecompile, null, 0);

                TableStatisticCollection tableList = _db.Tables;
                int length = tableList.Count;
                if (!_cancelOperation)
                {
                    for (int count = 0; count < length; count++)
                    {
                        if (!_cancelOperation)
                        {
                            TableStatistic table = tableList[count];
                            PerformStatusUpdate(Properties.Resources.StatusSetForRecompile, table.Name, count + 1, length);

                            if (!_cancelOperation)
                            {
                                _dataAccess.RecompileTable(table.Name);
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// To ensure fresh query plans that match the new statistics and indexes, this method
        /// sets the "recompile" for each table.
        /// </summary>
        private async Task SetForRecompileAsync()
        {
            if (_db != null && _db.Tables != null && _dataAccess != null)
            {
                PerformStatusUpdate(Properties.Resources.StatusSetForRecompile, null, 0);

                TableStatisticCollection tableList = _db.Tables;
                int length = tableList.Count;
                if (!_cancelOperation)
                {
                    for (int count = 0; count < length; count++)
                    {
                        if (!_cancelOperation)
                        {
                            TableStatistic table = tableList[count];
                            PerformStatusUpdate(Properties.Resources.StatusSetForRecompile, table.Name, count + 1, length);

                            if (!_cancelOperation)
                            {
                                await _dataAccess.RecompileTableAsync(table.Schema, table.Name).ConfigureAwait(false);
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region Private Event Methods / Functions        
        /// <summary>
        /// Raises the <see cref="E:OperationComplete" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnOperationComplete(EventArgs e)
        {
            OperationComplete?.Invoke(this, e);
        }
        /// <summary>
        /// Raises the <see cref="StatusUpdate" /> event.
        /// </summary>
        /// <param name="e">The <see cref="ProgressUpdateEventArgs"/> instance containing the event data.</param>
        private void OnStatusUpdate(ProgressUpdateEventArgs e)
        {
            StatusUpdate?.Invoke(this, e);
        }
        #endregion

        #region Private Status Update Methods
        /// <summary>
        /// Performs the status update.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="subStatus">The sub status.</param>
        /// <param name="percentComplete">The percent complete.</param>
        private void PerformStatusUpdate(string? status, string? subStatus, int percentComplete)
        {
            OnStatusUpdate(new ProgressUpdateEventArgs(status, subStatus, percentComplete));
        }
        /// <summary>
        /// Performs the status update.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="subStatus">The sub status.</param>
        /// <param name="current">The current.</param>
        /// <param name="total">The total.</param>
        private void PerformStatusUpdate(string? status, string? subStatus, int current, int total)
        {
            int percentDone = 0;
            if (total > 0)
                percentDone = Math.Percent(current, total);
            OnStatusUpdate(new ProgressUpdateEventArgs(status, subStatus, percentDone));
        }
        /// <summary>
        /// Performs a UI update when updating statistics on tables.
        /// </summary>
        /// <param name="tableName">
        /// A string containing the name of the table being processed.
        /// </param>
        /// <param name="percentDone">
        /// An integer containing the completion percentage.
        /// </param>
        private void PerformTableStatsUpdate(string? tableName, int percentDone)
        {
            PerformStatusUpdate(
                Properties.Resources.StatusUpdatingStats,
                tableName,
                percentDone);
        }
        /// <summary>
        /// Performs a UI update when updating statistics on tables.
        /// </summary>
        /// <param name="tableName">
        /// A string containing the name of the table being processed.
        /// </param>
        /// <param name="current">
        /// An integer indicating the ordinal index of the current table.
        /// </param>
        /// <param name="total">
        /// An integer indicating the total number of tables.
        /// </param>
        private void PerformTableStatsUpdate(string? tableName, int current, int total)
        {
            PerformStatusUpdate(
                Properties.Resources.StatusUpdatingStats,
                tableName,
                current,
                total);
        }
        /// <summary>
        /// Performs a UI update when collecting fragmentation statistics.
        /// </summary>
        /// <param name="subStatus">
        /// A string containing the sub-status text.
        /// </param>
        /// <param name="percentDone">
        /// An integer indicating the current completion percentage.
        /// </param>
        private void PerformFragStatsUpdate(string? subStatus, int percentDone)
        {
            PerformStatusUpdate(
                Properties.Resources.StatusCollecting, null, 0);
        }
        /// <summary>
        /// Performs a UI update when collecting fragmentation statistics.
        /// </summary>
        /// <param name="tableName">
        /// A string containing the name of the table being processed.
        /// </param>
        /// <param name="current">
        /// An integer indicating the ordinal index of the current table.
        /// </param>
        /// <param name="total">
        /// An integer indicating the total number of tables.
        /// </param>
        private void PerformFragStatsUpdate(string? tableName, int current, int total)
        {
            if (!string.IsNullOrEmpty(tableName))
            {
                PerformStatusUpdate(Properties.Resources.StatusCollecting,
                    "Reading Statistics For: " + tableName,
                    current, total);
            }
        }
        /// <summary>
        /// Performs a UI update when performing a smart index rebuild.
        /// </summary>
        /// <param name="tableName">
        /// A string containing the name of the table being processed.
        /// </param>
        /// <param name="percentDone">
        /// An integer indicating the current completion percentage.
        /// </param>
        private void PerformRebuildStatusUpdate(string? tableName, int percentDone)
        {
            if (!string.IsNullOrEmpty(tableName))
            {
                PerformStatusUpdate(Properties.Resources.StatusPerformSmartRebuild,
                    tableName, percentDone);
            }
        }
        /// <summary>
        /// Performs a UI update when performing a smart index rebuild.
        /// </summary>
        /// <param name="statusText">
        /// A string containing the sub-status text.
        /// </param>
        /// <param name="count">
        /// An integer indicating the ordinal index of the current table.
        /// </param>
        /// <param name="total">
        /// An integer indicating the total number of tables.
        /// </param>
        private void PerformRebuildStatusUpdate(string? statusText, int count, int total)
        {
            if (!string.IsNullOrEmpty(statusText))
            {
                PerformStatusUpdate(Properties.Resources.StatusPerformSmartRebuild,
                    statusText, count, total);
            }
        }
        /// <summary>
        /// Performs a UI update when performing a smart index rebuild.
        /// </summary>
        /// <param name="currentPass">
        /// An integer indicating the current pass number.
        /// </param>
        /// <param name="percentDone">
        /// An integer indicating the current completion percentage.
        /// </param>
        private void PerformRebuildStatusUpdate(int currentPass, int percentDone)
        {
            PerformStatusUpdate(Properties.Resources.StatusPerformSmartRebuild,
                Properties.Resources.SubStatusPass + currentPass, percentDone);
        }
        #endregion
    }
}