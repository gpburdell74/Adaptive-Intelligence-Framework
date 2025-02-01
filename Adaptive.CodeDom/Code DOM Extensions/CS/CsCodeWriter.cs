using Adaptive.Intelligence.Shared;
using System.Text;

namespace Adaptive.CodeDom
{
    /// <summary>
    /// Provides a mechanism for writing template-based c# code.
    /// </summary>
    /// <seealso cref="DisposableObjectBase" />
    public sealed class CsCodeWriter : DisposableObjectBase
    {
        #region Private Member Declarations        
        /// <summary>
        /// The string builder to use.
        /// </summary>
        private StringBuilder? _builder;
        /// <summary>
        /// The writer to use.
        /// </summary>
        private StringWriter? _writer;
        /// <summary>
        /// The number of tabs in the current indentation.
        /// </summary>
        private int _tabs;
        #endregion

        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="CsCodeWriter"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public CsCodeWriter()
        {
            _builder = new StringBuilder();
            _writer = new StringWriter(_builder);
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
                _builder?.Clear();
            }

            _writer = null;
            _builder = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Increases the indention level when writing a new text line.
        /// </summary>
        public void Indent()
        {
            _tabs++;
        }
        /// <summary>
        /// Decreases the indention level when writing a new text line.
        /// </summary>
        public void UnIndent()
        {
            _tabs--;
            if (_tabs < 0)
                _tabs = 0;
        }
        /// <summary>
        /// Writes the block start text.
        /// </summary>
        public void WriteBlockStart()
        {
            InnerWriteLine(CodeDomConstants.CsBlockStart);
            _tabs++;
        }
        /// <summary>
        /// Writes the block end text.
        /// </summary>
        public void WriteBlockEnd()
        {
            _tabs--;
            InnerWriteLine(CodeDomConstants.CsBlockEnd);
        }
        /// <summary>
        /// Writes the block end text.
        /// </summary>
        /// <param name="endStatement">
        /// A value indicating whether to include a semicolon as an end-of-statement character.
        /// </param>
        public void WriteBlockEnd(bool endStatement)
        {
            _tabs--;
            if (endStatement)
                InnerWriteLine(CodeDomConstants.CsLineEnd + Constants.SemiColon);
            else
                InnerWriteLine(CodeDomConstants.CsLineEnd);
        }
        /// <summary>
        /// Writes the line.
        /// </summary>
        public void WriteLine()
        {
            _writer?.WriteLine(string.Empty);
        }
        /// <summary>
        /// Writes the line.
        /// </summary>
        /// <param name="content">
        /// A string containing the content for the line of text.
        /// </param>
        public void WriteLine(string content)
        {
            InnerWriteLine(content);
        }
        /// <summary>
        /// Write the c# namespace declaration start.
        /// </summary>
        /// <param name="namespaceName">
        /// A string containing the namespace name text.
        /// </param>
        public void WriteNamespaceStart(string namespaceName)
        {
            WriteLine(CodeDomConstants.CsNamespace + namespaceName);
            WriteBlockStart();
        }
        /// <summary>
        /// Write the c# using declaration.
        /// </summary>
        /// <param name="namespaceName">
        /// A string containing the namespace name text.
        /// </param>
        public void WriteUsing(string namespaceName)
        {
            InnerWriteLine(CodeDomConstants.CsUsing + namespaceName + CodeDomConstants.CsLineEnd);
        }
        /// <summary>
        /// Writes the XML parameter section.
        /// </summary>
        /// <param name="paramName">
        /// A string containing the name of the parameter.
        /// </param>
        /// <param name="paramText">
        /// A string containing the description of the parameter.
        /// </param>
        public void WriteXmlParameter(string paramName, string paramText)
        {
            InnerWriteLine(string.Format(CodeDomConstants.CsXmlParamStart, paramName));
            InnerWriteLine(string.Format(CodeDomConstants.CsXmlComment, paramText));
            InnerWriteLine(CodeDomConstants.CsXmlParamEnd);
        }
        /// <summary>
        /// Writes the XML remarks section.
        /// </summary>
        /// <param name="remarksText">
        /// A string containing the remarks documentation text.
        /// </param>
        public void WriteXmlRemarks(string remarksText)
        {
            InnerWriteLine(CodeDomConstants.CsXmlRemarksStart);
            InnerWriteLine(string.Format(CodeDomConstants.CsXmlComment, remarksText));
            InnerWriteLine(CodeDomConstants.CsXmlRemarksEnd);
        }
        /// <summary>
        /// Writes the XML returns section.
        /// </summary>
        /// <param name="returnsText">
        /// A string containing the returns documentation text.
        /// </param>
        public void WriteXmlReturns(string returnsText)
        {
            InnerWriteLine(CodeDomConstants.CsXmlReturnsStart);
            InnerWriteLine(string.Format(CodeDomConstants.CsXmlComment, returnsText));
            InnerWriteLine(CodeDomConstants.CsXmlReturnsEnd);
        }
        /// <summary>
        /// Writes the XML summary section.
        /// </summary>
        /// <param name="summaryText">
        /// A string containing the summary documentation text.
        /// </param>
        public void WriteXmlSummary(string summaryText)
        {
            InnerWriteLine(CodeDomConstants.CsXmlSummaryStart);
            InnerWriteLine(string.Format(CodeDomConstants.CsXmlComment, summaryText));
            InnerWriteLine(CodeDomConstants.CsXmlSummaryEnd);
        }
        /// <summary>
        /// Writes the XML see also section.
        /// </summary>
        /// <param name="seeAlsoText">
        /// A string containing the see also documentation text.
        /// </param>
        public void WriteXmlSeeAlso(string seeAlsoText)
        {
            InnerWriteLine(string.Format(CodeDomConstants.CsXmlSeeAlso, seeAlsoText));
        }
        /// <summary>
        /// Returns the content of the writer as a string.
        /// </summary>
        /// <returns>
        /// A string containing the rendered code content.
        /// </returns>
        public override string ToString()
        {
            _writer?.Flush();
            if (_builder == null)
                return nameof(CsCodeWriter);
            else
                return _builder.ToString();
        }
        #endregion

        #region Private Methods / Functions        
        /// <summary>
        /// Writes the line of text.
        /// </summary>
        /// <param name="content">The content.</param>
        private void InnerWriteLine(string content)
        {
            if (_writer != null)
            {
                WriteTabs();
                _writer.WriteLine(content);
            }
        }
        /// <summary>
        /// Writes the tabs.
        /// </summary>
        private void WriteTabs()
        {
            if (_tabs > 0 && _writer != null)
            {
                string tabs = new string(Constants.TabChar, _tabs);
                _writer.Write(tabs);
            }
        }
        #endregion
    }
}
