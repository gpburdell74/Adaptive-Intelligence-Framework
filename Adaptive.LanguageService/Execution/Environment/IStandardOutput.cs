namespace Adaptive.Intelligence.LanguageService.Execution;

/// <summary>
/// Provides the signature definition for the standard output mechanism.
/// </summary>
/// <seealso cref="IDisposable" />
public interface IStandardOutput : IDisposable
{
    /// <summary>
    /// Sets the foreground and background colors for the console output.
    /// </summary>
    /// <param name="foreground">The foreground.</param>
    /// <param name="background">The background.</param>
    void Color(int foreground, int background);

    /// <summary>
    /// Clears the output screen or other display.
    /// </summary>
    void Cls();

    /// <summary>
    /// Moves the cursor to the specified position in the output display.
    /// </summary>
    /// <param name="x">
    /// An integer specifying the X co-ordinate for the cursor.
    /// </param>
    /// <param name="y">
    /// An integer specifying the Y co-ordinate for the cursor.
    /// </param>
    void Locate(int x, int y);

    /// <summary>
    /// Prints the specified text.
    /// </summary>
    /// <param name="text">
    /// A string contianing the text.
    /// </param>
    void Print(string text);

    /// <summary>
    /// Prints the specified text and terminates the line with a newline character.
    /// </summary>
    /// <param name="text">
    /// A string contianing the text.
    /// </param>
    void PrintLine(string text);
}