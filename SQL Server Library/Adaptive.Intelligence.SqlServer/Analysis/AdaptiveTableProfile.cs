using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.Shared.Logging;
using Adaptive.Intelligence.SqlServer.Schema;

namespace Adaptive.Intelligence.SqlServer.Analysis
{
    /// <summary>
    /// Provides and manages a user-defined profile for a specific SQL data table in the Easy Vote
    /// domain.
    /// </summary>
    /// <seealso cref="DisposableObjectBase" />
    /// <seealso cref="ITableProfile" />
    public sealed class AdaptiveTableProfile : DisposableObjectBase, ITableProfile
    {
        #region Private Member Declarations        
        /// <summary>
        /// The name for the data access class when generated for C#.
        /// </summary>
        private string? _dataAccessClassName = string.Empty;
        /// <summary>
        /// The name for the data definition class when generated for C#.
        /// </summary>
        private string? _dataDefinitionClassName = string.Empty;
        /// <summary>
        /// The description text.
        /// </summary>
        private string? _description = string.Empty;
        /// <summary>
        /// The friendly name.
        /// </summary>
        private string? _friendlyName = string.Empty;
        /// <summary>
        /// The name of the table as a plural object.
        /// </summary>
        private string? _pluralName = string.Empty;
        /// <summary>
        /// The qualified name.
        /// </summary>
        private string? _qualifiedName = string.Empty;
        /// <summary>
        /// The schema name
        /// </summary>
        private string? _schemaName = string.Empty;
        /// <summary>
        /// The name of the table as a singular object.
        /// </summary>
        private string? _singularName = string.Empty;
        /// <summary>
        /// The stored procedure name prefix value to use.
        /// </summary>
        private string? _storedProcedureNamePrefix = string.Empty;
        /// <summary>
        /// The stored procedure name suffix value to use.
        /// </summary>
        private string? _storedProcedureNameSuffix = string.Empty;
        private string? _subJoinObjectNamePrefix = string.Empty;
        private string? _subJoinObjectNameSuffix = string.Empty;
        /// <summary>
        /// The actual table name
        /// </summary>
        private string? _tableName = string.Empty;
        /// <summary>
        /// The name of the standard CRUD stored procedure to get all table records.
        /// </summary>
        private string? _getAllSpName;
        /// <summary>
        /// The name of the standard CRUD stored procedure to get a record by ID.
        /// </summary>
        private string? _getByIdSpName;
        /// <summary>
        /// The name of the standard CRUD stored procedure to insert a new record.
        /// </summary>
        private string? _insertSpName;
        /// <summary>
        /// The name of the standard CRUD stored procedure to update an existing record.
        /// </summary>
        private string? _updateSpName;
        /// <summary>
        /// The name of the standard CRUD stored procedure to delete a record.
        /// </summary>
        private string? _deleteSpName;

        /// <summary>
        /// The no sub joins flag.
        /// </summary>
        private bool _noSubJoins;

        /// <summary>
        /// The table object reference.
        /// </summary>
        private SqlTable? _tableReference;
        /// <summary>
        /// The crud stored procedure list.
        /// </summary>
        private SqlStoredProcedureCollection? _crudStoredProcedureList;
        /// <summary>
        /// The query parameters list.
        /// </summary>
        private SqlQueryParameterCollection? _queryParameters;
        /// <summary>
        /// The referenced table join object list.
        /// </summary>
        private ReferencedTableJoinCollection? _referencedTableJoins;
        /// <summary>
        /// The standard query fields to use.
        /// </summary>
        private List<string>? _standardQueryFieldsToUse;
        /// <summary>
        /// The key field names.
        /// </summary>
        private List<string>? _keyFieldNames;
        /// <summary>
        /// The sub join query fields to use.
        /// </summary>
        private List<string>? _subJoinQueryFieldsToUse;
        #endregion

