namespace Adaptive.BlazorBasic;

/// <summary>
/// Provides the name constants for all the error codes.
/// </summary>
internal static class ErrorNames
{
    /// <summary>
    /// Indicates a successful operation - no error.
    /// </summary>
    public const string ErrorSuccess = "SUCCESS";
    /// <summary>
    /// Indicates a SYNTAX error.
    /// </summary>
    public const string ErrorSyntax = "?SYNTAX ERROR";
    /// <summary>
    /// Indicates an ILLEGAL FUNCTION CALL error.
    /// </summary>
    public const string ErrorIllegalFunctionCall = "?ILLEGAL FUNCTION CALL";
    /// <summary>
    /// Indicates an overflow error.
    /// </summary>
    public const string ErrorOverflow = "OVERFLOW";
    /// <summary>
    /// Indicates an out of memory error.
    /// </summary>
    public const string ErrorOutOfMemory = "OUT OF MEMORY";
    /// <summary>
    /// Indicates a subscript out of range error.
    /// </summary>
    public const string ErrorSubscriptOutOfRange = "SUBSCRIPT OUT OF RANGE";
    /// <summary>
    /// Indicates a duplicate definition error.
    /// </summary>
    public const string ErrorDuplicateDefinition = "DUPLICATE DEFINITION";
    /// <summary>
    /// Indicates a division by zero error.
    /// </summary>
    public const string ErrorDivisionByZero = "DIVISION BY ZERO";
    /// <summary>
    /// Indicates a type mismatch error.
    /// </summary>
    public const string ErrorTypeMismatch = "TYPE MISMATCH";
    /// <summary>
    /// Indicates a variable not found error.
    /// </summary>
    public const string ErrorVariableNotFound = "VARIABLE NOT FOUND";
    /// <summary>
    /// Indicates a memory allocation failed error.
    /// </summary>
    public const string ErrorAllocationFailed = "MEMORY ALLOCATION FAILED";
    /// <summary>
    /// Indicates an invalid argument specification.
    /// </summary>
    public const string ErrorInvalidArgument = "INVALID ARGUMENT SPECIFIED";
    /// <summary>
    /// Indicates a general parsing or processing engine error.
    /// </summary>
    public const string ErrorEngineError = "BLAZOR BASIC ENGINE ERROR";
    /// <summary>
    /// Indicates a general error.
    /// </summary>
    public const string ErrorGeneralError = "GENERAL ERROR";
    /// <summary>
    /// Indicates a bad file name or number error.
    /// </summary>
    public const string ErrorBadFileNameOrNumber = "BAD FILE NAME OR NUMBER";
    /// <summary>
    /// Indicates a file not found error.
    /// </summary>
    public const string ErrorFileNotFound = "FILE NOT FOUND";
    /// <summary>
    /// Indicates a bad file mode error.
    /// </summary>
    public const string ErrorBadFileMode = "BAD FILE MODE";
    /// <summary>
    /// Indicates a file is already open error.
    /// </summary>
    public const string ErrorFileAlreadyOpen = "FILE ALREADY OPEN";
    /// <summary>
    /// Indicates a bad file name error.
    /// </summary>
    public const string ErrorBadFileName = "BAD FILE NAME";
    /// <summary>
    /// Indicates a device unavailable error.
    /// </summary>
    public const string ErrorDeviceUnavailable = "DEVICE UNAVAILABLE";
    /// <summary>
    /// Indicates a permission denied error.
    /// </summary>
    public const string ErrorPermissionDenied = "PERMISSION DENIED";
    /// <summary>
    /// Indicates a path and or file access error.
    /// </summary>
    public const string ErrorPathFileAccessError = "PATH OR FILE ACCESS ERROR";
    /// <summary>
    /// Indicates a path not found error.
    /// </summary>
    public const string ErrorPathNotFound = "PATH NOT FOUND";
    /// <summary>
    /// Indicates an out of stack space error.
    /// </summary>
    public const string ErrorOutOfStackSpace = "OUT OF STACK SPACE";
    /// <summary>
    /// Indicates an invalid handle error.
    /// </summary>
    public const string ErrorInvalidHandle = "INVALID HANDLE";
    /// <summary>
    /// Indicates a unknown or undefined error.
    /// </summary>
    public const string ErrorUnknown = "UNKNOWN ERROR";
    /// <summary>
    /// Indicates the system is broken, and must be shut down and restarted.
    /// </summary>
    public const string ErrorKernelPanic = "KERNEL PANIC - END OF DAYS - SHUT DOWN SYSTEM!!";

}
