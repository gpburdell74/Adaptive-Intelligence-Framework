using System.ComponentModel;

namespace Adaptive.Intelligence.Shared.UI.CommonDialogs;

/// <summary>
/// Provides a basic signature definition for the various parameters used when defining
/// and opening a file dialog for selecting file.
/// </summary>
public interface IOpenFileProfile : IFileProfile
{
    /// <summary>
    /// Gets or sets a value indicating whether the dialog box allows multiple files
    /// to be selected.
    /// </summary>
    /// <value>
    ///   <c>true</c> if the dialog box allows multiple files to be selected together or concurrently;
    ///   otherwise, <b>false</b>. The default value is false.
    /// </value>
    [DefaultValue(false)]
    bool Multiselect { get; set; }

    /// <summary>
    ///  Gets or sets a value indicating whether the read-only check box is selected.
    /// </summary>
    /// <value>
    /// <c>true</c> if the read-only check box is selected; otherwise, false. The default value,
    /// is <c>false</c>.
    /// </value>
    [DefaultValue(false)]
    bool ReadOnlyChecked { get; set; }

    /// <summary>
    ///  Gets or sets a value indicating whether the dialog box allows to select read-only files.
    /// </summary>
    /// <value>
    /// <c>true</c> if the dialog box allows selection of read-only files; otherwise, false. The default 
    /// value is <c>true</c>.
    /// </value>
    [DefaultValue(false)]
    bool SelectReadOnly { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the dialog box shows a preview for selected files.
    /// </summary>
    /// <value>
    /// <c>true</c> if the dialog box shows a preview for selected files; otherwise, false.
    /// The default value is <c>false</c>.
    /// </value>
    bool ShowPreview { get; set; }

    /// <summary>
    ///  Gets or sets a value indicating whether the dialog contains a read-only check box.
    /// </summary>
    /// <value>
    /// <c>true</c> if the dialog box contains a read-only check box; otherwise, false. The default value 
    /// is <c>false</c>.
    /// </value>
    [DefaultValue(false)]
    public bool ShowReadOnly { get; set; }


}