using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Adaptive.LCARS.UI
{
	/// <summary>
	/// Provides an LCARS check box control.
	/// </summary>
	/// <seealso cref="LcarsControlBase" />
	public partial class LCarsCheckBoxControl : LcarsControlBase
	{
		#region Public Events		
		/// <summary>
		/// Occurs when the check state changes.
		/// </summary>
		public event EventHandler CheckChanged;
		#endregion

		#region Private Member Declarations		
		/// <summary>
		/// The color to use when the item is checked.
		/// </summary>
		private Color _checkedColor = Color.DodgerBlue;
		/// <summary>
		/// The base color - the color under the control.
		/// </summary>
		private Color _baseColor = Color.Black;
		/// <summary>
		/// The color to use when the item is not checked.
		/// </summary>
		private Color _unCheckedColor = Color.LightBlue;
		/// <summary>
		/// The checked flag.
		/// </summary>
		private bool _checked;
		#endregion

		#region Constructor / Dispose Methods		
		/// <summary>
		/// Initializes a new instance of the <see cref="LCarsCheckBoxControl"/> class.
		/// </summary>
		/// <remarks>
		/// This is the default constructor.
		/// </remarks>
		public LCarsCheckBoxControl()
		{
			InitializeComponent();

			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			SetStyle(ControlStyles.ResizeRedraw, true);
			SetStyle(ControlStyles.Selectable, true);
			SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			SetStyle(ControlStyles.UserPaint, true);
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
		/// Gets or sets a the base, or underlying color for the control.
		/// </summary>
		/// <value>
		/// A <see cref="Color"/> value indicating the color *under* the control - used for filling in the black space around
		/// the curved area.
		/// </value>
		[Browsable(true),
		 Description("The base or underlying color of hte control."),
		 DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public Color BaseColor
		{
			get => _baseColor;
			set
			{
				_baseColor = value;
				Invalidate();
			}
		}
		/// <summary>
		/// Gets or sets a value indicating whether the control is checked.
		/// </summary>
		/// <value>
		///   <c>true</c> if the check state is <b>true</b>; otherwise, <c>false</c>.
		/// </value>
		[Browsable(true),
		 Description("Checks or un-checks the control."),
 		 DefaultValue(false),
		 DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public bool Checked
		{
			get => _checked;
			set
			{
				if (_checked != value)
				{
					_checked = value;
					OnCheckChanged(EventArgs.Empty);
					Invalidate();
				}
			}
		}
		/// <summary>
		/// Gets or sets a the color to use when the item is checked.
		/// </summary>
		/// <value>
		/// A <see cref="Color"/> value that will paint the "check" area of the control when checked.
		/// </value>
		[Browsable(true),
		 Description("The color to use when the item is checked."),
		 DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public Color CheckedColor
		{
			get => _checkedColor;
			set
			{
				_checkedColor = value;
				Invalidate();
			}
		}
		/// <summary>
		/// Gets or sets the text to be displayed in the label
		/// </summary>
		/// <value>
		/// A string containing the text to display.
		/// </value>
		[Localizable(true)]
		[AllowNull]
		[Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Category("Appearance")]
		[Bindable(true)]
		[Description("The text to display on the check box.")]
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
		/// Gets or sets a the color to use when the item is checked.
		/// </summary>
		/// <value>
		/// A <see cref="Color"/> value that will paint the "check" area of the control when NOT checked.
		/// </value>
		[Browsable(true),
		 Description("The color of the outline when unchecked."),
		 DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public Color UncheckedColor
		{
			get => _unCheckedColor;
			set
			{
				_unCheckedColor = value;
				Invalidate();
			}
		}
		#endregion

		#region Protected Method Overrides		
		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.Click" /> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		protected override void OnClick(EventArgs e)
		{
			SoundProvider.PlayButton1();
			Thread.Sleep(150);

			base.OnClick(e);

			_checked = !_checked;
			OnCheckChanged(EventArgs.Empty);
			Invalidate();
		}
		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
		protected override void OnPaint(PaintEventArgs e)
		{

			SolidBrush brush;
			if (_checked)
				brush = new SolidBrush(_checkedColor);
			else
				brush = new SolidBrush(_unCheckedColor);

			SolidBrush baseBrush = new SolidBrush(_baseColor);
			SolidBrush backBrush = new SolidBrush(BackColor);
			SolidBrush foreBrush = new SolidBrush(ForeColor);

			Graphics g = e.Graphics;
			SetGraphicsQuality(g);

			g.FillRectangle(baseBrush, 0, 0, Width, Height);

			int h = (int)(Height / 2);
			int st = h + 10;
			int r = h + 15;
			int rw = Width - r;


			if (_checked)
				g.FillRectangle(brush, h, 0, 10, Height);

			g.FillRectangle(baseBrush, st, 0, st + 5, Height);
			g.FillRectangle(backBrush, r, 0, rw, Height);


			if (_checked)
				g.FillPie(brush, 0, 0, Height, Height, 90, 180);
			else
			{
				Pen pen = new Pen(_unCheckedColor);
				pen.Width = 2;

				//g.DrawArc(pen, 1, 0, Height+3, Height-1, 90, 180);
				g.DrawLine(pen, Height / 2, 1, (Height / 2) + 10, 1);
				g.DrawLine(pen, (Height / 2) + 10, 0, (Height / 2) + 10, Height);
				g.DrawLine(pen, (Height / 2) + 10, Height - 1, (Height / 2), Height - 1);
				g.DrawArc(pen, 1, 0, Height + 3, Height - 1, 90, 180);
			}
			g.DrawString(Text, Font, foreBrush, new Point(r, 0));

			brush.Dispose();
			baseBrush.Dispose();
			backBrush.Dispose();
			foreBrush.Dispose();
		}
		#endregion

		#region Private Event Methods		
		/// <summary>
		/// Raises the <see cref="E:CheckChanged" /> event.
		/// </summary>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void OnCheckChanged(EventArgs e)
		{
			CheckChanged?.Invoke(this, e);
		}
		#endregion
	}
}
