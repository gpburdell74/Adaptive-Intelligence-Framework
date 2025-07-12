namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Represents an internal error within the execution engine.
/// </summary>
/// <seealso cref="BlazorBasicException" />
 public sealed class BasicEngineExecutionException : BlazorBasicException
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicEngineExecutionException"/> class.
    /// </summary>
    /// <param name="lineNumber">
    /// An integer indicating the line number on which the error occurred.
    /// </param>
    public BasicEngineExecutionException(int lineNumber) : base(lineNumber, BlazorBasicErrorCodes.EngineError)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BasicEngineExecutionException"/> class.
    /// </summary>
    /// <param name="lineNumber">
    /// An integer indicating the line number on which the error occurred.
    /// </param>
    /// <param name="message">
    /// A string containing the additional error message.
    /// </param>
    public BasicEngineExecutionException(int lineNumber, string message) : base(lineNumber, BlazorBasicErrorCodes.EngineError, message)
    {
    }
    #endregion
}
