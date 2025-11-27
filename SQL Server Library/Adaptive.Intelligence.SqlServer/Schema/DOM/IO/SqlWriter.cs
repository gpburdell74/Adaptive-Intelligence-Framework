using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.SqlServer.CodeDom.CodeProvider;
using System.Text;

namespace Adaptive.Intelligence.SqlServer.CodeDom.IO
{
    /// <summary>
    /// Provides a mechanism for writing SQL object(s) code to an output destination.
    /// </summary>
    /// <seealso cref="DisposableObjectBase" />
    public sealed class SqlWriter : DisposableObjectBase
    {
        #region Private Member Declarations

        #region Output Writer Objects.
        /// <summary>
        /// The optional string builder instance to write to.
        /// </summary>
        private StringBuilder? _builder;
        /// <summary>
        /// The optional output stream to write to.
        /// </summary>
        private Stream? _stream;
        /// <summary>
        /// The writer instance.
        /// </summary>
        private SqlTextWriter? _writer;
        /// <summary>
        /// The is stream local flag.
        /// </summary>
        private readonly bool _isStreamLocal;
        #endregion

        #region SQL Code Dom Generators and Writers.
        /// <summary>
        /// The SQL provider instance to provide language-specific code.
        /// </summary>
        private ISqlCodeProvider? _codeProvider;
        /// <summary>
        /// The statement writer instance.
        /// </summary>
        private SqlStatementWriter? _statementWriter;
        /// <summary>
        /// The clause writer instance.
        /// </summary>
        private SqlClauseWriter? _clauseWriter;
        /// <summary>
        /// The expression writer.
        /// </summary>
        private SqlExpressionWriter? _expressionWriter;
        #endregion

        #endregion

        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlWriter"/> class.
        /// </summary>
        /// <param name="codeProvider">
        /// An <see cref="ISqlCodeProvider"/> implementation used to provide the SQL language specific code to
        /// the writer.
        /// </param>
        public SqlWriter(ISqlCodeProvider codeProvider)
        {
            _codeProvider = codeProvider ?? throw new ArgumentNullException(nameof(codeProvider));

            // Create and set the output objects.
            _stream = new MemoryStream(10000);
            _isStreamLocal = true;
            _writer = new SqlTextWriter(_stream);

            // Create the SQL writing instances.
            _expressionWriter = new SqlExpressionWriter(_codeProvider, _writer);
            _clauseWriter = new SqlClauseWriter(_codeProvider, _expressionWriter, _writer);
            _statementWriter = new SqlStatementWriter(_codeProvider, _clauseWriter, _expressionWriter, _writer);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlWriter"/> class.
        /// </summary>
        /// <param name="codeProvider">
        /// An <see cref="ISqlCodeProvider"/> implementation used to provide the SQL language specific code to
        /// the writer.
        /// </param>
        /// <param name="builder">
        /// A <see cref="StringBuilder"/> instance to write to.
        /// </param>
        public SqlWriter(ISqlCodeProvider codeProvider, StringBuilder builder)
        {
            _codeProvider = codeProvider ?? throw new ArgumentNullException(nameof(codeProvider));
            _builder = builder ?? throw new ArgumentNullException(nameof(builder), @"A StringBuilder instance must be provided.");

            // Create and set the output objects.
            _stream = null;

            _isStreamLocal = false;
            _writer = new SqlTextWriter(_builder);

            // Create the SQL writing instances.
            _expressionWriter = new SqlExpressionWriter(_codeProvider, _writer);
            _clauseWriter = new SqlClauseWriter(_codeProvider, _expressionWriter, _writer);
            _statementWriter = new SqlStatementWriter(_codeProvider, _clauseWriter, _expressionWriter, _writer);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlWriter"/> class.
        /// </summary>
        /// <param name="codeProvider">
        /// An <see cref="ISqlCodeProvider"/> implementation used to provide the SQL language specific code to
        /// the writer.
        /// </param>
        /// <param name="destinationStream">
        /// The destination <see cref="Stream"/> to write the SQL code to.
        /// </param>
        public SqlWriter(ISqlCodeProvider codeProvider, Stream destinationStream)
        {
            _codeProvider = codeProvider ?? throw new ArgumentNullException(nameof(codeProvider));

            // Create and set the output objects.
            _stream = destinationStream;
            _isStreamLocal = false;
            _writer = new SqlTextWriter(_stream);

            // Create the SQL writing instances.
            _expressionWriter = new SqlExpressionWriter(_codeProvider, _writer);
            _clauseWriter = new SqlClauseWriter(_codeProvider, _expressionWriter, _writer);
            _statementWriter = new SqlStatementWriter(_codeProvider, _clauseWriter, _expressionWriter, _writer);
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
                _builder?.Clear();
                _writer?.Dispose();
                _statementWriter?.Dispose();
                _clauseWriter?.Dispose();
                _expressionWriter?.Dispose();

                if (_isStreamLocal)
                {
                    _stream?.Dispose();
                }
            }

            _statementWriter = null;
            _clauseWriter = null;
            _expressionWriter = null;
            _codeProvider = null;
            _builder = null;
            _writer = null;
            _stream = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Clears the buffers for the current writer and causes any buffered data to be written to the output stream.
        /// </summary>
        public void Flush()
        {
            _writer?.Flush();
        }
        /// <summary>
        /// Clears the buffers for the current writer and causes any buffered data to be written to the output stream.
        /// </summary>
        public async Task FlushAsync()
        {
            if (_writer != null)
            {
                await _writer.FlushAsync().ConfigureAwait(false);
            }
        }
        /// <summary>
        /// Increases the indentation level.
        /// </summary>
        public void Indent()
        {
            _writer?.Indent();
        }
        /// <summary>
        /// Decreases the indentation level.
        /// </summary>
        public void UnIndent()
        {
            _writer?.UnIndent();
        }
        /// <summary>
        /// Writes the expression.
        /// </summary>
        /// <param name="expression">
        /// The <see cref="SqlCodeExpression"/> expression to be written.
        /// </param>
        public void WriteExpression(SqlCodeExpression? expression)
        {
            if (expression != null)
            {
                _expressionWriter?.WriteExpression(expression);
            }
        }
        /// <summary>
        /// Writes the expression.
        /// </summary>
        /// <param name="expression">
        /// The <see cref="SqlCodeExpression"/> expression to be written.
        /// </param>
        public async Task WriteExpressionAsync(SqlCodeExpression? expression)
        {
            if (expression != null && _expressionWriter != null)
            {
                await _expressionWriter.WriteExpressionAsync(expression).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// Renders and writes the SQL statement to the output destination.
        /// </summary>
        /// <param name="statement">
        /// The <see cref="SqlCodeStatement"/>-derived instance to render and write.
        /// </param>
        public void WriteStatement(SqlCodeStatement? statement)
        {
            if (statement != null)
            {
                _statementWriter?.WriteStatement(statement);
            }
        }
        /// <summary>
        /// Renders and writes the SQL statement to the output destination.
        /// </summary>
        /// <param name="statement">
        /// The <see cref="SqlCodeStatement"/>-derived instance to render and write.
        /// </param>
        public async Task WriteStatementAsync(SqlCodeStatement? statement)
        {
            if (statement != null && _statementWriter != null)
            {
                await _statementWriter.WriteStatementAsync(statement).ConfigureAwait(false);
            }
        }
        #endregion
    }
}
