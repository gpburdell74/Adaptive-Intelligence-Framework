using System.ComponentModel;

namespace Adaptive.Intelligence.Shared.UI.CommonDialogs;

/// <summary>
/// Provides a basic signature definition for the various parameters used when defining
/// and opening a file dialog for reading or writing a file.  This interface is used as the 
/// base contract for both <see cref="IOpenFileProfile"/> and <see cref="ISaveAsFileProfile"/> 
/// to share common properties between the two types of file dialogs.
/// </summary>
public interface IFileProfile : IDisposable
{
    /// <summary>
    /// Gets or sets a value indicating whether [add extension].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [add extension]; otherwise, <c>false</c>.
    /// </value>
    [DefaultValue(false)]
    bool AddExtension { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the file is 
    /// added to the most recently-used file list when opened.
    /// </summary>
    /// <value>
    ///   <c>true</c> to add to the global MRU list; otherwise, <c>false</c>.
    /// </value>
    [DefaultValue(false)]
    bool AddToRecent { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the automatic upgrade 
    /// is enabled for the common dialog.
    /// </summary>
    /// <value>
    ///   <c>true</c> if the automatic upgrade is enabled; otherwise, <c>false</c>.
    /// </value>
    [DefaultValue(true)]
    bool AutoUpgradeEnabled { get; set; }

    /// <summary>
    ///  Gets or sets a value indicating whether the dialog box displays a
    ///  warning if the user specifies a file name that does not exist.
    /// </summary>
    /// <value>
    /// <b>true</b> to display a warning if the user specifies a file name that does not exist; otherwise, 
    /// <b>false</b>. The default value is <b>true</b>.
    /// </value>
    [DefaultValue(true)]
    bool CheckFileExists { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the dialog box displays a
    ///  warning if the user specifies a path that does not exist.
    /// </summary>
    /// <value>
    ///   <c>true</c> if [check path exists]; otherwise, <c>false</c>.
    /// </value>
    [DefaultValue(true)]
    bool CheckPathExists { get; set; }

    /// <summary>
    /// Gets or sets the default file extension.
    /// </summary>
    /// <value>
    /// A string specifying the default file extension value, or <b>null</b>
    /// if not used.
    /// </value>
    string? DefaultExt { get; set; }

    /// <summary>
    ///  Gets or sets the current file name filter string, which determines the choices
    ///  that appear in the "Open file type" or "Files of type" box in the dialog box.
    /// </summary>
    /// <value>
    /// A string containing filter patterns and descriptions, such as "Text files (*.txt)|*.txt|All files (*.*)|*.*". Each pattern is separated by a vertical bar (|). The description of each filter pattern is optional. If the description is not included, the pattern itself is used as the description. If the filter string is not valid, the dialog box displays all files.
    /// </value>
    string? Filter { get; set; }

    /// <summary>
    /// Gets or sets the index of the filter.
    /// </summary>
    /// <value>
    /// An integer specifying the index of the file filter to default to.  If the value is less than zero (0),
    /// or the value is greater than the number of filters specified in the <see cref="Filter"/> property, 
    /// the first filter is used by default.
    /// </value>
    int FilterIndex { get; set; }

    /// <summary>
    /// Gets or sets the initial directory.
    /// </summary>
    /// <value>
    /// A string specifying the initial directory displayed by the dialog. This can be a fully qualified path,
    /// such as "C:\My Documents", or a special folder name, such as "Desktop". If the value is <b>null</b> or 
    /// an empty string, the current directory is used as the initial directory. If the specified directory 
    /// does not exist, the dialog box will display the current directory instead.
    /// </value>
    string? InitialDirectory { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to restore the current directory to its original value.
    /// </summary>
    /// <value>
    ///   <c>true</c> to restore the current directory to its original value if the user changed the directory
    ///   while searching for files; otherwise, <c>false</c>. The default value is <c>false</c>.
    /// </value>
    [DefaultValue(false)]
    bool RestoreDirectory { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the Help button is displayed
    ///  in the file dialog.
    /// </summary>
    /// <value>
    ///   <c>true</c> to show the Help button; otherwise, <b>false</b>. The default value is <b>false</b>.
    /// </value>
    [DefaultValue(false)]
    bool ShowHelp { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to show hidden files in the dialog box.
    /// </summary>
    /// <value>
    ///   <c>true</c> to show hidden files in the dialog box; otherwise, <c>false</c>. The default value 
    ///   is <c>false</c>.
    /// </value>
    [DefaultValue(false)]
    bool ShowHiddenFiles { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to support multi dotted extensions.
    /// </summary>
    /// <value>
    ///   <c>true</c> to support multi dotted extensions in file names; otherwise, <c>false</c>.
    /// </value>
    bool SupportMultiDottedExtensions { get; set; }

    /// <summary>
    /// Gets or sets the title of the dialog.
    /// </summary>
    /// <value>
    /// A string containing hte title text of the dialog to be displayed in the title bar of the dialog. 
    /// If the value is <b>null</b> or an empty string, the default title is used.
    /// </value>
    string? Title { get; set; }
}