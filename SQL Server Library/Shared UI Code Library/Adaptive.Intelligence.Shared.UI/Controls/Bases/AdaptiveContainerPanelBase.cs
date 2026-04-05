using System.ComponentModel;
using Adaptive.Intelligence.Shared.Logging;

namespace Adaptive.Intelligence.Shared.UI
{
#pragma warning disable CS4014
#pragma warning disable VSTHRD110
    /// <summary>
    /// Provides the standard base control definition for container/panel Controls in UI applications.
    /// </summary>
    public class AdaptiveContainerPanelBase : ContainerControl
    {
        #region Public Events
        /// <summary>
        /// Occurs when the control is loaded.
        /// </summary>
        public event EventHandler? Load;
        /// <summary>
        /// Occurs when the content of the control is changed, and a parent
        /// container needs to be notified.
        /// </summary>
        public event EventHandler? ContentChanged;
        /// <summary>
        /// Occurs when the initial load is completed.
        /// </summary>
        public event EventHandler? InitLoadComplete;
        #endregion

        #region Static Member Variables
        /// <summary>
        /// The current process name.
        /// </summary>
        private static string? _processName;
        #endregion

        #region Private Member Declarations
        /// <summary>
        /// Development environment process name.
        /// </summary>
        private const string DevProcessName = "devenv.exe";
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
        /// Initializes a new instance of the <see cref="AdaptiveContainerPanelBase"/>
        /// class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public AdaptiveContainerPanelBase()
        {
            SetStyle(ControlStyles.ContainerControl, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            // Initialize the standard basic properties for the dialog.
            InternalInitializeComponent();

            // Set the base font usage.
            SetFont();

            // Capture the type name of the instance for future logging / error reporting.
            _typeName = GetType().Name;

            // Get the process name so we know if we are in design mode.
            if (string.IsNullOrEmpty(_processName))
                _processName = _processName ?? System.Diagnostics.Process.GetCurrentProcess().ProcessName;

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
            if (!IsDisposed && disposing)
            {
                _futureExecutionTargetList?.Clear();
            }

            _futureExecutionTargetList = null;
            _typeName = null;
            _processName = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets a value that indicates whether the
        /// <see cref="System.ComponentModel.Component"/> is currently in design
        /// mode.
        /// </summary>
        /// <value>
        /// <b>true</b> if the component is in design mode; otherwise
        /// <b>false</b>.
        /// </value>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected new bool DesignMode =>
            ((_processName == DevProcessName) ||
             (_processName == Path.GetFileNameWithoutExtension(DevProcessName)));
        #endregion

        #region Event Methods
        /// <summary>
        /// Raises the <see cref="ContentChanged" /> event.
        /// </summary>
        /// <param name="e">
        /// The <see cref="EventArgs"/> instance containing the event data.
        /// </param>
        protected virtual void OnContentChanged(EventArgs e)
        {
            ContinueInMainThread(() => { ContentChanged?.Invoke(this, e); });
        }
        /// <summary>
        /// Raises the <see cref="InitLoadComplete" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected virtual void OnInitLoadComplete(EventArgs e)
        {
            AdaptiveDebug.WriteLine(_typeName + "::OnInitLoadComplete()");
            InitLoadComplete?.Invoke(this, e);
        }
        #endregion

        #region Protected Method Overrides

        /// <summary>
        /// Raises the <see cref="UserControl.Load"/> event.
        /// </summary>
        /// <param name="e">
        /// The <see cref="EventArgs"/> instance containing the event data.
        /// </param>
        protected virtual void OnLoad(EventArgs e)
        {
            
            if (_typeName != null)
                AdaptiveDebug.WriteLine(_typeName);

            // Complete the standard Windows load.
            AdaptiveDebug.WriteLine(_typeName + "::OnLoad()");
            Load?.Invoke(this, e);
            Application.DoEvents();
            SuspendLayout();

            // If the handle has been created, run the dialog's startup process.
            AdaptiveDebug.WriteLine("IsHandleCreated: " + IsHandleCreated);
            if (IsHandleCreated && !DesignMode)
            {
                // Set the UI State before load.
                SetPreLoadState();

                // Assign the event handlers for the controls on the dialog.
                AdaptiveDebug.WriteLine("AssignEventHandlers()");
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
        /// Raises the <see cref="Control.HandleDestroyed" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected override void OnHandleDestroyed(EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(_typeName + "::OnHandleDestroyed()");
            base.OnHandleDestroyed(e);
            if (!DesignMode)
                RemoveEventHandlers();
        }
        #endregion

        #region Protected Methods for Descendant Classes To Override
        /// <summary>
        /// Called when the initial loading process is completed.
        /// </summary>
        protected virtual void OnInitLoadComplete()
        {
        }
        /// <summary>
        /// Assigns the event handlers for the controls on the dialog.
        /// </summary>
        protected virtual void AssignEventHandlers()
        {
        }
        /// <summary>
        /// Removes the event handlers for the controls on the dialog.
        /// </summary>
        protected virtual void RemoveEventHandlers()
        {
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
            if (!IsHandleCreated)
            {
                // We have to wait for the handle to be created before this
                // method can be invoked.
                _futureExecutionTargetList ??= new List<Action>();
                if (_futureExecutionTargetList != null && !IsDisposed)
                {
                    _futureExecutionTargetList.Add(target);
                }
            }
            else
            {
                if (!InvokeRequired && Environment.CurrentManagedThreadId == 1)
                {
                    target();
                }
                else
                {
                    BeginInvoke(target);
                }
            }
        }
        /// <summary>
        /// Sets the state of the UI controls before the data content is loaded.
        /// </summary>
        protected virtual void SetPreLoadState()
        {
            AdaptiveDebug.WriteLine(Name + "::SetPreLoadState()");

            Cursor = Cursors.WaitCursor;

        }
        /// <summary>
        /// Sets the state of the UI controls after the data content is loaded.
        /// </summary>
        protected virtual void SetPostLoadState()
        {
            AdaptiveDebug.WriteLine(Name + "::SetPostLoadState()");
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
        }
        /// <summary>
        /// Initializes the control and dialog state according to the form data.
        /// </summary>
        protected virtual void InitializeDataContent()
        {
        }
        /// <summary>
        /// An asynchronous method to initialize the control and dialog state
        /// according to the form data.
        /// </summary>
        protected virtual Task InitializeDataContentAsync()
        {
            return Task.FromResult(default(object));
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
            MessageBoxButtons buttons = MessageBoxButtons.OK;

            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                }));
            }
            else
                MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
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
            ContinueInMainThread(SetState);
        }
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Sets the graphics object to draw items at high-quality value.
        /// </summary>
        /// <param name="g">
        /// The <see cref="Graphics"/> instance to use.
        /// </param>
        protected void SetGraphicsQuality(Graphics g)
        {
            try
            {

                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            }
            catch
            {
            }
        }
        /// <summary>
        /// Sets the display state for the controls on the dialog based on
        /// current conditions.
        /// </summary>
        public void SetState()
        {
            SetSecurityState();
            SetDisplayState();
        }
        #endregion

