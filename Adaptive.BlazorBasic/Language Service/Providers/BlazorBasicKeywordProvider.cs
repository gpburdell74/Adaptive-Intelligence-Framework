using Adaptive.Intelligence.LanguageService.Providers;
using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.BlazorBasic.LanguageService;

/// <summary>
/// Provides the implementation for the provider that defines the list of built-in functions.
/// </summary>
public sealed class BlazorBasicKeywordProvider : DisposableObjectBase, IKeywordProvider
{
    /// <summary>
    /// Renders the list of keyword ID values for mapping.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}" /> of unique ID values used for mapping ID to text content.
    /// </returns>
    public List<int> RenderKeywordIds()
    {
        return new List<int>()
        {
            (int)BlazorBasicKeywords.NoOp,
            (int)BlazorBasicKeywords.Comment,
            (int)BlazorBasicKeywords.Comment,
            (int)BlazorBasicKeywords.Close,
            (int)BlazorBasicKeywords.Cls,
            (int)BlazorBasicKeywords.Dim,
            (int)BlazorBasicKeywords.Do,
            (int)BlazorBasicKeywords.End,
            (int)BlazorBasicKeywords.For,
            (int)BlazorBasicKeywords.Function,
            (int)BlazorBasicKeywords.If,
            (int)BlazorBasicKeywords.Input,
            (int)BlazorBasicKeywords.Let,
            (int)BlazorBasicKeywords.Loop,
            (int)BlazorBasicKeywords.Open,
            (int)BlazorBasicKeywords.Next,
            (int)BlazorBasicKeywords.Print,
            (int)BlazorBasicKeywords.Procedure,
            (int)BlazorBasicKeywords.Read,
            (int)BlazorBasicKeywords.Write,
            (int)BlazorBasicKeywords.Append,
            (int)BlazorBasicKeywords.Output,
            (int)BlazorBasicKeywords.Random,
            (int)BlazorBasicKeywords.As,
            (int)BlazorBasicKeywords.Then,

        };
    }

    /// <summary>
    /// Renders the list of keyword names for mapping.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}" /> of unique name values used for mapping text to ID values.
    /// </returns>
    public List<string> RenderKeywordNames()
    {
        return new List<string>()
        {
            KeywordNames.CommandNoOp,
            KeywordNames.CommandCommentShort,
            KeywordNames.CommandCommentRemark,
            KeywordNames.CommandClose,
            KeywordNames.CommandCls,
            KeywordNames.CommandDim,
            KeywordNames.CommandDo,
            KeywordNames.CommandEnd,
            KeywordNames.CommandFor,
            KeywordNames.CommandFunction,
            KeywordNames.CommandIf,
            KeywordNames.CommandInput,
            KeywordNames.CommandLet ,
            KeywordNames.CommandLoop ,
            KeywordNames.CommandOpen,
            KeywordNames.CommandNext,
            KeywordNames.CommandPrint,
            KeywordNames.CommandProcedure,
            KeywordNames.CommandRead,
            KeywordNames.CommandWrite,
            KeywordNames.IOAppend,
            KeywordNames.IOOutput,
            KeywordNames.IORandom,
            KeywordNames.KeywordAs,
            KeywordNames.KeywordThen

        };
    }
}
