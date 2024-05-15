using System.Text;

namespace Adaptive.Intelligence.Shared
{
	/// <summary>
	/// Provides extension methods for the <see cref="StringBuilder"/> class.
	/// </summary>
	public static class StringBuilderExtensions
	{
		/// <summary>
		/// Appends a comma character to the end of the string.
		/// </summary>
		/// <param name="builder">
		/// The <see cref="StringBuilder"/> instance being operated on.
		/// </param>
		public static void AppendComma(this StringBuilder builder)
		{
			builder.Append(Constants.Comma);
		}
		/// <summary>
		/// Appends a dot/period character to the end of the string.
		/// </summary>
		/// <param name="builder">
		/// The <see cref="StringBuilder"/> instance being operated on.
		/// </param>
		public static void AppendDot(this StringBuilder builder)
		{
			builder.Append(Constants.Dot);
		}
		/// <summary>
		/// Appends a space character to the end of the string.
		/// </summary>
		/// <param name="builder">
		/// The <see cref="StringBuilder"/> instance being operated on.
		/// </param>
		public static void AppendSpace(this StringBuilder builder)
		{
			builder.Append(Constants.Space);
		}
		/// <summary>
		/// Appends a space character to the end of the string.
		/// </summary>
		/// <param name="builder">
		/// The <see cref="StringBuilder"/> instance being operated on.
		/// </param>
		/// <param name="valueToAppend">
		/// A string containing the value to be appended.
		/// </param>
		public static void AppendWithPrecedingSpace(this StringBuilder builder, string valueToAppend)
		{
			builder.Append(Constants.Space);
			builder.Append(valueToAppend);
		}

	}
}
