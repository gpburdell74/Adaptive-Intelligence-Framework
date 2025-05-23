using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace Adaptive.Intelligence.Shared.UI.Controls;

/// <summary>
/// Provides an advanced styling button that supports templating.
/// </summary>
/// <seealso cref="Button" />
[ToolboxItem(typeof(Button))]
public class TemplatedButton : Button
{
    #region Private Member Declarations

    private string? _templateFile;
    /// <summary>
    /// The template.
    /// </summary>
    private ButtonTemplate? _template;

    /// <summary>
    /// The button drawing instance.
    /// </summary>
    private TemplatedButtonDrawingAlgorithm? _painter;

    // Internal flags.
    private bool _checked;

    #endregion

    #region Constructor / Dispose Methods

    /// <summary>
    /// Initializes a new instance of the <see cref="TemplatedButton"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public TemplatedButton()
    {
        // Set for owner drawing.
        SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        SetStyle(ControlStyles.DoubleBuffer, true);
        SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        SetStyle(ControlStyles.ResizeRedraw, true);
        SetStyle(ControlStyles.Selectable, true);
        SetStyle(ControlStyles.StandardClick, true);
        SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        SetStyle(ControlStyles.UserPaint, true);

        // Create default objects.
        _template = new ButtonTemplate();
        _painter = new TemplatedButtonDrawingAlgorithm(_template);
    }
    /// <summary>
    /// Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ButtonBase" /> and optionally releases the managed resources.
    /// </summary>
    /// <param name="disposing"><see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        if (!IsDisposed && disposing)
        {
            _template?.Dispose();
            _painter?.Dispose();
        }

        _template = null;
        _painter = null;

        base.Dispose(disposing);
    }

    #endregion

    #region Public Properties
    /// <summary>
    /// Gets or sets a value indicating whether the button is in a checked state.
    /// </summary>
    /// <value>
    ///   <b>true</b> if the button is in a checked state; otherwise, <b>false</b>.
    /// </value>
    [Browsable(true),
     Category("Behavior"),
     DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
     Description("Gets or sets the checked state of the button.")]
    public bool Checked
    {
        get => _checked;
        set
        {
            _checked = value;
            if (value)
            {
                _painter!.State = ButtonState.Checked;
            }
            else
            {
                _painter!.State = ButtonState.Normal;
            }
            Invalidate();
        }
    }

    /// <summary>
    /// Gets or sets the reference to the button template to use when drawing the button.
    /// </summary>
    /// <value>
    /// The <see cref="ButtonTemplate"/> instance.
    /// </value>
    [Browsable(false),
     Category("Appearance"),
     Description("Gets or sets the button template to use."),
     DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ButtonTemplate? Template
    {
        get => _template;
        set
        {
            if (_template != value)
            {
                _painter?.Dispose();
                _template = value;
                if (_template == null)
                    _template = new ButtonTemplate();

                _painter = new TemplatedButtonDrawingAlgorithm(_template);
                Invalidate();
            }
        }
    }

    [Browsable(true),
     Category("Appearance"),
     Description("Gets or sets the button template file to use."),
     DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
     Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
    public string? TemplateFile
    {
        get => _templateFile;
        set
        {
            _templateFile = value;
            if (value != null)
            {
                ButtonTemplate testTemplate = ButtonTemplate.Load(_templateFile);
                if (testTemplate != null)
                {
                    Template = testTemplate;
                }
            }
            Invalidate();
        }
    }
    #endregion

    #region Protected Method Overrides    
    /// <summary>
    /// Raises the <see cref="Control.EnabledChanged" /> event 
    /// and sets the button state accordingly.
    /// </summary>
    /// <param name="e">
    /// An <see cref="EventArgs" /> that contains the event data.
    /// </param>
    protected override void OnEnabledChanged(EventArgs e)
    {
        base.OnEnabledChanged(e);
        if (!Enabled)
        {
            _painter!.State = ButtonState.Disabled;
            Invalidate();
        }
    }

    /// <summary>
    /// Raises the <see cref="Control.MouseEnter" /> event 
    /// and sets the button state accordingly.
    /// </summary>
    /// <param name="e">
    /// An <see cref="EventArgs" /> that contains the event data.
    /// </param>
    protected override void OnMouseEnter(EventArgs e)
    {
        if (Enabled && _painter!.State != ButtonState.Hover && _painter.State != ButtonState.Checked)
        {
            _painter!.State = ButtonState.Hover;
            Invalidate();
        }
        base.OnMouseEnter(e);
    }

    /// <summary>
    /// Raises the <see cref="Control.MouseLeave" /> event 
    /// and sets the button state accordingly.
    /// </summary>
    /// <param name="e">
    /// An <see cref="EventArgs" /> that contains the event data.
    /// </param>
    protected override void OnMouseLeave(EventArgs e)
    {
        if (Enabled && _painter!.State != ButtonState.Normal && _painter.State != ButtonState.Checked)
        {
            _painter!.State = ButtonState.Normal;
            Invalidate();
        }
        base.OnMouseLeave(e);
    }

    /// <summary>
    /// Raises the <see cref="Control.MouseDown" /> event 
    /// and sets the button state accordingly.
    /// </summary>
    /// <param name="e">
    /// An <see cref="EventArgs" /> that contains the event data.
    /// </param>
    protected override void OnMouseDown(MouseEventArgs e)
    {
        if (Enabled && _painter!.State != ButtonState.Pressed && _painter.State != ButtonState.Checked)
        {
            _painter!.State = ButtonState.Pressed;
            Invalidate();
        }
        base.OnMouseDown(e);
    }

    /// <summary>
    /// Raises the <see cref="Control.MouseUp" /> event 
    /// and sets the button state accordingly.
    /// </summary>
    /// <param name="e">
    /// An <see cref="EventArgs" /> that contains the event data.
    /// </param>
    protected override void OnMouseUp(MouseEventArgs e)
    {
        if (Enabled && _painter!.State != ButtonState.Normal && _painter.State != ButtonState.Checked)
        {
            _painter!.State = ButtonState.Normal;
            Invalidate();
        }
        base.OnMouseUp(e);
    }

    /// <summary>
    /// Paints the background of the control.
    /// </summary>
    /// <param name="e">
    /// The <see cref="PaintEventArgs"/> instance containing the event data and 
    /// the <see cref="Graphics"/> instance used for drawing.
    /// </param>
    protected override void OnPaintBackground(PaintEventArgs e)
    {
        if (Visible)
        {
            if (_painter != null)
                _painter.DrawBackground(e.Graphics, ClientRectangle);
            else
                base.OnPaintBackground(e);
        }
    }

    /// <summary>
    /// Raises the <see cref="Control.Paint" /> event and paints the control.
    /// </summary>
    /// <param name="e">
    /// The <see cref="PaintEventArgs"/> instance containing the event data and 
    /// the <see cref="Graphics"/> instance used for drawing.
    /// </param>
    protected override void OnPaint(PaintEventArgs e)
    {
        if (Visible)
        {
            if (_painter != null)
                _painter.DrawButton(this, e.Graphics);
            else
                base.OnPaint(e);
        }
    }

    /// <summary>
    /// Raises the <see cref="Control.Resize" /> event and resets the graphics objects for the new size.
    /// </summary>
    /// <param name="e">
    /// An <see cref="EventArgs" /> that contains the event data.
    /// </param>
    protected override void OnResize(EventArgs e)
    {
        Invalidate();
        base.OnResize(e);
        Invalidate();
    }
    #endregion
}