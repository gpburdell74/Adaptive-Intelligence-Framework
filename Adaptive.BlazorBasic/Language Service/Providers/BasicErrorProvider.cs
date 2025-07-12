using Adaptive.Intelligence.LanguageService.Providers;
using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.BlazorBasic.LanguageService;

/// <summary>
/// Provides the implementation for the provider that defines the list of errors and error codes.
/// </summary>
public sealed class BasicErrorProvider : DisposableObjectBase, IErrorProvider
{
    /// <summary>
    /// Renders the list of error ID values (e.g. error codes) for mapping.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}" /> of unique ID values used for mapping ID to text content.
    /// </returns>
    public List<int> RenderErrorIds()
    {
        return new List<int>(27)
        {
            (int)BlazorBasicErrorCodes.Success,
            (int)BlazorBasicErrorCodes.Syntax,
            (int)BlazorBasicErrorCodes.IllegalFunctionCall,
            (int)BlazorBasicErrorCodes.Overflow,
            (int)BlazorBasicErrorCodes.OutOfMemory,
            (int) BlazorBasicErrorCodes.SubscriptOutOfRange,
            (int) BlazorBasicErrorCodes.DuplicateDefinition,
            (int) BlazorBasicErrorCodes.DivisionByZero,
            (int) BlazorBasicErrorCodes.TypeMismatch,
            (int) BlazorBasicErrorCodes.VariableNotFound,
            (int) BlazorBasicErrorCodes.AllocationFailed,
            (int) BlazorBasicErrorCodes.InvalidArgument,
            (int) BlazorBasicErrorCodes.EngineError,
            (int) BlazorBasicErrorCodes.GeneralError,
            (int) BlazorBasicErrorCodes.BadFileNameOrNumber,
            (int)BlazorBasicErrorCodes.FileNotFound,
            (int) BlazorBasicErrorCodes.BadFileMode,
            (int) BlazorBasicErrorCodes.FileAlreadyOpen,
            (int) BlazorBasicErrorCodes.BadFileName,
            (int) BlazorBasicErrorCodes.DeviceUnavailable,
            (int) BlazorBasicErrorCodes.PermissionDenied,
            (int) BlazorBasicErrorCodes.PathFileAccessError,
            (int)BlazorBasicErrorCodes.PathNotFound,
            (int)BlazorBasicErrorCodes.OutOfStackSpace,
            (int)BlazorBasicErrorCodes.InvalidHandle,
            (int)BlazorBasicErrorCodes.Unknown,
            (int)BlazorBasicErrorCodes.KernelPanic
        };
    }

    /// <summary>
    /// Renders the list of error names for mapping.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}" /> of unique name values used for mapping text to ID values.
    /// </returns>
    public List<string> RenderErrorNames()
    {
        return new List<string>(27)
        {
            ErrorNames.ErrorSuccess,
            ErrorNames.ErrorSyntax,
            ErrorNames.ErrorIllegalFunctionCall,
            ErrorNames.ErrorOverflow,
            ErrorNames.ErrorOutOfMemory,
            ErrorNames.ErrorSubscriptOutOfRange,
            ErrorNames.ErrorDuplicateDefinition,
            ErrorNames.ErrorDivisionByZero,
            ErrorNames.ErrorTypeMismatch,
            ErrorNames.ErrorVariableNotFound,
            ErrorNames.ErrorAllocationFailed,
            ErrorNames.ErrorInvalidArgument,
            ErrorNames.ErrorEngineError,
            ErrorNames.ErrorGeneralError,
            ErrorNames.ErrorBadFileNameOrNumber,
            ErrorNames.ErrorFileNotFound,
            ErrorNames.ErrorBadFileMode,
            ErrorNames.ErrorFileAlreadyOpen,
            ErrorNames.ErrorBadFileName,
            ErrorNames.ErrorDeviceUnavailable,
            ErrorNames.ErrorPermissionDenied,
            ErrorNames.ErrorPathFileAccessError,
            ErrorNames.ErrorPathNotFound,
            ErrorNames.ErrorOutOfStackSpace,
            ErrorNames.ErrorInvalidHandle,
            ErrorNames.ErrorUnknown,
            ErrorNames.ErrorKernelPanic
        };
    }
}
