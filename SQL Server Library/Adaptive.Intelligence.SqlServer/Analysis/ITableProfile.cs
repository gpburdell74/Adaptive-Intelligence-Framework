using Adaptive.Intelligence.SqlServer.Schema;

namespace Adaptive.Intelligence.SqlServer.Analysis
{
    /// <summary>
    /// Provides and manages a user-defined profile for a specific SQL data table in a business domain.
    /// </summary>
    /// <remarks>
    /// This interface may be implemented in a domain-specific class to provide information for a specific
    /// business domain data table.
    /// </remarks>
    public interface ITableProfile
    {
        #region Properties
        /// <summary>
        /// Gets or sets the name of the data access class that may be generated for working with
        /// the related table.
        /// </summary>
        /// <value>
        /// A string specifying the name of the data access class.
        /// </value>
        string? DataAccessClassName { get; set; }
        /// <summary>
        /// Gets or sets the name of the data definition class that may be generated to represent 
        /// a record in the related table.
        /// </summary>
        /// <value>
        /// A string specifying the name of the data definition class.
        /// </value>
        string? DataDefinitionClassName { get; set; }
        /// <summary>
        /// Gets or sets the description of the table.
        /// </summary>
        /// <value>
        /// A string containing a description of the table.
        /// </value>
        string? Description { get; set; }
        /// <summary>
        /// Gets or sets the friendly or user-readable name for the table.
        /// </summary>
        /// <remarks>
        /// This value may be used in generating SQL or code comments for the table.
        /// </remarks>
        /// <value>
        /// A string containing the friendly name for the table.
        /// </value>
        string? FriendlyName { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether to create sub-joins to referenced tables.
        /// </summary>
        /// <remarks>
        /// This value is used to suppress automatic detection of, and generation of SQL joins to other
        /// possibly related tables.  If this value is <b>true</b>, generation of properties that relate to
        /// other data definition objects will also be suppressed.
        /// </remarks>
        /// <value>
        ///   <b>true</b> to skip the generation of SQL joins to other tables; otherwise, <b>false</b>.
        /// </value>
        bool NoSubJoins { get; set; }
        /// <summary>
        /// Gets or sets the string value used to reference the table and its objects in plural form,
        /// e.g. "Customers" for a Customer or Customers table.
        /// </summary>
        /// <value>
        /// A string containing the plural name value.
        /// </value>
        string? PluralName { get; set; }
        /// <summary>
        /// Gets or sets the qualified name of the table.
        /// </summary>
        /// <example>
        /// "[dbo].[SomeTableNameHere]"
        /// </example>
        /// <value>
        /// A string containing the qualified name of the table.
        /// </value>
        string? QualifiedName { get; set; }
        /// <summary>
        /// Gets the reference to the list of query parameters that may be defined for each column in the table.
        /// </summary>
        /// <value>
        /// A <see cref="SqlQueryParameterCollection"/> instance containing the query parameter definitions.
        /// </value>
        SqlQueryParameterCollection? QueryParameters { get; }
        /// <summary>
        /// Gets the reference to the list of join definitions to be used when rendering a SELECT query
        /// for this table.
        /// </summary>
        /// <value>
        /// A <see cref="ReferencedTableJoinCollection"/> containing the join definition instances used to define and help 
        /// render SQL JOIN statements.
        /// </value>
        ReferencedTableJoinCollection? ReferencedTableJoins { get; }
        /// <summary>
        /// Gets or sets the string value used to reference the table and its objects in singular form,
        /// e.g. "Customer" for a Customer or Customers table.
        /// </summary>
        /// <value>
        /// A string containing the singular name value.
        /// </value>
        string? SingularName { get; set; }
        /// <summary>
        /// Gets or sets the reference to the list of standard query fields to use when rendering a SELECT stored
        /// procedure or query for this table.
        /// </summary>
        /// <remarks>
        /// In many cases, not all columns need to be retrieved on general SELECT queries for a table.  This property
        /// allows the user to specify which columns are queried for when querying directly on this table.  This is different
        /// from the content in the <see cref="SubJoinQueryFieldsToUse"/> property.
        /// </remarks>
        /// <value>
        /// A <see cref="List{T}"/> of <see cref="string"/> containing the names of the columns to include in
        /// the SELECT clause of the query.
        /// </value>
        List<string>? StandardQueryFieldsToUse { get; }
        /// <summary>
        /// Gets or sets the value to pre-pend to the name of a stored procedure when generated.
        /// </summary>
        /// <remarks>
        /// The general naming convention for a generated stored procedure will utilize the table name 
        /// and its function, e.g. "Customers" + INSERT = "CustomersInsert".  This property value will be 
        /// pre-pended to the stored procedure name, such that a value of "sp_" will render "sp_CustomersInsert".
        /// 
        /// This can be used in conjunction with the <see cref="StoredProcedureNameSuffix"/> property value.
        /// </remarks>
        /// <value>
        /// A string containing the value to pre-pend, or <see cref="string.Empty"/>.
        /// </value>
        string? StoredProcedureNamePrefix { get; set; }
        /// <summary>
        /// Gets or sets the value to append to the name of a stored procedure when generated.
        /// </summary>
        /// <remarks>
        /// The general naming convention for a generated stored procedure will utilize the table name 
        /// and its function, e.g. "Customers" + INSERT = "CustomersInsert".  This property value will be 
        /// appended to the stored procedure name, such that a value of "Procedure" will render "CustomersInsertProcedure".
        /// 
        /// This can be used in conjunction with the <see cref="StoredProcedureNamePrefix"/> property value.
        /// </remarks>
        /// <value>
        /// A string containing the value to append, or <see cref="string.Empty"/>.
        /// </value>
        string? StoredProcedureNameSuffix { get; set; }
        /// <summary>
        /// Gets or sets the sub-join data definition object name prefix.
        /// </summary>
        /// <remarks>
        /// When using joins in the stored procedures, the related data definition objects, when code generated,
        /// may be added to the main data definition.  This value is pre-pended to the property value name.  Thus,
        /// a value of "Related" to a "Customer" table join will render as "RelatedCustomer".
        /// 
        /// This value can be used in conjunction with the <see cref="SubJoinObjectNameSuffix"/> property value.
        /// </remarks>
        /// <value>
        /// A string containing the object prefix name value.
        /// </value>
        string? SubJoinObjectNamePrefix { get; set; }
        /// <summary>
        /// Gets or sets the sub-join data definition object name suffix.
        /// </summary>
        /// <remarks>
        /// When using joins in the stored procedures, the related data definition objects, when code generated,
        /// may be added to the main data definition.  This value is appended to the property value name.  Thus,
        /// a value of "Info" to a "Customer" table join will render as "CustomerInfo".
        /// 
        /// This value can be used in conjunction with the <see cref="SubJoinObjectNamePrefix"/> property value.
        /// </remarks>
        /// <value>
        /// A string containing the object prefix name value.
        /// </value>
        string? SubJoinObjectNameSuffix { get; set; }
        /// <summary>
        /// Gets the reference to the list of sub-join query fields to use when rendering a SELECT stored
        /// procedure or query for a table that references this table.
        /// </summary>
        /// <remarks>
        /// When a different table's SELECT query references this table, it may not be necessary to return the 
        /// content of every column on the table.  This allows the user to both standardize, and limit the columns being
        /// queried for when this table participates as a child reference in a JOIN.  This is different from 
        /// from the content in the <see cref="StandardQueryFieldsToUse"/> property.
        /// </remarks>
        /// <value>
        /// A <see cref="List{T}"/> of <see cref="string"/> containing the names of the columns to include in
        /// the SELECT clause of the query.
        /// </value>
        List<string>? SubJoinQueryFieldsToUse { get; }
        /// <summary>
        /// Gets or sets the name of the table being profiled.
        /// </summary>
        /// <value>
        /// A string containing the name of the table as it is defined in SQL Server.
        /// </value>
        string? TableName { get; set; }
        /// <summary>
        /// Gets or sets the reference to the <see cref="SqlTable"/> instance being profiled.
        /// </summary>
        /// <value>
        /// The <see cref="SqlTable"/> instance.
        /// </value>
        SqlTable? TableReference { get; set; }
        #endregion

        #region Methods / Functions        
        /// <summary>
        /// Attempts to read the contents of the instance from a local file.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="BinaryReader"/> used to read the content.
        /// </param>
        /// <returns>
        /// <b>true</b> if the operation is successful; otherwise, returns <b>false</b>.
        /// </returns>
        bool Load(BinaryReader reader);
        /// <summary>
        /// Attempts to save the contents of the instance to a local file.
        /// </summary>
        /// <param name="writer">
        /// The <see cref="BinaryWriter"/> used to write the content.
        /// </param>
        /// <returns>
        /// <b>true</b> if the operation is successful; otherwise, returns <b>false</b>.
        /// </returns>
        bool Save(BinaryWriter writer);
        #endregion
    }
}
