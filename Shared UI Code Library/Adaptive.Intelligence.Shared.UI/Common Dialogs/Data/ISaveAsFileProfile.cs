using System.ComponentModel;

namespace Adaptive.Intelligence.Shared.UI.CommonDialogs;

/// <summary>
/// Provides a basic signature definition for the various parameters used when defining
/// and opening a file dialog for saving a file under a new name.
/// </summary>
public interface ISaveAsFileProfile : IFileProfile
{
    /// <summary>
    /// Gets or sets a value indicating whether to check the file write permission before saving a file.
    /// </summary>
    /// <value>
    ///   <c>true</c> if to check for write access; otherwise, <c>false</c>.
    /// </value>
    bool CheckWriteAccess { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the Save As dialog box displays a warning if the 
    /// user specifies a file name that already exists.
    /// </summary>
    /// <value>
    /// <c>true</c> to prompt the user for permission to overwrite if the user specifies a file name that
    /// already exists; otherwise, <c>false</c>.
    /// </value>
    [DefaultValue(true)]
    bool OverwritePrompt { get; set; }
}