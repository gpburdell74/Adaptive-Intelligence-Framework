using Adaptive.Intelligence.Shared.Logging;
using Adaptive.Intelligence.LanguageService.Tokenization;

namespace Adaptive.Intelligence.BlazorBasic.LanguageService;

/// <summary>
/// Provides a token to represent a number value with a decimal point.
/// </summary>
/// <seealso cref="TokenBase" />
/// <seealso cref="IDataTypeToken{T}" />
public class FloatingPointToken : TokenBase, IDataTypeToken<float>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FloatingPointToken"/> class.
    /// </summary>
    /// <param name="text">
    /// A string containing the text the token was created from.
    /// </param>
    public FloatingPointToken(string? text) : base(TokenType.FloatingPoint, text)
    {

    }

    /// <summary>
    /// Gets the value being represented.
    /// </summary>
    /// <returns>
    /// The <see cref="float"/> value, if it can be provided/parsed.
    /// </returns>
    public float GetValue()
    {
        float value = 0;
        try
        {
            value = Convert.ToSingle(Text);
        }
        catch (Exception ex)
        {
            ExceptionLog.LogException(ex);
        }
        return value;
    }
}
