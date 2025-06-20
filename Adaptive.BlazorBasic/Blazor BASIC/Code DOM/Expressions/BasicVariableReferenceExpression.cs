using Adaptive.BlazorBasic.LanguageService.CodeDom;
using Adaptive.Intelligence.Shared;

namespace Adaptive.BlazorBasic.CodeDom;

/// <summary>
/// Represents a variable referenced by name in BASIC.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="ILanguageCodeExpression" />
public class BasicVariableReferenceExpression : DisposableObjectBase, ILanguageCodeExpression
{
    #region Private Member Declarations
    /// <summary>
    /// The variable name.
    /// </summary>
    private string? _variableName;
    #endregion

    #region Constructor / Dispose Methods    
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicVariableReferenceExpression"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public BasicVariableReferenceExpression()
    {

    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicVariableReferenceExpression"/> class.
    /// </summary>
    /// <param name="variableName">
    /// A string containing the name of the variable.
    /// </param>
    public BasicVariableReferenceExpression(string variableName)
    {
        _variableName = variableName;
    }
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        _variableName = null;
        base.Dispose(disposing);
    }
    #endregion

    #region Public Properties    
    /// <summary>
    /// Gets the name of the variable.
    /// </summary>
    /// <value>
    /// A string containing the name of the variable.
    /// </value>
    public string? VariableName => _variableName;
    #endregion
}
