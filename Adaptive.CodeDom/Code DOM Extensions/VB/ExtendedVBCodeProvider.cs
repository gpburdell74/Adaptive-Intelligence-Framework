using Microsoft.VisualBasic;
using System.CodeDom;
using System.CodeDom.Compiler;

namespace Adaptive.CodeDom
{
	/// <summary>
	/// Provides custom extensions and additions for the Visual Basic code provider / generator.
	/// </summary>
	/// <seealso cref="VBCodeProvider" />
	public sealed class ExtendedVBCodeProvider : VBCodeProvider, IExtendedCodeProvider
	{
		#region Public Method Overrides		
		/// <summary>
		/// Generates code for the specified Code Document Object Model (CodeDOM) statement and sends it to the specified text writer, using the specified options.
		/// </summary>
		/// <param name="statement">A <see cref="T:System.CodeDom.CodeStatement" /> containing the CodeDOM elements for which to generate code.</param>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to which output code is sent.</param>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CodeGeneratorOptions" /> that indicates the options to use for generating code.</param>
		public override void GenerateCodeFromStatement(CodeStatement statement, TextWriter writer, CodeGeneratorOptions options)
		{
			if (statement is CodeXmlSummaryComment summaryComment)
			{
				GenerateCodeFromXmlComment(XmlCommentConstants.TagSummary, summaryComment.Text, writer, options);
			}
			else if (statement is CodeXmlRemarksComment remarksComment)
			{
				GenerateCodeFromXmlComment(XmlCommentConstants.TagRemarks, remarksComment.Text, writer, options);
			}
			else
			{
				base.GenerateCodeFromStatement(statement, writer, options);
			}
		}
		/// <summary>
		/// Generates code for the specified Code Document Object Model (CodeDOM) expression and sends it to the specified text writer, using the specified options.
		/// </summary>
		/// <param name="expression">A <see cref="CodeExpression" /> object that indicates the expression for which to generate code.</param>
		/// <param name="writer">The <see cref="TextWriter" /> to which output code is sent.</param>
		/// <param name="options">A <see cref="CodeGeneratorOptions" /> that indicates the options to use for generating code.</param>
		public override void GenerateCodeFromExpression(CodeExpression expression, TextWriter writer, CodeGeneratorOptions options)
		{
			// If writing block starts and ends, use the extended code; otherwise, generate as normal.
			if (expression is CodeNamespaceBlockEnd)
			{
				writer.WriteLine(VbConstants.VbEndNamespace);
			}
			else if (expression is CodeNamespaceBlockStart)
			{
				writer.WriteLine(string.Empty);
			}
			else
			{
				base.GenerateCodeFromExpression(expression, writer, options);
			}
		}
		#endregion

		#region Public Methods / Functions		
		/// <summary>
		/// Generates the code for an XML comment.
		/// </summary>
		/// <param name="commentType">A string indicating the type of the comment.</param>
		/// <param name="text">A string containing the text of the comment.</param>
		/// <param name="writer">The <see cref="StringWriter" /> instance to write the generated code to.</param>
		/// <param name="options">The <see cref="CodeGeneratorOptions" /> to use when generating the code.</param>
		public void GenerateCodeFromXmlComment(string commentType, string text, TextWriter writer, CodeGeneratorOptions options)
		{
			// Create the Code DOM objects.
			CodeCommentStatement startStatement = new CodeCommentStatement(Intelligence.Shared.Constants.XmlBracketOpen + commentType + Intelligence.Shared.Constants.XmlBracketEnd, true);
			CodeCommentStatement midStatement = new CodeCommentStatement(text, true);
			CodeCommentStatement endStatement = new CodeCommentStatement(Intelligence.Shared.Constants.XmlBracketEndTag + commentType + Intelligence.Shared.Constants.XmlBracketEnd, true);

			// Generate the code.
			base.GenerateCodeFromStatement(startStatement, writer, options);
			base.GenerateCodeFromStatement(midStatement, writer, options);
			base.GenerateCodeFromStatement(endStatement, writer, options);
		}
		/// <summary>
		/// Generates the code for a begin or end region directive.
		/// </summary>
		/// <remarks>
		/// This method is not used to generate the block, but just a single or beginning line that contains
		/// the directive.
		/// </remarks>
		/// <param name="directive">A <see cref="CodeRegionDirective" /> enumerated value indicating the begin or end region.</param>
		/// <param name="writer">The <see cref="TextWriter" /> instance to write the generated code to.</param>
		/// <param name="options">The <see cref="CodeGeneratorOptions" /> to use when generating the code.</param>
		public void GenerateCodeFromRegionCodeDirective(CodeRegionDirective directive, TextWriter writer, CodeGeneratorOptions options)
		{
			// Create the temporary CodeDOM container objects.
			CodeNamespace ns = new CodeNamespace();
			CodeTypeDeclaration cs = new CodeTypeDeclaration();
			ns.Types.Add(cs);
			cs.StartDirectives.Add(directive);

			// Generate the standard region directive code.
			StringWriter innerWriter = new StringWriter();
			base.GenerateCodeFromNamespace(ns, innerWriter, options);
			string rawData = innerWriter.ToString();
			innerWriter.Dispose();

			// Remove preceding and trailing characters from the generated code.
			int leftIndex = -1;
			string singleLine = string.Empty;

			// Look for the "#region" or "# End Region" text so as to discard anything preceding it.
			if (directive.RegionMode == CodeRegionMode.Start)
				leftIndex = rawData.IndexOf(VbConstants.RegionStart, StringComparison.OrdinalIgnoreCase);
			else
				leftIndex = rawData.IndexOf(VbConstants.RegionEnd, StringComparison.OrdinalIgnoreCase);

			// Remove any trailing characters.
			int rightIndex = rawData.IndexOf(Intelligence.Shared.Constants.CarriageReturn, leftIndex);
			rightIndex--;

			singleLine = rawData.Substring(leftIndex, rightIndex - leftIndex);
			writer.WriteLine(singleLine);
		}
		/// <summary>
		/// Removes the trailing text after the code ends.
		/// </summary>
		/// <param name="code">A string containing the rendered class.</param>
		/// <returns>
		/// A string containing the modified text.
		/// </returns>
		public string RemoveEndClassBlockMarker(string code)
		{
			int index = code.LastIndexOf(CsConstants.CsBlockEnd);
			code = code.Substring(0, index - 1).Trim();
			return code;
		} 
		#endregion
	}
}
