namespace Adaptive.LCARS.UI
{
	/// <summary>
	/// Lists the types of rounding styles that are currently supported.
	/// </summary>
	public enum LcarsRoundingStyle
	{
		/// <summary>
		/// Indicates no rounding style is specified.
		/// </summary>
		None,
		/// <summary>
		/// Indicates the two left corners are rounded.
		/// </summary>
		Left,
		/// <summary>
		/// Indicates the two right corners are rounded.
		/// </summary>
		Right,
		/// <summary>
		/// Indicates all corners are rounded.
		/// </summary>
		Both
	}
}