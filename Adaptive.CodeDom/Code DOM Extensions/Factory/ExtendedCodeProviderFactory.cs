namespace Adaptive.CodeDom
{
	/// <summary>
	/// Provides static methods / functions for creating extended code provider instances.
	/// </summary>
	public static class ExtendedCodeProviderFactory
	{
		/// <summary>
		/// Creates the provider instance for the specified language.
		/// </summary>
		/// <param name="language">
		/// A <see cref="NetLanguage"/> enumerated value indicating the language for which to create the provider.
		/// </param>
		/// <returns>
		/// An <see cref="IExtendedCodeProvider"/> implementation instance for the specified language.
		/// </returns>
		public static IExtendedCodeProvider CreateProvider(NetLanguage language)
		{
			if (language == NetLanguage.CSharp)
				return new ExtendedCSharpCodeProvider();
			else if (language == NetLanguage.VisualBasic)
				return new ExtendedVBCodeProvider();
			else
				throw new NotSupportedException("The specified language is not supported.");
		}
	}
}
