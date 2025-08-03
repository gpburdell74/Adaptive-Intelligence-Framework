using Adaptive.Intelligence.Shared.Logging;
using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms.Design;

namespace Adaptive.Intelligence.Shared.UI;

/// <summary>
/// Provides a Windows Forms Panel control with a gradient background.
/// </summary>
/// <seealso cref="Panel" />
public class GradientPanel : Panel
{
    private string? _templateFile;

    #region Constructor / Dispose Methods    
    /// <summary>
    /// Initializes a new instance of the <see cref="GradientPanel"/> class.
    /// </summary>
    public GradientPanel() : base()
    {
        SetStyle(ControlStyles.ContainerControl, true);
        SetStyle(ControlStyles.Opaque, false);
        SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        SetStyle(ControlStyles.ResizeRedraw, true);
        SetStyle(ControlStyles.Selectable, false);
        SetStyle(ControlStyles.UserPaint, true);
        SetStyle(ControlStyles.SupportsTransparentBackColor, true);
    }
    /// <summary>
    /// Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Control" /> and its child controls and optionally releases the managed resources.
    /// </summary>
    /// <param name="disposing"><see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        if (!IsDisposed && disposing)
        {
            Template?.Dispose();
        }

        Template = null;
        _templateFile = null;
        base.Dispose(disposing);
    }
    #endregion

    #region Public Properties    
    /// <summary>
    /// Gets or sets the reference to the button template to use when drawing the button.
    /// </summary>
    /// <value>
    /// The <see cref="ButtonTemplate"/> instance.
    /// </value>
    [Browsable(false),
     Category("Appearance"),
     Description("Gets or sets the button template to use."),
     DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ButtonStateTemplate? Template { get; set; }

    /// <summary>
    /// Gets or sets the template file.
    /// </summary>
    /// <value>
    /// A string containing the fully-qualified path and name of the template file.
    /// </value>
    [Browsable(true),
     Category("Appearance"),
     Description("Gets or sets the button template file to use."),
     DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
     Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
    public string? TemplateFile
    {
        get => _templateFile;
        set
        {
            _templateFile = value;
            if (value != null)
            {
                ButtonTemplate testTemplate = ButtonTemplate.Load(_templateFile);
                if (testTemplate != null)
                {
                    Template = testTemplate.Normal;
                }
            }
            Invalidate();
        }
    }
    #endregion

    #region Protected Method Overrides    
    /// <summary>
    /// Paints the background of the control.
    /// </summary>
    /// <param name="e">The event data.</param>
    protected override void OnPaintBackground(PaintEventArgs e)
    {
        if (Template == null)
            base.OnPaintBackground(e);
        else
        {
            DrawBackground(e.Graphics);
        }
    }
    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
    protected override void OnPaint(PaintEventArgs e)
    {
        if (Template == null)
            base.OnPaint(e);
        else
        {
            DrawPanel(e.Graphics);
        }
    }
    /// <summary>
    /// Fires the event indicating that the panel has been resized. Inheriting controls should use this in favor of actually listening to the event, but should still call <see langword="base.onResize" /> to ensure that the event is fired for external listeners.
    /// </summary>
    /// <param name="eventargs">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected override void OnResize(EventArgs eventargs)
    {
        base.OnResize(eventargs);
        Invalidate();
    }
    #endregion

    #region Private Methods / Functions    
    /// <summary>
    /// Draws the panel.
    /// </summary>
    /// <param name="g">The g.</param>
    public void DrawPanel(Graphics g)
    {
        DrawBackground(g);

        // Use the correct font.
        Font? textFont = Template.Font;

        // Draw the image.
        DrawButtonImage(g);

        // Draw the text.
        DrawText(g, textFont);

        // Draw the border.
        DrawBorder(g);
    }

    /// <summary>
    /// Calculates the image position.
    /// </summary>
    /// <returns>
    /// A <see cref="Rectangle"/> containing the X and Y co-ordinates and the width and height of the image.
    /// </returns>
    private Rectangle CalculateImagePosition()
    {
        int imageX = 0;
        int imageY = 0;

        int height = 16;
        int width = 16;

        if (Template.Image != null)
        {
            int imageHeight;
            try
            {
                imageHeight = Template.Image.Height;
            }
            catch
            {
                imageHeight = 16;
            }

            if (imageHeight >= 32)
            {
                height = 32;
                width = 32;
            }

            // Calculate the Y location.
            switch (Template.ImageAlign)
            {
                case ContentAlignment.BottomCenter:
                case ContentAlignment.BottomLeft:
                case ContentAlignment.BottomRight:
                    imageY = (Height - 5) - height;
                    break;

                case ContentAlignment.TopCenter:
                case ContentAlignment.TopLeft:
                case ContentAlignment.TopRight:
                    imageY = 5;
                    break;

                case ContentAlignment.MiddleCenter:
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.MiddleRight:
                    imageY = ((Height - height) / 2);
                    break;
            }

            // Calculate the X location.
            switch (Template.ImageAlign)
            {
                case ContentAlignment.TopCenter:
                case ContentAlignment.BottomCenter:
                case ContentAlignment.MiddleCenter:
                    imageX = ((Width - width) / 2);
                    break;

                case ContentAlignment.TopRight:
                case ContentAlignment.MiddleRight:
                case ContentAlignment.BottomRight:
                    imageX = Width - (width + 5);
                    break;

                case ContentAlignment.TopLeft:
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.BottomLeft:
                    imageX = 5;
                    break;
            }
        }

        return new Rectangle(imageX, imageY, width, height);
    }
    /// <summary>
    /// Draws the button border.
    /// </summary>
    /// <param name="g">
    /// The <see cref="Graphics"/> object used to draw the button.
    /// </param>
    private void DrawBorder(Graphics g)
    {
        if (Template.BorderWidth > 0)
        {
            Pen? borderPen = GetBorderPen();
            if (borderPen != null)
            {
                g.DrawRectangle(borderPen, 0, 0, Width - 1, Height - 1);
                borderPen.Dispose();
            }
        }
    }
    /// <summary>
    /// Draws the text
    /// </summary>
    /// <param name="g">
    /// The <see cref="Graphics"/> object used to draw the button.
    /// </param>
    /// <param name="textFont">
    /// The <see cref="Font"/> to use with the text.
    /// </param>
    private void DrawText(Graphics? g, Font? textFont)
    {
        if (g != null && textFont != null)
        {
            TextFormatFlags formatFlags = GetTextFormatFlags();
            Rectangle adjustmentRectangle = CalculateTextRectangle();

            TextRenderer.DrawText(g, Text, textFont, adjustmentRectangle, Template.ForeColor, formatFlags);
        }
    }
    /// <summary>
    /// Gets the text format flags.
    /// </summary>
    /// <returns>
    /// A <see cref="TextFormatFlags"/> enumerated value indicating how to draw the text.
    /// </returns>
    private TextFormatFlags GetTextFormatFlags()
    {
        TextFormatFlags formatFlags = TextFormatFlags.WordBreak | TextFormatFlags.EndEllipsis;

        switch (Template.TextAlign)
        {
            case ContentAlignment.BottomCenter:
                formatFlags |= TextFormatFlags.Bottom | TextFormatFlags.HorizontalCenter;
                break;

            case ContentAlignment.BottomLeft:
                formatFlags |= TextFormatFlags.Bottom | TextFormatFlags.Left;
                break;

            case ContentAlignment.BottomRight:
                formatFlags |= TextFormatFlags.Bottom | TextFormatFlags.Right;
                break;

            case ContentAlignment.MiddleCenter:
                formatFlags |= TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter;
                break;

            case ContentAlignment.MiddleLeft:
                formatFlags |= TextFormatFlags.VerticalCenter | TextFormatFlags.Left;
                break;

            case ContentAlignment.MiddleRight:
                formatFlags |= TextFormatFlags.VerticalCenter | TextFormatFlags.Right;
                break;

            case ContentAlignment.TopCenter:
                formatFlags |= TextFormatFlags.Top | TextFormatFlags.HorizontalCenter;
                break;

            case ContentAlignment.TopLeft:
                formatFlags |= TextFormatFlags.Top | TextFormatFlags.Left;
                break;

            case ContentAlignment.TopRight:
                formatFlags |= TextFormatFlags.Top | TextFormatFlags.Right;
                break;
        }

        return formatFlags;
    }
    /// <summary>
    /// Calculates the actual drawing area for the text, based on the image settings.
    /// </summary>
    /// <returns>
    /// The <see cref="Rectangle"/> to draw the text in.
    /// </returns>
    private Rectangle CalculateTextRectangle()
    {
        int x = ClientRectangle.X;
        int y = ClientRectangle.Y;
        int width = Width;
        int height = Height;

        if (Template.Image == null || Template.TextImageRelation == TextImageRelation.Overlay)
        {
            // Standard Positioning
            y -= 2;
        }
        else
        {
            switch (Template.TextImageRelation)
            {
                case TextImageRelation.ImageBeforeText:
                    y = -2;
                    x += Template.Image.Width;
                    width -= Template.Image.Width;
                    break;

                case TextImageRelation.ImageAboveText:
                    y += Template.Image.Height + 2;
                    height -= y + 2;
                    break;

                case TextImageRelation.TextAboveImage:
                    y = -2;
                    height -= Template.Image.Height;
                    break;

                case TextImageRelation.TextBeforeImage:
                    y -= 2;
                    width -= Template.Image.Width;
                    break;
            }
        }

        return new Rectangle(x, y, width, height);
    }

    /// <summary>
    /// Draws the background of the control based on its state.
    /// </summary>
    /// <param name="g">The <see cref="Graphics"/> instance to use.
    /// </param>
    private void DrawBackground(Graphics g)
    {
        LinearGradientBrush backBrush;
        SetDrawingQuality(g);

        Rectangle actualRectangle = new Rectangle(0, -1, Width, Height + 2);

        backBrush = new LinearGradientBrush(ClientRectangle, Template.StartColor, Template.EndColor,
            Template.Mode);

        g.FillRectangle(backBrush, actualRectangle);
        backBrush.Dispose();
    }

    /// <summary>
    /// Draws the button image.
    /// </summary>
    /// <param name="g">The <see cref="Graphics"/> instance to use.
    /// </param>
    private void DrawButtonImage(Graphics? g)
    {
        if (Template.Image != null)
        {
            Rectangle location = CalculateImagePosition();
            try
            {
                g?.DrawImage(Template.Image, location);
            }
            catch (Exception ex)
            {
                ExceptionLog.LogException(ex);
            }
        }
    }

    /// <summary>
    /// Creates the pen object used to draw the text.
    /// </summary>
    /// <returns>
    /// A <see cref="Pen"/> instance.
    /// </returns>
    private Pen? GetBorderPen()
    {
        return Template.CreateBorderPen();
    }

    /// <summary>
    /// Sets the drawing quality.
    /// </summary>
    /// <param name="g">
    /// The <see cref="Graphics"/> instance to be set.
    /// </param>
    private void SetDrawingQuality(Graphics? g)
    {
        if (g != null)
        {
            // Set the drawing quality.
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        }
    }
    #endregion
}
