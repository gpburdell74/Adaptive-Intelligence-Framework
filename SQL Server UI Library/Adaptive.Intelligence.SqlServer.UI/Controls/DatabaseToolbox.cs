using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.Shared.UI;
using Microsoft.Data.SqlClient;
using System.ComponentModel;
using System.Data;

namespace Adaptive.Intelligence.SqlServer.UI
{
    /// <summary>
    /// Provides a control for viewing the server, databases, tables, procedures, etc. in a SQL Server database
    /// in a tree view.
    /// </summary>
    /// <seealso cref="AdaptiveControlBase" />
    public partial class DatabaseToolbox : AdaptiveControlBase
    {
        #region Public Events        
        /// <summary>
        /// Occurs when the instance is successfully connected to SQL Server.
        /// </summary>
        public event EventHandler? Connected;
        /// <summary>
        /// Occurs when a delete stored procedure request is made.
        /// </summary>
        public event SqlStoredProcedureEventHandler? DeleteStoredProcedure;
        /// <summary>
        /// Occurs when the user disconnects from SQL Server.
        /// </summary>
        public event EventHandler? Disconnected;
        /// <summary>
        /// Occurs when an edit stored procedure request is made.
        /// </summary>
        public event SqlStoredProcedureEventHandler? EditStoredProcedure;
        /// <summary>
        /// Occurs when an edit table profile is requested.
        /// </summary>
        public event SqlTableEventHandler? EditTableProfile;
        /// <summary>
        /// Occurs when a table-based generate all data classes is requested.
        /// </summary>
        public event SqlTableEventHandler? GenerateClasses;
        /// <summary>
        /// Occurs when a create table SQL generation is requested.
        /// </summary>
        public event SqlTableEventHandler? GenerateCreateTableSql;
        /// <summary>
        /// Occurs when a table-based generate data access is requested.
        /// </summary>
        public event SqlTableEventHandler? GenerateDataAccessClass;
        /// <summary>
        /// Occurs when a table-based generate data definition class is requested.
        /// </summary>
        public event SqlTableEventHandler? GenerateDataDefinition;
        /// <summary>
        /// Occurs when a generate stored procedure request is made.
        /// </summary>
        public event SqlStoredProcedureEventHandler? GenerateStoredProcedure;
        /// <summary>
        /// Occurs when a table-based generate all CRUD stored procedures is requested.
        /// </summary>
        public event SqlTableEventHandler? GenerateTableAllStoredProcedures;
        /// <summary>
        /// Occurs when a table-based generate delete stored procedure is requested.
        /// </summary>
        public event SqlTableEventHandler? GenerateTableDeleteStoredProcedure;
        /// <summary>
        /// Occurs when a table-based generate get all records stored procedure is requested.
        /// </summary>
        public event SqlTableEventHandler? GenerateTableGetAllStoredProcedure;
        /// <summary>
        /// Occurs when a table-based generate get records by id stored procedure is requested.
        /// </summary>
        public event SqlTableEventHandler? GenerateTableGetByIdStoredProcedure;
        /// <summary>
        /// Occurs when a table-based generate insert stored procedure is requested.
        /// </summary>
        public event SqlTableEventHandler? GenerateTableInsertStoredProcedure;
        /// <summary>
        /// Occurs when a table-based generate update stored procedure is requested.
        /// </summary>
        public event SqlTableEventHandler? GenerateTableUpdateStoredProcedure;
        /// <summary>
        /// Occurs when loading to update the UI.
        /// </summary>
        public event ProgressUpdateEventHandler? ProgressUpdate;
        #endregion

        #region Private Member Declarations        
        /// <summary>
        /// The loading flag.
        /// </summary>
        private bool _loading;
        #endregion

        #region Constructor / Dispose Methods        
        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseToolbox"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public DatabaseToolbox()
        {
            InitializeComponent();
        }
        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed && disposing)
            {
                components?.Dispose();
            }

            components = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties        
        /// <summary>
        /// Gets the name of the user's currently selected database.
        /// </summary>
        /// <value>
        /// A string containing the selected database name.
        /// </value>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SelectedDatabase => DbTree.SelectedDatabase;
        #endregion

