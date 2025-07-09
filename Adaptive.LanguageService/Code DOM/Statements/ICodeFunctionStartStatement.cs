using Adaptive.Intelligence.LanguageService.CodeDom.Expressions;

namespace Adaptive.Intelligence.LanguageService.CodeDom.Statements;

/// <summary>
/// Provides the signature definition for code statements that mark and define the start
/// of a procedure definition.
/// </summary>
/// <seealso cref="ICodeProcedureStartStatement"/>
/// <seealso cref="ICodeStatement" />
public interface ICodeFunctionStartStatement : ICodeProcedureStartStatement
{
    #region Public Properties
    /// <summary>
    /// Gets the data type of the return value of the function.
    /// </summary>
    /// <value>
    /// An <see cref="ICodeDataTypeExpression"/> whose evaluation returns the data type 
    /// of the value returned by executing the function.
    /// </value>
    ICodeDataTypeExpression? ReturnType { get; }
    #endregion

}