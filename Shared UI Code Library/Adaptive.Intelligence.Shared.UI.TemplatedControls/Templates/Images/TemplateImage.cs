using Adaptive.Intelligence.Shared.Logging;

namespace Adaptive.Intelligence.Shared.UI.TemplatedControls;

/// <summary>
/// Provides static methods and functions for converting images from one format to another.
/// </summary>
public static class TemplateImage
{
    /// <summary>
    /// Translates the specified <see cref="Image"/> object into a base-64 encoded string.
    /// </summary>
    /// <param name="source">
    /// The <see cref="Image"/> instance containing the source image.
    /// </param>
    /// <returns>
    /// A base-64 encoded string that is a copy of <paramref name="source"/>, or <b>null</b>.
    /// </returns>
    public static string? FromImage(Image? source)
    {
        string? encodedImage = null;

        if (source != null)
        {
            // Save the current object to a memory stream.
            MemoryStream containerStream = new MemoryStream(1000);
            try
            {
                source.Save(containerStream, source.RawFormat);
                containerStream.Flush();
                containerStream.Seek(0, SeekOrigin.Begin);
            }
            catch (Exception ex)
            {
                ExceptionLog.LogException(ex);
            }

            // If successful, copy the data to the new object, and dispose of the stream.
            if (containerStream != null)
            {
                try
                {
                    byte[] imageData = containerStream.ToArray();
                    encodedImage = Convert.ToBase64String(imageData);
                }
                catch (Exception ex)
                {
                    ExceptionLog.LogException(ex);
                }
                containerStream.Dispose();
            }
        }
        return encodedImage;
    }

    /// <summary>
    /// Translates the specified base-64 encoded string into an <see cref="Image"/> object.
    /// </summary>
    /// <param name="encodedImage">
    /// A string containing The base-64 encoded image.</param>
    /// <returns>
    /// The <see cref="Image"/> instance represented by the encoded string, or <b>null</b>.
    /// </returns>
    public static Image? ToImage(string? encodedImage)
    {
        Image? newImage = null;

        if (!string.IsNullOrEmpty(encodedImage))
        {
            try
            {
                byte[] imageData = Convert.FromBase64String(encodedImage);
                MemoryStream containerStream = new MemoryStream(imageData);
                newImage = (Image)System.Drawing.Image.FromStream(containerStream);
                containerStream.Dispose();
            }
            catch (Exception ex)
            {
                ExceptionLog.LogException(ex);
            }
        }
        return newImage;
    }
}
