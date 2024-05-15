﻿using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.Shared.UI;
using Adaptive.SqlServer.Client;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using System.ComponentModel;
using System.Data;
using System.Text;

#pragma warning disable CS4014

namespace Adaptive.Intelligence.SqlServer.UI
{
    /// <summary>
    /// Provides a dialog for editing SQL Query text.
    /// </summary>
    /// <seealso cref="AdaptiveDialogBase" />
    public partial class SqlQueryEditorDialog : AdaptiveDialogBase
    {
        #region Public Events        
        /// <summary>
        /// Occurs when the user clicks the save query menu item or button.
        /// </summary>
        public event SqlQueryEventHandler? SaveQueryClick;
        #endregion

        #region Private Member Declarations        
        /// <summary>
        /// The file name for the query.
        /// </summary>
        private string? _fileName;
        /// <summary>
        /// The operations instance.
        /// </summary>
        private ContextOperations? _operations;
        #endregion

        #region Constructor / Dispose Methods        
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlQueryEditorDialog"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public SqlQueryEditorDialog()
        {
            InitializeComponent();
            InnerSetText(Adaptive.Intelligence.SqlServer.UI.Properties.Resources.NewQueryTitle);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlQueryEditorDialog"/> class.
        /// </summary>
        /// <param name="connectionString">
        /// A <see cref="SqlConnectionStringBuilder"/> containing the connection parameters to connect to SQL Server.
        /// </param>
        public SqlQueryEditorDialog(SqlConnectionStringBuilder connectionString)
        {
            InitializeComponent();
            _operations = new ContextOperations();
            _operations.ConnectToDatabaseAsync(connectionString);

            InnerSetText(Adaptive.Intelligence.SqlServer.UI.Properties.Resources.NewQueryTitle);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlQueryEditorDialog"/> class.
        /// </summary>
        /// <param name="operations">
        /// A <see cref="ContextOperations"/> instance used to perform SQL tasks.
        /// </param>
        /// <param name="caption">
        /// A string containing the caption for the dialog.
        /// </param>
        /// <param name="sql">
        /// A string containing the SQL Query text.
        /// </param>
        public SqlQueryEditorDialog(ContextOperations operations, string caption, string sql)
        {
            InitializeComponent();
            _operations = operations;
            InnerSetText(caption);
            Editor.SqlQuery = sql;
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

            _operations = null;
            _fileName = null;
            components = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties        
        /// <summary>
        /// Gets or sets the name of the file the query is loaded from and/or saved to.
        /// </summary>
        /// <value>
        /// A string containing the fully-qualified path and name of the file, or <b>null</b>.
        /// </value>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string? FileName
        {
            get => _fileName;
            set
            {
                _fileName = value;
                if (value != null)
                    Text = Constants.OpenBracket + _fileName + Constants.CloseBracket;
            }
        }
        /// <summary>
        /// Gets or sets the content of the SQL Query.
        /// </summary>
        /// <value>
        /// A string containing the SQL Query.
        /// </value>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Sql
        {
            get => Editor.Text;
            set => Editor.Text = value;
        }

        #endregion

        #region Protected Methods        
        /// <summary>
        /// Raises the <see cref="SaveQueryClick" /> event.
        /// </summary>
        /// <param name="e">
        /// The <see cref="SqlQueryEventArgs"/> instance containing the event data.
        /// </param>
        private void OnSaveQueryClick(SqlQueryEventArgs e)
        {
            ContinueInMainThread(() =>
            {
                SaveQueryClick?.BeginInvoke(this, e, null, null);
            });
        }
        #endregion

        #region Protected Methods Overrides        
        /// <summary>
        /// Assigns the event handlers for the controls on the dialog.
        /// </summary>
        protected override void AssignEventHandlers()
        {
            _operations.Connected += HandleSqlConnected;
            _operations.Disconnected += HandleSqlDisconnected;

            SaveButton.Click += HandleSaveClicked;
            ExecuteButton.Click += HandleExecuteClicked;
            ParseButton.Click += HandleParseClicked;

            QueryMenuParse.Click += HandleParseClicked;
            QueryMenuExecute.Click += HandleExecuteClicked;

            Editor.TextChanged += HandleGenericControlChange;
            ResultsGrid.DataError += ResultsGrid_DataError;
        }

        private void ResultsGrid_DataError(object? sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        /// <summary>
        /// Removes the event handlers for the controls on the dialog.
        /// </summary>
        protected override void RemoveEventHandlers()
        {
            _operations.Connected -= HandleSqlConnected;
            _operations.Disconnected -= HandleSqlDisconnected;

            SaveButton.Click -= HandleSaveClicked;
            ExecuteButton.Click -= HandleExecuteClicked;
            ParseButton.Click -= HandleParseClicked;

            QueryMenuParse.Click -= HandleParseClicked;
            QueryMenuExecute.Click -= HandleExecuteClicked;

            Editor.TextChanged -= HandleGenericControlChange;
        }
        /// <summary>
        /// Sets the state of the UI controls before the data content is loaded.
        /// </summary>
        protected override void SetPreLoadState()
        {
            Cursor = Cursors.WaitCursor;
            Editor.Enabled = false;
            QueryToolbar.Enabled = false;
            SuspendLayout();
        }
        /// <summary>
        /// Sets the state of the UI controls before the data content is loaded.
        /// </summary>
        protected override void SetPostLoadState()
        {
            Cursor = Cursors.Default;
            Editor.Enabled = true;
            QueryToolbar.Enabled = true;
            QueryStatusText.Text = Properties.Resources.ReadyText;
            ResumeLayout();
        }
        /// <summary>
        /// Sets the display state for the controls on the dialog based on current conditions.
        /// </summary>
        protected override void SetDisplayState()
        {
            if (Editor != null)
            {
                if (_operations == null || !_operations.IsConnected)
                {
                    if (MainMenuStrip != null)
                    MainMenuStrip.Enabled = false;
                    QueryMenu.Enabled = false;
                    Editor.Enabled = false;
                }
                else 
                {
                    if (MainMenuStrip != null)
                        MainMenuStrip.Enabled = true;
                    QueryMenu.Enabled = true;
                    Editor.Enabled = true;

                    bool canSave = Editor.Text.Length > 0;

                    SaveButton.Enabled = canSave;
                    ParseButton.Enabled = canSave;
                    ExecuteButton.Enabled = canSave;

                    QueryMenuParse.Enabled = canSave;
                    QueryMenuExecute.Enabled = canSave;

                    if (MainMenuStrip != null)
                        MainMenuStrip.Visible = false;
                }
            }
        }
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            SetState();
        }

        #endregion

        #region Private Event Handlers
        /// <summary>
        /// Handles the event when the Save button is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void HandleSaveClicked(object? sender, EventArgs e)
        {
            SetPreLoadState();

            OnSaveQueryClick(new SqlQueryEventArgs(Editor.Text, _fileName));

            SetPostLoadState();
        }
        /// <summary>
        /// Handles the event when the Execute button is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void HandleExecuteClicked(object? sender, EventArgs e)
        {
            SetPreLoadState();
            StartExecuteAsync();
        }
        /// <summary>
        /// Handles the event when the Parse button is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void HandleParseClicked(object? sender, EventArgs e)
        {
            SetPreLoadState();
            StartParseAsync();
        }
        /// <summary>
        /// Handles the event when the connection to SQL Server is complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void HandleSqlConnected(object? sender, EventArgs e)
        {
            SetState();
        }
        /// <summary>
        /// Handles the event when disconnected from SQL Server.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void HandleSqlDisconnected(object? sender, EventArgs e)
        {
            SetState();
        }
        #endregion

        #region Private Methods / Functions        
        /// <summary>
        /// Starts the execution of the contained SQL Statements/queries.
        /// </summary>
        private async Task StartExecuteAsync()
        {
            if (_operations != null)
            {
                MessagesText.Text = string.Empty;

                string sql = Editor.Text;
                
                // Ensure the query is good.
                UserSqlExecutionResult parseResult = await _operations.ParseQueryAsync(sql).ConfigureAwait(false);
                if (parseResult.Success)
                {
                    List<TSqlStatement> statements = SqlDataProvider.GetStatements(sql);
                    StringBuilder messagesText = new StringBuilder();

                    foreach (TSqlStatement snippet in statements)
                    {
                        if (snippet is SelectStatement)
                        {
                            string queryString = sql.Substring(snippet.StartOffset, snippet.FragmentLength);
                            DataTable table = await _operations.GetDataTableAsync(queryString).ConfigureAwait(false);

                            ContinueInMainThread(() =>
                            {
                                ResultsGrid.SuspendLayout();
                                ResultsGrid.DataSource = table;
                                ResultsGrid.ResumeLayout();

                                ResultsPage.Visible = true;
                                QueryTabs.Visible = true;

                                SetState();
                                SetPostLoadState();
                            });
                        }
                        else
                        {
                            

                            string queryString = sql.Substring(snippet.StartOffset, snippet.FragmentLength);
                            UserSqlExecutionResult result = await _operations.ExecuteQueryAsync(queryString).ConfigureAwait(false);
                            if (result.Success)
                            {
                                messagesText.AppendLine("Query Executed Successfully.");
                                messagesText.AppendLine();
                            }
                            else
                            {
                                if (result.Errors != null)
                                {
                                    messagesText.AppendLine("Query Execution Failed.");
                                    messagesText.AppendLine("ERROR: " + result.Errors[0].ToString());
                                    messagesText.AppendLine();
                                }
                            }
                            
                        }
                    }

                    SetMessages(messagesText);
                }
                else
                {
                    StringBuilder messageBuilder = new StringBuilder();
                    foreach(SqlQueryError error in parseResult.Errors)
                    {
                        messageBuilder.AppendLine(error.Message);
                        messageBuilder.AppendLine();    
                    }
                    SetMessages(messageBuilder);
                }
            }
        }
        /// <summary>
        /// Starts the text parsing of the contained SQL Statement/query.
        /// </summary>
        private async Task StartParseAsync()
        {
            if (_operations != null)
            {
                MessagesText.Text = string.Empty;

                UserSqlExecutionResult result = await _operations.ParseQueryAsync(Editor.Text).ConfigureAwait(false);

                ContinueInMainThread(() =>
                {
                    if (result.Success)
                        ShowParseSuccess();
                    else
                    {
                        ShowParseFailed(result);
                    }
                    SetPostLoadState();
                    SetState();
                    result.Dispose();
                });
            }
            else
                ContinueInMainThread(SetPostLoadState);
        }
        /// <summary>
        /// Displays a message to the user that the SQL query execution succeeded.
        /// </summary>
        private void ShowExecutionSuccess()
        {
            MessageBox.Show(
                Properties.Resources.ExecuteSqlSuccessMessage,
                Properties.Resources.ExecuteSqlSuccessTitle,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            QueryStatusText.Text = Properties.Resources.ReadyText;
        }
        /// <summary>
        /// Displays a message to the user that the SQL query execution failed.
        /// </summary>
        /// <param name="result">
        /// The <see cref="UserSqlExecutionResult"/> instance containing the result of the operation.
        /// </param>
        private void ShowExecutionFailed(UserSqlExecutionResult result)
        {
            if (result.Errors != null && result.Errors.Count > 0)
            {
                // Highlight the error.
                SqlQueryError sqlErr = result.Errors[0];
                int line = sqlErr.LineNumber;
                Editor.MoveCursorToLine(line);
                Editor.SelectLine();

                MessageBox.Show(
                    sqlErr.Message,
                    Properties.Resources.ExecuteSqlFailedTitle,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                QueryStatusText.Text = sqlErr.Message;
            }
            else
            {
                MessageBox.Show(
                    Properties.Resources.ExecuteSqlFailedMessage,
                    Properties.Resources.ExecuteSqlFailedTitle,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                QueryStatusText.Text = Properties.Resources.ReadyText;
            }
        }
        /// <summary>
        /// Displays a message to the user that the parsing of the T-SQL query text succeeded.
        /// </summary>
        private void ShowParseSuccess()
        {
            MessageBox.Show(
                Properties.Resources.ParseSqlSuccessMessage,
                Properties.Resources.ExecuteSqlSuccessTitle,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            QueryStatusText.Text = Properties.Resources.ReadyText;

        }
        /// <summary>
        /// Displays a message to the user that the parsing of the T-SQL query text failed.
        /// </summary>
        /// <param name="result">
        /// The <see cref="UserSqlExecutionResult"/> instance containing the result of the operation.
        /// </param>
        private void ShowParseFailed(UserSqlExecutionResult result)
        {
            if (result.Errors != null && result.Errors.Count > 0)
            {
                SqlQueryError sqlError = result.Errors[0];
                HighlightError(sqlError);

                MessageBox.Show(sqlError.Message,
                    Properties.Resources.ExecuteSqlFailedTitle,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                QueryStatusText.Text = sqlError.Message;
            }
        }
        /// <summary>
        /// Highlights the error.
        /// </summary>
        /// <param name="sqlError">
        /// The <see cref="SqlQueryError"/> instance containing the error data.
        /// </param>
        private void HighlightError(SqlQueryError sqlError)
        {
            // Highlight the error.
            int line = sqlError.LineNumber;

            int offset = sqlError.Offset;
            if (offset > 1)
                offset -= 2;
            else if (offset > 0)
                offset -= 1;
            else
            {
                line -= 1;
                offset = Editor.Lines[line].Length - 1;
            }
            Editor.MoveCursorToLine(line);
            Editor.SelectionStart = offset;
            Editor.SelectionLength = 1;

        }
        private void SetMessages(StringBuilder messageBuilder)
        {
            ContinueInMainThread(() =>
            {
                QueryTabs.Visible = true;
                MessagesPage.Visible = true;
                MessagesText.Text += messageBuilder.ToString();
                SetState();
                SetPostLoadState();
            });
        }
        #endregion
    }
}
