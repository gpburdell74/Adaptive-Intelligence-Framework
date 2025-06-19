using Adaptive.BlazorBasic.LanguageService.CodeDom;
using Adaptive.Intelligence.Shared;

namespace Adaptive.BlazorBasic.CodeDom;

/// <summary>
/// Represents a code expression for an I/O operation indicating the number/handle value for the file or I/O operation.
/// </summary>
/// <remarks>
/// This generally represents the "#[number]"  section of an OPEN statement or the "#[number]" of a CLOSE or PRINT statement.
/// </remarks>
/// <example>
///     This represents the "#1" part of the following statements:
///     
///     OPEN "abc.dat" FOR OUTPUT AS #1
///     PRINT #1, "Data"
///     CLOSE #1
///     
/// </example>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="ILanguageCodeExpression" />
public class BasicFileNumberExpression : DisposableObjectBase, ILanguageCodeExpression
{
}
