using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.Shared.UI.Controls.Combo_Box;
using Adaptive.SqlServer.Client;
using Microsoft.Data.SqlClient;
using System.ComponentModel;

namespace Adaptive.Intelligence.SqlServer.UI
{
    /// <summary>
    /// Provides an advanced drop-down control for selecting a campaign finance filer for a customer.
    /// </summary>
    /// <seealso cref="AdvancedComboBox"/>
    public sealed class DatabaseListComboBox : AdvancedComboBox
    {
        #region Public Events
        /// <summary>
        /// Occurs when the selected database changes.
        /// </summary>
        public event EventHandler? SelectedDatabaseChanged;
        #endregion

        #region Private Member Declarations        
        /// <summary>
        /// The database/connection string list.
        /// </summary>
        private SqlConnectionStringBuilder? _connectionString;
        /// <summary>
        /// The database list.
        /// </summary>
        private List<string>? _dbList;
        #endregion

        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseListComboBox"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public DatabaseListComboBox()
        {
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed && disposing)
            {
                _connectionString?.Clear();
                _dbList?.Clear();
            }

            _dbList = null;
            _connectionString = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties        
        /// <summary>
        /// Gets or sets the connection string to use.
        /// </summary>
        /// <value>
        /// A <see cref="SqlConnectionStringBuilder"/> instance containing the connection string, or <b>null</b>.
        /// </value>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SqlConnectionStringBuilder? ConnectionString
        {
            get => _connectionString;
            set
            {
                _connectionString = value;
                LoadDataContentAsync();
            }

        }
        /// <summary>
        /// Gets the name of the user-selected database.
        /// </summary>
        /// <value>
        /// A string containing the selected database name, or <b>null</b>.
        /// </value>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SelectedDatabase
        {
            get
            {
                if (SelectedIndex == -1)
                    return null;
                else
                    return Items[SelectedIndex] as string;
            }
        }
        #endregion

        #region Protected Event Methods
        /// <summary>
        /// Raises the <see cref="SelectedDatabaseChanged" /> event.
        /// </summary>
        /// <param name="e">
        /// The <see cref="EventArgs"/> instance containing the event data.
        /// </param>
        private void OnSelectedDatabaseChanged(EventArgs e)
        {
            ContinueInMainThread(() => { SelectedDatabaseChanged?.Invoke(this, e); });
        }
        #endregion

        #region Protected Method Overrides        
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.ListControl.SelectedValueChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnSelectedValueChanged(EventArgs e)
        {
            base.OnSelectedValueChanged(e);
            OnSelectedDatabaseChanged(e);
        }
        /// <summary>
        /// Raises the <see cref="E:HandleCreated" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            AssignDataContentToControls();
        }
        /// <summary>
        /// Sets the state of the UI before the data content is loaded.
        /// </summary>
        public override void SetPreloadState()
        {
            // Set the display for loading.
            Cursor = Cursors.WaitCursor;
            Invalidate();
        }
        /// <summary>
        /// Sets the state of the UI after the data content is loaded.
        /// </summary>
        public override void SetPostloadState()
        {
            Cursor = Cursors.Default;
        }
        /// <summary>
        /// Initializes the content of the control when it is first loaded.
        /// </summary>
        protected override void AssignDataContentToControls()
        {
            Items.Clear();
            if (_dbList != null)
            {
                Items.AddRange(_dbList.ToArray());
            }
            Invalidate();
        }
        protected override async Task LoadDataContentAsync()
        {
            if (_connectionString != null)
            {
                SqlDataProvider provider = new SqlDataProvider(_connectionString);
                IOperationalResult<List<string>> result = await provider.GetDatabaseNamesAsync().ConfigureAwait(false);
                if (result.Success)
                {
                    _dbList = new List<string>();
                    _dbList.AddRange(result.DataContent);
                }
                else
                {
                    _dbList = null;
                }
                result.Dispose();
                provider.Dispose();
            }
        }
        #endregion
    }
}
