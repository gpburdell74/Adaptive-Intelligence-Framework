using Adaptive.Intelligence.Shared.Logging;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace Adaptive.Intelligence.Shared.UI;

/// <summary>
/// Provides an implementation of the <see cref="IButtonDrawingAlgorithm"/> interface to use when 
/// drawing template buttons.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="IButtonDrawingAlgorithm" />
public class TemplatedButtonDrawingAlgorithm : DisposableObjectBase, IButtonDrawingAlgorithm
{
    #region Private Member Declarations

    #region Private Constants

    private const int DefaultBorderWidth = 1;
    private const int DefaultImageHeight16 = 16;
    private const int DefaultImageHeight32 = 32;

    private const int MinPadding = 5;

    private static readonly Color DefaultBorderColor = Color.Gray;
    private static readonly Color DefaultNormalStartColor = Color.FromArgb(248, 248, 248);
    private static readonly Color DefaultNormalEndColor = Color.Silver;
    private static readonly Color DefaultNormalTextColor = Color.Black;

    private static readonly Color DefaultHoverStartColor = Color.FromArgb(218, 194, 204);
    private static readonly Color DefaultHoverEndColor = Color.FromArgb(224, 224, 224);
    private static readonly Color DefaultHoverTextColor = Color.Black;

    private static readonly Color DefaultPressedStartColor = Color.Gray;
    private static readonly Color DefaultPressedEndColor = Color.FromArgb(174, 45, 61);
    private static readonly Color DefaultPressedTextColor = Color.White;

    #endregion

    /// <summary>
    /// The current button state.
    /// </summary>
    private ButtonState _state = ButtonState.Normal;

    /// <summary>
    /// The template to use.
    /// </summary>
    private ButtonTemplate? _template;

    /// <summary>
    /// The current state template reference.
    /// </summary>
    private ButtonStateTemplate? _currentState;
    #endregion

    #region Constructor / Dispose Methods    
    /// <summary>
    /// Initializes a new instance of the <see cref="TemplatedButtonDrawingAlgorithm" /> class.
    /// </summary>
    /// <param name="template">
    /// The <see cref="ButtonTemplate"/> instance to use.
    /// </param>
    public TemplatedButtonDrawingAlgorithm(ButtonTemplate template)
    {
        _template = template;
        SetTemplateReference();
    }
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        if (!IsDisposed && disposing)
        {
            _template?.Dispose();
        }

