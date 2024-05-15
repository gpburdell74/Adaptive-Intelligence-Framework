using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace Adaptive.Intelligence.Shared.UI
{
    /// <summary>
    /// Provides a simple gauge-style control for displaying numbers.
    /// </summary>
    /// <seealso cref="UserControl" />
    public class SimpleGaugeControl : UserControl
    {
        #region Public Events
        /// <summary>
        /// Occurs when the value is changed.
        /// </summary>
        public event EventHandler? ValueChanged;
        #endregion

        #region Private Member Declarations
        /// <summary>
        /// The value to be displayed.
        /// </summary>
        private int _value;
        /// <summary>
        /// The border pen.
        /// </summary>
        private Pen? _borderPen;
        /// <summary>
        /// The back color brush.
        /// </summary>
        private SolidBrush? _backColorBrush;
        /// <summary>
        /// The drawing font.
        /// </summary>
        private Font? _font;
        #endregion

        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleGaugeControl"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public SimpleGaugeControl()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.CacheText, true);
            SetStyle(ControlStyles.ContainerControl, false);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.Selectable, false);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            MyInitializeComponent();
        }
        /// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Control" /> and its child controls and optionally releases the managed resources.</summary>
        /// <param name="disposing">
        /// <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources. </param>
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed && disposing)
            {
                _font?.Dispose();
                _backColorBrush?.Dispose();
                _borderPen?.Dispose();
            }

            _borderPen = null;
            _font = null;
            _backColorBrush = null;
            base.Dispose(false);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the background color for the control.
        /// </summary>
        /// <returns>
        /// A <see cref="Color" /> that represents the background color of the control. The default is the value of
        /// the <see cref="Control.DefaultBackColor" /> property.
        /// </returns>
        public override Color BackColor
        {
            get => base.BackColor;
            set
            {
                base.BackColor = value;
                _backColorBrush?.Dispose();
                _backColorBrush = new SolidBrush(value);
                Invalidate();
            }
        }
        /// <summary>
        /// Gets or sets the value to be displayed.
        /// </summary>
        /// <value>
        /// An integer value to be displayed.
        /// </value>
        [Browsable(true),
         Description("The integer value to be displayed"),
         Category("Data"),
         DefaultValue(0)]
        public int Value
        {
            get => _value;
            set
            {
                if (value != _value)
                {
                    _value = value;
                    Invalidate();
                    ValueChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        /// <summary>
        /// Gets or sets the text associated with this control.
        /// </summary>
        /// <returns>
        /// The text associated with this control.
        /// </returns>
        public override string Text => _value.ToString();
        #endregion

        #region Protected Method Overrides
        /// <summary>
        /// Paints the background of the control.
        /// </summary>
        /// <param name="e">
        /// A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.
        /// </param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            if (_backColorBrush != null)
                e.Graphics.FillRectangle(_backColorBrush, ClientRectangle);
        }

        /// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.</summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data. </param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            int x = 4;
            int y = 4;
            int width = Width - 8;
            int height = Height - 8;
            Graphics g = e.Graphics;

            g.CompositingMode = CompositingMode.SourceCopy;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;

            if (_borderPen != null)
                g.DrawRectangle(_borderPen, x, y, width, height);

            if (DesignMode)
            {
                TextRenderer.DrawText(g, "99999",
                    Font,
                    new Rectangle(0, 0, Width, Height),
                    ForeColor,
                    TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            }
            else
            {

                TextRenderer.DrawText(g, Value.ToString(),
                    Font,
                    new Rectangle(0, 0, Width, Height),
                    ForeColor,
                    TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            }
        }

        #endregion

        #region Private Methods / Functions
        /// <summary>
        /// Initializes the component.
        /// </summary>
        private void MyInitializeComponent()
        {
            this.SuspendLayout();
            // 
            // SimpleGaugeControl
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            _font = new Font("OCR A Extended", 32F, FontStyle.Bold);
            Font = _font;
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.Black;

            Margin = new Padding(48, 22, 48, 22);
            Name = "SimpleGaugeControl";
            Size = new Size(300, 100);

            _borderPen = new Pen(Color.Gray, 3);
            BackColor = Color.Black;
            ForeColor = Color.Aquamarine;
            _backColorBrush = new SolidBrush(BackColor);

            this.ResumeLayout(false);

        }
        #endregion
    }
}