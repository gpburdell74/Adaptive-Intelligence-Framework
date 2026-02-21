using Adaptive.Intelligence.Shared.UI;
using Adaptive.Intelligence.Shared.UI.TemplatedControls;
using System.ComponentModel;

namespace Adaptive.Template.Generator.UI;

/// <summary>
/// Provides the editor for creating / editing all five button state templates.
/// </summary>
/// <seealso cref="AdaptiveControlBase" />
public partial class ButtonTemplateEditorControl : AdaptiveControlBase
{
    #region Public Events
    /// <summary>
    /// Occurs when the template is modified.
    /// </summary>
    public event EventHandler? TemplateChanged;
    #endregion

    #region Private Member Declarations
    /// <summary>
    /// The template
    /// </summary>
    private ButtonTemplate? _template;
    #endregion

    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="ButtonTemplateEditorControl"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public ButtonTemplateEditorControl()
    {
        InitializeComponent();
        _template = new ButtonTemplate();
    }

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (!IsDisposed && disposing)
        {
            components?.Dispose();
        }

        _template = null;
        components = null;
        base.Dispose(disposing);
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets or sets the reference to the template being edited.
    /// </summary>
    /// <value>
    /// The <see cref="ButtonTemplate"/> instance being edited.
    /// </value>
    [Browsable(false),
     DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ButtonTemplate? Template
    {
        get => _template;
        set
        {
            _template = value;
            TestButton.Template = _template;
            if (_template == null)
            {
                NormalEditor.Template = null;
                HoverEditor.Template = null;
                PressedEditor.Template = null;
                DisabledEditor.Template = null;
                CheckedEditor.Template = null;
            }
            else
            {
                NormalEditor.Template = _template.Normal;
                HoverEditor.Template = _template.Hover;
                PressedEditor.Template = _template.Pressed;
                DisabledEditor.Template = _template.Disabled;
                CheckedEditor.Template = _template.Checked;
            }
            OnTemplateChanged(EventArgs.Empty);
            Invalidate();
        }
    }
    #endregion

    #region Protected Methods / Functions    
    /// <summary>
    /// Assigns the event handlers for the controls on the dialog.
    /// </summary>
    protected override void AssignEventHandlers()
    {
        NormalEditor.TemplateChanged += HandleTemplateChanged;
        HoverEditor.TemplateChanged += HandleTemplateChanged;
        PressedEditor.TemplateChanged += HandleTemplateChanged;
        DisabledEditor.TemplateChanged += HandleTemplateChanged;
        CheckedEditor.TemplateChanged += HandleTemplateChanged;

        DisabledCheck.CheckedChanged += HandleDisabledCheckChanged; 
        CheckedCheck.CheckedChanged += HandleCheckedCheckChanged;
    }
    /// <summary>
    /// Removes the event handlers for the controls on the dialog.
    /// </summary>
    /// <returns></returns>
    protected override void RemoveEventHandlers()
    {
        NormalEditor.TemplateChanged -= HandleTemplateChanged;
        HoverEditor.TemplateChanged -= HandleTemplateChanged;
        PressedEditor.TemplateChanged -= HandleTemplateChanged;
        DisabledEditor.TemplateChanged -= HandleTemplateChanged;
        CheckedEditor.TemplateChanged -= HandleTemplateChanged;

        DisabledCheck.CheckedChanged -= HandleDisabledCheckChanged;
        CheckedCheck.CheckedChanged -= HandleCheckedCheckChanged;
    }

    #endregion

    #region Protected Event Methods    
    /// <summary>
    /// Raises the <see cref="E:TemplateChanged" /> event.
    /// </summary>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    protected virtual void OnTemplateChanged(EventArgs e)
    {
        TestButton.Template = _template;
        TemplateChanged?.Invoke(this, e);
    }
    #endregion

    #region Private Event Handlers    
    /// <summary>
    /// Handles the event when a state template changes.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void HandleTemplateChanged(object? sender, EventArgs e)
    {
        OnTemplateChanged(e);
    }
    /// <summary>
    /// Handles the event when the Disabled check box's checked state changes.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void HandleDisabledCheckChanged(object? sender, EventArgs e)
    {
        SetPreLoadState();
        if (_template != null)
        {
            TestButton.Enabled =! DisabledCheck.Checked;
            Invalidate();
            Refresh();
        }
        SetPostLoadState();
    }
    /// <summary>
    /// Handles the event when the Checked check box's checked state changes.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void HandleCheckedCheckChanged(object? sender, EventArgs e)
    {
        SetPreLoadState();
        if (_template != null)
        {
            TestButton.Checked = CheckedCheck.Checked;
            Invalidate();
            Refresh();
        }
        SetPostLoadState();
    }
    #endregion

}
