namespace Adaptive.Intelligence.Shared.UI;

/// <summary>
/// Provides the syntax editor specifically for JSON text.
/// </summary>
/// <seealso cref="SyntaxEditor" />
public class JsonSyntaxEditor : SyntaxEditor
{
    /// <summary>
    /// Initializes a new instance of the <see cref="JsonSyntaxEditor"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public JsonSyntaxEditor()
    {
        SyntaxProvider = new JsonSyntaxProvider();
    }
    /// <summary>
    /// Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Control" /> and its child controls and optionally releases the managed resources.
    /// </summary>
    /// <param name="disposing"><see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        if (!IsDisposed && disposing)
        {
            SyntaxProvider?.Dispose();
        }

        SyntaxProvider = null;
        base.Dispose(disposing);
    }
}
