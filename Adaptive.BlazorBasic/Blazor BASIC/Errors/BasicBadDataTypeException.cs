using Adaptive.Intelligence.LanguageService.Errors;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Represents a bad data type specification error.
/// </summary>
/// <seealso cref="BlazorBasicException" />
/// <seealso cref="ICodeBadDataTypeException"/>
public sealed class BasicBadDataTypeException : BlazorBasicException, ICodeBadDataTypeException
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicBadDataTypeException"/> class.
    /// </summary>
    /// <param name="lineNumber">
    /// An integer indicating the line number on which the error occurred.
    /// </param>
    public BasicBadDataTypeException(int lineNumber) : base(lineNumber, BlazorBasicErrorCodes.BadDataType)
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicBadDataTypeException"/> class.
    /// </summary>
    /// <param name="lineNumber">
    /// An integer indicating the line number on which the error occurred.
    /// </param>
    /// <param name="message">
    /// A string containing the additional error message.
    /// </param>
    public BasicBadDataTypeException(int lineNumber, string message) : base(lineNumber, BlazorBasicErrorCodes.BadDataType, message)
    {
    }
    #endregion
}
