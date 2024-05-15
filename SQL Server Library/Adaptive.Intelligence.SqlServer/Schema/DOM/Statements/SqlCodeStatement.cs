using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.SqlServer.CodeDom.CodeProvider;
using Adaptive.Intelligence.SqlServer.CodeDom.IO;
using System.Text;

namespace Adaptive.Intelligence.SqlServer.CodeDom
{
    /// <summary>
    /// Represents and defines a SQL Server statement.
    /// </summary>
    /// <seealso cref="DisposableObjectBase" />
    public abstract class SqlCodeStatement : DisposableObjectBase, ICloneable
    {
        /// <summary>
        /// The statement type.
        /// </summary>
        private readonly SqlStatementType _statementType;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeStatement"/> class.
        /// </summary>
        /// <param name="statementType">
        /// A <see cref="SqlStatementType"/> enumerated value indicating the type of statement.
        /// </param>
        protected SqlCodeStatement(SqlStatementType statementType)
        {
            _statementType = statementType;
        }
        /// <summary>
        /// Gets the type of the statement.
        /// </summary>
        /// <remarks>
        /// This is used to avoid expensive type comparisons.
        /// </remarks>
        /// <value>
        /// A <see cref="SqlStatementType"/> enumerated value indicating the type of this statement.
        /// </value>
        public SqlStatementType StatementType => _statementType;

        #region Common Rendering Methods        
        /// <summary>
        /// Renders the current instance in T-SQL.
        /// </summary>
        /// <returns>
        /// A string containing the SQL code snippet or full statement, as rendered in T-SQL.
        /// </returns>
        public string Render()
        {
            // Create the objects.
            StringBuilder builder = new StringBuilder();
            TransactSqlCodeProvider provider = new TransactSqlCodeProvider();
            SqlWriter writer = new SqlWriter(provider, builder);

            // Render the T-SQL.
            Render(provider, writer);
            string code = builder.ToString();

            // Dispose and return.
            writer.Dispose();
            provider.Dispose();
            return code;
        }
        /// <summary>
        /// Renders the current instance to the provided SQL writer.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="TransactSqlCodeProvider"/> instance.
        /// </param>
        /// <param name="writer">
        /// The <see cref="SqlWriter"/> instance to be written to.
        /// </param>
        public void Render(TransactSqlCodeProvider provider, SqlWriter writer)
        {
            writer.WriteStatement(this);
            writer.Flush();
        }
        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Render();
        }
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public abstract SqlCodeStatement Clone();
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        object ICloneable.Clone()
        {
            return Clone();
        }

        #endregion

    }
}