namespace Adaptive.Intelligence.Shared.UI
{
    /// <summary>
    /// Provides a text box implementation for entering US phone number values.
    /// </summary>
    /// <seealso cref="TextBox" />
    public class PhoneNumberTextBox : TextBox
    {
        #region Private Member Declarations
        /// <summary>
        /// The default phone number value.
        /// </summary>
        private const string PlaceholderValue = "(000) 000 - 0000";
        /// <summary>
        /// The behavior to implement.
        /// </summary>
        private PhoneNumberTextBehavior? _behavior;
        #endregion

        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneNumberTextBox"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public PhoneNumberTextBox()
        {
            _behavior = new PhoneNumberTextBehavior();
            PlaceholderText = PlaceholderValue;
        }
        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Control" /> and its child controls and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources. </param>
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed && disposing)
            {
                _behavior?.Dispose();
            }

            _behavior = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets a value indicating whether the content entry is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the phone number in this instance valid; otherwise, <c>false</c>.
        /// </value>
        public bool IsValid
        {
            get
            {
                USPhoneNumber number = new USPhoneNumber(Text);
                return !number.IsNaPN;
            }
        }
        #endregion

        #region Protected Method Overrides
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.TextChanged" /> event.
        /// </summary>
        /// <remarks>
        /// This is used to ensure pasted and other invalid content does not get into the box.
        /// </remarks>
        /// <param name="e">
        /// An <see cref="T:System.EventArgs" /> that contains the event data.
        /// </param>
        protected override void OnTextChanged(EventArgs e)
        {
            int cursorPos = SelectionStart;

            base.OnTextChanged(e);
            Text = _behavior?.EnsureFormat(Text);

            SelectionStart = cursorPos;
        }
        /// <summary>
        /// Raises the <see cref="Control.KeyPress" /> event.
        /// </summary>
        /// <remarks>
        /// This is used to filter the characters typed by the user.
        /// </remarks>
        /// <param name="e">
        /// The <see cref="KeyPressEventArgs"/> instance containing the event data.
        /// </param>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)8) || (e.KeyChar == (char)13))
                e.Handled = false;
            else
            {
                if (_behavior != null)
                {
                    TextCharacterOpResult result = _behavior.ProcessCharacterAtLocation(e.KeyChar, Text, SelectionStart);
                    if (result.NewText != null)
                    {
                        Text = result.NewText;
                        SelectionStart = result.CursorPosition;
                    }

                    e.Handled = result.SetEventToHandled;
                }
                else
                    base.OnKeyPress(e);
            }
        }
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Gets the phone number from the text entry.
        /// </summary>
        /// <returns>
        /// A <see cref="USPhoneNumber"/> instance containing the parsed content of the
        /// <see cref="Control.Text"/> value.
        /// </returns>
        public USPhoneNumber GetNumber()
        {
            return new USPhoneNumber(Text);
        }
        #endregion
    }
}
