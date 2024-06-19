using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.Shared.Logging;
using Adaptive.Intelligence.Shared.UI;
using Adaptive.SqlServer.Client;
using Microsoft.Data.SqlClient;
using Microsoft.Win32;
using System.ComponentModel;

namespace Adaptive.Intelligence.SqlServer.UI
{
#pragma warning disable CS4014
    /// <summary>
    /// Provides a connection specification dialog for connecting to SQL Server.
    /// </summary>
    /// <seealso cref="AdaptiveDialogBase" />
    public partial class SqlServerConnectionDialog : AdaptiveDialogBase
    {
        #region Private Constants

        private const string RegistryPath = @"HKEY_CURRENT_USER\Software\SamSql";
        private const string RegistryKeyLastServer = "LastSqlServer";
        private const string RegistryKeyLastUser = "LastSqlUid";

        #endregion

        #region Constructor / Dispose Methods        
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlServerConnectionDialog"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public SqlServerConnectionDialog()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            SaveFormValues();

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
        /// Gets the connection string from the current dialog parameters.
        /// </summary>
        /// <value>
        /// A string containing the SQL Server connection string value, or <b>null</b>.
        /// </value>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string? ConnectString
        {
            get
            {
                SqlConnectionStringBuilder? builder = CreateBuilder();
                if (builder == null)
                    return null;
                else
                    return builder.ToString();
            }
        }
        #endregion

        #region Protected Method Overrides        
        /// <summary>
        /// Assigns the event handlers for the controls on the dialog.
        /// </summary>
        protected override void AssignEventHandlers()
        {
            // Buttons.
            TestButton.Click += HandleTestClicked;
            ConnectButton.Click += HandleConnectClicked;
            CloseButton.Click += HandleCloseClicked;

            // Lists.
            AuthTypeList.SelectedIndexChanged += HandleGenericControlChange;
            
            // Entries.
            ServerText.LostFocus += HandleConnectionInfoChanged;
            UserIdText.LostFocus += HandleConnectionInfoChanged;
            PasswordText.LostFocus += HandleConnectionInfoChanged;

            // Check box.
            DbCheck.CheckedChanged += HandleDbCheckChanged;
        }

		/// <summary>
		/// Removes the event handlers for the controls on the dialog.
		/// </summary>
		protected override void RemoveEventHandlers()
        {
            // Buttons.
            TestButton.Click -= HandleTestClicked;
            ConnectButton.Click -= HandleConnectClicked;
            CloseButton.Click -= HandleCloseClicked;

			// Lists.
			AuthTypeList.SelectedIndexChanged -= HandleGenericControlChange;

			// Entries.
			ServerText.LostFocus -= HandleConnectionInfoChanged;
            UserIdText.LostFocus -= HandleConnectionInfoChanged;
            PasswordText.LostFocus -= HandleConnectionInfoChanged;

            // Check box.
            DbCheck.CheckedChanged -= HandleDbCheckChanged;
        }
        /// <summary>
        /// Initializes the control and dialog state according to the form data.
        /// </summary>
        protected override void InitializeDataContent()
        {
            string? server = ReadRegistryKey(RegistryKeyLastServer);
            string? lastUid = ReadRegistryKey(RegistryKeyLastUser);
            UserIdText.Text = lastUid;
            ServerText.Text = server;

            AuthTypeList.Items.Add(Properties.Resources.AuthTypeWindows);
            AuthTypeList.Items.Add(Properties.Resources.AuthTypeSqlServer);
            AuthTypeList.SelectedIndex = 1;

            PersistCheck.Checked = true;
            AsynCheck.Checked = true;
            AllowMultiCheck.Checked = true;
            PoolingCheck.Checked = true;
            MinPoolSizeText.Value = 5;
            MaxPoolSizeText.Value = 100;

            PacketSizeText.Value = 32767;
            RetryIntervalText.Value = 5;
            RetryCountText.Value = 5;
            PoolFailIntervalCheck.Value = 5;

            DbCheck.Checked = false;
        }
        /// <summary>
        /// Sets the display state for the controls on the dialog based on
        /// current conditions.
        /// </summary>
        /// <remarks>
        /// This is called by <see cref="M:Adaptive.Intelligence.Shared.UI.AdaptiveDialogBase.SetState" /> after <see cref="M:Adaptive.Intelligence.Shared.UI.AdaptiveDialogBase.SetSecurityState" /> is called.
        /// </remarks>
        protected override void SetDisplayState()
        {
            LoginPanel.Visible = (AuthTypeList.SelectedIndex == 1);
            PoolingPanel.Visible = PoolingCheck.Checked;
            DbList.Visible = DbCheck.Checked;

            // Validate.
            bool isValid = PerformValidation();
            ConnectButton.Enabled = isValid;
            TestButton.Enabled = isValid;
        }
        /// <summary>
        /// Sets the state of the UI controls before the data content is loaded.
        /// </summary>
        protected override void SetPreLoadState()
        {
            Cursor = Cursors.WaitCursor;
            LoginPage.Enabled = false;
            OptionsPage.Enabled = false;
            SuspendLayout();
        }
        /// <summary>
        /// Sets the state of the UI controls after the data content is loaded.
        /// </summary>
        protected override void SetPostLoadState()
        {
            Cursor = Cursors.Default;
            LoginPage.Enabled = true;
            OptionsPage.Enabled = true;
            SuspendLayout();
        }
        #endregion

