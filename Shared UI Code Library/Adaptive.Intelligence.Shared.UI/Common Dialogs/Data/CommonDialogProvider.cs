namespace Adaptive.Intelligence.Shared.UI.CommonDialogs;

/// <summary>
/// Provides static methods / functions for using Windows Common Dialogs.
/// </summary>
public static class CommonDialogProvider
{
    /// <summary>
    /// The default filter string.
    /// </summary>
    private const string DEFAULT_FILTER = "All Files (*.*)|*.*";

    #region Opening Files

    /// <summary>
    /// Displays the common Open File dialog and returns the selected file name, 
    /// or <c>null</c> if the user cancels the dialog.
    /// </summary>
    /// <param name="dialogTitle">
    /// A string containing the title of the Open File dialog.
    /// </param>
    /// <param name="fileFilter">
    /// A string containing the filter string that determines what types of files are displayed.
    /// </param>
    /// <returns>
    /// A string containing the fully-qualified path and name of the file to be 
    /// opened, or <c>null</c> if the user cancels the dialog.
    /// </returns>
    public static string? GetFileToOpen(string dialogTitle, string? fileFilter)
    {
        return GetFileToOpen(dialogTitle, fileFilter, 0, null, null);
    }

    /// <summary>
    /// Displays the common Open File dialog and returns the selected file name, 
    /// or <c>null</c> if the user cancels the dialog.
    /// </summary>
    /// <param name="dialogTitle">
    /// A string containing the title of the Open File dialog.
    /// </param>
    /// <param name="fileFilter">
    /// A string containing the filter string that determines what types of files are displayed.
    /// </param>
    /// <param name="defaultExtension">
    /// A string containing the default file name extension. This extension is added to file names 
    /// that do not have an extension when the user specifies a file name and clicks the Save button.
    /// </param>
    /// <returns>
    /// A string containing the fully-qualified path and name of the file to be 
    /// opened, or <c>null</c> if the user cancels the dialog.
    /// </returns>
    public static string? GetFileToOpen(string dialogTitle, string? fileFilter, string? defaultExtension)
    {
        return GetFileToOpen(dialogTitle, fileFilter, 0, defaultExtension, null);
    }

    /// <summary>
    /// Displays the common Open File dialog and returns the selected file name, 
    /// or <c>null</c> if the user cancels the dialog.
    /// </summary>
    /// <param name="dialogTitle">
    /// A string containing the title of the Open File dialog.
    /// </param>
    /// <param name="fileFilter">
    /// A string containing the filter string that determines what types of files are displayed.
    /// </param>
    /// <param name="filterIndex">
    /// An integer specifying the index of the filter currently selected in the file dialog. The first 
    /// filter is indexed as 1.
    /// </param>
    /// <param name="defaultExtension">
    /// A string containing the default file name extension. This extension is added to file names that 
    /// do not have an extension when the user specifies a file name and clicks the Save button.
    /// </param>
    /// <param name="initialDirectory">
    /// A string containing the initial directory displayed by the file dialog when it is opened. This 
    /// can be a fully qualified path or a special folder name.
    /// </param>
    /// <returns>
    /// A string containing the fully-qualified path and name of the file to be 
    /// opened, or <c>null</c> if the user cancels the dialog.
    /// </returns>
    public static string? GetFileToOpen(
        string? dialogTitle, 
        string? fileFilter, 
        int filterIndex, 
        string? defaultExtension, 
        string? initialDirectory)
    {
        OpenFileProfile profile = new OpenFileProfile
        {
            Title = dialogTitle,
            Filter = fileFilter,
            FilterIndex = filterIndex,
            DefaultExt = defaultExtension,
            InitialDirectory = initialDirectory
        };
        return GetFileToOpen(profile);
    }

    /// <summary>
    /// Displays the common Open File dialog and returns the selected file name, 
    /// or <c>null</c> if the user cancels the dialog.
    /// </summary>
    /// <param name="profile">
    /// The <see cref="IOpenFileProfile"/> instance containing the configuration of 
    /// the parameters for the Open File dialog.
    /// </param>
    /// <returns>
    /// A string containing the fully-qualified path and name of the file to be 
    /// opened, or <c>null</c> if the user cancels the dialog.
    /// </returns>
    public static string? GetFileToOpen(IOpenFileProfile profile)
    {
        string? selectedFileName = null;

        // Specify the default filter if one is not provided.
        if (string.IsNullOrEmpty(profile.Filter))
        {
            profile.Filter = DEFAULT_FILTER;
        }

        OpenFileDialog dialog = new OpenFileDialog
        {
            AddExtension = profile.AddExtension,
            AddToRecent = profile.AddToRecent,
            AutoUpgradeEnabled = profile.AutoUpgradeEnabled,
            CheckFileExists = profile.CheckFileExists,
            CheckPathExists = profile.CheckPathExists,
            DefaultExt = profile.DefaultExt,
            Filter = profile.Filter,
            FilterIndex = profile.FilterIndex,
            InitialDirectory = profile.InitialDirectory,
            RestoreDirectory = profile.RestoreDirectory,
            SelectReadOnly = profile.SelectReadOnly,
            ShowPreview = profile.ShowPreview,
            ShowReadOnly = profile.ShowReadOnly,
            ShowHiddenFiles = profile.ShowHiddenFiles,
            ShowHelp = profile.ShowHelp,
            SupportMultiDottedExtensions = profile.SupportMultiDottedExtensions,
            Title = profile.Title
        };

        DialogResult result = dialog.ShowDialog();
        if (result == DialogResult.OK)
        {
            selectedFileName = dialog.FileName;
        }

        dialog.Dispose();
        return selectedFileName;
    }
    #endregion

