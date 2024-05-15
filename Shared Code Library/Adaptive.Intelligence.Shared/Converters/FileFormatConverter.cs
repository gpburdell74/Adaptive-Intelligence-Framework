namespace Adaptive.Intelligence.Shared
{
	/// <summary>
	/// Provides a class for translating the <see cref="FileFormat"/> enumerated value and file extensions to
	/// and from each representation.
	/// </summary>
	/// <seealso cref="FileFormat" />
	/// <seealso cref="IValueConverter{F, T}" />
	public sealed class FileFormatConverter : IValueConverter<FileFormat, string>
	{
		#region Private Member Declarations
		/// <summary>
		/// The conversion lookup table.
		/// </summary>
		private static readonly Dictionary<FileFormat, string> _convertTable;
		/// <summary>
		/// The re-conversion lookup table.
		/// </summary>
		private static readonly Dictionary<string, FileFormat> _reConvertTable;
		#endregion

		#region Static Constructor 
		/// <summary>
		/// Performs static data initialization for the <see cref="FileFormatConverter"/> class.
		/// </summary>
		/// <remarks>
		/// This is the static constructor.
		/// </remarks>
		static FileFormatConverter()
		{
			_convertTable = new Dictionary<FileFormat, string>();
			_reConvertTable = new Dictionary<string, FileFormat>();

			CreateConversionTable();
			CreateReconversionTable();
		}
		#endregion

		#region Public Methods / Functions
		/// <summary>
		/// Converts the original enumerated file format value to its equivalent file extension string.
		/// </summary>
		/// <param name="originalValue">
		/// The <see cref="FileFormat"/> enumerated value to be converted.
		/// </param>
		/// <returns>
		/// A string containing the file extension value for the specified type.
		/// </returns>
		public string Convert(FileFormat originalValue)
		{
			_convertTable.TryGetValue(originalValue, out string? extension);

			if (extension == null)
				extension = string.Empty;
			
			return extension;
		}
		/// <summary>
		/// Converts the file extension string to the matching <see cref="FileFormat"/> enumerated value.
		/// </summary>
		/// <param name="convertedValue">
		/// A string containing the original file extension value to be converted.
		/// </param>
		/// <returns>
		/// The matching <see cref="FileFormat"/> enumerated value.
		/// </returns>
		/// <remarks>
		/// The implementation of this method is be the inverse of the <see cref="Convert" /> method.
		/// </remarks>
		public FileFormat ConvertBack(string convertedValue)
		{
			FileFormat value = FileFormat.NotSpecified;
			if (!string.IsNullOrEmpty(convertedValue))
			{
				// Remove the leading ".", if present... ensure the whole thing is lowercase for comparison.
				if (convertedValue[0] == '.')
					convertedValue = convertedValue.Substring(1, convertedValue.Length - 1);
				_reConvertTable.TryGetValue(convertedValue.ToLower(), out value);
			}

			return value;
		}
		#endregion

		#region Private Static Methods / Functions
		/// <summary>
		/// Creates the static look up table for the data conversion.
		/// </summary>
		private static void CreateConversionTable()
		{
			_convertTable.Add(FileFormat.NotSpecified, string.Empty);
			_convertTable.Add(FileFormat.ExcelLegacy, Constants.ExtExcelLegacy);
			_convertTable.Add(FileFormat.Excel, Constants.ExtExcel);
			_convertTable.Add(FileFormat.ExcelMacro, Constants.ExtExcelMacro);
			_convertTable.Add(FileFormat.ExcelMacroLegacy, Constants.ExtExcelMacroLegacy);
			_convertTable.Add(FileFormat.ExcelTemplate, Constants.ExtExcelTemplate);
			_convertTable.Add(FileFormat.ExcelTemplateLegacy, Constants.ExtExcelTemplateLegacy);
			_convertTable.Add(FileFormat.MSAccessLegacyDatabase, Constants.ExtMSAccessLegacyDatabase);
			_convertTable.Add(FileFormat.MSAccessDatabase, Constants.ExtMSAccessDatabase);
			_convertTable.Add(FileFormat.OneNote, Constants.ExtOneNote);
			_convertTable.Add(FileFormat.OneNoteExport, Constants.ExtOneNoteExport);
			_convertTable.Add(FileFormat.PowerPointLegacy, Constants.ExtPowerPointLegacy);
			_convertTable.Add(FileFormat.PowerPoint, Constants.ExtPowerPoint);
			_convertTable.Add(FileFormat.Publisher, Constants.ExtPublisher);
			_convertTable.Add(FileFormat.WordDocumentLegacy, Constants.ExtWordDocumentLegacy);
			_convertTable.Add(FileFormat.WordDocument, Constants.ExtWordDocument);
			_convertTable.Add(FileFormat.WordMacro, Constants.ExtWordMacro);
			_convertTable.Add(FileFormat.WordLegacyMacro, Constants.ExtWordLegacyMacro);
			_convertTable.Add(FileFormat.WordTemplate, Constants.ExtWordTemplate);
			_convertTable.Add(FileFormat.WordTemplateLegacy, Constants.ExtWordTemplateLegacy);
			_convertTable.Add(FileFormat.Xps, Constants.ExtXps);
			_convertTable.Add(FileFormat.CommaSeparatedValues, Constants.ExtCommaSeparatedValues);
			_convertTable.Add(FileFormat.JavascriptObjectNotation, Constants.ExtJavascriptObjectNotation);
			_convertTable.Add(FileFormat.RichTextFile, Constants.ExtRichTextFile);
			_convertTable.Add(FileFormat.TextFile, Constants.ExtTextFile);
			_convertTable.Add(FileFormat.BatchFile, Constants.ExtBatchFile);
			_convertTable.Add(FileFormat.CommandConsoleFile, Constants.ExtCommandConsoleFile);
			_convertTable.Add(FileFormat.CommandFile, Constants.ExtCommandFile);
			_convertTable.Add(FileFormat.ControlPanel, Constants.ExtControlPanel);
			_convertTable.Add(FileFormat.DOSCommandFile, Constants.ExtDOSCommandFile);
			_convertTable.Add(FileFormat.DynamicLinkedLibrary, Constants.ExtDynamicLinkedLibrary);
			_convertTable.Add(FileFormat.ExecutableFile, Constants.ExtExecutableFile);
			_convertTable.Add(FileFormat.ImageFile, Constants.ExtImageFile);
			_convertTable.Add(FileFormat.NationalLanguageService, Constants.ExtNationalLanguageService);
			_convertTable.Add(FileFormat.PowerShellFile, Constants.ExtPowerShellFile);
			_convertTable.Add(FileFormat.RegistryFile, Constants.ExtRegistryFile);
			_convertTable.Add(FileFormat.SystemFile, Constants.ExtSystemFile);
			_convertTable.Add(FileFormat.WindowsInstallerFile, Constants.ExtWindowsInstallerFile);
			_convertTable.Add(FileFormat.Bitmap, Constants.ExtBitmap);
			_convertTable.Add(FileFormat.GenericImage, Constants.ExtGenericImage);
			_convertTable.Add(FileFormat.GraphicsInterchangeFormat, Constants.ExtGraphicsInterchangeFormat);
			_convertTable.Add(FileFormat.JointPhotographicExpertsGroup, Constants.ExtJointPhotographicExpertsGroup);
			_convertTable.Add(FileFormat.JointPhotographicExpertsGroupLong, Constants.ExtJointPhotographicExpertsGroupLong);
			_convertTable.Add(FileFormat.PhotoShop, Constants.ExtPhotoShop);
			_convertTable.Add(FileFormat.PortableNetworkGraphics, Constants.ExtPortableNetworkGraphics);
			_convertTable.Add(FileFormat.RawImage, Constants.ExtRawImage);
			_convertTable.Add(FileFormat.TaggedImageFileFormat, Constants.ExtTaggedImageFileFormat);
			_convertTable.Add(FileFormat.WindowsIcon, Constants.ExtWindowsIcon);
			_convertTable.Add(FileFormat.MovieFile, Constants.ExtMovieFile);
			_convertTable.Add(FileFormat.MPeg, Constants.ExtMPeg);
			_convertTable.Add(FileFormat.MPegLong, Constants.ExtMPegLong);
			_convertTable.Add(FileFormat.MPegV2, Constants.ExtMPegV2);
			_convertTable.Add(FileFormat.MPegV3, Constants.ExtMPegV3);
			_convertTable.Add(FileFormat.MPegV4, Constants.ExtMPegV4);
			_convertTable.Add(FileFormat.WindowsMovieFile, Constants.ExtWindowsMovieFile);
			_convertTable.Add(FileFormat.Cabinet, Constants.ExtCabinet);
			_convertTable.Add(FileFormat.GZip, Constants.ExtGZip);
			_convertTable.Add(FileFormat.PkZip, Constants.ExtPkZip);
			_convertTable.Add(FileFormat.Tar, Constants.ExtTar);
			_convertTable.Add(FileFormat.TarGZip, Constants.ExtTarGZip);
			_convertTable.Add(FileFormat.AspNetFile, Constants.ExtAspNetFile);
			_convertTable.Add(FileFormat.CPlusPlusProject, Constants.ExtCPlusPlusProject);
			_convertTable.Add(FileFormat.CPlusPlusFile, Constants.ExtCPlusPlusFile);
			_convertTable.Add(FileFormat.CFile, Constants.ExtCFile);
			_convertTable.Add(FileFormat.CSharpProject, Constants.ExtCSharpProject);
			_convertTable.Add(FileFormat.DumpFile, Constants.ExtDumpFile);
			_convertTable.Add(FileFormat.GitFile, Constants.ExtGitFile);
			_convertTable.Add(FileFormat.HeaderFile, Constants.ExtHeaderFile);
			_convertTable.Add(FileFormat.HtmlFile, Constants.ExtHtmlFile);
			_convertTable.Add(FileFormat.JavascriptFile, Constants.ExtJavascriptFile);
			_convertTable.Add(FileFormat.ProgramDatabase, Constants.ExtProgramDatabase);
			_convertTable.Add(FileFormat.PrecompiledHeader, Constants.ExtPrecompiledHeader);
			_convertTable.Add(FileFormat.Solution, Constants.ExtSolution);
			_convertTable.Add(FileFormat.SourceControlFile, Constants.ExtSourceControlFile);
			_convertTable.Add(FileFormat.SqlFile, Constants.ExtSqlFile);
			_convertTable.Add(FileFormat.SqlServerDatabase, Constants.ExtSqlServerDatabase);
			_convertTable.Add(FileFormat.SqlServerDatabaseLog, Constants.ExtSqlServerDatabaseLog);
			_convertTable.Add(FileFormat.TypeScriptFile, Constants.ExtTypeScriptFile);
			_convertTable.Add(FileFormat.VisualBasicFile, Constants.ExtVisualBasicFile);
			_convertTable.Add(FileFormat.VisualBasicProject, Constants.ExtVisualBasicProject);
			_convertTable.Add(FileFormat.XamlFile, Constants.ExtXamlFile);
			_convertTable.Add(FileFormat.XmlFile, Constants.ExtXmlFile);
			_convertTable.Add(FileFormat.BinaryFile, Constants.ExtBinaryFile);
			_convertTable.Add(FileFormat.CalendarFile, Constants.ExtCalendarFile);
			_convertTable.Add(FileFormat.CompiledHtml, Constants.ExtCompiledHtml);
			_convertTable.Add(FileFormat.CResourceFile, Constants.ExtCResourceFile);
			_convertTable.Add(FileFormat.DataFile, Constants.ExtDataFile);
			_convertTable.Add(FileFormat.GenericDatabase, Constants.ExtGenericDatabase);
			_convertTable.Add(FileFormat.Library, Constants.ExtLibrary);
			_convertTable.Add(FileFormat.LogFile, Constants.ExtLogFile);
			_convertTable.Add(FileFormat.NETResourceFile, Constants.ExtNETResourceFile);
			_convertTable.Add(FileFormat.PostScript, Constants.ExtPostScript);
			_convertTable.Add(FileFormat.TemporaryFile, Constants.ExtTemporaryFile);
		}
		/// <summary>
		/// Creates the static look up table for the data de-conversion.
		/// </summary>
		private static void CreateReconversionTable()
		{
			_reConvertTable.Add(string.Empty, FileFormat.NotSpecified);
			_reConvertTable.Add(Constants.ExtExcelLegacy, FileFormat.ExcelLegacy);
			_reConvertTable.Add(Constants.ExtExcel, FileFormat.Excel);
			_reConvertTable.Add(Constants.ExtExcelMacro, FileFormat.ExcelMacro);
			_reConvertTable.Add(Constants.ExtExcelMacroLegacy, FileFormat.ExcelMacroLegacy);
			_reConvertTable.Add(Constants.ExtExcelTemplate, FileFormat.ExcelTemplate);
			_reConvertTable.Add(Constants.ExtExcelTemplateLegacy, FileFormat.ExcelTemplateLegacy);
			_reConvertTable.Add(Constants.ExtMSAccessLegacyDatabase, FileFormat.MSAccessLegacyDatabase);
			_reConvertTable.Add(Constants.ExtMSAccessDatabase, FileFormat.MSAccessDatabase);
			_reConvertTable.Add(Constants.ExtOneNote, FileFormat.OneNote);
			_reConvertTable.Add(Constants.ExtOneNoteExport, FileFormat.OneNoteExport);
			_reConvertTable.Add(Constants.ExtPowerPointLegacy, FileFormat.PowerPointLegacy);
			_reConvertTable.Add(Constants.ExtPowerPoint, FileFormat.PowerPoint);
			_reConvertTable.Add(Constants.ExtPublisher, FileFormat.Publisher);
			_reConvertTable.Add(Constants.ExtWordDocumentLegacy, FileFormat.WordDocumentLegacy);
			_reConvertTable.Add(Constants.ExtWordDocument, FileFormat.WordDocument);
			_reConvertTable.Add(Constants.ExtWordMacro, FileFormat.WordMacro);
			_reConvertTable.Add(Constants.ExtWordLegacyMacro, FileFormat.WordLegacyMacro);
			_reConvertTable.Add(Constants.ExtWordTemplate, FileFormat.WordTemplate);
			_reConvertTable.Add(Constants.ExtWordTemplateLegacy, FileFormat.WordTemplateLegacy);
			_reConvertTable.Add(Constants.ExtXps, FileFormat.Xps);
			_reConvertTable.Add(Constants.ExtCommaSeparatedValues, FileFormat.CommaSeparatedValues);
			_reConvertTable.Add(Constants.ExtJavascriptObjectNotation, FileFormat.JavascriptObjectNotation);
			_reConvertTable.Add(Constants.ExtRichTextFile, FileFormat.RichTextFile);
			_reConvertTable.Add(Constants.ExtTextFile, FileFormat.TextFile);
			_reConvertTable.Add(Constants.ExtBatchFile, FileFormat.BatchFile);
			_reConvertTable.Add(Constants.ExtCommandConsoleFile, FileFormat.CommandConsoleFile);
			_reConvertTable.Add(Constants.ExtCommandFile, FileFormat.CommandFile);
			_reConvertTable.Add(Constants.ExtControlPanel, FileFormat.ControlPanel);
			_reConvertTable.Add(Constants.ExtDOSCommandFile, FileFormat.DOSCommandFile);
			_reConvertTable.Add(Constants.ExtDynamicLinkedLibrary, FileFormat.DynamicLinkedLibrary);
			_reConvertTable.Add(Constants.ExtExecutableFile, FileFormat.ExecutableFile);
			_reConvertTable.Add(Constants.ExtImageFile, FileFormat.ImageFile);
			_reConvertTable.Add(Constants.ExtNationalLanguageService, FileFormat.NationalLanguageService);
			_reConvertTable.Add(Constants.ExtPowerShellFile, FileFormat.PowerShellFile);
			_reConvertTable.Add(Constants.ExtRegistryFile, FileFormat.RegistryFile);
			_reConvertTable.Add(Constants.ExtSystemFile, FileFormat.SystemFile);
			_reConvertTable.Add(Constants.ExtWindowsInstallerFile, FileFormat.WindowsInstallerFile);
			_reConvertTable.Add(Constants.ExtBitmap, FileFormat.Bitmap);
			_reConvertTable.Add(Constants.ExtGenericImage, FileFormat.GenericImage);
			_reConvertTable.Add(Constants.ExtGraphicsInterchangeFormat, FileFormat.GraphicsInterchangeFormat);
			_reConvertTable.Add(Constants.ExtJointPhotographicExpertsGroup, FileFormat.JointPhotographicExpertsGroup);
			_reConvertTable.Add(Constants.ExtJointPhotographicExpertsGroupLong, FileFormat.JointPhotographicExpertsGroupLong);
			_reConvertTable.Add(Constants.ExtPhotoShop, FileFormat.PhotoShop);
			_reConvertTable.Add(Constants.ExtPortableNetworkGraphics, FileFormat.PortableNetworkGraphics);
			_reConvertTable.Add(Constants.ExtRawImage, FileFormat.RawImage);
			_reConvertTable.Add(Constants.ExtTaggedImageFileFormat, FileFormat.TaggedImageFileFormat);
			_reConvertTable.Add(Constants.ExtWindowsIcon, FileFormat.WindowsIcon);
			_reConvertTable.Add(Constants.ExtMovieFile, FileFormat.MovieFile);
			_reConvertTable.Add(Constants.ExtMPeg, FileFormat.MPeg);
			_reConvertTable.Add(Constants.ExtMPegLong, FileFormat.MPegLong);
			_reConvertTable.Add(Constants.ExtMPegV2, FileFormat.MPegV2);
			_reConvertTable.Add(Constants.ExtMPegV3, FileFormat.MPegV3);
			_reConvertTable.Add(Constants.ExtMPegV4, FileFormat.MPegV4);
			_reConvertTable.Add(Constants.ExtWindowsMovieFile, FileFormat.WindowsMovieFile);
			_reConvertTable.Add(Constants.ExtCabinet, FileFormat.Cabinet);
			_reConvertTable.Add(Constants.ExtGZip, FileFormat.GZip);
			_reConvertTable.Add(Constants.ExtPkZip, FileFormat.PkZip);
			_reConvertTable.Add(Constants.ExtTar, FileFormat.Tar);
			_reConvertTable.Add(Constants.ExtTarGZip, FileFormat.TarGZip);
			_reConvertTable.Add(Constants.ExtAspNetFile, FileFormat.AspNetFile);
			_reConvertTable.Add(Constants.ExtCPlusPlusProject, FileFormat.CPlusPlusProject);
			_reConvertTable.Add(Constants.ExtCPlusPlusFile, FileFormat.CPlusPlusFile);
			_reConvertTable.Add(Constants.ExtCFile, FileFormat.CFile);
			_reConvertTable.Add(Constants.ExtCSharpProject, FileFormat.CSharpProject);
			_reConvertTable.Add(Constants.ExtDumpFile, FileFormat.DumpFile);
			_reConvertTable.Add(Constants.ExtGitFile, FileFormat.GitFile);
			_reConvertTable.Add(Constants.ExtHeaderFile, FileFormat.HeaderFile);
			_reConvertTable.Add(Constants.ExtHtmlFile, FileFormat.HtmlFile);
			_reConvertTable.Add(Constants.ExtJavascriptFile, FileFormat.JavascriptFile);
			_reConvertTable.Add(Constants.ExtProgramDatabase, FileFormat.ProgramDatabase);
			_reConvertTable.Add(Constants.ExtPrecompiledHeader, FileFormat.PrecompiledHeader);
			_reConvertTable.Add(Constants.ExtSolution, FileFormat.Solution);
			_reConvertTable.Add(Constants.ExtSourceControlFile, FileFormat.SourceControlFile);
			_reConvertTable.Add(Constants.ExtSqlFile, FileFormat.SqlFile);
			_reConvertTable.Add(Constants.ExtSqlServerDatabase, FileFormat.SqlServerDatabase);
			_reConvertTable.Add(Constants.ExtSqlServerDatabaseLog, FileFormat.SqlServerDatabaseLog);
			_reConvertTable.Add(Constants.ExtTypeScriptFile, FileFormat.TypeScriptFile);
			_reConvertTable.Add(Constants.ExtVisualBasicFile, FileFormat.VisualBasicFile);
			_reConvertTable.Add(Constants.ExtVisualBasicProject, FileFormat.VisualBasicProject);
			_reConvertTable.Add(Constants.ExtXamlFile, FileFormat.XamlFile);
			_reConvertTable.Add(Constants.ExtXmlFile, FileFormat.XmlFile);
			_reConvertTable.Add(Constants.ExtBinaryFile, FileFormat.BinaryFile);
			_reConvertTable.Add(Constants.ExtCalendarFile, FileFormat.CalendarFile);
			_reConvertTable.Add(Constants.ExtCompiledHtml, FileFormat.CompiledHtml);
			_reConvertTable.Add(Constants.ExtCResourceFile, FileFormat.CResourceFile);
			_reConvertTable.Add(Constants.ExtDataFile, FileFormat.DataFile);
			_reConvertTable.Add(Constants.ExtGenericDatabase, FileFormat.GenericDatabase);
			_reConvertTable.Add(Constants.ExtLibrary, FileFormat.Library);
			_reConvertTable.Add(Constants.ExtLogFile, FileFormat.LogFile);
			_reConvertTable.Add(Constants.ExtNETResourceFile, FileFormat.NETResourceFile);
			_reConvertTable.Add(Constants.ExtPostScript, FileFormat.PostScript);
			_reConvertTable.Add(Constants.ExtTemporaryFile, FileFormat.TemporaryFile);
		}
		#endregion
	}
}
