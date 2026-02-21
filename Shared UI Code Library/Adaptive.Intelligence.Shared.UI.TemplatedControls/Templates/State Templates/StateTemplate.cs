using System.ComponentModel;

namespace Adaptive.Intelligence.Shared.UI.TemplatedControls.States;

/// <summary>
/// Provides a template for a single button state.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
public sealed class StateTemplate : ControlStateTemplateBase
{
    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="StateTemplate"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public StateTemplate()
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
        }

        Image = null;
        base.Dispose(disposing);
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets or sets the image that is displayed on a button control as a base-64 encoded string.
    /// </summary>
    /// <value>
    /// An <see cref="string"/> containing the base-64 encoded image to display on the button, or <b>null</b>.
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
    /// A new <see cref="StateTemplate"/> that is a copy of this instance.
    /// </returns>
    public override StateTemplate Clone()
    {
        return new StateTemplate
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
            Image = Image,
            ImageAlign = ImageAlign,
            TextAlign = TextAlign,
            TextImageRelation = TextImageRelation
        };
    }
    #endregion
} 