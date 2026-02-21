namespace Adaptive.Intelligence.Shared.UI.TemplatedControls.Algorithms;

/// <summary>
/// Provides the signature definition for implementing button drawing algorithm instances.
/// </summary>
/// <seealso cref="IDisposable" />
public interface IButtonDrawingAlgorithm : IDisposable
{
    /// <summary>
    /// Draws the background of the control based on its state.
    /// </summary>
    /// <param name="g">The <see cref="Graphics"/> instance to use.
    /// </param>
    /// <param name="drawingArea">
    /// A <see cref="Rectangle"/> specifying the drawing area.
    /// </param>
    void DrawBackground(Graphics g, Rectangle drawingArea);

    /// <summary>
    /// Draws the button.
    /// </summary>
    /// <param name="controlReference">
    /// The reference to the <see cref="Button"/> being drawn.
    /// </param>
    /// <param name="g">
    /// The <see cref="Graphics"/> reference from the button's paint methods.
    /// </param>
    void DrawButton(Button? controlReference, Graphics? g);
}