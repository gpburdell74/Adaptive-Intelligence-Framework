using Adaptive.Intelligence.Shared.Logging;
using System.ComponentModel;

namespace Adaptive.Intelligence.Shared.UI
{
    /// <summary>
    /// Provides a UI element for displaying a header on the top of application dialogs.
    /// </summary>
    public class DialogHeaderControl : Panel
    {
        #region Private Member Declarations
        /// <summary>
        /// The display image instance.
        /// </summary>
        private Image? _displayImage;
        /// <summary>
        /// The caption text.
        /// </summary>
        private string? _caption;
        /// <summary>
        /// The instructions text.
        /// </summary>
        private string? _instructions;
        /// <summary>
        /// Font instance.
        /// </summary>
        private Font? _captionFont;
        /// <summary>
        /// Font instance.
        /// </summary>
        private Font? _instructionsFont;

        private const int _height = 64;
        #endregion

        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="DialogHeaderControl"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public DialogHeaderControl()
        {
            SetStartObjects();
        }
        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="System.Windows.Forms.Control" /> and its child controls and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed && disposing)
            {
                _captionFont?.Dispose();
                _instructionsFont?.Dispose();
                _displayImage?.Dispose();
            }

            _displayImage = null;
            _captionFont = null;
            _instructionsFont = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the caption / title text to be displayed.
        /// </summary>
        /// <value>
        /// A string containing the caption / title text to be displayed.
        /// </value>
        [Browsable(true),
         Description("The caption / title text to be displayed."),
         Category("Appearance")]
        public string? Caption
        {
            get => _caption;
            set
            {
                _caption = value;
                Invalidate();
            }
        }
        /// <summary>
        /// Gets or sets the reference to the image to be displayed.
        /// </summary>
        /// <value>
        /// An <see cref="Image"/> to display, or <b>null</b> for no image.
        /// </value>
        [Browsable(true),
         Description("The icon or image to display on the dialog header."),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
         Category("Appearance")]
        public Image? DisplayImage
        {
            get => _displayImage;
            set
            {
                _displayImage = value;
                Invalidate();
            }
        }
        /// <summary>
        /// Gets or sets the instructions text to be displayed.
        /// </summary>
        /// <value>
        /// A string containing the instructions text to be displayed.
        /// </value>
        [Browsable(true),
         Description("The supplemental description text to be displayed."),
         Category("Appearance")]
        public string? Instructions
        {
            get => _instructions;
            set
            {
                _instructions = value;
                Invalidate();
            }
        }
        /// <summary>
        /// Gets or sets the height of the control.
        /// </summary>
        /// <value>
        /// The height of the control, in pixels.  This will always be a constant value of 64px.
        /// </value>
        [Browsable(false),
         Description("The height of the control, in pixels."),
        Category("Layout")]
        public new int Height
        {
            get => _height;
            // ReSharper disable once ValueParameterNotUsed
            set => base.Height = _height;
        }
        /// <summary>
        /// Gets or sets the height and width of the control.
        /// </summary>
        /// <value>
        /// A <see cref="Size"/> structure specifying the height and width of the control.
        /// </value>
        [Browsable(true),
         Description("The size of the control, in pixels."),
        Category("Layout")]
        public new Size Size
        {
            get
            {
                Size s = base.Size;
                s.Height = _height;
                return s;
            }
            set
            {
                Size s = value;
                s.Height = _height;
                base.Size = s;
            }
        }
        #endregion

        #region Protected Method Overrides
        /// <summary>
        /// Raises the <see cref="System.Windows.Forms.Control.Paint" /> event and performs the drawing tasks on the control.
        /// </summary>
        /// <param name="e">A <see cref="System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            EnsureFonts();

            // Read property once.
            Graphics g = e.Graphics;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            if (_displayImage != null)
            {
                int width = 0;
                try
                {
                    width = _displayImage.Width;
                    if (width > 32)
                        width = 32;
                }
                catch (Exception ex)
                {
                    ExceptionLog.LogException(ex);
                }

                DrawImage(g);
                DrawCaption(g, width + 10);
                DrawInstructions(g, width + 10);
            }
            else
            {
                DrawCaption(g, 5);
                DrawInstructions(g, 5);
            }

            g.DrawLine(SystemPens.ControlDark, 0, Height - 1, Width, Height - 1);
        }
        /// <summary>
        /// Fires the event indicating that the panel has been resized. Inheriting controls should use this in favor of actually listening to the event, but should still call base.onResize to ensure that the event is fired for external listeners.
        /// </summary>
        /// <param name="eventargs">An <see cref="EventArgs" /> that contains the event data.</param>
        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            Height = _height;
            Invalidate();
        }
        #endregion

        #region Private Methods / Functions
        /// <summary>
        /// Draws the image.
        /// </summary>
        /// <param name="g">
        /// The <see cref="Graphics"/> instance to use in drawing the image.
        /// </param>
        private void DrawImage(Graphics g)
        {
            if (_displayImage != null)
            {
                try
                {
                    int width = _displayImage.Width;
                    int height = _displayImage.Height;
                    if (width > 32)
                        width = 32;
                    if (height > 32)
                        height = 32;

                    g.DrawImage(_displayImage, new Rectangle(5, 5, width + 5, height + 5));
                }
                catch (Exception ex)
                {
                    ExceptionLog.LogException(ex);
                }
            }
        }
        /// <summary>
        /// Draws the caption.
        /// </summary>
        /// <param name="g">
        /// The <see cref="Graphics"/> instance to use in drawing the text.
        /// </param>
        /// <param name="left">
        /// The left co-ordinate at which to draw the text.
        /// </param>
        private void DrawCaption(Graphics g, int left)
        {
            Point drawingStart = new Point(left, 5);
            if ((_captionFont != null) && (_caption != null))
            {
                TextFormatFlags formatFlags = TextFormatFlags.Top | TextFormatFlags.Left;
                TextRenderer.DrawText(g, _caption, _captionFont, drawingStart, Color.Black, formatFlags);
            }
        }
        /// <summary>
        /// Draws the instructions.
        /// </summary>
        /// <param name="g">
        /// The <see cref="Graphics"/> instance to use in drawing the text.
        /// </param>
        /// <param name="left">
        /// The left co-ordinate at which to draw the text.
        /// </param>
        private void DrawInstructions(Graphics g, int left)
        {
            EnsureFonts();
            if (_captionFont != null)
            {
                SizeF captionSize = g.MeasureString(_caption, _captionFont);
                Point drawingStart = new Point(left, (int)captionSize.Height + 7);
                if (!string.IsNullOrEmpty(_instructions))
                {
                    Rectangle rect = new Rectangle(drawingStart.X, drawingStart.Y, ClientRectangle.Width - drawingStart.X - 10, Height - drawingStart.Y);

                    TextFormatFlags formatFlags = TextFormatFlags.Top | TextFormatFlags.Left | TextFormatFlags.WordBreak;
                    TextRenderer.DrawText(g, _instructions, _instructionsFont, rect, Color.Black, formatFlags);
                }
            }
        }
        /// <summary>
        /// Ensures the fonts objects exist.
        /// </summary>
        private void EnsureFonts()
        {
            if (_captionFont == null)
                _captionFont = new Font("Segoe UI", 10f, FontStyle.Bold);
            if (_instructionsFont == null)
                _instructionsFont = new Font("Segoe UI", 8.25f, FontStyle.Regular);
        }
        private void SetStartObjects()
        {
            BackColor = Color.White;
            ForeColor = Color.Black;
            EnsureFonts();
        }
        #endregion
    }
}
