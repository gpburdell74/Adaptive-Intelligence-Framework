using System.ComponentModel;

namespace Adaptive.Intelligence.Shared.UI
{
    /// <summary>
    /// Extends the rich text editor control for displaying and very basic editing of syntax-based text content.
    /// </summary>
    /// <seealso cref="RichTextBox" />
    public partial class SyntaxEditor : RichTextBox
    {
        #region Public Events
        /// <summary>
        /// Occurs when the control clears the error list.
        /// </summary>
        public event EventHandler? ClearErrors;
        /// <summary>
        /// Occurs when a parsing error occurs.
        /// </summary>
        public event System.EventHandler<string>? ParsingError;
        #endregion

        #region Private Member Declarations
        /// <summary>
        /// Windows Paint Message value.
        /// </summary>
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
        /// <summary>
        /// The paint flag.
        /// </summary>
        private bool _paint = true;
        /// <summary>
        /// The undoing flag.
        /// </summary>
        private bool _undoing = false;
        /// <summary>
        /// The undo buffer.
        /// </summary>
        private UndoBuffer<string>? _undoBuffer;
        #endregion

        #region Constructor / Dispose Methods        
        /// <summary>
        /// Initializes a new instance of the <see cref="SyntaxEditor"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public SyntaxEditor()
        {
            Cursor = Cursors.IBeam;
            Font = new Font("Courier", 12f);
            AcceptsTab = true;
            Multiline = true;
            _undoBuffer = new UndoBuffer<string>();
        }
        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Control" /> and its child controls and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed && disposing)
                _undoBuffer?.Dispose();

            _undoBuffer = null;
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
        /// Gets or sets the name of the file the JSON is to be stored in.
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
        /// Gets or sets the content of the code/text.
        /// </summary>
        /// <value>
        /// A string that compromises the code/text.
        /// </value>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Code
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

        #region Protected Properties        
        /// <summary>
        /// Gets or sets a value indicating whether this is a multi line <see cref="T:System.Windows.Forms.RichTextBox" /> control.
        /// </summary>
        /// <remarks>
        /// This property is overridden to always be <b>true</b>.
        /// </remarks>
        [Browsable(false)]
        public override bool Multiline { get => true; set => base.Multiline = true; }
        /// <summary>
        /// Gets or sets the reference to the text/code syntax provider.
        /// </summary>
        /// <value>
        /// An <see cref="ISyntaxProvider"/> instance containing the syntax coloring rules,
        /// or <b>null</b> for no provider.
        /// </value>
        protected virtual ISyntaxProvider? SyntaxProvider { get; set; }
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
        /// Suppresses the <see cref="E:System.Windows.Forms.TextBoxBase.AcceptsTabChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnAcceptsTabChanged(EventArgs e)
        {
            AcceptsTab = true;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.KeyDown" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (_undoBuffer != null)
            {
                // Undo = Ctrl + Z
                if (e.Control && e.KeyCode == Keys.Z)
                {
                    e.Handled = true;
                    // Undo operation.
                    if (_undoBuffer.HasData)
                    {
                        _undoing = true;
                        string? oldText = _undoBuffer.GetLast();
                        if (oldText == null)
                            Text = string.Empty;
                        else
                            Text = oldText;
                        _undoing = false;
                    }
                }
                else if (!e.Control && !e.Alt)
                {
                    if (!_undoing)
                    {
                        string value = Text;
                        if (!_undoBuffer!.IsSame(value))
                            _undoBuffer.Add(value);
                    }
                    base.OnKeyUp(e);
                }
            }
        }

