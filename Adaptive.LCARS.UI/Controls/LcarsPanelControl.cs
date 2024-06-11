using System;
using System.ComponentModel;
using System.Drawing;

namespace Adaptive.LCARS.UI
{
	/// <summary>
	/// Provides an LCARS panel implementation.
	/// </summary>
	/// <seealso cref="LcarsControlBase" />
	public class LcarsPanelControl : LcarsControlBase
	{
		#region Private Member Declarations
		/// <summary>
		/// The standard components container.
		/// </summary>
		private Container components;

		/// <summary>
		/// The corner rounding style.
		/// </summary>
		private LcarsRoundingStyle _roundStyle = LcarsRoundingStyle.None;

		/// <summary>
		/// The color under the panel.
		/// </summary>
		private Color _baseColor = Color.Transparent;

		#endregion

		#region Constructor / Dispose Methods		
		/// <summary>
		/// Initializes a new instance of the <see cref="LcarsPanelControl"/> class.
		/// </summary>
		/// <remarks>
		/// This is the default constructor for the <see cref="LcarsPanelControl"/> class.
		/// </remarks>
		public LcarsPanelControl()
		{
			// Create the standard Windows Control objects.
			components = new Container();

			// Set the standard User Control properties.
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.Black;
			Cursor = Cursors.Default;
			Name = nameof(LcarsPanelControl);
			Size = new Size(104, 32);
			Font = CreateLCARSFont(12, FontStyle.Regular);

			// Set the control style.
			SetStandardControlStyle();
			SetStyle(ControlStyles.ContainerControl, true);
		}
		/// <summary>
		/// Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Control" /> and its child controls and optionally releases the managed resources.
		/// </summary>
		/// <param name="disposing"><see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			if (!IsDisposed && disposing)
			{
				components?.Dispose();
			}
			base.Dispose(disposing);
		}
		#endregion

		public Color AlternateBackgroundColor { get; set; }

		public bool UseAlternateColor { get; set; }
		#region Public Properties		
		/// <summary>
		/// Gets or sets the color of the base under the panel.
		/// </summary>
		/// <remarks>
		/// This is only needed for the background when painting rounded corners on the panel.
		/// </remarks>
		/// <value>
		/// The <see cref="Color"/> value.
		/// </value>
		[Browsable(true),
		 Category("Appearance"),
		 Description("Sets the background color under the panel's background.")]
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
		/// Gets or sets the rounding style for the panel's corners.
		/// </summary>
		/// <value>
		/// A <see cref="LcarsRoundingStyle"/> enumerated value.
		/// </value>
		[Browsable(true), 
		 Category("Appearance"),
		 DefaultValue(LcarsRoundingStyle.None),
		 Description("Sets the rounding style of the panel's corners.")]
		public LcarsRoundingStyle Round
		{
			get
			{
				return _roundStyle;
			}
			set
			{
				_roundStyle = value;
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
		/// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			SetGraphicsQuality(g);

			// Create the brushes.
			SolidBrush backgroundBrush;
			SolidBrush underlyingBrush = new SolidBrush(_baseColor);

			if (!UseAlternateColor)
				backgroundBrush = new SolidBrush(BackColor);
			else
				backgroundBrush = new SolidBrush(AlternateBackgroundColor);

			// Fill the standard area.
			g.FillRectangle(underlyingBrush, 0, 0, ClientRectangle.Width, ClientRectangle.Height);
			if (_roundStyle == LcarsRoundingStyle.None)
			{
				g.FillRectangle(backgroundBrush, 0, 0, ClientRectangle.Width, ClientRectangle.Height);
			}
			else
			{
				int startX = Height / 2;
				int newWidth = Width - Height;
				g.FillRectangle(backgroundBrush, startX, 0, newWidth, Height);
			}
			
			// Paint the corners.
			if (_roundStyle != LcarsRoundingStyle.None)
			{
				PaintCorners(g, backgroundBrush, underlyingBrush);
			}

			backgroundBrush.Dispose();
			underlyingBrush.Dispose();
		}
		#endregion

		#region Private Methods / Functions		
		/// <summary>
		/// Paints the rounded corners for the panel.
		/// </summary>
		/// <param name="g">
		/// The <see cref="Graphics"/> object used to paint the corners.	
		/// </param>
		private void PaintCorners(Graphics g, SolidBrush backgroundBrush, SolidBrush underlyingBrush)
		{
			// Read these once.
			int height = ClientRectangle.Height;
			int width = ClientRectangle.Width;

			// Set flags based on user selection.
			bool leftCorner = false;
			bool rightCorner = false;
			switch (_roundStyle)
			{
				case LcarsRoundingStyle.Left:
					leftCorner = true;
					break;

				case LcarsRoundingStyle.Right:
					rightCorner = true;
					break;

				case LcarsRoundingStyle.Both:
					rightCorner = (leftCorner = true);
					break;
			}

			try
			{
				

				// Draw the rounded edge on the left or fill as a rectangle.
				if (leftCorner)
				{
					g.FillPie(backgroundBrush, 0, 0, height, height, 0, 360);
				}
				else
				{
					g.FillRectangle(backgroundBrush, 0, 0, height, height);
				}

				// Draw the rounded edge on the right or fill as a rectangle.
				if (rightCorner)
				{
					g.FillPie(backgroundBrush, width - height, 0, height, height, 0, 360);
				}
				else
				{
					g.FillRectangle(backgroundBrush, width - height, 0, height, height);
				}
			}
			catch
			{
			}
		}
		#endregion
	}
}
