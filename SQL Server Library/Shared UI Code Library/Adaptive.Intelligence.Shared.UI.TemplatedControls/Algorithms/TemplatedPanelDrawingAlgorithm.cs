using Adaptive.Intelligence.Shared.UI.TemplatedControls.States;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace Adaptive.Intelligence.Shared.UI.TemplatedControls.Algorithms;

/// <summary>
/// Provides an implementation of the <see cref="IPanelDrawingAlgorithm"/> interface to use when 
/// drawing template panels.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="IPanelDrawingAlgorithm" />
public class TemplatedPanelDrawingAlgorithm : DisposableObjectBase, IPanelDrawingAlgorithm
{
    #region Private Member Declarations

    #region Private Constants

    private const int DefaultBorderWidth = 1;

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
    /// The current panel state.
    /// </summary>
    private ControlStates _state = ControlStates.Normal;

    /// <summary>
    /// The template to use.
    /// </summary>
    private PanelTemplate? _template;

    /// <summary>
    /// The current state template reference.
    /// </summary>
    private StateTemplate? _currentState;
    #endregion

    #region Constructor / Dispose Methods    
    /// <summary>
    /// Initializes a new instance of the <see cref="TemplatedPanelDrawingAlgorithm" /> class.
    /// </summary>
    /// <param name="template">
    /// The <see cref="PanelTemplate"/> instance to use.
    /// </param>
    public TemplatedPanelDrawingAlgorithm(PanelTemplate template)
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
    /// Gets or sets the state of the related panel.
    /// </summary>
    /// <value>
    /// A <see cref="ControlStates"/> enumerated value indicating the state of the panel.
    /// </value>
    public ControlStates State
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
    /// Draws the panel.
    /// </summary>
    /// <param name="g">
    /// The <see cref="Graphics"/> reference from the button's paint methods.
    /// </param>
    /// <param name="controlReference">
    /// The reference to the <see cref="Panel"/> being drawn.
    /// </param>
    public void DrawPanel(Panel? controlReference, Graphics? g)
    {
        if (controlReference != null && g != null && controlReference.Visible && controlReference.IsHandleCreated)
        {
            Rectangle area = controlReference.ClientRectangle;

            // Draw the background.
            DrawBackground(g, area);

            // Set the font reference.
            Font? textFont = _currentState?.Font.ToFont();
            
            // Draw the text.
            DrawText(g, area, textFont, controlReference.Text);

            // Draw the border.
            DrawBorder(g, area);
        }
    }
    #endregion

    #region Private Methods / Functions
    /// <summary>
    /// Sets the current template to used based on panel state.
    /// </summary>
    private void SetTemplateReference()
    {
        if (_template != null)
        {
            switch (_state)
            {
                case ControlStates.Disabled:
                    _currentState = _template.Disabled;
                    break;

                case ControlStates.Normal:
                    _currentState = _template.Normal;
                    break;

                case ControlStates.Hover:
                    _currentState = _template.Hover;
                    break;
            }
        }
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
        int y = drawingArea.Y - 2;
        int width = drawingArea.Width;
        int height = drawingArea.Height;

        return new Rectangle(x, y, width, height);
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
