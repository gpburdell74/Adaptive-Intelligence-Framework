namespace Adaptive.Intelligence.Shared.UI
{
	/// <summary>
	/// Provides a method delegate for displaying a message box.
	/// </summary>
	/// <summary>
	/// Displays the message box with the specified parameters.
	/// </summary>
	/// <param name="parent">
	/// A reference to the <see cref="IWin32Window"/> that is the parent window of the message box, or <b>null</b>.
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
	public delegate DialogResult MessageBoxAction(IWin32Window? parent, string caption, string message, MessageBoxButtons buttons, MessageBoxIcon icon);
}