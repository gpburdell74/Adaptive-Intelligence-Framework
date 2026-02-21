using Adaptive.Intelligence.Shared.UI;

namespace Adaptive.Template.Generator.UI;

/// <summary>
/// Provides the central dialog for the application.
/// </summary>
/// <seealso cref="AdaptiveDialogBase" />
public partial class MainDialog : AdaptiveDialogBase
{
    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="MainDialog"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public MainDialog()
    {
        InitializeComponent();
    }

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (!IsDisposed && disposing)
        {
            components?.Dispose();
        }

        components = null;
        base.Dispose(disposing);
    }

    #endregion

    #region Protected Method Overrides    
    /// <summary>
    /// Assigns the event handlers for the controls on the dialog.
    /// </summary>
    protected override void AssignEventHandlers()
    {
        NewButtonButton.Click += HandleNewButtonClicked;
        OpenButtonButton.Click += HandleOpenButtonClicked;

        NewPanelButton.Click += HandleNewPanelClicked;
        OpenPanelButton.Click += HandleOpenPanelClicked;
    }
    /// <summary>
    /// Removes the event handlers for the controls on the dialog.
    /// </summary>
    protected override void RemoveEventHandlers()
    {
        NewButtonButton.Click -= HandleNewButtonClicked;
        OpenButtonButton.Click -= HandleOpenButtonClicked;

        NewPanelButton.Click -= HandleNewPanelClicked;
        OpenPanelButton.Click -= HandleOpenPanelClicked;
    }
    /// <summary>
    /// Sets the state of the UI controls before the data content is loaded.
    /// </summary>
    protected override void SetPreLoadState()
    {
        Cursor = Cursors.WaitCursor;
        SuspendLayout();
        Application.DoEvents();
    }
    /// <summary>
    /// Sets the state of the UI controls after the data content is loaded.
    /// </summary>
    protected override void SetPostLoadState()
    {
        Cursor = Cursors.Default;
        ResumeLayout();
    }
    #endregion

    #region Private Event Handlers
    /// <summary>
    /// Handles the event when the New Button button is clicked.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void HandleNewButtonClicked(object? sender, EventArgs e)
    {
        SetPreLoadState();

        AddEditButtonTemplateDialog addEditDialog = new AddEditButtonTemplateDialog();
        addEditDialog.ShowDialog();
        addEditDialog.Dispose();

        SetPostLoadState();
        SetState();
    }

    /// <summary>
    /// Handles the event when the New Panel button is clicked.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void HandleNewPanelClicked(object? sender, EventArgs e)
    {
        SetPreLoadState();

        AddEditPanelTemplateDialog addEditDialog = new AddEditPanelTemplateDialog();
        addEditDialog.ShowDialog();
        addEditDialog.Dispose();

        SetPostLoadState();
        SetState();
    }

    /// <summary>
    /// Handles the event when the Open Button button is clicked.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void HandleOpenButtonClicked(object? sender, EventArgs e)
    {
        SetPreLoadState();
        string? fileName = GetOpenButtonTemplateFilename();
        if (!string.IsNullOrEmpty(fileName))
        {
            AddEditButtonTemplateDialog editDialog = new AddEditButtonTemplateDialog(fileName);
            editDialog.ShowDialog();
            editDialog.Dispose();
        }

        SetPostLoadState();
        SetState();
    }

    /// <summary>
    /// Handles the event when the Open Panel button is clicked.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void HandleOpenPanelClicked(object? sender, EventArgs e)
    {
        SetPreLoadState();
        string? fileName = GetOpenPanelTemplateFilename();
        if (!string.IsNullOrEmpty(fileName))
        {
            AddEditPanelTemplateDialog editDialog = new AddEditPanelTemplateDialog(fileName);
            editDialog.ShowDialog();
            editDialog.Dispose();
        }

        SetPostLoadState();
        SetState();
    }

    /// <summary>
    /// Handles the event when the Convert Button Templates button is clicked.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void HandleConvertButtonClicked(object? sender, EventArgs e)
    {
        SetPreLoadState();
        ConvertButtonTemplatesDialog dialog = new ConvertButtonTemplatesDialog();
        dialog.ShowDialog();
        dialog.Dispose();
        SetPostLoadState();
        SetState();
    }

    /// <summary>
    /// Handles the event when the Convert Panel Templates button is clicked.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void HandleConvertPanelClicked(object? sender, EventArgs e)
    {
        SetPreLoadState();
        ConvertPanelTemplatesDialog dialog = new ConvertPanelTemplatesDialog();
        dialog.ShowDialog();
        dialog.Dispose();
        SetPostLoadState();
        SetState();
    }
    #endregion

    #region Private Methods / Functions

    /// <summary>
    /// Displays the open file dialog to allow a user to select a button template file.
    /// </summary>
    /// <returns>
    /// A string containing the new path and file name, or <b>null</b> if the user cancels.
    /// </returns>
    private string? GetOpenButtonTemplateFilename()
    {
        return GetOpenTemplateFileName(
            "Open Button Template",
            ".button.template",
            "Button Templates (*.button.template)|*.button.template|JSON Button Templates (*.button.template.json)|*.button.template.json");
    }

    /// <summary>
    /// Displays the open file dialog to allow a user to select a panel template file.
    /// </summary>
    /// <returns>
    /// A string containing the new path and file name, or <b>null</b> if the user cancels.
    /// </returns>
    private string? GetOpenPanelTemplateFilename()
    {
        return GetOpenTemplateFileName(
            "Open Panel Template",
            ".panel.template",
            "Panel Templates (*.panel.template)|*.panel.template|JSON Panel Templates (*.panel.template.json)|*.panel.template.json");
    }

    /// <summary>
    /// Displays the open file dialog to allow a user to select a file.
    /// </summary>
    /// <param name="title">
    /// A string containing the title for the dialog.
    /// </param>
    /// <param name="defaultExt">
    /// A string containing the defaul extension to use for the file dialog.
    /// </param>
    /// <param name="filter">
    /// A string containing the filter definition string.
    /// </param>
    /// <returns>
    /// A string containing the new path and file name, or <b>null</b> if the user cancels.
    /// </returns>
    private string? GetOpenTemplateFileName(string title, string defaultExt, string filter)
    {
        string? fileName = null;

        OpenFileDialog dialog = new OpenFileDialog()
        {
            AddExtension = true,
            AddToRecent = true,
            AutoUpgradeEnabled = true,
            DefaultExt = defaultExt,
            SupportMultiDottedExtensions = true,
            Filter = filter,
            CheckFileExists = true,
            Title = title
        };
        DialogResult result = dialog.ShowDialog();
        if (result == DialogResult.OK)
        {
            fileName = dialog.FileName;
        }
        dialog.Dispose();

        return fileName;
    }
    #endregion
}
