namespace Adaptive.Intelligence.Shared.UI.TemplatedControls;

/// <summary>
/// Provides the base definition for a control UI template for containing the settings for different states of
/// a templated control.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
public abstract class ControlTemplateBase : DisposableObjectBase, ICloneable
{
    #region Protected Constants

    internal const int DefaultBorderWidth = 1;
    internal const int MinPadding = 5;

    internal static readonly Color DefaultBorderColor = Color.Gray;

    internal static readonly Color DefaultNormalStartColor = Color.FromArgb(248, 248, 248);
    internal static readonly Color DefaultNormalEndColor = Color.Silver;
    internal static readonly Color DefaultNormalTextColor = Color.Black;

    internal static readonly Color DefaultHoverStartColor = Color.FromArgb(218, 194, 204);
    internal static readonly Color DefaultHoverEndColor = Color.FromArgb(224, 224, 224);
    internal static readonly Color DefaultHoverTextColor = Color.Black;

    internal static readonly Color DefaultDisabledStartColor = Color.FromArgb(255, 192, 192, 192);
    internal static readonly Color DefaultDisabledEndColor = Color.FromArgb(255, 224, 224, 224);
    internal static readonly Color DefaultDisabledTextColor = Color.Gray;

    #endregion

    #region Constructor / Dispose Methods

    /// <summary>
    /// Initializes a new instance of the <see cref="ControlTemplateBase"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    protected ControlTemplateBase()
    {
        InitializeTemplateInstances();
        SetDefaultValues();
    }
    #endregion

    #region Public Properties

    #endregion

    #region Public Methods / Functions
    /// <summary>
    /// Creates a new object that is a copy of the current instance.
    /// </summary>
    /// <returns>
    /// A new object that is a copy of this instance.
    /// </returns>
    public abstract object Clone();
    #endregion

    #region Protected Methods / Functions
    /// <summary>
    /// Initializes the state template instances.
    /// </summary>
    protected abstract void InitializeTemplateInstances();

    /// <summary>
    /// Sets the default values for the templates.
    /// </summary>
    protected abstract void SetDefaultValues();
    #endregion
}
