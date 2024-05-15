using Microsoft.CSharp;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Text;

namespace Adaptive.Intelligence.Shared.Code
{
    /// <summary>
    /// Provides static methods / functions for generating C# Code.
    /// </summary>
    public static class CodeDomFactory
    {
        /// <summary>
        /// Creates the XML value pre-defined text for boolean values.
        /// </summary>
        /// <param name="trueText">A string containing the description of the true condition.</param>
        /// <returns>
        /// An array of <see cref="CodeCommentStatement"/> instances used to define the XML value
        /// comment section.
        /// </returns>
        public static CodeCommentStatement[] CreateBooleanValueText(string trueText)
        {
            return new CodeCommentStatement[]
            {
                new CodeCommentStatement(new CodeComment("<value>", true)),
                new CodeCommentStatement(new CodeComment("<b>true<b> " + trueText + "; otherwise, <b>false</b>.", true)),
                new CodeCommentStatement(new CodeComment("</value>", true))
            };
        }
        /// <summary>
        /// Creates the XML value pre-defined text for boolean values.
        /// </summary>
        /// <param name="trueText">A string containing the description of the true condition.</param>
        /// <param name="falseText">A string containing the description of the false condition.</param>
        /// <returns>
        /// An array of <see cref="CodeCommentStatement"/> instances used to define the XML value
        /// comment section.
        /// </returns>
        public static CodeCommentStatement[] CreateBooleanValueText(string trueText, string falseText)
        {
            return new CodeCommentStatement[]
            {
                new CodeCommentStatement(new CodeComment("<value>", true)),
                new CodeCommentStatement(new CodeComment("<b>true</b> " + trueText + "; <b>false</b> " + falseText, true)),
                new CodeCommentStatement(new CodeComment("</value>", true))
            };
        }

        /// <summary>
        /// Creates the CodeDOM class object definition.
        /// </summary>
        /// <param name="className">
        /// A string specifying the name of the class.</param>
        /// <param name="baseClassType">
        /// A string specifying the data type of the base class.
        /// </param>
        /// <param name="summaryText">
        /// A string containing the XML summary text.
        /// </param>
        /// <param name="remarksText">
        /// A string containing the XML remarks text.
        /// </param>
        /// <returns>
        /// A <see cref="CodeTypeDeclaration"/> instance used to generate the class.
        /// </returns>
        public static CodeTypeDeclaration CreateClass(string className, string baseClassType, string summaryText, string remarksText)
        {
            CodeTypeDeclaration newClass = new CodeTypeDeclaration(className);
            newClass.BaseTypes.Add(new CodeTypeReference(baseClassType));
            newClass.Attributes = MemberAttributes.Public | MemberAttributes.Final;

            newClass.Comments.AddRange(CreateSummary(summaryText));
            if (!string.IsNullOrEmpty(remarksText))
                newClass.Comments.AddRange(CreateRemarks(summaryText));

            return newClass;
        }
        /// <summary>
        /// Generates a CodeDOM model for a Dispose method overload.
        /// </summary>
        /// <returns>
        /// A <see cref="CodeMemberMethod"/> containing the object model for a Dispose method.
        /// </returns>
        public static CodeMemberMethod CreateDisposeMethod()
        {
            CodeMemberMethod disposeMethod = new CodeMemberMethod();
            disposeMethod.Name = "Dispose";
            disposeMethod.Attributes = MemberAttributes.Family | MemberAttributes.Override;
            disposeMethod.Parameters.Add(
                new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(bool)), "disposing"));

            //    /// <summary>
            //    /// Releases unmanaged and - optionally - managed resources.
            //    /// </summary>
            //    /// <param name="disposing">
            //    /// <b>true</b> to release both managed and unmanaged resources;
            //    /// <b>false</b> to release only unmanaged resources.
            //    /// </param>
            disposeMethod.Comments.AddRange(CreateSummary("Releases unmanaged and - optionally - managed resources."));
            disposeMethod.Comments.AddRange(CreateParameter("disposing", "<b>true</b> to release both managed and unmanaged resources; <b>false</b> to release only unmanaged resources."));

