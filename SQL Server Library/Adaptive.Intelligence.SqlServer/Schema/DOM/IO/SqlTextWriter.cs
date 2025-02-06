using Adaptive.Intelligence.Shared;
using System.Text;

namespace Adaptive.Intelligence.SqlServer.CodeDom.IO
{
    /// <summary>
    /// Provides a mechanism for writing SQL content text to an output object.
    /// </summary>
    /// <remarks>
    /// This class encapsulates the basic write operations either to a <see cref="StringBuilder"/>
    /// instance, or a <see cref="Stream"/> instance.
    ///
    /// The class also provides indents and indention level tracking.
    /// </remarks>
    /// <seealso cref="DisposableObjectBase" />
    public sealed class SqlTextWriter : DisposableObjectBase
    {
        #region Private Member Declarations
        /// <summary>
        /// The optional string builder instance to write to.
        /// </summary>
        private StringBuilder? _builder;
        /// <summary>
        /// The indentation level (number of tabs).
        /// </summary>
        private int _indentLevel;
        /// <summary>
        /// The optional output stream to write to.
        /// </summary>
        private Stream? _stream;
        /// <summary>
        /// The stream writer instance.
        /// </summary>
        private StreamWriter? _writer;
        #endregion

        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlTextWriter"/> class.
        /// </summary>
        /// <param name="builder">
        /// A <see cref="StringBuilder"/> instance to write to.
        /// </param>
        public SqlTextWriter(StringBuilder builder)
        {
            _builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlTextWriter"/> class.
        /// </summary>
        /// <param name="destinationStream">
        /// The destination <see cref="Stream"/> to write the SQL code to.
        /// </param>
        public SqlTextWriter(Stream destinationStream)
        {
            if (destinationStream == null)
                throw new ArgumentNullException(nameof(destinationStream));

            if (!destinationStream.CanWrite)
                throw new InvalidOperationException("The provided stream cannot be written to.");

            _stream = destinationStream;
            _writer = new StreamWriter(_stream);
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
                _writer?.Dispose();
                _stream?.Dispose();
                _builder?.Clear();
            }

            _builder = null;
            _writer = null;
            _stream = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the indentation level.
        /// </summary>
        /// <value>
        /// An integer specifying the number of indentations to use when writing the next line of SQL text.
        /// </value>
        public int IndentLevel
        {
            get => _indentLevel;
            set
            {
                if (value < 0)
                    value = 0;
                _indentLevel = value;
            }
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
                await _writer.FlushAsync().ConfigureAwait(false);
        }
        /// <summary>
        /// Increases the indentation level.
        /// </summary>
        public void Indent()
        {
            _indentLevel++;
        }
        /// <summary>
        /// Decreases the indentation level.
        /// </summary>
        public void UnIndent()
        {
            IndentLevel--;
        }
        /// <summary>
        /// Writes the specified line of text to the current output.
        /// </summary>
        /// <param name="text">
        /// A string containing the text to be written.
        /// </param>
        public void Write(string text)
        {
            if (_builder != null)
                _builder.Append(text);
            else
            {
                _writer?.Write(text);
            }
        }
        /// <summary>
        /// Writes the specified line of text to the current output.
        /// </summary>
        /// <param name="text">
        /// A string containing the text to be written.
        /// </param>
        public async Task WriteAsync(string text)
        {
            if (_builder != null)
                _builder.Append(text);
            else if (_writer != null)
                await _writer.WriteAsync(text).ConfigureAwait(false);
        }
        /// <summary>
        /// Writes the end-of-line to the output stream.
        /// </summary>
        public void WriteLine()
        {
            if (_builder != null)
                _builder.AppendLine();
            else
            {
                _writer?.WriteLine();
            }
        }
        /// <summary>
        /// Writes the specified line of text to the current output.
        /// </summary>
        /// <param name="text">
        /// A string containing the text to be written.
        /// </param>
        public void WriteLine(string text)
        {
            if (_builder != null)
                _builder.AppendLine(text);
            else
            {
                _writer?.WriteLine(text);
            }
        }
        /// <summary>
        /// Writes the end-of-line to the output stream.
        /// </summary>
        public async Task WriteLineAsync()
        {
            if (_builder != null)
                _builder.AppendLine();
            else if (_writer != null)
                await _writer.WriteLineAsync().ConfigureAwait(false);
        }
        /// <summary>
        /// Writes the specified line of text to the current output.
        /// </summary>
        /// <param name="text">
        /// A string containing the text to be written.
        /// </param>
        public async Task WriteLineAsync(string text)
        {
            if (_builder != null)
                _builder.AppendLine(text);
            else if (_writer != null)
                await _writer.WriteLineAsync(text).ConfigureAwait(false);
        }
        /// <summary>
        /// Renders the tabs based on the current indention level.
        /// </summary>
        /// <returns>
        /// A string containing the tabs, or <see cref="string.Empty"/>.
        /// </returns>
        public void WriteTabs()
        {
            if (_indentLevel > 0)
                Write(new string('\t', _indentLevel));
        }
        /// <summary>
        /// Renders the tabs based on the current indention level.
        /// </summary>
        /// <returns>
        /// A string containing the tabs, or <see cref="string.Empty"/>.
        /// </returns>
        public async Task WriteTabsAsync()
        {
            if (_indentLevel > 0)
                await WriteAsync(new string(Constants.TabChar, _indentLevel));
        }
        #endregion
    }
}