        #region Private Event Handlers                
        /// <summary>
        /// Handles the event when the select database check box's check state changes.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void HandleDbCheckChanged(object? sender, EventArgs e)
        {
            DbList.Visible = DbCheck.Checked;
            if (DbCheck.Checked)
            {
                SqlConnectionStringBuilder? builder = CreateBuilder();
                if (builder != null)
                {
                    SetPreLoadState();
                    BeginBackgroundDatabaseLoadAsync(builder);
                }
                else
                    SetPostLoadState();
            }
        }
        /// <summary>
        /// Handles the event when the Connect button is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void HandleConnectClicked(object? sender, EventArgs e)
        {
            SetPreLoadState();
            SaveFormValues();
            DialogResult = DialogResult.OK;
            Close();
        }
        /// <summary>
        /// Handles the event when the Test button is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void HandleTestClicked(object? sender, EventArgs e)
        {
            SetPreLoadState();
            SaveFormValues();
            SqlConnectionStringBuilder? builder = CreateBuilder();
            if (builder != null)
                BeginTestConnectionAsync(builder);
            else
                SetPostLoadState();
        }
        /// <summary>
        /// Handles the event when the Cancel/Close button is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void HandleCloseClicked(object? sender, EventArgs e)
        {
            SetPreLoadState();
            DialogResult = DialogResult.Cancel;
            Close();
        }
        /// <summary>
        /// Handles the event when the connection information changes.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void HandleConnectionInfoChanged(object? sender, EventArgs e)
        {
            SetState();
        }
        #endregion

