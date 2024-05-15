namespace Adaptive.Intelligence.Css
{
	/// <summary>
	/// Lists the types of values that are used with the CSS position property.
	/// </summary>
	public enum ElementPosition
	{
		/// <summary>
		/// Indicates the position is not specified and should not be rendered.
		/// </summary>
		NotSpecified = 0,
		/// <summary>
		/// Indicates a static position: Static positioned elements are not affected by the top, bottom, left, and right properties.
		/// </summary>
		Static = 1,
		/// <summary>
		/// Indicates a relative position: elements are positioned relative to their normal (starting?) position.
		/// </summary>
		Relative = 2,
		/// <summary>
		/// Indicates a fixed position: elements remain in specified position; always visible - ignores scrolling.
		/// </summary>
		Fixed = 3,
		/// <summary>
		/// Indicates an absolute position: elements are positioned absolutely, but relative to their parent.
		/// </summary>
		Absolute = 4,
		/// <summary>
		/// Indicates a sticky position: works like relative, but cannot be scrolled off the page.
		/// </summary>
		Sticky = 5
	}
}
