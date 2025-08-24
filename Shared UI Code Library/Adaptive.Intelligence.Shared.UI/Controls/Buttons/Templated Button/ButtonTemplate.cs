using Adaptive.Intelligence.Shared.UI.Controls;
using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing.Drawing2D;

namespace Adaptive.Intelligence.Shared.UI;

/// <summary>
/// Provides a template for specifying the UI settings for different states of a <see cref="TemplatedButton"/>.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="ICloneable" />
public sealed class ButtonTemplate : DisposableObjectBase, ICloneable
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


    private static readonly Color DefaultCheckedStartColor = Color.FromArgb(255, 64, 64, 64);
    private static readonly Color DefaultCheckedEndColor = Color.FromArgb(255, 128, 128, 128);
    private static readonly Color DefaultCheckedTextColor = Color.White;

    private static readonly Color DefaultDisabledStartColor = Color.FromArgb(255, 192, 192, 192);
    private static readonly Color DefaultDisabledEndColor = Color.FromArgb(255, 224, 224, 224);
    private static readonly Color DefaultDisabledTextColor = Color.Gray;

    #endregion

    /// <summary>
    /// The settings template to use when the button is in a normal state.
    /// </summary>
    private ButtonStateTemplate? _normal = null;

    /// <summary>
    /// The settings template to use when the button is in a checked (true) state.
    /// </summary>
    private ButtonStateTemplate? _checked = null;

    /// <summary>
    /// The settings template to use when the button is in a hover state.
    /// </summary>
    private ButtonStateTemplate? _hover = null;

    /// <summary>
    /// The settings template to use when the button is in a pressed state.
    /// </summary>
    private ButtonStateTemplate? _pressed = null;

    /// <summary>
    /// The settings template to use when the button is in a disabled state.
    /// </summary>
    private ButtonStateTemplate? _disabled = null;

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
        _checked = new ButtonStateTemplate();
        _normal = new ButtonStateTemplate();
        _disabled = new ButtonStateTemplate();
        _hover = new ButtonStateTemplate();
        _pressed = new ButtonStateTemplate();

        SetDefaultValues();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ButtonTemplate"/> class.
    /// </summary>
    /// <remarks>
    /// This is used only when cloning the instance.
    /// </remarks>
    /// <param name="checkedTemplate">
    /// The <see cref="ButtonStateTemplate"/> to use when the button is in the checked state.
    /// </param>
    /// <param name="normalTemplate">
    /// The <see cref="ButtonStateTemplate"/> to use when the button is in the normal state.
    /// </param>
    /// <param name="disabledTemplate">
    /// The <see cref="ButtonStateTemplate"/> to use when the button is in the disabled state.
    /// </param>
    /// <param name="hoverTemplate">
    /// The <see cref="ButtonStateTemplate"/> to use when the button is in the hover state.
    /// </param>
    /// <param name="pressedTemplate">
    /// The <see cref="ButtonStateTemplate"/> to use when the button is in the pressed state.
    /// </param>
    internal ButtonTemplate(ButtonStateTemplate? checkedTemplate,
                           ButtonStateTemplate? normalTemplate,
                           ButtonStateTemplate? disabledTemplate,
                           ButtonStateTemplate? hoverTemplate,
                           ButtonStateTemplate? pressedTemplate)
    {
        _checked = checkedTemplate;
        _normal = normalTemplate;
        _disabled = disabledTemplate;
        _hover = hoverTemplate;
        _pressed = pressedTemplate;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ButtonTemplate"/> class.
    /// </summary>
    /// <param name="templateFile">
    /// A string containing the fully-qualified path and name of file to be read.
    /// </param>
    public ButtonTemplate(string templateFile)
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
    /// A <see cref="ButtonStateTemplate"/> instance, or <b>null</b>.
    /// </value>
    public ButtonStateTemplate? Checked { get => _checked; }

    /// <summary>
    /// Gets the reference to the template to use when the button is in a disabled state.
    /// </summary>
    /// <value>
    /// A <see cref="ButtonStateTemplate"/> instance, or <b>null</b>.
    /// </value>
    public ButtonStateTemplate? Disabled { get => _disabled; }


    /// <summary>
    /// Gets the reference to the template to use when the mouse is hovering over the button.
    /// </summary>
    /// <value>
    /// A <see cref="ButtonStateTemplate"/> instance, or <b>null</b>.
    /// </value>
    public ButtonStateTemplate? Hover { get => _hover; }

    /// <summary>
    /// Gets the reference to the template to use when the button is in a normal, idle state.
    /// </summary>
    /// <value>
    /// A <see cref="ButtonStateTemplate"/> instance, or <b>null</b>.
    /// </value>
    public ButtonStateTemplate? Normal { get => _normal; }

    /// <summary>
    /// Gets the reference to the template to use when the button is being pressed.
    /// </summary>
    /// <value>
    /// A <see cref="ButtonStateTemplate"/> instance, or <b>null</b>.
    /// </value>
    public ButtonStateTemplate? Pressed { get => _pressed; }

    #endregion

    #region 
    #endregion

    #region 
    #endregion

    #region Public Methods / Functions

    /// <summary>
    /// Clones this instance.
    /// </summary>
    /// <returns>
    /// A <see cref="ButtonTemplate"/> that is a copy of the current instance.
    /// </returns>
    public ButtonTemplate Clone()
    {
        return new ButtonTemplate(_checked, _normal, _disabled, _hover, _pressed);
    }

    /// <summary>
    /// Clones this instance.
    /// </summary>
    /// <returns>
    /// A <see cref="ButtonTemplate"/> that is a copy of the current instance.
    /// </returns>
    object ICloneable.Clone()
    {
        return Clone();
    }

    /// <summary>
    /// Loads the button template from the specified file.
    /// </summary>
    /// <param name="fileName">
    /// A string containing the fully-qualified path and name of the file.
    /// </param>
    /// <returns>
    /// The new <see cref="ButtonTemplate"/> instance that was loaded, or <b>null</b>.
    /// </returns>
    public static ButtonTemplate? Load(string fileName)
    {
        ButtonTemplate? template = null;

        ButtonTemplateFile file = new ButtonTemplateFile();
        OperationalResult<ButtonTemplate> result = file.LoadTemplate(fileName);
        if (result.Success)
            template = result.DataContent;
        result.Dispose();
        file.Dispose();

        return template;
    }


    /// <summary>
    /// Loads the button template from the specified file.
    /// </summary>
    /// <param name="templateData">
    /// A byte array containing the template data content.
    /// </param>
    /// <returns>
    /// The new <see cref="ButtonTemplate"/> instance that was loaded, or <b>null</b>.
    /// </returns>
    public static ButtonTemplate? Load(byte[]? templateData)
    {
        ButtonTemplate? template = null;

        ButtonTemplateFile file = new ButtonTemplateFile();
        OperationalResult<ButtonTemplate> result = file.LoadTemplate(templateData);
        if (result.Success)
            template = result.DataContent;
        result.Dispose();
        file.Dispose();

        return template;
    }

    /// <summary>
    /// Saves the button template to the specified file.
    /// </summary>
    /// <param name="fileName">
    /// A string containing the fully-qualified path and name of the file.
    /// </param>
    public bool Save(string fileName)
    {
        bool success = false;

        ButtonTemplateFile file = new ButtonTemplateFile();
        OperationalResult result = file.SaveTemplate(fileName, this);
        success = result.Success;
        result.Dispose();
        file.Dispose();

        return success;
    }
    #endregion

    #region Private Methods / Functions

    /// <summary>
    /// Sets the default values for the templates.
    /// </summary>
    private void SetDefaultValues()
    {
        // Create the font objects.
        _normal!.Font = new Font(UIConstants.StandardFontName, UIConstants.StandardFontSize, FontStyle.Regular);
        _hover!.Font = new Font(UIConstants.StandardFontName, UIConstants.StandardFontSize, FontStyle.Regular);
        _pressed!.Font = new Font(UIConstants.StandardFontName, UIConstants.StandardFontSize, FontStyle.Regular);
        _disabled!.Font = new Font(UIConstants.StandardFontName, UIConstants.StandardFontSize, FontStyle.Regular);
        _checked!.Font = new Font(UIConstants.StandardFontName, UIConstants.StandardFontSize, FontStyle.Regular);

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
