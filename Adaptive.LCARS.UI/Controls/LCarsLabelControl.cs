using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Adaptive.LCARS.UI
{
	/// <summary>
	/// Provides an LCARS-style label control.
	/// </summary>
	/// <seealso cref="LcarsControlBase" />
	public class LCarsLabelControl : LcarsControlBase
	{
		#region Private Member Declarations		
		/// <summary>
		/// The alignment for the text.
		/// </summary>
		private LcarsHorizontalAlignment _alignment = LcarsHorizontalAlignment.Left;
		#endregion

		#region Constructor / Dispose Methods		
		/// <summary>
		/// Initializes a new instance of the <see cref="LCarsLabelControl"/> class.
		/// </summary>
		/// <remarks>
		/// This is the default constructor.
		/// </remarks>
		public LCarsLabelControl()
		{
			// Set default values.
			Name = nameof(LCarsLabelControl);
			Size = new Size(128, 40);
			BackColor = SystemColors.Window;
			ForeColor = SystemColors.WindowText;
			Font = CreateLCARSFont();

			// Set control style.
			SetStandardControlStyle();
		}
		/// <summary>
		/// Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Control" /> and its child controls and optionally releases the managed resources.
		/// </summary>
		/// <param name="disposing"><see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}
		#endregion

		#region Public Properties		
		/// <summary>
		/// Gets or sets the horizontal text alignment.
		/// </summary>
		/// <value>
		/// An <see cref="LcarsHorizontalAlignment"/>  enumerated value.
		/// </value>
		[Category("Appearance"), 
		 Description("Text alignment")]
		public LcarsHorizontalAlignment TextAlign
		{
			get
			{
				return _alignment;
			}
			set
			{
				_alignment = value;
				Invalidate();
			}
		}
		/// <summary>
		/// Gets or sets the text to be displayed in the label
		/// </summary>
		/// <value>
		/// A string containing hte text to display.
		/// </value>
		[Localizable(true)]
		[AllowNull]
		[Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Category("Appearance")]
		[Bindable(true)]
		[Description("The text to display on the label.")]
		public new string Text
		{
			get => base.Text;
			set
			{
				base.Text = value;
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
			if (AutoSize)
			{
				Graphics g = CreateGraphics();
				SizeF size = g.MeasureString(Text, Font);
				g.Dispose();
				Width = (int)size.Width + 20;
			}
			Invalidate();
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			// Set graphics object and drawing quality
			Graphics g = e.Graphics;
			SetGraphicsQuality(g);

			// Create objects.
			int width = ClientSize.Width;
			int height = ClientSize.Height;
			
			SolidBrush backBrush = new SolidBrush(BackColor);

			// Fill background.
			FillRectangleSafe(g, backBrush, new Rectangle(0, 0, width, height));

			// Draw the text.
			DrawText(g, width, height);
			backBrush.Dispose();
		}
		#endregion

		#region Private Methods / Functions		
		/// <summary>
		/// Draws the label text.
		/// </summary>
		/// <param name="g">
		/// The <see cref="Graphics"/> instance used to draw the text.
		/// </param>
		/// <param name="width">
		/// The width of the control.
		/// </param>
		/// <param name="height">
		/// The height of the control.
		/// </param>
		private void DrawText(Graphics g, int width, int height)
		{
			// Measure the text size.
			SizeF textSize = g.MeasureString(Text, Font);

			// Calculate the X (left) position for the text.
			int x = 0;
			switch (_alignment)
			{
				case LcarsHorizontalAlignment.Right:
					x = width - (int)textSize.Width;
					break;

				case LcarsHorizontalAlignment.Center:
					x = width / 2 - (int)(textSize.Width / 2f);
					break;

				default:
					x = Padding.Left;
					break;
			}

			// Center the text vertically.
			int y = (int)(((Height+2) - textSize.Height) / 2);
			if (y < 0)
				y = 0;

			if (y == 0)
				y += Padding.Top;

			SolidBrush textBrush = new SolidBrush(ForeColor);
			g.DrawString(Text, Font, textBrush, new Rectangle(x,y,Width-Padding.Right, Height-Padding.Bottom));


			textBrush.Dispose();

		}
		#endregion
	}
}
