using Adaptive.CodeDom.Model;
using Adaptive.Intelligence;
using Microsoft.CSharp;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Text;

namespace Adaptive.CodeDom
{
    /// <summary>
    /// Provides extension and helper methods for generating CodeDOM objects and models.
    /// </summary>
    public static class CodeDomObjectFactory
    {
        #region Public Static Methods / Functions
        /// <summary>
        /// Creates the assignment statement.
        /// </summary>
        /// <param name="variableName">
        /// A string containing the name of the variable being assigned to.
        /// </param>
        /// <param name="value">
        /// An object containing the literal value being assigned to the variable.
        /// </param>
        /// <returns>
        /// A <see cref="CodeAssignStatement"/> representing the assignment statement.
        /// </returns>
        public static CodeAssignStatement CreateAssignmentStatement(string variableName, object value)
        {
            return new CodeAssignStatement(
                new CodeVariableReferenceExpression(variableName),
                new CodePrimitiveExpression(value));
        }
        /// <summary>
        /// Creates the assignment statement.
        /// </summary>
        /// <param name="variableName">
        /// A string containing the name of the variable being assigned to.
        /// </param>
        /// <param name="value">
        /// A <see cref="CodeExpression"/> containing the expression of the value being assigned to the variable.
        /// </param>
        /// <returns>
        /// A <see cref="CodeAssignStatement"/> representing the assignment statement.
        /// </returns>
        public static CodeAssignStatement CreateAssignmentStatement(string variableName, CodeExpression value)
        {
            return new CodeAssignStatement(
                new CodeVariableReferenceExpression(variableName),
                value);
        }
        /// <summary>
        /// Creates the base class code type reference definition.
        /// </summary>
        /// <param name="baseClass">
        /// The <see cref="BaseClassModel"/> instance containing the base class definition.
        /// </param>
        /// <returns>
        /// A <see cref="CodeTypeReference"/> instance used to generate the base class inheritance definition.
        /// </returns>
        public static CodeTypeReference CreateBaseClassDefinition(BaseClassModel baseClass)
        {
            CodeTypeReference baseClassToInherit = new CodeTypeReference();

            // System.Object
            if (baseClass.IsPOCO)
                baseClassToInherit.BaseType = CodeDomConstants.DefaultObjectName;

            // Specified class.
            else
            {
                baseClassToInherit.BaseType = baseClass.TypeName;
                // ADd the type arguments for generics, if present.
                if (baseClass.IsGeneric)
                    baseClassToInherit.TypeArguments.Add(baseClass.GenericTypeName);
            }

            return baseClassToInherit;
        }
        /// <summary>
        /// Creates the class constructor declaration with parameters.
        /// </summary>
        /// <param name="className">
        /// A string containing the name of the class.
        /// </param>
        /// <param name="parameterList">
        /// A <see cref="List{T}"/> of <see cref="CodeParameterDeclarationExpression"/> instances describing the constructor
        /// parameters.
        /// </param>
        /// <param name="baseVariables">
        /// A <see cref="List{T}"/> of <see cref="string"/> instances containing the names of the parameters to pass to
        /// the base constructor call, or <b>null</b> if not used.
        /// </param>
        /// <returns>
        /// The <see cref="CodeConstructor"/> instance used to generate the constructor code.
        /// </returns>
        public static CodeConstructor CreateConstructorWithParameters(string className, List<CodeParameterDeclarationExpression> parameterList, List<string>? baseVariables = null)
        {
            // Create the "wrapper" class object.  For C# code, the generated constructor will not be named properly without this.
            CodeTypeDeclaration classType = new CodeTypeDeclaration(className);
            classType.Name = className;
            classType.IsPartial = false;
            classType.IsClass = true;

            CodeConstructor constructor = new CodeConstructor();
            constructor.Attributes = MemberAttributes.Public;
            constructor.Parameters.AddRange(parameterList.ToArray());
            classType.Members.Add(constructor);

            // Add the base constructor call, if needed.
            if (baseVariables != null)
            {
                foreach (string variableName in baseVariables)
                {
                    constructor.BaseConstructorArgs.Add(new CodeVariableReferenceExpression(variableName));
                }
            }

            return constructor;
        }
        /// <summary>
        /// Creates the default constructor.
        /// </summary>
        /// <param name="className">
        /// A string containing the name of the class.
        /// </param>
        /// <param name="baseVariables">
        /// A <see cref="List{T}"/> of <see cref="string"/> instances containing the names of the parameters to pass to
        /// the base constructor call, or <b>null</b> if not used.
        /// </param>
        /// <returns>
        /// The <see cref="CodeConstructor"/> instance used to generate the constructor code.
        /// </returns>
        public static CodeConstructor CreateDefaultConstructor(string className, List<string>? baseVariables = null)
        {
            // Create the "wrapper" class object.  For C# code, the generated constructor will not be named properly without this.
            CodeTypeDeclaration classType = new CodeTypeDeclaration(className);
            classType.Name = className;
            classType.IsPartial = false;
            classType.IsClass = true;

            CodeConstructor constructor = new CodeConstructor();
            constructor.Attributes = MemberAttributes.Public;
            classType.Members.Add(constructor);

            // Add the base constructor call, if needed.
            if (baseVariables != null)
            {
                foreach (string variableName in baseVariables)
                {
                    constructor.BaseConstructorArgs.Add(new CodeVariableReferenceExpression(variableName));
                }
            }
            return constructor;
        }
        /// <summary>
        /// Creates the CodeDom object for generating a method.
        /// </summary>
        /// <param name="methodName">
        /// A string containing the name of the method.
        /// </param>
        /// <param name="returnType">
        /// A string indicating the name of the data type of the return value of the function.  If <b>null</b>, this assumes
        /// a method that returns "void".
        /// </param>
        /// <param name="parameterList">
        /// An <see cref="IEnumerable{T}"/> list of <see cref="NameValuePair{T}"/> of <see cref="string"/> containing the 
        /// names of parameters and their declared data types.
        /// The parameter list.</param>
        /// <param name="accessModifier">
        /// A <see cref="TypeAccessModifier"/> indicating the access modifier for the method/function.
        /// </param>
        /// <param name="isOverride">
        /// <b>true</b> if the method is declared as a method override; otherwise, <b>false</b>.
        /// </param>
        /// <param name="isVirtual">
        /// <b>true</b> if the method is declared as a virtual method; otherwise, <b>false</b>.
        /// </param>
        /// <returns>
        /// A <see cref="CodeMemberMethod"/> instance used to generate the method declaration.
        /// </returns>
        public static CodeMemberMethod CreateMethod(string methodName, string? returnType,
            IEnumerable<NameValuePair<string>> parameterList, TypeAccessModifier accessModifier,
            bool isOverride, bool isVirtual)
        {
            CodeMemberMethod method = new CodeMemberMethod();
            method.Name = methodName;
            if (accessModifier == TypeAccessModifier.Public)
                method.Attributes = MemberAttributes.Public;

            else if (accessModifier == TypeAccessModifier.Protected)
                method.Attributes = MemberAttributes.Family;

            else if (accessModifier == TypeAccessModifier.Private)
                method.Attributes = MemberAttributes.Private;

            else
                // Default to public.
                method.Attributes = MemberAttributes.Public;

            // Method Override
            if (isOverride)
                method.Attributes |= MemberAttributes.Override;

            // Virtual Method
            else if (!isVirtual)
                method.Attributes |= MemberAttributes.Final;

            // When creating a function, specify a return type; Otherwise, return "void" for a method.
            if (returnType != null)
                method.ReturnType = new CodeTypeReference(returnType);
            else
                method.ReturnType = null;

            // Add the parameter definitions.
            foreach (NameValuePair<string> item in parameterList)
            {
                CodeParameterDeclarationExpression parameter = new CodeParameterDeclarationExpression(item.Name, item.Value);
                method.Parameters.Add(parameter);
            }
            return method;
        }
        /// <summary>
        /// Creates the method call expression object.
        /// </summary>
        /// <param name="methodName">
        /// A string containing the name of the method to be called.
        /// </param>
        /// <returns>
        /// A <see cref="CodeMethodInvokeExpression"/> instance used to generate the method call code.
        /// </returns>
        public static CodeMethodInvokeExpression CreateMethodCallExpression(string methodName)
        {
            return new CodeMethodInvokeExpression
            {
                Method = new CodeMethodReferenceExpression(null, methodName)
            };
        }
        /// <summary>
        /// Creates the method call expression object.
        /// </summary>
        /// <param name="variableName">
        /// A string containing the variable name or qualifying name of the object instance to call the method on.
        /// </param>
        /// <param name="methodName">
        /// A string containing the name of the method to be called.
        /// </param>
        /// <returns>
        /// A <see cref="CodeMethodInvokeExpression"/> instance used to generate the method call code.
        /// </returns>
        public static CodeMethodInvokeExpression CreateMethodCallExpression(string variableName, string methodName)
        {
            return new CodeMethodInvokeExpression
            {
                Method = new CodeMethodReferenceExpression(new CodeVariableReferenceExpression(variableName), methodName)
            };
        }
        /// <summary>
        /// Creates the method call expression object.
        /// </summary>
        /// <param name="methodName">
        /// A string containing the name of the method to be called.
        /// </param>
        /// <param name="parameterList">
        /// A <see cref="List{T}"/> of <see cref="string"/> containing the list of parameters/variable names.
        /// </param>
        /// <returns>
        /// A <see cref="CodeMethodInvokeExpression"/> instance used to generate the method call code.
        /// </returns>
        public static CodeMethodInvokeExpression CreateMethodCallExpression(string methodName, List<string> parameterList)
        {
            CodeMethodInvokeExpression exp = new CodeMethodInvokeExpression();
            exp.Method = new CodeMethodReferenceExpression(null, methodName);
            foreach (string param in parameterList)
            {
                exp.Parameters.Add(new CodeVariableReferenceExpression(param));
            }
            return exp;
        }
        /// <summary>
        /// Creates the method call expression object.
        /// </summary>
        /// <param name="variableName">
        /// A string containing the variable name or qualifying name of the object instance to call the method on.
        /// </param>
        /// <param name="methodName">
        /// A string containing the name of the method to be called.
        /// </param>
        /// <param name="parameterList">
        /// A <see cref="List{T}"/> of <see cref="string"/> containing the list of parameters/variable names.
        /// </param>
        /// <returns>
        /// A <see cref="CodeMethodInvokeExpression"/> instance used to generate the method call code.
        /// </returns>
        public static CodeMethodInvokeExpression CreateMethodCallExpression(string variableName, string methodName, List<string> parameterList)
        {
            CodeMethodInvokeExpression exp = new CodeMethodInvokeExpression();
            exp.Method = new CodeMethodReferenceExpression(new CodeVariableReferenceExpression(variableName), methodName);
            foreach (string param in parameterList)
            {
                exp.Parameters.Add(new CodeVariableReferenceExpression(param));
            }
            return exp;
        }
        /// <summary>
        /// Creates the CodeDom object for declaring a new array instantiation.
        /// </summary>
        /// <param name="dataType">
        /// A string containing the name of the data type of the array to create.
        /// </param>
        /// <param name="sizeSpecification">
        /// An optional integer value indicating the size of the array to create.
        /// </param>
        /// <returns>
        /// The <see cref="CodeArrayCreateExpression"/> instance used to generate the array instantiation code.	
        /// </returns>
        public static CodeArrayCreateExpression CreateNewArrayExpression(string dataType, int? sizeSpecification)
        {
            CodeArrayCreateExpression arrayCreate;

            if (sizeSpecification != null)
                arrayCreate = new CodeArrayCreateExpression(new CodeTypeReference(dataType), sizeSpecification.Value);
            else
                arrayCreate = new CodeArrayCreateExpression(new CodeTypeReference(dataType));

            return arrayCreate;
        }
        /// <summary>
        /// Creates the CodeDom object for declaring a new array instantiation.
        /// </summary>
        /// <param name="dataType">
        /// A string containing the name of the data type of the array to create.
        /// </param>
        /// <param name="values">
        /// An <see cref="IEnumerable{T}"/> of <see cref="CodeExpression"/> instances containing the values to initialize the 
        /// array with.
        /// </param>
        /// <param name="sizeSpecification">
        /// An optional integer value indicating the size of the array to create.
        /// </param>
        /// <returns>
        /// The <see cref="CodeArrayCreateExpression"/> instance used to generate the array instantiation code.	
        /// </returns>
        public static CodeArrayCreateExpression CreateNewArrayExpressionWithInitializers(string dataType, int? sizeSpecification,
            IEnumerable<CodeExpression> values)
        {
            CodeArrayCreateExpression arrayCreate;

            if (sizeSpecification != null)
                arrayCreate = new CodeArrayCreateExpression(new CodeTypeReference(dataType), sizeSpecification.Value);
            else
                arrayCreate = new CodeArrayCreateExpression(new CodeTypeReference(dataType));

            arrayCreate.Initializers.AddRange(values.ToArray());
            return arrayCreate;
        }
        /// <summary>
        /// Creates the new object declaration statement.
        /// </summary>
        /// <param name="dataTypeName">
        /// A string containing the name of the data type.
        /// </param>
        /// <param name="variableName">
        /// A string containing the name of the variable being declared.
        /// </param>
        /// <returns>
        /// A <see cref="CodeVariableDeclarationStatement"/> instance with an initializer that renders something like this:
        /// 
        /// <code>
        ///		DataTable table = new DataTable();
        /// </code>
        /// 
        /// </returns>
        public static CodeVariableDeclarationStatement CreateNewObjectDeclaration(string dataTypeName, string variableName)
        {
            CodeObjectCreateExpression newObjectExpression = new CodeObjectCreateExpression(
                    new CodeTypeReference(dataTypeName), null);

            return new CodeVariableDeclarationStatement(
                dataTypeName, // The type of the variable
                variableName, // The name of the variable
                newObjectExpression // The expression to assign to the variable, in this case, the new object creation expression
                );
        }

