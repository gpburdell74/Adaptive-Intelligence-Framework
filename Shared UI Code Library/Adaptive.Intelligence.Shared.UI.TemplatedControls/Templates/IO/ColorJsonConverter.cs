using System.Text.Json;
using System.Text.Json.Serialization;

namespace Adaptive.Intelligence.Shared.UI.TemplatedControls.Templates.IO;

/// <summary>
/// Converts data to and from a .NET <see cref="Color"/> structure to and a usable JSON format.
/// </summary>
/// <seealso cref="JsonConverter{T}" />
/// <seealso cref="Color"/>
public sealed class ColorJsonConverter : JsonConverter<Color>
{
    /// <summary>
    /// Reads and converts the JSON to a <see cref="Color"/> structure.
    /// </summary>
    /// <param name="reader">
    /// The <see cref="Utf8JsonReader"/> reader instance used to read the JSON content.
    /// </param>
    /// <param name="typeToConvert">
    /// The data type to convert.
    /// </param>
    /// <param name="options">
    /// An object that specifies serialization options to use.
    /// </param>
    /// <returns>
    /// A <see cref="Color"/> structure created from the JSON data, if the data is valid.
    /// </returns>
    public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        Color color = Color.Empty;

        // Read the JSON value as a string (expected to be in HEX format, e.g., "#AARRGGBB")
        string? rawText = reader.GetString();

        if (rawText.StartsWith("#"))
        {
            rawText = rawText[1..]; // Remove the '#' character if present.
        }
        else
        {
            try
            {
                color = Color.FromName(rawText);
            }
            catch
            {
                color = Color.Empty;
            }
        }

        if (color == Color.Empty && !string.IsNullOrWhiteSpace(rawText))
        {
            if (rawText.Length == 6)
            {
                byte[] rgb = Convert.FromHexString(rawText);
                color = Color.FromArgb(255, rgb[0], rgb[1], rgb[2]);
            }
            else if (rawText.Length == 8)
            {
                byte[] rgb = Convert.FromHexString(rawText);
                color = Color.FromArgb(rgb[0], rgb[1], rgb[2], rgb[3]);
            }
        }
        
        return color;
    }

    /// <summary>
    /// Writes a specified <see cref="Color"/> structure as JSON text (specific to this library).
    /// </summary>
    /// <param name="writer">
    /// The <see cref="Utf8JsonWriter"/> instance used to write the JSON text.
    /// </param>
    /// <param name="value">
    /// The <see cref="Color"/> value to convert to JSON text.
    /// </param>
    /// <param name="options">
    /// An object that specifies the serialization options to use.
    /// </param>
    public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
    {
        // Convert the color to HEX format (#AARRGGBB)
        string hex =
            value.A.ToString("X2") +
            value.R.ToString("X2") +
            value.G.ToString("X2") +
            value.B.ToString("X2");

        writer.WriteStringValue("#"+hex);
    }
}

