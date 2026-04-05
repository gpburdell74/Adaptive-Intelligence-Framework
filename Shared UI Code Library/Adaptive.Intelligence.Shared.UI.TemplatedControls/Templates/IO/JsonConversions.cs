namespace Adaptive.Intelligence.Shared.UI.TemplatedControls.Templates.IO;

/// <summary>
/// Provides static methods / functions for converting data types to and from JSON text.
/// </summary>
public static class JsonConversions
{
    /// <summary>
    /// Safely converts a base64 string to a byte array. If the input string is null, empty, whitespace, 
    /// or the literal "null", this method returns null. If the input string is not a valid base64 string, 
    /// this method also returns null without throwing an exception.
    /// </summary>
    /// <param name="base64String">
    /// A string containing the base-64 encoded string to convert.
    /// </param>
    /// <returns>
    /// An array of bytes created from the provide string if successful; otherwise,
    /// returns <b>null</b>.
    /// </returns>
    public static byte[]? Base64StringToBytes(string? base64String)
    {
        byte[]? result = null;
        if (!string.IsNullOrWhiteSpace(base64String) && base64String != "null")
        {
            try
            {
                result = Convert.FromBase64String(base64String);
            }
            catch
            {
            }
        }
        return result;
    }

    /// <summary>
    /// Converts the byte array to a base64 string. 
    /// If the input byte array is null or empty, this method returns null.
    /// </summary>
    /// <param name="data">
    /// A byte array containing the data to be convrted.
    /// </param>
    /// <returns>
    /// A string containing the base-64 encoded version of the byte array if successful; otherwise,
    /// return <b>null</b>.
    /// </returns>
    public static string? BytesToBase64String(byte[]? data)
    {
        string? base64 = null;

        if (data != null && data.Length > 0)
        {
            try
            {
                base64 = Convert.ToBase64String(data);
            }
            catch
            {
            }
        }
        return base64;
    }

    /// <summary>
    /// Converts the provided byte array to an image instance.
    /// </summary>
    /// <param name="data">
    /// A byte array containing the image data.
    /// </param>
    /// <returns>
    /// The <see cref="Image"/> instance if successful; otherwise, returns <b>null</b> if the input data 
    /// is null, empty, or if the conversion fails.
    /// </returns>
    public static Image? BytesToImage(byte[]? data)
    {
        Image? image = null;
        if (data != null && data.Length > 0)
        {
            MemoryStream stream = new MemoryStream(data);
            try
            {
                image = Image.FromStream(stream);
            }
            catch
            {
                stream.Close();
                stream.Dispose();
            }
        }
        return image;
    }

    /// <summary>
    /// Converts the JSON text to an image instance.
    /// </summary>
    /// <param name="jsonText">
    /// A base-64 encoded string containing the image data, or "null" if no image is specified.
    /// </param>
    /// <returns>
    /// The <see cref="Image"/> instance if successful; otherwise, returns <b>null</b> if the input data 
    /// is null, empty, or if the conversion fails.
    /// </returns>
    public static Image? ImageFromJson(string? jsonText)
    {
        Image? image = null;
        if (!string.IsNullOrWhiteSpace(jsonText) && jsonText != "null")
        {
            byte[]? data = Base64StringToBytes(jsonText);
            if (data != null)
            {
                image = BytesToImage(data);
                Array.Clear(data, 0, data.Length);
            }
        }
        return image;
    }

    /// <summary>
    /// Converts a <see cref="Image"/> instance to a byte array.
    /// </summary>
    /// <param name="image">
    /// The <see cref="Image"/> instance to be converted.
    /// </param>
    /// <returns>
    /// A byte array containing the image data in PNG format if the conversion is successful; 
    /// otherwise, returns <b>null</b>.
    /// </returns>
    public static byte[]? ImageToBytes(Image? image)
    {
        byte[]? data = null;

        MemoryStream stream = new MemoryStream(1000);
        try
        {
            image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            data = stream.ToArray();
        }
        catch
        {
        }
        stream.Close();
        stream.Dispose();

        return data;
    }

    /// <summary>
    /// Converts a <see cref="Image"/> instance to string for JSON serialization.
    /// </summary>
    /// <param name="image">
    /// The <see cref="Image"/> instance to be converted.
    /// </param>
    /// <returns>
    /// A string containing hte base-64 encoding of the image data, or "null" if the input 
    /// image is null or if the conversion fails.
    /// </returns>
    public static string ImageToJson(Image? image)
    {
        string base64 = "null";

        if (image != null)
        {
            byte[]? imageData = ImageToBytes(image);
            if (imageData != null && imageData.Length > 0)
            {
                string? result = BytesToBase64String(imageData);
                if (result != null)
                {
                    base64 = result;
                }
                Array.Clear(imageData, 0, imageData.Length);

            }
        }
        return base64;
    }
}
