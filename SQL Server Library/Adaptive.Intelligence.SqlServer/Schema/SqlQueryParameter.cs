using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.SqlServer.Schema
{
    /// <summary>
    /// Represents a query parameter definition.
    /// </summary>
    public sealed class SqlQueryParameter : SqlDataTypeSpecification
    {
        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlQueryParameter"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public SqlQueryParameter()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlQueryParameter"/> class.
        /// </summary>
        /// <param name="sourceColumn">
        /// The <see cref="SqlColumn"/> instance used to create the parameter definition related to that column.
        /// </param>
        public SqlQueryParameter(SqlColumn sourceColumn)
        {
            ColumnId = sourceColumn.ColumnId;
            ColumnName = sourceColumn.ColumnName;
            TypeId = sourceColumn.TypeId;
            Precision = sourceColumn.Precision;
            Scale = sourceColumn.Scale;
            IsNullable = sourceColumn.IsNullable;
            IsIdentity = sourceColumn.IsIdentity;
            IsComputed = sourceColumn.IsComputed;

            // Adjust the length specifier for NVARCHAR ANSI padding.
            if (TypeId == (int)(SqlDataTypes.NVarCharOrSysName) && sourceColumn.IsAnsiPadded)
            {
                MaxLength = (short)(sourceColumn.MaxLength / 2f);
            }
            else
            {
                MaxLength = sourceColumn.MaxLength;
            }
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            ColumnName = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the column ID value.
        /// </summary>
        /// <value>
        /// An integer specifying the column ID value.
        /// </value>
        public int ColumnId
        {
            get; set;
        }
        /// <summary>
        /// Gets or sets the name of the column the parameter is related to.
        /// </summary>
        /// <value>
        /// A string containing the name of the column the parameter is related to.
        /// </value>
        public string? ColumnName
        {
            get; set;
        }
        /// <summary>
        /// Gets  the name of the parameter as related to the column.
        /// </summary>
        /// <value>
        /// A string containing the name of the parameter.
        /// </value>
        public string? ParameterName
        {
            get
            {
                if (ColumnName == null)
                {
                    return null;
                }

                return Constants.At +
                       ColumnName.Substring(0, 1).ToUpper() +
                       ColumnName.Substring(1, ColumnName.Length - 1);
            }
        }
        /// <summary>
        /// Gets or sets the user type ID in SQL Server.
        /// </summary>
        /// <value>
        /// An integer specifying the user type ID in SQL Server.
        /// </value>
        public int TypeId
        {
            get; set;
        }
        /// <summary>
        /// Gets a value indicating whether the column is an identity column.
        /// </summary>
        /// <value>
        /// <c>true</c> if the column is an identity column; otherwise, <c>false</c>.
        /// </value>
        public bool IsIdentity { get; set; }
        /// <summary>
        /// Gets a value indicating whether the column is a computed column.
        /// </summary>
        /// <value>
        /// <c>true</c> if the column is a computed column; otherwise, <c>false</c>.
        /// </value>
        public bool IsComputed { get; set; }
        /// <summary>
        /// Gets a value indicating whether the query parameter represents the Version column.
        /// </summary>
        /// <value>
        ///   <b>true</b> if the query parameter represents the Version column; otherwise, <b>false</b>.
        /// </value>
        public bool IsVersion => (ColumnName == "Version");
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            if (ColumnName == null)
            {
                return nameof(SqlQueryParameter);
            }

            return ColumnName;
        }
        #endregion
    }
}
