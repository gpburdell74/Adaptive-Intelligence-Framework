namespace Adaptive.Intelligence.Shared.UI
{
    /// <summary>
    /// Provides the signature definition for text box behavior implementations.
    /// </summary>
    public interface ITextCharacterBehavior
    {
        /// <summary>
        /// Determines whether the specified key can be accepted by the text box.
        /// </summary>
        /// <param name="enteredChar">
        /// The <see cref="char"/> entered by the user.
        /// </param>
        /// <returns>
        ///   <c>true</c> if the specified character is acceptable; otherwise, <c>false</c>.
        /// </returns>
        bool CanAcceptKey(char enteredChar);
        /// <summary>
        /// Ensures the text format is applied.
        /// </summary>
        /// <param name="currentText">
        /// A string containing the current text value.
        /// </param>
        /// <returns>
        /// A string containing the properly formatted text.
        /// </returns>
        string EnsureFormat(string currentText);
        /// <summary>
        /// Processes the character at the specified text location.
        /// </summary>
        /// <param name="enteredCharacter">
        /// The <see cref="char"/> entered by the user.
        /// </param>
        /// <param name="currentText">
        /// A string containing the current text value of the specified text box.
        /// </param>
        /// <param name="currentCursorPosition">
        /// An integer indicating the current cursor position.
        /// </param>
        /// <returns>
        /// A <see cref="TextCharacterOpResult"/> containing the recommended response and values
        /// for the calling text box.
        /// </returns>
        TextCharacterOpResult ProcessCharacterAtLocation(char enteredCharacter, string currentText,
            int currentCursorPosition);
    }
}
