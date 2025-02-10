using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.SqlServer.Schema
{
    /// <summary>
    /// Contains the results of searching for a table name.
    /// </summary>
    /// <seealso cref="DisposableObjectBase" />
    internal sealed class TableNameComparisonResult : DisposableObjectBase
    {
        #region Dispose Method        
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
        /// <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            Table = null;
            ComparedTo = null;
            CandidateValue = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties        
        /// <summary>
        /// Gets or sets the candidate string value.
        /// </summary>
        /// <value>
        /// A string containing the whole or partial name that was searched on.
        /// </value>
        public string? CandidateValue { get; set; }
        /// <summary>
        /// Gets or sets the value that was compared to.
        /// </summary>
        /// <value>
        /// A string containing the name of the table that was matched.
        /// </value>
        public string? ComparedTo { get; set; }
        /// <summary>
        /// Gets or sets the sureness of the match.
        /// </summary>
        /// <value>
        /// An integer between 0 and 100 that indicates the likelihood of the match.
        /// </value>
        public int Sureness { get; set; }
        /// <summary>
        /// Gets or sets the reference to the matched table.
        /// </summary>
        /// <value>
        /// The <see cref="SqlTable"/> instance that is the closest match, or <b>null</b>
        /// if one could not be matched.
        /// </value>
        public SqlTable? Table { get; set; }
        #endregion

        #region Public Methods / Functions        
        /// <summary>
        /// Adjusts the sureness value based on the content of the original string values.
        /// </summary>
        public void Adjust()
        {
            // If the matched table name is longer than the original candidate value,
            // subtract the string length.
            if (ComparedTo != null && CandidateValue != null && ComparedTo.Length > CandidateValue.Length)
            {
                Sureness -= (ComparedTo.Length - CandidateValue.Length);
            }

            // If the first characters do not match, subtract 1 value from the Sureness value.
            if (ComparedTo != null && CandidateValue != null && ComparedTo.Substring(0, 1) != CandidateValue.Substring(0, 1))
            {
                Sureness--;
            }
        }
        #endregion
    }
}
