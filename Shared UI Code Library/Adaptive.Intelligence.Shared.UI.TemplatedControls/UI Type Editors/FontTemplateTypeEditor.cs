using System.ComponentModel;
using System.Drawing.Design;

namespace Adaptive.Intelligence.Shared.UI.TemplatedControls;

/// <summary>
/// Provides a UI type editor for editing the font template on a control template file.
/// </summary>
public class FontTemplateTypeEditor : UITypeEditor
{
    /// <summary>
    /// Gets the edit style for the font template editor, which is a modal dialog.
    /// </summary>
    /// <param name="context">
    /// The context in which the editor is invoked.
    /// </param>
    /// <returns>
    /// A <see cref="UITypeEditorEditStyle"/> enumerated value indicating the type of UI editor
    /// the current instance requires and/or will implement.
    /// </returns>
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext? context)
    {
        return UITypeEditorEditStyle.Modal;
    }

    /// <summary>
    /// Edits the value of the specified object using the editor style provided by <see cref="GetEditStyle"/>.
    /// </summary>
    /// <param name="context">
    /// The context in which the editor is invoked.
    /// </param>
    /// <param name="provider">
    /// The service provider that can be used to obtain additional services.
    /// </param>
    /// <param name="value">
    /// The object to edit. This is expected to be of type <see cref="FontTemplate"/> for this editor.
    /// </param>
    /// <returns>
    /// The edited object, which is of type <see cref="FontTemplate"/>.
    /// </returns>
    public override object? EditValue(ITypeDescriptorContext? context, IServiceProvider provider, object? value)
    {
        FontTemplate? fontTemplate = value as FontTemplate;
        FontTemplate? newValue = fontTemplate;

        FontDialog dialog = new FontDialog();
        if (fontTemplate != null)
        {
            dialog.ShowEffects = true;
            dialog.Font = fontTemplate.ToFont();
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                Font selectedFont = dialog.Font;
                newValue = new FontTemplate
                {
                    FontFamily = selectedFont.FontFamily.Name,
                    Size = selectedFont.Size,
                    Style = selectedFont.Style,
                    Unit = selectedFont.Unit,
                    GdiCharSet = fontTemplate.GdiCharSet,
                    GdiVerticalFont = fontTemplate.GdiVerticalFont
                };
            }
        }
        return newValue;
    }
}
