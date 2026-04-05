namespace Adaptive.Intelligence.Shared.UI.TemplatedControls.Algorithms;

/// <summary>
/// Provides the signature definition for implementing panel drawing algorithm instances.
/// </summary>
/// <seealso cref="IDisposable" />
public interface IPanelDrawingAlgorithm : IDisposable
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
    /// Draws the panel.
    /// </summary>
    /// <param name="controlReference">
    /// The reference to the <see cref="Panel"/> being drawn.
    /// </param>
    /// <param name="g">
    /// The <see cref="Graphics"/> reference from the button's paint methods.
    /// </param>
    void DrawPanel(Panel? controlReference, Graphics? g);
}