        #region Constructor / Dispose Methods        
        /// <summary>
        /// Initializes a new instance of the <see cref="AdaptiveTableProfile"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public AdaptiveTableProfile()
        {
            _queryParameters = new SqlQueryParameterCollection();
            _referencedTableJoins = new ReferencedTableJoinCollection();
            _standardQueryFieldsToUse = new List<string>();
            _subJoinQueryFieldsToUse = new List<string>();
            _keyFieldNames = new List<string>();
            _crudStoredProcedureList = new SqlStoredProcedureCollection();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="AdaptiveTableProfile"/> class.
        /// </summary>
        /// <param name="sourceTable">
        /// A reference to the <see cref="SqlTable"/> instance to be profiled.
        /// </param>
        public AdaptiveTableProfile(SqlTable sourceTable)
        {
            _queryParameters = new SqlQueryParameterCollection();
            _referencedTableJoins = new ReferencedTableJoinCollection();
            _standardQueryFieldsToUse = new List<string>();
            _subJoinQueryFieldsToUse = new List<string>();
            _keyFieldNames = new List<string>();
            _crudStoredProcedureList = new SqlStoredProcedureCollection();
            _tableName = sourceTable.TableName;
            _tableReference = sourceTable;
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
                _queryParameters?.Clear();
                _referencedTableJoins?.Clear();
                _standardQueryFieldsToUse?.Clear();
                _subJoinQueryFieldsToUse?.Clear();
                _keyFieldNames?.Clear();
                _crudStoredProcedureList?.Clear();
            }

            _dataAccessClassName = null;
            _dataDefinitionClassName = null;
            _description = null;
            _friendlyName = null;
            _pluralName = null;
            _qualifiedName = null;
            _queryParameters = null;
            _referencedTableJoins = null;
            _crudStoredProcedureList = null;
            _singularName = null;
            _standardQueryFieldsToUse = null;
            _storedProcedureNamePrefix = null;
            _storedProcedureNameSuffix = null;
            _subJoinObjectNamePrefix = null;
            _subJoinObjectNameSuffix = null;
            _subJoinQueryFieldsToUse = null;
            _keyFieldNames = null;
            _tableName = null;
            _tableReference = null;
            _getAllSpName = null;
            _getByIdSpName = null;
            _insertSpName = null;
            _updateSpName = null;
            _deleteSpName = null;
            _schemaName = null;

            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties        
        /// <summary>
        /// Gets or sets the name of the data access class that may be generated for working with
        /// the related table.
        /// </summary>
        /// <value>
        /// A string specifying the name of the data access class.
        /// </value>
        public string? DataAccessClassName
        {
            get => _dataAccessClassName;
            set => _dataAccessClassName = value;
        }
        /// <summary>
        /// Gets or sets the name of the data definition class that may be generated to represent
        /// a record in the related table.
        /// </summary>
        /// <value>
        /// A string specifying the name of the data definition class.
        /// </value>
        public string? DataDefinitionClassName
        {
            get => _dataDefinitionClassName;
            set => _dataDefinitionClassName = value;
        }
        /// <summary>
        /// Gets or sets the description of the table.
        /// </summary>
        /// <value>
        /// A string containing a description of the table.
        /// </value>
        public string? Description
        {
            get => _description;
            set => _description = value;
        }
        /// <summary>
        /// Gets or sets the friendly or user-readable name for the table.
        /// </summary>
        /// <value>
        /// A string containing the friendly name for the table.
        /// </value>
        /// <remarks>
        /// This value may be used in generating SQL or code comments for the table.
        /// </remarks>
        public string? FriendlyName
        {
            get => _friendlyName;
            set => _friendlyName = value;
        }
        /// <summary>
        /// Gets the reference to the list of field names that denote foreign key fields.
        /// </summary>
        /// <value>
        /// A <see cref="List{T}"/> of <see cref="string"/> values that contain the field names.
        /// </value>
        public List<string>? KeyFieldNames => _keyFieldNames;
        /// <summary>
        /// Gets or sets a value indicating whether to create sub-joins to referenced tables.
        /// </summary>
        /// <value>
        ///   <b>true</b> to skip the generation of SQL joins to other tables; otherwise, <b>false</b>.
        /// </value>
        /// <remarks>
        /// This value is used to suppress automatic detection of, and generation of SQL joins to other
        /// possibly related tables.  If this value is <b>true</b>, generation of properties that relate to
        /// other data definition objects will also be suppressed.
        /// </remarks>
        public bool NoSubJoins
        {
            get => _noSubJoins;
            set => _noSubJoins = value;
        }
        /// <summary>
        /// Gets or sets the string value used to reference the table and its objects in plural form,
        /// e.g. "Customers" for a Customer or Customers table.
        /// </summary>
        /// <value>
        /// A string containing the plural name value.
        /// </value>
        public string? PluralName
        {
            get => _pluralName;
            set => _pluralName = value;
        }
        /// <summary>
        /// Gets or sets a value indicating whether this table references the Customers table.
        /// </summary>
        /// <value>
        ///   <b>true</b> if this table references the Customers table in a JOIN; otherwise, <b>false</b>.
        /// </value>
        public bool ReferencesCustomerTable { get; set; }
        /// <summary>
        /// Gets or sets the qualified name of the table.
        /// </summary>
        /// <value>
        /// A string containing the qualified name of the table.
        /// </value>
        /// <example>
        /// "[dbo].[SomeTableNameHere]"
        /// </example>
        public string? QualifiedName
        {
            get => _qualifiedName;
            set => _qualifiedName = value;
        }
        /// <summary>
        /// Gets the reference to the list of query parameters that may be defined for each column in the table.
        /// </summary>
        /// <value>
        /// A <see cref="SqlQueryParameterCollection" /> instance containing the query parameter definitions.
        /// </value>
        public SqlQueryParameterCollection QueryParameters
        {
            get
            {
                if (_queryParameters == null)
                {
                    _queryParameters = new SqlQueryParameterCollection();
                }

                return _queryParameters;
            }
        }
        /// <summary>
        /// Gets the reference to the list of join definitions to be used when rendering a SELECT query
        /// for this table.
        /// </summary>
        /// <value>
        /// A <see cref="ReferencedTableJoinCollection" /> containing the join definition instances used to define and help
        /// render SQL JOIN statements.
        /// </value>
        public ReferencedTableJoinCollection ReferencedTableJoins
        {
            get
            {
                if (_referencedTableJoins == null)
                {
                    _referencedTableJoins = new ReferencedTableJoinCollection();
                }

                return _referencedTableJoins;
            }
        }
        /// <summary>
        /// Gets or sets the name of the schema.
        /// </summary>
        /// <value>
        /// A string containing the name of the schema for the table, or <b>null</b>.
        /// </value>
        public string? SchemaName
        {
            get
            {
                if (string.IsNullOrEmpty(_schemaName))
                {
                    _schemaName = TSqlConstants.DefaultDatabaseOwner;
                }

                return _schemaName;
            }
            set => _schemaName = value;
        }
        /// <summary>
        /// Gets or sets the string value used to reference the table and its objects in singular form,
        /// e.g. "Customer" for a Customer or Customers table.
        /// </summary>
        /// <value>
        /// A string containing the singular name value.
        /// </value>
        public string? SingularName
        {
            get => _singularName;
            set => _singularName = value;
        }
        /// <summary>
        /// Gets or sets the reference to the list of standard query fields to use when rendering a SELECT stored
        /// procedure or query for this table.
        /// </summary>
        /// <value>
        /// A <see cref="List{T}" /> of <see cref="string" /> containing the names of the columns to include in
        /// the SELECT clause of the query.
        /// </value>
        /// <remarks>
        /// In many cases, not all columns need to be retrieved on general SELECT queries for a table.  This property
        /// allows the user to specify which columns are queried for when querying directly on this table.  This is different
        /// from the content in the <see cref="SubJoinQueryFieldsToUse" /> property.
        /// </remarks>
        public List<string> StandardQueryFieldsToUse
        {
            get
            {
                if (_standardQueryFieldsToUse == null)
                {
                    _standardQueryFieldsToUse = new List<string>();
                }

                return _standardQueryFieldsToUse;
            }
        }
        /// <summary>
        /// Gets the reference to the list of standard stored procedures that have been defined for the table.
        /// </summary>
        /// <value>
        /// A <see cref="SqlStoredProcedureCollection"/> instance containing the list.
        /// </value>
        public SqlStoredProcedureCollection? StandardStoredProcedures => _crudStoredProcedureList;
        /// <summary>
        /// Gets or sets the value to pre-pend to the name of a stored procedure when generated.
        /// </summary>
        /// <value>
        /// A string containing the value to pre-pend, or <see cref="string.Empty" />.
        /// </value>
        /// <remarks>
        /// The general naming convention for a generated stored procedure will utilize the table name
        /// and its function, e.g. "Customers" + INSERT = "CustomersInsert".  This property value will be
        /// pre-pended to the stored procedure name, such that a value of "sp_" will render "sp_CustomersInsert".
        /// This can be used in conjunction with the <see cref="StoredProcedureNameSuffix" /> property value.
        /// </remarks>
        public string? StoredProcedureNamePrefix
        {
            get => _storedProcedureNamePrefix;
            set => _storedProcedureNamePrefix = value;
        }
        /// <summary>
        /// Gets or sets the value to append to the name of a stored procedure when generated.
        /// </summary>
        /// <value>
        /// A string containing the value to append, or <see cref="string.Empty" />.
        /// </value>
        /// <remarks>
        /// The general naming convention for a generated stored procedure will utilize the table name
        /// and its function, e.g. "Customers" + INSERT = "CustomersInsert".  This property value will be
        /// appended to the stored procedure name, such that a value of "Procedure" will render "CustomersInsertProcedure".
        /// This can be used in conjunction with the <see cref="StoredProcedureNamePrefix" /> property value.
        /// </remarks>
        public string? StoredProcedureNameSuffix
        {
            get => _storedProcedureNameSuffix;
            set => _storedProcedureNameSuffix = value;
        }
        /// <summary>
        /// Gets or sets the sub-join data definition object name prefix.
        /// </summary>
        /// <value>
        /// A string containing the object prefix name value.
        /// </value>
        /// <remarks>
        /// When using joins in the stored procedures, the related data definition objects, when code generated,
        /// may be added to the main data definition.  This value is pre-pended to the property value name.  Thus,
        /// a value of "Related" to a "Customer" table join will render as "RelatedCustomer".
        /// This value can be used in conjunction with the <see cref="SubJoinObjectNameSuffix" /> property value.
        /// </remarks>
        public string? SubJoinObjectNamePrefix
        {
            get => _subJoinObjectNamePrefix;
            set => _subJoinObjectNamePrefix = value;
        }
        /// <summary>
        /// Gets or sets the sub-join data definition object name suffix.
        /// </summary>
        /// <value>
        /// A string containing the object prefix name value.
        /// </value>
        /// <remarks>
        /// When using joins in the stored procedures, the related data definition objects, when code generated,
        /// may be added to the main data definition.  This value is appended to the property value name.  Thus,
        /// a value of "Info" to a "Customer" table join will render as "CustomerInfo".
        /// This value can be used in conjunction with the <see cref="SubJoinObjectNamePrefix" /> property value.
        /// </remarks>
        public string? SubJoinObjectNameSuffix
        {
            get => _subJoinObjectNameSuffix;
            set => _subJoinObjectNameSuffix = value;
        }
        /// <summary>
        /// Gets or sets the reference to the list of sub-join query fields to use when rendering a SELECT stored
        /// procedure or query for a table that references this table.
        /// </summary>
        /// <value>
        /// A <see cref="List{T}" /> of <see cref="string" /> containing the names of the columns to include in
        /// the SELECT clause of the query.
        /// </value>
        /// <remarks>
        /// When a different table's SELECT query references this table, it may not be necessary to return the
        /// content of every column on the table.  This allows the user to both standardize, and limit the columns being
        /// queried for when this table participates as a child reference in a JOIN.  This is different from
        /// the content in the <see cref="StandardQueryFieldsToUse" /> property.
        /// </remarks>
        public List<string>? SubJoinQueryFieldsToUse => _subJoinQueryFieldsToUse;
        /// <summary>
        /// Gets or sets the name of the table being profiled.
        /// </summary>
        /// <value>
        /// A string containing the name of the table as it is defined in SQL Server.
        /// </value>
        public string? TableName
        {
            get => _tableName;
            set => _tableName = value;
        }
        /// <summary>
        /// Gets or sets the reference to the <see cref="SqlTable" /> instance being profiled.
        /// </summary>
        /// <value>
        /// The <see cref="SqlTable" /> instance.
        /// </value>
        public SqlTable? TableReference
        {
            get => _tableReference;
            set => _tableReference = value;
        }
        /// <summary>
        /// Gets or sets the name of the "get all" records stored procedure.
        /// </summary>
        /// <value>
        /// A string specifying the name of the get all records stored procedure.
        /// </value>
        public string? GetAllStoredProcedureName
        {
            get => _getAllSpName;
            set => _getAllSpName = value;
        }
        /// <summary>
        /// Gets or sets the name of the get by ID stored procedure.
        /// </summary>
        /// <value>
        /// A string specifying the name of the get by ID stored procedure.
        /// </value>
        public string? GetByIdStoredProcedureName
        {
            get => _getByIdSpName;
            set => _getByIdSpName = value;
        }
        /// <summary>
        /// Gets or sets the name of the create new (insert) stored procedure.
        /// </summary>
        /// <value>
        /// A string specifying the name of the create new (insert) stored procedure.
        /// </value>
        public string? InsertStoredProcedureName
        {
            get => _insertSpName;
            set => _insertSpName = value;
        }
        /// <summary>
        /// Gets or sets the name of the edit (update) stored procedure.
        /// </summary>
        /// <value>
        /// A string specifying the name of the edit (update) stored procedure.
        /// </value>
        public string? UpdateStoredProcedureName
        {
            get => _updateSpName;
            set => _updateSpName = value;
        }
        /// <summary>
        /// Gets or sets the name of the delete (or marked deleted) stored procedure.
        /// </summary>
        /// <value>
        /// A string specifying the name of the delete (or marked deleted) stored procedure.
        /// </value>
        public string? DeleteStoredProcedureName
        {
            get => _deleteSpName;
            set => _deleteSpName = value;
        }
        #endregion

        #region Public Methods / Functions        
        /// <summary>
        /// Creates the query parameter definitions for the columns on the table.
        /// </summary>
        public void CreateColumnParameters()
        {
            if (_tableReference != null && _tableReference.Columns != null)
            {
                if (_queryParameters == null)
                {
                    _queryParameters = new SqlQueryParameterCollection();
                }

                foreach (SqlColumn column in _tableReference.Columns)
                {
                    SqlQueryParameter queryParam = new SqlQueryParameter(column);
                    _queryParameters.Add(queryParam);

                }
            }
        }
        /// <summary>
        /// Attempts to read the contents of the instance from a local file.
        /// </summary>
        /// <param name="reader">The <see cref="BinaryReader" /> used to read the content.</param>
        /// <returns>
        ///   <b>true</b> if the operation is successful; otherwise, returns <b>false</b>.
        /// </returns>
        public bool Load(BinaryReader? reader)
        {
            bool success = false;

            if (reader != null && reader.BaseStream.CanRead)
            {
                if (_subJoinQueryFieldsToUse == null)
                {
                    _subJoinQueryFieldsToUse = new List<string>();
                }

                if (_standardQueryFieldsToUse == null)
                {
                    _standardQueryFieldsToUse = new List<string>();
                }

                try
                {
                    _dataAccessClassName = reader.ReadString();
                    _dataDefinitionClassName = reader.ReadString();
                    _description = reader.ReadString();
                    _friendlyName = reader.ReadString();
                    _noSubJoins = reader.ReadBoolean();
                    _pluralName = reader.ReadString();
                    _qualifiedName = reader.ReadString();
                    _singularName = reader.ReadString();
                    _storedProcedureNamePrefix = reader.ReadString();
                    _storedProcedureNameSuffix = reader.ReadString();
                    _subJoinObjectNamePrefix = reader.ReadString();
                    _subJoinObjectNameSuffix = reader.ReadString();
                    _tableName = reader.ReadString();
                    _getAllSpName = reader.ReadString();
                    _getByIdSpName = reader.ReadString();
                    _insertSpName = reader.ReadString();
                    _updateSpName = reader.ReadString();
                    _deleteSpName = reader.ReadString();

                    int len = reader.ReadInt32();
                    _subJoinQueryFieldsToUse.Clear();
                    for (int count = 0; count < len; count++)
                    {
                        _subJoinQueryFieldsToUse.Add(reader.ReadString());
                    }


                    len = reader.ReadInt32();
                    _standardQueryFieldsToUse.Clear();
                    for (int count = 0; count < len; count++)
                    {
                        _standardQueryFieldsToUse.Add(reader.ReadString());
                    }

                    success = true;
                }
                catch (Exception ex)
                {
                    ExceptionLog.LogException(ex);
                }
            }
            return success;

        }
        /// <summary>
        /// Attempts to save the contents of the instance to a local file.
        /// </summary>
        /// <param name="writer">The <see cref="BinaryWriter" /> used to write the content.</param>
        /// <returns>
        ///   <b>true</b> if the operation is successful; otherwise, returns <b>false</b>.
        /// </returns>
        public bool Save(BinaryWriter? writer)
        {
            bool success = false;

            if (writer != null && writer.BaseStream.CanWrite)
            {
                try
                {
                    writer.Write(_dataAccessClassName!);
                    writer.Write(_dataDefinitionClassName!);
                    writer.Write(_description!);
                    writer.Write(_friendlyName!);
                    writer.Write(_noSubJoins);
                    writer.Write(_pluralName!);
                    writer.Write(_qualifiedName!);
                    writer.Write(_singularName!);
                    writer.Write(_storedProcedureNamePrefix!);
                    writer.Write(_storedProcedureNameSuffix!);
                    writer.Write(_subJoinObjectNamePrefix!);
                    writer.Write(_subJoinObjectNameSuffix!);
                    writer.Write(_tableName!);
                    writer.Write(_getAllSpName != null ? _getAllSpName : string.Empty);
                    writer.Write(_getByIdSpName != null ? _getByIdSpName : string.Empty);
                    writer.Write(_insertSpName != null ? _insertSpName : string.Empty);
                    writer.Write(_updateSpName != null ? _updateSpName : string.Empty);
                    writer.Write(_deleteSpName != null ? _deleteSpName : string.Empty);

                    if (_subJoinQueryFieldsToUse == null)
                    {
                        writer.Write(0);
                    }
                    else
                    {
                        writer.Write(_subJoinQueryFieldsToUse.Count);
                        foreach (string item in _subJoinQueryFieldsToUse)
                        {
                            writer.Write(item);
                        }
                    }

                    if (_standardQueryFieldsToUse == null)
                    {
                        writer.Write(0);
                    }
                    else
                    {
                        writer.Write(_standardQueryFieldsToUse.Count);
                        foreach (string item in _standardQueryFieldsToUse)
                        {
                            writer.Write(item);
                        }
                    }
                    success = true;

                }
                catch (Exception ex)
                {
                    ExceptionLog.LogException(ex);
                }
            }
            return success;
        }
        #endregion
    }
}