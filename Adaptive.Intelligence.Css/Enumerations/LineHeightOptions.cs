namespace Adaptive.Intelligence.Css
{
	/// <summary>
	/// Lists the type of line-height specifications that are currently supported.
	/// </summary>
	public enum LineHeightOptions
	{
		/// <summary>
		/// Indicates no line-height is specified and should not be rendered.
		/// </summary>
		NotSpecified = 0,
		/// <summary>
		/// Indicates a normal line height. This is default.
		/// </summary>
		Normal = 1,
		/// <summary>
		/// Indicates the property is set to its default value.
		/// </summary>
		Initial = 2,
		/// <summary>
		/// Indicates the property value is inherited from its parent element.
		/// </summary>
		Inherit = 3,
		/// <summary>
		/// Indicates the specification is a unit (number and (optionally) a unit).
		/// </summary>
		IsUnit = 4
	}
}
