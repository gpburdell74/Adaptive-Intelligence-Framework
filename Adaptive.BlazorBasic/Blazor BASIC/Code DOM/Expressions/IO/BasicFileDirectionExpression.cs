using Adaptive.BlazorBasic.LanguageService.CodeDom;
using Adaptive.Intelligence.Shared;

namespace Adaptive.BlazorBasic.CodeDom;

/// <summary>
/// Represents a code expression for an I/O operation indicating the direction of the I/O or manner of creation of a file.
/// </summary>
/// <remarks>
/// This generally represents the "AS OUTPUT" or "AS INPUT", etc., section of an OPEN statement.
/// </remarks>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="ILanguageCodeExpression" />
public class BasicFileDirectionExpression : DisposableObjectBase, ILanguageCodeExpression
{
}
