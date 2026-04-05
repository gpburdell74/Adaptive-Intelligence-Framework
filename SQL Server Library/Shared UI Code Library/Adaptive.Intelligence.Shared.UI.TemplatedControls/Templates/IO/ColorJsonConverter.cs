using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Adaptive.Intelligence.Shared.UI.TemplatedControls.Templates.IO;

public class ColorJsonConverter : JsonConverter<Color>
{
    public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? rawText = reader.GetString();

        if (string.IsNullOrWhiteSpace(rawText))
            return Color.Empty;

        try
        {
            return ColorTranslator.FromHtml(rawText);
        }
        catch
        {
            throw new JsonException($"Invalid color value: {rawText}");
        }
    }

    public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
    {
        // Convert to HEX format (#RRGGBB)
        string hex = ColorTranslator.ToHtml(value);
        writer.WriteStringValue(hex);
    }
}

