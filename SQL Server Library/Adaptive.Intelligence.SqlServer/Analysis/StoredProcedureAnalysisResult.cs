using Adaptive.Intelligence.Shared;
using System.Collections.Specialized;
using System.Text;

namespace Adaptive.Intelligence.SqlServer.Analysis
{
    /// <summary>
    /// Contains the results from analyzing the stored procedure contents across two databases.
    /// </summary>
    /// <seealso cref="DisposableObjectBase" />
    public sealed class StoredProcedureAnalysisResult : DisposableObjectBase
    {
        #region Private Member Declarations        
        /// <summary>
        /// The primary database name.
        /// </summary>
        private string? _primaryDatabase;
        /// <summary>
        /// The secondary database name.
        /// </summary>
        private string? _secondaryDatabase;
        /// <summary>
        /// The comparisons list, keyed by stored procedure name.
        /// </summary>
        private Dictionary<string, StoredProcedureComparisonResult>? _comparisons;
        /// <summary>
        /// The complete list of stored procedure names.
        /// </summary>
        private StringCollection? _completeNameList;
        /// <summary>
        /// The list of stored procedures in the primary database.
        /// </summary>
        private Dictionary<string, StringCollection>? _primaryProcedures;
        /// <summary>
        /// The list of stored procedures in the secondary database.
        /// </summary>
        private Dictionary<string, StringCollection>? _secondaryProcedures;
        /// <summary>
        /// The list of stored procedures present in the secondary database but missing in the primary.
        /// </summary>
        private StringCollection? _missingInPrimary;
        /// <summary>
        /// The list of stored procedures present in the primary database but missing in the secondary.
        /// </summary>
        private StringCollection? _missingInSecondary;
        #endregion

        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="StoredProcedureAnalysisResult"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public StoredProcedureAnalysisResult()
        {
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
                _primaryProcedures?.Clear();
                _secondaryProcedures?.Clear();
                _comparisons?.Clear();
                _completeNameList?.Clear();
                _missingInPrimary?.Clear();
                _missingInSecondary?.Clear();
            }

            _primaryProcedures = null;
            _secondaryProcedures = null;
            _comparisons = null;
            _completeNameList = null;
            _missingInPrimary = null;
            _missingInSecondary = null;
            _primaryDatabase = null;
            _secondaryDatabase = null;

            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the reference to the comparison results for the stored procedures.
        /// </summary>
        /// <value>
        /// A <see cref="Dictionary{TKey, TValue}"/> of <see cref="string"/> and
        /// <see cref="StoredProcedureComparisonResult"/> instances.
        /// </value>
        public Dictionary<string, StoredProcedureComparisonResult>? Comparisons => _comparisons;
        /// <summary>
        /// Gets the reference to the complete stored procedure name list.
        /// </summary>
        /// <value>
        /// A <see cref="StringCollection"/> containing the name values.
        /// </value>
        public StringCollection? CompleteNameList => _completeNameList;
        /// <summary>
        /// Gets the name of the first / left side database for comparison.
        /// </summary>
        /// <value>
        /// A string containing the name of the database.
        /// </value>
        public string? PrimaryDatabaseName => _primaryDatabase;
        /// <summary>
        /// Gets or sets the reference to the list of the first / left database stored procedures.
        /// </summary>
        /// <value>
        /// A <see cref="Dictionary{TKey, TValue}"/> where the stored procedure name is the
        /// key, and the value is a <see cref="StringCollection"/> containing the procedure code.
        /// </value>
        public Dictionary<string, StringCollection>? PrimaryDatabaseProcedures
        {
            get => _primaryProcedures;
            set => _primaryProcedures = value;
        }
        /// <summary>
        /// Gets the reference to the list of stored procedures that are missing in the first / left database.
        /// </summary>
        /// <value>
        /// A <see cref="List{T}"/> of <see cref="string"/> containing the stored procedure names.
        /// </value>
        public StringCollection? MissingInPrimary => _missingInPrimary;
        /// <summary>
        /// Gets the reference to the list of stored procedures that are missing in the right / second database.
        /// </summary>
        /// <value>
        /// A <see cref="List{T}"/> of <see cref="string"/> containing the stored procedure names.
        /// </value>
        public StringCollection? MissingInSecondary => _missingInSecondary;
        /// <summary>
        /// Gets the name of the second / right side database for comparison.
        /// </summary>
        /// <value>
        /// A string containing the name of the database.
        /// </value>
        public string? SecondaryDatabaseName => _secondaryDatabase;
        /// <summary>
        /// Gets or sets the reference to the list of the second / right database stored procedures.
        /// </summary>
        /// <value>
        /// A <see cref="Dictionary{TKey, TValue}"/> where the stored procedure name is the
        /// key, and the value is a <see cref="StringCollection"/> containing the procedure code.
        /// </value>
        public Dictionary<string, StringCollection>? SecondaryDatabaseProcedures
        {
            get => _secondaryProcedures;
            set => _secondaryProcedures = value;
        }
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Analyzes the loaded stored procedure contents to determine which procedures are missing from
        /// which databases, and which stored procedure text content does not match in each database.
        /// </summary>
        public void Analyze(string primaryDatabase, string secondaryDatabase)
        {
            _primaryDatabase = primaryDatabase;
            _secondaryDatabase = secondaryDatabase;

            // Generate a complete list of stored procedure names.
            CreateCompleteNameList();

            // Find SPs missing in each database.
            FindMissing();

            // Compare the existing SP text to each other.
            CompareSpText();

        }
        #endregion

