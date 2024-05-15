using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.SqlServer.Analysis;
using Adaptive.Intelligence.SqlServer.CodeDom;
using Adaptive.Intelligence.SqlServer.Schema;
using Microsoft.SqlServer.TransactSql.ScriptDom;

namespace Adaptive.Intelligence.SqlServer.ORM
{
    /// <summary>
    /// Provides methods and functions for generating SQL code DOM objects to represent specific statements.
    /// </summary>
    /// <seealso cref="DisposableObjectBase" />
    public sealed class AdaptiveSqlCodeDomGenerator : DisposableObjectBase
    {
        #region Private Member Declarations
        /// <summary>
        /// The meta data table.
        /// </summary>
        private AdaptiveTableMetadata? _metaData;
        #endregion

        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="AdaptiveSqlCodeDomGenerator"/> class.
        /// </summary>
        /// <param name="metaDataContainer">
        /// The reference to the <see cref="AdaptiveTableMetadata"/> table metadata container.
        /// </param>
        public AdaptiveSqlCodeDomGenerator(AdaptiveTableMetadata? metaDataContainer)
        {
            _metaData = metaDataContainer;
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
        /// <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed && disposing)
                _metaData?.Dispose();

            _metaData = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Methods / Functions

        #region CRUD Stored Procedure Generation Methods
        /// <summary>
        /// Creates the SQL Code DOM model for the Delete() stored procedure.
        /// </summary>
        /// <remarks>
        /// This is used to generate a stored procedure to either physically delete a record, or "soft" delete
        /// the record by marking the "Deleted" field as 1.
        /// </remarks>
        /// <param name="table">
        /// The <see cref="SqlTable"/> instance to create the stored procedure for.
        /// </param>
        /// <returns>
        /// A <see cref="SqlCodeCreateStoredProcedureStatement"/> containing the model for creating
        /// the GetAll stored procedure for the table.
        /// </returns>
        public SqlCodeCreateStoredProcedureStatement CreateDeleteStoredProcedure(SqlTable table, bool hardDelete)
        {
            // Find the profile for the table.
            AdaptiveTableProfile profile = _metaData[table.TableName];

            // Create the statement object.
            SqlCodeCreateStoredProcedureStatement storedProcedureStatement = new SqlCodeCreateStoredProcedureStatement(
                new SqlCodeDatabaseNameOwnerNameExpression(TSqlConstants.DefaultDatabaseOwner),
                profile.DeleteStoredProcedureName);

            // Add the ID parameter.
            // @Id  NVARCHAR(128)
            storedProcedureStatement.Parameters.Add(
                new SqlCodeParameterDefinitionExpression(TSqlConstants.StandardParameterId,
                    new SqlCodeDataTypeSpecificationExpression(SqlDataTypes.NVarCharOrSysName, 128, false)));

            if (!hardDelete)
            {
                // Add the UPDATE statement.  (Deleted = 1)
                SqlCodeUpdateStatement deleteStatement = GenerateSoftDeleteStatement(table);
                storedProcedureStatement.Statements.Add(deleteStatement);
            }
            else
            {
                SqlCodeDeleteStatement hardDeleteStatement = GenerateHardDeleteStatement(table);
                storedProcedureStatement.Statements.Add(hardDeleteStatement);
            }

            return storedProcedureStatement;
        }
        /// <summary>
        /// Creates the SQL Code DOM model for the GetAll() stored procedure.
        /// </summary>
        /// <remarks>
        /// This is used to generate the get-all-records (top 10000) for the table in the
        /// standard Adaptive CRUD operations.
        /// </remarks>
        /// <param name="procedureName">
        /// A string containing the stored procedure name.
        /// </param>
        /// <param name="table">
        /// The <see cref="SqlTable"/> instance to create the stored procedure for.
        /// </param>
        /// <returns>
        /// A <see cref="SqlCodeCreateStoredProcedureStatement"/> containing the model for creating
        /// the GetAll stored procedure for the table.
        /// </returns>
        public SqlCodeCreateStoredProcedureStatement CreateGetAllStoredProcedure(SqlTable table, string procedureName)
        {
            // Create the statement object.
            // CREATE PROCEDURE [dbo].[<tableName>GetById]
            // AS
            //   BEGIN
            SqlCodeCreateStoredProcedureStatement storedProcedureStatement = new SqlCodeCreateStoredProcedureStatement(
                new SqlCodeDatabaseNameOwnerNameExpression(TSqlConstants.DefaultDatabaseOwner), procedureName);

            // Add the SELECT statement.
            SqlCodeSelectStatement selectStatement = GenerateSelectAllStatement(table);
            if (storedProcedureStatement.Statements != null)
                storedProcedureStatement.Statements.Add(selectStatement);

            return storedProcedureStatement;
        }
        /// <summary>
        /// Creates the SQL Code DOM model for the GetById() stored procedure.
        /// </summary>
        /// <remarks>
        /// This is used to generate the get record by ID procedure for the table in the
        /// standard EasyVote CRUD operations.
        /// </remarks>
        /// <param name="table">
        /// The <see cref="SqlTable"/> instance to create the stored procedure for.
        /// </param>
        /// <returns>
        /// A <see cref="SqlCodeCreateStoredProcedureStatement"/> containing the model for creating
        /// the GetAll stored procedure for the table.
        /// </returns>
        public SqlCodeCreateStoredProcedureStatement CreateGetByIdStoredProcedure(SqlTable table)
        {
            // Find the profile for the table.
            AdaptiveTableProfile profile = _metaData[table.TableName];

            // Create the statement object.
            // CREATE PROCEDURE [dbo].[<tableName>GetById]
            SqlCodeCreateStoredProcedureStatement storedProcedureStatement = new SqlCodeCreateStoredProcedureStatement(
                new SqlCodeDatabaseNameOwnerNameExpression(TSqlConstants.DefaultDatabaseOwner),
                profile.GetByIdStoredProcedureName);

            // Add the ID parameter.
            // @Id NVARCHAR(128)
            storedProcedureStatement.Parameters.Add(
                new SqlCodeParameterDefinitionExpression(TSqlConstants.StandardParameterId,
                    new SqlCodeDataTypeSpecificationExpression(SqlDataTypes.NVarCharOrSysName, 128, false)));

            // Add the SELECT statement.
            SqlCodeSelectStatement selectStatement = GenerateSelectByIdStatement(table);
            storedProcedureStatement.Statements.Add(selectStatement);

            return storedProcedureStatement;
        }
        /// <summary>
        /// Creates the SQL Code DOM model for the Insert() stored procedure.
        /// </summary>
        /// <remarks>
        /// This is used to generate the insert into stored procedure for the table in the
        /// standard Adaptive CRUD operations.
        /// </remarks>
        /// <param name="table">
        /// The <see cref="SqlTable"/> instance to create the stored procedure for.
        /// </param>
        /// <returns>
        /// A <see cref="SqlCodeCreateStoredProcedureStatement"/> containing the model for creating
        /// the GetAll stored procedure for the table.
        /// </returns>
        public SqlCodeCreateStoredProcedureStatement CreateInsertStoredProcedure(SqlTable table)
        {

            // Find the profile for the table.
            AdaptiveTableProfile profile = _metaData[table.TableName]!;

            // Create the statement object.
            // CREATE PROCEDURE [dbo].[<tableName>Insert]
            SqlCodeCreateStoredProcedureStatement storedProcedureStatement = new SqlCodeCreateStoredProcedureStatement(
                new SqlCodeDatabaseNameOwnerNameExpression(TSqlConstants.DefaultDatabaseOwner),
                profile.InsertStoredProcedureName);

            // Add the parameters.
            //
            // Create a parameter for each of the columns that will be inserted.
            foreach (SqlQueryParameter queryParam in profile.QueryParameters)
            {
                // Ignore the ID, CreatedAt, UpdatedAt, and Deleted columns.
                if (queryParam.ColumnName != TSqlConstants.StandardColumnId && queryParam.ColumnName != TSqlConstants.StandardColumnCreatedAt &&
                    queryParam.ColumnName != TSqlConstants.StandardColumnUpdatedAt && queryParam.ColumnName != TSqlConstants.StandardColumnDeleted &&
                    queryParam.ColumnName != TSqlConstants.StandardColumnVersion)

                {
                    storedProcedureStatement.Parameters.Add(
                        new SqlCodeParameterDefinitionExpression(queryParam.ParameterName,
                            new SqlCodeDataTypeSpecificationExpression((SqlDataTypes)queryParam.TypeId, queryParam.MaxLength,
                                    queryParam.IsNullable, queryParam.Precision, queryParam.Scale, false)));
                }
            }

            // AS
            //   BEGIN
            //
            //      DECLARE @Id NVARCHAR(128) = NEWID()
            //
            SqlCodeVariableDeclarationStatement variable = new SqlCodeVariableDeclarationStatement(
                new SqlCodeVariableDefinitionExpression(TSqlConstants.StandardParameterId,
                    new SqlCodeDataTypeSpecificationExpression(SqlDataTypes.NVarCharOrSysName, 128, false)),
                new SqlCodeFunctionCallExpression(TSqlConstants.SqlSysFunctionNewId, null));

            storedProcedureStatement.Statements.Add(variable);
            storedProcedureStatement.Statements.Add(new SqlCodeLiteralStatement(string.Empty));

            // Add the INSERT statement.
            SqlCodeInsertStatement insertStatement = GenerateInsertStatement(table);
            storedProcedureStatement.Statements.Add(insertStatement);

            //
            //  SELECT @Id
            //
            // END

            SqlCodeSelectStatement selectStatement = new SqlCodeSelectStatement();
            if (selectStatement.SelectClause != null)
            {
                selectStatement.SelectClause.SelectItemsList.Add(
                    new SqlCodeSelectListItemExpression(
                        new SqlCodeVariableReferenceExpression(TSqlConstants.StandardParameterId)));
            }
            if (selectStatement.FromClause != null)
                selectStatement.FromClause.SourceTable = null;

            storedProcedureStatement.Statements.Add(selectStatement);

            return storedProcedureStatement;
        }
		/// <summary>
		/// Creates the SQL Code DOM model for the Update() stored procedure.
		/// </summary>
		/// <remarks>
		/// This is used to generate the update record by ID stored procedure for the table in the
		/// standard Adaptive CRUD operations.
		/// </remarks>
		/// <param name="table">
		/// The <see cref="SqlTable"/> instance to create the stored procedure for.
		/// </param>
        /// <returns>
		/// A <see cref="SqlCodeCreateStoredProcedureStatement"/> containing the model for creating
		/// the GetAll stored procedure for the table.
		/// </returns>
		public SqlCodeCreateStoredProcedureStatement CreateUpdateStoredProcedure(SqlTable table)
        {
            // Find the profile for the table.
            AdaptiveTableProfile profile = _metaData[table.TableName];

            // Create the statement object.
            // CREATE PROCEDURE [dbo].[<tableName>Update]
            SqlCodeCreateStoredProcedureStatement storedProcedureStatement = new SqlCodeCreateStoredProcedureStatement(
                new SqlCodeDatabaseNameOwnerNameExpression(TSqlConstants.DefaultDatabaseOwner),
                profile.UpdateStoredProcedureName);

            // Create a parameter for each of the columns that will be inserted.
            foreach (SqlQueryParameter queryParam in profile.QueryParameters)
            {
                // Ignore the CreatedAt, UpdatedAt, and Deleted columns.
                if (queryParam.ColumnName != TSqlConstants.StandardColumnCreatedAt &&
                    queryParam.ColumnName != TSqlConstants.StandardColumnUpdatedAt &&
                    queryParam.ColumnName != TSqlConstants.StandardColumnDeleted &&
                    !queryParam.IsVersion)
                {
                    storedProcedureStatement.Parameters.Add(
                        new SqlCodeParameterDefinitionExpression(queryParam.ParameterName,
                            new SqlCodeDataTypeSpecificationExpression((SqlDataTypes)queryParam.TypeId, queryParam.MaxLength,
                                    queryParam.IsNullable, queryParam.Precision, queryParam.Scale, false)));
                }
            }

            // AS
            //   BEGIN
            //      UPDATE [dbo].[TableName]
            //          SET
            storedProcedureStatement.Statements.Add(GenerateUpdateStatement(table));

            //
            // SELECT ... by ID statement.
            //
            //
            // END
            storedProcedureStatement.Statements.Add(GenerateSelectByIdStatement(table));
            return storedProcedureStatement;
        }
        #endregion

        #region SELECT, INSERT, UPDATE statement generation
        /// <summary>
        /// Generates the SQL Code DOM for the select all statement for a table.
        /// </summary>
        /// <param name="table">
        /// The <see cref="SqlTable"/> instance to create the select statement for.
        /// </param>
        /// <returns>
        /// A <see cref="SqlCodeSelectStatement"/> containing the model for creating
        /// the SELECT statement.
        /// </returns>
        public SqlCodeSelectStatement GenerateSelectAllStatement(SqlTable table)
        {
            // Find the profile for the table.
            AdaptiveTableProfile profile = _metaData[table.TableName];

            // Render the select Statement - since this is a select all query, limit the return to the TOP 10000 rows.
            SqlCodeSelectStatement selectStatement = new SqlCodeSelectStatement();
            selectStatement.SelectClause.TopValue = 10000;

            // Set the list of fields/columns to query for from the table itself.
            SetPrimaryTableItemsToQueryFor(selectStatement.SelectClause.SelectItemsList, profile, table);

            // For each of the tables to be joined on, add the list of fields/columns from each of the joined tables.
            if (profile.ReferencedTableJoins.Count > 0)
                SetJoinTableItemsToQueryFor(selectStatement.SelectClause.SelectItemsList, profile, table);

            // Add a separator line.
            selectStatement.SelectClause.SelectItemsList.AddExpression(new SqlCodeLiteralExpression(string.Empty));

            // Now Create the FROM Clause.
            SetFromClause(selectStatement.FromClause, profile);

            // And finally, the WHERE clause.
            //
            // WHERE
            //      [Deleted] = 0
            selectStatement.WhereClause.Conditions.Add(
                new SqlCodeConditionListExpression(
                    new SqlCodeConditionExpression(
                        new SqlCodeTableColumnReferenceExpression(table.TableName, TSqlConstants.StandardColumnDeleted),
                        new SqlCodeLiteralExpression(TSqlConstants.BitValueFalse),
                        SqlComparisonOperator.EqualTo),
                    SqlConditionOperator.NotSpecified));
            return selectStatement;
        }
        /// <summary>
        /// Generates the SQL Code DOM for the select record by ID statement for a table.
        /// </summary>
        /// <param name="table">
        /// The <see cref="SqlTable"/> instance to create the select statement for.
        /// </param>
        /// <returns>
        /// A <see cref="SqlCodeSelectStatement"/> containing the model for creating
        /// the SELECT statement.
        /// </returns>
        public SqlCodeSelectStatement GenerateSelectByIdStatement(SqlTable table)
        {
            // Find the profile for the table.
            AdaptiveTableProfile profile = _metaData[table.TableName];

            // Render the select Statement.
            SqlCodeSelectStatement selectStatement = new SqlCodeSelectStatement();

            // Set the list of fields/columns to query for from the table itself.
            SetPrimaryTableItemsToQueryFor(selectStatement.SelectClause.SelectItemsList, profile, table);

            // For each of the tables to be joined on, add the list of fields/columns from each of the joined tables.
            if (profile.ReferencedTableJoins.Count > 0)
                SetJoinTableItemsToQueryFor(selectStatement.SelectClause.SelectItemsList, profile, table);

            // Add a separator line.
            selectStatement.SelectClause.SelectItemsList.AddExpression(new SqlCodeLiteralExpression(string.Empty));

            // Now Create the FROM Clause.
            SetFromClause(selectStatement.FromClause, profile);

            // And finally, the WHERE clause.
            //
            // WHERE
            //      [Id] = @Id
            selectStatement.WhereClause.Conditions.Add(
                new SqlCodeConditionListExpression(
                    new SqlCodeConditionExpression(
                        new SqlCodeTableColumnReferenceExpression(table.TableName, TSqlConstants.StandardColumnId),
                        new SqlCodeParameterReferenceExpression(TSqlConstants.StandardParameterId),
                        SqlComparisonOperator.EqualTo),
                    SqlConditionOperator.NotSpecified));

            return selectStatement;
        }
        /// <summary>
        /// Generates the SQL Code DOM for the INSERT statement for a table.
        /// </summary>
        /// <param name="table">
        /// The <see cref="SqlTable"/> instance to create the select statement for.
        /// </param>
        /// <returns>
        /// A <see cref="SqlCodeInsertStatement"/> containing the model for creating
        /// the INSERT statement.
        /// </returns>
        public SqlCodeInsertStatement GenerateInsertStatement(SqlTable table)
        {
            // Get the profile.
            AdaptiveTableProfile profile = _metaData[table.TableName];

            // INSERT INTO [dbo].[TableName]
            SqlCodeInsertStatement insertStatement = new SqlCodeInsertStatement(
                new SqlCodeTableReferenceExpression(profile.TableName));

            //      ([Id],
            insertStatement.InsertColumnList.Add(new SqlCodeColumnNameExpression(TSqlConstants.StandardColumnId));

            // [column], [column], ...
            // Create a parameter for each of the columns that will be inserted.
            foreach (SqlQueryParameter queryParam in profile.QueryParameters)
            {
                // Ignore the ID, CreatedAt, UpdatedAt, and Deleted columns.
                if (queryParam.ColumnName != TSqlConstants.StandardColumnId && queryParam.ColumnName != TSqlConstants.StandardColumnCreatedAt &&
                    queryParam.ColumnName != TSqlConstants.StandardColumnUpdatedAt && queryParam.ColumnName != TSqlConstants.StandardColumnDeleted &&
                    queryParam.ColumnName != TSqlConstants.StandardColumnVersion)
                {
                    insertStatement.InsertColumnList.Add(new SqlCodeColumnNameExpression(queryParam.ColumnName));
                }
            }
            //      [Deleted])
            insertStatement.InsertColumnList.Add(new SqlCodeColumnNameExpression(TSqlConstants.StandardColumnDeleted));

            //  VALUES
            //      (@Id,
            insertStatement.ValuesList.Add(new SqlCodeVariableReferenceExpression(TSqlConstants.StandardParameterId));

            // @[column], @[column] ...
            foreach (SqlQueryParameter queryParam in profile.QueryParameters)
            {
                // Ignore the ID, CreatedAt, UpdatedAt, and Deleted columns.
                if (queryParam.ColumnName != TSqlConstants.StandardColumnId && queryParam.ColumnName != TSqlConstants.StandardColumnCreatedAt &&
                    queryParam.ColumnName != TSqlConstants.StandardColumnUpdatedAt && queryParam.ColumnName != TSqlConstants.StandardColumnDeleted &&
                    queryParam.ColumnName != TSqlConstants.StandardColumnVersion)
                {
                    insertStatement.ValuesList.Add(new SqlCodeParameterReferenceExpression(queryParam.ColumnName));
                }
            }
            //   ..., 0)
            insertStatement.ValuesList.Add(new SqlCodeLiteralExpression(TSqlConstants.BitValueFalse));
            return insertStatement;
        }
        /// <summary>
        /// Generates the SQL Code DOM for the UPDATE statement for a table.
        /// </summary>
        /// <param name="table">
        /// The <see cref="SqlTable"/> instance to create the select statement for.
        /// </param>
        /// <returns>
        /// A <see cref="SqlCodeInsertStatement"/> containing the model for creating
        /// the UPDATE statement.
        /// </returns>
        public SqlCodeUpdateStatement GenerateUpdateStatement(SqlTable table)
        {
            // Get the table profile.
            AdaptiveTableProfile profile = _metaData[table.TableName];

            // UPDATE [dbo].[TableName]
            //   SET
            SqlCodeUpdateStatement updateStatement = new SqlCodeUpdateStatement(
                new SqlCodeTableReferenceExpression(profile.TableName));

            // [column] = @parameter,
            // [column] = @parameter,
            // [column] = @parameter,
            // ...
            //
            // Create an assignment pair for each of the columns that will be updated.
            foreach (SqlQueryParameter queryParam in profile.QueryParameters)
            {
                // Ignore the ID, CreatedAt, UpdatedAt, and Deleted columns.
                if (queryParam.ColumnName != TSqlConstants.StandardColumnCreatedAt &&
                    queryParam.ColumnName != TSqlConstants.StandardColumnUpdatedAt &&
                    queryParam.ColumnName != TSqlConstants.StandardColumnDeleted &&
                    queryParam.ColumnName != TSqlConstants.StandardColumnId &&
                    !queryParam.IsVersion)
                {
                    updateStatement.UpdateColumnList.Add(
                        new SqlCodeColumnNameExpression(queryParam.ColumnName),
                        new SqlCodeParameterReferenceExpression(queryParam.ColumnName));
                }
            }
            //  [UpdatedAt] = SYSUTCDATETIME()
            updateStatement.UpdateColumnList.Add(
                new SqlCodeColumnNameExpression(TSqlConstants.StandardColumnUpdatedAt),
                new SqlCodeFunctionCallExpression("SYSUTCDATETIME", null));

            //  WHERE
            //      [Id] = @Id
            updateStatement.WhereClause = new SqlCodeWhereClause();
            updateStatement.WhereClause.Conditions.Add(
                new SqlCodeConditionListExpression(
                    new SqlCodeConditionExpression(
                        new SqlCodeTableColumnReferenceExpression(table.TableName, TSqlConstants.StandardColumnId),
                        new SqlCodeParameterReferenceExpression(TSqlConstants.StandardParameterId),
                        SqlComparisonOperator.EqualTo),
                    SqlConditionOperator.NotSpecified));

            return updateStatement;
        }
        /// <summary>
        /// Generates the SQL Code DOM for the UPDATE statement for a table used specifically
        /// to "soft" delete a record.
        /// </summary>
        /// <remarks>
        /// The rendered query works by simply marking the Deleted column as 1 (true).
        /// </remarks>
        /// <param name="table">
        /// The <see cref="SqlTable"/> instance to create the select statement for.
        /// </param>
        /// <returns>
        /// A <see cref="SqlCodeUpdateStatement"/> containing the model for creating
        /// the UPDATE statement for the delete operation.
        /// </returns>
        public SqlCodeUpdateStatement GenerateSoftDeleteStatement(SqlTable table)
        {
            // Get the table profile.
            AdaptiveTableProfile profile = _metaData[table.TableName];

            // UPDATE [dbo].[TableName]
            //   SET
            SqlCodeUpdateStatement updateStatement = new SqlCodeUpdateStatement(
                new SqlCodeTableReferenceExpression(profile.TableName));

            // [Deleted] = 1
            updateStatement.UpdateColumnList.Add(
                new SqlCodeColumnNameExpression(TSqlConstants.StandardColumnDeleted),
                new SqlCodeLiteralExpression(TSqlConstants.BitValueTrue));

            //  WHERE
            //      [Id] = @Id
            updateStatement.WhereClause = new SqlCodeWhereClause();
            updateStatement.WhereClause.Conditions.Add(
                new SqlCodeConditionListExpression(
                    new SqlCodeConditionExpression(
                        new SqlCodeTableColumnReferenceExpression(table.TableName, TSqlConstants.StandardColumnId),
                        new SqlCodeParameterReferenceExpression(TSqlConstants.StandardParameterId),
                        SqlComparisonOperator.EqualTo),
                    SqlConditionOperator.NotSpecified));

            return updateStatement;
        }
        /// <summary>
        /// Generates the SQL Code DOM for the DELETE statement for a table used specifically
        /// to "hard" delete a record.
        /// </summary>
        /// <remarks>
        /// The rendered query will physically delete the record.
        /// </remarks>
        /// <param name="table">
        /// The <see cref="SqlTable"/> instance to create the select statement for.
        /// </param>
        /// <returns>
        /// A <see cref="SqlCodeDeleteStatement"/> containing the model for creating
        /// the UPDATE statement for the delete operation.
        /// </returns>
        public SqlCodeDeleteStatement GenerateHardDeleteStatement(SqlTable table)
        {
            // DELETE
            SqlCodeDeleteStatement statement = new SqlCodeDeleteStatement();

            // FROM [dbo].[table]
            statement.FromClause.SourceTable = new SqlCodeTableReferenceExpression(table.TableName!);

            // WHERE
            //      [table].[Id] = @Id
            statement.WhereClause.Conditions.Add(
                new SqlCodeConditionListExpression(
                        new SqlCodeConditionExpression(
                        new SqlCodeTableColumnReferenceExpression(table.TableName!, TSqlConstants.StandardColumnId),
                        new SqlCodeVariableReferenceExpression("@Id"),
                        SqlComparisonOperator.EqualTo),
                        SqlConditionOperator.NotSpecified));

            return statement;
        }
        #endregion

        #endregion

        #region Private Methods / Functions
        private static void SetPrimaryTableItemsToQueryFor(SqlCodeSelectListItemExpressionCollection? itemsList, 
            AdaptiveTableProfile? profile, SqlTable? table)
        {
            if (profile != null && itemsList != null && table != null)
            {
                // If the profile has specified a list of fields to use when querying this table,
                // add this list to the SELECT clause.
                if (profile.StandardQueryFieldsToUse.Count == 0)
                {
                    // If the profile does not specify a list, add all columns in the table.
                    foreach (SqlColumn col in table.Columns)
                    {
                        // Always skip the Version column.
                        if (col.ColumnName != SqlParam.FieldNameVersion)
                            itemsList.AddExpression(table.TableName, col.ColumnName);
                    }
                }
                else
                {
                    // Use the profile-defined list.
                    int len = profile.StandardQueryFieldsToUse.Count;
                    for (int count = 0; count < len; count++)
                        itemsList.AddExpression(table.TableName, profile.StandardQueryFieldsToUse[count]);

                }
            }
        }
        private void SetJoinTableItemsToQueryFor(SqlCodeSelectListItemExpressionCollection itemsList, AdaptiveTableProfile profile, SqlTable table)
        {
            // For each table that is being joined to, generate a list of columns to query for.
            foreach (ReferencedTableJoin item in profile.ReferencedTableJoins)
            {
                SqlTable referencedTable = item.ReferencedTable;
                AdaptiveTableProfile referencedProfile = _metaData[referencedTable.TableName];

                // Add a new line and a general comment line for this section.
                itemsList.AddExpression(new SqlCodeLiteralExpression(string.Empty));
                itemsList.AddExpression(new SqlCodeCommentExpression(referencedProfile.FriendlyName));

                // If there is no profile, or no list of columns has been specified in the profile to use when
                // joining on this table, add all the columns from the table.
                if (referencedProfile.SubJoinQueryFieldsToUse.Count == 0)
                {
                    foreach (SqlColumn col in referencedTable.Columns)
                    {
                        if (col.ColumnName != TSqlConstants.StandardColumnVersion)
                            itemsList.AddExpression(referencedTable.TableName, col.ColumnName);
                    }
                }
                else
                {
                    // Otherwise, add the list of columns as specified in the profile for when joining
                    // to this table.
                    foreach (string column in referencedProfile.SubJoinQueryFieldsToUse)
                        itemsList.AddExpression(referencedTable.TableName, column);
                }
            }
        }
        private static void SetFromClause(SqlCodeFromClause fromClause, AdaptiveTableProfile profile)
        {
            // Set the source table object definition.  This will render as something like:
            // [dbo].[TableName] or [dbo].[TableName] [Alias] if an alias is specified.
            fromClause.SourceTable = new SqlCodeTableReferenceExpression(profile.TableName);

            // Now add the JOIN definitions, if present.
            if (profile.ReferencedTableJoins.Count > 0)
            {
                foreach (ReferencedTableJoin joinDefinition in profile.ReferencedTableJoins)
                {
                    SqlTable referencedTable = joinDefinition.ReferencedTable;
                    SqlCodeJoinClause joinClause = new SqlCodeJoinClause();

                    // If the referenced field is nullable, a LEFT JOIN must be used,
                    // otherwise, default to an INNER JOIN.
                    joinClause.IsLeftJoin = (referencedTable.Columns[joinDefinition.ReferencedTableField].IsNullable);
                    joinClause.ReferencedTable = new SqlCodeTableReferenceExpression("dbo", referencedTable.TableName, null);

                    // Create the objects for the ON clause.
                    joinClause.LeftColumn = new SqlCodeTableColumnReferenceExpression(profile.TableName, joinDefinition.KeyField);
                    joinClause.RightColumn = new SqlCodeTableColumnReferenceExpression(referencedTable.TableName, joinDefinition.ReferencedTableField);
                    joinClause.Operator = SqlComparisonOperator.EqualTo;

                    fromClause.Joins.Add(joinClause);
                }
            }
        }
        #endregion
    }
}