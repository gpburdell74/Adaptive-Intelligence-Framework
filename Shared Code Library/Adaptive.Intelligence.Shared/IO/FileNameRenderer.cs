namespace Adaptive.Intelligence.Shared.IO;

/// <summary>
/// Provides static methods and functions for rendering file and path names with
/// regard to the underlyiong operating system.
/// </summary>
public static class FileNameRenderer
{
    #region Private Static Members

    private static bool _windows;
    private static bool _linuxOrMac;
    
    #endregion

    static FileNameRenderer()
    {
       _windows = System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(
            System.Runtime.InteropServices.OSPlatform.Windows);

       _linuxOrMac = (
           (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(
               System.Runtime.InteropServices.OSPlatform.Linux)) ||
           (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(
               System.Runtime.InteropServices.OSPlatform.OSX)));
    }

    /// <summary>
    /// Rewrites the path and file name to the appropriate OS format.
    /// </summary>
    /// <param name="path">
    /// A string containing the fully-qualified path to modify.
    /// </param>
    /// <param name="fileName">
    /// A string containing the file name to modify,
    /// </param>
    /// <returns>
    /// A string containing the fully-qualified path and file name in the
    /// proper format for the current operating system.
    /// </returns>
    public static string? RenderFileName(string path, string fileName)
    {
        if (_linuxOrMac)
        {
            path = path.Replace('\\', '/');
            fileName = fileName.Replace('\\', '/'); 
        }

        return Path.Combine(path, fileName);
        
    }
    
    /// <summary>
    /// Rewrites the path and file name to the appropriate OS format using the
    /// default path of the local user.
    /// </summary>
    /// <param name="fileName">
    /// A string containing the file name to modify,
    /// </param>
    /// <returns>
    /// A string containing the fully-qualified path and file name in the
    /// proper format for the current operating system.
    /// </returns>
    public static string RenderFileNameInUserPath(string fileName)
    {
       string path = System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
       return RenderFileName(path, fileName);

    }

    public static string RenderInTempPath(string path)
    {
        if (_windows)
            return Path.Combine(Path.GetTempPath(),
                path);
        else
        {
            return Path.Combine(Path.GetTempPath(),
                path);
        }
    }
}