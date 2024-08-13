using Adaptive.Intelligence;
using Adaptive.Intelligence.Shared;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Text;

namespace Adaptive.CodeDom.Model
{
	/// <summary>
	/// Provides methods / functions for generating code from Adaptive.CodeDom.Model classes.	
	/// </summary>
	/// <seealso cref="Adaptive.Intelligence.Shared.ExceptionTrackingBase" />
	public sealed class ClassModelGenerator : ExceptionTrackingBase
	{
		#region Private Member Declarations		
		/// <summary>
		/// The indent level
		/// </summary>
		private int _indentLevel;
		#endregion

		#region Constructor / Dispose Methods		
		/// <summary>
		/// Initializes a new instance of the <see cref="ClassModelGenerator"/> class.
		/// </summary>
		/// <remarks>
		/// This is the default constructor.
		/// </remarks>
		public ClassModelGenerator()
		{
		}
		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
		/// <b>false</b> to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}
		#endregion

		#region Public Properties		
		/// <summary>
		/// Gets or sets the indention level.
		/// </summary>
		/// <value>
		/// An integer specifying the number of tab characters to render at the start of a code line.
		/// </value>
		public int IndentionLevel
		{
			get => _indentLevel;
			set
			{
				_indentLevel = value;
				if (value < 0)
					_indentLevel = 0;
			}
		}
		#endregion