        #region Private Member Declarations
        /// <summary>
        /// Compares the text of each stored procedure version in each DB, when present, to determine
        /// if there are differences.
        /// </summary>
        private void CompareSpText()
        {
            // Left to Right.
            CompareLeftToRight();
        }
        /// <summary>
        /// Creates the complete list of all stored procedure names.
        /// </summary>
        private void CreateCompleteNameList()
        {
            _completeNameList = new StringCollection();
            if (_primaryProcedures != null)
            {
                foreach (string key in _primaryProcedures.Keys)
                {
                    if (!_completeNameList.Contains(key))
                    {
                        _completeNameList.Add(key);
                    }
                }
            }

            if (_secondaryProcedures != null)
            {
                foreach (string key in _secondaryProcedures.Keys)
                {
                    if (!_completeNameList.Contains(key))
                    {
                        _completeNameList.Add(key);
                    }
                }
            }
        }
        /// <summary>
        /// Determines which stored procedures are missing from each database.
        /// </summary>
        private void FindMissing()
        {
            // Determine which procedures are missing in each database.
            _missingInPrimary = new StringCollection();
            _missingInSecondary = new StringCollection();
            _comparisons = new Dictionary<string, StoredProcedureComparisonResult>();

            if (_completeNameList != null)
            {
                foreach (string? storedProcName in _completeNameList)
                {
                    if (storedProcName != null)
                    {
                        // Get or create the reference to the stored procedure info.
                        StoredProcedureComparisonResult? result;
                        if (!_comparisons.ContainsKey(storedProcName))
                        {
                            result = new StoredProcedureComparisonResult
                            {
                                StoredProcedureName = storedProcName
                            };
                            _comparisons.Add(storedProcName, result);
                        }
                        else
                        {
                            result = _comparisons[storedProcName];
                        }

                        // Determine whether the procedure is missing in the list of procedures for the primary database.
                        if (_primaryProcedures != null && !_primaryProcedures.ContainsKey(storedProcName))
                        {
                            _missingInPrimary.Add(storedProcName);
                            result.MissingInLeft = true;
                        }

                        // Determine whether the procedure is missing in the list of procedures for the secondary database.
                        if (_secondaryProcedures != null && !_secondaryProcedures.ContainsKey(storedProcName))
                        {
                            _missingInSecondary.Add(storedProcName);
                            result.MissingInRight = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Compares the existing stored procedures in Test and Development.
        /// </summary>
        private void CompareLeftToRight()
        {
            if (_primaryProcedures != null && _secondaryProcedures != null && _completeNameList != null)
            {
                if (_comparisons == null)
                {
                    _comparisons = new Dictionary<string, StoredProcedureComparisonResult>();
                }

                foreach (string? storedProcedureName in _completeNameList)
                {
                    if (storedProcedureName != null)
                    {
                        if (_primaryProcedures.ContainsKey(storedProcedureName) &&
                            _secondaryProcedures.ContainsKey(storedProcedureName))
                        {
                            // Compare the text.
                            StringCollection leftVersion = _primaryProcedures[storedProcedureName];
                            StringCollection rightVersion = _secondaryProcedures[storedProcedureName];
                            bool different = CompareProcedureText(leftVersion, rightVersion);

                            // Store the results.
                            StoredProcedureComparisonResult compareResult = _comparisons[storedProcedureName];
                            compareResult.DifferentFromLeft = different;
                            compareResult.LeftText = ToText(leftVersion);
                            compareResult.RightText = ToText(rightVersion);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Compares the existing stored procedures in Production and Development.
        /// </summary>
        private void CompareRightToLeft()
        {
            if (_primaryProcedures != null && _secondaryProcedures != null && _completeNameList != null)
            {
                if (_comparisons == null)
                {
                    _comparisons = new Dictionary<string, StoredProcedureComparisonResult>();
                }

                foreach (string? storedProcedureName in _completeNameList)
                {
                    if (storedProcedureName != null)
                    {
                        if (_primaryProcedures.ContainsKey(storedProcedureName) &&
                            _secondaryProcedures.ContainsKey(storedProcedureName))
                        {
                            // Compare the text.
                            StringCollection leftVersion = _primaryProcedures[storedProcedureName];
                            StringCollection rightVersion = _secondaryProcedures[storedProcedureName];
                            bool different = CompareProcedureText(leftVersion, rightVersion);

                            // Store the results.
                            StoredProcedureComparisonResult compareResult = _comparisons[storedProcedureName];
                            compareResult.DifferentFromRight = different;
                            compareResult.LeftText = ToText(leftVersion);
                            compareResult.RightText = ToText(rightVersion);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Determines whether the text values are different.
        /// </summary>
        /// <param name="leftSide">The left side.</param>
        /// <param name="rightSide">The right side.</param>
        /// <returns></returns>
        private bool CompareProcedureText(StringCollection leftSide, StringCollection rightSide)
        {
            string leftText = GetStringForComparison(leftSide);
            string rightText = GetStringForComparison(rightSide);

            return (string.Compare(leftText, rightText, StringComparison.OrdinalIgnoreCase) != 0);
        }
        #endregion

        #region Private Member Declarations        
        /// <summary>
        /// Gets the SQL string for comparison.
        /// </summary>
        /// <param name="items">
        /// A <see cref="StringCollection"/> containing the list of lines that make up the stored procedure text.
        /// </param>
        /// <returns>
        /// A string containing the SP code that removes the whitespace characters.
        /// </returns>
        private string GetStringForComparison(StringCollection items)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string? line in items)
            {
                if (line != null)
                {
                    // TODO: Make this far more efficient.
                    string modLine = line.Replace(
                        Constants.CrLf, Constants.Space)
                        .Replace(Constants.Linefeed, Constants.Space)
                        .Replace(Constants.CarriageReturn, Constants.Space)
                        .Replace(Constants.Tab, Constants.Space)
                        .Replace(Constants.TwoSpaces, Constants.Space)
                        .Replace(Constants.TwoSpaces, Constants.Space).ToLower().Trim();

                    builder.Append(modLine + Constants.Space);
                }
            }
            return builder.ToString().Trim();
        }
        /// <summary>
        /// Converts the string collection content to a single string.
        /// </summary>
        /// <param name="text">
        /// A <see cref="StringCollection"/> containing the list of lines that make up the stored procedure text.
        /// </param>
        /// <returns>
        /// A string containing the concatenated text.
        /// </returns>
        private string ToText(StringCollection text)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string? line in text)
            {
                if (line != null)
                {
                    builder.AppendLine(line);
                }
            }
            return builder.ToString();
        }
        #endregion
    }
}