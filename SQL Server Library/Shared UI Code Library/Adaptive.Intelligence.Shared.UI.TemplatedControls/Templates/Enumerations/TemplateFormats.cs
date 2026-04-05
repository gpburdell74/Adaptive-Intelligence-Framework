namespace Adaptive.Intelligence.Shared.UI.TemplatedControls;

/// <summary>
/// Lists the types of control template data formats that are currently supported.
/// </summary>
public enum TemplateFormats
{
    /// <summary>
    /// Indicates the template data file format is either not specified or unknown.
    /// </summary>
    NoneOrNotSpecified = 0,
    /// <summary>
    /// Indicates the old binary template format used in version 0.92 and earlier.
    /// </summary>
    OldVersion92Format = 1,
    /// <summary>
    /// Indicates the template is in "pretty-printed" JSON text format.
    /// </summary>
    JsonTextFormat = 2,
    /// <summary>
    /// Indicates the template is in compressed JSON text format.
    /// </summary>
    JsonTextMinifiedFormat = 3,
    /// <summary>
    /// Indicates the template is in a binary serialized format.
    /// </summary>
    BinaryFormat = 3,
    /// <summary>
    /// Indicates another data format not listed.
    /// </summary>
    OtherFormat = 99
}
