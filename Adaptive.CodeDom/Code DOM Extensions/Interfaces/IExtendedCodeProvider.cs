using System.CodeDom;
using System.CodeDom.Compiler;

namespace Adaptive.CodeDom
{
    /// <summary>
    /// Provides the signature definition for language-specific Code DOM code provider instances.
    /// </summary>
    public interface IExtendedCodeProvider : IDisposable
    {
        /// <summary>
        /// Creates the escaped identifier.
        /// </summary>
        /// <param name="value">The value to contain the escaped identifier.</param>
        /// <returns></returns>
        string CreateEscapedIdentifier(string value);
        /// <summary>
        /// Creates a valid identifier for the specified value.
        /// </summary>
        /// <param name="value">
        /// The string for which to generate a valid identifier.
        /// </param>
        /// <returns>
        /// A valid identifier for the specified value.
        /// </returns>
        string CreateValidIdentifier(string value);
        /// <summary>
        /// Generates code for the specified Code Document Object Model (CodeDOM) compilation unit and sends it to 
        /// the specified text writer, using the specified options.
        /// </summary>
        /// <param name="compileUnit">
        /// A <see cref="CodeCompileUnit"/> for which to generate code.
        /// </param>
        /// <param name="writer">
        /// The <see cref="TextWriter"/> to which the output code is sent.
        /// </param>
        /// <param name="options">
        /// A <see cref="CodeGeneratorOptions"/> that indicates the options to use for generating code.
        /// </param>
        void GenerateCodeFromCompileUnit(CodeCompileUnit compileUnit, TextWriter writer, CodeGeneratorOptions options);
        /// <summary>
        /// Generates code for the specified Code Document Object Model (CodeDOM) expression and sends it to the 
        /// specified text writer, using the specified options.
        /// </summary>
        /// <param name="expression">
        /// A <see cref="CodeExpression"/> that indicates the expression for which to generate code.
        /// </param>
        /// <param name="writer">
        /// The <see cref="TextWriter"/> to which the output code is sent.
        /// </param>
        /// <param name="options">
        /// A <see cref="CodeGeneratorOptions"/> that indicates the options to use for generating code.
        /// </param>
        void GenerateCodeFromExpression(CodeExpression expression, TextWriter writer, CodeGeneratorOptions options);
        /// <summary>
        /// Generates code for the specified Code Document Object Model (CodeDOM) member declaration and sends it 
        /// to the specified text writer, using the specified options.
        /// </summary>
        /// <param name="member">
        /// A <see cref="CodeTypeMember"/> that indicates the member for which to generate code.
        /// </param>
        /// <param name="writer">
        /// The <see cref="TextWriter"/> to which the output code is sent.
        /// </param>
        /// <param name="options">
        /// A <see cref="CodeGeneratorOptions"/> that indicates the options to use for generating code.
        /// </param>
        void GenerateCodeFromMember(CodeTypeMember member, TextWriter writer, CodeGeneratorOptions options);
        /// <summary>
        /// Generates code for the specified Code Document Object Model (CodeDOM) namespace and sends it to the specified 
        /// text writer, using the specified options.
        /// </summary>
        /// <param name="codeNamespace">
        /// A <see cref="CodeNamespace"/> that indicates the namespace for which to generate code.
        /// </param>
        /// <param name="writer">
        /// The <see cref="TextWriter"/> to which the output code is sent.
        /// </param>
        /// <param name="options">
        /// A <see cref="CodeGeneratorOptions"/> that indicates the options to use for generating code.
        /// </param>
        void GenerateCodeFromNamespace(CodeNamespace codeNamespace, TextWriter writer, CodeGeneratorOptions options);
        /// <summary>
        /// Generates the code for a begin or end region directive.
        /// </summary>
        /// <param name="directive">
        /// A <see cref="CodeRegionDirective"/> enumerated value indicating the begin or end region.
        /// </param>
        /// <param name="writer">
        /// The <see cref="TextWriter"/> instance to write the generated code to.
        /// </param>
        /// <param name="options">
        /// The <see cref="CodeGeneratorOptions"/> to use when generating the code.
        /// </param>
        void GenerateCodeFromRegionCodeDirective(CodeRegionDirective directive, TextWriter writer, CodeGeneratorOptions options);
        /// <summary>
        /// Generates code for the specified Code Document Object Model (CodeDOM) statement and sends it to the 
        /// specified text writer, using the specified options.
        /// </summary>
        /// <param name="statement">
        /// A <see cref="CodeStatement"/> containing the CodeDOM elements for which to generate code.
        /// </param>
        /// <param name="writer">
        /// The <see cref="TextWriter"/> instance to write the generated code to.
        /// </param>
        /// <param name="options">
        /// The <see cref="CodeGeneratorOptions"/> to use when generating the code.
        /// </param>
        void GenerateCodeFromStatement(CodeStatement statement, TextWriter writer, CodeGeneratorOptions options);
        /// <summary>
        /// Generates code for the specified Code Document Object Model (CodeDOM) type declaration and sends it to the 
        /// specified text writer, using the specified options.
        /// </summary>
        /// <param name="codeType">
        /// A <see cref="CodeTypeDeclaration"/> object that indicates the type for which to generate code.
        /// </param>
        /// <param name="writer">
        /// The <see cref="TextWriter"/> instance to write the generated code to.
        /// </param>
        /// <param name="options">
        /// The <see cref="CodeGeneratorOptions"/> to use when generating the code.
        /// </param>
        void GenerateCodeFromType(CodeTypeDeclaration codeType, TextWriter writer, CodeGeneratorOptions options);
        /// <summary>
        /// Generates the code for an XML comment.
        /// </summary>
        /// <param name="commentType">
        /// A string indicating the type of the comment.
        /// </param>
        /// <param name="text">
        /// A string containing the text of the comment.
        /// </param>
        /// <param name="writer">
        /// The <see cref="StringWriter"/> instance to write the generated code to.
        /// </param>
        /// <param name="options">
        /// The <see cref="CodeGeneratorOptions"/> to use when generating the code.
        /// </param>
        void GenerateCodeFromXmlComment(string commentType, string text, TextWriter writer, CodeGeneratorOptions options);
        /// <summary>
        /// Gets the type indicated by the specified <see cref="CodeTypeReference"/>.
        /// </summary>
        /// <param name="type">
        /// A <see cref="CodeTypeReference"/> that indicates the type to return.
        /// </param>
        /// <returns>
        /// A text representation of the specified type, formatted for the language in which code is generated by this code 
        /// generator.
        /// </returns>
        string GetTypeOutput(CodeTypeReference type);
        /// <summary>
        /// Returns a value that indicates whether the specified value is a valid identifier for the current language.
        /// </summary>
        /// <param name="value">
        /// The value to verify as a valid identifier.
        /// </param>
        /// <returns>
        ///   true if the value parameter is a valid identifier; otherwise, false.
        /// </returns>
        bool IsValidIdentifier(string value);
        /// <summary>
        /// Removes the trailing text after the code ends.
        /// </summary>
        /// <param name="code">
        /// A string containing the rendered class.
        /// </param>
        /// <returns>
        /// A string containing the modified text.
        /// </returns>
        string RemoveEndClassBlockMarker(string code);
        /// <summary>
        /// Returns a value indicating whether the specified code generation support is provided.
        /// </summary>
        /// <param name="generatorSupport">
        /// A <see cref="GeneratorSupport"/> object that indicates the type of code generation support to verify.
        /// </param>
        /// <returns>
        /// <b>true</b> if the specified code generation support is provided; otherwise, <b>false</b>.
        /// </returns>
        bool Supports(GeneratorSupport generatorSupport);
    }
}