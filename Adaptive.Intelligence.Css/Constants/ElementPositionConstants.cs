namespace Adaptive.Intelligence.Css
{
	/// <summary>
	/// Provides the constants definitions for CSS position property values.
	/// </summary>
	public static class ElementPositionConstants
	{
		/// <summary>
		/// Indicates a static position: Static positioned elements are not affected by the top, bottom, left, and right properties.
		/// </summary>
		public const string Static = "static";
		/// <summary>
		/// Indicates a relative position: elements are positioned relative to their normal (starting?) position.
		/// </summary>
		public const string Relative = "relative";
		/// <summary>
		/// Indicates a fixed position: elements remain in specified position; always visible - ignores scrolling.
		/// </summary>
		public const string Fixed = "fixed";
		/// <summary>
		/// Indicates an absolute position: elements are positioned absolutely, but relative to their parent.
		/// </summary>
		public const string Absolute = "absolute";
		/// <summary>
		/// Indicates a sticky position: works like relative, but cannot be scrolled off the page.
		/// </summary>
		public const string Sticky = "sticky";
	}
}
