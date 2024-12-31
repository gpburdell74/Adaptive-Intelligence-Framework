namespace Adaptive.Intelligence.Shared.UI
{
    /// <summary>
    /// Provides the signature definition for a IoC / dependency injection container for creating and Windows
    /// dialogs and other OS-related dialogs.
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// Gets the reference to a UI control created on the main thread used to perform Invoke operations.
        /// </summary>
        /// <value>
        /// A <see cref="Control"/> instance.
        /// </value>
        Control UIControl { get; }
        /// <summary>
        /// Gets the managed ID for the UI thread.
        /// </summary>
        /// <value>
        /// An integer specifying the managed thread identifier.
        /// </value>
        int ManagedUIThreadId { get;  }
        /// <summary>
        /// Gets or sets the reference to the services provider instance.
        /// </summary>
        /// <value>
        /// The <see cref="IServiceProvider"/> instance for the application.
        /// </value>
        IServiceProvider Services { get; }

        /// <summary>
        /// Continues execution in the context of the main UI thread.
        /// </summary>
        /// <param name="target">
        /// An <see cref="Action"/> delegate pointing to the code content to execute
        /// in the main thread.
        /// </param>
        void ContinueInMainThread(Action target);

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
        DialogResult DisplayMessageBox(string caption, string message);

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
        DialogResult DisplayMessageBox(string caption, string message, MessageBoxButtons buttons, MessageBoxIcon icon);
    }
}
