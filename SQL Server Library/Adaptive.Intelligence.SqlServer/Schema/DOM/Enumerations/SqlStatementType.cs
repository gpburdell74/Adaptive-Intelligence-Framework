namespace Adaptive.Intelligence.SqlServer.CodeDom
{
    /// <summary>
    /// Lists the types of SQL statements that are currently supported by the code generator.
    /// </summary>
    public enum SqlStatementType
    {
        /// <summary>
        /// Indicates an empty statement.
        /// </summary>
        None = 0,
        /// <summary>
        /// Indicates a command statement / block.
        /// </summary>
        Comment = 1,
        /// <summary>
        /// Indicates a SELECT statement.
        /// </summary>
        Select = 2,
        /// <summary>
        /// Indicates an INSERT statement.
        /// </summary>
        Insert = 3,
        /// <summary>
        /// Indicates an UPDATE statement.
        /// </summary>
        Update = 4,
        /// <summary>
        /// Indicates a CREATE PROCEDURE statement.
        /// </summary>
        CreateStoredProcedure = 5,
        /// <summary>
        /// Indicates an ALTER PROCEDURE statement.
        /// </summary>
        AlterStoredProcedure = 6,
        /// <summary>
        /// Indicates a DROP PROCEDURE statement.
        /// </summary>
        DropStoredProcedure = 7,
        /// <summary>
        /// Indicates an assignment statement, such as SET x = y.
        /// </summary>
        Assignment = 8,
        /// <summary>
        /// Indicates a variable declaration statement, such as DECLARE @VarName NVARCHAR(32).
        /// </summary>
        VariableDeclaration = 9,
        /// <summary>
        /// Indicates a DELETE statement.
        /// </summary>
        Delete = 10,
        /// <summary>
        /// Indicates a literal string used as a SQL statement.
        /// </summary>
        Literal = 16,
    }
}