using Adaptive.Intelligence.Shared.Logging;

namespace Adaptive.Intelligence.Shared.UI
{
    /// <summary>
    /// Provides a base definition for implementing a dialog service.
    /// </summary>
    public abstract class DialogServiceBase : ExceptionTrackingBase, IDialogService
    {
        #region Private Member Declarations
        /// <summary>
        /// The UI thread managed ID value.
        /// </summary>
        private int _uiThreadId;
        /// <summary>
        /// The control instance.
        /// </summary>
        private Control? _control;
        /// <summary>
        /// The service provider reference.
        /// </summary>
        private IServiceProvider? _serviceProvider;
        /// <summary>
        /// The future execution target list for when invoked when not in the main thread.
        /// </summary>
        private List<Action>? _futureExecutionTargetList;
        #endregion

        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="DialogServiceBase"/> class.
        /// </summary>
        /// <remarks>
        /// This constructor must be called by the main UI thread.
        /// </remarks>
        /// <param name="serviceProvider">
        /// The reference to the <see cref="IServiceProvider"/> instance for the application.
        /// </param>
        protected DialogServiceBase(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _uiThreadId = Environment.CurrentManagedThreadId;
            _control = new Control();
            _control.HandleCreated += HandleControlCreated;
            _control.CreateControl();
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
                _futureExecutionTargetList?.Clear();
                _control?.Dispose();
            }

            _futureExecutionTargetList = null;
            _control = null;
            _serviceProvider = null;

            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the reference to a UI control created on the main thread used to perform Invoke operations.
        /// </summary>
        /// <value>
        /// A <see cref="Control"/> instance.
        /// </value>
        public Control UIControl => _control!;
        /// <summary>
        /// Gets the managed ID for the UI thread.
        /// </summary>
        /// <value>
        /// An integer specifying the managed thread identifier.
        /// </value>
        public int ManagedUIThreadId => _uiThreadId;
        /// <summary>
        /// Gets or sets the reference to the services provider instance.
        /// </summary>
        /// <value>
        /// The <see cref="IServiceProvider"/> instance for the application.
        /// </value>
        public IServiceProvider Services => _serviceProvider!;
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Continues execution in the context of the main UI thread.
        /// </summary>
        /// <param name="target">
        /// An <see cref="Action"/> delegate pointing to the code content to execute
        /// in the main thread.
        /// </param>
        public virtual void ContinueInMainThread(Action target)
        {
            if (_control != null)
            {
                if (!_control.IsHandleCreated)
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
                    if (!_control.InvokeRequired && Environment.CurrentManagedThreadId == _uiThreadId)
                    {
                        try
                        {
                            target();
                        }
                        catch (Exception ex)
                        {
                            Exceptions.Add(ex);
                            ExceptionLog.LogException(ex);
                        }
                    }
                    else
                    {
                        try
                        {
                            _control.BeginInvoke(target);
                        }
                        catch (Exception ex)
                        {
                            Exceptions.Add(ex);
                            ExceptionLog.LogException(ex);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Displays a message box in the main UI thread.
        /// </summary>
        /// <param name="caption">
        /// A string containing the caption text for the message box.
        /// </param>
        /// <param name="message">
        /// A string containing the message text.
        /// </param>
        /// <returns>
        /// A <see cref="DialogResult"/> enumerated value indicating the result of the operation.
        /// </returns>
        public virtual DialogResult DisplayMessageBox(string caption, string message)
        {
            return DisplayMessageBox(caption, message, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Displays a message box in the main UI thread.
        /// </summary>
        /// <param name="caption">
        /// A string containing the caption text for the message box.
        /// </param>
        /// <param name="message">
        /// A string containing the message text.
        /// </param>
        /// <param name="buttons">
        /// A <see cref="MessageBox"/> enumerated value indicating the buttons to display.
        /// </param>
        /// <param name="icon">
        /// A <see cref="MessageBoxIcon"/> enumerated value indicating the icon to display.
        /// </param>
        /// <returns>
        /// A <see cref="DialogResult"/> enumerated value indicating the result of the operation.
        /// </returns>
        public virtual DialogResult DisplayMessageBox(string caption, string message, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            if (Environment.CurrentManagedThreadId == _uiThreadId)
            {
                return MessageBox.Show(message, caption, buttons, icon);
            }
            else
            {
                DialogResult result = DialogResult.None;
                ContinueInMainThread(() =>
                {
                    result = MessageBox.Show(message, caption, buttons, icon);
                });
                return result;
            }
        }
        #endregion

        #region Private Event Handlers        
        /// <summary>
        /// Handles the event when the "control" control's handle is created.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void HandleControlCreated(object? sender, EventArgs e)
        {
            if (_control != null)
            {
                _control.HandleCreated -= HandleControlCreated;
                // If there are any methods(s) waiting to be executed, execute them
                // now that the window handle has been created.
                if (_futureExecutionTargetList != null && _futureExecutionTargetList.Count > 0)
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
                            Exceptions.Add(ex);
                        }
                    }
                    _futureExecutionTargetList?.Clear();
                }
            }
        }
        #endregion
    }
}