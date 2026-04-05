using Adaptive.Intelligence.Shared.Logging;

namespace Adaptive.Intelligence.Shared.UI
{
    /// <summary>
    /// Provides an implementation of the Windows Forms Tree View.
    /// </summary>
    /// <seealso cref="TreeView" />
    public class AdaptiveTreeView : TreeView
    {
        #region Private Member Declarations
        /// <summary>
        /// The designer process name.
        /// </summary>
        private const string DesignProcessName = "devenv";
        /// <summary>
        /// Current process name.
        /// </summary>
        private readonly string _processName;
        #endregion

        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="AdaptiveTreeView"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public AdaptiveTreeView()
        {
            _processName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed && disposing)
            {
                ReleaseResources();
            }

            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets a value that indicates whether the <see cref="System.ComponentModel.Component" /> is currently in design mode.
        /// </summary>
        /// <value>
        /// <b>true</b> if the component is in design mode; otherwise, <b>false</b>.
        /// </value>
        public new bool DesignMode => (_processName.Contains(DesignProcessName));
        #endregion

        #region Protected Methods        
        /// <summary>
        /// Raises the <see cref="M:System.Windows.Forms.Control.CreateControl" /> method.
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            SuspendLayout();
            if (!DesignMode)
            {
                // Load the data either synchronously or asynchronously, based on the user
                // implementation.
                Task asyncTask = InitializeContentAsync();
                if (asyncTask.Status != TaskStatus.RanToCompletion)
                {
                    asyncTask.GetAwaiter().OnCompleted(ContinueLoad);
                }
                else
                    ContinueLoad();
            }
            ResumeLayout();
        }
        /// <summary>
        /// Continues the load process synchronously.
        /// </summary>
        private void ContinueLoad()
        {
            InitializeContent();
            AssignEventHandlers();
            SetState();
        }
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.HandleDestroyed" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);
            RemoveEventHandlers();
        }
        /// <summary>
        /// Creates and assigns the event handlers for the controls on the control.
        /// </summary>
        protected virtual void AssignEventHandlers()
        {

        }
        /// <summary>
        /// Ensures the specified method is executed in the main UI thread.
        /// </summary>
        /// <param name="actionToExecute">The action to execute.</param>
        protected void ContinueInMainThread(Action actionToExecute)
        {
            if (IsHandleCreated)
            {
                try
                {
                    if (InvokeRequired)
                        BeginInvoke(actionToExecute);
                    else
                        actionToExecute();
                }
                catch (Exception ex)
                {
                    ExceptionLog.LogException(ex);
                }
            }
        }
        /// <summary>
        /// Initializes the content of the control when it is first loaded.
        /// </summary>
        protected virtual void InitializeContent()
        {

        }
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        /// <summary>
        /// Provides an asynchronous method to initialize the content of the control when it is first loaded.
        /// </summary>
        protected virtual async Task InitializeContentAsync()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
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
        /// <summary>
        /// Provides a shortcut method to set the Wait cursor for the dialog and all controls on the dialog.
        /// </summary>
        protected virtual void SetWaitCursor()
        {
            Cursor = Cursors.WaitCursor;
            foreach (Control ctl in Controls)
            {
                if (!(ctl is WebBrowser))
                    ctl.Cursor = Cursors.WaitCursor;
            }
        }
        /// <summary>
        /// Provides a shortcut method to set the default (arrow) cursor for the dialog and all controls on the dialog.
        /// </summary>
        protected virtual void SetDefaultCursor()
        {
            Cursor = Cursors.Default;
            foreach (Control ctl in Controls)
                ctl.Cursor = Cursors.Default;
        }
        #endregion
    }
}
