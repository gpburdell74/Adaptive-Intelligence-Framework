using Adaptive.Intelligence.Shared.Logging;
using Microsoft.Win32;

namespace Adaptive.Intelligence.Shared.IO
{
    /// <summary>
    /// Provides static methods and functions for getting information about a file.
    /// </summary>
    public static class FileShellInfo
    {
        #region Public Methods / Functions
        /// <summary>
        /// Gets the file type description.
        /// </summary>
        /// <param name="fileName">
        /// A string containing the name of the file.
        /// </param>
        /// <returns>
        /// A string containing the file type description, if successful; otherwise,
        /// returns <b>null</b>.
        /// </returns>
        public static string? GetFileTypeDescription(string fileName)
        {
            // Extract the file extension.
            string fileExtension = Path.GetExtension(fileName);

            // Read the sub-key value for the file extension.
            string? keyValue = ReadSubKeyForFileExtension(fileExtension);

            // Open and read the value of the specified sub-key.
            string? description = null;
            if (!string.IsNullOrEmpty(keyValue))
            {
                try
                {
                    RegistryKey? registryKey = OpenSubKey(keyValue);
                    if (registryKey != null)
                    {
                        description = ReadRegistryKeyValue(registryKey);
                        registryKey.Close();
                        registryKey.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    ExceptionLog.LogException(ex);
                }
            }

            // Set the default value if the operation did not succeed or the value is not there.
            if (string.IsNullOrEmpty(description))
                description = Constants.FileDescGeneric;

            return description;
        }
        /// <summary>
        /// Provides a function for formatting the size of a file in bytes.
        /// </summary>
        /// <param name="originalSize">
        /// A <see cref="long"/> value indicating the file size, in bytes.</param>
        /// <returns>
        /// A string containing the formatted result.
        /// </returns>
        public static string FormatFileSize(long originalSize)
        {
            string result;
            if (originalSize < 1024L)
            {
                result = originalSize.ToString() + " bytes";
            }
            else if (originalSize < 1048576L)
            {
                result = (originalSize / 1024f).ToString("###,###,###,###,##0.0##") + " KB";
            }
            else
            {
                result = (originalSize / 1048576f).ToString("###,###,###,###,##0.0##") + " MB";
            }

            return result;
        }
        /// <summary>
        /// Gets the related application.
        /// </summary>
        /// <param name="fileName">
        /// A string containing the the name of the file.
        /// </param>
        /// <returns>
        /// A string containing the name of the application that is used with the file.
        /// </returns>
        public static string? GetRelatedApplication(string? fileName)
        {
            string? appName = string.Empty;

            // Extract the file extension.
            string? fileExtension = Path.GetExtension(fileName);

            // Read the sub-key value for the file extension.
            if (fileExtension != null)
            {
                string? subKey = ReadSubKeyForFileExtension(fileExtension);

                // Read the sub key to find the application.
                if (!string.IsNullOrEmpty(subKey))
                {
                    // Try the edit sub key.
                    RegistryKey? registryKey = OpenSubKey(subKey + Constants.RegSubKeyNameEdit);
                    if (registryKey != null)
                    {
                        // Read the value.
                        appName = ReadRegistryKeyValue(registryKey);
                        registryKey.Close();
                        registryKey.Dispose();
                    }
                    else
                    {
                        // If that did not work, try the open sub key.
                        RegistryKey? subRegistryKey = OpenSubKey(subKey + Constants.RegSubKeyNameOpen);
                        if (subRegistryKey != null)
                        {
                            // Read the value.
                            appName = ReadRegistryKeyValue(subRegistryKey);
                            subRegistryKey.Close();
                            subRegistryKey.Dispose();
                        }
                    }
                }

                // Remove the extraneous items from the application name.
                if (!string.IsNullOrEmpty(appName))
                    appName = CleanUpFileName(appName);
            }
            return appName;
        }
        /// <summary>
        /// Gets the location of the icon to use for the file.
        /// </summary>
        /// <param name="fileName">
        /// A string containing the the name of the file.
        /// </param>
        /// <returns>
        /// A string containing the location of the file that specifies the icon used to
        /// represent the file.
        /// </returns>
        public static string? GetRelatedIconStore(string fileName)
        {
            // Extract the file extension.
            string fileExtension = Path.GetExtension(fileName);

            // Read the sub-key value for the file extension.
            string? subKey = ReadSubKeyForFileExtension(fileExtension);
            string? iconProviderLocation = string.Empty;

            if (!string.IsNullOrEmpty(subKey))
            {
                RegistryKey? registryKey = OpenSubKey(subKey + Constants.RegSubKeyNameDefaultIcon);
                if (registryKey != null)
                {
                    iconProviderLocation = (string)registryKey.GetValue(string.Empty, string.Empty);
                    registryKey.Close();
                    registryKey.Dispose();
                }
            }

            if (!string.IsNullOrEmpty(iconProviderLocation))
                iconProviderLocation = CleanUpFileName(iconProviderLocation);

            return iconProviderLocation;
        }
        ///// <summary>
        ///// Gets the related icon for the specified file.
        ///// </summary>
        ///// <param name="fileName">
        ///// A string containing the the name of the file.
        ///// </param>
        ///// <param name="large">
        ///// <b>true</b> to retrieve the large icon for the file, <b>false</b> to retrieve the small icon.
        ///// </param>
        ///// <returns>
        ///// An <see cref="Icon"/> instance, of available, otherwise, returns <b>null</b>.
        ///// </returns>
        //public static byte[]? GetRelatedIcon(string fileName, bool large)
        //{
        //	byte[]? iconData = null;

        //	// Get the icon provider location.
        //	string? iconProviderFile = GetRelatedIconStore(fileName);
        //	if (!string.IsNullOrEmpty(iconProviderFile))
        //	{
        //		// Remove extra commas from the file name.
        //		int index = iconProviderFile.IndexOf(Constants.Comma, StringComparison.InvariantCultureIgnoreCase);
        //		if (index > -1)
        //		{
        //			index = SafeConverter.ToInt32(iconProviderFile.Substring(index + 1, iconProviderFile.Length - (index + 1)));
        //			iconProviderFile = iconProviderFile.Substring(0, index);
        //		}

        //		if (!string.IsNullOrEmpty(iconProviderFile))
        //		{
        //			iconProviderFile = GetRelatedApplication(fileName);
        //		}

        //		// Set the flags for icon retrieval.
        //		IO.ShellFileInfoDto dto = ShellFileInfoProvider.GetShellInformation(fileName);
        //		int len = dto.IconImage.Length;

        //		iconData = new byte[len];
        //		Array.Copy(dto.IconImage, 0, iconData, 0, len);

        //	}

        //	return iconData;
        //}

        #endregion

        #region Private Static Methods / Functions
        /// <summary>
        /// Reads the registry sub key for the specified file extension.
        /// </summary>
        /// <param name="fileExtension">
        /// A string containing the file extension.
        /// </param>
        /// <returns>
        /// A string containing the value of the registry sub-key, if present.
        /// </returns>
        private static string? ReadSubKeyForFileExtension(string fileExtension)
        {
            string? keyValue = null;
            // Find the correct registry key value to read.
            RegistryKey? registryKey = OpenSubKey(fileExtension);
            if (registryKey != null)
            {
                keyValue = ReadRegistryKeyValue(registryKey);
                registryKey.Close();
                registryKey.Dispose();
            }

            return keyValue;
        }
        /// <summary>
        /// Opens the registry sub key.
        /// </summary>
        /// <param name="pathToOpen">
        /// A string specifying the path to open.
        /// </param>
        /// <returns>
        /// The opened <see cref="RegistryKey"/> instance if successful;
        /// otherwise, returns <b>null</b>.
        /// </returns>
        private static RegistryKey? OpenSubKey(string pathToOpen)
        {
            RegistryKey? registryKey = null;
            try
            {
                registryKey = Registry.ClassesRoot.OpenSubKey(pathToOpen);
            }
            catch (Exception ex)
            {
                ExceptionLog.LogException(ex);
            }

            return registryKey;
        }
        /// <summary>
        /// Reads the default value from the provided registry key.
        /// </summary>
        /// <param name="registryKey">
        /// The <see cref="RegistryKey"/> instance to read the default value from.
        /// </param>
        /// <returns>
        /// A string containing the default value, or <b>null</b> if the operation fails.
        /// </returns>
        private static string? ReadRegistryKeyValue(RegistryKey? registryKey)
        {
            string? keyValue = null;

            if (registryKey != null)
            {
                try
                {
                    keyValue = (string)registryKey.GetValue(string.Empty, string.Empty);
                }
                catch (Exception ex)
                {
                    ExceptionLog.LogException(ex);
                }
            }

            return keyValue;
        }
        /// <summary>
        /// Cleans up the file name.
        /// </summary>
        /// <param name="fileName">
        /// A string containing the raw file name data.
        /// </param>
        /// <returns>
        /// The file name with extra characters removed.
        /// </returns>
        private static string? CleanUpFileName(string? fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                int index = fileName.IndexOf(Constants.Backslash, StringComparison.InvariantCultureIgnoreCase);
                if (index > -1)
                {
                    index = fileName.IndexOf(Constants.Backslash, index + 1, StringComparison.InvariantCultureIgnoreCase);
                    fileName = fileName.Substring(0, index + 1);
                }

                do
                {
                    index = fileName.LastIndexOf(Constants.PercentSign, StringComparison.InvariantCultureIgnoreCase);
                    if (index > -1)
                    {
                        fileName = fileName.Substring(0, index);
                    }
                } while (index > -1);

                fileName = fileName.Replace(Constants.Backslash, string.Empty);
            }

            return fileName;
        }
        #endregion
    }
}
