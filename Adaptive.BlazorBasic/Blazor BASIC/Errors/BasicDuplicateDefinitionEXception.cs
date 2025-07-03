namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Represents a duplicate definition error, of a procedure, function or variable or other such item.
/// </summary>
/// <seealso cref="BlazorBasicException" />
public class BasicDuplicateDefinitionException : BlazorBasicException
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicDuplicateDefinitionException"/> class.
    /// </summary>
    /// <param name="lineNumber">An integer indicating the line number on which the error occurred.</param>
    public BasicDuplicateDefinitionException(int lineNumber) : base(lineNumber, BlazorBasicErrorCodes.DuplicateDefinition)
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicDuplicateDefinitionException"/> class.
    /// </summary>
    /// <param name="lineNumber">The line number.</param>
    /// <param name="message">The message.</param>
    public BasicDuplicateDefinitionException(int lineNumber, string message) : base(lineNumber, BlazorBasicErrorCodes.DuplicateDefinition, message)
    {
    }
    #endregion
}