        _currentState = null;
        _template = null;
        base.Dispose(disposing);
    }
    #endregion

    #region Public Properties    
    /// <summary>
    /// Gets or sets the state of the related button.
    /// </summary>
    /// <value>
    /// A <see cref="ButtonState"/> enumerated value indicating the state of the button.
    /// </value>
    public ButtonState State
    {
        get => _state;
        set
        {
            if (_state != value)
            {
                _state = value;
                SetTemplateReference();
            }
        }
    }
    #endregion

    #region Public Methods / Functions

    /// <summary>
    /// Draws the background of the control based on its state.
    /// </summary>
    /// <param name="g">The <see cref="Graphics"/> instance to use.
    /// </param>
    /// <param name="drawingArea">
    /// A <see cref="Rectangle"/> specifying the drawing area.
    /// </param>
    public void DrawBackground(Graphics g, Rectangle drawingArea)
    {
        LinearGradientBrush backBrush;
        SetDrawingQuality(g);

        if (_currentState != null)
        {
            Rectangle actualRectangle = new Rectangle(0, -1, drawingArea.Width, drawingArea.Height + 2);
            backBrush = new LinearGradientBrush(
                drawingArea,
                _currentState.StartColor,
                _currentState.EndColor,
                _currentState.Mode);

            g.FillRectangle(backBrush, actualRectangle);
            backBrush.Dispose();
        }
    }

    /// <summary>
    /// Draws the button.
    /// </summary>
    /// <param name="g">
    /// The <see cref="Graphics"/> reference from the button's paint methods.
    /// </param>
    /// <param name="controlReference">
    /// The reference to the <see cref="Button"/> being drawn.
    /// </param>
    public void DrawButton(Button? controlReference, Graphics? g)
    {
        if (controlReference != null && g != null && controlReference.Visible && controlReference.IsHandleCreated)
        {
            Rectangle area = controlReference.ClientRectangle;

            // Draw the background.
            DrawBackground(g, area);

            // Set the font reference.
            Font ? textFont = _currentState?.Font;

            // Draw the image.
            DrawButtonImage(g, area);

            // Draw the text.
            DrawText(g, area, textFont, controlReference.Text);

            // Draw the border.
            DrawBorder(g, area);
        }
    }
    #endregion

    #region Private Methods / Functions    
    /// <summary>
    /// Sets the current template to used based on button state.
    /// </summary>
    private void SetTemplateReference()
    {
        if (_template != null)
        {
            switch (_state)
            {
                case ButtonState.Checked:
                    _currentState = _template.Checked;
                    break;

                case ButtonState.Disabled:
                    _currentState = _template.Disabled;
                    break;

                case ButtonState.Normal:
                    _currentState = _template.Normal;
                    break;

                case ButtonState.Hover:
                    _currentState = _template.Hover;
                    break;

                case ButtonState.Pressed:
                    _currentState = _template.Pressed;
                    break;
            }
        }
    }

    /// <summary>
    /// Calculates the image position and size.
    /// </summary>
    /// <param name="drawingArea">
    /// A <see cref="Rectangle"/> specifying the drawing area.
    /// </param>
    /// <returns>
    /// A <see cref="Rectangle"/> containing the X and Y co-ordinates and the width and height of the image.
    /// </returns>
    private Rectangle CalculateImagePosition(Rectangle drawingArea)
    {
        if (_currentState == null)
            return new Rectangle(0, 0, 0, 0);

        int imageX = 0;
        int imageY = 0;

        int height = DefaultImageHeight16;
        int width = DefaultImageHeight16;

        Image? image = _currentState?.Image;
        if (image != null)
        {
            int imageHeight;
            try
            {
                imageHeight = image.Height;
            }
            catch
            {
                imageHeight = DefaultImageHeight16;
            }

            if (imageHeight >= DefaultImageHeight32)
            {
                height = DefaultImageHeight32;
                width = DefaultImageHeight32;
            }

            // Calculate the Y location.
            switch (_currentState!.ImageAlign)
            {
                case ContentAlignment.BottomCenter:
                case ContentAlignment.BottomLeft:
                case ContentAlignment.BottomRight:
                    imageY = (drawingArea.Height - MinPadding) - height;
                    break;

                case ContentAlignment.TopCenter:
                case ContentAlignment.TopLeft:
                case ContentAlignment.TopRight:
                    imageY = MinPadding;
                    break;

                case ContentAlignment.MiddleCenter:
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.MiddleRight:
                    imageY = ((drawingArea.Height - height) / 2);
                    break;
            }

            // Calculate the X location.
            switch (_currentState!.ImageAlign)
            {
                case ContentAlignment.TopCenter:
                case ContentAlignment.BottomCenter:
                case ContentAlignment.MiddleCenter:
                    imageX = ((drawingArea.Width - width) / 2);
                    break;

                case ContentAlignment.TopRight:
                case ContentAlignment.MiddleRight:
                case ContentAlignment.BottomRight:
                    imageX = drawingArea.Width - (width + MinPadding);
                    break;

                case ContentAlignment.TopLeft:
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.BottomLeft:
                    imageX = MinPadding;
                    break;
            }
        }

        return new Rectangle(imageX, imageY, width, height);
    }

    /// <summary>
    /// Draws the button border.
    /// </summary>
    /// <param name="drawingArea">
    /// A <see cref="Rectangle"/> specifying the drawing area.
    /// </param>
    /// <param name="g">
    /// The <see cref="Graphics"/> object used to draw the button.
    /// </param>
    private void DrawBorder(Graphics g, Rectangle drawingArea)
    {
        if (_currentState != null && _currentState.BorderWidth > 0 && _currentState.BorderStyle != BorderStyle.None)
        {
            Pen? borderPen = _currentState.CreateBorderPen();
            if (borderPen != null)
            {
                g.DrawRectangle(borderPen, 0, 0, drawingArea.Width - 1, drawingArea.Height - 1);
                borderPen.Dispose();
            }
        }
    }

    /// <summary>
    /// Draws the text.
    /// </summary>
    /// <param name="g">
    /// The <see cref="Graphics"/> object used to draw the button.
    /// </param>
    /// <param name="textFont">
    /// The <see cref="Font"/> to use with the text.
    /// </param>
    /// <param name="drawingArea">
    /// A <see cref="Rectangle"/> specifying the drawing area.
    /// </param>
    /// <param name="text">
    /// A string containing the text to be drawn.
    /// </param>
    private void DrawText(Graphics? g, Rectangle drawingArea, Font? textFont, string text)
    {
        if (g != null && textFont != null && _currentState != null)
        {
            TextFormatFlags formatFlags = GetTextFormatFlags();
            Rectangle adjustmentRectangle = CalculateTextRectangle(drawingArea);

            TextRenderer.DrawText(g, text, textFont, adjustmentRectangle, _currentState.ForeColor, formatFlags);
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

        if (_currentState != null)
        {
            switch (_currentState.TextAlign)
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
        }

        return formatFlags;
    }

    /// <summary>
    /// Calculates the actual drawing area for the text, based on the image settings.
    /// </summary>
    /// <returns>
    /// The <see cref="Rectangle"/> to draw the text in.
    /// </returns>
    private Rectangle CalculateTextRectangle(Rectangle drawingArea)
    {
        if (_currentState == null)
            return new Rectangle(0, 0, 0, 0);

        int x = drawingArea.X;
        int y = drawingArea.Y;
        int width = drawingArea.Width;
        int height = drawingArea.Height;

        if (_currentState.Image == null ||
            !ImageIsValid(_currentState.Image) || 
            _currentState.TextImageRelation == TextImageRelation.Overlay)
        {
            // Standard Positioning
            y -= 2;
        }
        else
        {
            switch (_currentState.TextImageRelation)
            {
                case TextImageRelation.ImageBeforeText:
                    y = -2;
                    try
                    {
                        x += _currentState.Image.Width;
                        width -= _currentState.Image.Width;
                    }
                    catch { }
                    
                    break;

                case TextImageRelation.ImageAboveText:
                    y += _currentState.Image.Height + 2;
                    height -= y + 2;
                    break;

                case TextImageRelation.TextAboveImage:
                    y = -2;
                    height -= _currentState.Image.Height;
                    break;

                case TextImageRelation.TextBeforeImage:
                    y -= 2;
                    width -= _currentState.Image.Width;
                    break;
            }
        }

        return new Rectangle(x, y, width, height);
    }

    /// <summary>
    /// Draws the button image.
    /// </summary>
    /// <param name="g">The <see cref="Graphics"/> instance to use.
    /// </param>
    /// <param name="drawingArea">
    /// A <see cref="Rectangle"/> specifying the drawing area.
    /// </param>
    private void DrawButtonImage(Graphics g, Rectangle drawingArea)
    {
        Image? image = _currentState?.Image;
        if (image != null && ImageIsValid(image))
        {
            Rectangle location = CalculateImagePosition(drawingArea);
            try
            {
                g?.DrawImage(image, location);
            }
            catch (Exception ex)
            {
                ExceptionLog.LogException(ex);
            }
        }
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

    private bool ImageIsValid(Image image)
    {
        bool isValid = false;

        try
        {
            int x = image.Width;
            int y = image.Height;
            isValid = true;
        }
        catch (Exception ex)
        {
            ExceptionLog.LogException(ex);
        }
        return isValid;
    }
    #endregion
}
