namespace Adaptive.Intelligence.Shared.UI.CommonDialogs;

/// <summary>
/// Provides a basic definition and container for the various parameters used when defining
/// and opening a file dialog for saving a file under a new name.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
public class SaveAsFileProfile : DisposableObjectBase, ISaveAsFileProfile
{
    /// <summary>
    /// Gets or sets a value indicating whether [add extension].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [add extension]; otherwise, <c>false</c>.
    /// </value>
    public bool AddExtension { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the file is
    /// added to the most recently-used file list when opened.
    /// </summary>
    /// <value>
    ///   <c>true</c> to add to the global MRU list; otherwise, <c>false</c>.
    /// </value>
    public bool AddToRecent { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the automatic upgrade
    /// is enabled for the common dialog.
    /// </summary>
    /// <value>
    ///   <c>true</c> if the automatic upgrade is enabled; otherwise, <c>false</c>.
    /// </value>
    public bool AutoUpgradeEnabled { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the dialog box displays a
    /// warning if the user specifies a file name that does not exist.
    /// </summary>
    /// <value>
    /// <b>true</b> to display a warning if the user specifies a file name that does not exist; otherwise,
    /// <b>false</b>. The default value is <b>true</b>.
    /// </value>
    public bool CheckFileExists { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the dialog box displays a
    /// warning if the user specifies a path that does not exist.
    /// </summary>
    /// <value>
    ///   <c>true</c> if [check path exists]; otherwise, <c>false</c>.
    /// </value>
    public bool CheckPathExists { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to check the file write permission before saving a file.
    /// </summary>
    /// <value>
    ///   <c>true</c> if to check for write access; otherwise, <c>false</c>.
    /// </value>
    public bool CheckWriteAccess { get; set; }

    /// <summary>
    /// Gets or sets the default file extension.
    /// </summary>
    /// <value>
    /// A string specifying the default file extension value, or <b>null</b>
    /// if not used.
    /// </value>
    public string? DefaultExt { get; set; }

    /// <summary>
    /// Gets or sets the current file name filter string, which determines the choices
    /// that appear in the "Open file type" or "Files of type" box in the dialog box.
    /// </summary>
    /// <value>
    /// A string containing filter patterns and descriptions, such as "Text files (*.txt)|*.txt|All files (*.*)|*.*". Each pattern is separated by a vertical bar (|). The description of each filter pattern is optional. If the description is not included, the pattern itself is used as the description. If the filter string is not valid, the dialog box displays all files.
    /// </value>
    public string? Filter { get; set; }

    /// <summary>
    /// Gets or sets the index of the filter.
    /// </summary>
    /// <value>
    /// An integer specifying the index of the file filter to default to.  If the value is less than zero (0),
    /// or the value is greater than the number of filters specified in the <see cref="Filter" /> property,
    /// the first filter is used by default.
    /// </value>
    public int FilterIndex { get; set; } = -1;

    /// <summary>
    /// Gets or sets the initial directory.
    /// </summary>
    /// <value>
    /// A string specifying the initial directory displayed by the dialog. This can be a fully qualified path,
    /// such as "C:\My Documents", or a special folder name, such as "Desktop". If the value is <b>null</b> or
    /// an empty string, the current directory is used as the initial directory. If the specified directory
    /// does not exist, the dialog box will display the current directory instead.
    /// </value>
    public string? InitialDirectory { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the Save As dialog box displays a warning if the
    /// user specifies a file name that already exists.
    /// </summary>
    /// <value>
    /// <c>true</c> to prompt the user for permission to overwrite if the user specifies a file name that
    /// already exists; otherwise, <c>false</c>.
    /// </value>
    public bool OverwritePrompt { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the read-only check box is selected.
    /// </summary>
    /// <value>
    /// <c>true</c> if the read-only check box is selected; otherwise, false. The default value,
    /// is <c>false</c>.
    /// </value>
    public bool ReadOnlyChecked { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to restore the current directory to its original value.
    /// </summary>
    /// <value>
    /// <c>true</c> to restore the current directory to its original value if the user changed the directory
    /// while searching for files; otherwise, <c>false</c>. The default value is <c>false</c>.
    /// </value>
    public bool RestoreDirectory { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the Help button is displayed
    /// in the file dialog.
    /// </summary>
    /// <value>
    ///   <c>true</c> to show the Help button; otherwise, <b>false</b>. The default value is <b>false</b>.
    /// </value>
    public bool ShowHelp { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to show hidden files in the dialog box.
    /// </summary>
    /// <value>
    /// <c>true</c> to show hidden files in the dialog box; otherwise, <c>false</c>. The default value
    /// is <c>false</c>.
    /// </value>
    public bool ShowHiddenFiles { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to support multi dotted extensions.
    /// </summary>
    /// <value>
    ///   <c>true</c> to support multi dotted extensions in file names; otherwise, <c>false</c>.
    /// </value>
    public bool SupportMultiDottedExtensions { get; set; }

    /// <summary>
    /// Gets or sets the title of the dialog.
    /// </summary>
    /// <value>
    /// A string containing hte title text of the dialog to be displayed in the title bar of the dialog.
    /// If the value is <b>null</b> or an empty string, the default title is used.
    /// </value>
    public string? Title { get; set; }

    /// <summary>
    /// Creates a new object that is a copy of the current instance.
    /// </summary>
    /// <returns>
    /// A new <see cref="SaveAsFileProfile"/> that is a copy of this instance.
    /// </returns>
    public SaveAsFileProfile Clone()
    {
        return new SaveAsFileProfile
        {
            AddExtension = AddExtension,
            AddToRecent = AddToRecent,
            AutoUpgradeEnabled = AutoUpgradeEnabled,
            CheckFileExists = CheckFileExists,
            CheckPathExists = CheckPathExists,
            CheckWriteAccess = CheckWriteAccess,
            DefaultExt = DefaultExt,
            Filter = Filter,
            FilterIndex = FilterIndex,
            InitialDirectory = InitialDirectory,
            ReadOnlyChecked = ReadOnlyChecked,
            RestoreDirectory = RestoreDirectory,
            ShowHelp = ShowHelp,
            ShowHiddenFiles = ShowHiddenFiles,
            SupportMultiDottedExtensions = SupportMultiDottedExtensions,
            Title = Title
        };
    }
}