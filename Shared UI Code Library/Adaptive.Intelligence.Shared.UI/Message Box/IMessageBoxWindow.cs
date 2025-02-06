namespace Adaptive.Intelligence.Shared.UI
{
    /// <summary>
    /// Provides the signature definition for a window to be used as a MessageBox replacement.
    /// </summary>
    /// <seealso cref="IContainerControl" />
    /// <seealso cref="IWin32Window" />
    /// <seealso cref="IDisposable" />
    public interface IMessageBoxWindow : IContainerControl, IWin32Window, IDisposable
    {
        #region Properties		
        /// <summary>
        /// Gets or sets the buttons to be displayed on the message box.
        /// </summary>
        /// <value>
        /// The <see cref="MessageBoxButtons"/> enumeration indicating the buttons to display.
        /// </value>
        MessageBoxButtons Buttons { get; set; }
        /// <summary>
        /// Gets or sets the caption text for the message box.
        /// </summary>
        /// <value>
        /// A string containing the caption text.
        /// </value>
        string? Caption { get; set; }
        /// <summary>
        /// Gets or sets the message text for the message box.
        /// </summary>
        /// <value>
        /// A string containing the message text.
        /// </value>
        string? Message { get; set; }
        /// <summary>
        /// Gets or sets the icon to be displayed in the message box.
        /// </summary>
        /// <value>
        /// The <see cref="MessageBoxIcon"/> enumerated value indicating the icon to be displayed.
        /// </value>
        MessageBoxIcon MessageIcon { get; set; }
        #endregion

        #region Methods		
        /// <summary>
        /// Shows the message box window and returns the user response.
        /// </summary>
        /// <returns>
        /// A <see cref="DialogResult"/> enumerated value indicating the user response.
        /// </returns>
        DialogResult Show();
        #endregion
    }
}
