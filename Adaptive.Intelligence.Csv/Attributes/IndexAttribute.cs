namespace Adaptive.Intelligence.Csv.Attributes;

/// <summary>
/// Provides an attribute to the ordinal index of a column in a CSV file.
/// </summary>
public sealed class IndexAttribute : Attribute
{
    /// <summary>
    /// Initializes an new instance of the <see cref="IndexAttribute"/> class.
    /// </summary>
    /// <param name="index">
    /// A integer specifying the ordinal index of the column in the CSV file.
    /// </param>
    public IndexAttribute(int index)
    {
        Index = index;
    }

    /// <summary>
    /// Gets or sets the ordinal index of the decorated column in the CSV file.
    /// </summary>
    /// <value>
    /// An integer specifying the ordinal index of the column.
    /// </value>
    public int Index { get; init; }
}