        #region Protected Method Overrides        
        /// <summary>
        /// Assigns the event handlers for the controls on the dialog.
        /// </summary>
        protected override void AssignEventHandlers()
        {
            ToolboxCloseButton.Click += HandleDisconnectClick;
            DisconnectImageButton.Click += HandleDisconnectClick;

            DbTree.ProgressUpdate += HandleTreeProgressUpdate;
            DbTree.GenerateTableGetAllStoredProcedure += HandleTableGenerateGetAllClick;
            DbTree.GenerateTableGetByIdStoredProcedure += HandleTableGenerateGetByIdClick;
            DbTree.GenerateTableDeleteStoredProcedure += HandleTableGenerateDeleteClick;
            DbTree.GenerateTableInsertStoredProcedure += HandleTableGenerateInsertClick;
            DbTree.GenerateTableUpdateStoredProcedure += HandleTableGenerateUpdateClick;
            DbTree.GenerateTableAllStoredProcedures += HandleTableGenerateAllStoredProcsClick;
            DbTree.GenerateCreateTableSql += HandleTableGenerateCreateTableSqlClick;

            DbTree.GenerateDataAccessClass += HandleTableGenerateDataAccessClick;
            DbTree.GenerateDataDefinition += HandleTableGenerateDataDefinitionClick;
        }
        /// <summary>
        /// Removes the event handlers for the controls on the dialog.
        /// </summary>
        protected override void RemoveEventHandlers()
        {
            ToolboxCloseButton.Click -= HandleDisconnectClick;
            DisconnectImageButton.Click -= HandleDisconnectClick;

            DbTree.ProgressUpdate -= HandleTreeProgressUpdate;
            DbTree.GenerateTableGetAllStoredProcedure -= HandleTableGenerateGetAllClick;
            DbTree.GenerateTableGetByIdStoredProcedure -= HandleTableGenerateGetByIdClick;
            DbTree.GenerateTableDeleteStoredProcedure -= HandleTableGenerateDeleteClick;
            DbTree.GenerateTableInsertStoredProcedure -= HandleTableGenerateInsertClick;
            DbTree.GenerateTableUpdateStoredProcedure -= HandleTableGenerateUpdateClick;
            DbTree.GenerateTableAllStoredProcedures -= HandleTableGenerateAllStoredProcsClick;
            DbTree.GenerateCreateTableSql -= HandleTableGenerateCreateTableSqlClick;

            DbTree.GenerateDataAccessClass -= HandleTableGenerateDataAccessClick;
            DbTree.GenerateDataDefinition -= HandleTableGenerateDataDefinitionClick;
        }
        /// <summary>
        ///Sets the display state for the controls on the dialog based on
        /// current conditions.
        /// </summary>
        /// <remarks>
        /// This is called by <see cref="AdaptiveControlBase.SetState" /> after <see cref="AdaptiveControlBase.SetSecurityState" /> is called.
        /// </remarks>
        protected override void SetDisplayState()
        {
            LoadingLabel.Visible = _loading;
            DbTree.Visible = !_loading;

            bool connected = (DbTree.Server != null);

            ConnectImageButton.Enabled = !connected;
            DisconnectImageButton.Enabled = connected;
            RefreshButton.Enabled = connected;

            Application.DoEvents();
        }

        /// <summary>
        /// Sets the state of the UI controls before the data content is loaded.
        /// </summary>
        protected override void SetPostLoadState()
        {
            Cursor = Cursors.Default;
            Enabled = true;
            DbTree.Enabled = true;
            DbTree.Cursor = Cursors.Default;
            ResumeLayout();
        }

        /// <summary>
        /// Sets the state of the UI controls before the data content is loaded.
        /// </summary>
        protected override void SetPreLoadState()
        {
            Cursor = Cursors.WaitCursor;
            Enabled = false;
            DbTree.Enabled = false;
            DbTree.Cursor = Cursors.WaitCursor;
            SuspendLayout();

            Application.DoEvents();
        }
        #endregion

        #region Event Methods        
        /// <summary>
        /// Raises the <see cref="E:Connected" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnConnected(EventArgs e)
        {
            ContinueInMainThread(() =>
            {
                Connected?.Invoke(this, e);
            });
        }