		#region Public Methods / Functions		
		/// <summary>
		/// Renders the list of namespace imports/usings.
		/// </summary>
		/// <param name="usingsList">
		/// A <see cref="List{T}"/> of <see cref="string"/> containing the namespace names.
		/// <param name="language">
		/// A <see cref="NetLanguage"/> enumerated value indicating the language to render the code in.
		/// </param>
		/// <param name="order">
		/// An optional parameter indicating whether or how to sort the list of usings.
		/// </param>
		/// <returns>
		/// A string containing the rendered code.	
		/// </returns>
		public string RenderNamespaceImports(List<string>? usingsList, NetLanguage language, SortOrder order= SortOrder.NoSort)
		{
			string renderedCode = string.Empty;

			if (usingsList != null && usingsList.Count > 0)
			{
				// Sort the usings, if specified.
				if (order != SortOrder.NoSort)
				{
					usingsList.Sort();
					if (order == SortOrder.Descending)
						usingsList.Reverse();
				}

				// Create the Code DOM namespace container instance.
				CodeNamespace tempNamespace = new CodeNamespace();

				// Create the string writer and code generation options.
				StringWriter writer = new StringWriter();
				CodeGeneratorOptions opts = CreateOptions();

				// Create and append a namespace import definition to the namespace container.
				foreach (string nameSpace in usingsList)
				{
					tempNamespace.Imports.Add(new CodeNamespaceImport(nameSpace));
				}

				// Render the code based on the language.
				IExtendedCodeProvider provider = ExtendedCodeProviderFactory.CreateProvider(language);
				provider.GenerateCodeFromNamespace(tempNamespace, writer, opts);
				provider.Dispose();

				// Return the code string.
				writer.Flush();
				renderedCode = writer.ToString();
				writer.Dispose();
			}

			return renderedCode;
		}
		/// <summary>
		/// Renders the name space block start text.
		/// </summary>
		/// <param name="language">
		/// A <see cref="NetLanguage"/> enumerated value indicating the language to render the code in.
		/// </param>
		/// <returns>
		/// A string containing the rendered code.	
		/// </returns>
		public string RenderNameSpaceStart(NetLanguage language)
		{
			StringWriter writer = new StringWriter();
			CodeGeneratorOptions opts = CreateOptions();

			// Generate the code.
			IExtendedCodeProvider provider = ExtendedCodeProviderFactory.CreateProvider(language);
			provider.GenerateCodeFromExpression(new CodeNamespaceBlockStart(), writer, opts);
			
			writer.Flush();
			string code = writer.ToString();
			writer.Dispose();
			
			return code;
		}
		/// <summary>
		/// Renders the name space block end text.
		/// </summary>
		/// <param name="language">
		/// A <see cref="NetLanguage"/> enumerated value indicating the language to render the code in.
		/// </param>
		/// <returns>
		/// A string containing the rendered code.	
		/// </returns>
		public string RenderNameSpaceEnd(NetLanguage language)
		{
			StringWriter writer = new StringWriter();
			CodeGeneratorOptions opts = CreateOptions();

			// Generate the code.
			IExtendedCodeProvider provider = ExtendedCodeProviderFactory.CreateProvider(language);
			provider.GenerateCodeFromExpression(new CodeNamespaceBlockEnd(), writer, opts);

			writer.Flush();
			string code = writer.ToString();
			writer.Dispose();

			return code;
		}
		/// <summary>
		/// Renders the name of the name space.
		/// </summary>
		/// <param name="namespaceName">A string containing the name of the namespace.
		/// </param>
		/// <param name="language">
		/// A <see cref="NetLanguage"/> enumerated value indicating the language to use.
		/// </param>
		/// <returns>
		/// A string containing the rendered code.
		/// </returns>
		public string RenderNameSpaceName(string namespaceName, NetLanguage language)
		{
			// Create the Code DOM container instance.
			CodeNamespace tempNamespace = new CodeNamespace(namespaceName);

			StringWriter writer = new StringWriter();
			CodeGeneratorOptions opts = CreateOptions();

			IExtendedCodeProvider provider = ExtendedCodeProviderFactory.CreateProvider(language);
			provider.GenerateCodeFromNamespace(tempNamespace, writer, opts);
			writer.Flush();

			string code = writer.ToString();
			writer.Dispose();

			// Remove the last CR/LF pair.
			int index = code.IndexOf(Constants.CarriageReturn, 2);
			return code.Substring(0, index).Trim();
		}
		/// <summary>
		/// Renders the class start code.
		/// </summary>
		/// <param name="templateClass">
		/// The <see cref="ClassModel"/> containing the class model and contents.
		/// </param>
		/// <param name="language">
		/// A <see cref="NetLanguage"/> enumerated value indicating the language to use.
		/// </param>
		/// <returns>
		/// A string containing the rendered code.
		/// </returns>
		public string RenderClassStart(ClassModel templateClass, NetLanguage language)
		{
			StringWriter writer = new StringWriter();
			CodeGeneratorOptions opts = CreateOptions();
			IExtendedCodeProvider provider = ExtendedCodeProviderFactory.CreateProvider(language);


			// Generate the comments first.
			if (templateClass.Summary != null)
				provider.GenerateCodeFromXmlComment(XmlCommentConstants.TagSummary, templateClass.Summary, writer, opts);

			if (!string.IsNullOrEmpty(templateClass.Remarks))
				provider.GenerateCodeFromXmlComment(XmlCommentConstants.TagRemarks, templateClass.Remarks, writer, opts);

			// Create the Code DOM class object.
			CodeTypeDeclaration declaration = new CodeTypeDeclaration();
			declaration.Name = templateClass.ClassName;
			declaration.IsClass = true;
			declaration.IsPartial = false;

			// Set the base class inheritance, if specified.
			if (templateClass.BaseClass != null && !templateClass.BaseClass.IsPOCO)
				declaration.BaseTypes.Add(CodeDomObjectFactory.CreateBaseClassDefinition(templateClass.BaseClass));

			// Set the interface inheritance(s), if specified.
			if (templateClass.Interfaces != null && templateClass.Interfaces.Count > 0)
			{
				foreach (string interfaceName in templateClass.Interfaces)
				{
					declaration.BaseTypes.Add(new CodeTypeReference(interfaceName));
				}
			}

			// Add the attributes / access modifiers for the class.
			TypeAttributes attr = TypeAttributes.Class;

			// Abstract, Inheritable, or Sealed (Final)...
			if (templateClass.IsAbstract)
				attr |= TypeAttributes.Abstract;

			else if (templateClass.IsSealed)
				attr |= TypeAttributes.Sealed;

			// Public or Not Public...
			if (templateClass.IsPublic)
				attr |= TypeAttributes.Public;

			else if (templateClass.IsProtected)
				attr |= TypeAttributes.NestedFamANDAssem;

			declaration.TypeAttributes = attr;

			// Generate the class.
			provider.GenerateCodeFromType(declaration, writer, opts);
			writer.Flush();
			string code = writer.ToString();

			// Remove the last block end declaration from the code.
			code = provider.RemoveEndClassBlockMarker(code);
			if (language == NetLanguage.CSharp)
			{
				int index = code.LastIndexOf("}");
				code = code.Substring(0, index - 1).Trim();
			}
			else
			{
				int index = code.ToLower().LastIndexOf("end class");
				code = code.Substring(0, index - 1).Trim();
			}
			code = IndentLines(code);

			writer.Dispose();
			return code;
		}
		/// <summary>
		/// Renders the code for starting a class definition.
		/// </summary>
		/// <param name="className">
		/// A string containing the name of the class.</param>
		/// <param name="inheritsFrom">
		/// A string containing the name of the data type the class inherits from, or <b>null</b>.
		/// </param>
		/// <param name="interfaceName">
		/// A string containing the name of the interface the class implements, or <b>null</b>.
		/// </param>
		/// <param name="xmlSummary">
		/// A string containing the XML Summary comment for the class, or <b>null</b>.
		/// </param>
		/// <param name="xmlRemarks">
		/// A string containing the XML remarks comment for the class, or <b>null</b>.
		/// </param>
		/// <param name="isAbstract">
		/// A boolean value indicating whether the class is an abstract class.
		/// </param>
		/// <param name="isSealed">
		/// A boolean value indicating whether the class is a sealed or final class.
		/// </param>
		/// <param name="isPublic">
		/// A boolean value indicating whether the access modifier for the class is public.
		/// </param>
		/// <param name="language">
		/// A <see cref="NetLanguage"/> enumerated value indicating the language to render the class into.
		/// </param>
		/// <returns>
		/// A string containing the code snippet used to start a class definition.
		/// </returns>
		public string RenderClassStart(string className, string? inheritsFrom, string? interfaceName,
			string? xmlSummary, string? xmlRemarks, bool isAbstract, bool isSealed, bool isPublic, NetLanguage language)
		{
			StringWriter writer = new StringWriter();
			CodeGeneratorOptions opts = CreateOptions();
			IExtendedCodeProvider provider = ExtendedCodeProviderFactory.CreateProvider(language);

			// Generate the comments first.
			if (xmlSummary != null)
				provider.GenerateCodeFromXmlComment("summary", xmlSummary, writer, opts);

			if (xmlRemarks != null)
				provider.GenerateCodeFromXmlComment("remarks", xmlRemarks, writer, opts);

			// Create the CodeDOM class object.
			CodeTypeDeclaration declaration = new CodeTypeDeclaration();
			declaration.Name = className;

			// Add the base class and interface, if specified.
			if (inheritsFrom != null)
				declaration.BaseTypes.Add(new CodeTypeReference(inheritsFrom));
			if (interfaceName != null)
				declaration.BaseTypes.Add(new CodeTypeReference(interfaceName));

			// Yes, this is a class and we are not doing partial class files.
			declaration.IsClass = true;
			declaration.IsPartial = false;

			// Set the access modifier(s) and attributes.
			TypeAttributes attr = TypeAttributes.Class;
			if (isAbstract)
				attr |= TypeAttributes.Abstract;
			else if (isSealed)
				attr |= TypeAttributes.Sealed;

			if (isPublic)
				attr |= TypeAttributes.Public;
			else
				attr |= TypeAttributes.NotPublic;

			declaration.TypeAttributes = attr;

			// Generate the empty class definition.
			provider.GenerateCodeFromType(declaration, writer, opts);
			writer.Flush();
			string code = writer.ToString();

			// Indent the code lines correctly.
			code = IndentLines(code);

			writer.Dispose();
			provider.Dispose();
			return code;
		}
		#endregion

