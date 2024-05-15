using Adaptive.Intelligence.Shared.Logging;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace Adaptive.Intelligence.Shared.UI
{
	/// <summary>
	/// Provides a graphic control for drawing a line or rectangle.
	/// </summary>
	[DefaultBindingProperty("Width"),
	 Description("Provides a control for drawing lines on a dialog."),
	 DefaultEvent("Click"),
	 DefaultProperty("Width"),
	 ToolboxItemFilter("System.Windows.Forms"),
	 ToolboxItem(true),
	 ToolboxBitmap(typeof(LineControl), "LineControl")]
	public class LineControl : UserControl
	{
		#region Private Member Declarations

		#region Private Constants
		private const int DefaultWidth = 2;

		private static readonly Color DefaultStartColor = SystemColors.ControlDark;
		private static readonly Color DefaultEndColor = SystemColors.ControlLight;
		private static readonly Color DefaultBevelTopColor = SystemColors.ControlDark;
		private static readonly Color DefaultBevelBottomColor = SystemColors.ControlLight;

		private static readonly LineControlMode DefaultMode = LineControlMode.Line;
		private static readonly LineControlOrientation DefaultOrientation = LineControlOrientation.Horizontal;
		private static readonly LinearGradientMode DefaultGradientMode = LinearGradientMode.ForwardDiagonal;

		#endregion

		private Color _startColor = DefaultStartColor;
		private Color _endColor = DefaultEndColor;
		private Color _bevelTopColor = DefaultBevelTopColor;
		private Color _bevelBottomColor = DefaultBevelBottomColor;
		private LineControlMode _mode = DefaultMode;
		private LineControlOrientation _orientation = DefaultOrientation;
		private LinearGradientMode _gradientDirection = DefaultGradientMode;

		private int _width = DefaultWidth;

		private LinearGradientBrush? _gradientBrush;
		private Pen? _topBevelPen;
		private Pen? _bottomBevelPen;

		#endregion

		#region Constructor / Dispose Methods
		/// <summary>
		/// Initializes a new instance of the <see cref="LineControl"/> class.
		/// </summary>
		/// <remarks>
		/// This is the default constructor.
		/// </remarks>
		public LineControl()
		{
			// Set for owner drawing.
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.DoubleBuffer, true);
			SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			SetStyle(ControlStyles.ResizeRedraw, true);
			SetStyle(ControlStyles.Selectable, true);
			SetStyle(ControlStyles.StandardClick, true);
			SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			SetStyle(ControlStyles.UserPaint, true);
		}
		/// <summary>
		/// Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ButtonBase" /> and optionally releases the managed resources.
		/// </summary>
		/// <param name="disposing"><see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			if (!IsDisposed && disposing)
			{
				_gradientBrush?.Dispose();
				_topBevelPen?.Dispose();
				_bottomBevelPen?.Dispose();
			}

			_gradientBrush = null;
			_topBevelPen = null;
			_bottomBevelPen = null;

			base.Dispose(disposing);
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets or sets the top / left color when drawing a beveled line.
		/// property.
		/// </summary>
		/// <remarks>
		/// This sets the line width to 2 pixels.
		/// </remarks>
		/// <value>
		/// A <see cref="Color"/> determining the top / left color of the beveled line.
		/// </value>
		[Browsable(true),
		 Category("Appearance"),
		 Description("Sets the top color to use for beveling.")]
		public Color BevelBottomColor
		{
			get => _bevelBottomColor;
			set
			{
				_bevelBottomColor = value;
				OnResize(EventArgs.Empty);
				SetDrawingObjects();
				Invalidate();
			}
		}
		/// <summary>
		/// Gets or sets the top / left color when drawing a beveled line.
		/// property.
		/// </summary>
		/// <remarks>
		/// This sets the line width to 2 pixels.
		/// </remarks>
		/// <value>
		/// A <see cref="Color"/> determining the top / left color of the beveled line.
		/// </value>
		[Browsable(true),
		 Category("Appearance"),
		 Description("Sets the bottom color to use for beveling.")]
		public Color BevelTopColor
		{
			get => _bevelTopColor;
			set
			{
				_bevelTopColor = value;
				OnResize(EventArgs.Empty);
				SetDrawingObjects();
				Invalidate();
			}
		}
		/// <summary>
		/// Gets or sets the direction of the gradient being drawn.
		/// property.
		/// </summary>
		/// <value>
		/// A <see cref="LinearGradientMode"/> enumerated value.
		/// </value>
		[Browsable(true),
		 Category("Appearance"),
		 Description("Gets or sets the direction of the gradient.")]
		public LinearGradientMode Direction
		{
			get => _gradientDirection;
			set
			{
				_gradientDirection = value;
				SetDrawingObjects();
				Invalidate();
			}
		}
		/// <summary>
		/// Gets or sets the ending color of the gradient line.
		/// property.
		/// </summary>
		/// <remarks>
		/// Set this to the same value as the <see cref="StartColor"/> for a non-gradient line.
		/// </remarks>
		/// <value>
		/// A <see cref="Color"/> determining the ending color of the gradient.
		/// </value>
		[Browsable(true),
		 Category("Appearance"),
		 Description("Sets the color for the ending of the gradient.")]
		public Color EndColor
		{
			get => _endColor;
			set
			{
				_endColor = value;
				SetDrawingObjects();
				Invalidate();
			}
		}
		/// <summary>
		/// Gets or sets the height of the line control.
		/// </summary>
		/// <value>
		/// An integer specifying the height of the control, in pixels.
		/// </value>
		[Browsable(true),
		 Category("Appearance"),
		 Description("Sets the height of the control.")]
		public new int Height
		{
			get => base.Height;
			set
			{
				if (_mode == LineControlMode.Bevel)
				{
					if (_orientation == LineControlOrientation.Horizontal)
						base.Height = 2;
					else
						base.Height = value;
				}
				else
					base.Height = value;
				SetLineWidth(_width);
				SetDrawingObjects();
				Invalidate();
			}
		}
		/// <summary>
		/// Gets or sets a value indicating the line height or width, based on the <see cref="Orientation"/>
		/// property.
		/// </summary>
		/// <value>
		/// An integer indicating the height or width of the line, in pixels.
		/// </value>
		[Browsable(true),
		 Category("Appearance"),
		 Description("Gets or sets the height or width of the line based on mode and orientation.")]
		public int LineWidth
		{
			get => _width;
			set
			{
				SetLineWidth(value);
				SetDrawingObjects();
				Invalidate();
			}
		}
		/// <summary>
		/// Gets or sets a value indicating the drawing mode of the line.
		/// property.
		/// </summary>
		/// <value>
		/// A <see cref="LineControlMode"/> enumerated value.
		/// </value>
		[Browsable(true),
		 Category("Appearance"),
		 Description("Gets or sets drawing mode for the line.")]
		public LineControlMode Mode
		{
			get => _mode;
			set
			{
				_mode = value;
				SetLineWidth(_width);
				SetDrawingObjects();
				Invalidate();
			}
		}
		/// <summary>
		/// Gets or sets a value indicating the orientation of the line.
		/// property.
		/// </summary>
		/// <value>
		/// A <see cref="LineControlOrientation"/> enumerated value.
		/// </value>
		[Browsable(true),
		 Category("Appearance"),
		 Description("Gets or sets the vertical or horizontal orientation of the line.")]
		public LineControlOrientation Orientation
		{
			get => _orientation;
			set
			{
				_orientation = value;
				SetLineWidth(_width);
				SetDrawingObjects();
				Invalidate();
			}
		}
		/// <summary>
		/// Gets or sets the starting color of the gradient line.
		/// property.
		/// </summary>
		/// <remarks>
		/// Set this to the same value as the <see cref="EndColor"/> for a non-gradient line.
		/// </remarks>
		/// <value>
		/// A <see cref="Color"/> determining the ending color of the gradient.
		/// </value>
		[Browsable(true),
		 Category("Appearance"),
		 Description("Sets the color for the starting of the gradient.")]
		public Color StartColor
		{
			get => _startColor;
			set
			{
				_startColor = value;
				SetDrawingObjects();
				Invalidate();
			}
		}
		/// <summary>
		/// Gets or sets the width of the line control.
		/// </summary>
		/// <value>
		/// An integer specifying the width of the control, in pixels.
		/// </value>
		[Browsable(true),
		 Category("Appearance"),
		 Description("Sets the width of the control.")]
		public new int Width
		{
			get => base.Width;
			set
			{
				if (_mode == LineControlMode.Bevel)
				{
					if (_orientation == LineControlOrientation.Vertical)
						base.Width = 2;
					else
						base.Width = value;
				}
				else
					base.Width = value;
				SetLineWidth(_width);
				SetDrawingObjects();
				Invalidate();
			}
		}

		#endregion

		#region Protected Method Overrides
		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
		/// </summary>
		/// <param name="e">
		/// A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.
		/// </param>
		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics g = e.Graphics;

			switch (_mode)
			{
				case LineControlMode.Bevel:
					DrawBeveledLine(g);
					break;

				default:
					DrawGradientLine(g);
					break;
			}
		}
		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			SetLineWidth(_width);
			SetDrawingObjects();
			Invalidate();
		}
		#endregion

		#region Private Methods / Functions
		/// <summary>
		/// Creates the drawing object instances.
		/// </summary>
		private void SetDrawingObjects()
		{
			if (Visible)
			{
				_gradientBrush?.Dispose();
				_gradientBrush = null;
				_topBevelPen?.Dispose();
				_topBevelPen = null;
				_bottomBevelPen?.Dispose();
				_bottomBevelPen = null;

				// Gradient Line Mode.
				if (_mode == LineControlMode.Line)
				{
					RectangleF rectangle;
					if (_width == 0)
						_width = 1;

					if (_orientation == LineControlOrientation.Horizontal)
						rectangle = new RectangleF(0, 0, Width, _width);
					else
						rectangle = new RectangleF(0, 0, _width, Height);

					try
					{
						_gradientBrush = new LinearGradientBrush(rectangle, _startColor, _endColor, _gradientDirection);
					}
					catch (Exception ex)
					{
						ExceptionLog.LogException(ex);
					}
				}
				else
				{
					_topBevelPen = new Pen(_bevelTopColor, 1);
					_bottomBevelPen = new Pen(_bevelBottomColor, 1);
				}
			}
		}
		/// <summary>
		/// Sets the width of the line and handles the sizing.
		/// </summary>
		/// <param name="width">
		/// An integer indicating the width or height, in pixels.
		/// </param>
		private void SetLineWidth(int width)
		{
			_width = width;
			if (_orientation == LineControlOrientation.Horizontal)
			{
				base.Height = _width;
				if (_mode == LineControlMode.Bevel)
					base.Width = 2;
			}
			else
			{
				base.Width = _width;
				if (_mode == LineControlMode.Bevel)
					base.Height = 2;
			}
		}
		/// <summary>
		/// Draws the beveled line.
		/// </summary>
		/// <param name="g">
		/// The <see cref="Graphics"/> instance used to perform the drawing.
		/// </param>
		private void DrawBeveledLine(Graphics g)
		{
			g.SmoothingMode = SmoothingMode.None;
			if (_bottomBevelPen != null && _topBevelPen != null)
			{
				switch (_orientation)
				{
					case LineControlOrientation.Horizontal:
						g.DrawLine(_bottomBevelPen, 0, 1, Width, 1);
						g.DrawLine(_topBevelPen, 0, 0, Width, 0);
						break;

					default:
						g.DrawLine(_topBevelPen, 0, 0, 0, Height);
						g.DrawLine(_bottomBevelPen, 1, 0, 1, Height);
						break;
				}
			}
		}
		/// <summary>
		/// Draws the gradient line.
		/// </summary>
		/// <param name="g">
		/// The <see cref="Graphics"/> instance used to perform the drawing.
		/// </param>
		private void DrawGradientLine(Graphics g)
		{
			if (_gradientBrush != null)
				g.FillRectangle(_gradientBrush, new Rectangle(0, 0, Width, Height));
		}
		#endregion
	}
}
