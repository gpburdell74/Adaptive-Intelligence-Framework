using Adaptive.Intelligence.Shared.Logging;
using Adaptive.Intelligence.Shared.UI.TemplatedControls;
using Adaptive.Intelligence.Shared.UI.TemplatedControls.Algorithms;
using System.ComponentModel;
using System.Drawing.Design;
using System.Text.Json;
using System.Windows.Forms.Design;

namespace Adaptive.Intelligence.Shared.UI;

/// <summary>
/// Provides an advanced styling button that supports templates for button designs.
/// </summary>
/// <seealso cref="Button" />
[ToolboxItem(typeof(Button)),
  DesignerCategory("Code"),
  RefreshProperties(RefreshProperties.All)]
public class TemplatedButton : Button
{
    #region Private Member Declarations

    /// <summary>
    /// The JSON representation of the template.
    /// </summary>
    private string? _jsonTemplate;

    /// <summary>
    /// The last file name value.
    /// </summary>
    private string? _lastFile;

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
        SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        SetStyle(ControlStyles.UserPaint, true);
        UpdateStyles();

        // Create default objects.
        _template = new ButtonTemplate();
        _painter = new TemplatedButtonDrawingAlgorithm(_template);
        SetImageReferences();
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
        _jsonTemplate = null;

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
                _painter!.State = ControlStates.Checked;
            }
            else
            {
                _painter!.State = ControlStates.Normal;
            }
            Invalidate();
        }
    }

    /// <summary>
    /// Gets or sets the reference to the template to be used.
    /// </summary>
    /// <value>
    /// The reference to the <see cref="ButtonTemplate"/> instance to use when drawing the button, or <b>null</b>."/>
    /// </value>
    [Browsable(false),
     DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ButtonTemplate? Template
    {
        get => _template;
        set
        {
            _template = value;
            if (_template != null)
            {
                _painter = new TemplatedButtonDrawingAlgorithm(_template);
                SetImageReferences();
            }
            Invalidate();
            Refresh();
        }
    }

    /// <summary>
    /// Gets or sets the template data by loading and reading the specified file.
    /// </summary>
    /// <value>
    /// A string containing the path and name of the file to be read, or <b>null</b>.
    /// </value>
    [Browsable(true),
     Category("Appearance"),
     Description("Loads the JSON button template from the specified file."),
     Editor(typeof(FileNameEditor), typeof(UITypeEditor)),
     DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public string? TemplateFromFile
    {
        get
        {
            if (DesignMode)
            {
                return _lastFile;
            }
            else
            {
                return string.Empty;
            }
        }
        set
        {
            if (DesignMode)
            {
                _lastFile = value;
                _template?.Dispose();
                if (!string.IsNullOrEmpty(value) && File.Exists(value))
                {
                    TemplateJson = File.ReadAllText(value);
                }
            }
            Invalidate();
        }
    }

    /// <summary>
    /// Gets or sets the reference to the JSON text defining the button template to use 
    /// when drawing the button.
    /// </summary>
    /// <value>
    /// A string containing the JSON defining the template, or <b>null</b>.
    /// </value>
    [Browsable(true),
     Category("Appearance"),
     Description("Gets or sets the JSON definition for the template to use."),
     DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public string? TemplateJson
    {
        get => _jsonTemplate;
        set
        {
            _painter?.Dispose();
            _template?.Dispose();

            DeserializeTemplate(value);
            if (_template != null)
            {
                _painter = new TemplatedButtonDrawingAlgorithm(_template);
                SetImageReferences();
            }
            Invalidate();
            Refresh();
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
            _painter!.State = ControlStates.Disabled;
        }
        else
        {
            _painter!.State = ControlStates.Normal;
        }
        Invalidate();
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
        if (Enabled && 
            _painter!.State != ControlStates.Hover && 
            _painter.State != ControlStates.Checked)
        {
            _painter!.State = ControlStates.Hover;
        }
        base.OnMouseEnter(e);
        Invalidate();
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
        if (Visible && _painter != null)
        {
            if (Enabled && 
                _painter.State != ControlStates.Normal && 
                _painter.State != ControlStates.Checked)
            {
                _painter.State = ControlStates.Normal;
            }
            base.OnMouseLeave(e);
            Invalidate();
        }
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
        if (_painter != null && Enabled &&
                       _painter.State != ControlStates.Pressed &&
            _painter.State != ControlStates.Checked)
        {
            _painter.State = ControlStates.Pressed;
        }

        base.OnMouseDown(e);
        Invalidate();
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
        if (Visible)
        {
            if (Enabled && _painter!.State != ControlStates.Normal && _painter.State != ControlStates.Checked)
            {
                _painter!.State = ControlStates.Normal;
            }
            base.OnMouseUp(e);
            Invalidate();
        }
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
            if (ImageAlign == ContentAlignment.MiddleLeft && Name == "CopyButton")
            {
                System.Diagnostics.Debug.WriteLine("TemplatedButton: OnPaint: ImageAlign is MiddleCenter");
            }

            {
                // Ensure image references are set correctly.
                SetImageReferences();
            }
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

    #region Private Methods / Functions
    /// <summary>
    /// Sets the image object references for the button.
    /// </summary>
    private void SetImageReferences()
    {
        if (Image != null && _template != null)
        {
            if (_template.Normal != null && _template.Normal.Image == null)
                _template.Normal.Image = Image;

            if (_template.Disabled != null && _template.Disabled.Image == null)
                _template.Disabled.Image = Image;

            if (_template.Hover != null && _template.Hover.Image == null)
                _template.Hover.Image = Image;

            if (_template.Pressed != null && _template.Pressed.Image == null)
                _template.Pressed.Image = Image;

            if (_template.Checked != null && _template.Checked.Image == null)
                _template.Checked.Image = Image;
        }
    }

    /// <summary>
    /// Deserializes the text data into a button template instance.
    /// </summary>
    /// <param name="jsonTemplate">
    /// A string containing the JSON text defining the template.
    /// </param>
    /// <returns>
    /// The new <see cref="ButtonTemplate"/> instance, or <b>null</b>.
    /// </returns>
    private ButtonTemplate? DeserializeTemplate(string? jsonTemplate)
    {
        _jsonTemplate = jsonTemplate;
        if (string.IsNullOrEmpty(_jsonTemplate))
        {
            _template = new ButtonTemplate();
        }
        else
        {
            try
            {
                _template = JsonSerializer.Deserialize<ButtonTemplate>(_jsonTemplate);
            }
            catch (JsonException ex)
            {
                ExceptionLog.LogException(ex);
                _template = new ButtonTemplate();
            }
        }
        return _template;
    }
    #endregion
}