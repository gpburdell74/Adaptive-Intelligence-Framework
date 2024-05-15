namespace Adaptive.Intelligence.Css
{
	/// <summary>
	/// Provides the constants definitions for the "align-content" property.
	/// </summary>
	public static class CssAlignContentOptionConstants
	{
		/// <summary>
		///  Default value.Lines stretch to take up the remaining space
		/// </summary>
		public const string Stretch = "stretch";
		/// <summary>
		/// Lines are packed toward the center of the flex container
		/// </summary>
		public const string Center = "center";
		/// <summary>
		/// Lines are packed toward the start of the flex container.
		/// </summary>
		public const string FlexStart = "flex-start";
		/// <summary>
		/// Lines are packed toward the end of the flex container.
		/// </summary>
		public const string FlexEnd = "flex-end";
		/// <summary>
		/// Lines are evenly distributed in the flex container.
		/// </summary>
		public const string SpaceBetween = "space-between";
		/// <summary>
		/// Lines are evenly distributed in the flex container, with half-size spaces on either end.
		/// </summary>
		public const string SpaceAround = "space-around";
		/// <summary>
		/// Lines are evenly distributed in the flex container, with equal space around them.
		/// </summary>
		public const string SpaceEvenly = "space-evenly";
		/// <summary>
		/// Sets this property to its default value.
		/// </summary>
		public const string Initial = CssLiterals.ValueInitial;
		/// <summary>
		/// Inherits this property from its parent element.
		/// </summary>
		public const string Inherit = CssLiterals.ValueInherit;
	}
}