        /// <summary>
        /// Raises the <see cref="E:DeleteStoredProcedure" /> event.
        /// </summary>
        /// <param name="e">The <see cref="SqlStoredProcedureEventArgs"/> instance containing the event data.</param>
        private void OnDeleteStoredProcedure(SqlStoredProcedureEventArgs e)
        {
            ContinueInMainThread(() =>
            {
                DeleteStoredProcedure?.Invoke(this, e);
            });
        }

        /// <summary>
        /// Raises the <see cref="E:Disconnected" /> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <returns></returns>
        private void OnDisconnected(EventArgs e)
        {
            Disconnected?.Invoke(this, e);
        }
        /// <summary>
        /// Raises the <see cref="E:EditStoredProcedure" /> event.
        /// </summary>
        /// <param name="e">The <see cref="SqlStoredProcedureEventArgs"/> instance containing the event data.</param>
        private void OnEditStoredProcedure(SqlStoredProcedureEventArgs e)
        {
            ContinueInMainThread(() =>
            {
                EditStoredProcedure?.Invoke(this, e);
            });
        }

        /// <summary>
        /// Raises the <see cref="E:EditTableProfile" /> event.
        /// </summary>
        /// <param name="e">The <see cref="SqlTableEventArgs"/> instance containing the event data.</param>
        private void OnEditTableProfile(SqlTableEventArgs e)
        {
            ContinueInMainThread(() =>
            {
                EditTableProfile?.Invoke(this, e);
            });
        }

        /// <summary>
        /// Raises the <see cref="E:GenerateClasses" /> event.
        /// </summary>
        /// <param name="e">The <see cref="SqlTableEventArgs"/> instance containing the event data.</param>
        private void OnGenerateClasses(SqlTableEventArgs e)
        {
            ContinueInMainThread(() =>
            {
                GenerateClasses?.Invoke(this, e);
            });
        }

        /// <summary>
        /// Raises the <see cref="E:GenerateCreateTableSql" /> event.
        /// </summary>
        /// <param name="e">The <see cref="SqlTableEventArgs"/> instance containing the event data.</param>
        private void OnGenerateCreateTableSql(SqlTableEventArgs e)
        {
            ContinueInMainThread(() =>
            {
                GenerateCreateTableSql?.Invoke(this, e);
            });
        }

        /// <summary>
        /// Raises the <see cref="GenerateDataAccessClass" /> event.
        /// </summary>
        /// <param name="e">The <see cref="SqlTableEventArgs"/> instance containing the event data.</param>
        private void OnGenerateDataAccessClass(SqlTableEventArgs e)
        {
            ContinueInMainThread(() =>
            {
                GenerateDataAccessClass?.Invoke(this, e);
            });
        }

        /// <summary>
        /// Raises the <see cref="E:GenerateDataDefinition" /> event.
        /// </summary>
        /// <param name="e">The <see cref="SqlTableEventArgs"/> instance containing the event data.</param>
        private void OnGenerateDataDefinition(SqlTableEventArgs e)
        {
            ContinueInMainThread(() =>
            {
                GenerateDataDefinition?.Invoke(this, e);
            });
        }

        /// <summary>
        /// Raises the <see cref="E:GenerateStoredProcedure" /> event.
        /// </summary>
        /// <param name="e">The <see cref="SqlStoredProcedureEventArgs"/> instance containing the event data.</param>
        private void OnGenerateStoredProcedure(SqlStoredProcedureEventArgs e)
        {
            ContinueInMainThread(() =>
            {
                GenerateStoredProcedure?.Invoke(this, e);
            });
        }

        /// <summary>
        /// Raises the <see cref="E:GenerateTableAllStoredProcedures" /> event.
        /// </summary>
        /// <param name="e">The <see cref="SqlTableEventArgs"/> instance containing the event data.</param>
        private void OnGenerateTableAllStoredProcedures(SqlTableEventArgs e)
        {
            ContinueInMainThread(() =>
            {
                GenerateTableAllStoredProcedures?.Invoke(this, e);
            });
        }

        /// <summary>
        /// Raises the <see cref="E:GenerateTableDeleteStoredProcedure" /> event.
        /// </summary>
        /// <param name="e">The <see cref="SqlTableEventArgs"/> instance containing the event data.</param>
        private void OnGenerateTableDeleteStoredProcedure(SqlTableEventArgs e)
        {
            ContinueInMainThread(() =>
            {
                GenerateTableDeleteStoredProcedure?.Invoke(this, e);
            });
        }

