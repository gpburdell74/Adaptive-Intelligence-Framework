﻿using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.SqlServer.ORM;
using Adaptive.Intelligence.SqlServer.Schema;
using Adaptive.SqlServer.Client;

namespace Adaptive.Intelligence.SqlServer.Analysis
{
    /// <summary>
    /// Provides a central provider for containing the Schema data, profile, and other metadata content
    /// describing the tables in an Adaptive SQL Server database.
    /// </summary>
    /// <seealso cref="DisposableObjectBase" />
    public sealed class AdaptiveTableMetadata : DisposableObjectBase
    {
        #region Public Events
        /// <summary>
        /// Occurs when a progress update is communicated to the UI.
        /// </summary>
        public event ProgressUpdateEventHandler? ProgressUpdate;
        #endregion

        #region Private Member Declarations
        /// <summary>
        /// The database instance.
        /// </summary>
        private SqlDatabase? _db;
        /// <summary>
        /// The tables reference.
        /// </summary>
        private SqlTableCollection? _tables;
        /// <summary>
        /// The data types reference.
        /// </summary>
        private SqlDataTypeCollection? _dataTypes;
        /// <summary>
        /// The profile's information.
        /// </summary>
        private AdaptiveTableProfileCollection? _profiles;
        #endregion

        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="AdaptiveTableMetadata"/> class.
        /// </summary>
        /// <param name="database">
        /// A reference to the <see cref="SqlDatabase"/> instance containing the schema information.
        /// </param>
        public AdaptiveTableMetadata(SqlDatabase? database)
        {
            _db = database ?? throw new ArgumentNullException(nameof(database));
            _tables = _db.Tables;
            _dataTypes = _db.DataTypes;

            _profiles = new AdaptiveTableProfileCollection();
            _profiles.CreateContentForTables(_db.Tables);
            _profiles.Save();
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
                _profiles?.Save();
                _profiles?.Clear();
            }

            _db = null;
            _profiles = null;
            _tables = null;
            _dataTypes = null;

            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the reference to the SQL database schema instance.
        /// </summary>
        /// <value>
        /// A <see cref="SqlDatabase"/> instance containing the schema for the database.
        /// </value>
        public SqlDatabase? Database => _db;
        /// <summary>
        /// Gets the reference to the list of data types in the database.
        /// </summary>
        /// <value>
        /// A <see cref="SqlDataTypeCollection"/> instance containing the data type definitions.
        /// </value>
        public SqlDataTypeCollection DataTypes
        {
            get
            {
                if (_dataTypes == null)
                    _dataTypes = new SqlDataTypeCollection();
                return _dataTypes;
            }
        }
        /// <summary>
        /// Gets the reference to the list of table profiles.
        /// </summary>
        /// <value>
        /// A <see cref="SqlDataTypeCollection"/> instance containing the table profile definitions.
        /// </value>
        public AdaptiveTableProfileCollection Profiles
        {
            get
            {
                if (_profiles == null)
                    _profiles = new AdaptiveTableProfileCollection();
                return _profiles;
            }
        }
        /// <summary>
        /// Gets the <see cref="AdaptiveTableProfile"/> with the specified table name.
        /// </summary>
        /// <value>
        /// The <see cref="AdaptiveTableProfile"/>.
        /// </value>
        /// <param name="tableName">
        /// A string specifying the name of the table.
        /// </param>
        /// <returns>
        /// The <see cref="AdaptiveTableProfile"/> instance for the table, or <b>null</b>.
        /// </returns>
        public AdaptiveTableProfile? this[string? tableName]
        {
            get
            {
                if (tableName == null || _profiles == null)
                    return null;
                else
                    return _profiles[tableName];
            }
        }
        /// <summary>
        /// Gets the reference to the list of tables in the database.
        /// </summary>
        /// <value>
        /// A <see cref="SqlTableCollection"/> instance containing the table schema definitions.
        /// </value>
        public SqlTableCollection? Tables => _tables;
        #endregion

