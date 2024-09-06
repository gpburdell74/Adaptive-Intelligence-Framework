using Adaptive.Intelligence.Shared;
using System.ComponentModel;
using System.Drawing;

namespace Adaptive.Intelligence.SqlServer.UI
{
    /// <summary>
    /// Extends the rich text editor control for displaying and very basic editing of SQL queries 
    /// and statements.
    /// </summary>
    /// <seealso cref="RichTextBox" />
    public class SqlTextEditor : RichTextBox
    {
        #region Private Member Declarations        
        private const short WM_PAINT = 0x00f;
        /// <summary>
        /// The file name value.
        /// </summary>
        private string? _fileName;
        /// <summary>
        /// The re-entrant flag to indicate not to re-enter the text changed event while it is currently
        /// processing.
        /// </summary>
        private bool _reEntrantFlag;

        private bool _paint = true;

        #endregion

        #region Constructor / Dispose Methods        
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlTextEditor"/> class.
        /// </summary>
        public SqlTextEditor()
        {
            Cursor = Cursors.IBeam;
        }
        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Control" /> and its child controls and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            _fileName = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties        
        /// <summary>
        /// Gets or sets a value indicating whether the control can respond to user interaction.
        /// </summary>
        public new bool Enabled
        {
            get => base.Enabled;
            set
            {
                base.Enabled = value;
                Invalidate();
            }
        }
        /// <summary>
        /// Gets or sets the name of the file the SQL query is to be stored in.
        /// </summary>
        /// <value>
        /// A string containing the name of the file that was last loaded or saved, or
        /// <b>null</b> if one is not specified.
        /// </value>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string FileName
        {
            get
            {
                if (_fileName == null)
                    return string.Empty;
                else
                    return _fileName;
            }
            set
            {
                _fileName = value;

            }
        }
        /// <summary>
        /// Gets or sets the content of the SQL query text.
        /// </summary>
        /// <value>
        /// A string that compromises the T-SQL query.
        /// </value>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SqlQuery
        {
            get => Text;
            set
            {
                Text = value;
                Invalidate();
                OnTextChanged(EventArgs.Empty);
            }
        }
        #endregion

        #region Protected Method Overrides        
        /// <summary>
        /// Processes Windows messages.
        /// </summary>
        /// <param name="m">A Windows Message object.</param>
        protected override void WndProc(ref Message m)
        {
            // Code courtesy of Mark Mihevc  
            // sometimes we want to eat the paint message so we don't have to see all the  
            // flicker from when we select the text to change the color.  
            if (m.Msg == WM_PAINT)
            {
                if (_paint)
                    base.WndProc(ref m); // if we decided to paint this control, just call the RichTextBox WndProc  
                else
                    m.Result = IntPtr.Zero; // not painting, must set this to IntPtr.Zero if not painting otherwise serious problems.  
            }
            else
                base.WndProc(ref m); // message other than WM_PAINT.
        }
        /// <summary>
        /// Raises the <see cref="Control.TextChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs" /> that contains the event data.</param>
        protected override void OnTextChanged(EventArgs e)
        {
           

            // Perform all base activities.
            base.OnTextChanged(e);

            // Do nothing if this event is already processing.
            if (!_reEntrantFlag)
            {
                _reEntrantFlag = true;
                _paint = false;
                SuspendLayout();

                // Remember where we are.
                int currentCursorPos = SelectionStart;

                // Colorize the SQL statement.
                SelectionColor = Color.Black;
                
                HighlightSqlText();

                // Return the cursor to where it was before.
                SelectionStart = currentCursorPos;
                SelectionLength = 0;
                SelectionColor = Color.Black;

                ResumeLayout();
                _paint = true;
                _reEntrantFlag = false;
            }
        }
        #endregion

        #region Public Methods / Functions        
        /// <summary>
        /// Moves the cursor to the specified line number.
        /// </summary>
        /// <param name="line">
        /// An integer specifying the line number.
        /// </param>
        public void MoveCursorToLine(int line)
        {
            SelectionStart = GetFirstCharIndexFromLine(line - 1);
            SelectionLength = 0;
        }
        /// <summary>
        /// Selects the text in the current line.
        /// </summary>
        public void SelectLine()
        {
            int pos = GetFirstCharIndexOfCurrentLine();
            int lineNumber = GetLineFromCharIndex(SelectionStart);
            int len = Lines[lineNumber].Length;
            SelectionStart = pos;
            SelectionLength = len;
        }
        #endregion

