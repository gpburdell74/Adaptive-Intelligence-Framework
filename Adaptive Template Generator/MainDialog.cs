using Adaptive.Intelligence.Shared.UI;

namespace Adaptive.Template.Generator.UI;

/// <summary>
/// Provides the central dialog for the application.
/// </summary>
/// <seealso cref="AdaptiveDialogBase" />
public partial class MainDialog : AdaptiveDialogBase
{
    #region Private Member Declarations    
    /// <summary>
    /// The last file name used.
    /// </summary>
    private string? _lastFileName = null;

    /// <summary>
    /// The template being created or edited.
    /// </summary>
    private ButtonTemplate? _template;
    #endregion

    #region    
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
            _template?.Dispose();
            components?.Dispose();
        }

        _template = null;
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
        NewButton.Click += HandleNewClicked;
        OpenButton.Click += HandleOpenClicked;
        SaveButton.Click += HandleSaveClicked;
        SaveAsButton.Click += HandleSaveAsClicked;
    }
    /// <summary>
    /// Removes the event handlers for the controls on the dialog.
    /// </summary>
    protected override void RemoveEventHandlers()
    {
        NewButton.Click -= HandleNewClicked;
        OpenButton.Click -= HandleOpenClicked;
        SaveButton.Click -= HandleSaveClicked;
        SaveAsButton.Click -= HandleSaveAsClicked;
    }
    /// <summary>
    /// Sets the state of the UI controls before the data content is loaded.
    /// </summary>
    protected override void SetPreLoadState()
    {
        Cursor = Cursors.WaitCursor;
        Toolbar.Enabled = false;
        TemplateEditor.Enabled = false;
        SuspendLayout();
        Application.DoEvents();
    }
    /// <summary>
    /// Sets the state of the UI controls after the data content is loaded.
    /// </summary>
    protected override void SetPostLoadState()
    {
        Cursor = Cursors.Default;
        Toolbar.Enabled = true;
        TemplateEditor.Enabled = true;
        ResumeLayout();
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
        bool fileOpen = (_template != null);

        SaveButton.Enabled = (!string.IsNullOrEmpty(_lastFileName));
        SaveButton.Visible = fileOpen;
        SaveAsButton.Visible = fileOpen;
        TemplateEditor.Visible = fileOpen;
    }
    #endregion

    #region Private Event Handlers    
    /// <summary>
    /// Handles the event when the New button is clicked.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void HandleNewClicked(object? sender, EventArgs e)
    {
        SetPreLoadState();
        _template = new ButtonTemplate();
        TemplateEditor.Template = _template;
        TemplateEditor.Visible = true;
        SetPostLoadState();
        SetState();
    }
    /// <summary>
    /// Handles the event when the Open button is clicked.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void HandleOpenClicked(object? sender, EventArgs e)
    {
        SetPreLoadState();
        string? fileName = GetOpenFilename();
        if (!string.IsNullOrEmpty(fileName))
        {
            _template = ButtonTemplate.Load(fileName);
            if (_template == null)
                ShowError("Error Loading File", "Could not load the template file.");
            else
            {
                _lastFileName = fileName;
                TemplateEditor.Template = _template;
                TemplateEditor.Visible = true;
                Invalidate();
            }
        }
        SetPostLoadState();
        SetState();
    }
    /// <summary>
    /// Handles the event when the Save button is clicked.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void HandleSaveClicked(object? sender, EventArgs e)
    {
        SetPreLoadState();
        if (_lastFileName == null)
            HandleSaveAsClicked(sender, e);
        else
        {
            _template?.Save(_lastFileName);
        }
        SetPostLoadState();
        SetState();
    }
    /// <summary>
    /// Handles the event when the Save As button is clicked.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void HandleSaveAsClicked(object? sender, EventArgs e)
    {
        SetPreLoadState();
        string? fileName = GetSaveAsFileName();
        if (!string.IsNullOrEmpty(fileName))
        {
            _lastFileName = fileName;
            _template?.Save(fileName);
        }
        SetPostLoadState();
        SetState();
    }

    #endregion

    #region Private Methods / Functions    
    /// <summary>
    /// Gets the file name to save the edited template to.
    /// </summary>
    /// <returns>
    /// A string containing the fully-qualified path and name of the file.
    /// </returns>
    private string? GetOpenFilename()
    {
        string? fileName = null;

        OpenFileDialog dialog = new OpenFileDialog()
        {
            AddExtension = true,
            AddToRecent = true,
            AutoUpgradeEnabled = true,
            DefaultExt = "template",
            SupportMultiDottedExtensions = true,
            Filter = "Button Templates (*.template)|*.template",
            CheckFileExists = true,
            Title = "Open Button Template"
        };
        DialogResult result = dialog.ShowDialog();
        if (result == DialogResult.OK)
        {
            fileName = dialog.FileName;
        }
        dialog.Dispose();

        return fileName;
    }
    /// <summary>
    /// Gets the file name to save the edited template to.
    /// </summary>
    /// <returns>
    /// A string containing the fully-qualified path and name of the file.
    /// </returns>
    private string? GetSaveAsFileName()
    {
        string? fileName = null;

        SaveFileDialog dialog = new SaveFileDialog()
        {
             AddExtension = true,
             AddToRecent = true,
             AutoUpgradeEnabled = true,
             DefaultExt = "template",
             SupportMultiDottedExtensions=true,
             Filter = "Button Templates (*.template)|*.template",
             CheckWriteAccess = true,
             Title = "Save Button Template As"
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

    #region
    #endregion

}
