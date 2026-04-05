using System.Text;

namespace Adaptive.Intelligence.Shared.UI.TemplatedControls;

/// <summary>
/// Contains the information for specifying a font name, size, style, and weight.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
public sealed class FontTemplate : DisposableObjectBase
{
    #region Private Member Declarations
    /// <summary>
    /// The default font family name.
    /// </summary>
    private const string DEFAULT_FONT_FAMILY = "Segoe UI";

    /// <summary>
    /// The default font size.
    /// </summary>
    private const float DEFAULT_FONT_SIZE = 9.75f;

    /// <summary>
    /// The default character set.
    /// </summary>
    private const byte DEFAULT_CHARSET = 1;

    #endregion

    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="FontTemplate"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public FontTemplate() : this(DEFAULT_FONT_FAMILY, DEFAULT_FONT_SIZE, FontStyle.Regular,
         GraphicsUnit.Point, false, DEFAULT_CHARSET)

    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FontTemplate"/> class.
    /// </summary>
    /// <param name="fontFamily">
    /// A string containing the font family name / font name.
    /// </param>
    /// <param name="size">
    /// A float containing the size of the font.
    /// </param>
    /// <param name="style">
    /// A <see cref="FontStyle"/> enumerated value specifying the style.
    /// </param>
    /// <param name="unit">
    /// An optional <see cref="GraphicsUnit"/> enumerated value specifying the unit for the font size.
    /// </param>
    /// <param name="gdiVerticalFont">
    /// An optional parameter specifying whether the font is vertical.
    /// </param>
    /// <param name="gdiCharSet">
    /// An optional paramter specifying the GDI character set to use.
    /// </param>
    public FontTemplate(
        string fontFamily,
        float size,
        FontStyle style,
        GraphicsUnit unit = GraphicsUnit.Point,
        bool gdiVerticalFont = false,
        byte gdiCharSet = DEFAULT_CHARSET)
    {
        FontFamily = fontFamily;
        GdiCharSet = gdiCharSet;
        GdiVerticalFont = gdiVerticalFont;
        Size = size;
        Style = style;
        Unit = unit;
    }
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        FontFamily = string.Empty;
        base.Dispose(disposing);
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets or sets the font family name or the name of the font to be used.
    /// </summary>
    /// <value>
    /// A string containing the font name and/or the font family name value.
    /// </value>
    public string FontFamily { get; set; }

    /// <summary>
    /// Gets or sets a vlaue indicating the GDI character set to be used.
    /// </summary>
    /// <value>
    /// A byte value specifying the GDI character set.  This is typically 1 for ANSI.
    /// </value>
    public byte GdiCharSet { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the font is vertical.
    /// </summary>
    /// <value>
    ///   <c>true</c> if the font is vertical; otherwise, <c>false</c>.
    /// </value>
    public bool GdiVerticalFont { get; set; }

    /// <summary>
    /// Gets or sets the size of the font, in the specified <see cref="Unit"/> value.
    /// </summary>
    /// <value>
    /// A <see cref="float"/> containing the size of the font.
    /// </value>
    public float Size { get; set; }

    /// <summary>
    /// Gets or sets the font style.
    /// </summary>
    /// <remarks>
    /// This property can be a combination of one or more <see cref="FontStyle"/> enumerated values, indicating
    /// whether the font is bold, italic, underlined, or struck through or a combination of these styles.
    /// </remarks>
    /// <value>
    /// A <see cref="FontStyle"/> enumerated value containing the font style.
    /// </value>
    public FontStyle Style { get; set; }

    /// <summary>
    /// Gets or sets the unit used in determining the font size.
    /// </summary>
    /// <value>
    /// A <see cref="GraphicsUnit"/> enumerated value specifying the unit type.
    /// </value>
    public GraphicsUnit Unit { get; set; }
    #endregion

    #region Public Methods / Functions
    /// <summary>
    /// Creates a <see cref="Font"/> object from the current <see cref="FontTemplate"/> instance.
    /// </summary>
    /// <returns>
    /// The <see cref="Font"/> instance to be used.
    /// </returns>
    public Font ToFont()
    {
        return new Font(
            new FontFamily(FontFamily ?? DEFAULT_FONT_FAMILY),
            Size,
            Style,
            Unit,
            GdiCharSet,
            GdiVerticalFont);
    }

    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append(FontFamily + " " + Size);
        switch (Unit)
        {
            case GraphicsUnit.Point:
                builder.Append("pt ");
                break;

            case GraphicsUnit.Pixel:
                builder.Append("px ");
                break;

            case GraphicsUnit.Millimeter:
                builder.Append("mm ");
                break;

            case GraphicsUnit.Document:
                builder.Append("dc ");
                break;

            case GraphicsUnit.Inch:
                builder.Append("\" ");
                break;

            case GraphicsUnit.World:
                builder.Append("w ");
                break;
        }
        if ((int)Style > 0)
        {
            builder.Append(" (");
            if (Style.HasFlag(FontStyle.Bold))
            {
                builder.Append("Bold ");
            }
            if (Style.HasFlag(FontStyle.Italic))
            {
                builder.Append("Italic ");
            }
            if (Style.HasFlag(FontStyle.Underline))
            {
                builder.Append("Underline ");
            }
            if (Style.HasFlag(FontStyle.Strikeout))
            {
                builder.Append("Strikeout ");
            }

            builder.Append(")");
        }
        return builder.ToString();
    }
    #endregion
}