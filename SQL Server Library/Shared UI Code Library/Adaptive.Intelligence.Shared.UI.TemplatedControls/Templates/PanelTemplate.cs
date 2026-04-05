using Adaptive.Intelligence.Shared.UI.TemplatedControls.States;
using System.Drawing.Drawing2D;

namespace Adaptive.Intelligence.Shared.UI.TemplatedControls;

/// <summary>
/// Provides a template for specifying the UI settings for different states of a 
/// <see cref="TemplatedGradientPanel"/>.
/// </summary>
/// <seealso cref="ControlTemplateBase" />
public sealed class PanelTemplate : ControlTemplateBase
{
    #region Private Member Declarations

    /// <summary>
    /// The settings template to use when the panel is in a normal state.
    /// </summary>
    private StateTemplate? _normal = null;

    /// <summary>
    /// The settings template to use when the panel is in a hover state.
    /// </summary>
    private StateTemplate? _hover = null;

    /// <summary>
    /// The settings template to use when the panel is in a disabled state.
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
    public PanelTemplate()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ButtonTemplate"/> class.
    /// </summary>
    /// <remarks>
    /// This is used only when cloning the instance.
    /// </remarks>
    /// <param name="normalTemplate">
    /// The <see cref="StateTemplate"/> to use when the button is in the normal state.
    /// </param>
    /// <param name="disabledTemplate">
    /// The <see cref="StateTemplate"/> to use when the button is in the disabled state.
    /// </param>
    /// <param name="hoverTemplate">
    /// The <see cref="StateTemplate"/> to use when the button is in the hover state.
    /// </param>
    internal PanelTemplate(StateTemplate? normalTemplate,
                           StateTemplate? disabledTemplate,
                           StateTemplate? hoverTemplate)
    {
        _normal = normalTemplate;
        _disabled = disabledTemplate;
        _hover = hoverTemplate;
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
            _normal?.Dispose();
            _disabled?.Dispose();
            _hover?.Dispose();

        }

        _normal = null;
        _disabled = null;
        _hover = null;

        base.Dispose(disposing);
    }
    #endregion

    #region Public Properties
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
    #endregion

    #region Public Methods / Functions
    /// <summary>
    /// Clones this instance.
    /// </summary>
    /// <returns>
    /// A <see cref="PanelTemplate"/> that is a copy of the current instance.
    /// </returns>
    public override PanelTemplate Clone()
    {
        return new PanelTemplate(_normal, _disabled, _hover);
    }
    #endregion

    #region Protected Methods / Functions
    /// <summary>
    /// Initializes the state template instances.
    /// </summary>
    protected override void InitializeTemplateInstances()
    {
        // Create the state templates.
        _normal = new StateTemplate();
        _disabled = new StateTemplate();
        _hover = new StateTemplate();
    }

    /// <summary>
    /// Sets the default values for the templates.
    /// </summary>
    protected override void SetDefaultValues()
    {
        if (_normal == null || _disabled == null || _hover == null)
        {
            throw new InvalidOperationException("State templates have not been initialized.");
        }            

        // Create the font objects.
        _normal.Font = new FontTemplate(UIConstants.StandardFontName, UIConstants.StandardFontSize, FontStyle.Regular);
        _hover.Font = new FontTemplate(UIConstants.StandardFontName, UIConstants.StandardFontSize, FontStyle.Regular);
        _disabled.Font = new FontTemplate(UIConstants.StandardFontName, UIConstants.StandardFontSize, FontStyle.Regular);

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
    }
    #endregion
}
