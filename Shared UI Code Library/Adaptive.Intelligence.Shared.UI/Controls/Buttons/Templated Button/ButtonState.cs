namespace Adaptive.Intelligence.Shared.UI;

/// <summary>
/// Lists the button states that are currently supported.
/// </summary>
public enum ButtonState
{
    /// <summary>
    /// Indicates the button is in a normal, idle state.
    /// </summary>
    Normal = 0,
    /// <summary>
    /// Indicates the button is in a hovered / hovering state.
    /// </summary>
    Hover = 1,
    /// <summary>
    /// Indicates the button is in a checked (true) state.
    /// </summary>
    Checked = 2,
    /// <summary>
    /// Indicates the button is in a mouse-down / pressed state.
    /// </summary>
    Pressed = 3,
    /// <summary>
    /// Indicates the button is in a disabled state.
    /// </summary>
    Disabled = 4
}
