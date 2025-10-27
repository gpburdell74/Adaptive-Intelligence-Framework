namespace Adaptive.Intelligence.Csv.Attributes;

/// <summary>
/// Provides an attribute to describe a CSV column's header text.
/// </summary>
public sealed class HeaderNameAttribute : Attribute
{
    /// <summary>
    /// Initializes an new instance of the <see cref="HeaderNameAttribute"/> class.
    /// </summary>
    /// <param name="headerText">
    /// A string containing the column's header text, if present.
    /// </param>
    public HeaderNameAttribute(string? headerText)
    {
        HeaderName = headerText;
    }

    /// <summary>
    /// Gets or sets the text of the header for the CSV column.
    /// </summary>
    /// <value>
    /// A string containing the text of the header for the CSV column.
    /// </value>
    public string? HeaderName { get; init; }
}