        /// <summary>
        /// Creates a private constant declaration.
        /// </summary>
        /// <param name="dataType">
        /// A string containing the name of the data type of the constant.
        /// </param>
        /// <param name="variableName">
        /// A string containing the name of the constant.
        /// </param>
        /// <param name="valueAsText">
        /// A string containing the representation of the constant's value as a string.
        /// </param>
        /// <param name="isPrivate">
        /// A value indicating whether to declare the constant as private.  Default is <b>true</b>.
        /// </param>
        /// <returns>
        /// A <see cref="CodeMemberField"/> instance used to generate the constant declaration code.
        /// </returns>
        public static CodeMemberField CreatePrivateConstantDeclaration(Type dataType, string variableName, string valueAsText, bool isPrivate = true)
        {
            CodeMemberField definition = new CodeMemberField(dataType, variableName);
            definition.InitExpression = new CodePrimitiveExpression(valueAsText);
            definition.Attributes = MemberAttributes.Const;
            if (isPrivate)
                definition.Attributes |= MemberAttributes.Private;
            else
                definition.Attributes |= MemberAttributes.Public;

            return definition;
        }
        /// <summary>
        /// Creates a private constant declaration.
        /// </summary>
        /// <param name="dataType">
        /// A string containing the name of the data type of the constant.
        /// </param>
        /// <param name="variableName">
        /// A string containing the name of the constant.
        /// </param>
        /// <param name="valueExpression">
        /// A <see cref="CodeExpression"/> instance containing the expression of the value being assigned to the constant.
        /// </param>
        /// <param name="isPrivate">
        /// A value indicating whether to declare the constant as private.  Default is <b>true</b>.
        /// </param>
        /// <returns>
        /// A <see cref="CodeMemberField"/> instance used to generate the constant declaration code.
        /// </returns>
        public static CodeMemberField CreatePrivateConstantDeclaration(Type dataType, string variableName, CodeExpression valueExpression, bool isPrivate = true)
        {
            CodeMemberField definition = new CodeMemberField(dataType, variableName);
            definition.InitExpression = valueExpression;
            if (isPrivate)
                definition.Attributes |= MemberAttributes.Private;
            else
                definition.Attributes |= MemberAttributes.Public;
            return definition;
        }
        /// <summary>
        /// Creates the CodeDOM function return statement.
        /// </summary>
        /// <param name="expression">
        /// The <see cref="CodeExpression"/> instance containing the definition of what is being returned.
        /// </param>
        /// <returns>
        /// The <see cref="CodeMethodReturnStatement"/> instance used to generate the return statement code.
        /// </returns>
        public static CodeMethodReturnStatement CreateReturnStatement(CodeExpression expression)
        {
            CodeMethodReturnStatement statement = new CodeMethodReturnStatement();
            statement.Expression = expression;
            return statement;
        }
        #endregion

