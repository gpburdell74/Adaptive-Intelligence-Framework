using Adaptive.Intelligence.Shared.UI;
using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.Shared.UI.TemplatedControls;
using Adaptive.Intelligence.Shared.UI.TemplatedControls.Templates.IO;

namespace Adaptive.Template.Generator.UI;

/// <summary>
/// Provides a dialog for creating or editing a panel template.
/// </summary>
/// <seealso cref="Adaptive.Intelligence.Shared.UI.AdaptiveDialogBase" />
public partial class AddEditPanelTemplateDialog : AdaptiveDialogBase
{
    #region Private Member Declarations
    /// <summary>
    /// The last file name that was used.
    /// </summary>
    private string? _lastFileName = null;

    /// <summary>
    /// The template instance.
    /// </summary>
    private PanelTemplate? _template = null;

    /// <summary>
    /// The unsaved changes flag.
    /// </summary>
    private bool _unsavedChanges;
    #endregion

    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="AddEditButtonTemplateDialog"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public AddEditPanelTemplateDialog()
    {
        InitializeComponent();
        _template = new PanelTemplate();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AddEditPanelTemplateDialog"/> class.
    /// </summary>
    /// <param name="fileName">
    /// A string containing the path and name of the template file.
    /// </param>

    public AddEditPanelTemplateDialog(string fileName) : this()
    {
        _lastFileName = fileName;
        PerformFileLoad();
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (!IsDisposed && disposing)
        {
            _template?.Dispose();
            components?.Dispose();
        }

        _template = null;
        _lastFileName = null;
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
        SaveButton.Click += HandleSaveClicked;
        SaveAsButton.Click += HandleSaveAsClicked;
        CloseButton.Click += HandleCloseClicked;

        Editor.TemplateChanged += HandleTemplateChanged;
    }

    /// <summary>
    /// Removes the event handlers for the controls on the dialog.
    /// </summary>
    protected override void RemoveEventHandlers()
    {
        SaveButton.Click -= HandleSaveClicked;
        SaveAsButton.Click -= HandleSaveAsClicked;
        CloseButton.Click -= HandleCloseClicked;

        Editor.TemplateChanged -= HandleTemplateChanged;
    }

    /// <summary>
    /// Initializes the control and dialog state according to the form data.
    /// </summary>
    protected override void InitializeDataContent()
    {
        if (_lastFileName == null)
        {
            Text = "Add New Panel Template";
        }
        else
        {
            Text = $"Edit Panel Template - {_lastFileName}";
        }

        Editor.Template = _template;
        Invalidate();
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
        bool canSave = _template != null;

        if (_unsavedChanges)
        {
            CloseButton.Text = "Cancel";
        }
        else
        {
            CloseButton.Text = "Close";
        }

        SaveButton.Enabled = canSave;
        SaveAsButton.Enabled = canSave;
    }

    /// <summary>
    /// Sets the state of the UI controls before the data content is loaded.
    /// </summary>
    protected override void SetPreLoadState()
    {
        Cursor = Cursors.WaitCursor;
        Editor.Enabled = false;
        SaveButton.Enabled = false;
        SaveAsButton.Enabled = false;
        CloseButton.Enabled = false;
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
        Editor.Enabled = true;
        SaveButton.Enabled = true;
        SaveAsButton.Enabled = true;
        CloseButton.Enabled = true;
        SetDisplayState();
        Invalidate();
        Application.DoEvents();
        ResumeLayout();
    }
    #endregion

    #region Private Event Handlers
    /// <summary>
    /// Handles the event when the template changes.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void HandleTemplateChanged(object? sender, EventArgs e)
    {
        _unsavedChanges = true;
        SetDisplayState();
    }
    /// <summary>
    /// Handles the Event when the Save button is clicked.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void HandleSaveClicked(object? sender, EventArgs e)
    {
        if (_template != null && _lastFileName != null)
        {
            SetPreLoadState();

            OperationalResult result = new OperationalResult();
            TemplateFileManager.SavePanelTemplate(_template, _lastFileName, TemplateFormats.JsonTextMinifiedFormat, result);
            if (!result.Success)
            {
                string message = $"Failed to save template to file '{_lastFileName}'.";
                if (result.HasExceptions)
                    message += $" Error: {result.FirstException?.Message}";
                MessageBox.Show(message, "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                _unsavedChanges = false;
            }
        }
        SetPostLoadState();
        SetDisplayState();
    }

    /// <summary>
    /// Handles the event when the Save As button is clicked.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void HandleSaveAsClicked(object? sender, EventArgs e)
    {
        SetPreLoadState();
        if (_template != null)
        {
            string? newFileName = GetSaveAsFileName();
            if (!string.IsNullOrEmpty(newFileName))
            {
                _lastFileName = newFileName;
                HandleSaveClicked(sender, e);
            }
        }
        SetPostLoadState();
        SetDisplayState();
    }

    /// <summary>
    /// Handles the event when the Close/Cancel button is clicked.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void HandleCloseClicked(object? sender, EventArgs e)
    {
        bool canContinue = true;

        SetPreLoadState();
        if (_unsavedChanges)
        {
            canContinue = GetUserConfirmation(
                "Unsaved Template Changes",
                "There are unsaved changes to the template. Do you want to save before closing?");

            if (canContinue)
            {
                HandleSaveClicked(sender, e);
            }
        }
        Close();
    }
    #endregion

    #region Private Methods / Functions
    /// <summary>
    /// Performs the template file load process.
    /// </summary>
    private void PerformFileLoad()
    {
        if (!string.IsNullOrEmpty(_lastFileName) && File.Exists(_lastFileName))
        {
            TemplateFormats format = TemplateFormats.OldVersion92Format;
            if (_lastFileName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                format = TemplateFormats.JsonTextFormat;

            OperationalResult result = new OperationalResult();
            _template = TemplateFileManager.LoadPanelTemplate(_lastFileName, format, result);
            if (!result.Success)
            {
                string message = $"Failed to load template from file '{_lastFileName}'.";
                if (result.HasExceptions)
                    message += $" Error: {result.FirstException?.Message}";
                MessageBox.Show(message, "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _template = null;
                _lastFileName = null;
            }
        }
    }

    /// <summary>
    /// Displayes the Save As Dialog to allow the user to specify a path and file name to save
    /// the template to.
    /// </summary>
    /// <returns>
    /// A string containing the path and name of the file, otherwise null if the user cancels out of the dialog.
    /// </returns>
    private string? GetSaveAsFileName()
    {
        string? fileName = null;

        SaveFileDialog saveFileDialog = new SaveFileDialog
        {
            Filter = "JSON Template Files (*.panel.template.json)|*.json|All Files (*.*)|*.*",
            DefaultExt = "panel.template.json",
            AddExtension = true,
            Title = "Save Panel Template As",
            FileName = _lastFileName ?? "New Template.panel.template.json"
        };
        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            fileName = saveFileDialog.FileName;
        }
        saveFileDialog.Dispose();

        return fileName;
    }
    #endregion
}