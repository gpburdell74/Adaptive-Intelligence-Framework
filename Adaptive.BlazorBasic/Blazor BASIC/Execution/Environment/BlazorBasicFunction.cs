using Adaptive.Intelligence.LanguageService.CodeDom.Statements;
using Adaptive.Intelligence.LanguageService.Execution;
using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.BlazorBasic.Execution;

/// <summary>
/// Represents a Blazor BASIC function definition and instance.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="BasicProcedure"/>
/// <seealso cref="IFunction" />
public sealed class BasicFunction : BasicProcedure, IFunction
{
    #region Private Member Declarations
    /// <summary>
    /// The return type.
    /// </summary>
    private Type? _returnType;
    #endregion

    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicFunction"/> class.
    /// </summary>
    /// <param name="parent">
    /// The reference to the <see cref="IScopeContainer"/> instance acting as the parent
    /// scope container.
    /// </param>
    /// <param name="returnType">
    /// The <see cref="Type"/> of the function's return type.
    /// </param>
    public BasicFunction(IScopeContainer parent, Type returnType) : base(parent)
    {
        _returnType = returnType;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BasicFunction"/> class.
    /// </summary>
    /// <param name="code">
    /// A <see cref="List{T}"/> of <see cref="ICodeStatement"/> instances containing the 
    /// code for the procedure definition.
    /// The code.</param>
    /// <param name="parent">
    /// The reference to the <see cref="IScopeContainer"/> instance acting as the parent
    /// scope container.
    /// </param>
    /// <param name="returnType">
    /// The <see cref="Type"/> of the function's return type.
    /// </param>
    public BasicFunction(List<ICodeStatement> code, IScopeContainer parent, Type returnType) : base(code, parent)
    {
        _returnType = returnType;
    }

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        _returnType = null;
        base.Dispose(disposing);
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets the data type of the return value for the function instance.
    /// </summary>
    /// <value>
    /// The data <see cref="Type" /> of the expected return value.
    /// </value>
    public Type ReturnType => _returnType;
    #endregion

    #region Public Methods / Functions
    /// <summary>
    /// Executes the code element within the context of its parent scope.
    /// </summary>
    /// <param name="lineNumber">An integer specifying the current line number value.</param>
    /// <param name="engine">The current <see cref="T:Adaptive.Intelligence.LanguageService.Execution.IExecutionEngine" /> instance.</param>
    /// <param name="environment">The current <see cref="T:Adaptive.Intelligence.LanguageService.Execution.IExecutionEnvironment" /> instance.</param>
    /// <returns></returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public new object Execute(int lineNumber, IExecutionEngine engine, IExecutionEnvironment environment)
    {
        return null;
    }
    /// <summary>
    /// Returns the string representation of the current instance.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
        return "FUNCTION " + Name + "() AS " + _returnType?.Name;
    }
    #endregion
}