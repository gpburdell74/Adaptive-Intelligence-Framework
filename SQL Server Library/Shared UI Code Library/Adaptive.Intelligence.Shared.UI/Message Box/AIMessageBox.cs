using Adaptive.Intelligence.Shared.Logging;

namespace Adaptive.Intelligence.Shared.UI
{
    /// <summary>
    /// Provides the static methods / functions for executing the process of showing a Message Box in various conditions.
    /// </summary>
    public static class AIMessageBox
    {
        #region Private Static Members
        /// <summary>
        /// A <see cref="Control"/> instance used to maintain the reference to the main UI thread.
        /// </summary>
        private static ThreadAwareControl? _threadControl;
        /// <summary>
        /// The main UI thread ID value.
        /// </summary>
        private static readonly int _mainUIThreadId;
        #endregion

        #region Static Constructor		
        /// <summary>
        /// Initializes the <see cref="AIMessageBox"/> class.
        /// </summary>
        /// <remarks>
        /// This is the static constructor.
        /// </remarks>
        static AIMessageBox()
        {
            _threadControl = new ThreadAwareControl();
            _threadControl.Show();
            _threadControl.CreateControl();
            _mainUIThreadId = _threadControl.ThreadId;
        }
        #endregion

        #region Public Static Properties		
        /// <summary>
        /// Gets a value indicating whether the static instance of the <see cref="AIMessageBox"/> is initialized.
        /// </summary>
        /// <value>
        ///   <c>true</c> if initialized; otherwise, <c>false</c>.
        /// </value>
        public static bool Initialized => (_threadControl != null);
        /// <summary>
        /// Gets or sets the reference to the <see cref="IMessageBoxWindow"/> implementation to use when
        /// displaying a message box.
        /// </summary>
        /// <remarks>
        /// When this value is <b>null</b>, the standard Windows Message Box is displayed.
        /// </remarks>
        /// <value>
        /// The <see cref="IMessageBoxWindow"/> instance to use, or <b>null</b>.
        /// </value>
        public static IMessageBoxWindow? WindowToUse { get; set; }
        #endregion

        #region Static De-Initialization		
        /// <summary>
        /// De-initializes the static instance and releases internal resources.
        /// </summary>
        /// <remarks>
        /// This is used because a static instance cannot be <see cref="IDisposable"/>.
        /// </remarks>
        public static void DeInitialize()
        {
            _threadControl?.Dispose();
            _threadControl = null;

            WindowToUse?.Dispose();
            WindowToUse = null;

            GC.Collect();
        }
        #endregion

