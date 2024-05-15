using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.Css;

/// <summary>
/// Contains a <see cref="int"/> value with am optional unit specification.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
public sealed class IntWithUnit : DisposableObjectBase
{
    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="IntWithUnit"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public IntWithUnit() : this(-1, CssUnit.NotSpecified)
    {
        Unit = CssUnit.NotSpecified;
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="IntWithUnit"/> class.
    /// </summary>
    /// <param name="value">
    /// A <see cref="int"/> specifying the value.
    /// </param>
    public IntWithUnit(int value) : this(value, CssUnit.NotSpecified)
    {
        Value = value;
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="IntWithUnit"/> class.
    /// </summary>
    /// <param name="value">
    /// A <see cref="int"/> specifying the value.
    /// </param>
    /// <param name="unit">
    /// A <see cref="CssUnit"/> enumerated value indicating the unit of measurement.
    /// </param>
    public IntWithUnit(int value, CssUnit? unit)
    {
        Value = value;
        Unit = unit;
    }
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        Unit = null;
        base.Dispose(disposing);
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets or sets the unit to use with the value.
    /// </summary>
    /// <value>
    /// A <see cref="CssUnit"/> enumerated value, or <b>null</b> if not set.
    /// </value>
    public CssUnit? Unit { get; set; }
    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    /// <value>
    /// A <see cref="int"/> specifying the value.
    /// </value>
    public int Value { get; set; }
    #endregion

    #region Public Static Methods / Functions
    /// <summary>
    /// Parses the specified original text into a value and unit representation/
    /// </summary>
    /// <param name="originalText">
    /// A string containing the original text to be parsed.
    /// </param>
    /// <returns>
    /// A <see cref="IntWithUnit"/> instance with values from the original data, or 
    /// <b>null</b> if the string could not be parsed.
    /// </returns>
    public static IntWithUnit? Parse(string? originalText)
    {
        IntWithUnit? unit = null;

        if (!string.IsNullOrEmpty(originalText))
        {
            unit = new IntWithUnit();

            // Find where the unit specification starts.  To the left of the unit is the number;
            // split and parse the values.
            int unitStart = originalText.FindFirstNonNumericCharacter(false);
            if (unitStart == -1)
            {
                if (int.TryParse(originalText, out int newValue))
                    unit.Value = newValue;
            }
            else
            {
                if (int.TryParse(originalText, out int newValue))
                    unit.Value = newValue;

                string unitName = originalText.Substring(unitStart, originalText.Length - (unitStart + 1));
                unit.Unit = CssUnitConverter.FromText(unitName);
            }
        }
        return unit;
    }
    #endregion
}
