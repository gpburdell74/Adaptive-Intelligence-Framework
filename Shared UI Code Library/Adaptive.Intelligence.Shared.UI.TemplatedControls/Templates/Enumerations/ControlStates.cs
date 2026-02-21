namespace Adaptive.Intelligence.Shared.UI.TemplatedControls;

/// <summary>
/// Lists the control states that are currently supported for implementing templates for the UI.
/// </summary>
public enum ControlStates
{
    /// <summary>
    /// Indicates the control is in a normal, idle state.
    /// </summary>
    Normal = 0,

    /// <summary>
    /// Indicates the control is in a hovered / hovering state. 
    /// This value may not be used in all controls.
    /// </summary>
    Hover = 1,

    /// <summary>
    /// Indicates the control is in a checked (true) state.
    /// This value may not be used in all controls.
    /// </summary>
    Checked = 2,

    /// <summary>
    /// Indicates the control is in a mouse-down / pressed state.
    /// This value may not be used in all controls.
    /// </summary>
    Pressed = 3,

    /// <summary>
    /// Indicates the control is in a disabled state.
    /// </summary>
    Disabled = 4
}
