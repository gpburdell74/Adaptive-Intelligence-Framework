using Adaptive.Intelligence.Shared.Logging;
using System.ComponentModel;
using System.Runtime.Versioning;

namespace Adaptive.Intelligence.Shared.UI
{
#pragma warning disable CS4014
#pragma warning disable VSTHRD110
    /// <summary>
    /// Provides the standard base dialog definition for dialogs in UI applications.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public class AdaptiveDialogBase : Form
    {
        #region Private Member Declarations
        /// <summary>
        /// The type name for the instance.
        /// </summary>
        private string? _typeName;
        /// <summary>
        /// Points to a method to be executed only once the handle for the
        /// control is created.
        /// </summary>
        private List<Action>? _futureExecutionTargetList;
        #endregion

        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="AdaptiveDialogBase"/>
        /// class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public AdaptiveDialogBase()
        {
            AdaptiveDebug.WriteLine(UIConstants.MethodStartText(Name, nameof(AdaptiveDialogBase)));

            // Initialize the standard basic properties for the dialog.
            InternalInitializeComponent();

            // Set the base font usage.
            SetFont();

            // Capture the type name of the instance for future logging / error reporting.
            _typeName = GetType().Name;

            // Create the list to store method calls to be executed in the future on the main thread.
            _futureExecutionTargetList = new List<Action>();
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources;
        /// <c>false</c> to release only unmanaged resources.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            AdaptiveDebug.WriteLine(UIConstants.MethodStartText(Name, nameof(Dispose)));

            if (!IsDisposed && disposing)
            {
                _futureExecutionTargetList?.Clear();
            }

            _futureExecutionTargetList = null;
            _typeName = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets a value that indicates whether the
        /// <see cref="Component"/> is currently in design
        /// mode.
        /// </summary>
        /// <value>
        /// <b>true</b> if the component is in design mode; otherwise
        /// <b>false</b>.
        /// </value>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected new bool DesignMode => UIConstants.InDesignMode() || base.DesignMode;

        /// <summary>
        /// Gets or sets a value indicating whether the control can respond to user interaction.
        /// </summary>
        /// <value>
        /// <b>true</b> if the dialog is enabled; otherwise, <b>false</b>.
        /// </value>
        public new bool Enabled
        {
            get => base.Enabled;
            set
            {
                base.Enabled = value;
                Invalidate();
            }
        }
        #endregion

        #region Protected Method Overrides
        /// <summary>
        /// Raises the <see cref="UserControl.Load"/> event.
        /// </summary>
        /// <param name="e">
        /// The <see cref="EventArgs"/> instance containing the event data.
        /// </param>
        protected override void OnLoad(EventArgs e)
        {
            AdaptiveDebug.WriteLine(UIConstants.MethodStartText(Name, nameof(OnLoad)));

            // Set the form state variables.
            HelpButton = false;
            StartPosition = FormStartPosition.CenterScreen;
            KeyPreview = true;

            // Complete the standard Windows load.
            AdaptiveDebug.WriteLine(UIConstants.MethodStartText(_typeName, nameof(OnLoad)));
            base.OnLoad(e);

            SuspendLayout();

            // If the handle has been created, run the dialog's startup process.
            AdaptiveDebug.WriteLine(UIConstants.IsHandleCreatedText(IsHandleCreated));
            if (IsHandleCreated && !DesignMode)
            {
                // Set the UI State before load.
                SetPreLoadState();

                // Assign the event handlers for the controls on the dialog.
                AdaptiveDebug.WriteLine(nameof(AssignEventHandlers));
                AssignEventHandlers();

                // Initialize Data may be implemented synchronously or
                // asynchronously. Ensure execution proceeds sequentially.
                StartInitializeDataContentAsync();
            }
            else
            {
                ResumeLayout();
            }
        }
        /// <summary>
        /// Raises the <see cref="E:HandleCreated"/> event.
        /// </summary>
        /// <param name="e">
        /// The <see cref="EventArgs"/> instance containing the event data.
        /// </param>
        protected override void OnHandleCreated(EventArgs e)
        {
            AdaptiveDebug.WriteLine(UIConstants.MethodStartText(Name, nameof(OnHandleCreated)));
            base.OnHandleCreated(e);

            // If there are any methods(s) waiting to be executed, execute them
            // now that the window handle has been created.
            if (!DesignMode && _futureExecutionTargetList != null && _futureExecutionTargetList.Count > 0)
            {
                foreach (Action methodPointer in _futureExecutionTargetList)
                {
                    try
                    {
                        methodPointer();
                    }
                    catch (Exception ex)
                    {
                        ExceptionLog.LogException(ex);
                    }
                }
                _futureExecutionTargetList?.Clear();
            }
        }
        /// <summary>
        /// Raises the <see cref="Form.Closing"/> event.
        /// </summary>
        /// <param name="e">
        /// A <see cref="CancelEventArgs"/> that contains the event data.
        /// </param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            AdaptiveDebug.WriteLine(UIConstants.MethodStartText(Name, nameof(OnClosing)));
            base.OnFormClosing(e);
            if ((!e.Cancel) && (!DesignMode))
                RemoveEventHandlers();
        }
        #endregion

        #region Protected Methods for Descendant Classes To Override
        /// <summary>
        /// Called when the initial loading process is completed.
        /// </summary>
        protected virtual void OnInitLoadComplete()
        {
            AdaptiveDebug.WriteLine(UIConstants.MethodStartText(Name, nameof(OnInitLoadComplete)));
        }
        /// <summary>
        /// Assigns the event handlers for the controls on the dialog.
        /// </summary>
        protected virtual void AssignEventHandlers()
        {
            AdaptiveDebug.WriteLine(UIConstants.MethodStartText(Name, nameof(AssignEventHandlers)));
        }
        /// <summary>
        /// Removes the event handlers for the controls on the dialog.
        /// </summary>
        protected virtual void RemoveEventHandlers()
        {
            AdaptiveDebug.WriteLine(UIConstants.MethodStartText(Name, nameof(RemoveEventHandlers)));
        }
        /// <summary>
        /// Continues the execution of the provided lambda or method in the main
        /// UI thread.
        /// </summary>
        /// <param name="target">
        /// An <see cref="Action"/> representing the code section to invoke.
        /// </param>
        protected void ContinueInMainThread(Action target)
        {
            AdaptiveDebug.WriteLine(UIConstants.MethodStartText(Name, nameof(ContinueInMainThread)));

            if (!IsHandleCreated)
            {
                // We have to wait for the handle to be created before this
                // method can be invoked.
                if (_futureExecutionTargetList == null)
                    _futureExecutionTargetList = new List<Action>();

                if (_futureExecutionTargetList != null && !IsDisposed)
                    _futureExecutionTargetList.Add(target);
            }
            else
            {
                if (!InvokeRequired && Environment.CurrentManagedThreadId == 1)
                    target();
                else
                    BeginInvoke(target);
            }
        }
        /// <summary>
        /// Sets the state of the UI controls before the data content is loaded.
        /// </summary>
        protected virtual void SetPreLoadState()
        {
            AdaptiveDebug.WriteLine(UIConstants.MethodStartText(Name, nameof(SetPreLoadState)));

            Cursor = Cursors.WaitCursor;

        }
        /// <summary>
        /// Sets the state of the UI controls after the data content is loaded.
        /// </summary>
        protected virtual void SetPostLoadState()
        {
            AdaptiveDebug.WriteLine(UIConstants.MethodStartText(Name, nameof(SetPostLoadState)));
            Cursor = Cursors.Default;
        }
        /// <summary>
        /// When implemented in a derived class, sets the display state for the controls on the dialog based on
        /// current conditions.
        /// </summary>
        /// <remarks>
        /// This is called by <see cref="SetState"/> after <see cref="SetSecurityState"/> is called.
        /// </remarks>
        protected virtual void SetDisplayState()
        {
            AdaptiveDebug.WriteLine(UIConstants.MethodStartText(Name, nameof(SetDisplayState)));
        }
        /// <summary>
        /// Sets the visual state of the control based on security conditions and current user status.
        /// </summary>
        /// <remarks>
        /// This is invoked by <see cref="SetState"/> in order to enforce security and role-based user permissions
        /// when needed.
        /// </remarks>
        protected virtual void SetSecurityState()
        {
            AdaptiveDebug.WriteLine(UIConstants.MethodStartText(Name, nameof(SetSecurityState)));
        }
        /// <summary>
        /// Initializes the control and dialog state according to the form data.
        /// </summary>
        protected virtual void InitializeDataContent()
        {
            AdaptiveDebug.WriteLine(UIConstants.MethodStartText(Name, nameof(InitializeDataContent)));
        }
        /// <summary>
        /// An asynchronous method to initialize the control and dialog state
        /// according to the form data.
        /// </summary>
        protected virtual Task InitializeDataContentAsync()
        {
            AdaptiveDebug.WriteLine(UIConstants.MethodStartText(Name, nameof(InitializeDataContentAsync)));

            return Task.FromResult(default(object));
        }
        /// <summary>
        /// Provides a method for setting the window text in the descendant class constructors without the 
		/// standard warning.
        /// </summary>
        /// <param name="windowText">
		/// A string containing the new window text.
		/// </param>
        protected void InnerSetText(string windowText)
        {
            Text = windowText;
        }
        /// <summary>
        /// Prompts the user to confirm an operation.
        /// </summary>
        /// <param name="caption">
        /// The caption to display on the message box.
        /// </param>
        /// <param name="message">
        /// The message to display on the message box.
        /// </param>
        /// <returns>
        /// <b>true</b> if the user clicks Yes, otherwise, returns <b>false</b>.
        /// </returns>
        protected static bool GetUserConfirmation(string caption, string message)
        {
            AdaptiveDebug.WriteLine(UIConstants.MethodStartText(nameof(AdaptiveDialogBase), nameof(InitializeDataContentAsync)));

            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Question);
            return result == DialogResult.Yes;
        }
        /// <summary>
        /// Displays the specified error message to the user.
        /// </summary>
        /// <param name="caption">
        /// The caption to display on the message box.
        /// </param>
        /// <param name="message">
        /// The message to display on the message box.
        /// </param>
        protected void ShowError(string caption, string message)
        {
            AdaptiveDebug.WriteLine(UIConstants.MethodStartText(Name, nameof(ShowError)));

            MessageBoxButtons buttons = MessageBoxButtons.OK;

            DialogResult result = MessageBox.Show(
                message, caption, buttons, MessageBoxIcon.Error);
        }
        /// <summary>
        /// Displays the specified error message to the user.
        /// </summary>
        /// <param name="caption">
        /// The caption to display on the message box.
        /// </param>
        /// <param name="message">
        /// The message to display on the message box.
        /// </param>
        protected void ShowMessage(string caption, string message)
        {
            AdaptiveDebug.WriteLine(UIConstants.MethodStartText(Name, nameof(ShowMessage)));

            MessageBoxButtons buttons = MessageBoxButtons.OK;

            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    MessageBox.Show(message, caption, buttons, MessageBoxIcon.Information);
                }));
            }
            else
                MessageBox.Show(message, caption, buttons, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Handles the generic event when the content of a control changes.
        /// </summary>
        /// <remarks>
        /// This is used in various locations to invoke <see cref="SetState"/>,
        /// so now this provides the standard event handler built-in.
        /// </remarks>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The <see cref="EventArgs"/> instance containing the event data.
        /// </param>
        protected virtual void HandleGenericControlChange(object? sender, EventArgs e)
        {
            AdaptiveDebug.WriteLine(UIConstants.MethodStartText(Name, nameof(HandleGenericControlChange)));
            ContinueInMainThread(SetState);
        }
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Sets the display state for the controls on the dialog based on
        /// current conditions.
        /// </summary>
        public void SetState()
        {
            AdaptiveDebug.WriteLine(UIConstants.MethodStartText(Name, nameof(SetState)));

            SetSecurityState();
            SetDisplayState();
        }
        #endregion

        #region Private Methods / Functions
        /// <summary>
        /// Completes the background data loading process in the main UI thread.
        /// </summary>
        private void FinishBackgroundLoad()
        {
            AdaptiveDebug.WriteLine(UIConstants.MethodStartText(Name, nameof(FinishBackgroundLoad)));

            if (IsHandleCreated && !DesignMode)
            {
                // Assign the data to the controls on the dialog.
                InitializeDataContent();

                // Se the visual state.
                SetState();

                // Set the UI State after load.
                SetPostLoadState();
            }

            ResumeLayout();

            // Indicate the initial load process has completed.
            OnInitLoadComplete();
        }
        /// <summary>
        /// Performs the basic setup of the dialog from the constructor.
        /// </summary>
        private void InternalInitializeComponent()
        {
            AdaptiveDebug.WriteLine(UIConstants.MethodStartText(Name, nameof(HandleGenericControlChange)));

            ComponentResourceManager resources = new ComponentResourceManager(typeof(AdaptiveDialogBase));
            SuspendLayout();

            // DialogBase
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(850, 556);
            Font = new Font("Segoe UI", 9.75f);
            Name = "DialogBase";
            DoubleBuffered = true;
            ResumeLayout(false);
        }
        /// <summary>
        /// Sets the font.
        /// </summary>
        private void SetFont()
        {
            AdaptiveDebug.WriteLine(UIConstants.MethodStartText(Name, nameof(SetFont)));

            Font = UIConstants.CreateStandardFont();
        }

        private void InitializeComponent()
        {

        }

        /// <summary>
        /// Starts the process of background asynchronous loading of any data content for the dialog.
        /// </summary>
        /// <remarks>
        /// When completed, the <see cref="FinishBackgroundLoad"/> method is invoked on the main UI thread.
        /// </remarks>
        private async Task StartInitializeDataContentAsync()
        {
            AdaptiveDebug.WriteLine(UIConstants.MethodStartText(Name, nameof(StartInitializeDataContentAsync)));

            // Initialize Data may be implemented synchronously or
            // asynchronously. Ensure execution proceeds sequentially.
            try
            {
                await InitializeDataContentAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                ExceptionLog.LogException(ex);
            }

            ContinueInMainThread(FinishBackgroundLoad);
        }
        #endregion
    }
}