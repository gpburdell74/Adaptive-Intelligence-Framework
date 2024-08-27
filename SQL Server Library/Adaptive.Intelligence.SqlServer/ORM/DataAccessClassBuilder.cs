// Ignore Spelling: Sql
// Ignore Spelling: ORM

using Adaptive.CodeDom;
using Adaptive.CodeDom.Model;
using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.SqlServer.Analysis;
using Adaptive.Intelligence.SqlServer.Schema;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using System.CodeDom;

namespace Adaptive.Intelligence.SqlServer.ORM
{
	/// <summary>
	/// Provides the builder class for generating the Data Access classes for a table.
	/// </summary>
	/// <seealso cref="DisposableObjectBase" />
	public sealed class DataAccessClassBuilder : DisposableObjectBase
	{
		#region Private Member Declarations        
		/// <summary>
		/// The database reference.
		/// </summary>
		private DatabaseInfo? _db;
		/// <summary>
		/// The table reference.
		/// </summary>
		private SqlTable? _table;
		/// <summary>
		/// The table profile reference.
		/// </summary>
		private AdaptiveTableProfile? _profile;
		/// <summary>
		/// The code generator/writer instance.
		/// </summary>
		private CsCodeWriter? _writer;
		/// <summary>
		/// The standard name space for the class.
		/// </summary>
		private string? _standardNameSpace;
		/// <summary>
		/// The class being built.
		/// </summary>
		private ClassModel? _class;
		#endregion

		#region Constructor / Dispose Methods        
		/// <summary>
		/// Initializes a new instance of the <see cref="DataAccessClassBuilder"/> class.
		/// </summary>
		/// <param name="dbInfo">
		/// The reference to the <see cref="DatabaseInfo"/> instance containing the database information.
		/// </param>
		/// <param name="targetTable">
		/// The <see cref="SqlTable"/> reference to the table the data access class is being generated for.
		/// </param>
		/// <param name="profile">
		/// The <see cref="AdaptiveTableProfile"/> reference to the profile for the table the data access class is being generated for.
		/// </param>
		/// <param name="standardNameSpace">
		/// A string containing the namespace to define the class under.
		/// </param>
		public DataAccessClassBuilder(DatabaseInfo dbInfo, SqlTable targetTable, AdaptiveTableProfile profile, string standardNameSpace)
		{
			_db = dbInfo;
			_table = targetTable;
			_profile = profile;
			_writer = new CsCodeWriter();
			_standardNameSpace = standardNameSpace;
			_class = new ClassModel();
		}
		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
		/// <b>false</b> to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			if (!IsDisposed && disposing)
			{
				_class?.Dispose();
				_writer?.Dispose();
			}

			_class = null;
			_writer = null;
			_db = null;
			_table = null;
			_profile = null;
			_standardNameSpace = null;
			base.Dispose(disposing);
		}
		#endregion

		#region Public Methods / Functions        
		/// <summary>
		/// Generates the code for the data access class.
		/// </summary>
		/// <param name="language">
		/// A <see cref="NetLanguage"/> enumerated value indicating the language to generate the code in.
		/// </param>
		/// <returns>
		/// A string containing the C# code file contents.
		/// </returns>
		public string GenerateDataAccessClass(NetLanguage language)
		{
			if (_profile != null)
			{
				// Create the modeling object.
				_class?.Dispose();
				_class = new ClassModel
				{
					IsPublic = true,
					IsSealed = true,
					
				};

				_class.ClassName = _profile.DataAccessClassName;

                // Write the standard using list.
                GenerateUsings();

				// Write the namespace.
				GenerateNamespaceStart();

				// Write the class comments.
				GenerateClassStart();

				// #region Private Constants Declarations
				GeneratePrivateConstants();

				// Constructors.
				GenerateConstructors();

				// Protected Methods 
				GenerateProtectedMethods();

				// TODO: Make this better code.
				// Ensure the "class Name" is known to all the sections.
				foreach (CodeSectionModel model in _class.CodeSections)
					model.ClassName = _class.ClassName;

				ClassModelGenerator generator = new ClassModelGenerator();
				string code = generator.RenderClass(_class, language);
				return code;
			}
			else
				return string.Empty;


			return string.Empty;
		}
		#endregion

