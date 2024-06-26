// Ignore Spelling: Sql
// Ignore Spelling: ORM

using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.Shared.Code;
using Adaptive.Intelligence.SqlServer.Analysis;
using Adaptive.Intelligence.SqlServer.Properties;
using Adaptive.Intelligence.SqlServer.Schema;

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
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
        /// <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed && disposing)
                _writer?.Dispose();

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
        /// <returns>
        /// A string containing the C# code file contents.
        /// </returns>
        public string GenerateDataAccessClass()
        {
            if (_profile != null && _writer != null)
            {
                // Short cuts.
                string? dataAccessClassName = _profile.DataAccessClassName;
                string? dataDefinitionClassName = _profile.DataDefinitionClassName;

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

                // Region For Public Methods / Functions
                _writer.WriteLine(string.Format(CodeConstants.CsRegionStart, "Public Methods / Functions"));
                _writer.WriteLine();
                _writer.WriteLine(CodeConstants.CsRegionEnd);
                _writer.WriteLine();

                // Region For Private Methods / Functions
                _writer.WriteLine(string.Format(CodeConstants.CsRegionStart, "Private Methods / Functions"));
                _writer.WriteLine();
                _writer.WriteLine(CodeConstants.CsRegionEnd);
                _writer.WriteLine();

                // End Class
                _writer.WriteBlockEnd();

                // End Namespace
                _writer.WriteBlockEnd();

                return _writer.ToString();
            }
            else
                return string.Empty;

        }
        #endregion

        #region Private Methods / Functions        
        /// <summary>
        /// Generates the standard using(s) list.
        /// </summary>
        private void GenerateUsings()
        {
            if (_writer != null)
            {
                // Write the standard using(s) list.
                _writer.WriteUsing(Resources.UsingSystemCollectionsGeneric);
                _writer.WriteUsing(Resources.UsingSystemDataClient);
                _writer.WriteUsing(Resources.UsingSystemThreadingTasks);
				_writer.WriteLine();
            }
        }
        /// <summary>
        /// Generates the start of the namespace block.
        /// </summary>
        private void GenerateNamespaceStart()
        {
            if (_writer!= null && _standardNameSpace != null)
                _writer.WriteNamespaceStart(_standardNameSpace);
        }
        /// <summary>
        /// Generates the class declaration start block.
        /// </summary>
        private void GenerateClassStart()
        {
            if (_writer != null)
            {
                if (_table != null && _profile != null)
                {
                    _writer.WriteXmlSummary("Provides the data access methods for the records in the " + _table.TableName + " table.");
                    _writer.WriteXmlSeeAlso(_profile.DataDefinitionClassName);

                    // Write the Class Declaration
                    _profile.DataAccessClassName = _profile.DataAccessClassName.Replace("sDataAccess", "DataAccess");
                    _writer.WriteLine("public sealed class " + _profile.DataAccessClassName + " : DataAccessBase<" + _profile.DataDefinitionClassName + ">");
                    _writer.WriteBlockStart();
                }
            }
        }
        /// <summary>
        /// Generates the private constants section.
        /// </summary>
        private void GeneratePrivateConstants()
        {
            if (_writer != null)
            {
                _writer.WriteLine("#region Private Constants");

                // Write the Read SP Names.
                _writer.WriteXmlSummary("The stored procedure to retrieve all the records in the table.");
                _writer.WriteLine("private const string SqlGetAll = \"" + _table.TableName + "GetAll\";");

                _writer.WriteXmlSummary("The stored procedure to retrieve a single record by ID value.");
                _writer.WriteLine("private const string SqlGetById = \"" + _table.TableName + "GetById\";");

                // Write the other CRUD SP Names.
                _writer.WriteXmlSummary("The stored procedure to delete a record.");
                _writer.WriteLine("private const string SqlDelete = \"" + _table.TableName + "Delete\";");

                _writer.WriteXmlSummary("The stored procedure to insert a record.");
                _writer.WriteLine("private const string SqlInsert = \"" + _table.TableName + "Insert\";");

                _writer.WriteXmlSummary("The stored procedure to update a record.");
                _writer.WriteLine("private const string SqlUpdate = \"" + _table.TableName + "Update\";");
                _writer.WriteLine();

                // Write the SP Parameter Definitions.
                //
                //private const string SqlParamCenterName = "@CenterName";
                foreach (SqlColumn col in _table.Columns)
                {
                    if (col.ColumnName != "Id" && col.ColumnName != "UpdatedAt" && col.ColumnName != "CreatedAt" &&
                        col.ColumnName != "Deleted" && col.ColumnName != "Version")
                    {
                        _writer.WriteLine("private const string SqlParam" + col.ColumnName + " = \"@" + col.ColumnName + "\";");
                    }
                }

                _writer.WriteLine(CodeConstants.CsRegionEnd);
                _writer.WriteLine();
            }
        }
        /// <summary>
        /// Generates the constructor definitions.
        /// </summary>
        private void GenerateConstructors()
        {
            _writer.WriteLine("#region Constructor");

            // Standard Parameterless Constructor.
            //
            // public CustomerDataAccess() : base(SqlGetAll, SqlGetById,
            //      SqlDelete, SqlInsert, SqlUpdate)

            _writer.WriteXmlSummary("Initializes a new instance of the <see cref=\"" + _profile.DataAccessClassName + "\"/> class.");
            _writer.WriteXmlRemarks("This is the default constructor.");
            _writer.WriteLine("public " + _profile.DataAccessClassName + "() : base(SqlGetAll, SqlGetById, ");
            _writer.Indent();
            _writer.WriteLine("SqlDelete, SqlInsert, SqlUpdate)");
            _writer.UnIndent();
            _writer.WriteLine(CodeConstants.CsBlockStart);
            _writer.WriteLine(CodeConstants.CsBlockEnd);

            // Constructor with SqlDataProvider parameter.
            _writer.WriteXmlSummary("Initializes a new instance of the <see cref=\"" + _profile.DataAccessClassName + "\"/> class.");
            _writer.WriteXmlParameter("provider", "A reference to the <see cref=\"SqlDataProvider\"/> instance to use instead of locally creating one.");
            _writer.WriteLine("public " + _profile.DataAccessClassName + "(SqlDataProvider provider) : base(SqlGetAll, SqlGetById, ");
            _writer.Indent();
            _writer.WriteLine("SqlDelete, SqlInsert, SqlUpdate, provider)");
            _writer.UnIndent();
            _writer.WriteLine(CodeConstants.CsBlockStart);
            _writer.WriteLine(CodeConstants.CsBlockEnd);

            _writer.WriteLine(CodeConstants.CsRegionEnd);
            _writer.WriteLine();
        }
        /// <summary>
        /// Generates the protected methods.
        /// </summary>
        private void GenerateProtectedMethods()
        {
            _writer.WriteLine(string.Format(CodeConstants.CsRegionStart, "Protected Method Overrides"));

            GenerateCreateInsertParameters();
            GenerateCreateUpdateParameters();
            GeneratePopulateRecord();

            _writer.WriteLine(CodeConstants.CsRegionEnd);
            _writer.WriteLine();
        }
        /// <summary>
        /// Generates the create insert parameters method.
        /// </summary>
        private void GenerateCreateInsertParameters()
        {
            _writer.WriteXmlSummary("Creates the array of parameters to use when inserting a new record.");
            _writer.WriteXmlParameter("instance", "The <see cref=\"" + _profile.DataDefinitionClassName + "\"/> instance being inserted.");
            _writer.WriteXmlReturns("An array of <see cref=\"SqlParameter\"/> instances to be supplied to the stored procedure.");
            _writer.WriteLine("protected override SqlParameter[] CreateInsertParameters(" + _profile.DataDefinitionClassName + " instance)");
            _writer.WriteBlockStart();

            _writer.WriteLine("return new SqlParameter[]");
            _writer.WriteBlockStart();

            // If the profile does not specify a list, add all columns in the table.
            foreach (SqlColumn col in _table.Columns)
            {
                // Always skip the Version column.
                if (col.ColumnName != TSqlConstants.StandardColumnDeleted &&
                    col.ColumnName != TSqlConstants.StandardColumnId)
                {
                    // CreateParameter(SqlParamParentId, instance.ParentId),
                    _writer.WriteLine("CreateParameter(SqlParam" + col.ColumnName + ", instance." + col.ColumnName + "), ");
                }
            }
            _writer.WriteBlockEnd(true);
            _writer.WriteBlockEnd();
        }
        /// <summary>
        /// Generates the create update parameters method.
        /// </summary>
        private void GenerateCreateUpdateParameters()
        {
            _writer.WriteXmlSummary("Creates the array of parameters to use when updating a record.");
            _writer.WriteXmlParameter("instance", "The <see cref=\"" + _profile.DataDefinitionClassName + "\"/> instance being updated.");
            _writer.WriteXmlReturns("An array of <see cref=\"SqlParameter\"/> instances to be supplied to the stored procedure.");
            _writer.WriteLine("protected override SqlParameter[] CreateUpdateParameters(" + _profile.DataDefinitionClassName + " instance)");
            _writer.WriteBlockStart();

            _writer.WriteLine("return new SqlParameter[]");
            _writer.WriteBlockStart();

            _writer.WriteLine("CreateParameter(SqlParamId, instance.Id),");
            foreach (SqlColumn col in _table.Columns)
            {
                // Always skip the Version column.
                if (col.ColumnName != TSqlConstants.StandardColumnDeleted &&
                    col.ColumnName != TSqlConstants.StandardColumnId)
                {
                    // CreateParameter(SqlParamParentId, instance.ParentId),
                    _writer.WriteLine("CreateParameter(SqlParam" + col.ColumnName + ", instance." + col.ColumnName + "), ");
                }
            }
            _writer.WriteBlockEnd(true);
            _writer.WriteBlockEnd();
        }
        /// <summary>
        /// Generates the populate record method.
        /// </summary>
        private void GeneratePopulateRecord()
        {
            _writer.WriteXmlSummary("Populates the provided instance from the data source.");
            _writer.WriteXmlParameter("reader", "The <see cref=\"SafeSqlDataReader\"/> instance used to read the data.");
            _writer.WriteXmlParameter("instance", "The <see cref=\"" + _profile.DataDefinitionClassName + "\"/> instance to be populated.");
            _writer.WriteLine("protected override void PopulateRecord(SafeSqlDataReader reader, " + _profile.DataDefinitionClassName + " instance)");
            _writer.WriteBlockStart();

            //int index = 0;
            _writer.WriteLine("int index = 0;");
            _writer.WriteLine();

            // If the profile has specified a list of fields to use when querying this table,
            // add this list to the SELECT clause.
            if (_profile.StandardQueryFieldsToUse.Count == 0)
            {
                // If the profile does not specify a list, add all columns in the table.
                foreach (SqlColumn col in _table.Columns)
                {
                    // Always skip the Version column.
                    if (col.ColumnName != "Version")
                    {
                        //instance.Id = reader.GetString(index++);
                        string colName = col.ColumnName;
                        if (colName.StartsWith("Key"))
                            colName = colName.Substring(3, colName.Length - 3);
                        _writer.WriteLine("instance." + colName + " = reader." + GetMethodName(col.TypeId) + "(index++);");
                    }
                }
            }
            else
            {
                // Use the profile-defined list.
                int len = _profile.StandardQueryFieldsToUse.Count;
                for (int count = 0; count < len; count++)
                {
                    //instance.Id = reader.GetString(index++);
                    SqlColumn col = _table.Columns[_profile.StandardQueryFieldsToUse[count]];
                    string colName = col.ColumnName;
                    if (colName.StartsWith("Key"))
                        colName = colName.Substring(3, colName.Length - 3);
                    _writer.WriteLine("instance." + col.ColumnName + " = reader." + GetMethodName(col.TypeId) + "(index++);");

                }
            }

            // For each table that is being joined to, generate a list of columns to query for.
            foreach (ReferencedTableJoin item in _profile.ReferencedTableJoins)
            {
                SqlTable referencedTable = item.ReferencedTable;
                AdaptiveTableProfile referencedProfile = _db.GetTableProfile(referencedTable.Schema, referencedTable.TableName);
                _writer.WriteLine();
                string variableName = RenderVariableName(referencedProfile);
                _writer.WriteLine("// " + referencedProfile.FriendlyName);
                string varDeclaration =
                    referencedProfile.DataDefinitionClassName + " " + variableName + " = new " +
                        referencedProfile.DataDefinitionClassName + "();";
                varDeclaration = varDeclaration.Replace("sRecord", "Record");
                _writer.WriteLine(varDeclaration);

                // If there is no profile, or no list of columns has been specified in the profile to use when 
                // joining on this table, add all the columns from the table.
                if (referencedProfile == null || referencedProfile.SubJoinQueryFieldsToUse.Count == 0)
                {
                    foreach (SqlColumn col in referencedTable.Columns)
                    {
                        string name = col.ColumnName;
                        //instance.Id = reader.GetString(index++);
                        //
                        // Remove the "Key" prefix for 4.5 property names.
                        if (name.StartsWith("Key"))
                            name = name.Substring(3, name.Length - 3);
                        _writer.WriteLine(variableName + "." + name + " = reader." + GetMethodName(col.TypeId) + "(index++);");
                    }
                }
                else
                {
                    // Otherwise, add the list of columns as specified in the profile for when joining
                    // to this table.
                    foreach (string column in referencedProfile.SubJoinQueryFieldsToUse)
                    {
                        //instance.Id = reader.GetString(index++);
                        SqlColumn col = referencedTable.Columns[column];
                        _writer.WriteLine(variableName + "." + column + " = reader." + GetMethodName(col.TypeId) + "(index++);");
                    }
                }
                _writer.WriteLine("instance." + item.ReferencedTable + " = " + variableName + ";");
            }

            _writer.WriteBlockEnd();
        }

        private string GetMethodName(int dataTypeId)
        {
			Adaptive.Intelligence.SqlServer.Schema.SqlDataType type = _db.Database.DataTypes.GetTypeById(dataTypeId);
            string name = type.GetDotNetType().Name.Replace("System.", string.Empty);
            return "Get" + name;
        }
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