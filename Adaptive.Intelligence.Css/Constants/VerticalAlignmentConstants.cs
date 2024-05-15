namespace Adaptive.Intelligence.Css
{
	/// <summary>
	/// Provides the constants for the vertical-align property values.
	/// </summary>
	public static class VerticalAlignmentConstants
	{
		/// <summary>
		/// Indicates the HTML element aligns with the baseline of the parent.
		/// </summary>
		public const string Baseline = "baseline";
		/// <summary>
		/// Indicates the HTML element aligns the baseline of the element with the subscript-baseline of its parent.
		/// </summary>
		public const string Sub = "sub";
		/// <summary>
		/// Indicates the HTML element aligns the baseline of the element with the superscript-baseline of its parent.
		/// </summary>
		public const string Super = "super";
		/// <summary>
		/// Indicates the HTML element aligns the top of the element with the top of the parent element's font.
		/// </summary>
		public const string TextTop = "text-top";
		/// <summary>
		/// Indicates the HTML element aligns the bottom of the element with the bottom of the parent element's font.
		/// </summary>
		public const string TextBottom = "text-bottom";
		/// <summary>
		/// Indicates the HTML element aligns the middle of the element with the baseline plus half the x-height of the parent.
		/// </summary>
		public const string Middle = "middle";
		/// <summary>
		/// Indicates the HTML element the top of the element and its descendants with the top of the entire line.
		/// </summary>
		public const string Top =  "top";
		/// <summary>
		/// Indicates the HTML element aligns the bottom of the element and its descendants with the bottom of the entire line.
		/// </summary>
		public const string Bottom =  "bottom";
		/// <summary>
		/// Indicates the HTML element inherits its setting from the parent. 
		/// </summary>
		public const string Inherit = CssLiterals.ValueInherit;
		/// <summary>
		/// Indicates the initial setting for the item.
		/// </summary>
		public const string Initial = CssLiterals.ValueInitial;
		/// <summary>
		/// Indicates the HTML element vertical alignment reverts to the previous setting.
		/// </summary>
		public const string Revert = CssLiterals.ValueRevert;
		/// <summary>
		/// Indicates the HTML element vertical alignment reverts to the parent of the parent.
		/// </summary>
		public const string RevertLayer = CssLiterals.ValueRevertLayer;
		/// <summary>
		/// Indicates the vertical alignment value is un-set and should not be rendered/
		/// </summary>
		public const string Unset = CssLiterals.ValueUnset;
	}
}
