namespace Adaptive.Intelligence.Css
{
	/// <summary>
	/// Lists the types of vertical alignment that are currently supported.
	/// </summary>
	public enum VerticalAlignment
	{
		/// <summary>
		/// Indicates the vertical alignment is not specified and should not be rendered.
		/// </summary>
		NotSpecified = 0,
		/// <summary>
		/// Indicates the HTML element aligns with the baseline of the parent.
		/// </summary>
		Baseline = 1,
		/// <summary>
		/// Indicates the HTML element aligns the baseline of the element with the subscript-baseline of its parent.
		/// </summary>
		Sub = 2,
		/// <summary>
		/// Indicates the HTML element aligns the baseline of the element with the superscript-baseline of its parent.
		/// </summary>
		Super = 3,
		/// <summary>
		/// Indicates the HTML element aligns the top of the element with the top of the parent element's font.
		/// </summary>
		TextTop = 4,
		/// <summary>
		/// Indicates the HTML element aligns the bottom of the element with the bottom of the parent element's font.
		/// </summary>
		TextBottom = 5,
		/// <summary>
		/// Indicates the HTML element aligns the middle of the element with the baseline plus half the x-height of the parent.
		/// </summary>
		Middle = 6,
		/// <summary>
		/// Indicates the HTML element the top of the element and its descendants with the top of the entire line.
		/// </summary>
		Top = 7,
		/// <summary>
		/// Indicates the HTML element aligns the bottom of the element and its descendants with the bottom of the entire line.
		/// </summary>
		Bottom = 8,
		/// <summary>
		/// Indicates the HTML element inherits its setting from the parent. 
		/// </summary>
		Inherit = 21,
		/// <summary>
		/// Indicates the initial setting for the item.
		/// </summary>
		Initial = 22,
		/// <summary>
		/// Indicates the HTML element vertical alignment reverts to the previous setting.
		/// </summary>
		Revert = 23,
		/// <summary>
		/// Indicates the HTML element vertical alignment reverts to the parent of the parent.
		/// </summary>
		RevertLayer = 24,
		/// <summary>
		/// Indicates the vertical alignment value is un-set and should not be rendered/
		/// </summary>
		Unset = 25,
		/// <summary>
		/// Indicates the HTML element's vertical alignment is expressed in a value and unit pair expression.
		/// </summary>
		IsUnit = 30
	}
}
