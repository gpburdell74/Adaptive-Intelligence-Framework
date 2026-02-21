namespace Adaptive.Intelligence.Shared.UI.TemplatedControls.States;

/// <summary>
/// Provides a template for a panel in a specified UI state.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
public sealed class PanelStateTemplate : ControlStateTemplateBase
{
    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="PanelStateTemplate"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public PanelStateTemplate()
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
            Font?.Dispose();
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Public Properties
    #endregion

    #region Public Methods / Functions
    /// <summary>
    /// Creates a new object that is a copy of the current instance.
    /// </summary>
    /// <returns>
    /// A new <see cref="PanelStateTemplate"/> that is a copy of this instance.
    /// </returns>
    public override PanelStateTemplate Clone()
    {
        return new PanelStateTemplate
        {
            BorderStyle = BorderStyle,
            BorderColor = BorderColor,
            EndColor = EndColor,
            StartColor = StartColor,
            ForeColor = ForeColor,
            Font = Font,
            Mode = Mode,
            BorderWidth = BorderWidth,
            CornerRadius = CornerRadius
        };
    }
    #endregion
}