        /// <summary>
        /// Raises the <see cref="E:GenerateTableGetAllStoredProcedure" /> event.
        /// </summary>
        /// <param name="e">The <see cref="SqlTableEventArgs"/> instance containing the event data.</param>
        private void OnGenerateTableGetAllStoredProcedure(SqlTableEventArgs e)
        {
            ContinueInMainThread(() =>
            {
                GenerateTableGetAllStoredProcedure?.Invoke(this, e);
            });
        }

        /// <summary>
        /// Raises the <see cref="E:GenerateTableGetByIdStoredProcedure" /> event.
        /// </summary>
        /// <param name="e">The <see cref="SqlTableEventArgs"/> instance containing the event data.</param>
        private void OnGenerateTableGetByIdStoredProcedure(SqlTableEventArgs e)
        {
            ContinueInMainThread(() =>
            {
                GenerateTableGetByIdStoredProcedure?.Invoke(this, e);
            });
        }

        /// <summary>
        /// Raises the <see cref="E:GenerateTableInsertStoredProcedure" /> event.
        /// </summary>
        /// <param name="e">The <see cref="SqlTableEventArgs"/> instance containing the event data.</param>
        private void OnGenerateTableInsertStoredProcedure(SqlTableEventArgs e)
        {
            ContinueInMainThread(() =>
            {
                GenerateTableInsertStoredProcedure?.Invoke(this, e);
            });
        }

        /// <summary>
        /// Raises the <see cref="E:GenerateTableUpdateStoredProcedure" /> event.
        /// </summary>
        /// <param name="e">The <see cref="SqlTableEventArgs"/> instance containing the event data.</param>
        private void OnGenerateTableUpdateStoredProcedure(SqlTableEventArgs e)
        {
            ContinueInMainThread(() =>
            {
                GenerateTableUpdateStoredProcedure?.Invoke(this, e);
            });
        }

        /// <summary>
        /// Raises the <see cref="ProgressUpdate" /> event.
        /// </summary>
        /// <param name="e">The <see cref="ProgressUpdateEventArgs"/> instance containing the event data.</param>
        private void OnProgressUpdate(ProgressUpdateEventArgs e)
        {
            ContinueInMainThread(() =>
            {
                ProgressUpdate?.Invoke(this, e);
            });
        }
        #endregion

        #region Private Event Handlers        
        /// <summary>
        /// Handles the disconnect click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void HandleDisconnectClick(object? sender, EventArgs e)
        {
            SetPreLoadState();
            DbTree.Server = null;
            OnDisconnected(e);
        }
        /// <summary>
        /// Handles the event when the Stored Procedure Menu-> Delete procedure menu item is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void HandleStoredProcDelete(object? sender, SqlStoredProcedureEventArgs e)
        {
            OnDeleteStoredProcedure(e);
        }

        /// <summary>
        /// Handles the event when the Stored Procedure Menu-> Edit Procedures menu item is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void HandleStoredProcEdit(object? sender, SqlStoredProcedureEventArgs e)
        {
            OnEditStoredProcedure(e);
        }

        /// <summary>
        /// Handles the event when the Stored Procedure Menu-> Generate all stored procedures menu item is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void HandleStoredProcGenerate(object? sender, SqlStoredProcedureEventArgs e)
        {
        }

        /// <summary>
        /// Handles the event when the ... menu item is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void HandleStoredProcMenuClick(object? sender, SqlStoredProcedureEventArgs e)
        {
        }

        /// <summary>
        /// Handles the event when the Table Menu-> Generate all stored procedures menu item is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void HandleTableGenerateAllStoredProcsClick(object? sender, SqlTableEventArgs e)
        {
            OnGenerateTableAllStoredProcedures(e);
        }

        /// <summary>
        /// Handles the event when the Table Menu-> Generate Classes menu item is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void HandleTableGenerateClassesClick(object? sender, SqlTableEventArgs e)
        {
            OnGenerateClasses(e);
        }

        /// <summary>
        /// Handles the event when the Table Menu-> Generate Create Table SQL menu item is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void HandleTableGenerateCreateTableClick(object? sender, SqlTableEventArgs e)
        {
            OnGenerateCreateTableSql(e);
        }

