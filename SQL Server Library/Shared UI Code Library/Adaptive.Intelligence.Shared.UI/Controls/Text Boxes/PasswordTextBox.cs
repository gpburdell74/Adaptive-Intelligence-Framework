using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Adaptive.Intelligence.Shared.UI
{
    /// <summary>
    /// Provides a text box for entering a password while allowing the user to hide or view the password.
    /// </summary>
    public partial class PasswordTextBox : AdaptiveControlBase
    {
        #region Constructor / Dispose Methods        
        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordTextBox"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public PasswordTextBox()
        {
            InitializeComponent();
        }
        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed && disposing)
            {
                components?.Dispose();
            }

            components = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties        
        /// <summary>
        /// Gets or sets the font of the text displayed by the control.
        /// </summary>
        /// <value>
        /// The <see cref="Font"/> to be used.
        /// </value>
        [Category("Appearance"),
         Localizable(true),
         AmbientValue(null),
         Description(""),
         AllowNull]
        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
                if (IsHandleCreated)
                {
                    PasswordText.Font = value;
                    ViewHideButton.Font = value;
                    Invalidate();
                    Height = PasswordText.Height;
                    Invalidate();
                }
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether only numeric characters are allowed in the text box.
        /// </summary>
        /// <value>
        /// <b>true</b> if only numeric characters are allowed in the text box; otherwise, <b>false</b>.
        /// the focus.
        /// </value>
        [Localizable(true)]
        [DefaultValue(false)]
        [Description("Indicates whether only numeric characters are allowed in the text box.")]
        [Category("Behavior")]
        public bool NumericOnly { get; set; }
        /// <summary>
        /// Gets or sets the text that is displayed when the control has no text and does not have the focus.
        /// </summary>
        /// <value>
        /// A string containing the text that is displayed when the control has no text and does not have 
        /// the focus.
        /// </value>
        [Localizable(true)]
        [DefaultValue("")]
        [Description("The text that is displayed when the control has no text and does not have focus.")]
        [AllowNull]
        public string PlaceholderText
        {
            get => PasswordText.PlaceholderText;
            set
            {
                PasswordText.PlaceholderText = value;
                Invalidate();
            }
        }
        /// <summary>
        ///  Gets or sets the current text in the text box.
        /// </summary>
        /// <value>
        /// A string containing the text of the password value.
        /// </value>
        [AllowNull]
        public override string Text
        {
            get => PasswordText.Text;
            set
            {
                PasswordText.Text = value;
                Invalidate();
            }
        }
        #endregion

        #region Protected Method Overrides
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.GotFocus" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnGotFocus(EventArgs e)
        {
            PasswordText.Focus();
            PasswordText.SelectionStart = 0;
            PasswordText.SelectionLength = PasswordText.Text.Length;
        }
        /// <summary>
        /// Raises the <see cref="E:HandleCreated" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            Height = PasswordText.Height;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (IsHandleCreated)
            {
                Height = PasswordText.Height;
                Invalidate();
            }
        }
        /// <summary>
        /// Assigns the event handlers for the controls on the dialog.
        /// </summary>
        protected override void AssignEventHandlers()
        {
            ViewHideButton.Click += HandleViewHideClicked;
            PasswordText.TextChanged += HandleTextChanged;
            PasswordText.KeyPress += HandleKeyPress;


            ViewHideButton.LostFocus += HandleLostFocus;
            PasswordText.LostFocus += HandleLostFocus;
        }
        /// <summary>
        /// Removes the event handlers for the controls on the dialog.
        /// </summary>
        protected override void RemoveEventHandlers()
        {
            ViewHideButton.Click -= HandleViewHideClicked;
            PasswordText.TextChanged -= HandleTextChanged;
            PasswordText.KeyPress -= HandleKeyPress;

            ViewHideButton.LostFocus -= HandleLostFocus;
            PasswordText.LostFocus -= HandleLostFocus;
        }
        #endregion

        #region Private Event Handlers
        /// <summary>
        /// Handles the event when the text box or button loses focus.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void HandleLostFocus(object? sender, EventArgs e)
        {
            // Raise the lost focus event for the control if neither of the sub-controls has focus.
            if (!ViewHideButton.Focused && !PasswordText.Focused)
                OnLostFocus(e);
        }
        /// <summary>
        /// Handles the event when the View/Hide button is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void HandleViewHideClicked(object? sender, EventArgs e)
        {
            if (IsHandleCreated)
            {
                if (PasswordText.PasswordChar == '*')
                {
                    PasswordText.PasswordChar = Constants.NullChar;
                    ViewHideButton.Image = Properties.Resources.HideItem;
                }
                else
                {
                    PasswordText.PasswordChar = '*';
                    ViewHideButton.Image = Properties.Resources.ViewItem;
                }
            }
        }
        /// <summary>
        /// Handles the event when the text changes in the text box.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void HandleTextChanged(object? sender, EventArgs e)
        {
            OnTextChanged(e);
        }
        /// <summary>
        /// Handles the event when a key is pressed in the text box.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void HandleKeyPress(object? sender, KeyPressEventArgs e)
        {
            if (NumericOnly)
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                    e.Handled = true;
            }
        }
        #endregion
    }
}
