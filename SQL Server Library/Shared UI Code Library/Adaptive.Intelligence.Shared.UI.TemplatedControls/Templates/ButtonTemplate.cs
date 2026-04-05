using Adaptive.Intelligence.Shared.UI.TemplatedControls.States;
using System.Drawing.Drawing2D;


namespace Adaptive.Intelligence.Shared.UI.TemplatedControls;

/// <summary>
/// Provides a template for specifying the UI settings for different states of a <see cref="TemplatedButton"/>.
/// </summary>
/// <seealso cref="ControlTemplateBase" />
/// <seealso cref="ICloneable" />
public sealed class ButtonTemplate : ControlTemplateBase
{
    #region Private Member Declarations

    #region Private Constants

    private const int DefaultImageHeight16 = 16;
    private const int DefaultImageHeight32 = 32;

    private static readonly Color DefaultPressedStartColor = Color.Gray;
    private static readonly Color DefaultPressedEndColor = Color.FromArgb(174, 45, 61);
    private static readonly Color DefaultPressedTextColor = Color.White;

    private static readonly Color DefaultCheckedStartColor = Color.FromArgb(255, 64, 64, 64);
    private static readonly Color DefaultCheckedEndColor = Color.FromArgb(255, 128, 128, 128);
    private static readonly Color DefaultCheckedTextColor = Color.White;

    #endregion

    /// <summary>
    /// The settings template to use when the button is in a normal state.
    /// </summary>
    private StateTemplate? _normal = null;

    /// <summary>
    /// The settings template to use when the button is in a checked (true) state.
    /// </summary>
    private StateTemplate? _checked = null;

    /// <summary>
    /// The settings template to use when the button is in a hover state.
    /// </summary>
    private StateTemplate? _hover = null;

    /// <summary>
    /// The settings template to use when the button is in a pressed state.
    /// </summary>
    private StateTemplate? _pressed = null;

    /// <summary>
    /// The settings template to use when the button is in a disabled state.
    /// </summary>
    private StateTemplate? _disabled = null;

    #endregion

    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="ButtonTemplate"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public ButtonTemplate()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ButtonTemplate"/> class.
    /// </summary>
    /// <remarks>
    /// This is used only when cloning the instance.
    /// </remarks>
    /// <param name="checkedTemplate">
    /// The <see cref="StateTemplate"/> to use when the button is in the checked state.
    /// </param>
    /// <param name="normalTemplate">
    /// The <see cref="StateTemplate"/> to use when the button is in the normal state.
    /// </param>
    /// <param name="disabledTemplate">
    /// The <see cref="StateTemplate"/> to use when the button is in the disabled state.
    /// </param>
    /// <param name="hoverTemplate">
    /// The <see cref="StateTemplate"/> to use when the button is in the hover state.
    /// </param>
    /// <param name="pressedTemplate">
    /// The <see cref="StateTemplate"/> to use when the button is in the pressed state.
    /// </param>
    internal ButtonTemplate(StateTemplate? checkedTemplate,
                           StateTemplate? normalTemplate,
                           StateTemplate? disabledTemplate,
                           StateTemplate? hoverTemplate,
                           StateTemplate? pressedTemplate)
    {
        _checked = checkedTemplate;
        _normal = normalTemplate;
        _disabled = disabledTemplate;
        _hover = hoverTemplate;
        _pressed = pressedTemplate;
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
            _checked?.Dispose();
            _normal?.Dispose();
            _disabled?.Dispose();
            _hover?.Dispose();
            _pressed?.Dispose();

        }

        _checked = null;
        _normal = null;
        _disabled = null;
        _hover = null;
        _pressed = null;

        base.Dispose(disposing);
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets the reference to the template to use when the button is in a checked state.
    /// </summary>
    /// <value>
    /// A <see cref="StateTemplate"/> instance, or <b>null</b>.
    /// </value>
    public StateTemplate? Checked { get => _checked; set => _checked = value; }

    /// <summary>
    /// Gets the reference to the template to use when the button is in a disabled state.
    /// </summary>
    /// <value>
    /// A <see cref="StateTemplate"/> instance, or <b>null</b>.
    /// </value>
    public StateTemplate? Disabled { get => _disabled; set => _disabled = value; }

    /// <summary>
    /// Gets the reference to the template to use when the mouse is hovering over the button.
    /// </summary>
    /// <value>
    /// A <see cref="StateTemplate"/> instance, or <b>null</b>.
    /// </value>
    public StateTemplate? Hover { get => _hover; set => _hover = value; }

