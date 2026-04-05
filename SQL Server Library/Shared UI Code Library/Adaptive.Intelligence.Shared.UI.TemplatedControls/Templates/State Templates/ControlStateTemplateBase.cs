using Adaptive.Intelligence.Shared.Logging;
using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing.Drawing2D;

namespace Adaptive.Intelligence.Shared.UI.TemplatedControls.States;

/// <summary>
/// Provides the base definition for defining a template for a control in a specified UI state.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
public abstract class ControlStateTemplateBase : DisposableObjectBase
{
    #region Private Member Declarations
    /// <summary>
    /// The font template instance.
    /// </summary>
    private FontTemplate? _fontTemplate;
    #endregion

    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="ControlTemplateBase"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    protected ControlStateTemplateBase()
    {
        _fontTemplate = new FontTemplate();
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
            _fontTemplate?.Dispose();
        }

        _fontTemplate = null;
        base.Dispose(disposing);
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets or sets the border style.
    /// </summary>
    /// <value>
    /// A <see cref="BorderStyle"/> enumerated value indicating the border style for the button.
    /// </value>
    [Browsable(true),
     Category("Appearance"),
     Description("Sets the style of the border to be drawn for the button."),
     DefaultValue(1)]
    public BorderStyle BorderStyle { get; set; } = BorderStyle.FixedSingle;

    /// <summary>
    /// Gets or sets the border color value.
    /// </summary>
    /// <value>
    /// A <see cref="Color"/> structure indicating the border color.
    /// </value>
    [Browsable(true),
     Category("Appearance"),
     Description("The color of the border, if drawn.")]
    public Color BorderColor { get; set; } = SystemColors.ControlDark;

    /// <summary>
    /// Gets or sets the width of the border.
    /// </summary>
    /// <value>
    /// An integer specifying the width of the border, in pixels, if drawn.
    /// </value>
    [Browsable(true),
     Category("Appearance"),
     Description("The width of the border in pixels, if drawn."),
     DefaultValue(1)]
    public int BorderWidth { get; set; } = 1;

    /// <summary>
    /// Gets or sets the circular radius of the button's rounded edges.
    /// </summary>
    /// <value>
    /// An integer specifying the radius of the corners of the button, in pixels, if drawn.
    /// </value>
    [Browsable(true),
     Category("Appearance"),
     Description("Specifies the radius of the corners of the button, in pixels, if drawn"),
     DefaultValue(0)]
    public int CornerRadius { get; set; } = 0;

    /// <summary>
    /// Gets or sets the color of the ending background gradient.
    /// </summary>
    /// <value>
    /// A <see cref="Color"/> specifying the ending of the color gradient when the background is drawn.
    /// </value>
    [Browsable(true),
     Category("Appearance"),
     Description("The ending of the color gradient when the background is drawn.")]
    public Color EndColor { get; set; } = SystemColors.Control;

    /// <summary>
    /// Gets or sets the reference to the font to use to draw text.
    /// </summary>
    /// <value>
    /// A <see cref="Font"/> instance.
    /// </value>
    [Browsable(true),
     Category("Appearance"),
     Localizable(true),
     AmbientValue(null),
     Description("The font used to display the text."),
     Editor(typeof(FontTemplateTypeEditor), typeof(UITypeEditor))]
    public FontTemplate Font
    {
        get
        {
            if (_fontTemplate == null)
            {
                _fontTemplate = new FontTemplate();
            }
            return _fontTemplate;
        }
        set
        {
            _fontTemplate?.Dispose();
            _fontTemplate = value;
        }
    }
    /// <summary>
    /// Gets or sets the foreground color for the button.
    /// </summary>
    /// <value>
    /// A <see cref="Color"/> specifying the foreground color value.
    /// </value>
    [Browsable(true),
     Category("Appearance"),
     Description("The foreground color of this component, which is used to display text.")]
    public Color ForeColor { get; set; } = SystemColors.ControlText;

    /// <summary>
    /// Gets or sets the mode in how the background gradient will be drawn.
    /// </summary>
    /// <value>
    /// A <see cref="LinearGradientMode"/> enumerated value indicating the direction of the gradient.
    /// </value>
    [Browsable(true),
     Category("Appearance"),
     Description("Specifies the direction of a linear gradient for the background of the button.")]
    public LinearGradientMode Mode { get; set; } = LinearGradientMode.Horizontal;

    /// <summary>
    /// Gets or sets the color of the starting background gradient.
    /// </summary>
    /// <value>
    /// A <see cref="Color"/> specifying the starting of the color gradient when the background is drawn.
    /// </value>
    [Browsable(true),
     Category("Appearance"),
     Description("The starting of the color gradient when the background is drawn.")]
    public Color StartColor { get; set; } = SystemColors.Control;

    /// <summary>
    /// Gets or sets the alignment of the image on the button control.
    /// </summary>
    /// <value>
    /// A <see cref="ContentAlignment"/> enumerated value.
    /// </value>
    [DefaultValue(ContentAlignment.MiddleCenter)]
    [Description("Determines how the text is aligned on the button.")]
    [Category("Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [RefreshProperties(RefreshProperties.Repaint)]
    public ContentAlignment TextAlign { get; set; } = ContentAlignment.MiddleCenter;

    #endregion

    #region Public Methods / Functions    
    /// <summary>
    /// Creates a new object that is a copy of the current instance.
    /// </summary>
    /// <returns>
    /// A new object that is a copy of this instance.
    /// </returns>
    /// <exception cref="NotImplementedException">
    /// This is not implemented in the base class.
    /// </exception>
    public virtual object Clone()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Creates a graphical <see cref="Pen"/> instance for drawing borders on the button.
    /// </summary>
    /// <returns>
    /// A <see cref="Pen"/> with the specified <see cref="BorderWidth"/> and <see cref="BorderColor"/>.
    /// </returns>
    public virtual Pen? CreateBorderPen()
    {
        Pen? pen = null;

        // Width of zero or style set to none mean no pen is needed.
        if (BorderWidth > 0 && BorderStyle != BorderStyle.None)
        {
            try
            {
                pen = new Pen(BorderColor, BorderWidth);
            }
            catch (Exception ex)
            {
                ExceptionLog.LogException(ex);
                pen = null;
            }
        }
        return pen;
    }
    #endregion
}