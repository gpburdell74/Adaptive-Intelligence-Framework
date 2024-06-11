using System.Drawing;

namespace Adaptive.LCARS.UI
{
	/// <summary>
	/// Provides the base implementation for LCARS-styled controls.
	/// </summary>
	/// <seealso cref="UserControl" />
	public class LcarsControlBase : UserControl
	{
		#region Private Member Declarations		
		/// <summary>
		/// The lcars font name.
		/// </summary>
		private const string LCARSFontName = "LCARS";
		#endregion

		#region Protected Methods / Functions
		/// <summary>
		/// Creates the standard LCARS font object with a size of 12 points.
		/// </summary>
		/// <returns>
		/// The <see cref="Font"/> object used for the controls and drawing.
		/// </returns>
		protected Font CreateLCARSFont()
		{
			return CreateLCARSFont(12, FontStyle.Regular);
		}
		/// <summary>
		/// Creates the standard LCARS font object.
		/// </summary>
		/// <param name="size">
		/// The size of the text in points.
		/// </param>
		/// <returns>
		/// The <see cref="Font"/> object used for the controls and drawing.
		/// </returns>
		protected Font CreateLCARSFont(float size)
		{
			return CreateLCARSFont(size, FontStyle.Regular);	
		}
		/// <summary>
		/// Creates the LCARS Font object.
		/// </summary>
		/// <param name="size">
		/// The size of the text in points.
		/// </param>
		/// <param name="style">
		/// The <see cref="FontStyle"/> enumerated value indicating the font style.
		/// </param>
		/// <returns>
		/// The <see cref="Font"/> object used for the controls and drawing.
		/// </returns>
		protected Font CreateLCARSFont(float size, FontStyle style)
		{
			return new Font(LCARSFontName, size, style, GraphicsUnit.Point);
		}
		/// <summary>
		/// Draws the specified text on the control.
		/// </summary>
		/// <param name="g">
		/// The <see cref="Graphics"/> object used for drawing.
		/// </param>
		/// <param name="text">
		/// The text to be drawn.
		/// </param>
		/// <param name="textBrush">
		/// The <see cref="Brush"/> used to paint the object.
		/// </param>
		/// <param name="x">
		/// The X co-ordinate at which to start drawing.
		/// </param>
		/// <param name="y">
		/// The Y co-ordinate at which to start drawing.
		/// </param>
		protected void DrawTextSafe(Graphics g, string text, Brush textBrush, int x, int y)
		{
			try
			{
				g.DrawString(text, Font, textBrush, x, y);
			}
			catch
			{
			}
		}
		/// <summary>
		/// Fills the specified rectangle with the color of the specified brush.
		/// </summary>
		/// <param name="g">
		/// The <see cref="Graphics"/> object used for drawing.
		/// </param>
		/// <param name="fillBrush">
		/// The <see cref="Brush"/> used to paint the object.
		/// </param>
		/// <param name="rectangle">
		/// The <see cref="Rectangle"/> defining the area to fill.
		/// </param>
		protected void FillRectangleSafe(Graphics g, Brush fillBrush, Rectangle rectangle)
		{
			try
			{
				g.FillRectangle(fillBrush, rectangle);
			}
			catch
			{

			}
		}
		/// <summary>
		/// Sets the standard control style values for user-drawn controls.
		/// </summary>
		protected void SetStandardControlStyle()
		{
			SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.ResizeRedraw, true);
			SetStyle(ControlStyles.SupportsTransparentBackColor, true);
		}
		/// <summary>
		/// Sets the graphics object to draw items at high-quality value.
		/// </summary>
		/// <param name="g">
		/// The <see cref="Graphics"/> instance to use.
		/// </param>
		protected void SetGraphicsQuality(Graphics g)
		{
			try
			{

				g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
				g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
				g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
				g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
				g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
			}
			catch
			{
			}
		}
		#endregion
	}
}
