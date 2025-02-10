using Adaptive.Intelligence.Shared;
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
        /// The name of the database to operate in.
        /// </summary>
        private string? _databaseName;
        /// <summary>
        /// The operations instance.
        /// </summary>
        private ContextOperations? _operations;
        /// <summary>
        /// The query was saved flag.
        /// </summary>
        private bool _saved;
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
            _databaseName = "master";
            InnerSetText(Properties.Resources.NewQueryTitle);
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
            _databaseName = connectionString.InitialCatalog;
            _operations.ConnectToSqlServerAsync(connectionString.ToString());

            InnerSetText(Properties.Resources.NewQueryTitle);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlQueryEditorDialog"/> class.
        /// </summary>
        /// <param name="operations">
        /// A <see cref="ContextOperations"/> instance used to perform SQL tasks.
        /// </param>
        /// <param name="databaseName">
        /// A string containing the name of the database to operate in.
        /// </param>
        /// <param name="caption">
        /// A string containing the caption for the dialog.
        /// </param>
        /// <param name="sql">
        /// A string containing the SQL Query text.
        /// </param>
        public SqlQueryEditorDialog(ContextOperations operations, string databaseName, string caption, string sql)
        {
            InitializeComponent();
            _operations = operations;
            _databaseName = databaseName;
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
            _databaseName = null;
            components = null;
            _saved = false;

            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the name of the database the query is to be executed in.   
        /// </summary>
        /// <value>
        /// A string containing the name of the database.
        /// </value>
        public string DatabaseName
        {
            get
            {
                if (_databaseName == null)
                    _databaseName = string.Empty;
                return _databaseName;
            }
        }
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
            set
            {
                if (Editor.Text != value)
                {
                    Editor.Text = value;
                    _saved = false;
                }
            }
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
                SaveQueryClick?.Invoke(this, e);

                // Update based on results.
                _saved = e.Success;
                if (_saved)
                {
                    _fileName = e.FileName;
                    Text = _fileName;
                }

                SetState();
            });
        }
        #endregion

        #region Protected Methods Overrides        
        /// <summary>
        /// Assigns the event handlers for the controls on the dialog.
        /// </summary>
        protected override void AssignEventHandlers()
        {
            if (_operations != null)
            {
                _operations.Connected += HandleSqlConnected;
                _operations.Disconnected += HandleSqlDisconnected;
            }

            SaveButton.Click += HandleSaveClicked;
            ExecuteButton.Click += HandleExecuteClicked;
            ParseButton.Click += HandleParseClicked;

            QueryMenuParse.Click += HandleParseClicked;
            QueryMenuExecute.Click += HandleExecuteClicked;

            Editor.TextChanged += HandleEditorTextChanged;
            ResultsGrid.DataError += HandleResultsGridDataError;
        }

        /// <summary>
        /// Removes the event handlers for the controls on the dialog.
        /// </summary>
        protected override void RemoveEventHandlers()
        {
            if (_operations != null)
            {
                _operations.Connected -= HandleSqlConnected;
                _operations.Disconnected -= HandleSqlDisconnected;
            }

            SaveButton.Click -= HandleSaveClicked;
            ExecuteButton.Click -= HandleExecuteClicked;
            ParseButton.Click -= HandleParseClicked;

            QueryMenuParse.Click -= HandleParseClicked;
            QueryMenuExecute.Click -= HandleExecuteClicked;

            Editor.TextChanged -= HandleEditorTextChanged;
            ResultsGrid.DataError -= HandleResultsGridDataError;
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

                // Add a "*" if the document has not been saved since the last edit...
                if (!_saved && !Text.EndsWith('*'))
                    Text += "*";

                // or remove the "*" if the document has been saved since the last edit...
                else if (_saved && Text.EndsWith('*'))
                    Text = Text.Substring(0, Text.Length - 1);

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
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.Closing" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data.</param>
        protected override void OnClosing(CancelEventArgs e)
        {
            if (_saved)
                base.OnClosing(e);
            else
            {
                DialogResult result = MessageBox.Show(
                    Properties.Resources.SaveChangesMessage,
                    Properties.Resources.SaveChangesTitle,
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    SqlQueryEventArgs eventArgs = new SqlQueryEventArgs(Editor.Text, _fileName);
                    OnSaveQueryClick(eventArgs);
                    e.Cancel = eventArgs.Success;
                    base.OnClosing(e);
                }
                else if (result == DialogResult.No)
                    base.OnClosing(e);
            }
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

            SetState();
            SetPostLoadState();
        }
        /// <summary>
        /// Handles the event when the text editor's Text content changes.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void HandleEditorTextChanged(object? sender, EventArgs e)
        {
            _saved = false;
            SetState();
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
        /// <summary>
        /// Handles the results grid data error.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DataGridViewDataErrorEventArgs"/> instance containing the event data.</param>
        private void HandleResultsGridDataError(object? sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        #endregion

        #region Public Methods / Functions		
        /// <summary>
        /// Loads the specified SQL file content into the dialog.
        /// </summary>
        /// <param name="fileName">
        /// A string containing the fully-qualified path and name of the file.</param>
        public void LoadSqlFile(string fileName)
        {
            Shared.IO.TextFile file = new Shared.IO.TextFile(fileName);
            if (file.OpenForRead())
            {
                _fileName = fileName;
                Editor.Text = file.ReadAll();
                file.Close();

                // We just loaded, so...
                Text = _fileName;
                _saved = true;
            }
            file.Dispose();
            Invalidate();
            SetState();

        }
        /// <summary>
        /// Attempts to save the contents of this dialog.
        /// </summary>
        public void Save()
        {
            SetPreLoadState();

            SqlQueryEventArgs eventArgs = new SqlQueryEventArgs(Editor.Text, _fileName);
            OnSaveQueryClick(eventArgs);

            SetPostLoadState();
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
                UserSqlExecutionResult parseResult = await _operations.ParseQueryAsync(_databaseName!, sql).ConfigureAwait(false);
                if (parseResult.Success)
                {
                    List<TSqlStatement>? statements = SqlDataProvider.GetStatements(sql);
                    StringBuilder messagesText = new StringBuilder();
                    if (statements != null)
                    {
                        foreach (TSqlStatement snippet in statements)
                        {
                            if (snippet is SelectStatement)
                            {
                                string queryString = sql.Substring(snippet.StartOffset, snippet.FragmentLength);
                                DataTable? table = await _operations.GetDataTableAsync(_databaseName!, queryString).ConfigureAwait(false);

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
                                UserSqlExecutionResult result = await _operations.ExecuteQueryAsync(_databaseName!, queryString).ConfigureAwait(false);
                                ContinueInMainThread(() =>
                                {
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

                                            QueryTabs.SelectedTab = MessagesPage;
                                            MessagesPage.BringToFront();


                                        }
                                    }
                                });
                            }
                        }

                        SetMessages(messagesText);
                    }
                }
                else
                {
                    StringBuilder messageBuilder = new StringBuilder();
                    if (parseResult != null && parseResult.Errors != null)
                    {
                        foreach (SqlQueryError error in parseResult.Errors)
                        {
                            messageBuilder.AppendLine(error.Message);
                            messageBuilder.AppendLine();
                        }
                        SetMessages(messageBuilder);
                    }
                }
            }

            ContinueInMainThread(SetPostLoadState);
        }
        /// <summary>
        /// Starts the text parsing of the contained SQL Statement/query.
        /// </summary>
        private async Task StartParseAsync()
        {
            if (_operations != null)
            {
                MessagesText.Text = string.Empty;

                UserSqlExecutionResult result = await _operations.ParseQueryAsync(_databaseName!, Editor.Text).ConfigureAwait(false);

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
