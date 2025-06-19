using Adaptive.BlazorBasic.LanguageService;
using Adaptive.Intelligence.Shared;

namespace Adaptive.BlazorBasic.Parser;

/// <summary>
/// Represents a code item that is a user-defined value that references a procedure, function, or variable.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
internal class UserReferenceRecord : DisposableObjectBase
{
    #region Constructor / Dispose Methods    
    /// <summary>
    /// Initializes a new instance of the <see cref="UserReferenceRecord"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public UserReferenceRecord()
    {

    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UserReferenceRecord"/> class.
    /// </summary>
    /// <param name="lineNumber">The line number.</param>
    /// <param name="name">The name.</param>
    /// <param name="normalizedName">Name of the normalized.</param>
    /// <param name="codeLine">The code line.</param>
    public UserReferenceRecord(int lineNumber, string? name, string? normalizedName, ITokenizedCodeLine? codeLine)
    {
        LineNumber = lineNumber;
        Name = name;
        NormalizedName = normalizedName;
        CodeLine = codeLine;
    }

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        Name = null;
        NormalizedName = null;
        CodeLine = null;

        base.Dispose(disposing);
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets or sets the reference to the tokenized code line that contains the reference.
    /// </summary>
    /// <value>
    /// The parent <see cref="ITokenizedCodeLine"/> instance, or <b>null</b>.
    /// </value>
    public ITokenizedCodeLine? CodeLine { get; set; }
    /// <summary>
    /// Gets or sets the line number.
    /// </summary>
    /// <value>
    /// An integer specifying the line number.
    /// </value>
    public int LineNumber { get; set; }
    /// <summary>
    /// Gets or sets the name value.
    /// </summary>
    /// <value>
    /// A string containing the name of the item being referenced.
    /// </value>
    public string? Name { get; set; }
    /// <summary>
    /// Gets or sets the name value normalized as a key value.
    /// </summary>
    /// <value>
    /// A string containing the name of the item being referenced, normalized for usage as a key value.
    /// </value>
    public string? NormalizedName { get; set; }
    #endregion
}
