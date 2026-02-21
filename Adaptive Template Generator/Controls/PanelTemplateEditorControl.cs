using Adaptive.Intelligence.Shared.UI;
using Adaptive.Intelligence.Shared.UI.TemplatedControls;
using System.ComponentModel;

namespace Adaptive.Template.Generator.UI;

/// <summary>
/// Provides the editor for creating / editing all three panel state templates.
/// </summary>
/// <seealso cref="AdaptiveControlBase" />
public partial class PanelTemplateEditorControl : AdaptiveControlBase
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
    private PanelTemplate? _template;
    #endregion

    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="PanelTemplateEditorControl"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public PanelTemplateEditorControl()
    {
        InitializeComponent();
        _template = new PanelTemplate();
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
    /// The <see cref="PanelTemplate"/> instance being edited.
    /// </value>
    [Browsable(false),
     DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public PanelTemplate? Template
    {
        get => _template;
        set
        {
            _template = value;
            TestPanel.Template = _template;
            if (_template == null)
            {
                NormalEditor.Template = null;
                HoverEditor.Template = null;
                DisabledEditor.Template = null;
            }
            else
            {
                NormalEditor.Template = _template.Normal;
                HoverEditor.Template = _template.Hover;
                DisabledEditor.Template = _template.Disabled;
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
        DisabledEditor.TemplateChanged += HandleTemplateChanged;
    }
    /// <summary>
    /// Removes the event handlers for the controls on the dialog.
    /// </summary>
    /// <returns></returns>
    protected override void RemoveEventHandlers()
    {
        NormalEditor.TemplateChanged -= HandleTemplateChanged;
        HoverEditor.TemplateChanged -= HandleTemplateChanged;
        DisabledEditor.TemplateChanged -= HandleTemplateChanged;
    }

    #endregion

    #region Protected Event Methods    
    /// <summary>
    /// Raises the <see cref="E:TemplateChanged" /> event.
    /// </summary>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    protected virtual void OnTemplateChanged(EventArgs e)
    {
        TestPanel.Template = _template;
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
    #endregion
}