		#region Private Methods / Functions		
		/// <summary>
		/// Creates the options instance used when generating code.
		/// </summary>
		/// <returns>
		/// A <see cref="CodeGeneratorOptions"/> instance.
		/// </returns>
		private CodeGeneratorOptions CreateOptions()
		{
			CodeGeneratorOptions opts = new CodeGeneratorOptions();
			opts.VerbatimOrder = true;
			opts.BlankLinesBetweenMembers = false;
			opts.BracingStyle = "C";

			if (_indentLevel > 0)
				opts.IndentString = new string('\t', _indentLevel);

			return opts;
		}
		/// <summary>
		/// Indents the lines in the specified string.
		/// </summary>
		/// <param name="original">
		/// The original string containing the code to be modified.
		/// </param>
		/// <returns>
		/// A string containing the indented code.
		/// </returns>
		private string IndentLines(string original)
		{
			// Parse the original into an array of lines.
			string[] lines = original.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

			// Pre-pend the current indent level characters to each line.
			StringBuilder builder = new StringBuilder();
			foreach (string line in lines)
			{
				builder.AppendLine(new string('\t', _indentLevel) + line);
			}
			return builder.ToString();
		}
		/// <summary>
		/// Renders a region-based section of code.
		/// </summary>
		/// <param name="sectionType">
		/// A <see cref="CodeSectionType"/> enumerated value indicating the region/section of code being generated.
		/// </param>
		/// <param name="section">
		/// The <see cref="CodeSectionModel"/> instance whose contents are to be rendered.
		/// </param>
		/// <param name="language">
		/// A <see cref="NetLanguage"/> enumerated value indicating the language to render the code into.
		/// </param>
		/// <returns>
		/// A string containing the rendered code snippet.
		/// </returns>
		public string RenderSection(CodeSectionType sectionType, CodeSectionModel section, NetLanguage language)
		{
			StringWriter writer = new StringWriter();
			CodeGeneratorOptions opts = CreateOptions();
			IExtendedCodeProvider provider = ExtendedCodeProviderFactory.CreateProvider(language);

			// Create the region start and end CodeDOM objects.
			CodeRegionDirective startRegion = new CodeRegionDirective(CodeRegionMode.Start, GetSectionTypeName(sectionType));
			CodeRegionDirective endRegion = new CodeRegionDirective(CodeRegionMode.End, string.Empty);

			// WRite the region start.
			provider.GenerateCodeFromRegionCodeDirective(startRegion, writer, opts);

			// Render section content.
			if (section.Parts != null)
			{
				foreach (CodePartModel part in section.Parts)
				{
					// XML Summary comment.
					if (part.Summary != null)
						provider.GenerateCodeFromXmlComment("summary", part.Summary, writer, opts);

					// XML Remarks comment.
					if (part.Remarks != null)
						provider.GenerateCodeFromXmlComment("remarks", part.Remarks, writer, opts);

					// XML Returns comment.
					if (part.Returns != null)
						provider.GenerateCodeFromXmlComment("returns", part.Returns, writer, opts);

					// XML Value comment.
					if (part.Value != null)
						provider.GenerateCodeFromXmlComment("value", part.Value, writer, opts);

					// XML Parameter comment(s).
					foreach (NameValuePair<string> item in part.ParameterComments)
					{
						provider.GenerateCodeFromXmlComment("param", item.Value, writer, opts);
					}

					// Code Expression
					if (part.Content is CodeExpression expression)
						provider.GenerateCodeFromExpression(expression, writer, opts);

					// Code Statement
					else if (part.Content is CodeStatement statement)
						provider.GenerateCodeFromStatement(statement, writer, opts);

					// Class, Struct, or other type definition.
					else if (part.Content is CodeTypeMember member)
					{
						// TODO: Refactor
						if (part.Content is CodeConstructor constItem)
						{
							CodeTypeDeclaration tempClass = new CodeTypeDeclaration(section.ClassName);
							tempClass.Members.Add(constItem);
							StringWriter x = new StringWriter();
							provider.GenerateCodeFromType(tempClass, x, opts);
							string z = x.ToString().Replace("\r", " ").Replace("\n", " ").Trim();
							int index = z.IndexOf("{");
							string y = z.Substring(index + 1).Trim();
							index = y.IndexOf("{");
							y = y.Substring(0, index - 1).Trim().Replace("\t", "");
							y += "\r\n{\r\n}";
							writer.WriteLine(y);
						}
						else
						{
							provider.GenerateCodeFromMember(member, writer, opts);
						}
					}
					else
					{
						string dd = part.Content.GetType().Name;
					}
				}

			}

			// Write the region end.
			provider.GenerateCodeFromRegionCodeDirective(endRegion, writer, opts);
			return IndentLines(writer.ToString());
		}
		/// <summary>
		/// Renders a region-based section of code.
		/// </summary>
		/// <param name="sectionList">
		/// A <see cref="CodeSectionModelCollection"/> containing a list of code sections.
		/// </param>
		/// <param name="sectionType">
		/// A <see cref="CodeSectionType"/> enumerated value indicating the region/section of code being generated.
		/// </param>
		/// <param name="language">
		/// A <see cref="NetLanguage"/> enumerated value indicating the language to render the code into.
		/// </param>
		/// <returns>
		/// A string containing the rendered code snippet.
		/// </returns>
		public string RenderSection(CodeSectionModelCollection sectionList, CodeSectionType sectionType,NetLanguage language)
		{
			string code = string.Empty;

			CodeSectionModel? section = sectionList.GetSectionByType(sectionType);
			if (section != null)
				code = RenderSection(sectionType, section, language);

			return code;
		}
		/// <summary>
		/// Renders the code section/regions for the specified class definition.
		/// </summary>
		/// <param name="classModel">
		/// The <see cref="ClassModel"/> instance to be rendered.
		/// </param>
		/// <param name="language">
		/// A <see cref="NetLanguage"/> enumerated value indicating the language to render the code into.
		/// </param>
		/// <returns>
		/// A string containing the rendered code snippet.
		/// </returns>
		public string RenderClassSections(ClassModel classModel, NetLanguage language)
		{
			StringBuilder builder = new StringBuilder();
			CodeSectionModelCollection? sectionList = classModel.CodeSections;

			// Public Events
			builder.AppendLine(RenderSection(sectionList, CodeSectionType.PublicEvents, language));

			// Private Constants
			builder.AppendLine(RenderSection(sectionList, CodeSectionType.PrivateConstants, language));

			// Private Members
			builder.AppendLine(RenderSection(sectionList, CodeSectionType.PrivateMembers, language));

			// Constructor / Dispose Methods
			builder.AppendLine(RenderSection(sectionList, CodeSectionType.ConstructorDispose, language));

			// Public Properties
			builder.AppendLine(RenderSection(sectionList, CodeSectionType.PublicProperties, language));

			// Protected Properties
			builder.AppendLine(RenderSection(sectionList, CodeSectionType.ProtectedProperties, language));

			// Abstract Methods
			builder.AppendLine(RenderSection(sectionList, CodeSectionType.AbstractMethods, language));

			// Protected Methods
			builder.AppendLine(RenderSection(sectionList, CodeSectionType.ProtectedMethods, language));

			// Public Methods / Functions
			builder.AppendLine(RenderSection(sectionList, CodeSectionType.PublicMethods, language));

			// Private Methods / Functions
			builder.AppendLine(RenderSection(sectionList, CodeSectionType.PrivateMethods, language));

			// Private Event Methods
			builder.AppendLine(RenderSection(sectionList, CodeSectionType.EventMethods, language));

			// Private Event Handlers
			builder.AppendLine(RenderSection(sectionList, CodeSectionType.EventHandlers, language));

			return builder.ToString();
		}
		/// <summary>
		/// Renders the code to mark the end of a class definition.
		/// </summary>
		/// <param name="language">
		/// A <see cref="NetLanguage"/> enumerated value indicating the language to render the code into.
		/// </param>
		/// <returns>
		/// A string containing the code snippet.
		/// </returns>
		public string RenderClassEnd(NetLanguage language)
		{
			string line = string.Empty;
			if (_indentLevel			> 0)
				line = new string('\t', _indentLevel);

			if (language == NetLanguage.CSharp)
			{
				line += "}\r\n";
			}
			else
			{
				line += "End Class\r\n";
			}
			return line;
		}
		/// <summary>
		/// Renders the class definition.
		/// </summary>
		/// <param name="classModel">
		/// The <see cref="ClassModel"/> instance representing the class model to be rendered.
		/// </param>
		/// <param name="language">
		/// A <see cref="NetLanguage"/> enumerated value indicating the language to render the code into.
		/// </param>
		/// <returns>
		/// A string containing the rendered code.
		/// </returns>
		public string RenderClass(ClassModel classModel, NetLanguage language)
		{
			StringBuilder builder = new StringBuilder();

			// Render the usings / Imports list.
			builder.AppendLine(RenderNamespaceImports(classModel.Imports, language));

			// Render the name space start block.
			builder.AppendLine(RenderNameSpaceName(classModel.Namespace, language));
			builder.AppendLine(RenderNameSpaceStart(language));

			// Render the class start block.
			_indentLevel++;
			builder.AppendLine(RenderClassStart(classModel, language));

			// Render each of the class code sections.
			_indentLevel++;
			builder.AppendLine(RenderClassSections(classModel, language));

			// Render the class end block.
			_indentLevel--;
			builder.AppendLine(RenderClassEnd(language));

			// Render the namespace end block.
			_indentLevel--;
			builder.AppendLine(RenderNameSpaceEnd(language));

			return builder.ToString();
		}
		/// <summary>
		/// Gets the name of the section type.
		/// </summary>
		/// <param name="sectionType">
		/// A <see cref="CodeSectionType"/> enumerated value indicating the type of the section.
		/// </param>
		/// <returns>
		/// A string containing the name of the section.
		/// </returns>
		public static string GetSectionTypeName(CodeSectionType sectionType)
		{
			string regionName = string.Empty;

			switch (sectionType)
			{
				case CodeSectionType.PublicEvents:
					regionName = "Public Events";
					break;

				case CodeSectionType.PrivateConstants:
					regionName = "Private Constants";
					break;

				case CodeSectionType.PrivateMembers:
					regionName = "Private Member Declarations";
					break;

				case CodeSectionType.ConstructorDispose:
					regionName = "Constructor / Dispose Methods";
					break;

				case CodeSectionType.PublicProperties:
					regionName = "Public Properties";
					break;

				case CodeSectionType.ProtectedProperties:
					regionName = "Protected Properties";
					break;

				case CodeSectionType.AbstractMethods:
					regionName = "Abstract Methods / Functions";
					break;

				case CodeSectionType.ProtectedMethods:
					regionName = "Protected Methods / Functions";
					break;

				case CodeSectionType.PublicMethods:
					regionName = "Public Methods / Functions";
					break;

				case CodeSectionType.PrivateMethods:
					regionName = "Private Methods / Functions";
					break;

				case CodeSectionType.EventHandlers:
					regionName = "Event Handlers";
					break;

				case CodeSectionType.EventMethods:
					regionName = "Event Methods";
					break;

				default:
					break;

			}
			return regionName;
		}
		#endregion
	}
}
