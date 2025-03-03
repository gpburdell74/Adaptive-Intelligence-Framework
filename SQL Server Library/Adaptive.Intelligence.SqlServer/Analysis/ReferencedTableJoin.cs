using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.SqlServer.Schema;

namespace Adaptive.Intelligence.SqlServer.Analysis
{
    /// <summary>
    /// Contains the necessary information required to render a T-SQL JOIN statement between tables.
    /// </summary>
    /// <seealso cref="DisposableObjectBase" />
    public sealed class ReferencedTableJoin : DisposableObjectBase
    {
        #region Private Member Declarations        
        /// <summary>
        /// The referenced table
        /// </summary>
        private SqlTable? _referencedTable;
        /// <summary>
        /// The key field
        /// </summary>
        private string? _keyField;
        /// <summary>
        /// The referenced table field
        /// </summary>
        private string? _referencedTableField;
        /// <summary>
        /// The table alias.
        /// </summary>
        private string? _tableAlias;
        /// <summary>
        /// The left join flag.
        /// </summary>
        private bool _leftJoin;
        #endregion

        #region Constructor / Dispose Methods        
        /// <summary>
        /// Initializes a new instance of the <see cref="ReferencedTableJoin"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public ReferencedTableJoin()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ReferencedTableJoin"/> class.
        /// </summary>
        /// <param name="table">
        /// A reference to the <see cref="SqlTable"/> being referenced.
        /// </param>
        /// <param name="keyField">
        /// A string containing the name of the key column field on the parent table.
        /// </param>
        /// <param name="referencedTableField">
        /// A string containing the name of the column on the table being referenced.
        /// </param>
        /// <param name="leftJoin">
        /// A value indicating whether this instance represents a LEFT join instead of an INNER join.
        /// </param>
        public ReferencedTableJoin(SqlTable table, string keyField, string referencedTableField, bool leftJoin)
        {
            _referencedTable = table;
            _keyField = keyField;
            _referencedTableField = referencedTableField;
            _tableAlias = table.TableName;
            _leftJoin = leftJoin;
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
        /// <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            _referencedTable = null;
            _keyField = null;
            _referencedTableField = null;
            _tableAlias = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the name of the key column field on the parent table.
        /// </summary>
        /// <value>
        /// A string containing the name of the key column field on the parent table.
        /// </value>
        public string? KeyField
        {
            get => _keyField;
            set => _keyField = value;
        }
        /// <summary>
        /// Gets or sets the reference to the <see cref="SqlTable"/> being referenced.
        /// </summary>
        /// <value>
        /// A <see cref="SqlTable"/> instance.
        /// </value>
        public SqlTable? ReferencedTable
        {
            get => _referencedTable;
            set => _referencedTable = value;
        }
        /// <summary>
        /// Gets or sets the name of the column on the table being referenced.
        /// </summary>
        /// <value>
        /// A string containing the name of the column on the table being referenced.
        /// </value>
        public string ReferencedTableField
        {
            get => _referencedTableField ?? string.Empty;
            set => _referencedTableField = value;
        }
        /// <summary>
        /// Gets or sets the table alias to use instead of the referenced table name.
        /// </summary>
        /// <remarks>
        /// This is generally used when a table may be used in a join more than once in a query.
        /// </remarks>
        /// <value>
        /// A string containing the table name alias value.
        /// </value>
        public string? TableAlias
        {
            get => _tableAlias;
            set => _tableAlias = value;
        }
        /// <summary>
        /// Gets or sets a value indicating whether the join is a LEFT join.
        /// </summary>
        /// <value>
        ///   <b>true</b> the join is a LEFT join; otherwise, <b>false</b> to use an INNER join.
        /// </value>
        public bool UsesLeftJoin
        {
            get => _leftJoin;
            set => _leftJoin = value;
        }
        #endregion
    }
}
