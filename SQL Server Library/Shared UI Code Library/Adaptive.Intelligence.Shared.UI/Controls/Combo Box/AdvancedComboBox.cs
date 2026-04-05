using System.ComponentModel;

namespace Adaptive.Intelligence.Shared.UI.Controls
{
    /// <summary>
    /// Provides a descendant class of the ComboBox control as a base
    /// class for loading and displaying and allowing the user to select business
    /// objects.
    /// </summary>
    [ToolboxItem(true),
     Description("Implements a list selector for business objects.")]
    public class AdvancedComboBox : ComboBox
    {
        #region Private Member Declarations
        /// <summary>
        /// The designer process name.
        /// </summary>
        private const string DesignProcessName = "devenv";
        /// <summary>
        /// Current process name.
        /// </summary>
        private static string? _processName;
        #endregion

        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="AdvancedComboBox"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public AdvancedComboBox() : base()
        {
            if (_processName == null)
                _processName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets a value that indicates whether the <see cref="System.ComponentModel.Component" /> is currently in design mode.
        /// </summary>
        /// <value>
        /// <b>true</b> if the component is in design mode; otherwise, <b>false</b>.
        /// </value>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool DesignMode
        {
            get
            {
                if (!string.IsNullOrEmpty(_processName))
                    return (_processName.Contains(DesignProcessName));
                else
                    return true;
            }
        }
        #endregion

        #region Protected Properties
        /// <summary>
        /// Gets the parent form handle.
        /// </summary>
        /// <value>
        /// The parent form handle, or <see cref="IntPtr.Zero"/>.
        /// </value>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected virtual IntPtr ParentFormHandle
        {
            get
            {
                IntPtr value = IntPtr.Zero;
                Control? parent = Parent;
                while ((parent != null) && (!(parent is Form)))
                    parent = parent.Parent;

                if (parent != null)
                    value = parent.Handle;

                return value;
            }
        }
        #endregion

        #region Protected Methods / Functions
        /// <summary>
        /// Creates and assigns the event handlers for the controls on the control.
        /// </summary>
        protected virtual void AssignEventHandlers()
        {
        }
        /// <summary>
        /// Raises the <see cref="Control.VisibleChanged" /> event.
        /// </summary>
        /// <remarks>
        /// When the control becomes visible, the <see cref="SetState"/> method is invoked.
        /// </remarks>
        /// <param name="e">
        /// The <see cref="EventArgs"/> instance containing the event data.
        /// </param>
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (!InvokeRequired)
            {
                if (!DesignMode && Visible)
                    SetState();
            }
            else
            {
                BeginInvoke(() =>
                {
                    if (!DesignMode && Visible)
                    {
                        SetState();
                    }
                });
            }
        }
        /// <summary>
        /// Raises the <see cref="Control.HandleDestroyed" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);
            if (!DesignMode)
                RemoveEventHandlers();
        }

        /// <summary>
        /// Provides a method to initialize the content of the control when it is first loaded.
        /// </summary>
        protected virtual void InitializeDataContent()
        {
        }

        /// <summary>
        /// Provides an asynchronous method to initialize the content of the control when it is first loaded.
        /// </summary>
        protected virtual Task LoadDataContentAsync()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Initializes the content of the control when it is first loaded.
        /// </summary>
        protected virtual void AssignDataContentToControls()
        {
        }
        /// <summary>
        /// Ensures the specified method call occurs in the Main UI thread.
        /// </summary>
        /// <param name="target">The target.</param>
        protected void ContinueInMainThread(Action target)
        {
            if (!InvokeRequired)
                target();
            else
            {
                Invoke(target);
            }
        }
        /// <summary>
        /// Provides a generic event handler for when the control content changes.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected virtual void HandleGenericControlContentChange(object sender, EventArgs e)
        {
            SetState();
        }
        /// <summary>
        /// Sets the state of the UI before the data content is loaded.
        /// </summary>
        public virtual void SetPreloadState()
        {
        }
        /// <summary>
        /// Sets the state of the UI after the data content is loaded.
        /// </summary>
        public virtual void SetPostloadState()
        {
        }
        /// <summary>
        /// Sets the visual state of the control based on current conditions.
        /// </summary>
        protected virtual void SetState()
        {
        }
        /// <summary>
        /// Removes the event handlers for the controls on the control.
        /// </summary>
        protected virtual void RemoveEventHandlers()
        {
        }
        /// <summary>
        /// Releases any resources held before the control is closed/destroyed.
        /// </summary>
        protected virtual void ReleaseResources()
        {
        }
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Provides an asynchronous method for refreshing the content of the list.
        /// </summary>
        /// <remarks>
        /// When overridden in a derived class, this method will implement the
        /// asynchronous data source load of the content.
        /// </remarks>
        public async Task RefreshListContentAsync()
        {
            BeginInvoke(SetPreloadState);

            await LoadDataContentAsync().ConfigureAwait(false);

            if (InvokeRequired)
            {
                BeginInvoke(AssignDataContentToControls);
                BeginInvoke(SetPostloadState);
                BeginInvoke(SetState);
            }
            else
            {
                AssignDataContentToControls();
                SetPostloadState();
                SetState();
            }
        }
        #endregion
    }
}