        #region Public Methods / Functions        
        /// <summary>
        /// Finds the name of the table by schema and table name.
        /// </summary>
        /// <param name="schema">
        /// A string containing the table schema name.
        /// </param>
        /// <param name="tableName">
        /// A string containing the name of the table to look for.
        /// </param>
        /// <returns>
        /// The <see cref="AdaptiveTableProfile"/> instance that was found, or <b>null</b>.
        /// </returns>
        public AdaptiveTableProfile? FindBySchemaAndName(string schema, string tableName)
        {
            AdaptiveTableProfile? foundProfile = null;

            if (_profiles != null)
            {
                foreach (AdaptiveTableProfile profile in _profiles)
                {
                    if (string.Compare(profile.SchemaName, schema, true) == 0 &&
                        string.Compare(profile.TableName, tableName, true) == 0)
                    {
                        foundProfile = profile;

                    }
                }
            }
            return foundProfile;
        }
        /// <summary>
        /// Adds or fills in any missing meta data information for the tables in the database.
        /// </summary>
        public void SetMetaData(AdaptiveTableAnalyzer analyzer)
        {
            if (_tables != null && _profiles != null)
            {
                foreach (SqlTable table in _tables)
                {
                    UpdateTableProfile(table);
                }
                _profiles?.Save();
            }
        }
        /// <summary>
        /// Starts the analysis process on the SQL tables..
        /// </summary>
        /// <param name="provider">
        /// The <see cref="SqlDataProvider"/> instance used to communicate with the database.
        /// </param>
        public async Task StartAnalysisAsync(SqlDataProvider? provider)
        {
            if (provider != null && _db != null && _profiles != null)
            {
                AdaptiveTableAnalyzer analyzer = new AdaptiveTableAnalyzer(_db, _profiles);

                analyzer.ProgressUpdate += HandleProgressUpdate;
                await analyzer.AnalyzeTablesForQueryCreationAsync(provider).ConfigureAwait(false);
                analyzer.ProgressUpdate -= HandleProgressUpdate;

                SetMetaData(analyzer);
                analyzer.Dispose();

                await _profiles.SaveAsync().ConfigureAwait(false);
            }
        }
        #endregion

        #region Private Event Handlers
        /// <summary>
        /// Handles the progress update event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ProgressUpdateEventArgs"/> instance containing the event data.</param>
        private void HandleProgressUpdate(object sender, ProgressUpdateEventArgs e)
        {
            ProgressUpdate?.Invoke(this, e);
        }
        #endregion

        #region Private Methods / Functions        
        /// <summary>
        /// Updates the table profile instance with the table data.
        /// </summary>
        /// <param name="table">
        /// The <see cref="SqlTable"/> instance representing the table definition.
        /// </param>
        private void UpdateTableProfile(SqlTable? table)
        {
            if (_profiles != null && table != null)
            {
                AdaptiveTableProfile? profile = _profiles.GetProfile(table.TableName);
                if (profile != null)
                {

                    // Separate the singular from the plural, where possible.
                    if (string.IsNullOrEmpty(profile.SingularName))
                        profile.SingularName = GeneralUtils.GetSingleEnglishForm(table.TableName);

                    string singleName = profile.SingularName;

                    if (string.IsNullOrEmpty(profile.DataAccessClassName))
                        profile.DataAccessClassName = singleName + OrmDatabaseOptions.Current.DataAccessClassSuffix;

                    if (string.IsNullOrEmpty(profile.DataDefinitionClassName))
                        profile.DataDefinitionClassName = singleName;

                    if (string.IsNullOrEmpty(profile.Description))
                        profile.Description = table.TableName + OrmDatabaseOptions.Current.TableNameSuffix;

                    if (string.IsNullOrEmpty(profile.FriendlyName))
                        profile.FriendlyName = table.TableName;

                    if (string.IsNullOrEmpty(profile.PluralName))
                        profile.PluralName = table.TableName;

                    if (string.IsNullOrEmpty(profile.QualifiedName))
                        profile.QualifiedName =
                            OrmDatabaseOptions.Current.DefaultQualifiedOwner +
                            OrmDatabaseOptions.Current.QualifiedNameSeparatorOpen +
                            table.TableName +
                            OrmDatabaseOptions.Current.QualifiedNameSeparatorClose;

                    if (string.IsNullOrEmpty(profile.StoredProcedureNamePrefix))
                    {
                        if (OrmDatabaseOptions.Current.UseTableNamesForStoredProcedureNames)
                            profile.StoredProcedureNamePrefix = table.TableName;
                    }

                    if (string.IsNullOrEmpty(profile.GetAllStoredProcedureName))
                        profile.GetAllStoredProcedureName = profile.StoredProcedureNamePrefix +
                            OrmDatabaseOptions.Current.RetrieveAllRecordsStoredProcedureName;

                    if (string.IsNullOrEmpty(profile.GetByIdStoredProcedureName))
                        profile.GetByIdStoredProcedureName = profile.StoredProcedureNamePrefix +
                            OrmDatabaseOptions.Current.RetrieveRecordByIdStoredProcedureName;

                    if (string.IsNullOrEmpty(profile.InsertStoredProcedureName))
                        profile.InsertStoredProcedureName = profile.StoredProcedureNamePrefix +
                            OrmDatabaseOptions.Current.InsertStoredProcedureName;

                    if (string.IsNullOrEmpty(profile.UpdateStoredProcedureName))
                        profile.UpdateStoredProcedureName = profile.StoredProcedureNamePrefix +
                            OrmDatabaseOptions.Current.UpdateStoredProcedureName;

                    if (string.IsNullOrEmpty(profile.DeleteStoredProcedureName))
                        profile.DeleteStoredProcedureName = profile.StoredProcedureNamePrefix +
                            OrmDatabaseOptions.Current.DeleteStoredProcedureName;
                }
            }
        }
        #endregion
    }
}