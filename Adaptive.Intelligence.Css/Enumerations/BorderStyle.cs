namespace Adaptive.Intelligence.Css
{
    /// <summary>
    /// Lists the border styles that are currently suppored.
    /// </summary>
    public enum BorderStyle
    {
        /// <summary>
        /// Indicates the border style is not specified and should not be rendered.
        /// </summary>
        NotSpecified = 0,
        /// <summary>
        /// Indicates a dotted border.
        /// </summary>
        Dotted = 1,
        /// <summary>
        /// Indicates a dashed border.
        /// </summary>
        Dashed = 2,
        /// <summary>
        /// Indicates a solid border.
        /// </summary>
        Solid = 3,
        /// <summary>
        /// Indicates a double border.
        /// </summary>
        Double = 4,
        /// <summary>
        /// Indicates a 3D grooved border. The effect depends on the border-color value
        /// </summary>
        Groove = 5,
        /// <summary>
        /// Indicates a 3D ridged border. The effect depends on the border-color value
        /// </summary>
        Ridge = 6,
        /// <summary>
        /// Indicates a 3D inset border. The effect depends on the border-color value
        /// </summary>
        Inset = 7,
        /// <summary>
        /// Indicates a 3D outset border. The effect depends on the border-color value 
        /// </summary>
        Outset = 8,
        /// <summary>
        /// Indicates no border at all.
        /// </summary>
        None = 9,
        /// <summary>
        /// Indicates a hidden border.
        /// </summary>
        Hidden = 10
    }
}