        #region Private Methods / Functions        
        /// <summary>
        /// Creates the builder when using Windows Authentication.
        /// </summary>
        /// <returns>
        /// A <see cref="SqlConnectionStringBuilder"/> with the general login parameters.
        /// </returns>
        private SqlConnectionStringBuilder CreateWinAuthBuilder()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource = ServerText.Text,
                Authentication = SqlAuthenticationMethod.ActiveDirectoryIntegrated,
                TrustServerCertificate = true
            };
            SetFromOptions(builder);
            return builder;
        }
        /// <summary>
        /// Creates the builder when using SQL Server Authentication.
        /// </summary>
        /// <returns>
        /// A <see cref="SqlConnectionStringBuilder"/> with the general login parameters.
        /// </returns>
        private SqlConnectionStringBuilder CreateSqlAuthBuilder()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource = ServerText.Text,
                Authentication = SqlAuthenticationMethod.SqlPassword,
                UserID = UserIdText.Text,
                Password = PasswordText.Text
            };
            SetFromOptions(builder);
            return builder;
        }
        /// <summary>
        /// Populates the builder object from the currently specified connection options.
        /// </summary>
        /// <param name="builder">
        /// The <see cref="SqlConnectionStringBuilder"/> to set the parameters for.
        /// </param>
        private void SetFromOptions(SqlConnectionStringBuilder builder)
        {
            builder.PersistSecurityInfo = PersistCheck.Checked;
            builder.Enlist = EnlistCheck.Checked;

            // TODO: Fix this odd issue later.
            //builder.MultipleActiveResultSets = AllowMultiCheck.Checked;
            builder.MultipleActiveResultSets = false;

			if (PoolingCheck.Checked)
            {
                builder.Pooling = true;
                builder.MaxPoolSize = MaxPoolSizeText.Value;
                builder.MinPoolSize = MinPoolSizeText.Value;
            }
            else
                builder.Pooling = false;

            builder.PacketSize = PacketSizeText.Value;
            builder.ConnectRetryCount = RetryCountText.Value;
            builder.ConnectRetryInterval = RetryIntervalText.Value;
            builder.PoolBlockingPeriod = PoolBlockingPeriod.Auto;

            if (DbCheck.Checked && DbList.SelectedIndex > -1)
            {
                string? dbName = DbList.Items[DbList.SelectedIndex] as string;
                if (dbName != null)
                {
                    builder.InitialCatalog = dbName;
                }
                else
                {
                    builder.InitialCatalog = "master";
                }
            }
            else
            {
                builder.InitialCatalog = "master";
            }
        }
        /// <summary>
        /// Saves the user entries for later use.
        /// </summary>
        private void SaveFormValues()
        {
            try
            {
                Registry.SetValue(RegistryPath, RegistryKeyLastServer, ServerText.Text);
            }
            catch (Exception ex)
            {
                ExceptionLog.LogException(ex);
            }

            try
            {
                Registry.SetValue(RegistryPath, RegistryKeyLastUser, UserIdText.Text);
            }
            catch (Exception ex)
            {
                ExceptionLog.LogException(ex);
            }
        }
        /// <summary>
        /// Starts the process of testing the specified connection parameters.
        /// </summary>
        /// <param name="builder">
        /// The <see cref="SqlConnectionStringBuilder"/> instance used to provide the connection parameters for SQL Server.
        /// </param>
        private async Task BeginTestConnectionAsync(SqlConnectionStringBuilder builder)
        {
            SqlDataProvider provider = new SqlDataProvider(builder);
            IOperationalResult result = await provider.TestConnectionAsync().ConfigureAwait(false);
            provider.Dispose();

            ContinueInMainThread(() =>
            {
                if (result.Success)
                {
                    MessageBox.Show(
                        Properties.Resources.ConnectMessageSuccess,
                        Properties.Resources.ConnectTitleSuccess,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {

                    string exceptionData;
                    if (!string.IsNullOrEmpty(result.Message))
                        exceptionData = ": " + result.Message;
                    else if (result.Exceptions?.Count > 0)
                        exceptionData = ": " + result.Exceptions[0].Message;
                    else
                        exceptionData = ".";

                    MessageBox.Show(
                        Properties.Resources.ConnectMessageFailure,
                        Properties.Resources.ConnectTitleError + exceptionData,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                SetPostLoadState();
                result.Dispose();
            });
        }
        /// <summary>
        /// Starts the process of background loading the list of SQL Server databases.
        /// </summary>
        /// <param name="builder">
        /// The <see cref="SqlConnectionStringBuilder"/> instance used to provide the connection parameters for SQL Server.
        /// </param>
        private async Task BeginBackgroundDatabaseLoadAsync(SqlConnectionStringBuilder builder)
        {
            SqlDataProvider provider = new SqlDataProvider(builder);
            IOperationalResult<List<string>> result = await provider.GetDatabaseNamesAsync().ConfigureAwait(false);

            if ((result.Success) && (result.DataContent != null))
            {
                string[] dbList = result.DataContent.ToArray();
                ContinueInMainThread(() =>
                {
                    DbList.Items.Clear();
                    DbList.Items.AddRange(dbList);
                    SetPostLoadState();
                });
            }
            else
                ContinueInMainThread(SetPostLoadState);

            result.Dispose();
            provider.Dispose();
        }
        /// <summary>
        /// Verifies the content of the dialog is valid for connecting to SQL Server.
        /// </summary>
        /// <returns>
        ///   <see langword="true" /> if validation is successful; otherwise, <see langword="false" />. If called from the <see cref="E:System.Windows.Forms.Control.Validating" /> or <see cref="E:System.Windows.Forms.Control.Validated" /> event handlers, this method will always return <see langword="false" />.
        /// </returns>
        private bool PerformValidation()
        {
            ErrorProvider.Clear();

            bool isValid = false;

            if (ServerText.Text.Length < 2)
                ErrorProvider.SetError(ServerText, Properties.Resources.ValidationMessageServerNameRequired);

            else if ((AuthTypeList.SelectedIndex == 1) && (UserIdText.Text.Length == 0))
                ErrorProvider.SetError(UserIdText, Properties.Resources.ValidationMessageUserIdRequired);

            else if ((AuthTypeList.SelectedIndex == 1) && (PasswordText.Text.Length == 0))
                ErrorProvider.SetError(PasswordText, Properties.Resources.ValidationMessagePasswordRequired);

            else if ((PoolingCheck.Checked) && (MinPoolSizeText.Value == 0))
                ErrorProvider.SetError(MinPoolSizeText, Properties.Resources.ValidationMessagePoolInstance);

            else if ((PoolingCheck.Checked) && (MaxPoolSizeText.Value == 0))
                ErrorProvider.SetError(MaxPoolSizeText, Properties.Resources.ValidationMessagePoolInstance);

            else if (DbCheck.Checked && string.IsNullOrEmpty(DbList.SelectedText))
                ErrorProvider.SetError(DbList, Properties.Resources.ValidationMessageSelectDb);

            else
            {
                isValid = true;
            }

            return isValid;
        }
        /// <summary>
        /// Creates the connection string builder instance from the parameters on the dialog.
        /// </summary>
        /// <returns>
        /// A <see cref="SqlConnectionStringBuilder"/> instance.
        /// </returns>
        private SqlConnectionStringBuilder? CreateBuilder()
        {
            SqlConnectionStringBuilder? builder = null;

            if (AuthTypeList.SelectedIndex == 0)
                builder = CreateWinAuthBuilder();

            else if (!string.IsNullOrEmpty(UserIdText.Text) &&
                     (!string.IsNullOrEmpty(PasswordText.Text)))

                builder = CreateSqlAuthBuilder();

            return builder;
        }
        /// <summary>
        /// Reads the specified registry key from the application registry path.
        /// </summary>
        /// <param name="key">
        /// A string containing the name of the key to be read.
        /// </param>
        /// <returns>
        /// A string containing the value, or <b>null</b>.
        /// </returns>
        private string? ReadRegistryKey(string key)
        {
            string? value = null;

            try
            {
                value = (string?)Registry.GetValue(RegistryPath, key, null);
            }
            catch (Exception ex)
            {
                ExceptionLog.LogException(ex);
            }
            return value;
        }
        #endregion
    }
}
