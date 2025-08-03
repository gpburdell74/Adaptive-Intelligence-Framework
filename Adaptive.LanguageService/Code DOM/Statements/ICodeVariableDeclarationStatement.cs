using Adaptive.Intelligence.LanguageService.CodeDom.Expressions;

namespace Adaptive.Intelligence.LanguageService.CodeDom.Statements;

/// <summary>
/// Provides the signature definition for a code statement that declares and instantiates a variable.
/// </summary>
/// <seealso cref="ICodeStatement" />

public interface ICodeVariableDeclarationStatement : ICodeStatement
{
    /// <summary>
    /// Gets the reference to the expression defining type of the data.
    /// </summary>
    /// <value>
    /// A <see cref="ICodeDataTypeExpression"/> instance representing the data type of the variable.
    /// </value>
    ICodeDataTypeExpression? DataType { get; }

    /// <summary>
    /// Gets the reference to the expression providing the variable name.
    /// </summary>
    /// <value>
    /// A <see cref="ICodeVariableNameExpression"/> defining the variable name.
    /// </value>
    ICodeVariableNameExpression? VariableReference { get; }
}
