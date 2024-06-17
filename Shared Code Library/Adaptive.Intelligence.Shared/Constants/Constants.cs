namespace Adaptive.Intelligence.Shared
{
	/// <summary>
	/// Provides constants definitions for the application.
	/// </summary>
	public static class Constants
	{
		#region Character Constants		
		/// <summary>
		/// The backspace character.
		/// </summary>
		public const char BackspaceChar = (char)8;
		/// <summary>
		/// The backward slash string.
		/// </summary>
		public const char BackslashChar = '\\';
		/// <summary>
		/// The close parenthesis string.
		/// </summary>
		public const char CloseParenChar = ')';
		/// <summary>
		/// The comma character.
		/// </summary>
		public const char CommaChar = ',';
		/// <summary>
		/// The dash string.
		/// </summary>
		public const char DashChar = '-';
		/// <summary>
		/// The dot string.
		/// </summary>
		public const char DotChar = '.';
		/// <summary>
		/// The null character.
		/// </summary>
		public const char NullChar = '\0';
		/// <summary>
		/// The open parenthesis string.
		/// </summary>
		public const char OpenParenChar = '(';
		/// <summary>
		/// The forward slash string.
		/// </summary>
		public const char SlashChar = '/';
		/// <summary>
		/// The space string.
		/// </summary>
		public const char SpaceChar = ' ';
		/// <summary>
		/// The single quote character.
		/// </summary>
		public const char SingleQuoteChar = '\'';
		/// <summary>
		/// The tab character.
		/// </summary>
		public const char TabChar = '\t';
		#endregion

		#region Single-Character String Constants				
		/// <summary>
		/// The "@" character.
		/// </summary>
		public const string At = "@";
		/// <summary>
		/// The backward slash string.
		/// </summary>
		public const string Backslash = "\\";
		/// <summary>
		/// The carriage return string.
		/// </summary>
		public const string CarriageReturn = "\r";
		/// <summary>
		/// The close bracket string.
		/// </summary>
		public const string CloseBracket = "]";
		/// <summary>
		/// The close curly bracket string.
		/// </summary>
		public const string CloseCurlyBracket = "}";
		/// <summary>
		/// The close parenthesis string.
		/// </summary>
		public const string CloseParen = ")";
		/// <summary>
		/// The colon string.
		/// </summary>
		public const string Colon = ":";
		/// <summary>
		/// The comma string.
		/// </summary>
		public const string Comma = ",";
		/// <summary>
		/// The comma string with a trailing space.
		/// </summary>
		public const string CommaWithSpace = ", ";
		/// <summary>
		/// The carriage-return and linefeed string.
		/// </summary>
		public const string CrLf = "\r\n";
		/// <summary>
		/// The dash string.
		/// </summary>
		public const string Dash = "-";
		/// <summary>
		/// The dot string.
		/// </summary>
		public const string Dot = ".";
		/// <summary>
		/// The equals sign.
		/// </summary>
		public const string EqualsSign = "=";
		/// <summary>
		/// The linefeed string.
		/// </summary>
		public const string Linefeed = "\n";
		/// <summary>
		/// The percent sign.
		/// </summary>
		public const string PercentSign = "%";
		/// <summary>
		/// The open bracket string.
		/// </summary>
		public const string OpenBracket = "[";
		/// <summary>
		/// The open curly bracket string.
		/// </summary>
		public const string OpenCurlyBracket = "{";
		/// <summary>
		/// The open parenthesis string.
		/// </summary>
		public const string OpenParen = "(";
		/// <summary>
		/// The semi colon string.
		/// </summary>
		public const string SemiColon = ";";
		/// <summary>
		/// The single quote character.
		/// </summary>
		public const string SingleQuote = "'";
		/// <summary>
		/// The forward slash string.
		/// </summary>
		public const string Slash = "/";
		/// <summary>
		/// The space string.
		/// </summary>
		public const string Space = " ";
		/// <summary>
		/// The tab string.
		/// </summary>
		public const string Tab = "\t";
		/// <summary>
		/// A string of two spaces.
		/// </summary>
		public const string TwoSpaces = "  ";
		#endregion

		#region True/False Constants
		/// <summary>
		/// Specifies a possible character value that represents the boolean true value.
		/// </summary>
		public const char TrueY = 'y';
		/// <summary>
		/// Specifies a possible character value that represents the boolean true value.
		/// </summary>
		public const char TrueT = 't';
		/// <summary>
		/// Specifies a possible character value that represents the boolean true value.
		/// </summary>
		public const char True1 = '1';
		/// <summary>
		/// Specifies a possible string value that represents the boolean true value.
		/// </summary>
		public const string TrueValueYes = "yes";
		/// <summary>
		/// Specifies a possible string value that represents the boolean true value.
		/// </summary>
		public const string TrueValueSi = "si";
		/// <summary>
		/// Specifies a possible string value that represents the boolean true value.
		/// </summary>
		public const string TrueValueTrue = "true";
		/// <summary>
		/// Specifies a possible string value that represents the boolean true value.
		/// </summary>
		public const string TrueValueBT = ".t.";
		/// <summary>
		/// Specifies a possible string value that represents the boolean true value.
		/// </summary>
		public const string TrueValueBY = ".y.";
		/// <summary>
		/// Specifies a possible string value that represents the boolean true value.
		/// </summary>
		public const string TrueValueMinus1 = "-1";
		/// <summary>
		/// Specifies a possible string value that represents the boolean true value.
		/// </summary>
		public const string TrueValueOK = "ok";
		/// <summary>
		/// Specifies a possible formatted string value that represents the boolean true value.
		/// </summary>
		public const string TrueFormatted = "Yes";
		/// <summary>
		/// Specifies a possible formatted string value that represents the boolean false value.
		/// </summary>
		public const string FalseFormatted = "No";
		#endregion

		#region Date Constants
		/// <summary>
		/// Specifies the US date format.
		/// </summary>
		public const string USDateFormat = "MM/dd/yyyy";
		/// <summary>
		/// Specifies the US date and time format.
		/// </summary>
		public const string USFullDateFormat = "MM/dd/yyyy hh:mm:ss tt";
		/// <summary>
		/// Specifies the US time format.
		/// </summary>
		public const string USTimeFormat = "h:mm tt";
		#endregion

		#region Data Type Names		
		/// <summary>
		/// The <see cref="DateTimeOffset"/> data type name in lower case.
		/// </summary>
		public const string DataTypeDateTimeOffsetLower = "datetimeoffset";
		/// <summary>
		/// The <see cref="Guid"/> data type name in lower case.
		/// </summary>
		public const string DataTypeGuidLower = "guid";
		#endregion

		#region Value Constants
		/// <summary>
		/// The "NULL" text.
		/// </summary>
		public const string NullText = "NULL";
		/// <summary>
		/// The numeric zero value.
		/// </summary>
		public const string Zero = "0";
		#endregion

		#region File Extension Constants

		#region MS Office Document Types
		/// <summary>
		/// Defines the file extension for a legacy MS Excel spreadsheet.
		/// </summary>
		public const string ExtExcelLegacy = "xls";
		/// <summary>
		/// Defines the file extension for an MS Excel spreadsheet.
		/// </summary>
		public const string ExtExcel = "xlsx";
		/// <summary>
		/// Defines the file extension for an MS Excel macro file.
		/// </summary>
		public const string ExtExcelMacro = "xlsm";
		/// <summary>
		/// Defines the file extension for a legacy MS Excel macro file.
		/// </summary>
		public const string ExtExcelMacroLegacy = "xlm";
		/// <summary>
		/// Defines the file extension for an MS Excel template.
		/// </summary>
		public const string ExtExcelTemplate = "xlst";
		/// <summary>
		/// Defines the file extension for a legacy MS Excel template.
		/// </summary>
		public const string ExtExcelTemplateLegacy = "xlt";
		/// <summary>
		/// Defines the file extension for legacy MS Access database.
		/// </summary>
		public const string ExtMSAccessLegacyDatabase = "mdb";
		/// <summary>
		/// Defines the file extension for an MS Access database.
		/// </summary>
		public const string ExtMSAccessDatabase = "accdb";
		/// <summary>
		/// Defines the file extension for a OneNote package file.
		/// </summary>
		public const string ExtOneNote = "onepkg";
		/// <summary>
		/// Defines the file extension for a OneNote export file.
		/// </summary>
		public const string ExtOneNoteExport = "one";
		/// <summary>
		/// Defines the file extension for a legacy MS PowerPoint file.
		/// </summary>
		public const string ExtPowerPointLegacy = "ppt";
		/// <summary>
		/// Defines the file extension for an MS PowerPoint file.
		/// </summary>
		public const string ExtPowerPoint = "pptx";
		/// <summary>
		/// Defines the file extension for an MS Publisher file.
		/// </summary>
		public const string ExtPublisher = "pub";
		/// <summary>
		/// Defines the file extension for legacy MS Word document.
		/// </summary>
		public const string ExtWordDocumentLegacy = "doc";
		/// <summary>
		/// Defines the file extension for an MS Word document.
		/// </summary>
		public const string ExtWordDocument = "docx";
		/// <summary>
		/// Defines the file extension for an MS Word macro file.
		/// </summary>
		public const string ExtWordMacro = "docm";
		/// <summary>
		/// Defines the file extension for legacy MS Word macro file.
		/// </summary>
		public const string ExtWordLegacyMacro = "dom";
		/// <summary>
		/// Defines the file extension for an MS Word template.
		/// </summary>
		public const string ExtWordTemplate = "doct";
		/// <summary>
		/// Defines the file extension for legacy MS Word template.
		/// </summary>
		public const string ExtWordTemplateLegacy = "dot";
		/// <summary>
		/// Defines the file extension for an MS XML Paper Specification file.
		/// </summary>
		public const string ExtXps = "xps";
		#endregion

		#region Text File Types		
		/// <summary>
		/// Defines the file extension for comma-separated value text file.
		/// </summary>
		public const string ExtCommaSeparatedValues = "csv";
		/// <summary>
		/// Defines the file extension for JSON file.
		/// </summary>
		public const string ExtJavascriptObjectNotation = "json";
		/// <summary>
		/// Defines the file extension for rich text file. 
		/// </summary>
		public const string ExtRichTextFile = "rtf";
		/// <summary>
		/// Defines the file extension for standard text file.
		/// </summary>
		public const string ExtTextFile = "txt";
		#endregion

		#region Executable and OS File Types		
		/// <summary>
		/// Defines the file extension for batch file
		/// </summary>
		public const string ExtBatchFile = "bat";
		/// <summary>
		/// Defines the file extension for Microsoft Command Console file
		/// </summary>
		public const string ExtCommandConsoleFile = "msc";
		/// <summary>
		/// Defines the file extension for command file.
		/// </summary>
		public const string ExtCommandFile = "cmd";
		/// <summary>
		/// Defines the file extension for control panel definition file 
		/// </summary>
		public const string ExtControlPanel = "cpl";
		/// <summary>
		/// Defines the file extension for legacy DOS command/executable file.
		/// </summary>
		public const string ExtDOSCommandFile = "com";
		/// <summary>
		/// Defines the file extension for Windows dynamically-linked library file.
		/// </summary>
		public const string ExtDynamicLinkedLibrary = "dll";
		/// <summary>
		/// Defines the file extension for Windows executable file.
		/// </summary>
		public const string ExtExecutableFile = "exe";
		/// <summary>
		/// Defines the file extension for Windows image file.
		/// </summary>
		public const string ExtImageFile = "iso";
		/// <summary>
		/// Defines the file extension for national language service / font mapping file for Windows
		/// </summary>
		public const string ExtNationalLanguageService = "nls";
		/// <summary>
		/// Defines the file extension for Windows PowerShell file.
		/// </summary>
		public const string ExtPowerShellFile = "ps1";
		/// <summary>
		/// Defines the file extension for Windows registry file.
		/// </summary>
		public const string ExtRegistryFile = "reg";
		/// <summary>
		/// Defines the file extension for general system file or driver.
		/// </summary>
		public const string ExtSystemFile = "sys";
		/// <summary>
		/// Defines the file extension for Windows/Microsoft Installer file.
		/// </summary>
		public const string ExtWindowsInstallerFile = "msi";
		#endregion

		#region Common Image Files		
		/// <summary>
		/// Defines the file extension for bitmap image file.
		/// </summary>
		public const string ExtBitmap = "bmp";
		/// <summary>
		/// Defines the file extension for generic image file.
		/// </summary>
		public const string ExtGenericImage = "img";
		/// <summary>
		/// Defines the file extension for GIF image file.
		/// </summary>
		public const string ExtGraphicsInterchangeFormat = "gif";
		/// <summary>
		/// Defines the file extension for JPEG image file.
		/// </summary>
		public const string ExtJointPhotographicExpertsGroup = "jpg";
		/// <summary>
		/// Defines the file extension for JPEG image file
		/// </summary>
		public const string ExtJointPhotographicExpertsGroupLong = "jpeg";
		/// <summary>
		/// Defines the file extension for PhotoShop image file.
		/// </summary>
		public const string ExtPhotoShop = "psd";
		/// <summary>
		/// Defines the file extension for PNG image file.
		/// </summary>
		public const string ExtPortableNetworkGraphics = "png";
		/// <summary>
		/// Defines the file extension for raw image file.
		/// </summary>
		public const string ExtRawImage = "raw";
		/// <summary>
		/// Defines the file extension for TIFF image file.
		/// </summary>
		public const string ExtTaggedImageFileFormat = "tiff";
		/// <summary>
		/// Defines the file extension for Windows Icon image file.
		/// </summary>
		public const string ExtWindowsIcon = "ico";
		#endregion

		#region Common Video Files		
		/// <summary>
		/// Defines the file extension for video file.
		/// </summary>
		public const string ExtMovieFile = "mov";
		/// <summary>
		/// Defines the file extension for an MPEG (v1) video file.
		/// </summary>
		public const string ExtMPeg = "mpg";
		/// <summary>
		/// Defines the file extension for an MPEG (v1) video file.
		/// </summary>
		public const string ExtMPegLong = "mpeg";
		/// <summary>
		/// Defines the file extension for an MPEG (v2) video file.
		/// </summary>
		public const string ExtMPegV2 = "mp2";
		/// <summary>
		/// Defines the file extension for an MPEG (v3) video file.
		/// </summary>
		public const string ExtMPegV3 = "mp3";
		/// <summary>
		/// Defines the file extension for an MPEG (v4) video file.
		/// </summary>
		public const string ExtMPegV4 = "mp4";
		/// <summary>
		/// Defines the file extension for Windows Movie file.
		/// </summary>
		public const string ExtWindowsMovieFile = "wmv";
		#endregion

		#region Common Compressed Data File Types		
		/// <summary>
		/// Defines the file extension for cabinet compression archive file.
		/// </summary>
		public const string ExtCabinet = "cab";
		/// <summary>
		/// Defines the file extension for G-ZIP compression archive file.
		/// </summary>
		public const string ExtGZip = "gzip";
		/// <summary>
		/// Defines the file extension for PK-ZIP compression archive file.
		/// </summary>
		public const string ExtPkZip = "zip";
		/// <summary>
		/// Defines the file extension for TAR compression archive file.
		/// </summary>
		public const string ExtTar = "tar";
		/// <summary>
		/// Defines the file extension for TAR+GZIP compression archive file.
		/// </summary>
		public const string ExtTarGZip = "tar.gz";
		#endregion

		#region Common Coding File Types		
		/// <summary>
		/// Defines the file extension for an ASP .NET page definition.
		/// </summary>
		public const string ExtAspNetFile = "aspx";
		/// <summary>
		/// Defines the file extension for C++ project file.
		/// </summary>
		public const string ExtCPlusPlusProject = "cppproj";
		/// <summary>
		/// Defines the file extension for C++ source code file.
		/// </summary>
		public const string ExtCPlusPlusFile = "cpp";
		/// <summary>
		/// Defines the file extension for C source code file.
		/// </summary>
		public const string ExtCFile = "c";
		/// <summary>
		/// Defines the file extension for C# project file.
		/// </summary>
		public const string ExtCSharpProject = "cs";
		/// <summary>
		/// Defines the file extension for memory dump file.
		/// </summary>
		public const string ExtDumpFile = "dmp";
		/// <summary>
		/// Defines the file extension for GIT file.
		/// </summary>
		public const string ExtGitFile = "git";
		/// <summary>
		/// Defines the file extension for C or C++ header file.
		/// </summary>
		public const string ExtHeaderFile = "h";
		/// <summary>
		/// Defines the file extension for an HTML file.
		/// </summary>
		public const string ExtHtmlFile = "html";
		/// <summary>
		/// Defines the file extension for JavaScript source file.
		/// </summary>
		public const string ExtJavascriptFile = "js";
		/// <summary>
		/// Defines the file extension for program database file.
		/// </summary>
		public const string ExtProgramDatabase = "pdb";
		/// <summary>
		/// Defines the file extension for precompiled header file.
		/// </summary>
		public const string ExtPrecompiledHeader = "pch";
		/// <summary>
		/// Defines the file extension for Visual Studio solution file. 
		/// </summary>
		public const string ExtSolution = "sln";
		/// <summary>
		/// Defines the file extension for source control data file.
		/// </summary>
		public const string ExtSourceControlFile = "scc";
		/// <summary>
		/// Defines the file extension for SQL source code file.
		/// </summary>
		public const string ExtSqlFile = "sql";
		/// <summary>
		/// Defines the file extension for SQL server database file.
		/// </summary>
		public const string ExtSqlServerDatabase = "mdf";
		/// <summary>
		/// Defines the file extension for SQL server database log file.
		/// </summary>
		public const string ExtSqlServerDatabaseLog = "ldf";
		/// <summary>
		/// Defines the file extension for TypeScript source file.
		/// </summary>
		public const string ExtTypeScriptFile = "ts";
		/// <summary>
		/// Defines the file extension for Visual Basic source code file.
		/// </summary>
		public const string ExtVisualBasicFile = "vb";
		/// <summary>
		/// Defines the file extension for Visual Basic Project file.
		/// </summary>
		public const string ExtVisualBasicProject = "vbproj";
		/// <summary>
		/// Defines the file extension for an Extensible Application Markup Language source definition file.
		/// </summary>
		public const string ExtXamlFile = "xaml";
		/// <summary>
		/// Defines the file extension for an Extensible Markup Language file.
		/// </summary>
		public const string ExtXmlFile = "xml";
		#endregion

		#region Common Generic File Types		
		/// <summary>
		/// Defines the file extension for binary data file.
		/// </summary>
		public const string ExtBinaryFile = "bin";
		/// <summary>
		/// Defines the file extension for calendar file.
		/// </summary>
		public const string ExtCalendarFile = "cal";
		/// <summary>
		/// Defines the file extension for compiled HTML file.
		/// </summary>
		public const string ExtCompiledHtml = "chm";
		/// <summary>
		/// Defines the file extension for C or C++ resource file.
		/// </summary>
		public const string ExtCResourceFile = "rc";
		/// <summary>
		/// Defines the file extension for generic data file.
		/// </summary>
		public const string ExtDataFile = "dat";
		/// <summary>
		/// Defines the file extension for generic database file.
		/// </summary>
		public const string ExtGenericDatabase = "db";
		/// <summary>
		/// Defines the file extension for general code library file.
		/// </summary>
		public const string ExtLibrary = "lib";
		/// <summary>
		/// Defines the file extension for general log file.
		/// </summary>
		public const string ExtLogFile = "log";
		/// <summary>
		/// Defines the file extension for .NET resource file.
		/// </summary>
		public const string ExtNETResourceFile = "resx";
		/// <summary>
		/// Defines the file extension for PostScript file.
		/// </summary>
		public const string ExtPostScript = "ps";
		/// <summary>
		/// Defines the file extension for general temporary file.
		/// </summary>
		public const string ExtTemporaryFile = "tmp";
		#endregion

		#endregion

		#region Gender Conversion Constants
		/// <summary>
		/// Specifies a string code for male.
		/// </summary>
		public const string GenderCodeMale = "M";
		/// <summary>
		/// Specifies a string code for female.
		/// </summary>
		public const string GenderCodeFemale = "F";
		/// <summary>
		/// Specifies a string code for male.
		/// </summary>
		public const char GenderCodeCharMale = 'M';
		/// <summary>
		/// Specifies the string text for male.
		/// </summary>
		public const string GenderMale = "Male";
		/// <summary>
		/// Specifies the string text for female.
		/// </summary>
		public const string GenderFemale = "Female";
		#endregion

		#region Formatting Constants		
		/// <summary>
		/// The hexadecimal format string.
		/// </summary>
		public const string HexFormat = "x2";
		/// <summary>
		/// The hexadecimal format string for a single character.
		/// </summary>
		public const string HexFormatSingle = "X";
		/// <summary>
		/// The phone number format string.
		/// </summary>
		public const string PhoneNumberFormat = "({0}) {1}-{2}";
		/// <summary>
		/// A general date format string.
		/// </summary>
		public const string DateFormat = "{0:MM/dd/yyyy}";
		/// <summary>
		/// A general date/time format string.
		/// </summary>
		public const string DateTimeFormat = "{0:MM/dd/yyyy hh:mm tt}";
		#endregion

		#region File Dscription Constants
		/// <summary>
		/// The description for a generic file.
		/// </summary>
		public const string FileDescGeneric = "File";
		#endregion

		#region Registry Constants
		/// <summary>
		/// The registry sub-key value for editing a file.
		/// </summary>
		public const string RegSubKeyNameEdit = "\\shell\\edit\\command";
		/// <summary>
		/// The registry sub-key value for opening a file.
		/// </summary>
		public const string RegSubKeyNameOpen = "\\shell\\Open\\command";
		/// <summary>
		/// The registry sub-key value for the location of the default icon.
		/// </summary>
		public const string RegSubKeyNameDefaultIcon = "\\DefaultIcon";
		#endregion
	}
}
