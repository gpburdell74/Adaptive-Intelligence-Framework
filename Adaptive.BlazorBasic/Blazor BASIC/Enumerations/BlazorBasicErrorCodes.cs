namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Lists the error codes used by the Blazor BASIC language service.
/// </summary>
public enum BlazorBasicErrorCodes
{
    /// <summary>
    /// Indicates a successful operation - no error.
    /// </summary>
    Success = 0,
    /// <summary>
    /// Indicates a SYNTAX error.
    /// </summary>
    Syntax = 2,
    /// <summary>
    /// Indicates an ILLEGAL FUNCTION CALL error.
    /// </summary>
    IllegalFunctionCall = 5,
    /// <summary>
    /// Indicates an overflow error.
    /// </summary>
    Overflow = 6,
    /// <summary>
    /// Indicates an out of memory error.
    /// </summary>
    OutOfMemory = 7,
    /// <summary>
    /// Indicates a subscript out of range error.
    /// </summary>
    SubscriptOutOfRange = 9,
    /// <summary>
    /// Indicates a duplicate definition error.
    /// </summary>
    DuplicateDefinition = 10,
    /// <summary>
    /// Indicates a division by zero error.
    /// </summary>
    DivisionByZero = 11,
    /// <summary>
    /// Indicates a type mismatch error.
    /// </summary>
    TypeMismatch = 13,
    /// <summary>
    /// Indicates a variable not found error.
    /// </summary>
    VariableNotFound = 14,
    /// <summary>
    /// Indicates a memory allocation failed error.
    /// </summary>
    AllocationFailed = 15,
    /// <summary>
    /// Indicates an invalid argument error.
    /// </summary>
    InvalidArgument = 16,
    /// <summary>
    /// Indicates an invalid or bad data type is specified.
    /// </summary>
    BadDataType = 17,
    /// <summary>
    /// Indicates a general parsing or processing engine error.
    /// </summary>
    EngineError = 50,
    /// <summary>
    /// Indicates a general error.
    /// </summary>
    GeneralError = 51,
    /// <summary>
    /// Indicates a bad file name or number error.
    /// </summary>
    BadFileNameOrNumber = 52,
    /// <summary>
    /// Indicates a file not found error.
    /// </summary>
    FileNotFound = 53,
    /// <summary>
    /// Indicates a bad file mode error.
    /// </summary>
    BadFileMode = 54,
    /// <summary>
    /// Indicates a file is already open error.
    /// </summary>
    FileAlreadyOpen = 55,
    /// <summary>
    /// Indicates the requested file handle is already in use.
    /// </summary>
    FileHandleInUse = 56,
    /// <summary>
    /// Indicates a bad file name error.
    /// </summary>
    BadFileName = 64,
    /// <summary>
    /// Indicates a device unavailable error.
    /// </summary>
    DeviceUnavailable = 68,
    /// <summary>
    /// Indicates a permission denied error.
    /// </summary>
    PermissionDenied = 70,
    /// <summary>
    /// Indicates a path and or file access error.
    /// </summary>
    PathFileAccessError = 75,
    /// <summary>
    /// Indicates a path not found error.
    /// </summary>
    PathNotFound = 76,
    /// <summary>
    /// Indicates an out of stack space error.
    /// </summary>
    OutOfStackSpace = 256,
    /// <summary>
    /// Indicates a general I/O error.
    /// </summary>
    IOError = 257,
    /// <summary>
    /// Indicates an invalid handle error.
    /// </summary>
    InvalidHandle = 258,
    /// <summary>
    /// Indicates a feature or operation not supported error.
    /// </summary>
    NotSupported = 259,
    /// <summary>
    /// Indicates a unknown or undefined error.
    /// </summary>
    Unknown = 999,
    /// <summary>
    /// Indicates the system is broken, and must be shut down and restarted.
    /// </summary>
    KernelPanic = 1025
}