		#region Private Methods / Functions        
		/// <summary>
		/// Generates the standard using(s) list.
		/// </summary>
		private void GenerateUsings()
		{
			if (_class != null)
			{
				// Add the default usings / imports.
				foreach (string namespaceName in GenerationSettings.Current.DefaultUsingsOrImports)
				{
					_class.AddImport(namespaceName);
				}
			}
		}
		/// <summary>
		/// Generates the start of the namespace block.
		/// </summary>
		private void GenerateNamespaceStart()
		{
			if (_class != null)
			{
				if (string.IsNullOrEmpty(_standardNameSpace))
					_class.Namespace = GenerationSettings.Current.DefaultNamespace;
				else
					_class.Namespace = _standardNameSpace;
			}
		}
		/// <summary>
		/// Generates the class declaration start block.
		/// </summary>
		private void GenerateClassStart()
		{
			if (_class != null && _profile != null)
			{
				_class.Summary = OrmCodeGenerationOptions.Current.DataAccessClassSummary;
				_class.Remarks = OrmCodeGenerationOptions.Current.DataAccessClassRemarks;
				_class.AddSeeAlsoText(OrmCodeGenerationOptions.Current.DataAccessBaseClassName);

				_class.IsPublic = true;
				_class.IsSealed = true;

				_class.SetBaseClassProperties(
					OrmCodeGenerationOptions.Current.DataAccessBaseClassName,
					false, true, _profile.DataDefinitionClassName);

				_class.AddInterface(typeof(IDisposable));
			}
		}
		/// <summary>
		/// Generates the private constants section.
		/// </summary>
		private void GeneratePrivateConstants()
		{
			if (_class != null && _table != null && _class.CodeSections != null)
			{
				CodeSectionModel? codeSection = _class.CodeSections.GetSectionByType(CodeSectionType.PrivateConstants);
				if (codeSection != null)
				{
					// Efficiency - reference once.
					OrmDatabaseOptions opts = OrmDatabaseOptions.Current;
					OrmCodeGenerationOptions codeOpts = OrmCodeGenerationOptions.Current;
					Type stringType = typeof(string);

					// SqlGetAll - The stored procedure to retrieve all the records in the table.
					codeSection.AddPrivateConstant(
						codeOpts.SpConstantNameForGetAll,
						opts.RenderStoredProcedureName(_table.TableName, opts.RetrieveAllRecordsStoredProcedureName),
						stringType,
						codeOpts.SpConstantSummaryForGetAll);

					// SqlGetById - The stored procedure to retrieve a single record by ID value.
					codeSection.AddPrivateConstant(
						codeOpts.SpConstantNameForGetById,
						opts.RenderStoredProcedureName(_table.TableName, opts.RetrieveRecordByIdStoredProcedureName),
						stringType,
						codeOpts.SpConstantSummaryForGetById);

					// SqlDelete
					codeSection.AddPrivateConstant(
						codeOpts.SpConstantNameForDelete,
						opts.RenderStoredProcedureName(_table.TableName, opts.DeleteStoredProcedureName),
						stringType,
						codeOpts.SpConstantSummaryForDelete);

					// SqlInsert
					codeSection.AddPrivateConstant(
						codeOpts.SpConstantNameForInsert,
						opts.RenderStoredProcedureName(_table.TableName, opts.InsertStoredProcedureName),
						stringType,
						codeOpts.SpConstantSummaryForInsert);

					// SqlUpdate
					codeSection.AddPrivateConstant(
						codeOpts.SpConstantNameForUpdate,
						opts.RenderStoredProcedureName(_table.TableName, opts.UpdateStoredProcedureName),
						stringType,
						codeOpts.SpConstantSummaryForUpdate);

					//
					// Write the stored procedure variable/parameter definitions/declarations.
					// e.g.:
					//          private const string SqlParamCenterName = "@CenterName";
					//

					foreach (SqlColumn col in _table.Columns)
					{
						codeSection.AddPrivateConstant(
											   codeOpts.SpConstantNamePrefixForParameter + col.ColumnName,
											   TSqlConstants.SqlParameterPrefix + col.ColumnName,
											   stringType,
											   codeOpts.SpConstantSqlParameterSummary.Replace("{0}", col.ColumnName));
					}
				}
			}
		}
		/// <summary>
		/// Generates the constructor definitions.
		/// </summary>
		private void GenerateConstructors()
		{
			if (_class != null && _table != null)
			{
				CodeSectionModel? model = _class.GetCodeSectionByType(CodeSectionType.ConstructorDispose);
				if (model != null)
				{
					OrmCodeGenerationOptions codeOpts = OrmCodeGenerationOptions.Current;

					// Standard Parameterless Constructor.
					CodePartModel defaultConstructor = new CodePartModel();
					defaultConstructor.Summary = codeOpts.ParameterLessConstructorXmlSummaryTemplate.Replace("{0}", model.ClassName);
					defaultConstructor.Remarks = codeOpts.ParameterLessConstructorXmlRemarksTemplate.Replace("{0}", model.ClassName);
                    defaultConstructor.Content = CodeDomObjectFactory.CreateDefaultConstructor(_class.ClassName,
						new List<string>
						{
							codeOpts.SpConstantNameForGetAll,
							codeOpts.SpConstantNameForGetById,
							codeOpts.SpConstantNameForDelete,
							codeOpts.SpConstantNameForInsert,
							codeOpts.SpConstantNameForUpdate
						});
					model.AddPart(defaultConstructor);

					// Constructor with SqlDataProvider parameter.
					CodePartModel secondConstructor = new CodePartModel();
					secondConstructor.Summary = codeOpts.ConstructorXmlSummaryTemplate;
					secondConstructor.AddParameterComment(
						codeOpts.ConstructorProviderVariableName,
						codeOpts.ConstructorProviderVariableParamSummary);
					secondConstructor.Content = CodeDomObjectFactory.CreateConstructorWithParameters(_class.ClassName,
						new List<CodeParameterDeclarationExpression>
						{
							new CodeParameterDeclarationExpression(codeOpts.ConstructorProviderVariableTypeName, codeOpts.ConstructorProviderVariableName)
						},
						new List<string>
						{
							codeOpts.SpConstantNameForGetAll,
							codeOpts.SpConstantNameForGetById,
							codeOpts.SpConstantNameForDelete,
							codeOpts.SpConstantNameForInsert,
							codeOpts.SpConstantNameForUpdate
						});
					model.AddPart(secondConstructor);
				}
			}
		}
		/// <summary>
		/// Generates the protected methods.
		/// </summary>
		private void GenerateProtectedMethods()
		{
			if (_class != null && _table != null)
			{
				CodeSectionModel? model = _class.GetCodeSectionByType(CodeSectionType.ProtectedMethods);
				if (model != null)
				{
					GenerateCreateInsertParameters(model);
					GenerateCreateUpdateParameters(model);
					GeneratePopulateRecord(model);
				}
			}
		}
		/// <summary>
		/// Generates the create insert parameters method.
		/// </summary>
		private void GenerateCreateInsertParameters(CodeSectionModel model)
		{
			if (_class != null && _table != null)
			{
				OrmCodeGenerationOptions codeOpts = OrmCodeGenerationOptions.Current;
				
				// Create the comments for the method - renders content for something similar to:
				//
				// protected override SqlParameter[] CreateInsertParameters(CustomerEntity instance)
				//
				CodePartModel insertMethod = new CodePartModel();
				insertMethod.Name = "CreateInsertParameters";
				insertMethod.Summary = "Creates the array of parameters to use when inserting a new record.";
				insertMethod.Returns = "An array of <see cref=\"SqlParameter\"/> instances to be supplied to the stored procedure.";
				insertMethod.AddParameterComment("instance", "The <see cref=\"" + _profile.DataDefinitionClassName + "\"/> instance being inserted.");

				// Create the CodeDOM method object.
				// e.g.: 
				// protected override SqlParameter[] CreateInsertParameters(CustomerEntity instance)
				//
				CodeMemberMethod method = CodeDomObjectFactory.CreateMethod(
					"CreateInsertParameters",
					"SqlParameter[]",
					new NameValuePair<string>[]
					{
						new NameValuePair<string>(_class.ClassName, "instance")
					},
					TypeAccessModifier.Protected, true, false);

				// Translate the table columns into parameter array values populated by calling the base class'
				// CreateParameter() method.
				// e. g.
				//        CreateParameter("SqlParamId", instance.Id),
				//        ...
				//         
				List<CodeExpression> arrayValues = new List<CodeExpression>();
				foreach (SqlColumn col in _table.Columns)
				{
					// Do not include the ID column for an INSERT statement.
					if (string.Compare(col.ColumnName, TSqlConstants.StandardColumnId, StringComparison.OrdinalIgnoreCase) != 0)
					{
						arrayValues.Add(
							CodeDomObjectFactory.CreateMethodCallExpression(
								"CreateParameter",
									new List<string> { "SqlParam" + col, "instance." + col })
								);
					}
				}

				// Render the array creation code object.
				CodeArrayCreateExpression arrayExp = CodeDomObjectFactory.CreateNewArrayExpressionWithInitializers(
						"SqlParameter", null, arrayValues);

				// Encapsulate the array creation code object in a return statement.
				method.Statements.Add(CodeDomObjectFactory.CreateReturnStatement(arrayExp));
				insertMethod.Content = method;
				model.AddPart(insertMethod);
			}
		}
		/// <summary>
		/// Generates the create update parameters method.
		/// </summary>
		private void GenerateCreateUpdateParameters(CodeSectionModel model)
		{
			if (_class != null && _table != null)
			{
				OrmCodeGenerationOptions codeOpts = OrmCodeGenerationOptions.Current;

				// Create the comments for the method - renders content for something similar to:
				//
				// protected override SqlParameter[] CreateInsertParameters(CustomerEntity instance)
				//
				CodePartModel updateMethod = new CodePartModel();
				updateMethod.Name = "CreateUpdateParameters";
				updateMethod.Summary = "Creates the array of parameters to use when updating a record.";
				updateMethod.Returns = "An array of <see cref=\"SqlParameter\"/> instances to be supplied to the stored procedure.";
				updateMethod.AddParameterComment("instance", "The <see cref=\"" + _profile.DataDefinitionClassName + "\"/> instance being updated.");

				// Create the CodeDOM method object.
				// e.g.: 
				// protected override SqlParameter[] CreateInsertParameters(CustomerEntity instance)
				//
				CodeMemberMethod method = CodeDomObjectFactory.CreateMethod(
					"CreateUpdateParameters",
					"SqlParameter[]",
					new NameValuePair<string>[]
					{
						new NameValuePair<string>(_class.ClassName, "instance")
					},
					TypeAccessModifier.Protected, true, false);

				// Translate the table columns into parameter array values populated by calling the base class'
				// CreateParameter() method.
				// e. g.
				//        CreateParameter("SqlParamId", instance.Id),
				//        ...
				//         
				List<CodeExpression> arrayValues = new List<CodeExpression>();
				foreach (SqlColumn col in _table.Columns)
				{
					arrayValues.Add(
						CodeDomObjectFactory.CreateMethodCallExpression(
							"CreateParameter",
								new List<string> { "SqlParam" + col, "instance." + col })
							);
				}

				// Render the array creation code object.
				CodeArrayCreateExpression arrayExp = CodeDomObjectFactory.CreateNewArrayExpressionWithInitializers(
						"SqlParameter", null, arrayValues);

				// Encapsulate the array creation code object in a return statement.
				method.Statements.Add(CodeDomObjectFactory.CreateReturnStatement(arrayExp));
				updateMethod.Content = method;
				model.AddPart(updateMethod);
			}
		}
		/// <summary>
		/// Generates the populate record method.
		/// </summary>
		private void GeneratePopulateRecord(CodeSectionModel model)
		{
			if (_class != null && _table != null)
			{
				OrmCodeGenerationOptions codeOpts = OrmCodeGenerationOptions.Current;

				// Create the comments for the method - renders content for something similar to:
				CodePartModel populateMethod = new CodePartModel();
				populateMethod.Name = "PopulateRecord";
				populateMethod.Summary = "Populates the provided instance from the data source.";
				populateMethod.AddParameterComment("reader", "The <see cref=\"SafeSqlDataReader\"/> instance used to read the data.");
				populateMethod.AddParameterComment("instance", "The <see cref=\"" + _profile.DataDefinitionClassName + "\"/> instance to be populated.");

				// Create the method call.
				//
				// Renders something like:
				//
				// protected override void PopulateRecord(SafeSqlDataReader reader, CustomerEntity instance)
				CodeMemberMethod method = CodeDomObjectFactory.CreateMethod(
					"PopulateRecord",
					null,
					new NameValuePair<string>[]
					{
						new NameValuePair<string>("SafeSqlDataReader", "reader"),
						new NameValuePair<string>(_profile.DataDefinitionClassName, "instance")
					},
					TypeAccessModifier.Protected,
					true,
					false);

				// int index = 0;
				CodeAssignStatement indexEqualsZero = CodeDomObjectFactory.CreateAssignmentStatement("index", 0);
				method.Statements.Add(indexEqualsZero);

				// Populate the entity object from the columns in the table.
				foreach (SqlColumn col in _table.Columns)
				{
					//
					// instance.Id = reader.GetInt32(index++);
					//
					CodeAssignStatement variableRead = CodeDomObjectFactory.CreateAssignmentStatement(
						"instance." + col,
						CodeDomObjectFactory.CreateMethodCallExpression("reader." + GetMethodName(col.TypeId),
						new List<string> { "index++" })
					);
					method.Statements.Add(variableRead);
				}

				// For each table that is being joined to, generate a list of columns to query for.
				foreach (ReferencedTableJoin item in _profile.ReferencedTableJoins)
				{
					SqlTable referencedTable = item.ReferencedTable;
					AdaptiveTableProfile referencedProfile = _db.GetTableProfile(referencedTable.Schema, referencedTable.TableName);

					// Create a variable name for the child entity instance.
					string variableName = RenderVariableName(referencedProfile);

					// Add a comment.
					CodeCommentStatement itemComment = new CodeCommentStatement(variableName);
					method.Statements.Add(itemComment);

					// ChildEntityType variableName = new ChildEntityType();
					CodeVariableDeclarationStatement statement = CodeDomObjectFactory.CreateNewObjectDeclaration(
						referencedProfile.DataDefinitionClassName,
						variableName);
					method.Statements.Add(statement);

					// Populate the child entity object from the columns in the table.
					foreach (SqlColumn col in referencedTable.Columns)
					{
						//
						// childEntity.Id = reader.GetInt32(index++);   
						//
						string name = col.ColumnName;
						CodeAssignStatement variableRead = CodeDomObjectFactory.CreateAssignmentStatement(
							variableName + "." + col,
							CodeDomObjectFactory.CreateMethodCallExpression("reader." + GetMethodName(col.TypeId),
								new List<string> { "index++" })
							);

						method.Statements.Add(variableRead);
					}
				}

				populateMethod.Content = method;
				model.AddPart(populateMethod);
			}
		}
		/// <summary>
		/// Gets the name of the method to call on the data reader for the specified data type.
		/// </summary>
		/// <param name="dataTypeId">
		/// An integer specifying the SQL data type being read.
		/// </param>
		/// <returns>
		/// A string containing the name of the method to call.
		/// </returns>
		private string GetMethodName(int dataTypeId)
		{
			string methodName = "GetString"; // Default to GetString when all else fails.

			// Null check.
			if (_db != null && _db.Database != null && _db.Database.DataTypes != null && _db.Database.DataTypes.Count > 0)
			{
				OrmCodeGenerationOptions codeOpts = OrmCodeGenerationOptions.Current;

				// Find the data type from the standard list in sys.types using the user_type_id column value.
				SqlDataType? type = _db.Database.DataTypes.GetTypeById(dataTypeId);

				// If found, render the correct method name to call for the reader instance.
				if (type != null)
				{
					// Get the equivalent .NET type name and remove the namespace qualifier.
					string name = type.GetDotNetType().Name.Replace("System.", string.Empty);

					// Return "Get" + the type name,
					methodName = "Get" + name;

				}
			}

			return methodName;
		}
		/// <summary>
		/// Renders the name of the variable for a child entity object from it's related table name.
		/// </summary>
		/// <param name="referencedProfile">
		/// The referenced <see cref="AdaptiveTableProfile"/> instance.
		/// </param>
		/// <returns>
		/// A string containing the new variable name.
		/// </returns>
		private string RenderVariableName(AdaptiveTableProfile referencedProfile)
		{
			string? name = null;

			if (!string.IsNullOrEmpty(referencedProfile.FriendlyName))
			{
				name = referencedProfile.FriendlyName.Replace(" ", string.Empty);
			}
			else
			{
				name = referencedProfile.SingularName;
			}

			// Remove plural naming.
			if (name.EndsWith("s"))
				name = name.Substring(0, name.Length - 1);

			// Ensure the first character is lowerCase.
			name = name.Substring(0, 1).ToLower() + name.Substring(1, name.Length - 1);
			return name;
		}
		#endregion
	}
}