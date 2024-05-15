using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.SqlServer.Schema
{
    /// <summary>
    /// Represents a foreign key column reference.
    /// </summary>
    public sealed class SqlForeignKeyColumn : DisposableObjectBase
    {
        #region Private Constants
        private const string FieldConstraintObjectId = "constraintObjectId";
        private const string FieldConstraintColumnId = "constraintColumnId";
        private const string FieldParentObjectId = "parentObjectId";
        private const string FieldParentColumnId = "parentColumnId";
        private const string FieldReferencedObjectId = "referencedObjectId";
        private const string FieldReferencedColumnId = "referencedColumnId";
        #endregion

        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlForeignKeyColumn"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public SqlForeignKeyColumn()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlForeignKeyColumn"/> class.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="SafeSqlDataReader"/> instance used to read the content
        /// from the data source.
        /// </param>
        public SqlForeignKeyColumn(SafeSqlDataReader reader)
        {
            SetFromReader(reader);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the ID of the foreign key constraint.
        /// </summary>
        /// <value>
        /// An integer specifying the ID of the foreign key constraint.
        /// </value>
        public int ConstraintObjectId { get; set; }
        /// <summary>
        /// Gets or sets the ID of the column, or set of columns, that comprise the foreign key.
        /// </summary>
        /// <value>
        /// An integer specifying the ID of the column, or set of columns, that comprise the foreign key.
        /// </value>
        public int ConstraintColumnId { get; set; }
        /// <summary>
        /// Gets or sets the ID of the parent of the constraint, which is the referencing object.
        /// </summary>
        /// <value>
        /// An integer specifying the ID of the parent of the constraint, which is the referencing object.
        /// </value>
        public int ParentObjectId { get; set; }
        /// <summary>
        /// Gets or sets the ID of the parent column, which is the referencing column.
        /// </summary>
        /// <value>
        /// An integer specifying the ID of the parent column, which is the referencing column.
        /// </value>
        public int ParentColumnId { get; set; }
        /// <summary>
        /// Gets or sets the ID of the referenced object, which has the candidate key.
        /// </summary>
        /// <value>
        /// An integer specifying the ID of the referenced object, which has the candidate key.
        /// </value>
        public int ReferencedObjectId { get; set; }
        /// <summary>
        /// Gets or sets the ID of the referenced column (candidate key column).
        /// </summary>
        /// <value>
        /// An integer specifying the ID of the referenced column (candidate key column).
        /// </value>
        public int ReferencedColumnId { get; set; }
        #endregion

        #region Private Methods / Functions
        /// <summary>
        /// Sets property values from the reader.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="SafeSqlDataReader"/> instance used to read the content
        /// from the data source.
        /// </param>
        private void SetFromReader(SafeSqlDataReader reader)
        {
            ConstraintObjectId = reader.GetInt32(FieldConstraintObjectId);
            ConstraintColumnId = reader.GetInt32(FieldConstraintColumnId);
            ParentObjectId = reader.GetInt32(FieldParentObjectId);
            ParentColumnId = reader.GetInt32(FieldParentColumnId);
            ReferencedObjectId = reader.GetInt32(FieldReferencedObjectId);
            ReferencedColumnId = reader.GetInt32(FieldReferencedColumnId);
        }
        #endregion
    }
}
