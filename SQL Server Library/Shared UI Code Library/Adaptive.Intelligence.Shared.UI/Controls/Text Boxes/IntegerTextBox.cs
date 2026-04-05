using Adaptive.Intelligence.Shared.Logging;
using System.ComponentModel;

namespace Adaptive.Intelligence.Shared.UI
{
    /// <summary>
    /// Provides a text box that only accepts integer values.
    /// </summary>
    /// <seealso cref="TextBox" />
    [DefaultBindingProperty("Value")]
    [Description("Provides a text editing feature for integer values.")]
    public sealed class IntegerTextBox : TextBox
    {
        #region Public Properties
        /// <summary>
        /// Gets or sets the integer value for the text box.
        /// </summary>
        /// <value>
        /// An integer indicating the value represented in the text box.
        /// </value>
        [Browsable(true),
         Description("Gets or sets the value for the text box."),
         Category("Data"),
         DefaultValue(0)]
        public int Value
        {
            get
            {
                if (string.IsNullOrEmpty(Text))
                    Text = Constants.Zero;

                int returnValue = 0;
                try
                {
                    returnValue = Convert.ToInt32(Text);
                }
                catch (Exception ex)
                {
                    ExceptionLog.LogException(ex);
                }
                return returnValue;
            }
            set
            {
                Text = value.ToString();
                Invalidate();
            }
        }
        #endregion

        #region Protected Method Overrides
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
            // If the user pressed backspace or a number, allow the base class to process it.
            // Otherwise, cancel the event.
            if (e.KeyChar == Constants.BackspaceChar || char.IsNumber(e.KeyChar))
            {
                base.OnKeyPress(e);
            }
            else
                e.Handled = true;
        }
        #endregion
    }
}