        #region Private Methods / Functions
        /// <summary>
        /// Sets the font.
        /// </summary>
        private void SetFont()
        {
            Font = new Font("Segoe UI", 9.75f);
        }
        /// <summary>
        /// Performs the basic setup of the dialog from the constructor.
        /// </summary>
        private void InternalInitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(AdaptiveContainerPanelBase));
            SuspendLayout();

            // DialogBase
            ClientSize = new Size(850, 556);
            Font = new Font("Segoe UI", 9.75f);
            Name = "AdaptiveContainerPanelBase";
            DoubleBuffered = true;
            ResumeLayout(false);
        }
        /// <summary>
        /// Starts the process of background asynchronous loading of any data content for the dialog.
        /// </summary>
        /// <remarks>
        /// When completed, the <see cref="FinishBackgroundLoad"/> method is invoked on the main UI thread.
        /// </remarks>
        private async Task StartInitializeDataContentAsync()
        {
            // Initialize Data may be implemented synchronously or
            // asynchronously. Ensure execution proceeds sequentially.
            AdaptiveDebug.WriteLine("StartInitializeDataContentAsync()");

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
        /// <summary>
        /// Completes the background data loading process in the main UI thread.
        /// </summary>
        private void FinishBackgroundLoad()
        {
            AdaptiveDebug.WriteLine(Name + "::FinishBackgroundLoad()");
            if (IsHandleCreated && !DesignMode)
            {
                // Assign the data to the controls on the dialog.
                AdaptiveDebug.WriteLine("InitializeDataContent()");
                InitializeDataContent();

                // Se the visual state.
                AdaptiveDebug.WriteLine("SetState()");
                SetState();

                // Set the UI State after load.
                AdaptiveDebug.WriteLine("SetPostLoadState()");
                SetPostLoadState();
            }

            ResumeLayout();
            OnInitLoadComplete();
        }
        #endregion
    }
}
