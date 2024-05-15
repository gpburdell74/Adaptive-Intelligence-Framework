using System.Text;

namespace Adaptive.Intelligence.Shared.UI
{
    /// <summary>
    /// Provides the text box behavior implementation for a US phone-number entry text box.
    /// </summary>
    /// <seealso cref="ITextCharacterBehavior" />
    public sealed class PhoneNumberTextBehavior : DisposableObjectBase, ITextCharacterBehavior
    {
        #region Private Member Declarations
        /// <summary>
        /// The list of accepted characters.
        /// </summary>
        private List<char>? _acceptedChars;
        #endregion

        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneNumberTextBehavior"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public PhoneNumberTextBehavior()
        {
            _acceptedChars = new List<char>
            {
                ((char)8),
                '1', '2', '3', '4', '5', '6', '7', '8', '9', '0',
                ' ', '-', '(', ')'
            };
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <b>true</b> to release both managed and unmanaged resources;
        /// <b>false</b> to release only unmanaged resources.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed && disposing)
            {
                _acceptedChars?.Clear();
            }

            _acceptedChars = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Determines whether the specified key can be accepted by the text box.
        /// </summary>
        /// <param name="enteredChar">The <see cref="char" /> entered by the user.</param>
        /// <returns>
        ///   <c>true</c> if the specified character is acceptable; otherwise, <c>false</c>.
        /// </returns>
        public bool CanAcceptKey(char enteredChar)
        {
            if (_acceptedChars == null)
                return false;
            
            return _acceptedChars.Contains(enteredChar);
        }
        /// <summary>
        /// Ensures the text format is applied.
        /// </summary>
        /// <param name="currentText">A string containing the current text value.</param>
        /// <returns>
        /// A string containing the properly formatted text.
        /// </returns>
        public string EnsureFormat(string currentText)
        {
            string data = string.Empty;
            if (!string.IsNullOrEmpty(currentText))
            {
                // 1. Extract just the numeric values.
                StringBuilder numbersOnly = new StringBuilder(20);
                foreach (char c in currentText)
                {
                    if (char.IsNumber(c))
                        numbersOnly.Append(c);
                }

                string numbers = numbersOnly.ToString();

                // 2. Re-add the numbers, in the appropriate character locations with the formatting characters.
                //      Do not assume the entire number is provided.  Stop formatting when the list of numbers
                //      ends.
                //
                //      Format: (XXX) XXX - XXXX
                StringBuilder newNumber = new StringBuilder();
                newNumber.Append(Constants.OpenParen);
                int length = numbers.Length;

                for (int pos = 0; pos < length; pos++)
                {
                    switch (pos)
                    {
                        // (404) - First 3 digits = area code
                        case 3:
                            newNumber.Append(Constants.CloseParen + Constants.Space);
                            newNumber.Append(numbers[pos]);
                            break;

                        // (404) 123 - Second 3 digits = prefix
                        case 6:
                            newNumber.Append( Constants.Space + Constants.Dash + Constants.Space);
                            newNumber.Append(numbers[pos]);
                            break;

                        // Remaining 4 digits - ignore anything remaining after tha.
                        default:
                            if (newNumber.Length < 16)
                                newNumber.Append(numbers[pos]);
                            break;
                    }
                }

                data = newNumber.ToString();
            }

            return data;
        }
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
        public TextCharacterOpResult ProcessCharacterAtLocation(char enteredCharacter, string currentText,
            int currentCursorPosition)
        {
            TextCharacterOpResult result = new TextCharacterOpResult();

            if (CanAcceptKey(enteredCharacter))
            {
                result.NewText = EnsureFormat(currentText + enteredCharacter);
                result.CursorPosition = (result.NewText.Length + 1);
            }

            return result;
        }
        #endregion
    }
}