        #region Private Methods / Functions        
        /// <summary>
        /// Highlights the specified word in the current text.
        /// </summary>
        /// <param name="word">
        /// A string specifying the word to be highlighted.
        /// </param>
        /// <param name="color">
        /// The <see cref="Color"/> to highlight the word with.
        /// </param>
        private void HighlightWord(string word, Color color)
        {
            string text = Text;
            int pos = 0;
            int len = text.Length - 1;
            int wordLen = word.Length;
            do
            {
                pos = text.IndexOf(word, pos, StringComparison.InvariantCultureIgnoreCase);
                //pos = Find(word, pos, RichTextBoxFinds.WholeWord);
                if (pos > -1)
                {
                    Select(pos, word.Length);
                    SelectionColor = color;
                    pos += wordLen;
                }

            } while (pos > 0 && pos < len);
        }
        /// <summary>
        /// Colorizes and highlights the T-SQl keywords and special values in the SQL text.
        /// </summary>
        private void HighlightSqlText()
        {
            HighlightWord(TSqlConstants.SqlSelect, Color.Blue);
            HighlightWord(TSqlConstants.SqlFrom, Color.Blue);
            HighlightWord(TSqlConstants.SqlWhere, Color.Blue);
            HighlightWord(TSqlConstants.SqlCreate, Color.Blue);
            HighlightWord(TSqlConstants.SqlUpdate, Color.Magenta);
            HighlightWord(TSqlConstants.SqlInsert, Color.Blue);
            HighlightWord(TSqlConstants.SqlInto, Color.Blue);
            HighlightWord(TSqlConstants.SqlProcedure, Color.Blue);
            HighlightWord(" " + TSqlConstants.SqlAs + " ", Color.Blue);
            HighlightWord(TSqlConstants.SqlBegin, Color.Blue);
            HighlightWord(TSqlConstants.SqlEnd, Color.Blue);
            HighlightWord(TSqlConstants.SqlTop, Color.Blue);
            HighlightWord(TSqlConstants.SqlSet, Color.Blue);
            HighlightWord(TSqlConstants.SqlValues, Color.Blue);
            HighlightWord(TSqlConstants.SqlAlter, Color.Blue);
            HighlightWord(TSqlConstants.SqlPrimary, Color.Blue);
            HighlightWord(TSqlConstants.SqlAnsiNulls, Color.Blue);

            HighlightWord(TSqlConstants.SqlInner, Color.Gray);
            HighlightWord(TSqlConstants.SqlLeft, Color.Gray);
            HighlightWord(TSqlConstants.SqlJoin, Color.Gray);

            HighlightWord(TSqlConstants.SqlDataTypeBigInt, Color.Blue);
            HighlightWord(TSqlConstants.SqlDataTypeBinary, Color.Blue);
            HighlightWord(TSqlConstants.SqlNull, Color.Blue);
            HighlightWord(TSqlConstants.SqlDataTypeBit, Color.Blue);
            HighlightWord(TSqlConstants.SqlDataTypeChar, Color.Blue);
            HighlightWord(TSqlConstants.SqlDataTypeDate, Color.Blue);
            HighlightWord(TSqlConstants.SqlDataTypeDateTime, Color.Blue);
            HighlightWord(TSqlConstants.SqlDataTypeDateTime2, Color.Blue);
            HighlightWord(TSqlConstants.SqlDataTypeDateTimeOffset, Color.Blue);
            HighlightWord(TSqlConstants.SqlDataTypeDecimal, Color.Blue);
            HighlightWord(TSqlConstants.SqlDataTypeFloat, Color.Blue);
            HighlightWord(TSqlConstants.SqlDataTypeImage, Color.Blue);
            HighlightWord(TSqlConstants.SqlDataTypeInt, Color.Blue);
            HighlightWord(TSqlConstants.SqlDataTypeMoney, Color.Blue);
            HighlightWord(TSqlConstants.SqlDataTypeNChar, Color.Blue);
            HighlightWord(TSqlConstants.SqlDataTypeNText, Color.Blue);
            HighlightWord(TSqlConstants.SqlDataTypeNumeric, Color.Blue);
            HighlightWord(TSqlConstants.SqlDataTypeNVarCharOrSysName, Color.Blue);
            HighlightWord(TSqlConstants.SqlDataTypeReal, Color.Blue);
            HighlightWord(TSqlConstants.SqlDataTypeSmallDateTime, Color.Blue);
            HighlightWord(TSqlConstants.SqlDataTypeSmallInt, Color.Blue);
            HighlightWord(TSqlConstants.SqlDataTypeSmallMoney, Color.Blue);
            HighlightWord(TSqlConstants.SqlDataTypeSpatialType, Color.Blue);
            HighlightWord(TSqlConstants.SqlDataTypeSqlVariant, Color.Blue);
            HighlightWord(TSqlConstants.SqlDataTypeText, Color.Blue);
            HighlightWord(TSqlConstants.SqlDataTypeTime, Color.Blue);
            HighlightWord(TSqlConstants.SqlDataTypeTimeStamp, Color.Blue);
            HighlightWord(TSqlConstants.SqlDataTypeTinyInt, Color.Blue);
            HighlightWord(TSqlConstants.SqlDataTypeUniqueIdentifier, Color.Blue);
            HighlightWord(TSqlConstants.SqlDataTypeVarBinary, Color.Blue);
            HighlightWord(TSqlConstants.SqlDataTypeVarChar, Color.Blue);
            HighlightWord(TSqlConstants.SqlDataTypeXml, Color.Blue);

            HighlightWord(TSqlConstants.SqlOpenParenthesisDelimiter, Color.Gray);
            HighlightWord(TSqlConstants.SqlOperatorEqualTo, Color.Gray);
            HighlightWord(TSqlConstants.SqlOperatorGreaterThan, Color.Gray);
            HighlightWord(TSqlConstants.SqlOperatorGreaterThanOrEqualTo, Color.Gray);
            HighlightWord(TSqlConstants.SqlOperatorIsNotNull, Color.Gray);
            HighlightWord(TSqlConstants.SqlOperatorIsNull, Color.Gray);
            HighlightWord(TSqlConstants.SqlOperatorLessThan, Color.Gray);
            HighlightWord(TSqlConstants.SqlOperatorLessThanOrEqualTo, Color.Gray);
            HighlightWord(TSqlConstants.SqlOperatorNot, Color.Gray);
            HighlightWord(TSqlConstants.SqlOperatorNotEqualTo, Color.Gray);
            HighlightWord(TSqlConstants.SqlOperatorNotIn, Color.Gray);

            HighlightWord(TSqlConstants.SqlSysFunctionNewId, Color.Magenta);
            HighlightWord(TSqlConstants.SqlSysFunctionSysUtcDateTime, Color.Magenta);

            ColorizeCommentBlocks();
            ColorizeCommentLines();

            ForeColor = Color.Black;
        }
        /// <summary>
        /// Colorizes the comment lines.
        /// </summary>
        private void ColorizeCommentLines()
        {
            int pos = 0;
            int len = Text.Length - 1;
            do
            {
                // Find each line that starts with or contains "--"
                pos = Find(TSqlConstants.SqlCommentLineDelimiter, pos, RichTextBoxFinds.None);
                if (pos > -1)
                {
                    // Find the end of line.
                    int rnPos = Find(Constants.CarriageReturn, pos + 1, RichTextBoxFinds.None);
                    if (rnPos > -1)
                    {
                        SelectionStart = pos;
                        SelectionLength = rnPos - pos;
                        SelectionColor = Color.Green;
                        pos++;
                    }
                    else
                    {
                        int index = GetLineFromCharIndex(pos);
                        int llen = Lines[index].Length;

                        SelectionColor = Color.Green;
                        SelectionStart = pos;
                        SelectionLength = llen;
                        SelectionColor = Color.Green;

                        pos = -1;
                    }
                }
            } while (pos > 0 && pos < len - 1);
        }
        /// <summary>
        /// Colorizes the comment blocks.
        /// </summary>
        private void ColorizeCommentBlocks()
        {
            int pos = 0;
            int endPos = -1;
            int textLen = Text.Length - 1;
            do
            {
                // Find each /*  and */ pair.
                pos = Find(TSqlConstants.SqlCommentBlockStart, pos, RichTextBoxFinds.None);
                endPos = Find(TSqlConstants.SqlCommentBlockEnd, pos + 1, RichTextBoxFinds.None);
                if (pos > 0 && endPos == -1)
                {
                    // If there is no ending */, color all the remaining text.
                    Select(pos, textLen - pos);
                    SelectionColor = Color.Green;
                    pos = -1;
                }
                else if (endPos > -1 && pos > 0)
                {
                    Select(pos, (endPos - pos) + 2);
                    SelectionColor = Color.Green;
                    pos = endPos;
                }
            } while (pos > 0 && endPos > 0 && pos < textLen);

        }
        #endregion

    }
}