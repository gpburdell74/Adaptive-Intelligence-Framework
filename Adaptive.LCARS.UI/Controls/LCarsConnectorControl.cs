using System.ComponentModel;

namespace Adaptive.LCARS.UI
{
	/// <summary>
	/// Provides an LCARS-style "elbow" curved connector control.
	/// </summary>
	/// <seealso cref="LcarsControlBase" />
	public class LCarsConnectorControl : LcarsControlBase
	{
		#region Private Member Declarations		
		/// <summary>
		/// The orientation.
		/// </summary>
		private LcarsConnectorOrientation _orientation = LcarsConnectorOrientation.LeftTop;
		/// <summary>
		/// The colum height
		/// </summary>
		private int _colWidth = 20;
		/// <summary>
		/// The row height
		/// </summary>
		private int _rowHeight = 20;
		/// <summary>
		/// The arc interior.
		/// </summary>
		private int _arcInt = 50;
		/// <summary>
		/// The arc exterior.
		/// </summary>
		private int _arcExt = 100;
		#endregion

		#region Constructor / Dispose Methods		
		/// <summary>
		/// Initializes a new instance of the <see cref="LCarsConnectorControl"/> class.
		/// </summary>
		/// <remarks>
		/// This is the default constructor.
		/// </remarks>
		public LCarsConnectorControl()
		{
			// Set default property values.
			BackColor = Color.Black;
			ForeColor = Color.White;
			Name = nameof(LCarsConnectorControl);
			Size = new Size(104, 32);

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
		/// Gets or sets the diameter of the external arc.
		/// </summary>
		/// <value>
		/// The arc diameter value, in pixels.
		/// </value>
		[Category("Appearance"), Description("Specifies the external arc diameter.")]
		public int ArcExternal
		{
			get
			{
				return _arcExt;
			}
			set
			{
				_arcExt = value;
				Invalidate();
			}
		}
		/// <summary>
		/// Gets or sets the diameter of the internal arc.
		/// </summary>
		/// <value>
		/// The arc diameter value, in pixels.
		/// </value>
		[Category("Appearance"), Description("Specifies the internal arc diameter.")]
		public int ArcInternal
		{
			get
			{
				return _arcInt;
			}
			set
			{
				_arcInt = value;
				Invalidate();
			}
		}
		/// <summary>
		/// Gets or sets the orientation of the control.
		/// </summary>
		/// <value>
		/// An <see cref="LcarsConnectorOrientation"/> enumerated value.
		/// </value>
		[Category("Appearance"), 
		 Description("The orientation of the control.")]
		public LcarsConnectorOrientation Orientation
		{
			get
			{
				return _orientation;
			}
			set
			{
				_orientation = value;
				Invalidate();
			}
		}
		/// <summary>
		/// Gets or sets the width of the column / vertical bar.
		/// </summary>
		/// <value>
		/// The width value, in pixels.
		/// </value>
		[Category("Appearance"), Description("Specifies the width of the column (vertical bar).")]
		public int ColWidth
		{
			get
			{
				return _colWidth;
			}
			set
			{
				_colWidth = value;
				Invalidate();
			}
		}
		/// <summary>
		/// Gets or sets the height of the row / horizontal bar.
		/// </summary>
		/// <value>
		/// The height value, in pixels.
		/// </value>
		[Category("Appearance"), Description("Specifies the height of the row (Horizontal bar).")]
		public int RowHeight
		{
			get
			{
				return _rowHeight;
			}
			set
			{
				_rowHeight = value;
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

			int width = ClientSize.Width;
			int height = ClientSize.Height;

			//Pen pen = new Pen(ForeColor);
			SolidBrush backgroundBrush = new SolidBrush(BackColor);
			SolidBrush connectorBrush = new SolidBrush(ForeColor);

			int exteriorArcX = 0;
			int exteriorArcY = 0;
			int exteriorFillX = 0;
			int exteriorFillY = 0;

			int interiorArcX = 0;
			int interiorArcY = 0;
			int interiorFillX = 0;
			int interiorFillY = 0;

			int finishFillX = 0;
			int finishFillY = 0;

			switch (_orientation)
			{
				case LcarsConnectorOrientation.LeftTop:
					exteriorArcX = 0;
					exteriorArcY = 0;
					exteriorFillX = 0;
					exteriorFillY = 0;
					interiorArcX = _colWidth;
					interiorArcY = _rowHeight;
					interiorFillX = _colWidth;
					interiorFillY = _rowHeight;
					finishFillX = _colWidth;
					finishFillY = _rowHeight;
					break;

				case LcarsConnectorOrientation.LeftBottom:
					exteriorArcX = 0;
					exteriorArcY = height - _arcExt / 2;
					exteriorFillX = 0;
					exteriorFillY = height - _arcExt;
					interiorArcX = _colWidth;
					interiorArcY = 0;
					interiorFillX = _colWidth;
					interiorFillY = height - _rowHeight - _arcInt / 2;
					finishFillX = _colWidth;
					finishFillY = height - _rowHeight - _arcInt;
					break;

				case LcarsConnectorOrientation.RightTop:
					exteriorArcX = width - _arcExt / 2;
					exteriorArcY = 0;
					exteriorFillX = width - _arcExt;
					exteriorFillY = 0;
					interiorArcX = 0;
					interiorArcY = _rowHeight;
					interiorFillX = width - _colWidth - _arcInt / 2;
					interiorFillY = _rowHeight;
					finishFillX = width - _colWidth - _arcInt;
					finishFillY = _rowHeight;
					break;

				case LcarsConnectorOrientation.RightBottom:
					exteriorArcX = width - _arcExt / 2;
					exteriorArcY = height - _arcExt / 2;
					exteriorFillX = width - _arcExt;
					exteriorFillY = height - _arcExt;
					interiorArcX = 0;
					interiorArcY = 0;
					interiorFillX = width - _colWidth - _arcInt / 2;
					interiorFillY = height - _rowHeight - _arcInt / 2;
					finishFillX = width - _colWidth - _arcInt;
					finishFillY = height - _rowHeight - _arcInt;
					break;
			}

			try
			{
				g.FillRectangle(connectorBrush, 0, 0, width, height);

				g.FillRectangle(backgroundBrush, exteriorArcX, exteriorArcY, _arcExt / 2, _arcExt / 2);
				g.FillPie(connectorBrush, exteriorFillX, exteriorFillY, _arcExt, _arcExt, 0, 360);

				g.FillRectangle(backgroundBrush, interiorArcX, interiorArcY, width - _colWidth, height - _rowHeight);
				g.FillRectangle(connectorBrush, interiorFillX, interiorFillY, _arcInt / 2, _arcInt / 2);

				g.FillPie(backgroundBrush, finishFillX, finishFillY, _arcInt, _arcInt, 0, 360);
			}
			catch
			{

			}
			connectorBrush.Dispose();
			backgroundBrush.Dispose();
		}
		#endregion
	}
}
