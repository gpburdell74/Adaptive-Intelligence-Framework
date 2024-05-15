namespace Adaptive.Intelligence.Css
{
    /// <summary>
    /// Lists the types of units used in CSS that are currently supported.
    /// </summary>
    public enum CssUnit
    {
        /// <summary>
        /// Indicates no unit, or one is not specified.
        /// </summary>
        NotSpecified = 0,
        /// <summary>
        /// Indicates an absolute length centimeters unit. (cm).
        /// </summary>
        Centimeters = 1,
        /// <summary>
        /// Indicates an absolute length millimeters unit. (mm).
        /// </summary>
        Millimeters = 2,
        /// <summary>
        /// Indicates an absolute length inches unit. (in).
        /// </summary>
        Inches = 3,
        /// <summary>
        /// Indicates an absolute size pixels unit. (px).
        /// </summary>
        Pixels = 4,
        /// <summary>
        /// Indicates an absolute size points unit. (pt) - this is about 1/72 of an inch.
        /// </summary>
        Points = 5,
        /// <summary>
        /// Indicates an absolute size picas unit. (pc) - 1 pc == 12 pts.
        /// </summary>
        Picas = 6,
        /// <summary>
        /// Indicates the relative font size (em) unit where 1em == the current font size.
        /// </summary>
        Em = 10,
        /// <summary>
        /// Indicates the relative font size (em) relative to the x-height of the font.
        /// </summary>
        Ex = 11,
        /// <summary>
        /// Indicates the relative font size (em) relative to the "0" character.
        /// </summary>
        Ch = 12,
        /// <summary>
        /// Indicates the relative font size (em) relative to the root element.
        /// </summary>
        Rem = 13,
        /// <summary>
        /// Indicates the relative font size (em) relative to relative to 1% of the width of the viewport.
        /// </summary>
        ViewpoirtWidth = 14,
        /// <summary>
        /// Indicates the relative font size (em) relative to relative to 1% of the height of the viewport.
        /// </summary>
        ViewportHeight = 15,
        /// <summary>
        /// Indicates the relative font size (em) relative to relative to 1% of the smaller dimension of the viewport.
        /// </summary>
        ViewportMin = 16,
        /// <summary>
        /// Indicates the relative font size (em) relative to relative to 1% of the larger dimension of the viewport.
        /// </summary>
        ViewportMax = 17,
        /// <summary>
        /// Indicates a percentage value.
        /// </summary>
        Percent = 20
    }
}
