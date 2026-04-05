using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Adaptive.Intelligence.Shared.UI.CommonDialogs;

/// <summary>
/// Provides an ellipsis button control that can be used to open a file dialog for selecting a new name
/// for a file to be saved under.  This control is typically used in conjunction with a text box to 
/// allow users to browse for a file and display the selected file path in the text box.
/// </summary>
/// <seealso cref="AIButton" />
/// <seealso cref="OpenFileDialog" />
public class FileSaveAsEllipseButton : AIButton
{
    #region Private Member Declarations
    /// <summary>
    /// The ellipsis / button text.
    /// </summary>
    private const string Ellipsis = "...";

    /// <summary>
    /// The profile containing the parameters for the Save File As dialog.
    /// </summary>
    private ISaveAsFileProfile _profile = new SaveAsFileProfile();
    #endregion

    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="FileSaveAsEllipseButton"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public FileSaveAsEllipseButton()
    {
        _profile = new SaveAsFileProfile();
        InitializeComponent();
    }

    /// <summary>
    /// Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ButtonBase" /> and optionally releases the managed resources.
    /// </summary>
    /// <param name="disposing"><see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        _profile?.Dispose();
        base.Dispose(disposing);
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets or sets a value indicating whether [add extension].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [add extension]; otherwise, <c>false</c>.
    /// </value>
    [Browsable(true),
     Description(""),
     DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool AddExtension { get => _profile.AddExtension; set => _profile.AddExtension = value; }

    /// <summary>
    /// Gets or sets a value indicating whether the file is
    /// added to the most recently-used file list when opened.
    /// </summary>
    /// <value>
    ///   <c>true</c> to add to the global MRU list; otherwise, <c>false</c>.
    /// </value>
    [Browsable(true),
        Description(""),
     DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool AddToRecent { get => _profile.AddToRecent; set => _profile.AddToRecent = value; }

    /// <summary>
    /// Gets or sets a value indicating whether the automatic upgrade
    /// is enabled for the common dialog.
    /// </summary>
    /// <value>
    ///   <c>true</c> if the automatic upgrade is enabled; otherwise, <c>false</c>.
    /// </value>
    [Browsable(true),
        Description(""),
     DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool AutoUpgradeEnabled { get => _profile.AutoUpgradeEnabled; set => _profile.AutoUpgradeEnabled = value; }

    /// <summary>
    /// Gets or sets a value indicating whether the dialog box displays a
    /// warning if the user specifies a file name that does not exist.
    /// </summary>
    /// <value>
    /// <b>true</b> to display a warning if the user specifies a file name that does not exist; otherwise,
    /// <b>false</b>. The default value is <b>true</b>.
    /// </value>
    [Browsable(true),
        Description(""),
     DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool CheckFileExists { get => _profile.CheckFileExists; set => _profile.CheckFileExists = value; }

    /// <summary>
    /// Gets or sets a value indicating whether the dialog box displays a
    /// warning if the user specifies a path that does not exist.
    /// </summary>
    /// <value>
    ///   <c>true</c> if [check path exists]; otherwise, <c>false</c>.
    /// </value>
    [Browsable(true),
        Description(""),
     DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool CheckPathExists { get => _profile.CheckPathExists; set => _profile.CheckPathExists = value; }

    /// <summary>
    /// Gets or sets the default file extension.
    /// </summary>
    /// <value>
    /// A string specifying the default file extension value, or <b>null</b>
    /// if not used.
    /// </value>
    [Browsable(true),
        Description(""),
     DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public string? DefaultExt { get => _profile.DefaultExt; set => _profile.DefaultExt = value; }

    /// <summary>
    /// Gets or sets the current file name filter string, which determines the choices
    /// that appear in the "Open file type" or "Files of type" box in the dialog box.
    /// </summary>
    /// <value>
    /// A string containing filter patterns and descriptions, such as "Text files (*.txt)|*.txt|All files (*.*)|*.*". Each pattern is separated by a vertical bar (|). The description of each filter pattern is optional. If the description is not included, the pattern itself is used as the description. If the filter string is not valid, the dialog box displays all files.
    /// </value>
    [Browsable(true),
        Description(""),
     DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public string? Filter { get => _profile.Filter; set => _profile.Filter = value; }

    /// <summary>
    /// Gets or sets the index of the filter.
    /// </summary>
    /// <value>
    /// An integer specifying the index of the file filter to default to.  If the value is less than zero (0),
    /// or the value is greater than the number of filters specified in the <see cref="Filter" /> property,
    /// the first filter is used by default.
    /// </value>
    [Browsable(true),
        Description(""),
     DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int FilterIndex { get => _profile.FilterIndex; set => _profile.FilterIndex = value; }

    /// <summary>
    /// Gets or sets the initial directory.
    /// </summary>
    /// <value>
    /// A string specifying the initial directory displayed by the dialog. This can be a fully qualified path,
    /// such as "C:\My Documents", or a special folder name, such as "Desktop". If the value is <b>null</b> or
    /// an empty string, the current directory is used as the initial directory. If the specified directory
    /// does not exist, the dialog box will display the current directory instead.
    /// </value>
    [Browsable(true),
        Description(""),
     DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public string? InitialDirectory { get => _profile.InitialDirectory; set => _profile.InitialDirectory = value; }

    /// <summary>
    /// Gets or sets a value indicating whether to restore the current directory to its original value.
    /// </summary>
    /// <value>
    /// <c>true</c> to restore the current directory to its original value if the user changed the directory
    /// while searching for files; otherwise, <c>false</c>. The default value is <c>false</c>.
    /// </value>
    [Browsable(true),
        Description(""),
     DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool RestoreDirectory { get => _profile.RestoreDirectory; set => _profile.RestoreDirectory = value; }

    /// <summary>
    /// Gets or sets a value indicating whether the Help button is displayed
    /// in the file dialog.
    /// </summary>
    /// <value>
    ///   <c>true</c> to show the Help button; otherwise, <b>false</b>. The default value is <b>false</b>.
    /// </value>
    [Browsable(true),
        Description("Gets or sets a value indicating whether the Help button is displayed in the file dialog."),
     DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool ShowHelp { get => _profile.ShowHelp; set => _profile.ShowHelp = value; }

    /// <summary>
    /// Gets or sets a value indicating whether to show hidden files in the dialog box.
    /// </summary>
    /// <value>
    /// <c>true</c> to show hidden files in the dialog box; otherwise, <c>false</c>. The default value
    /// is <c>false</c>.
    /// </value>
    [Browsable(true),
        Description("Gets or sets a value indicating whether to show hidden files in the dialog box."),
     DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool ShowHiddenFiles { get => _profile.ShowHiddenFiles; set => _profile.ShowHiddenFiles = value; }

    /// <summary>
    /// Gets or sets a value indicating whether to support multi dotted extensions.
    /// </summary>
    /// <value>
    ///   <c>true</c> to support multi dotted extensions in file names; otherwise, <c>false</c>.
    /// </value>
    [Browsable(true),
        Description("Gets or sets a value indicating whether to support multi dotted extensions."),
     DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool SupportMultiDottedExtensions { get => _profile.SupportMultiDottedExtensions; set => _profile.SupportMultiDottedExtensions = value; }

    /// <summary>
    /// Gets or sets the title of the dialog.
    /// </summary>
    /// <value>
    /// A string containing hte title text of the dialog to be displayed in the title bar of the dialog.
    /// If the value is <b>null</b> or an empty string, the default title is used.
    /// </value>
    [Browsable(true),
        Description("The title of the Open File Dialog."),
     DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public string? Title { get => _profile.Title; set => _profile.Title = value; }

    /// <summary>
    /// Gets or sets the text.
    /// </summary>
    /// <remarks>
    /// This value cannot be changed.
    /// </remarks>
    /// <value>
    /// A string equal to "...".
    /// </value>
    [AllowNull]
    public override string Text
    {
        get => Ellipsis;
        set
        {
            base.Text = Ellipsis;
            Invalidate();
        }
    }
    #endregion

    #region Protected Method Overrides
    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.Click" /> event,
    /// and displays the Open File dialog.
    /// </summary>
    /// <param name="e">
    /// An <see cref="T:System.EventArgs" /> that contains the event data.
    /// </param>
    protected override void OnClick(EventArgs e)
    {
        if (_profile != null)
        {
            CommonDialogProvider.GetSaveAsFileName(_profile);
        }
        base.OnClick(e);
    }
    #endregion

    #region Private Methods / Functions
    /// <summary>
    /// Initializes the component.
    /// </summary>
    private void InitializeComponent()
    {
        Text = "...";
    }
    #endregion

}