    #region Saving Files

    /// <summary>
    /// Displays the common Save As File dialog and returns the selected file name, 
    /// or <c>null</c> if the user cancels the dialog.
    /// </summary>
    /// <param name="dialogTitle">
    /// A string containing the title of the Save as File dialog.
    /// </param>
    /// <param name="fileFilter">
    /// A string containing the filter string that determines what types of files are displayed.
    /// </param>
    /// <returns>
    /// A string containing the fully-qualified path and name of the file to be 
    /// created and/or written, or <c>null</c> if the user cancels the dialog.
    /// </returns>
    public static string? GetSaveAsFileName(string dialogTitle, string? fileFilter)
    {
        return GetSaveAsFileName(dialogTitle, fileFilter, 0, null, null);
    }

    /// <summary>
    /// Displays the common Save As File dialog and returns the selected file name, 
    /// or <c>null</c> if the user cancels the dialog.
    /// </summary>
    /// <param name="dialogTitle">
    /// A string containing the title of the Open File dialog.
    /// </param>
    /// <param name="fileFilter">
    /// A string containing the filter string that determines what types of files are displayed.
    /// </param>
    /// <param name="defaultExtension">
    /// A string containing the default file name extension. This extension is added to file names 
    /// that do not have an extension when the user specifies a file name and clicks the Save button.
    /// </param>
    /// <returns>
    /// A string containing the fully-qualified path and name of the file to be 
    /// created and/or written, or <c>null</c> if the user cancels the dialog.
    /// </returns>
    public static string? GetSaveAsFileName(string dialogTitle, string? fileFilter, string? defaultExtension)
    {
        return GetSaveAsFileName(dialogTitle, fileFilter, 0, defaultExtension, null);
    }

    /// <summary>
    /// Displays the common Save As File dialog and returns the selected file name, 
    /// or <c>null</c> if the user cancels the dialog.
    /// </summary>
    /// <param name="dialogTitle">
    /// A string containing the title of the Open File dialog.
    /// </param>
    /// <param name="fileFilter">
    /// A string containing the filter string that determines what types of files are displayed.
    /// </param>
    /// <param name="filterIndex">
    /// An integer specifying the index of the filter currently selected in the file dialog. The first 
    /// filter is indexed as 1.
    /// </param>
    /// <param name="defaultExtension">
    /// A string containing the default file name extension. This extension is added to file names that 
    /// do not have an extension when the user specifies a file name and clicks the Save button.
    /// </param>
    /// <param name="initialDirectory">
    /// A string containing the initial directory displayed by the file dialog when it is opened. This 
    /// can be a fully qualified path or a special folder name.
    /// </param>
    /// <returns>
    /// A string containing the fully-qualified path and name of the file to be 
    /// created and/or written, or <c>null</c> if the user cancels the dialog.
    /// </returns>
    public static string? GetSaveAsFileName(
        string? dialogTitle,
        string? fileFilter,
        int filterIndex,
        string? defaultExtension,
        string? initialDirectory)
    {
        SaveAsFileProfile profile = new SaveAsFileProfile
        {
            Title = dialogTitle,
            Filter = fileFilter,
            FilterIndex = filterIndex,
            DefaultExt = defaultExtension,
            InitialDirectory = initialDirectory
        };
        return GetSaveAsFileName(profile);
    }

    /// <summary>
    /// Displays the common Save As File As dialog and returns the selected file name, 
    /// or <c>null</c> if the user cancels the dialog.
    /// </summary>
    /// <param name="profile">
    /// The <see cref="ISaveAsFileProfile"/> instance containing the configuration of 
    /// the parameters for the Save As File dialog.
    /// </param>
    /// <returns>
    /// A string containing the fully-qualified path and name of the file to be 
    /// created and/or written, or <c>null</c> if the user cancels the dialog.
    /// </returns>
    public static string? GetSaveAsFileName(ISaveAsFileProfile profile)
    {
        string? selectedFileName = null;

        // Specify the default filter if one is not provided.
        if (string.IsNullOrEmpty(profile.Filter))
        {
            profile.Filter = DEFAULT_FILTER;
        }

        SaveFileDialog dialog = new SaveFileDialog
        {
            AddExtension = profile.AddExtension,
            AddToRecent = profile.AddToRecent,
            AutoUpgradeEnabled = profile.AutoUpgradeEnabled,
            CheckFileExists = profile.CheckFileExists,
            CheckPathExists = profile.CheckPathExists,
            CheckWriteAccess = profile.CheckWriteAccess,
            DefaultExt = profile.DefaultExt,
            Filter = profile.Filter,
            FilterIndex = profile.FilterIndex,
            InitialDirectory = profile.InitialDirectory,
            OverwritePrompt = profile.OverwritePrompt,
            RestoreDirectory = profile.RestoreDirectory,
            ShowHelp = profile.ShowHelp,
            ShowHiddenFiles = profile.ShowHiddenFiles,
            SupportMultiDottedExtensions = profile.SupportMultiDottedExtensions,
            Title = profile.Title
        };

        DialogResult result = dialog.ShowDialog();
        if (result == DialogResult.OK)
        {
            selectedFileName = dialog.FileName;
        }

        dialog.Dispose();
        return selectedFileName;
    }
    #endregion

}
