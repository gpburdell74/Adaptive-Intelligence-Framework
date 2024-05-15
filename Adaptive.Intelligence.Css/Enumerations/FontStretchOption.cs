namespace Adaptive.Intelligence.Css
{
    /// <summary>
    /// Lists the font-stretch options that are currently supported.
    /// </summary>
    public enum FontStretchOption
    {
        /// <summary>
        /// Indicates that this value is not set.
        /// </summary>
        NotSpecified = 0,
        /// <summary>
        /// Indicates ultra condensed: Makes the text as narrow as it gets.
        /// </summary>
        UltraCondensed = 1,
        /// <summary>
        /// Indicates extra condensed: Makes the text narrower than condensed, but not as narrow as ultra-condensed.
        /// </summary>
        ExtraCondensed= 2,
        /// <summary>
        /// Indicates condensed: Makes the text narrower than semi-condensed, but not as narrow as extra-condensed
        /// </summary>
        Condensed  = 3,
        /// <summary>
        /// Indicates semi-condensed:  Makes the text narrower than normal, but not as narrow as condensed
        /// </summary>
        SemiCondensed = 4,
        /// <summary>
        /// Indicates the norma value: No font stretching.
        /// </summary>
        Normal  = 5,
        /// <summary>
        /// Indicates semi-expandedd:  Makes the text wider than normal, but not as wide as expanded
        /// </summary>
        SemiExpanded = 6,
        /// <summary>
        /// Indicates expanded: Makes the text wider than semi-expanded, but not as wide as extra-expanded
        /// </summary>
        Expanded = 7,
        /// <summary>
        /// Indicates extra expanded: Makes the text wider than expanded, but not as wide as ultra-expanded.
        /// </summary>
        ExtraExpanded = 8,
        /// <summary>
        /// Indicates ultra expanded: Makes the text as wide as it gets.
        /// </summary>
        UltraExpanded = 9,
        /// <summary>
        /// Indicates initial: Sets this property to its default value.
        /// </summary>
        Initial = 20,
        /// <summary>
        /// Indicates inherit: Inherits this property from its parent element. 
        /// </summary>
        Inherit = 30
    }
}
