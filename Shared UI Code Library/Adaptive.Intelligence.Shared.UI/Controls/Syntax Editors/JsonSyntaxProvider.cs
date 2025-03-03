// Ignore Spelling: Json

using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Adaptive.Intelligence.Shared.UI
{
    /// <summary>
    /// Provides a class for providing the syntax coloring rules for JSON text.
    /// </summary>
    /// <seealso cref="ISyntaxProvider" />
    public class JsonSyntaxProvider : DisposableObjectBase, ISyntaxProvider
    {
        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonSyntaxProvider"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public JsonSyntaxProvider()
        {
            InitWordList();
            InitColorList();
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
        /// <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            Colors.Clear();
            WordList.Clear();
            CommentBlockEnd = null;
            CommentBlockStart = null;
            CommentLineEndDelimiter = null;
            CommentLineStartDelimiter = null;

            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets a value that indicates whether an extra comma at the end of a list
        /// of JSON values in an object or array is allowed (and ignored) within the JSON
        /// payload being deserialized.
        /// </summary>
        /// <returns>
        /// true if an extra comma at the end of a list of JSON values in an object or array
        /// is allowed (and ignored); false otherwise.
        /// </returns>
        public bool AllowTrailingCommas { get; set; } = true;

        /// <summary>
        /// Gets the reference to the string to color dictionary.
        /// </summary>
        /// <value>
        /// A <see cref="Dictionary{TKey, TValue}" /> of string key values as listed in
        /// the <see cref="WordList" /> property along with the associated color.
        /// </value>
        public Dictionary<string, Color> Colors { get; private set; } = new Dictionary<string, Color>();

        /// <summary>
        /// Gets the comment block end delimiter.
        /// </summary>
        /// <value>
        /// A string containing the comment block end value, or <b>null</b> if comments are not used.
        /// </value>
        public string? CommentBlockEnd { get; private set; } = JsonConstants.CommentBlockEnd;

        /// <summary>
        /// Gets the comment block start delimiter.
        /// </summary>
        /// <value>
        /// A string containing the comment block start value, or <b>null</b> if comments are not used.
        /// </value>
        public string? CommentBlockStart { get; private set; } = JsonConstants.CommentBlockStart;

        /// <summary>
        /// Gets the ending delimiter for a comment line.
        /// </summary>
        /// <value>
        /// A string containing the comment single line end value, or <b>null</b> if comments are not used.
        /// </value>
        public string? CommentLineEndDelimiter { get; private set; } = System.Environment.NewLine;

        /// <summary>
        /// Gets the starting delimiter for a comment line.
        /// </summary>
        /// <value>
        /// A string containing the comment single line start value, or <b>null</b> if comments are not used.
        /// </value>
        public string? CommentLineStartDelimiter { get; private set; } = JsonConstants.CommentLineDelimiter;

        /// <summary>
        /// Gets or sets the reference to the java script encoder to use for JSON.
        /// </summary>
        /// <value>
        /// The <see cref="JavaScriptEncoder"/> instance to use.
        /// </value>
        public JavaScriptEncoder Encoder { get; set; } = JavaScriptEncoder.Default;

        /// <summary>
        /// Determines whether fields are handled on serialization and deserialization.
        /// The default value is false.
        /// </summary>
        /// <value>
        /// <b>true</b> to include fields on serialization; otherwise, <b>false</b>.
        /// </value>
        public bool IncludeFields { get; set; } = true;

        /// <summary>
        /// Specifies the policy used to convert a property's name on an object to another format, such as camel-casing.
        /// The resulting property name is expected to match the JSON payload during deserialization, and
        /// will be used when writing the property name during serialization.
        /// </summary>
        /// <remarks>
        /// The policy is not used for properties that have a <see cref="JsonPropertyNameAttribute"/> applied.
        /// This property can be set to <see cref="JsonNamingPolicy.CamelCase"/> to specify a camel-casing policy.
        /// </remarks>
        /// <value>
        /// A <see cref="JsonNamingPolicy"/> enumerated value.
        /// </value>
        public JsonNamingPolicy PropertyNamingPolicy { get; set; } = JsonNamingPolicy.CamelCase;

        /// <summary>
        /// Gets a value indicating whether comments are supported.
        /// </summary>
        /// <value>
        ///   <c>true</c> if comments are supported; otherwise, <c>false</c>.
        /// </value>
        public bool SupportsComments { get; private set; } = true;

        /// <summary>
        /// Gets the reference to the list of keywords and other values to highlight.
        /// </summary>
        /// <value>
        /// A <see cref="List{T}" /> of <see cref="string" /> containing the string values
        /// to be highlighted.
        /// </value>
        public List<string> WordList { get; private set; } = new List<string>();
        /// <summary>
        /// Defines whether JSON should pretty print which includes:
        /// indenting nested JSON tokens, adding new lines, and adding white space between property names and values.
        /// By default, the JSON is serialized without any extra white space.
        /// </summary>
        /// <value>
        /// <b>true</b> to write indentations; otherwise, returns <b>false</b>.
        /// </value>
        public bool WriteIndented { get; set; } = true;
        #endregion

        #region Public Methods / Functions        
        /// <summary>
        /// Formats the code/text value.
        /// </summary>
        /// <param name="originalContent">A string containing the original content to be formatted.</param>
        /// <returns>
        /// An <see cref="IOperationalResult{T}" /> of <see cref="string" /> containing the formatted version of
        /// the text if successful; otherwise, contains the list of exceptions.
        /// </returns>
        public IOperationalResult<string?> FormatCode(string originalContent)
        {
            OperationalResult<string?> result = new OperationalResult<string?>();

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                AllowTrailingCommas = AllowTrailingCommas,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                IncludeFields = IncludeFields,
                PropertyNamingPolicy = PropertyNamingPolicy,
                WriteIndented = true,
            };

            try
            {
                var jObject = JsonSerializer.Deserialize<JsonElement>(originalContent, options);
                string formattedText = JsonSerializer.Serialize(jObject, options);
                result.Success = true;
                result.DataContent = formattedText;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.AddException(ex);
            }

            return result;
        }
        #endregion

        #region Private Methods / Functions        
        /// <summary>
        /// Initializes the keyword to color dictionary.
        /// </summary>
        private void InitColorList()
        {
            Colors = new Dictionary<string, Color>();
            Colors.Add(JsonConstants.BraceOpen, Color.Purple);
            Colors.Add(JsonConstants.BraceClose, Color.Purple);
            Colors.Add(JsonConstants.BracketOpen, Color.Blue);
            Colors.Add(JsonConstants.BracketClose, Color.Blue);
            Colors.Add(JsonConstants.Comma, Color.Magenta);
            Colors.Add(JsonConstants.DoubleQuote, Color.Green);
            Colors.Add(JsonConstants.CommentBlockEnd, Color.Green);
            Colors.Add(JsonConstants.CommentBlockStart, Color.Green);
            Colors.Add(JsonConstants.CommentLineDelimiter, Color.Green);
            Colors.Add(JsonConstants.Dot, Color.Red);
            Colors.Add(JsonConstants.KeywordArray, Color.Blue);
            Colors.Add(JsonConstants.KeywordBoolean, Color.Blue);
            Colors.Add(JsonConstants.KeywordBoolFalse, Color.Blue);
            Colors.Add(JsonConstants.KeywordBoolTrue, Color.Blue);
            Colors.Add(JsonConstants.KeywordId, Color.Blue);
            Colors.Add(JsonConstants.KeywordNull, Color.Blue);
            Colors.Add(JsonConstants.KeywordType, Color.Blue);
        }
        /// <summary>
        /// Initializes the syntax keyword list.
        /// </summary>
        private void InitWordList()
        {
            WordList = new List<string>
            {
                JsonConstants.BraceOpen,
                JsonConstants.BraceClose,
                JsonConstants.BracketOpen,
                JsonConstants.BracketClose,
                JsonConstants.Comma,
                JsonConstants.DoubleQuote,
                JsonConstants.CommentBlockEnd,
                JsonConstants.CommentBlockStart,
                JsonConstants.CommentLineDelimiter,
                JsonConstants.Dot,
                JsonConstants.KeywordArray,
                JsonConstants.KeywordBoolean,
                JsonConstants.KeywordBoolFalse,
                JsonConstants.KeywordBoolTrue,
                JsonConstants.KeywordId,
                JsonConstants.KeywordNull,
                JsonConstants.KeywordType
            };
        }
        #endregion
    }
}
