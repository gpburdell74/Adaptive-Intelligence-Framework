using Adaptive.Intelligence.LanguageService.CodeDom;
using Adaptive.Intelligence.Shared;
using System.Text;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Provides methods and functions for rendering Blazor BASIC code statements from the Code DOM objects.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="ILanguageCodeStatementRenderer" />
public class BlazorBasicStatementRenderer : DisposableObjectBase, ILanguageCodeStatementRenderer
{
    #region Private Member Declarations
    /// <summary>
    /// The current indentation level.
    /// </summary>
    private int _indentLevel;
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicStatementRenderer"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public BlazorBasicStatementRenderer()
    {
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets or sets the number of spaces to use when indenting, and <see cref="P:Adaptive.Intelligence.LanguageService.CodeDom.ILanguageCodeStatementRenderer.UseTabs" /> is false.
    /// </summary>
    /// <value>
    /// An integer indicating the number of spaces to use.
    /// </value>
    public int IndentionSpaces { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether to use tabs for indention.
    /// </summary>
    /// <value>
    /// <c>true</c> if to use tabs for indention; otherwise, <c>false</c> to use the <see cref="P:Adaptive.Intelligence.LanguageService.CodeDom.ILanguageCodeStatementRenderer.IndentionSpaces" />
    /// number of spaces.
    /// </value>
    public bool UseTabs { get; set; }
    #endregion

    #region Public Methods / Functions
    /// <summary>
    /// Renders the code statement as a string.
    /// </summary>
    /// <param name="statement">
    /// The <see cref="ILanguageCodeStatement" /> instance to render, or <b>null</b>.
    /// </param>
    /// <returns>
    /// A string containing the rendered content, or <b>null</b>.
    /// </returns>
    public string? RenderStatement(ILanguageCodeStatement? statement)
    {
        string? line = null;

        if (statement != null)
            return ApplyIndentationSettings(statement.TabModification) + statement.Render();
        return line;
    }

    /// <summary>
    /// Renders the code statement as a string.
    /// </summary>
    /// <param name="builder">
    /// The <see cref="StringBuilder" /> instance to append a line of code to.
    /// </param>
    /// <param name="statement">
    /// The <see cref="ILanguageCodeStatement" /> instance to render, or <b>null</b>.
    /// </param>
    public void RenderStatement(StringBuilder builder, ILanguageCodeStatement? statement)
    {
        if (statement != null)
        {
            builder.AppendLine(RenderStatement(statement));
        }
    }
    /// <summary>
    /// Renders the code statement as a string.
    /// </summary>
    /// <param name="destinationStream">
    /// The <see cref="Stream" /> instance to write to.
    /// </param>
    /// <param name="statement">
    /// The <see cref="ILanguageCodeStatement" /> instance to render, or <b>null</b>.
    /// </param>
    public void RenderStatement(Stream destinationStream, ILanguageCodeStatement? statement)
    {
        if (statement != null)
        {
            if (destinationStream != null && destinationStream.CanWrite)
            {
                StreamWriter writer = new StreamWriter(destinationStream);
                writer.WriteLine(RenderStatement(statement));
                writer.Flush();
            }
        }
    }

    /// <summary>
    /// Renders the list of code statements.
    /// </summary>
    /// <param name="builder">
    /// The <see cref="StringBuilder" /> instance to append a line of code to.
    /// </param>
    /// <param name="statementList">
    /// An <see cref="IEnumerable{T}" /> list of  <see cref="ILanguageCodeStatement" /> instances to be rendered, or <b>null</b>.
    /// </param>
    public void RenderStatements(StringBuilder builder, IEnumerable<ILanguageCodeStatement>? statementList)
    {
        if (statementList != null)
        {
            foreach (ILanguageCodeStatement statement in statementList)
            {
                builder.AppendLine(RenderStatement(statement));
            }
        }
    }

    /// <summary>
    /// Renders the list of code statements.
    /// </summary>
    /// <param name="destinationStream">
    /// The <see cref="Stream" /> instance to write to.
    /// </param>
    /// <param name="statementList">
    /// An <see cref="IEnumerable{T}" /> list of  <see cref="ILanguageCodeStatement" /> instances to be rendered, or <b>null</b>.
    /// </param>
    public void RenderStatements(Stream destinationStream, IEnumerable<ILanguageCodeStatement>? statementList)
    {
        if (statementList != null)
        {
            if (destinationStream.CanWrite)
            {
                StreamWriter writer = new StreamWriter(destinationStream);
                foreach (ILanguageCodeStatement statement in statementList)
                {
                    writer.WriteLine(RenderStatement(statement));
                }
                writer.Flush();
            }
        }
    }
    #endregion

    #region Private Methods / Functions
    /// <summary>
    /// Applies the indentation settings to the start of the line of code.
    /// </summary>
    /// <param name="tabState">
    /// The <see cref="RenderTabState"/> enumeration value indicating how to modify the indentation.
    /// </param>
    /// <returns>
    /// A string with the preceding indentation content.
    /// </returns>
    private string ApplyIndentationSettings(RenderTabState tabState)
    {
        // Modify the indentation level based on the statement.
        switch(tabState)
        {
            case RenderTabState.NoTab:
                _indentLevel = 0;
                break;

            case RenderTabState.Indent:
                _indentLevel++;
                break;

            case RenderTabState.Exdent:
                _indentLevel--;
                break;
        }

        // Return the tabs or spaces plus the original statement.
        string result;
        if (UseTabs)
        {
            result = new string('\t', _indentLevel);
        }
        else
        {
            result = new string(' ', _indentLevel * IndentionSpaces);
        }

        // Modify the indentation level based on the statement for the "after effects".
        switch (tabState)
        {
            case RenderTabState.IndentAfter:
            case RenderTabState.NoTabAndIndentAfter:
                _indentLevel++;
                break;
        }

        return result;
    }
    #endregion
}
