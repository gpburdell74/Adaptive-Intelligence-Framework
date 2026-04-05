using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Resources;
using System.Windows.Forms.Design;

namespace Adaptive.Intelligence.Shared.UI.Controls;

/// <summary>
/// Provides a UI type editor for selecting binary resources from the project's resources.
/// </summary>
/// <seealso cref="UITypeEditor" />
public class ResourceImageUITypeEditor : UITypeEditor
{
    /// <summary>
    /// Gets the editor style used by the <see cref="M:System.Drawing.Design.UITypeEditor.EditValue(System.IServiceProvider,System.Object)" /> method.
    /// </summary>
    /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information.</param>
    /// <returns>
    /// A <see cref="T:System.Drawing.Design.UITypeEditorEditStyle" /> value that indicates the style of editor used by the <see cref="M:System.Drawing.Design.UITypeEditor.EditValue(System.IServiceProvider,System.Object)" /> method. If the <see cref="T:System.Drawing.Design.UITypeEditor" /> does not support this method, then <see cref="M:System.Drawing.Design.UITypeEditor.GetEditStyle" /> will return <see cref="F:System.Drawing.Design.UITypeEditorEditStyle.None" />.
    /// </returns>
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext? context)
    {
        // We want a dropdown list
        return UITypeEditorEditStyle.DropDown;
    }

    /// <summary>
    /// Edits the specified object's value using the editor style indicated by the <see cref="M:System.Drawing.Design.UITypeEditor.GetEditStyle" /> method.
    /// </summary>
    /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information.</param>
    /// <param name="provider">An <see cref="T:System.IServiceProvider" /> that this editor can use to obtain services.</param>
    /// <param name="value">The object to edit.</param>
    /// <returns>
    /// The new value of the object. If the value of the object has not changed, this should return the same object it was passed.
    /// </returns>
    public override object? EditValue(ITypeDescriptorContext? context, IServiceProvider provider, object? value)
    {
        if (provider.GetService(typeof(IWindowsFormsEditorService)) is not IWindowsFormsEditorService edSvc)
            return value;

        // Get the template file data...
        Dictionary<string, byte[]> templates = LoadResourceItems();

        // Create a ListBox to show resource names
        var listBox = new ListBox
        {
            BorderStyle = BorderStyle.None
        };

        foreach (var key in templates.Keys)
            listBox.Items.Add(key);

        // Handle selection
        listBox.Click += (s, e) =>
        {
            if (listBox.SelectedItem != null)
            {
                string? selectedKey = listBox.SelectedItem?.ToString();
                if (selectedKey != null)
                {
                    value = templates[selectedKey];
                    edSvc.CloseDropDown();
                }
            }
        };

        // Show dropdown
        edSvc.DropDownControl(listBox);

        return value;
    }

    private Dictionary<string, byte[]> LoadResourceItems()
    {
        Dictionary<string, byte[]> returnData = new Dictionary<string, byte[]>();

        // Get all image resources from Properties.Resources
        ResourceManager resourceManager = Properties.Resources.ResourceManager;

        ResourceSet? resourceSet = resourceManager.GetResourceSet(
            System.Globalization.CultureInfo.CurrentUICulture, false, true);

        if (resourceSet != null)
        {
            IDictionaryEnumerator enumerator = resourceSet.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Value is byte[])
                {
                    string? key = enumerator.Key.ToString();
                    if (key != null && enumerator.Value != null)
                    {
                        returnData.Add(key, (byte[])enumerator.Value);
                    }
                }
            }
        }
        return returnData;
    }
}
