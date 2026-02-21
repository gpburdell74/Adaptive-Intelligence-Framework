using Adaptive.Intelligence.Shared.IO;
using Adaptive.Intelligence.Shared.UI;

namespace Adaptive.Template.Generator.UI;

/// <summary>
/// Provides a dialog to allow the user to convert button templates from the old format to the new format.
/// </summary>
/// <seealso cref="Adaptive.Intelligence.Shared.UI.AdaptiveDialogBase" />
public partial class ConvertButtonTemplatesDialog : AdaptiveDialogBase
{
    #region Constructor / Dispose Methods    
    /// <summary>
    /// Initializes a new instance of the <see cref="ConvertButtonTemplatesDialog"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public ConvertButtonTemplatesDialog()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources;
    /// <c>false</c> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        if ( !IsDisposed && disposing)
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
        CloseButton.Click += HandleCloseClicked;
        ConvertButton.Click += HandleConvertClicked;
        SaveAsButton.Click += HandleSaveAsClicked;
        SelectButton.Click += HandleSelectClicked;

        SelectText.TextChanged += HandleGenericControlChange;
        SaveAsText.TextChanged += HandleGenericControlChange;

        FromOldFormatOption.CheckedChanged += HandleConvertOptionChanged;
        FromJsonOption.CheckedChanged += HandleConvertOptionChanged;
        ToOldFormatOption.CheckedChanged += HandleConvertOptionChanged;
        ToJsonOption.CheckedChanged += HandleConvertOptionChanged;

    }

    /// <summary>
    /// Removes the event handlers for the controls on the dialog.
    /// </summary>
    protected override void RemoveEventHandlers()
    {
        CloseButton.Click -= HandleCloseClicked;
        ConvertButton.Click -= HandleConvertClicked;
        SaveAsButton.Click -= HandleSaveAsClicked;
        SelectButton.Click -= HandleSelectClicked;

        SelectText.TextChanged -= HandleGenericControlChange;
        SaveAsText.TextChanged -= HandleGenericControlChange;

        FromOldFormatOption.CheckedChanged -= HandleConvertOptionChanged;
        FromJsonOption.CheckedChanged -= HandleConvertOptionChanged;
        ToOldFormatOption.CheckedChanged -= HandleConvertOptionChanged;
        ToJsonOption.CheckedChanged -= HandleConvertOptionChanged;
    }

    /// <summary>
    /// When implemented in a derived class, sets the display state for the controls on the dialog based on
    /// current conditions.
    /// </summary>
    /// <remarks>
    /// This is called by <see cref="M:Adaptive.Intelligence.Shared.UI.AdaptiveDialogBase.SetState" /> after <see cref="M:Adaptive.Intelligence.Shared.UI.AdaptiveDialogBase.SetSecurityState" /> is called.
    /// </remarks>
    protected override void SetDisplayState()
    {
        bool canConvert = 
            SaveAsText.Text.Length > 0 && 
            SelectText.Text.Length > 0 &&
            SafeIO.FileExists(SelectText.Text);

        ConvertButton.Enabled = canConvert;
    }

    /// <summary>
    /// Sets the state of the UI controls before the data content is loaded.
    /// </summary>
    protected override void SetPreLoadState()
    {
        Cursor = Cursors.WaitCursor;
        CloseButton.Enabled = false;
        ConvertButton.Enabled = false;
        SaveAsButton.Enabled = false;
        SelectButton.Enabled = false;
        SelectText.Enabled = false;
        SaveAsText.Enabled = false;
        FromGroup.Enabled = false;
        ToGroup.Enabled = false;
        Invalidate();
        Application.DoEvents();
        SuspendLayout();
    }

    /// <summary>
    /// Sets the state of the UI controls after the data content is loaded.
    /// </summary>
    protected override void SetPostLoadState()
    {
        Cursor = Cursors.Default;
        CloseButton.Enabled = true;
        ConvertButton.Enabled = true;
        SaveAsButton.Enabled = true;
        SelectButton.Enabled = true;
        SelectText.Enabled = true;
        SaveAsText.Enabled = true;
        FromGroup.Enabled = true;
        ToGroup.Enabled = true;
        ResumeLayout();
    }
    #endregion


    #region Private Event Handlers

    /// <summary>
    /// Handles the event when one of the conversion option values changes.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void HandleConvertOptionChanged(object? sender, EventArgs e)
    {
        FromOldFormatOption.CheckedChanged -= HandleConvertOptionChanged;
        FromJsonOption.CheckedChanged -= HandleConvertOptionChanged;
        ToOldFormatOption.CheckedChanged -= HandleConvertOptionChanged;
        ToJsonOption.CheckedChanged -= HandleConvertOptionChanged;

        if (sender == FromOldFormatOption && FromOldFormatOption.Checked)
        {
            ToJsonOption.Checked = true;
        }
        else if (sender == FromJsonOption && FromJsonOption.Checked)
        {
            ToOldFormatOption.Checked = true;
        }
        else if (sender == ToOldFormatOption && ToOldFormatOption.Checked)
        {
            FromJsonOption.Checked = true;
        }
        else if (sender == ToJsonOption && ToJsonOption.Checked)
        {
            FromOldFormatOption.Checked = true;
        }

        FromOldFormatOption.CheckedChanged += HandleConvertOptionChanged;
        FromJsonOption.CheckedChanged += HandleConvertOptionChanged;
        ToOldFormatOption.CheckedChanged += HandleConvertOptionChanged;
        ToJsonOption.CheckedChanged += HandleConvertOptionChanged;
    }

    /// <summary>
    /// Handles the event when the Select Ellipsis button is clicked to select a button template file.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void HandleSelectClicked(object? sender, EventArgs e)
    {
        SetPreLoadState();
        string? fileName = GetOpenButtonTemplateFilename();
        if (!string.IsNullOrEmpty(fileName) && SafeIO.FileExists(fileName))
        {
            SelectText.Text = fileName;
        }
        SetPostLoadState();
        SetState();
    }

    /// <summary>
    /// Handles the event when the Select Ellipsis button is clicked to select a button template file.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void HandleSaveAsClicked(object? sender, EventArgs e)
    {
        SetPreLoadState();
        string? fileName = GetOpenButtonTemplateFilename();
        if (!string.IsNullOrEmpty(fileName) && SafeIO.FileExists(fileName))
        {
            SelectText.Text = fileName;
        }
        SetPostLoadState();
        SetState();
    }

    /// <summary>
    /// Handles the event when the Convert button is clicked.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void HandleConvertClicked(object? sender, EventArgs e)
    {
        SetPreLoadState();
        SetPostLoadState();
        SetState();
    }

    /// <summary>
    /// Handles the event when the Close button is clicked.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void HandleCloseClicked(object? sender, EventArgs e)
    {
        SetPreLoadState();
        Close();
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
            "*.template",
            "AI Control Templates (*.template)|*.template|JSON Files (*.json)|*..json|All Files (*.*)|*.*)");
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
