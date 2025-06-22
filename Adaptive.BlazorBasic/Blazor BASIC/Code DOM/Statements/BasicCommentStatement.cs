using Adaptive.BlazorBasic.Services;
using Adaptive.LanguageService.Tokenization;
using System.Text;

namespace Adaptive.BlazorBasic.CodeDom;

/// <summary>
/// Represents a comment statement
/// </summary>
/// <remarks>
/// This statement is started with a ' or REM command.
/// </remarks>
/// <example>
///     
///     ' This is a comment.
///     REM THis is also a comment.
///     
/// </example>
/// <seealso cref="BasicCodeStatement" />
public class BasicCommentStatement : BasicCodeStatement
{
    #region Private Member Declarations    
    /// <summary>
    /// The comment text.
    /// </summary>
    private string? _commentText;
    #endregion

    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicCommentStatement"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public BasicCommentStatement(BlazorBasicLanguageService service) : base(service)
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicCommentStatement"/> class.
    /// </summary>
    /// <param name="commentText">
    /// A string containing the comment text.
    /// </param>
    public BasicCommentStatement(BlazorBasicLanguageService service, string commentText) : base(service)
    {
        _commentText = commentText;
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicCommentStatement"/> class.
    /// </summary>
    /// <param name="codeLine">An <see cref="ITokenizedCodeLine" /> containing the code line to be parsed.</param>
    public BasicCommentStatement(BlazorBasicLanguageService service, ITokenizedCodeLine codeLine) : base(service, codeLine)
    {
    }
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        _commentText = null;
        base.Dispose(disposing);
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets the comment text.
    /// </summary>
    /// <value>
    /// A string containing the comment text.
    /// </value>
    public string? CommentText => _commentText;
    #endregion

    #region Protected Method Overrides
    /// <summary>
    /// Parses the specified code content.
    /// </summary>
    /// <param name="codeLine">
    /// An <see cref="ITokenizedCodeLine"/> containing the code tokens to parse.
    /// </param>
    protected override void ParseIntoExpressions(ITokenizedCodeLine codeLine)
    {
        StringBuilder builder = new StringBuilder();

        int length = codeLine.Count;
        for (int index = 2; index < length; index++)
        {
            builder.Append(codeLine[index].Text);
        }
        _commentText = builder.ToString();
    }
    #endregion
}