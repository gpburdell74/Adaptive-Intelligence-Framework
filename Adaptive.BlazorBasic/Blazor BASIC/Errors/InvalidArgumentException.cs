namespace Adaptive.BlazorBasic;

/// <summary>
/// Represents a syntax error.
/// </summary>
/// <seealso cref="BlazorBasicException" />
public class InvalidArgumentException : BlazorBasicException
{
    public InvalidArgumentException(int lineNumber) : base(lineNumber,BlazorBasicErrorCodes.InvalidArgument)
    {
    }
    public InvalidArgumentException(int lineNumber, string message) : base(lineNumber, BlazorBasicErrorCodes.InvalidArgument, message)
    {
    }
}
