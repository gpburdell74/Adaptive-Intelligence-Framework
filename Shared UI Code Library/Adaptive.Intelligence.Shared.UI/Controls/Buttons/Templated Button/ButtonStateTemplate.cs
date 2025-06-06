using Adaptive.Intelligence.Shared.Logging;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace Adaptive.Intelligence.Shared.UI;

/// <summary>
/// Provides a template for a single button state.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
public sealed class ButtonStateTemplate : DisposableObjectBase, ICloneable
{

    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="ButtonStateTemplate"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public ButtonStateTemplate()
    {
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
            Image?.Dispose();
        }

        Image = null;
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
     Description("The font used to display text in the button.")]
    public Font Font { get; set; } = new Font("Tahoma", 12f);

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
    /// Gets or sets the image that is displayed on a button control.
    /// </summary>
    /// <value>
    /// An <see cref="Image"/> instance, or <b>null</b>.
    /// </value>
    [Browsable(true),
     Category("Appearance"),
     Description("Get or sets the image to display in the button."),
     DefaultValue(null)]
    public Image? Image { get; set; } = null;

    /// <summary>
    /// Gets or sets the alignment of the image on the button control.
    /// </summary>
    /// <value>
    /// A <see cref="ContentAlignment"/> enumerated value.
    /// </value>
    [DefaultValue(ContentAlignment.MiddleCenter)]
    [Localizable(true)]
    [Description("Determines how the image is aligned on the button.")]
    [Category("Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public ContentAlignment ImageAlign { get; set; } = ContentAlignment.MiddleCenter;

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
    /// Gets or sets a value indicating whether to draw a shadow under the text on the button
    /// </summary>
    /// <value>
    ///   <c>true</c> to draw a shadow under the text on the button; otherwise, <c>false</c>.
    /// </value>
    [Browsable(true),
     Category("Appearance"),
     DefaultValue(false),
     Description("Indicates whether to draw a shadow under the text on the button.")]
    public bool ShadowText { get; set; } = false;

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

    /// <summary>
    /// Gets or sets the position of text and image relative to each other.
    /// </summary>
    /// <value>
    /// An <see cref="TextImageRelation"/> enumerated value indicating the relationship. The default is 
    /// <see cref="TextImageRelation.ImageBeforeText"/>
    /// </value>
    [DefaultValue(TextImageRelation.Overlay)]
    [Localizable(true)]
    [CategoryAttribute("Appearance")]
    [DescriptionAttribute("The relation of positions of the image and the text.")]
    public TextImageRelation TextImageRelation { get; set; } = TextImageRelation.ImageBeforeText;
    #endregion

    #region Public Methods / Functions
    /// <summary>
    /// Creates a new object that is a copy of the current instance.
    /// </summary>
    /// <returns>
    /// A new <see cref="ButtonStateTemplate"/> that is a copy of this instance.
    /// </returns>
    public ButtonStateTemplate Clone()
    {
        return new ButtonStateTemplate
        {
            BorderStyle = BorderStyle,
            BorderColor = BorderColor,
            EndColor = EndColor,
            StartColor = StartColor,
            ForeColor = ForeColor,
            Font = Font,
            Mode = Mode,
            BorderWidth = BorderWidth,
            CornerRadius = CornerRadius,
            ShadowText = ShadowText,
            Image = CopyImage(Image),
            ImageAlign = ImageAlign,
            TextAlign = TextAlign,
            TextImageRelation = TextImageRelation
        };
    }
    /// <summary>
    /// Creates a new object that is a copy of the current instance.
    /// </summary>
    /// <returns>
    /// A new object that is a copy of this instance.
    /// </returns>
    object ICloneable.Clone()
    {
        return Clone();
    }

    /// <summary>
    /// Copies the specified <see cref="Image"/> object into a new image object.
    /// </summary>
    /// <param name="source">
    /// The <see cref="Image"/> instance containing the source image.
    /// </param>
    /// <returns>
    /// An <see cref="Image"/> that is a copy of <paramref name="source"/>, or <b>null</b>.
    /// </returns>
    public Image? CopyImage(Image? source)
    {
        Image? newImage = null;

        if (source != null)
        {

            // Save the current object to a memory stream.
            MemoryStream containerStream = new MemoryStream(1000);
            try
            {
                source.Save(containerStream, source.RawFormat);
                containerStream.Flush();
                containerStream.Seek(0, SeekOrigin.Begin);
            }
            catch (Exception ex)
            {
                ExceptionLog.LogException(ex);
            }

            // If successful, copy the data to the new object, and dispose of the stream.
            if (containerStream != null)
            {
                try
                {
                    newImage = Image.FromStream(containerStream);
                }
                catch (Exception ex)
                {
                    ExceptionLog.LogException(ex);
                }
                containerStream.Dispose();
            }
        }

        return newImage;
    }

    /// <summary>
    /// Creates a graphical <see cref="Pen"/> instance for drawing borders on the button.
    /// </summary>
    /// <returns>
    /// A <see cref="Pen"/> with the specified <see cref="BorderWidth"/> and <see cref="BorderColor"/>.
    /// </returns>
    public Pen? CreateBorderPen()
    {
        Pen? pen = null;

        // Width of zero or style set to none mean no pen is needed.
        if (BorderWidth > 0 && BorderStyle != BorderStyle.None)
        {
            try
            {
                pen = new Pen(BorderColor, BorderWidth);
            }
            catch(Exception ex)
            {
                pen = null;
                ExceptionLog.LogException(ex);
            }
        }
        return pen;
    }
    #endregion
}
