using System.ComponentModel;
using System.Drawing.Design;

namespace Adaptive.Intelligence.Shared.UI.TemplatedControls;

public class FontTemplateTypeEditor : UITypeEditor
{
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext? context)
    {
        return UITypeEditorEditStyle.Modal;
    }


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
