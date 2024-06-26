namespace Adaptive.CodeDom
{
	/// <summary>
	/// Lists the types of inheritance modifiers that are currently supported.
	/// </summary>
	public enum InheritanceModifier
	{
		/// <summary>
		/// Indicates no inheritance modifier is specified.
		/// </summary>
		NotSpecified = 0,
		/// <summary>
		/// Indicates an abstract class.
		/// </summary>
		Abstract = 1,
		/// <summary>
		/// Indicates a sealed / final class.
		/// </summary>
		SealedFinal = 2,
		/// <summary>
		/// Indicates a static class.
		/// </summary>
		Static = 3
	}
}