        #region Private Static Methods / Functions
        #endregion

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
        /// The <see cref="PropertyProfileCollection"/> instance containing the property meta data
        /// used to generate appropriate Dispose code for the properties of the parent class.
        /// </param>
        public static void CreateDisposeMethodContent(CodeMemberMethod disposeMethod, PropertyProfileCollection? propertyList)
        {
            // Determine which definitions go in which blocks...
            PropertyProfileCollection? clearableList = propertyList?.ClearableProperties();
            PropertyProfileCollection? disposableList = propertyList?.DisposableProperties();
            PropertyProfileCollection? nullableList = propertyList?.NullableProperties();

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
            if (propertyList != null && propertyList.HasClearableOrDisposableItems)
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

                if (clearableList != null)
                {
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
                }

                if (disposableList != null)
                {
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
            }

            // Generate the set to null block:
            //
            // Generates a block similar to:
            //      <property> = null;
            //      <property2> = null;
            //
            disposeMethod.Statements.Add(new CodeSnippetStatement(string.Empty));
            if (nullableList != null)
            {
                foreach (PropertyProfile propertyItem in nullableList)
                {
                    CodeAssignStatement statement = new CodeAssignStatement(
                        new CodeVariableReferenceExpression(propertyItem.PropertyName),
                        new CodePrimitiveExpression(null));
                    disposeMethod.Statements.Add(statement);
                }
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
