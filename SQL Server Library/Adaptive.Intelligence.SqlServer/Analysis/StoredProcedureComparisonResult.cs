using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.SqlServer.Analysis
{
    /// <summary>
    /// Contains the result from comparing a stored procedure across databases.
    /// </summary>
    public sealed class StoredProcedureComparisonResult : DisposableObjectBase
    {
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <b>true</b> to release both managed and unmanaged resources;
        /// <b>false</b> to release only unmanaged resources.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            LeftText = null;
            RightText = null;
            StoredProcedureName = null;
            base.Dispose(disposing);
        }
        /// <summary>
        /// Gets or sets a value indicating whether the stored procedure text is
        /// different from the development version in the test version.
        /// </summary>
        /// <value>
        ///   <b>true</b> if the stored procedure text is different from the development version in the test version;
        ///   otherwise, <b>false</b>.
        /// </value>
        public bool DifferentFromLeft { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the stored procedure text is
        /// different from the development version in the production version.
        /// </summary>
        /// <value>
        ///   <b>true</b> if the stored procedure text is different from the development version in the production version;
        ///   otherwise, <b>false</b>.
        /// </value>
        public bool DifferentFromRight { get; set; }
        /// <summary>
        /// Gets a value indicating whether the stored procedure is defined differently in any database.
        /// </summary>
        /// <value>
        ///   <b>true</b> if the stored procedure definitions in the databases have differences; otherwise, <b>false</b>.
        /// </value>
        public bool HasDifferences => (DifferentFromLeft || DifferentFromRight);
        /// <summary>
        /// Gets or sets the text of the left-side stored procedure being compared.
        /// </summary>
        /// <value>
        /// A string containing the text of the stored procedure.
        /// </value>
        public string? LeftText { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the stored procedure is missing in the
        /// development database.
        /// </summary>
        /// <value>
        ///   <b>true</b> if the stored procedure is missing in development; otherwise, <b>false</b>.
        /// </value>
        public bool MissingInLeft { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the stored procedure is missing in the
        /// test database.
        /// </summary>
        /// <value>
        ///   <b>true</b> if the stored procedure is missing in Test; otherwise, <b>false</b>.
        /// </value>
        public bool MissingInRight { get; set; }
        /// <summary>
        /// Gets or sets the text of the right-side stored procedure being compared.
        /// </summary>
        /// <value>
        /// A string containing the text of the stored procedure.
        /// </value>
        public string? RightText { get; set; }
        /// <summary>
        /// Gets or sets the name of the stored procedure.
        /// </summary>
        /// <value>
        /// A string containing the name of the stored procedure.
        /// </value>
        public string? StoredProcedureName { get; set; }
    }
}
