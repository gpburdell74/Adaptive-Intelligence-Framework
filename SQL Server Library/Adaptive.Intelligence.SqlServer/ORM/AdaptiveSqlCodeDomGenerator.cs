// Ignore Spelling: Sql
// Ignore Spelling: ORM

using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.SqlServer.Analysis;
using Adaptive.Intelligence.SqlServer.CodeDom;
using Adaptive.Intelligence.SqlServer.Schema;

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
        /// The reference to the <see cref="AdaptiveTableMetadata"/> table meta data container.
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
        /// <param name="hardDelete">
        /// A value indicating whether to generate a hard-delete procedure.
        /// </param>
        /// <returns>
        /// A <see cref="SqlCodeCreateStoredProcedureStatement"/> containing the model for creating
        /// the GetAll stored procedure for the table.
        /// </returns>
        public SqlCodeCreateStoredProcedureStatement? CreateDeleteStoredProcedure(SqlTable table, bool hardDelete)
        {
            SqlCodeCreateStoredProcedureStatement? storedProcedureStatement = null;

            // Find the profile for the table.
            AdaptiveTableProfile? profile = FindProfile(table);
            if (profile != null)
            {
                // Create the statement object.
                storedProcedureStatement = new SqlCodeCreateStoredProcedureStatement(
                    new SqlCodeDatabaseNameOwnerNameExpression(table.Schema),
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
        /// <param name="table">
        /// The <see cref="SqlTable"/> instance to create the stored procedure for.
        /// </param>
        /// <returns>
        /// A <see cref="SqlCodeCreateStoredProcedureStatement"/> containing the model for creating
        /// the GetAll stored procedure for the table.
        /// </returns>
        public SqlCodeCreateStoredProcedureStatement? CreateGetAllStoredProcedure(SqlTable table)
        {
            SqlCodeCreateStoredProcedureStatement? storedProcedureStatement = null;

            // Find the profile for the table.
            AdaptiveTableProfile? profile = FindProfile(table);
            if (profile != null)
            {

                // Create the statement object.
                // CREATE PROCEDURE [<schema>].[<tableName>GetById]
                // AS
                //   BEGIN
                storedProcedureStatement = new SqlCodeCreateStoredProcedureStatement(
                            new SqlCodeDatabaseNameOwnerNameExpression(table.Schema),
                            profile.GetAllStoredProcedureName);

                // Add the SELECT statement.
                SqlCodeSelectStatement selectStatement = GenerateSelectAllStatement(table);
                if (storedProcedureStatement.Statements != null)
                    storedProcedureStatement.Statements.Add(selectStatement);
            }
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
        public SqlCodeCreateStoredProcedureStatement? CreateGetByIdStoredProcedure(SqlTable table)
        {
            SqlCodeCreateStoredProcedureStatement? storedProcedureStatement = null;

            // Find the profile for the table.
            AdaptiveTableProfile? profile = FindProfile(table);
            if (profile != null)
            {
                // Create the statement object.
                // CREATE PROCEDURE [<schema>].[<tableName>GetById]
                storedProcedureStatement = new SqlCodeCreateStoredProcedureStatement(
                    new SqlCodeDatabaseNameOwnerNameExpression(table.Schema),
                    profile.GetByIdStoredProcedureName);

                // Add the ID parameter.
                // @Id NVARCHAR(128)
                storedProcedureStatement.Parameters.Add(
                    new SqlCodeParameterDefinitionExpression(TSqlConstants.StandardParameterId,
                        new SqlCodeDataTypeSpecificationExpression(SqlDataTypes.NVarCharOrSysName, 128, false)));

                // Add the SELECT statement.
                SqlCodeSelectStatement selectStatement = GenerateSelectByIdStatement(table);
                storedProcedureStatement.Statements.Add(selectStatement);
            }
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
        public SqlCodeCreateStoredProcedureStatement? CreateInsertStoredProcedure(SqlTable table)
        {
            SqlCodeCreateStoredProcedureStatement? storedProcedureStatement = null;

            // Find the profile for the table.
            AdaptiveTableProfile? profile = FindProfile(table);

            if (profile != null)
            {
                // Create the statement object.
                // CREATE PROCEDURE [<schema>].[<tableName>Insert]
                storedProcedureStatement = new SqlCodeCreateStoredProcedureStatement(
                    new SqlCodeDatabaseNameOwnerNameExpression(table.Schema),
                    profile.InsertStoredProcedureName);

                // Add the parameters.
                //
                // Create a parameter for each of the columns that will be inserted.
                foreach (SqlQueryParameter queryParam in profile.QueryParameters)
                {
                    // Ignore the ID, CreatedAt, UpdatedAt, and Deleted columns.
                    if (queryParam.ColumnName != TSqlConstants.StandardColumnId &&
                        queryParam.ColumnName != TSqlConstants.StandardColumnDeleted)
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
            }
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
        public SqlCodeCreateStoredProcedureStatement? CreateUpdateStoredProcedure(SqlTable table)
        {
            SqlCodeCreateStoredProcedureStatement? storedProcedureStatement = null;

            // Find the profile for the table.
            AdaptiveTableProfile? profile = FindProfile(table);

            if (profile != null)
            {
                // Create the statement object.
                // CREATE PROCEDURE [<schema>].[<tableName>Update]
                storedProcedureStatement = new SqlCodeCreateStoredProcedureStatement(
                    new SqlCodeDatabaseNameOwnerNameExpression(table.Schema),
                    profile.UpdateStoredProcedureName);

                // Create a parameter for each of the columns that will be inserted.
                foreach (SqlQueryParameter queryParam in profile.QueryParameters)
                {
                    // Ignore the CreatedAt, UpdatedAt, and Deleted columns.
                    if (queryParam.ColumnName != TSqlConstants.StandardColumnDeleted &&
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
                //      UPDATE [<schema>].[TableName]
                //          SET
                var statement = GenerateUpdateStatement(table);
                if (statement != null)
                    storedProcedureStatement.Statements.Add(statement);

                //
                // SELECT ... by ID statement.
                //
                //
                // END
                storedProcedureStatement.Statements.Add(GenerateSelectByIdStatement(table));
            }

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
            SqlCodeSelectStatement selectStatement = new SqlCodeSelectStatement();

            // Find the profile for the table.
            AdaptiveTableProfile? profile = FindProfile(table);

            if (profile != null)
            {
                // Render the select Statement - since this is a select all query, limit the return to the TOP 10000 rows.
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
            }
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
            // Render the select Statement.
            SqlCodeSelectStatement selectStatement = new SqlCodeSelectStatement();

            // Find the profile for the table.
            AdaptiveTableProfile? profile = FindProfile(table);
            if (profile != null)
            {


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

            }
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
            // INSERT INTO [<schema>].[TableName]
            SqlCodeInsertStatement insertStatement = new SqlCodeInsertStatement();

            // Get the profile.
            AdaptiveTableProfile? profile = FindProfile(table);
            if (profile != null)
            {
                // INSERT INTO [<schema>].[TableName]
                insertStatement = new SqlCodeInsertStatement(
                    new SqlCodeTableReferenceExpression(profile.SchemaName, profile.TableName));

                //      ([Id],
                insertStatement.InsertColumnList.Add(new SqlCodeColumnNameExpression(TSqlConstants.StandardColumnId));

                // [column], [column], ...
                // Create a parameter for each of the columns that will be inserted.
                foreach (SqlQueryParameter queryParam in profile.QueryParameters)
                {
                    // Ignore the ID, CreatedAt, UpdatedAt, and Deleted columns.
                    if (queryParam.ColumnName != TSqlConstants.StandardColumnId &&
                        queryParam.ColumnName != TSqlConstants.StandardColumnDeleted)
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
                    if (queryParam.ColumnName != TSqlConstants.StandardColumnId &&
                        queryParam.ColumnName != TSqlConstants.StandardColumnDeleted)
                    {
                        insertStatement.ValuesList.Add(new SqlCodeParameterReferenceExpression(queryParam.ColumnName));
                    }
                }
                //   ..., 0)
                insertStatement.ValuesList.Add(new SqlCodeLiteralExpression(TSqlConstants.BitValueFalse));
            }
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
        public SqlCodeUpdateStatement? GenerateUpdateStatement(SqlTable table)
        {
            SqlCodeUpdateStatement? updateStatement = null;

            if (_metaData != null)
            {
                // Get the table profile.
                AdaptiveTableProfile? profile = _metaData[table.TableName];
                if (profile != null)
                {
                    // UPDATE [<schema>].[TableName]
                    //   SET
                    updateStatement = new SqlCodeUpdateStatement(
                        new SqlCodeTableReferenceExpression(table.Schema, table.TableName));

                    // [column] = @parameter,
                    // [column] = @parameter,
                    // [column] = @parameter,
                    // ...
                    //
                    // Create an assignment pair for each of the columns that will be updated.
                    foreach (SqlQueryParameter queryParam in profile.QueryParameters)
                    {
                        // Ignore the ID, CreatedAt, UpdatedAt, and Deleted columns.
                        if (queryParam.ColumnName != TSqlConstants.StandardColumnDeleted &&
                            queryParam.ColumnName != TSqlConstants.StandardColumnId &&
                            !queryParam.IsVersion)
                        {
                            updateStatement.UpdateColumnList.Add(
                                new SqlCodeColumnNameExpression(queryParam.ColumnName),
                                new SqlCodeParameterReferenceExpression(queryParam.ColumnName));
                        }
                    }

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

                }
            }
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
            SqlCodeUpdateStatement updateStatement = new SqlCodeUpdateStatement();

            // Get the table profile.
            AdaptiveTableProfile? profile = FindProfile(table);
            if (profile != null)
            {
                // UPDATE [dbo].[TableName]
                //   SET
                updateStatement = new SqlCodeUpdateStatement(
                   new SqlCodeTableReferenceExpression(profile.SchemaName, profile.TableName));

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
            }

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

            // FROM [<schema>].[table]
            statement.FromClause.SourceTable = new SqlCodeTableReferenceExpression(table.Schema, table.TableName!);

            // WHERE
            //      [table].[Id] = @Id
            statement.WhereClause.Conditions.Add(
                new SqlCodeConditionListExpression(
                        new SqlCodeConditionExpression(
                        new SqlCodeTableColumnReferenceExpression(table.TableName!, TSqlConstants.StandardColumnId),
                        new SqlCodeVariableReferenceExpression(TSqlConstants.SqlParamId),
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
            if (_metaData != null)
            {
                // For each table that is being joined to, generate a list of columns to query for.
                foreach (ReferencedTableJoin item in profile.ReferencedTableJoins)
                {
                    SqlTable? referencedTable = item.ReferencedTable;
                    if (referencedTable != null)
                    {
                        AdaptiveTableProfile? referencedProfile = _metaData[referencedTable.TableName];
                        if (referencedProfile != null)
                        {
                            // Add a new line and a general comment line for this section.
                            itemsList.AddExpression(new SqlCodeLiteralExpression(string.Empty));
                            itemsList.AddExpression(new SqlCodeCommentExpression(referencedProfile.FriendlyName));

                            // If there is no profile, or no list of columns has been specified in the profile to use when
                            // joining on this table, add all the columns from the table.
                            if (referencedProfile.SubJoinQueryFieldsToUse == null || referencedProfile.SubJoinQueryFieldsToUse.Count == 0)
                            {
                                foreach (SqlColumn col in referencedTable.Columns)
                                {
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
                }
            }
        }
        private static void SetFromClause(SqlCodeFromClause fromClause, AdaptiveTableProfile profile)
        {
            // Set the source table object definition.  This will render as something like:
            // [<schema>].[TableName] or [<schema>].[TableName] [Alias] if an alias is specified.
            fromClause.SourceTable = new SqlCodeTableReferenceExpression(profile.SchemaName, profile.TableName);

            // Now add the JOIN definitions, if present.
            if (profile.ReferencedTableJoins.Count > 0)
            {
                foreach (ReferencedTableJoin joinDefinition in profile.ReferencedTableJoins)
                {
                    SqlTable? referencedTable = joinDefinition.ReferencedTable;
                    if (referencedTable != null)
                    {
                        SqlCodeJoinClause joinClause = new SqlCodeJoinClause();

                        // If the referenced field is null able, a LEFT JOIN must be used,
                        // otherwise, default to an INNER JOIN.
                        joinClause.IsLeftJoin = referencedTable.IsColumnNullable(joinDefinition.ReferencedTableField);

                        joinClause.ReferencedTable = new SqlCodeTableReferenceExpression(
                            referencedTable.Schema, referencedTable.TableName, null);

                        // Create the objects for the ON clause.
                        joinClause.LeftColumn = new SqlCodeTableColumnReferenceExpression(profile.TableName, joinDefinition.KeyField!);
                        joinClause.RightColumn = new SqlCodeTableColumnReferenceExpression(referencedTable.TableName, joinDefinition.ReferencedTableField);
                        joinClause.Operator = SqlComparisonOperator.EqualTo;

                        fromClause.Joins?.Add(joinClause);
                    }
                }
            }
        }
        /// <summary>
        /// Finds the table profile for the specified table.
        /// </summary>
        /// <param name="table">
        /// The <see cref="SqlTable"/> instance to find the profile information for.
        /// </param>
        /// <returns>
        /// The <see cref="AdaptiveTableProfile"/> instance, or <b>null</b>.
        /// </returns>
        private AdaptiveTableProfile? FindProfile(SqlTable? table)
        {
            AdaptiveTableProfile? profile = null;

            // Find the profile for the table.
            if (_metaData != null && table != null)
                profile = _metaData[table.TableName];

            return profile;
        }
        #endregion
    }
}