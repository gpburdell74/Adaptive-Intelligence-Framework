using System.Text;

namespace Adaptive.Intelligence.LanguageService.CodeDom.Statements;

/// <summary>
/// Provides the signature definition for rendering one or more code statements.
/// </summary>
/// <seealso cref="System.IDisposable" />
public interface ICodeStatementRenderer : IDisposable 
{
    /// <summary>
    /// Gets or sets the number of spaces to use when indenting, and <see cref="UseTabs"/> is false.
    /// </summary>
    /// <value>
    /// An integer indicating the number of spaces to use.
    /// </value>
    int IndentionSpaces { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether to use tabs for indention.
    /// </summary>
    /// <value>
    ///   <c>true</c> if to use tabs for indention; otherwise, <c>false</c> to use the <see cref="IndentionSpaces"/>
    ///   number of spaces.
    /// </value>
    bool UseTabs { get; set; }

    /// <summary>
    /// Renders the code statement as a string.
    /// </summary>
    /// <param name="statement">
    /// The <see cref="ICodeStatement"/> instance to render, or <b>null</b>.
    /// </param>
    /// <returns>
    /// A string containing the rendered content, or <b>null</b>.
    /// </returns>
    string? RenderStatement(ICodeStatement? statement);

    /// <summary>
    /// Renders the code statement as a string.
    /// </summary>
    /// <param name="builder">
    /// The <see cref="StringBuilder"/> instance to append a line of code to.
    /// </param>
    /// <param name="statement">
    /// The <see cref="ICodeStatement"/> instance to render, or <b>null</b>.
    /// </param>
    /// <returns>
    /// A string containing the rendered content, or <b>null</b>.
    /// </returns>
    void RenderStatement(StringBuilder builder, ICodeStatement? statement);


    /// <summary>
    /// Renders the code statement as a string.
    /// </summary>
    /// <param name="destinationStream">
    /// The <see cref="Stream"/> instance to write to.
    /// </param>
    /// <param name="statement">
    /// The <see cref="ICodeStatement"/> instance to render, or <b>null</b>.
    /// </param>
    /// <returns>
    /// A string containing the rendered content, or <b>null</b>.
    /// </returns>
    void RenderStatement(Stream destinationStream, ICodeStatement? statement);

    /// <summary>
    /// Renders the list of code statements.
    /// </summary>
    /// <param name="builder">
    /// The <see cref="StringBuilder"/> instance to append each line of code to.
    /// </param>
    /// <param name="statementList">
    /// An <see cref="IEnumerable{T}"/> list of  <see cref="ICodeStatement"/> instances to be rendered, or <b>null</b>.
    /// </param>
    void RenderStatements(StringBuilder builder, IEnumerable<ICodeStatement>? statementList);

    /// <summary>
    /// Renders the list of code statements.
    /// </summary>
    /// <param name="destinationStream">
    /// The <see cref="Stream"/> instance to write to.
    /// </param>
    /// <param name="statementList">
    /// An <see cref="IEnumerable{T}"/> list of  <see cref="ICodeStatement"/> instances to be rendered, or <b>null</b>.
    /// </param>
    void RenderStatements(Stream destinationStream, IEnumerable<ICodeStatement>? statementList);

}