            return disposeMethod;
        }

        /// <summary>
        /// Generates the CodeDOM object model content for a dispose method.
        /// </summary>
        /// <param name="disposeMethod">
        /// The <see cref="CodeMemberMethod"/> instance defining the Dispose method.
        /// </param>
        /// <param name="propertyList">
        /// The <see cref="PropertyProfileCollection"/> instance containing the property metadata
        /// used to generate appropriate Dispose code for the properties of the parent class.
        /// </param>
        public static void CreateDisposeMethodContent(CodeMemberMethod disposeMethod, PropertyProfileCollection propertyList)
        {
            // Determine which definitions go in which blocks...
            PropertyProfileCollection clearableList = propertyList.ClearableProperties();
            PropertyProfileCollection disposableList = propertyList.DisposableProperties();
            PropertyProfileCollection nullableList = propertyList.NullableProperties();

            // Generate the code block only if needed.
            //
            // Generates a block similar to:
            // if (!IsDisposed && disposing)
            // {
            //      if (<property> != null)
            //          <property>.Clear();
            //      if (<property2> != null)
            //          <property2>.Dispose();
            // }
            //
            if (propertyList.HasClearableOrDisposableItems)
            {
                // if (!IsDisposed && disposing)
                CodeConditionStatement ifDisposingStatement = new CodeConditionStatement(
                    new CodeBinaryOperatorExpression(
                        new CodeBinaryOperatorExpression(
                            new CodeVariableReferenceExpression("IsDisposed"),
                            CodeBinaryOperatorType.IdentityInequality,
                            new CodePrimitiveExpression(null)),
                    CodeBinaryOperatorType.BooleanAnd,
                        new CodeBinaryOperatorExpression(
                            new CodeVariableReferenceExpression("disposing"),
                            CodeBinaryOperatorType.IdentityInequality,
                            new CodePrimitiveExpression(true))),
                    new CodeStatement[] { });

                foreach (PropertyProfile propertyItem in clearableList)
                {
                    CodeConditionStatement ifNotNullStatement = new CodeConditionStatement(
                        // if (<name> != null)
                        new CodeBinaryOperatorExpression(
                            new CodeVariableReferenceExpression(propertyItem.PropertyName),
                            CodeBinaryOperatorType.IdentityInequality,
                            new CodePrimitiveExpression(null)),
                        new CodeStatement[]
                        {
                            // <name>.Clear();
                            new CodeExpressionStatement(
                                new CodeMethodInvokeExpression(
                                    new CodeMethodReferenceExpression(
                                        new CodeVariableReferenceExpression(propertyItem.PropertyName), "Clear")))
                        });

                    ifDisposingStatement.TrueStatements.Add(ifDisposingStatement);
                }
                foreach (PropertyProfile propertyItem in disposableList)
                {
                    CodeConditionStatement ifNotNullStatement = new CodeConditionStatement(
                        // if (<name> != null)
                        new CodeBinaryOperatorExpression(
                            new CodeVariableReferenceExpression(propertyItem.PropertyName),
                            CodeBinaryOperatorType.IdentityInequality,
                            new CodePrimitiveExpression(null)),
                        new CodeStatement[]
                        {
                            // <name>.Clear();
                            new CodeExpressionStatement(
                                new CodeMethodInvokeExpression(
                                    new CodeMethodReferenceExpression(
                                        new CodeVariableReferenceExpression(propertyItem.PropertyName), "Dispose")))
                        });

                    ifDisposingStatement.TrueStatements.Add(ifDisposingStatement);
                }
            }

            // Generate the set to null block:
            //
            // Generates a block similar to:
            //      <property> = null;
            //      <property2> = null;
            //
            disposeMethod.Statements.Add(new CodeSnippetStatement(string.Empty));
            foreach (PropertyProfile propertyItem in nullableList)
            {
                CodeAssignStatement statement = new CodeAssignStatement(
                    new CodeVariableReferenceExpression(propertyItem.PropertyName),
                    new CodePrimitiveExpression(null));
                disposeMethod.Statements.Add(statement);
            }

            // base.Dispose(disposing);
            disposeMethod.Statements.Add(new CodeMethodInvokeExpression(
                new CodeMethodReferenceExpression(new CodeBaseReferenceExpression(), "Dispose"),
                new CodeVariableReferenceExpression("disposing")));
        }

        /// <summary>
        /// Creates the XML parameter comment section.
        /// </summary>
        /// <param name="parameterName">A string containing the name of the parameter.</param>
        /// <param name="text">A string containing the description of the parameter.</param>
        /// <returns>
        /// An array of <see cref="CodeCommentStatement"/> instances used to define an XML parameter
        /// comment section.
        /// </returns>
        public static CodeCommentStatement[] CreateParameter(string parameterName, string text)
        {
            return new CodeCommentStatement[]
            {
                new CodeCommentStatement(new CodeComment("<param name=\"" + parameterName + "\">", true)),
                new CodeCommentStatement(new CodeComment(text, true)),
                new CodeCommentStatement(new CodeComment("</param>", true))
            };
        }

        /// <summary>
        /// Creates the property definition.
        /// </summary>
        /// <param name="property">
        /// The <see cref="PropertyProfile"/> instance defining the property.
        /// </param>
        /// <returns>
        /// A <see cref="CodeMemberProperty"/> used to render the property definition.
        /// </returns>
        public static CodeMemberProperty CreatePropertyDefinition(PropertyProfile property)
        {
            //public <type> <Field Name> { get; set;}
            //e.g.: public int ActionId { get; set; }
            CodeMemberProperty newProperty = new CodeMemberProperty
            {
                Attributes = MemberAttributes.Public | MemberAttributes.Final,
                HasGet = true,
                HasSet = true,
                Name = property.PropertyName,
                Type = new CodeTypeReference(property.TypeName)
            };

            //  /// <summary>
            //  /// Gets or sets the ...
            //  /// </summary>
            newProperty.Comments.AddRange(CreateSummary("Gets or sets the // TODO: Describe this"));
            //  /// <value>
            //  /// A(n) <data type> specifying ...
            //  /// </value>
            string valueText = "A";
            string? ptName = property.TypeName;
            if (ptName != null && (ptName.StartsWith("A") || ptName.StartsWith("E") || ptName.StartsWith("I") ||
                ptName.StartsWith("O") || ptName.StartsWith("U")))
            {
                valueText += "n";
            }
            valueText += " <see cref=\"" + ptName + "\"/> ";

            newProperty.Comments.AddRange(CreateValueText(valueText));
            return newProperty;
        }

        /// <summary>
        /// Creates the XML remarks objects.
        /// </summary>
        /// <param name="remarksText">A string containing the summary text.</param>
        /// <returns>
        /// An array of <see cref="CodeCommentStatement"/> instances used to define the XML remarks
        /// comment section.
        /// </returns>
        public static CodeCommentStatement[] CreateRemarks(string remarksText)
        {
            return new CodeCommentStatement[]
            {
                new CodeCommentStatement(new CodeComment("<remarks>", true)),
                new CodeCommentStatement(new CodeComment(remarksText, true)),
                new CodeCommentStatement(new CodeComment("</remarks>", true))
            };
        }

        /// <summary>
        /// Creates the XML returns objects.
        /// </summary>
        /// <param name="returnsText">A string containing the returns text.</param>
        /// <returns>
        /// An array of <see cref="CodeCommentStatement"/> instances used to define the XML returns
        /// comment section.
        /// </returns>
        public static CodeCommentStatement[] CreateReturns(string returnsText)
        {
            return new CodeCommentStatement[]
            {
                new CodeCommentStatement(new CodeComment("<returns>", true)),
                new CodeCommentStatement(new CodeComment(returnsText, true)),
                new CodeCommentStatement(new CodeComment("</returns>", true))
            };
        }

        /// <summary>
        /// Creates the XML summary objects.
        /// </summary>
        /// <param name="summaryText">A string containing the summary text.</param>
        /// <returns>
        /// An array of <see cref="CodeCommentStatement"/> instances used to define the XML summary
        /// comment section.
        /// </returns>
        public static CodeCommentStatement[] CreateSummary(string summaryText)
        {
            return new CodeCommentStatement[]
            {
                new CodeCommentStatement(new CodeComment("<summary>", true)),
                new CodeCommentStatement(new CodeComment(summaryText, true)),
                new CodeCommentStatement(new CodeComment("</summary>", true))
            };
        }
        /// <summary>
        /// Creates the XML value objects.
        /// </summary>
        /// <param name="valueText">A string containing the value text.</param>
        /// <returns>
        /// An array of <see cref="CodeCommentStatement"/> instances used to define the XML value
        /// comment section.
        /// </returns>
        public static CodeCommentStatement[] CreateValueText(string valueText)
        {
            return new CodeCommentStatement[]
            {
                new CodeCommentStatement(new CodeComment("<value>", true)),
                new CodeCommentStatement(new CodeComment(valueText, true)),
                new CodeCommentStatement(new CodeComment("</value>", true))
            };
        }
        /// <summary>
        /// Creates the end of a region section.
        /// </summary>
        /// <returns>
        /// A <see cref="CodeRegionDirective"/> instance used to render the end of a region.
        /// </returns>
        public static CodeRegionDirective EndRegion()
        {
            return new CodeRegionDirective(CodeRegionMode.End, null);
        }

        /// <summary>
        /// Renders the code from the provided CodeDOM object.
        /// </summary>
        /// <param name="nameSpaceContainer">
        /// The <see cref="CodeNamespace"/> instance containing the class definition(s).
        /// </param>
        /// <returns>
        /// A string containing the C# rendered code.
        /// </returns>
        public static string RenderCode(CodeNamespace nameSpaceContainer)
        {
            // Create the code writer.
            StringBuilder builder = new StringBuilder();
            StringWriter writer = new StringWriter(builder);
            writer.WriteLine("using System;");
            writer.WriteLine("using System.Data.SqlClient;");
            writer.WriteLine();

            // Generate the code.
            CodeGeneratorOptions opts = new CodeGeneratorOptions
            {
                BlankLinesBetweenMembers = false,
                BracingStyle = "C",
                IndentString = "\t",
                VerbatimOrder = true
            };
            CSharpCodeProvider provider = new CSharpCodeProvider();
            provider.GenerateCodeFromNamespace(nameSpaceContainer, writer, opts);
            writer.Flush();
            string code = builder.ToString();

            provider.Dispose();
            writer.Dispose();
            builder.Clear();
            return code;
        }

        /// <summary>
        /// Creates the start of a region section.
        /// </summary>
        /// <param name="regionName">
        /// A string containing the name of the region.
        /// </param>
        /// <returns>
        /// A <see cref="CodeRegionDirective"/> instance used to render the start of a region.
        /// </returns>
        public static CodeRegionDirective StartRegion(string regionName)
        {
            return new CodeRegionDirective(CodeRegionMode.Start, regionName);
        }
    }
}