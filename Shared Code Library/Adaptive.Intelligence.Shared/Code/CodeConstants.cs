namespace Adaptive.Intelligence.Shared.Code
{
    /// <summary>
    /// Contains the constants for various keywords and symbols for .NET code.
    /// </summary>
    public static class CodeConstants
    {
        #region C#        
        /// <summary>
        /// The C# namespace keyword.
        /// </summary>
        public const string CsNamespace = "namespace ";
        /// <summary>
        /// The C# using keyword.
        /// </summary>
        public const string CsUsing = "using ";

        /// <summary>
        /// The C# end of statement string.
        /// </summary>
        public const string CsLineEnd = ";";
        /// <summary>
        /// The C# block start string.
        /// </summary>
        public const string CsBlockStart = "{";
        /// <summary>
        /// The C# block end string.
        /// </summary>
        public const string CsBlockEnd = "}";
        /// <summary>
        /// The C# XML comment param start tag.
        /// </summary>
        public const string CsXmlParamStart = "/// <param name=\"{0}\">";
        /// <summary>
        /// The C# XML comment param closing tag.
        /// </summary>
        public const string CsXmlParamEnd = "/// </param>";
        /// <summary>
        /// The C# XML comment remarks start tag.
        /// </summary>
        public const string CsXmlRemarksStart = "/// <remarks>";
        /// <summary>
        /// The C# comment remarks end tag.
        /// </summary>
        public const string CsXmlRemarksEnd = "/// </remarks>";
        /// <summary>
        /// The C# XML comment returns start tag.
        /// </summary>
        public const string CsXmlReturnsStart = "/// <returns>";
        /// <summary>
        /// The C# comment returns end tag.
        /// </summary>
        public const string CsXmlReturnsEnd = "/// </returns>";
        /// <summary>
        /// The C# XML comment summary start tag.
        /// </summary>
        public const string CsXmlSummaryStart = "/// <summary>";
        /// <summary>
        /// The C# comment summary end tag.
        /// </summary>
        public const string CsXmlSummaryEnd = "/// </summary>";
        /// <summary>
        /// The C# XML generic comment line.
        /// </summary>
        public const string CsXmlComment = "/// {0}";
        /// <summary>
        /// The C# XML comment See Also tag.
        /// </summary>
        public const string CsXmlSeeAlso = "/// <seealso cref=\"{0}\"/>";
        /// <summary>
        /// The C# region start text.
        /// </summary>
        public const string CsRegionStart = "#region {0}";
        /// <summary>
        /// The C# end region text.
        /// </summary>
        public const string CsRegionEnd = "#endregion";
        #endregion
    }
}
