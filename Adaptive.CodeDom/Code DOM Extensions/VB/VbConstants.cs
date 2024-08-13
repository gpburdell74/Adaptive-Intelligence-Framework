using Adaptive.Intelligence.Shared;

namespace Adaptive.CodeDom
{
	/// <summary>
	/// Provides the Visual Basic language specific constants.
	/// </summary>
	public static class VbConstants
	{
		/// <summary>
		/// The C# block start character.
		/// </summary>
		public const string VbBlockStart = "{";
		/// <summary>
		/// The C# block end character.
		/// </summary>
		public const string VbBlockEnd = Constants.CrLf;
		/// <summary>
		/// The end namespace text.
		/// </summary>
		public const string VbEndNamespace = "End Namespace";	
		/// <summary>
		/// The region start text.
		/// </summary>
		public const string RegionStart = "# Region";
		/// <summary>
		/// The region end text.
		/// </summary>
		public const string RegionEnd = "# End Region";
	}
}
