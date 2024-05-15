namespace Adaptive.Intelligence.Css
{
	/// <summary>
	/// Lists the options for the "align-content" property that are currently supported.
	/// </summary>
	public enum AlignContentOption
	{
		/// <summary>
		/// Indicates the content aligmment is not specified and should not be rendered.
		/// </summary>
		NotSpecified = 0,
		/// <summary>
		/// Indicates stretch: Default value. Lines stretch to take up the remaining space.
		/// </summary>
		Stretch = 1,
		/// <summary>
		/// Indicates center: Lines are packed toward the center of the flex container.
		/// </summary>
		Center = 2,
		/// <summary>
		/// Indicates flex-start: Lines are packed toward the start of the flex container.
		/// </summary>
		FlexStart = 3,
		/// <summary>
		/// Indicates flex-end: Lines are packed toward the end of the flex container.
		/// </summary>
		FlexEnd = 4,
		/// <summary>
		/// Indicates space-between: Lines are evenly distributed in the flex container.
		/// </summary>
		SpaceBetween = 5,
		/// <summary>
		/// Indicates space-around: Lines are evenly distributed in the flex container, with half-size spaces on either end.
		/// </summary>
		SpaceAround = 6,
		/// <summary>
		/// Indicates space-evenly:Lines are evenly distributed in the flex container, with equal space around them.
		/// </summary>
		SpaceEvenly = 7,
		/// <summary>
		/// Indicates initial: Sets this property to its default value. Read about initial
		/// </summary>
		Initial = 8, 
		/// <summary>
		/// Indicates inherit: Inherits from the parent container.
		/// </summary>
		Inherit = 9
	}
}
