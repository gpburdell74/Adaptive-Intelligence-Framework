using Adaptive.CodeDom.Properties;

namespace Adaptive.CodeDom
{
    /// <summary>
    /// Provides constants definitions for the library.
    /// </summary>
    public static class CodeDomConstants
    {
        /// <summary>
        /// The default object name.
        /// </summary>
        public static string DefaultObjectName = "System.Object";
        /// <summary>
        /// The standard disposable interface name.
        /// </summary>
        public static string DisposableInterface = "IDisposable";

        #region Default Region Names
        /// <summary>
        /// The Public Events region name text.
        /// </summary>
        public static string RegionPublicEvents = Resources.RegionPublicEvents;
        /// <summary>
        /// The Private Constants region name text.
        /// </summary>
        public static string RegionPrivateConstants = Resources.RegionPrivateConstants;
        /// <summary>
        /// The Private Member Declarations region name text.
        /// </summary>
        public static string RegionPrivateMembers = Resources.RegionPrivateMembers;
        /// <summary>
        /// The Constructor / Dispose Methods region name text.
        /// </summary>
        public static string RegionConstructorDispose = Resources.RegionConstructorDispose;
        /// <summary>
        /// The Public Properties region name text.
        /// </summary>
        public static string RegionPublicProperties = Resources.RegionPublicProperties;
        /// <summary>
        /// The Protected Properties region name text.
        /// </summary>
        public static string RegionProtectedProperties = Resources.RegionProtectedProperties;
        /// <summary>
        /// The Abstract Methods / Functions region name text.
        /// </summary>
        public static string RegionAbstractMethods = Resources.RegionAbtractMethods;
        /// <summary>
        /// The Protected Methods / Functions region name text.
        /// </summary>
        public static string RegionProtectedMethods = Resources.RegionProtectedMethods;
        /// <summary>
        /// The Public Methods / Functions region name text.
        /// </summary>
        public static string RegionPublicMethods = Resources.RegionPublicMethods;
        /// <summary>
        /// The Private Methods / Functions region name text.
        /// </summary>
        public static string RegionPrivateMethods = Resources.RegionPrivateMethods;
        /// <summary>
        /// The Event Handlers region name text.
        /// </summary>
        public static string RegionEventHandlers = Resources.RegionEventHandlers;
        /// <summary>
        /// The Event Methods region name text.
        /// </summary>
        public static string RegionEventMethods = Resources.RegionEventMethods;
        #endregion

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

        #region Visual Basic
        /// <summary>
        /// The C# namespace keyword.
        /// </summary>
        public const string VbNamespace = "Namespace ";
        /// <summary>
        /// The C# using keyword.
        /// </summary>
        public const string VbUsing = "Imports ";

        /// <summary>
        /// The C# end of statement string.
        /// </summary>
        public const string VbLineEnd = "\r\n";
        /// <summary>
        /// The C# block start string.
        /// </summary>
        public const string VbBlockStart = "\t";
        /// <summary>
        /// The C# block end string.
        /// </summary>
        public const string VbBlockEnd = "End ";
        /// <summary>
        /// The C# XML comment param start tag.
        /// </summary>
        public const string VbXmlParamStart = "''' <param name=\"{0}\">";
        /// <summary>
        /// The C# XML comment param closing tag.
        /// </summary>
        public const string VbXmlParamEnd = "''' </param>";
        /// <summary>
        /// The C# XML comment remarks start tag.
        /// </summary>
        public const string VbXmlRemarksStart = "''' <remarks>";
        /// <summary>
        /// The C# comment remarks end tag.
        /// </summary>
        public const string VbXmlRemarksEnd = "''' </remarks>";
        /// <summary>
        /// The C# XML comment returns start tag.
        /// </summary>
        public const string VbXmlReturnsStart = "''' <returns>";
        /// <summary>
        /// The C# comment returns end tag.
        /// </summary>
        public const string VbXmlReturnsEnd = "''' </returns>";
        /// <summary>
        /// The C# XML comment summary start tag.
        /// </summary>
        public const string VbXmlSummaryStart = "''' <summary>";
        /// <summary>
        /// The C# comment summary end tag.
        /// </summary>
        public const string VbXmlSummaryEnd = "''' </summary>";
        /// <summary>
        /// The C# XML generic comment line.
        /// </summary>
        public const string VbXmlComment = "''' {0}";
        /// <summary>
        /// The C# XML comment See Also tag.
        /// </summary>
        public const string VbXmlSeeAlso = "''' <seealso cref=\"{0}\"/>";
        /// <summary>
        /// The C# region start text.
        /// </summary>
        public const string VbRegionStart = "# Region {0}";
        /// <summary>
        /// The C# end region text.
        /// </summary>
        public const string VbRegionEnd = "# End Region";
        #endregion
    }
}