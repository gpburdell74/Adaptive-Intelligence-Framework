namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Represents a bad data type specification error.
/// </summary>
/// <seealso cref="BlazorBasicException" />
public class BasicBadDataTypeException : BlazorBasicException
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicBadDataTypeException"/> class.
    /// </summary>
    /// <param name="lineNumber">An integer indicating the line number on which the error occurred.</param>
    public BasicBadDataTypeException(int lineNumber) : base(lineNumber, BlazorBasicErrorCodes.BadDataType)
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicBadDataTypeException"/> class.
    /// </summary>
    /// <param name="lineNumber">The line number.</param>
    /// <param name="message">The message.</param>
    public BasicBadDataTypeException(int lineNumber, string message) : base(lineNumber, BlazorBasicErrorCodes.BadDataType, message)
    {
    }
    #endregion
}
