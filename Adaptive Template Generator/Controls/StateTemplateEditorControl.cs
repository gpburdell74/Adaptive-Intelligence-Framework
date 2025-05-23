using Adaptive.Intelligence.Shared.UI;
using System.ComponentModel;

namespace Adaptive.Template.Generator.UI;

/// <summary>
/// PRovides a control for creating or editing a button state template.
/// </summary>
/// <seealso cref="AdaptiveControlBase" />
public partial class StateTemplateEditorControl : AdaptiveControlBase
{
    #region Public Events    
    /// <summary>
    /// Occurs when the template is modified.
    /// </summary>
    public event EventHandler? TemplateChanged;
    #endregion

    #region Private Member Declarations    
    /// <summary>
    /// The button state for the template.
    /// </summary>
    private Intelligence.Shared.UI.ButtonState _state = Intelligence.Shared.UI.ButtonState.Normal;

    /// <summary>
    /// The template to edit.
    /// </summary>
    private ButtonStateTemplate? _template;
    #endregion

    #region Constructor / Dispose Methods    
    /// <summary>
    /// Initializes a new instance of the <see cref="StateTemplateEditorControl"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public StateTemplateEditorControl()
    {
        InitializeComponent();
        _template = new ButtonStateTemplate();
        PropEditor.SelectedObject = _template;
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
    /// Gets or sets the state of the button for the template.
    /// </summary>
    /// <value>
    /// A <see cref="ButtonState"/> enumerated value indicating the state of the button
    /// the template is being designed for.
    /// </value>
    public Intelligence.Shared.UI.ButtonState ButtonState
    {
        get => _state;
        set
        {
            if (_state != value)
            {
                _state = value;
                SetHeader();
                Invalidate();
            }
        }
    }

    /// <summary>
    /// Gets or sets the reference to the template being edited.
    /// </summary>
    /// <value>
    /// The <see cref="ButtonStateTemplate"/> instance.
    /// </value>
    [Browsable(false),
     DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ButtonStateTemplate? Template
    {
        get => _template;
        set
        {
            _template = value;
            PropEditor.SelectedObject = _template;
            ExamplePanel.Template = _template;
            Invalidate();
        }
    }
    #endregion

    #region Protected Method Overrides    
    /// <summary>
    /// Assigns the event handlers for the controls on the dialog.
    /// </summary>
    protected override void AssignEventHandlers()
    {
        PropEditor.PropertyValueChanged += HandlePropertyValueChanged;
    }
    /// <summary>
    /// Removes the event handlers for the controls on the dialog.
    /// </summary>
    protected override void RemoveEventHandlers()
    {
        PropEditor.PropertyValueChanged -= HandlePropertyValueChanged;
    }
    /// <summary>
    /// Initializes the control and dialog state according to the form data.
    /// </summary>
    protected override void InitializeDataContent()
    {
        ExamplePanel.Template = _template;
    }
    #endregion

    #region Protected Event Methods    
    /// <summary>
    /// Raises the <see cref="E:TemplateChanged" /> event.
    /// </summary>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    protected virtual void OnTemplateChanged(EventArgs e)
    {
        TemplateChanged?.Invoke(this, e);
    }
    #endregion

    #region Private Event Handlers    
    /// <summary>
    /// Handles the event when a property value changes.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void HandlePropertyValueChanged(object? sender, EventArgs e)
    {
        // If the template is null, do nothing.
        if (_template == null)
            return;
        // If the template is not null, set the template to the new value.
        ExamplePanel.Template = _template;
        ExamplePanel.Invalidate();
    }
    #endregion

    #region Private Methods / Functions
    /// <summary>
    /// Sets the header text.
    /// </summary>
    private void SetHeader()
    {
        switch (_state)
        {
            case Intelligence.Shared.UI.ButtonState.Normal:
                Header.Text = "Normal State";
                break;

            case Intelligence.Shared.UI.ButtonState.Checked:
                Header.Text = "Checked State";
                break;

            case Intelligence.Shared.UI.ButtonState.Disabled:
                Header.Text = "Disabled State";
                break;

            case Intelligence.Shared.UI.ButtonState.Hover:
                Header.Text = "Hover State";
                break;

            case Intelligence.Shared.UI.ButtonState.Pressed:
                Header.Text = "Pressed State";
                break;
        }
    }
    #endregion

}
