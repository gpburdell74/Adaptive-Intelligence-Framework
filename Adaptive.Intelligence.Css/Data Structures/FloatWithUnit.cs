using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.Css
{
    /// <summary>
    /// Contains a <see cref="float"/> value with am optional unit specification.
    /// </summary>
    /// <seealso cref="DisposableObjectBase" />
    public sealed class FloatWithUnit : DisposableObjectBase
    {
        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="FloatWithUnit"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public FloatWithUnit() : this(null, CssUnit.NotSpecified)
        {
            Unit = CssUnit.NotSpecified;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="FloatWithUnit"/> class.
        /// </summary>
        /// <param name="value">
        /// A <see cref="float"/> specifying the value.
        /// </param>
        public FloatWithUnit(float value) : this(value, CssUnit.NotSpecified)
        {
            Value = value;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="FloatWithUnit"/> class.
        /// </summary>
        /// <param name="value">
        /// A <see cref="float"/> specifying the value.
        /// </param>
        /// <param name="unit">
        /// A <see cref="CssUnit"/> enumerated value indicating the unit of measurement.
        /// </param>
        public FloatWithUnit(float? value, CssUnit? unit)
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
        /// A <see cref="float"/> specifying the value.
        /// </value>
        public float? Value { get; set; }
        #endregion

        #region Public Methods / Functions		
        /// <summary>
        /// Parses the provided string and populates the current instance.
        /// </summary>
        /// <param name="source">
        /// A string containing the data to be parsed.
        /// </param>
        public void FromString(string? source)
        {
            FloatWithUnit? item = Parse(source);
            if (item != null)
            {
                Value = item.Value;
                Unit = item.Unit;
                item.Dispose();
            }
        }
        /// <summary>
        /// Converts the current instance to a string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            if (Unit == null || Unit == CssUnit.NotSpecified)
            {
                if (Value == null)
                    return null;
                else
                    return Value.ToString();
            }
            else
            {
                return Value.ToString() + CssUnitConverter.ToText(Unit.Value);
            }
        }
        #endregion

        #region Public Static Methods / Functions		
        /// <summary>
        /// Parses the specified original text into a value and unit representation/
        /// </summary>
        /// <param name="originalText">
        /// A string containing the original text to be parsed.
        /// </param>
        /// <returns>
        /// A <see cref="FloatWithUnit"/> instance with values from the original data, or 
        /// <b>null</b> if the string could not be parsed.
        /// </returns>
        public static FloatWithUnit? Parse(string? originalText)
        {
            FloatWithUnit? unit = null;

            if (!string.IsNullOrEmpty(originalText))
            {
                unit = new FloatWithUnit();

                // Find where the unit specification starts.  To the left of the unit is the number;
                // split and parse the values.
                int unitStart = originalText.FindFirstNonNumericCharacter(true);
                if (unitStart == -1)
                {
                    if (float.TryParse(originalText, out float newValue))
                        unit.Value = newValue;
                }
                else
                {
                    if (float.TryParse(originalText.Substring(0, unitStart), out float newValue))
                        unit.Value = newValue;

                    string unitName = originalText.Substring(unitStart, originalText.Length - unitStart);
                    unit.Unit = CssUnitConverter.FromText(unitName);
                }
            }
            return unit;
        }
        #endregion
    }
}
