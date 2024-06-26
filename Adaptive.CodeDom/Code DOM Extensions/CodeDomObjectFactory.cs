using Adaptive.CodeDom.Model;
using Adaptive.Intelligence;
using System.CodeDom;

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

			if (sizeSpecification == null)
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
			definition.Attributes =  MemberAttributes.Const;
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
		public static CodeMemberField CreatePrivateConstantDeclaration(Type dataType, string variableName, CodeExpression valueExpression, bool isPrivate=true)
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

	}
}
