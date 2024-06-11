using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Adaptive.LCARS.UI
{
	/// <summary>
	/// Provides an LCARS-style button control.
	/// </summary>
	/// <seealso cref="LcarsControlBase" />
	public class LCarsButtonControl : LcarsPanelControl
	{
		#region Private Member Declarations				
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		/// <summary>
		/// The default text value.
		/// </summary>
		private const string DefaultText = "LCARS";
		/// <summary>
		/// The blink timer.
		/// </summary>
		private System.Windows.Forms.Timer? _blinkTimer;
		/// <summary>
		/// The blink back color.
		/// </summary>
		private Color _blinkBackColor = Color.White;
		/// <summary>
		/// The blink fore color.
		/// </summary>
		private Color _blinkForeColor = Color.Black;
		/// <summary>
		/// The text position.
		/// </summary>
		private Point _textPos = new Point(0, 0);
		/// <summary>
		/// The sound number.
		/// </summary>
		private int _soundNumber = 1;
		/// <summary>
		/// The blink flag.
		/// </summary>
		private bool _blink = false;
		/// <summary>
		/// The blink state / alternating flag.
		/// </summary>
		private bool _blinkState = false;
		/// <summary>
		/// The hover flag.
		/// </summary>
		private bool _hover = false;
		#endregion

		#region Constructor / Dispose Methods		
		/// <summary>
		/// Initializes a new instance of the <see cref="LCarsButtonControl"/> class.
		/// </summary>
		/// <remarks>
		/// This is the default constructor for the <see cref="LCarsButtonControl"/> class.
		/// </remarks>
		public LCarsButtonControl()
		{
			// Create standard objects and set standard values.
			components = new Container();
			this.AutoScaleMode = AutoScaleMode.Font;

			BackColor = Color.Black;
			Cursor = Cursors.Hand;
			Font = CreateLCARSFont();
			Name = nameof(LCarsButtonControl);
			Size = new Size(104, 32);

			// Set the control style.
			SetStandardControlStyle();

			// Create the blink timer.
			_blinkTimer = new System.Windows.Forms.Timer(components);
			_blinkTimer.Interval = 1000;
			_blinkTimer.Tick += HandleTimerElapsed;

		}
		/// <summary>
		/// Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Control" /> and its child controls and optionally releases the managed resources.
		/// </summary>
		/// <param name="disposing"><see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			if (!IsDisposed && disposing)
			{
				if (_blinkTimer != null)
				{
					_blinkTimer.Tick -= HandleTimerElapsed;
					_blinkTimer.Enabled = false;
					_blinkTimer.Dispose();
				}
				components?.Dispose();
			}

			_blinkTimer = null;

			base.Dispose(disposing);
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets or sets the color of the button background when blinking.
		/// </summary>
		/// <value>
		/// The <see cref="Color"/> to alternate to when blinking.
		/// </value>
		[Category("Appearance"),
		 Description("The background color of the button when it is blinking.")]
		public Color BlinkBackColor
		{
			get
			{
				return _blinkBackColor;
			}
			set
			{
				_blinkBackColor = value;
				Invalidate();
			}
		}
		/// <summary>
		/// Gets or sets the color of the button text when blinking.
		/// </summary>
		/// <value>
		/// The <see cref="Color"/> to alternate to when blinking.
		/// </value>
		[Category("Appearance"),
		 Description("The background color of the text when it is blinking.")]
		public Color BlinkForeColor
		{
			get
			{
				return _blinkForeColor;
			}
			set
			{
				_blinkForeColor = value;
				Invalidate();
			}
		}
		/// <summary>
		/// Gets or sets a value indicating whether the button blinks.
		/// </summary>
		/// <value>
		///   <c>true</c> if the button blinks; otherwise, <c>false</c>.
		/// </value>
		[Category("Appearance"), Description("Turn the button blink on or off.")]
		public bool Blink
		{
			get
			{
				return _blink;
			}
			set
			{
				if (value != _blink)
				{
					_blink = value;
					_blinkTimer.Enabled = _blink;
				}
			}
		}
		/// <summary>
		/// Gets or sets the number of the button sound to use when clicked.
		/// </summary>
		/// <value>
		/// A number from one (1) to (5) to specify a sound, or zero (0) for no sound.
		/// </value>
		public int SoundNumber
		{
			get => _soundNumber;
			set => _soundNumber = value;
		}
		/// <summary>
		/// Gets or sets the text displayed on the button.
		/// </summary>
		/// <value>
		/// A string containing the text.
		/// </value>
		[Localizable(true)]
		[AllowNull]
		[Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Category("Appearance")]
		[Bindable(true)]
		[Description("The text to display on the button.")]
		public new string Text
		{
			get => base.Text;
			set
			{
				base.Text = value;
				Invalidate();
			}
		}
		/// <summary>
		/// Gets or sets the text position.
		/// </summary>
		/// <value>
		/// A <see cref="Point"/> structure specifying the text position.
		/// </value>
		[Category("Location"), Description("The position at which to draw the text.")]
		public Point TextPosition
		{
			get
			{
				return _textPos;
			}
			set
			{
				_textPos = value;
				Invalidate();
			}
		}
		#endregion

		#region Protected Method Overrides		
		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			Invalidate();
		}
		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.MouseHover" /> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		protected override void OnMouseHover(EventArgs e)
		{
			base.OnMouseHover(e);
			_hover = true;
			Invalidate();
		}
		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			_hover = false;
			Invalidate();
		}
		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

			// Play the button sound.	
			switch (_soundNumber)
			{
				case 1:
					SoundProvider.PlayButton1();
					break;

				case 2:
					SoundProvider.PlayButton2();
					break;

				case 3:
					SoundProvider.PlayButton3();
					break;

				case 4:
					SoundProvider.PlayButton4();
					break;

				case 5:
					SoundProvider.PlayButton5();
					break;
			}
		}
		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			SetGraphicsQuality(g);

			if (_blinkState)
			{
				AlternateBackgroundColor = BlinkBackColor;
				UseAlternateColor = true;
			}
			else
				UseAlternateColor = false;

			// Ensure the corners are painted.
			base.OnPaint(e);

			// Draw the text.
			DrawText(g);
		}
		#endregion

		#region Private Methods / Functions
		#endregion

		#region Private Event Handlers		
		/// <summary>
		/// Handles the event when the active timer's time interval has elapsed.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void HandleTimerElapsed(object? sender, EventArgs e)
		{
			// Achieve the blinking by alternating drawing colors for each interval.
			_blinkState = !_blinkState;
			Invalidate();
		}

		#endregion

		#region Private Methods / Functions		
		/// <summary>
		/// Draws the background.
		/// </summary>
		/// <param name="g">
		/// The <see cref="Graphics"/> instance to use for drawing tasks.
		/// </param>
		private void DrawBackground(Graphics g)
		{
			// Create the brush based on the button state.
			Color backgroundColor;
			if (_blinkState)
				backgroundColor = _blinkBackColor;
			else
				backgroundColor = BackColor;
			SolidBrush brush = new SolidBrush(backgroundColor);

			// Fill the background.
			FillRectangleSafe(g, brush, ClientRectangle);
			brush.Dispose();
		}
		/// <summary>
		/// Draws the text on the button.
		/// </summary>
		/// <param name="g">
		/// The <see cref="Graphics"/> instance to use for drawing tasks.
		/// </param>
		private void DrawText(Graphics g)
		{
			// Create the brush based on the button state.
			Color foreColor;
			if (_blinkState)
				foreColor = _blinkForeColor;
			else
				foreColor = ForeColor;

			if (!string.IsNullOrEmpty(Text))
			{
				SolidBrush brush = new SolidBrush(foreColor);
				DrawTextSafe(g, Text, brush, _textPos.X, _textPos.Y);
				brush.Dispose();
			}
		}
		#endregion

	}
}