    /// <summary>
    /// Gets the reference to the template to use when the button is in a normal, idle state.
    /// </summary>
    /// <value>
    /// A <see cref="StateTemplate"/> instance, or <b>null</b>.
    /// </value>
    public StateTemplate? Normal { get => _normal; set => _normal = value; }

    /// <summary>
    /// Gets the reference to the template to use when the button is being pressed.
    /// </summary>
    /// <value>
    /// A <see cref="StateTemplate"/> instance, or <b>null</b>.
    /// </value>
    public StateTemplate? Pressed { get => _pressed; set => _pressed = value; }

    #endregion

    #region Public Methods / Functions

    /// <summary>
    /// Clones this instance.
    /// </summary>
    /// <returns>
    /// A <see cref="ButtonTemplate"/> that is a copy of the current instance.
    /// </returns>
    public override ButtonTemplate Clone()
    {
        return new ButtonTemplate(_checked, _normal, _disabled, _hover, _pressed);
    }
    #endregion

    #region Protected Methods / Functions
    /// <summary>
    /// Initializes the state template instances.
    /// </summary>
    protected override void InitializeTemplateInstances()
    {
        _checked = new StateTemplate();
        _normal = new StateTemplate();
        _disabled = new StateTemplate();
        _hover = new StateTemplate();
        _pressed = new StateTemplate();
    }

    /// <summary>
    /// Sets the default values for the templates.
    /// </summary>
    protected override void SetDefaultValues()
    {
        if (_normal == null || _hover == null || _pressed == null || _disabled == null || _checked == null)
        {
            throw new Exception("State Template instances have not been initialized.");
        }
            
        // Create the font objects.
        _normal.Font = new FontTemplate(Adaptive.Intelligence.Shared.UI.UIConstants.StandardFontName, UIConstants.StandardFontSize, FontStyle.Regular);
        _hover.Font = new FontTemplate(UIConstants.StandardFontName, UIConstants.StandardFontSize, FontStyle.Regular);
        _pressed.Font = new FontTemplate(UIConstants.StandardFontName, UIConstants.StandardFontSize, FontStyle.Regular);
        _disabled.Font = new FontTemplate(UIConstants.StandardFontName, UIConstants.StandardFontSize, FontStyle.Regular);
        _checked.Font = new FontTemplate(UIConstants.StandardFontName, UIConstants.StandardFontSize, FontStyle.Regular);

        // Checked State
        _checked.StartColor = DefaultCheckedStartColor;
        _checked.EndColor = DefaultCheckedEndColor;
        _checked.ForeColor = DefaultCheckedTextColor;
        _checked.Mode = LinearGradientMode.ForwardDiagonal;
        _checked.BorderWidth = DefaultBorderWidth;
        _checked.BorderColor = DefaultBorderColor;
        _checked.CornerRadius = 0;

        // Disabled State
        _disabled.StartColor = DefaultDisabledStartColor;
        _disabled.EndColor = DefaultDisabledEndColor;
        _disabled.ForeColor = DefaultDisabledTextColor;
        _disabled.Mode = LinearGradientMode.ForwardDiagonal;
        _disabled.BorderWidth = DefaultBorderWidth;
        _disabled.BorderColor = DefaultBorderColor;
        _disabled.CornerRadius = 0;

        // Normal State
        _normal.StartColor = DefaultNormalStartColor;
        _normal.EndColor = DefaultNormalEndColor;
        _normal.ForeColor = DefaultNormalTextColor;
        _normal.Mode = LinearGradientMode.ForwardDiagonal;
        _normal.BorderWidth = DefaultBorderWidth;
        _normal.BorderColor = DefaultBorderColor;
        _normal.CornerRadius = 0;

        // Hover State
        _hover.StartColor = DefaultHoverStartColor;
        _hover.EndColor = DefaultHoverEndColor;
        _hover.ForeColor = DefaultHoverTextColor;
        _hover.Mode = LinearGradientMode.BackwardDiagonal;
        _hover.BorderWidth = DefaultBorderWidth;
        _hover.BorderColor = DefaultBorderColor;
        _hover.CornerRadius = 0;

        // Pressed State
        _pressed.StartColor = DefaultPressedStartColor;
        _pressed.EndColor = DefaultPressedEndColor;
        _pressed.ForeColor = DefaultPressedTextColor;
        _pressed.Mode = LinearGradientMode.BackwardDiagonal;
        _pressed.BorderWidth = DefaultBorderWidth;
        _pressed.BorderColor = DefaultBorderColor;
        _pressed.CornerRadius = 0;
    }
    #endregion
}
