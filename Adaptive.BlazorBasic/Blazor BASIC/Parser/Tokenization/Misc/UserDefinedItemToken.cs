using Adaptive.BlazorBasic.LanguageService;

namespace Adaptive.BlazorBasic.Parser;

/// <summary>
/// Provides a token for an element of text that represents a user-defined value, such as a procedure or function call,
/// or a parameter or variable reference, or other such element.
/// </summary>
/// <seealso cref="TokenBase" />
/// <seealso cref="IUserDefinedToken"/>
public class UserDefinedItemToken : TokenBase, IUserDefinedToken
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserDefinedItemToken"/> class.
    /// </summary>
    /// <param name="text">
    /// A string containing the text the token was created from.
    /// </param>
    public UserDefinedItemToken(string? text) : base(TokenType.UserDefinedItem, text)
    {
        if (text == "OUTPUT")
            System.Diagnostics.Debug.Write("X");
    }
}
