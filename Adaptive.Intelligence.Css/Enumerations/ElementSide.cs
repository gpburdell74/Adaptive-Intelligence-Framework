namespace Adaptive.Intelligence.Css
{
    /// <summary>
    /// Lists the types / sides of a border definition that are currently supported.
    /// </summary>
    public enum ElementSide
    {
        /// <summary>
        /// Indicates the specific side is not specufied.
        /// </summary>
        NotSpecifed = 0,
        /// <summary>
        /// Indicates all sides of an HTML element.
        /// </summary>
        AllSides = 1,
        /// <summary>
        /// Indicates the left side of an HTML element.
        /// </summary>
        Left = 2,
        /// <summary>
        /// Indicates the right side of an HTML element.
        /// </summary>
        Right = 3,
        /// <summary>
        /// Indicates the top side of an HTML element.
        /// </summary>
        Top = 4,
        /// <summary>
        /// Indicates the bottom side of an HTML element.
        /// </summary>
        Bottom = 5
    }
}