        /// <summary>
        /// Raises the <see cref="Control.TextChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs" /> that contains the event data.</param>
        protected override void OnTextChanged(EventArgs e)
        {
            // Perform all base activities.
            base.OnTextChanged(e);

            // Do nothing if there is no syntax provider, or 
            // if this event is already processing.
            if (SyntaxProvider != null && !_reEntrantFlag)
            {
                _reEntrantFlag = true;
                _paint = false;
                SuspendLayout();

                // Remember where we are.
                int currentCursorPos = SelectionStart;

                // Colorize the JSON statement.
                SelectionColor = Color.Black;

                HighlightCodeText();

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

        #region Protected Virtual Methods

        #endregion

        #region Public Methods / Functions        
        /// <summary>
        /// Attempts to format and display the code/text.
        /// </summary>
        public virtual void Format()
        {
            OnClearErrors();

            if (SyntaxProvider != null)
            {
                IOperationalResult<string?> result = SyntaxProvider.FormatCode(Text);
                if (result.Success)
                {
                    Text = result.DataContent;
                }
                else
                {
                    if (result.Exceptions != null)
                    {
                        foreach (Exception ex in result.Exceptions)
                            OnParsingError(ex);
                    }
                }
            }
        }
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

        #region Protected Event Methods
        /// <summary>
        /// Raises the <see cref="ClearErrors"/> event.
        /// </summary>
        protected virtual void OnClearErrors()
        {
            ClearErrors?.Invoke(this, EventArgs.Empty);
        }
        /// <summary>
        /// Raises the <see cref="ParsingError"/> event.
        /// </summary>
        /// <param name="ex">
        /// The <see cref="Exception"/> instance that was caught.
        /// </param>
        protected virtual void OnParsingError(Exception ex)
        {
            ParsingError?.Invoke(this, ex.Message);
            if (ex.InnerException != null)
                OnParsingError(ex.InnerException);
        }
        /// <summary>
        /// Raises the <see cref="ParsingError"/> event.
        /// </summary>
        protected virtual void OnParsingError()
        {
            ParsingError?.Invoke(this, string.Empty);
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
                if (pos > -1)
                {
                    Select(pos, word.Length);
                    SelectionColor = color;
                    pos += wordLen;
                }

            } while (pos > 0 && pos < len);
        }
        /// <summary>
        /// Colorizes and highlights the JSON keywords and special values in the JSON text.
        /// </summary>
        private void HighlightCodeText()
        {
            if (SyntaxProvider != null)
            {
                foreach (string word in SyntaxProvider.WordList)
                {
                    HighlightWord(word, SyntaxProvider.Colors[word]);
                }

                if (SyntaxProvider.SupportsComments)
                {
                    ColorizeCommentBlocks();
                    ColorizeCommentLines();
                }

                ForeColor = Color.Black;
            }
        }
        /// <summary>
        /// Colorizes the comment lines.
        /// </summary>
        private void ColorizeCommentLines()
        {
            if (SyntaxProvider != null && SyntaxProvider.CommentLineStartDelimiter != null && SyntaxProvider.CommentLineEndDelimiter != null)
            {
                int pos = 0;
                int len = Text.Length - 1;
                do
                {
                    // Find each line that starts with or contains "--"
                    pos = Find(SyntaxProvider.CommentLineStartDelimiter, pos, RichTextBoxFinds.None);
                    if (pos > -1)
                    {
                        // Find the end of line.
                        int rnPos = Find(SyntaxProvider.CommentLineEndDelimiter, pos + 1, RichTextBoxFinds.None);
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
        }
        /// <summary>
        /// Colorizes the comment blocks.
        /// </summary>
        private void ColorizeCommentBlocks()
        {
            if (SyntaxProvider != null && SyntaxProvider.CommentBlockStart != null && SyntaxProvider.CommentBlockEnd != null)
            {
                int pos = 0;
                int endPos = -1;
                int textLen = Text.Length - 1;
                do
                {
                    // Find each /*  and */ pair.
                    pos = Find(SyntaxProvider.CommentBlockStart, pos, RichTextBoxFinds.None);
                    endPos = Find(SyntaxProvider.CommentBlockEnd, pos + 1, RichTextBoxFinds.None);
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
        }
        #endregion

    }
}