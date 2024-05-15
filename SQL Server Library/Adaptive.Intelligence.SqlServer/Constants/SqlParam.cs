namespace Adaptive.Intelligence.SqlServer
{
    /// <summary>
    /// Provides the definition for SQL Server parameter names.
    /// </summary>
    public static class SqlParam
    {
        /// <summary>
        /// The SQL parameter object identifier parameter name.
        /// </summary>
        public const string ObjectId = "@ObjectId";
        /// <summary>
        /// The SQL procedure name object identifier parameter name.
        /// </summary>
        public const string ProcedureName = "@ProcedureName";
        /// <summary>
        /// The SQL table name object identifier parameter name.
        /// </summary>
        public const string TableName = "@TableName";

        /// <summary>
        /// The "Version" field name string.
        /// </summary>
        public const string FieldNameVersion = "Version";

    }
}