        #region Public Message Box Methods		
        /// <summary>
        /// Shows the message box with the information icon.
        /// </summary>
        /// <param name="caption">
        /// A string containing the caption text to be displayed.
        /// </param>
        /// <param name="message">
        /// A string containing the message text to be displayed.
        /// </param>
        /// <param name="buttons">
        /// A <see cref="MessageBoxButtons"/> enumerated value indicating the buttons to be displayed.
        /// </param>
        /// <returns>
        /// A <see cref="DialogResult"/> enumerated value indicating the user response.
        /// </returns>
        public static DialogResult ShowInfo(string caption, string message, MessageBoxButtons buttons)
        {
            return Show(caption, message, buttons, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Shows the message box with the information icon.
        /// </summary>
        /// <param name="parent">
        /// A reference to the <see cref="Control"/> that is the parent window of the message box, or <b>null</b>.
        /// </param>
        /// <param name="caption">
        /// A string containing the caption text to be displayed.
        /// </param>
        /// <param name="message">
        /// A string containing the message text to be displayed.
        /// </param>
        /// <param name="buttons">
        /// A <see cref="MessageBoxButtons"/> enumerated value indicating the buttons to be displayed.
        /// </param>
        /// <returns>
        /// A <see cref="DialogResult"/> enumerated value indicating the user response.
        /// </returns>
        public static DialogResult ShowInfo(Control parent, string caption, string message, MessageBoxButtons buttons)
        {
            return Show(parent, caption, message, buttons, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Shows the message box with the error icon.
        /// </summary>
        /// <param name="caption">
        /// A string containing the caption text to be displayed.
        /// </param>
        /// <param name="message">
        /// A string containing the message text to be displayed.
        /// </param>
        /// <param name="buttons">
        /// A <see cref="MessageBoxButtons"/> enumerated value indicating the buttons to be displayed.
        /// </param>
        /// <returns>
        /// A <see cref="DialogResult"/> enumerated value indicating the user response.
        /// </returns>
        public static DialogResult ShowError(string caption, string message, MessageBoxButtons buttons)
        {
            return Show(caption, message, buttons, MessageBoxIcon.Error);
        }
        /// <summary>
        /// Shows the message box with the error icon.
        /// </summary>
        /// <param name="parent">
        /// A reference to the <see cref="Control"/> that is the parent window of the message box, or <b>null</b>.
        /// </param>
        /// <param name="caption">
        /// A string containing the caption text to be displayed.
        /// </param>
        /// <param name="message">
        /// A string containing the message text to be displayed.
        /// </param>
        /// <param name="buttons">
        /// A <see cref="MessageBoxButtons"/> enumerated value indicating the buttons to be displayed.
        /// </param>
        /// <returns>
        /// A <see cref="DialogResult"/> enumerated value indicating the user response.
        /// </returns>
        public static DialogResult ShowError(Control parent, string caption, string message, MessageBoxButtons buttons)
        {
            return Show(parent, caption, message, buttons, MessageBoxIcon.Error);
        }
        /// <summary>
        /// Shows the message box with the warning icon.
        /// </summary>
        /// <param name="caption">
        /// A string containing the caption text to be displayed.
        /// </param>
        /// <param name="message">
        /// A string containing the message text to be displayed.
        /// </param>
        /// <param name="buttons">
        /// A <see cref="MessageBoxButtons"/> enumerated value indicating the buttons to be displayed.
        /// </param>
        /// <returns>
        /// A <see cref="DialogResult"/> enumerated value indicating the user response.
        /// </returns>
        public static DialogResult ShowWarning(string caption, string message, MessageBoxButtons buttons)
        {
            return Show(caption, message, buttons, MessageBoxIcon.Warning);
        }
        /// <summary>
        /// Shows the message box with the warning icon.
        /// </summary>
        /// <param name="parent">
        /// A reference to the <see cref="Control"/> that is the parent window of the message box, or <b>null</b>.
        /// </param>
        /// <param name="caption">
        /// A string containing the caption text to be displayed.
        /// </param>
        /// <param name="message">
        /// A string containing the message text to be displayed.
        /// </param>
        /// <param name="buttons">
        /// A <see cref="MessageBoxButtons"/> enumerated value indicating the buttons to be displayed.
        /// </param>
        /// <returns>
        /// A <see cref="DialogResult"/> enumerated value indicating the user response.
        /// </returns>
        public static DialogResult ShowWarning(Control parent, string caption, string message, MessageBoxButtons buttons)
        {
            return Show(parent, caption, message, buttons, MessageBoxIcon.Warning);
        }
        /// <summary>
        /// Shows the message box with the question icon.
        /// </summary>
        /// <param name="caption">
        /// A string containing the caption text to be displayed.
        /// </param>
        /// <param name="message">
        /// A string containing the message text to be displayed.
        /// </param>
        /// <param name="buttons">
        /// A <see cref="MessageBoxButtons"/> enumerated value indicating the buttons to be displayed.
        /// </param>
        /// <returns>
        /// A <see cref="DialogResult"/> enumerated value indicating the user response.
        /// </returns>
        public static DialogResult ShowQuestion(string caption, string message, MessageBoxButtons buttons)
        {
            return Show(caption, message, buttons, MessageBoxIcon.Question);
        }
        /// <summary>
        /// Shows the message box with the question icon.
        /// </summary>
        /// <param name="parent">
        /// A reference to the <see cref="Control"/> that is the parent window of the message box, or <b>null</b>.
        /// </param>
        /// <param name="caption">
        /// A string containing the caption text to be displayed.
        /// </param>
        /// <param name="message">
        /// A string containing the message text to be displayed.
        /// </param>
        /// <param name="buttons">
        /// A <see cref="MessageBoxButtons"/> enumerated value indicating the buttons to be displayed.
        /// </param>
        /// <returns>
        /// A <see cref="DialogResult"/> enumerated value indicating the user response.
        /// </returns>
        public static DialogResult ShowQuestion(Control parent, string caption, string message, MessageBoxButtons buttons)
        {
            return Show(parent, caption, message, buttons, MessageBoxIcon.Question);
        }
        /// <summary>
        /// Shows the message box with OK and Cancel buttons.
        /// </summary>
        /// <param name="caption">
        /// A string containing the caption text to be displayed.
        /// </param>
        /// <param name="message">
        /// A string containing the message text to be displayed.
        /// </param>
        /// <param name="icon">
        /// A <see cref="MessageBoxIcon"/> enumerated value indicating the icon to be displayed.
        /// </param>
        /// <returns>
        /// A <see cref="DialogResult"/> enumerated value indicating the user response.
        /// </returns>
        public static DialogResult ShowOkCancel(string caption, string message, MessageBoxIcon icon)
        {
            return Show(caption, message, MessageBoxButtons.OKCancel, icon);
        }
        /// <summary>
        /// Shows the message box with OK and Cancel buttons.
        /// </summary>
        /// <param name="parent">
        /// A reference to the <see cref="Control"/> that is the parent window of the message box, or <b>null</b>.
        /// </param>
        /// <param name="caption">
        /// A string containing the caption text to be displayed.
        /// </param>
        /// <param name="message">
        /// A string containing the message text to be displayed.
        /// </param>
        /// <param name="icon">
        /// A <see cref="MessageBoxIcon"/> enumerated value indicating the icon to be displayed.
        /// </param>
        /// <returns>
        /// A <see cref="DialogResult"/> enumerated value indicating the user response.
        /// </returns>
        public static DialogResult ShowOkCancel(Control parent, string caption, string message, MessageBoxIcon icon)
        {
            return Show(parent, caption, message, MessageBoxButtons.OKCancel, icon);
        }
        /// <summary>
        /// Shows the message box with Yes and No buttons and a question mark icon.
        /// </summary>
        /// <param name="caption">
        /// A string containing the caption text to be displayed.
        /// </param>
        /// <param name="message">
        /// A string containing the message text to be displayed.
        /// </param>
        /// <returns>
        /// A <see cref="DialogResult"/> enumerated value indicating the user response.
        /// </returns>
        public static DialogResult ShowYesNo(string caption, string message)
        {
            return Show(caption, message, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
        /// <summary>
        /// Shows the message box with Yes and No buttons and a question mark icon.
        /// </summary>
        /// <param name="parent">
        /// A reference to the <see cref="Control"/> that is the parent window of the message box, or <b>null</b>.
        /// </param>
        /// <param name="caption">
        /// A string containing the caption text to be displayed.
        /// </param>
        /// <param name="message">
        /// A string containing the message text to be displayed.
        /// </param>
        /// <returns>
        /// A <see cref="DialogResult"/> enumerated value indicating the user response.
        /// </returns>
        public static DialogResult ShowYesNo(Control parent, string caption, string message)
        {
            return Show(parent, caption, message, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
        /// <summary>
        /// Shows the message box with Yes, No, and Cancel buttons and a question mark icon.
        /// </summary>
        /// <param name="caption">
        /// A string containing the caption text to be displayed.
        /// </param>
        /// <param name="message">
        /// A string containing the message text to be displayed.
        /// </param>
        /// <returns>
        /// A <see cref="DialogResult"/> enumerated value indicating the user response.
        /// </returns>
        public static DialogResult ShowYesNoCancel(string caption, string message)
        {
            return Show(caption, message, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        }
        /// <summary>
        /// Shows the message box with Yes, No, and Cancel buttons and a question mark icon.
        /// </summary>
        /// <param name="parent">
        /// A reference to the <see cref="Control"/> that is the parent window of the message box, or <b>null</b>.
        /// </param>
        /// <param name="caption">
        /// A string containing the caption text to be displayed.
        /// </param>
        /// <param name="message">
        /// A string containing the message text to be displayed.
        /// </param>
        /// <returns>
        /// A <see cref="DialogResult"/> enumerated value indicating the user response.
        /// </returns>
        public static DialogResult ShowYesNoCancel(Control parent, string caption, string message)
        {
            return Show(parent, caption, message, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        }
        /// <summary>
        /// Displays the message box with the specified parameters.
        /// </summary>
        /// <param name="caption">
        /// A string containing the caption text to be displayed.
        /// </param>
        /// <param name="message">
        /// A string containing the message text to be displayed.
        /// </param>
        /// <param name="buttons">
        /// A <see cref="MessageBoxButtons"/> enumerated value indicating the buttons to be displayed.
        /// </param>
        /// <param name="icon">
        /// A <see cref="MessageBoxIcon"/> enumerated value indicating the icon to be displayed.
        /// </param>
        /// <returns>
        /// A <see cref="DialogResult"/> enumerated value indicating the user response.
        /// </returns>
        public static DialogResult Show(string caption, string message, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return Show(_threadControl, caption, message, buttons, icon);
        }
        /// <summary>
        /// Displays the message box with the specified parameters.
        /// </summary>
        /// <param name="ownerWindow">
        /// A reference to the <see cref="Control"/> that is the parent window of the message box, or <b>null</b>.
        /// </param>
        /// <param name="caption">
        /// A string containing the caption text to be displayed.
        /// </param>
        /// <param name="message">
        /// A string containing the message text to be displayed.
        /// </param>
        /// <param name="buttons">
        /// A <see cref="MessageBoxButtons"/> enumerated value indicating the buttons to be displayed.
        /// </param>
        /// <param name="icon">
        /// A <see cref="MessageBoxIcon"/> enumerated value indicating the icon to be displayed.
        /// </param>
        /// <returns>
        /// A <see cref="DialogResult"/> enumerated value indicating the user response.
        /// </returns>
        public static DialogResult Show(Control? ownerWindow, string caption, string message, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            // Do nothing if the owner window object is not specified and the local static control was not created.
            ownerWindow ??= _threadControl;
            if (ownerWindow == null)
                return DialogResult.None;

            DialogResult userResult;

            // If not on the main UI thread, push the execution to the main thread.
            if (!IsInMainThread(ownerWindow))
            {
                // Create the method to call.
                MessageBoxAction target = CreateAction();
                userResult = ShowAndWait(ownerWindow, target, caption, message, buttons, icon);
            }
            else
            {
                if (WindowToUse != null)
                {
                    WindowToUse.Caption = caption;
                    WindowToUse.Message = message;
                    WindowToUse.Buttons = buttons;
                    WindowToUse.MessageIcon = icon;
                    userResult = WindowToUse.Show();
                }
                else
                    // If in the main thread, just perform the task.
                    userResult = MessageBox.Show(ownerWindow, message, caption, buttons, icon);
            }
            return userResult;
        }
        #endregion

        #region Private Static Methods / Functions				
        /// <summary>
        /// Creates the method / action to be executed.
        /// </summary>
        /// <returns>
        /// A <see cref="MessageBoxAction"/> delegate pointing to the code to be executed in the main thread.
        /// </returns>
        private static MessageBoxAction CreateAction()
        {
            return new MessageBoxAction(
                (messageBoxOwnerWindow,
                    messageBoxCaption,
                    messageBoxMessage,
                    MessageBoxButtons,
                    MessageBoxIconToUse) =>
                {
                    DialogResult result = DialogResult.TryAgain;

                    try
                    {
                        // If a substitute is supplied, use that one.
                        if (WindowToUse != null)
                        {
                            WindowToUse.Caption = messageBoxCaption;
                            WindowToUse.Message = messageBoxMessage;
                            WindowToUse.Buttons = MessageBoxButtons;
                            WindowToUse.MessageIcon = MessageBoxIconToUse;
                            result = WindowToUse.Show();
                        }
                        else
                        {
                            // Otherwise, show the standard Windows message box.
                            result = MessageBox.Show(
                                messageBoxOwnerWindow,
                                messageBoxMessage,
                                messageBoxCaption,
                                MessageBoxButtons,
                                MessageBoxIconToUse);
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionLog.LogException(ex);
                    }
                    return result;
                });
        }
        /// <summary>
        /// Determines whether the current thread is the main UI thread.
        /// </summary>
        /// <param name="control">
        /// The <see cref="Control"/> implementation to be checked.
        /// </param>
        /// <returns>
        ///   <c>true</c> if the current thread is the main UI thread; otherwise, <b>false</b>.
        /// </returns>
        private static bool IsInMainThread(Control control)
        {
            return ((!control.InvokeRequired) && Environment.CurrentManagedThreadId == _mainUIThreadId);
        }
        /// <summary>
        /// Shows the message box in a thread-safe fashion and waits for the user response.
        /// </summary>
        /// <param name="ownerWindow">
        /// A reference to the <see cref="Control"/> that is the parent window of the message box, or <b>null</b>.
        /// </param>
        /// <param name="target">
        /// A <see cref="MessageBoxAction"/> delegate serving as the function pointer to be called by the main UI thread when it is ready.
        /// </param>
        /// <param name="caption">
        /// A string containing the caption text to be displayed.
        /// </param>
        /// <param name="message">
        /// A string containing the message text to be displayed.
        /// </param>
        /// <param name="buttons">
        /// A <see cref="MessageBoxButtons"/> enumerated value indicating the buttons to be displayed.
        /// </param>
        /// <param name="icon">
        /// A <see cref="MessageBoxIcon"/> enumerated value indicating the icon to be displayed.
        /// </param>
        /// <returns>
        /// A <see cref="DialogResult"/> enumerated value indicating the user response.
        /// </returns>
        private static DialogResult ShowAndWait(Control ownerWindow, MessageBoxAction target, string caption, string message, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            DialogResult userResult = DialogResult.Abort;

            // Send the message to the window to perform the function.
            IAsyncResult result = ownerWindow.BeginInvoke(() =>
            {
                userResult = target(ownerWindow, caption, message, buttons, icon);
            });

            // Wait for the result to complete.
            while (!result.IsCompleted)
            {
                result.AsyncWaitHandle.WaitOne(75);
                if (Environment.CurrentManagedThreadId == _mainUIThreadId)
                {
                    // Only invoke this when in the UI thread.
                    Application.DoEvents();
                }
            }

            // End the invocation.
            ownerWindow.EndInvoke(result);
            result.AsyncWaitHandle.Dispose();

            return userResult;
        }
        #endregion
    }
}