        /// <summary>
        /// Handles the event when the Table Menu-> Generate Data Access Class menu item is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void HandleTableGenerateDataAccessClick(object? sender, SqlTableEventArgs e)
        {
            OnGenerateDataAccessClass(e);
        }

        /// <summary>
        /// Handles the event when the Table Menu-> Generate all stored procedures menu item is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void HandleTableGenerateDataDefinitionClick(object? sender, SqlTableEventArgs e)
        {
            OnGenerateDataDefinition(e);
        }

        /// <summary>
        /// Handles the event when the Table Menu-> Generate Delete SP menu item is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void HandleTableGenerateDeleteClick(object? sender, SqlTableEventArgs e)
        {
            OnGenerateTableDeleteStoredProcedure(e);
        }

        /// <summary>
        /// Handles the event when the Table Menu-> Edit Profile menu item is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void HandleTableGenerateEditProfileClick(object? sender, SqlTableEventArgs e)
        {
            OnEditTableProfile(e);
        }

        /// <summary>
        /// Handles the event when the Table Menu-> Generate All stored procedures menu item is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void HandleTableGenerateGetAllClick(object? sender, SqlTableEventArgs e)
        {
            OnGenerateTableGetAllStoredProcedure(e);
        }

        /// <summary>
        /// Handles the event when the Table Menu-> Generate Get records by ID stored procedures menu item is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void HandleTableGenerateGetByIdClick(object? sender, SqlTableEventArgs e)
        {
            OnGenerateTableGetByIdStoredProcedure(e);
        }

        /// <summary>
        /// Handles the event when the Table Menu-> Generate Insert SP menu item is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void HandleTableGenerateInsertClick(object? sender, SqlTableEventArgs e)
        {
            OnGenerateTableInsertStoredProcedure(e);
        }

        /// <summary>
        /// Handles the event when the Table Menu-> Generate Update SP menu item is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void HandleTableGenerateUpdateClick(object? sender, SqlTableEventArgs e)
        {
            OnGenerateTableUpdateStoredProcedure(e);
        }
        /// <summary>
        /// Handles the event when the Table Menu-> Create Table SQL menu item is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SqlTableEventArgs"/> instance containing the event data.</param>
        private void HandleTableGenerateCreateTableSqlClick(object? sender, SqlTableEventArgs e)
        {
            OnGenerateCreateTableSql(e);
        }
        /// <summary>
        /// Handles the tree progress update.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ProgressUpdateEventArgs"/> instance containing the event data.</param>
        private void HandleTreeProgressUpdate(object? sender, ProgressUpdateEventArgs e)
        {
            OnProgressUpdate(e);
        }
        #endregion

        #region Public Methods / Functions        
        /// <summary>
        /// Connects to the specified server and downloads the schema content into the tree.
        /// </summary>
        /// <param name="builder">
        /// The <see cref="SqlConnectionStringBuilder"/> instance containing the parameters for connecting 
        /// to SQL Server.
        /// </param>
        public async Task<bool> ConnectToServerAsync(SqlConnectionStringBuilder builder)
        {
            bool success = false;

            if (!_loading)
            {
                _loading = true;

                ContinueInMainThread(() =>
                {
                    SetState();
                    SetPreLoadState();
                });

                // Background load the schema data.
                Schema.SqlServer newServer = new Schema.SqlServer(builder.DataSource);
                success = await newServer.LoadSchemaAsync(builder.ToString()).ConfigureAwait(false);

                // Populate the tree and set the UI to a post-load state in the UI thread.
                ContinueInMainThread(() =>
                {
                    DbTree.Server = null;
                    if (success)
                        DbTree.Server = newServer;

                    _loading = false;
                    SetState();
                    SetPostLoadState();
                    if (success)
                        OnConnected(EventArgs.Empty);
                    else
                        OnDisconnected(EventArgs.Empty);
                });
            }
            return success;
        }

        /// <summary>
        /// Disconnects this instance from the connected SQL Server.
        /// </summary>
        public void Disconnect()
        {
            if (!_loading)
            {
                _loading = true;
                DbTree.Server = null;
                _loading = false;
                SetState();
                SetPostLoadState();
            }
        }
        #endregion
    }
}
