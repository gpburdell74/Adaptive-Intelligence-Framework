using System.Text.Json;
using System.Text.Json.Serialization;

namespace Adaptive.Intelligence.Shared.UI.TemplatedControls.Templates.IO;

/// <summary>
/// Converts data to and from a .NET <see cref="Image"/> class to and a usable JSON format.
/// </summary>
/// <seealso cref="JsonConverter{T}" />
/// <seealso cref="Image"/>
public sealed class ImageJsonConverter: JsonConverter<Image?>
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
    public override Image? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Read the JSON value as a string (expected to be in HEX format, e.g., "#AARRGGBB")
        string? rawText = reader.GetString();
        return JsonConversions.ImageFromJson(rawText);
    }

    /// <summary>
    /// Writes a specified <see cref="Color"/> structure as JSON text (specific to this library).
    /// </summary>
    /// <param name="writer">
    /// The <see cref="Utf8JsonWriter"/> instance used to write the JSON text.
    /// </param>
    /// <param name="value">
    /// The <see cref="Image"/> value to convert to JSON text, or <b>null</b> if no image is specified.
    /// </param>
    /// <param name="options">
    /// An object that specifies the serialization options to use.
    /// </param>
    public override void Write(Utf8JsonWriter writer, Image? value, JsonSerializerOptions options)
    {
        string jsonText = JsonConversions.ImageToJson(value);
        writer.WriteStringValue(jsonText);
        writer.Flush();
    }
}

