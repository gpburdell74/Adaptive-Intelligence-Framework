using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace Adaptive.Intelligence.Shared.UI
{
    /// <summary>
    /// Provides a control for drawing a vertical label.
    /// </summary>
    /// <seealso cref="Label" />
    [ToolboxItem(true),
     Description("Provides a label for displaying vertical text.")]
    public sealed class VerticalLabel : Label
    {
        #region Protected Member Overrides
        /// <summary>
        /// Raises the <see cref="System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SizeF size = g.MeasureString(Text, Font);

            int height = (int)size.Height / 2;
            int transformX = (height - (height / 2)) - 2;

            g.TranslateTransform(transformX, Height - 4);
            g.RotateTransform(-90);

            g.CompositingQuality = CompositingQuality.HighQuality;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            Color cColor = Color.FromArgb(65, ForeColor.R, ForeColor.G, ForeColor.B);
            g.DrawString(Text, Font, new SolidBrush(cColor),
                1, 1);

            g.DrawString(Text, Font, new SolidBrush(ForeColor),
                0, 0);
        }
        #endregion
    }
}