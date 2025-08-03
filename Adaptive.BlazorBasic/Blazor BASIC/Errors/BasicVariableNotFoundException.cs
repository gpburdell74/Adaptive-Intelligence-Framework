namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Represents a syntax error.
/// </summary>
/// <seealso cref="BlazorBasicException" />
public class BasicVariableNotFoundException : BlazorBasicException
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicVariableNotFoundException"/> class.
    /// </summary>
    /// <param name="lineNumber">An integer indicating the line number on which the error occurred.</param>
    public BasicVariableNotFoundException(int lineNumber) : base(lineNumber, BlazorBasicErrorCodes.VariableNotFound)
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicVariableNotFoundException"/> class.
    /// </summary>
    /// <param name="lineNumber">The line number.</param>
    /// <param name="message">The message.</param>
    public BasicVariableNotFoundException(int lineNumber, string message) : base(lineNumber, BlazorBasicErrorCodes.VariableNotFound, message)
    {
    }
    #endregion
}
