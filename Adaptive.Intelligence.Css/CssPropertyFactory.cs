namespace Adaptive.Intelligence.Css
{
	/// <summary>
	/// Provides static methods / functions for creatin CSS property instances from provided CSS text values.
	/// </summary>
	public static class CssPropertyFactory
	{
		#region CSS Border
		/// <summary>
		/// Creates the CSS border property instance from the provided text.
		/// </summary>
		/// <param name="cssDefinition">
		/// A string containing the CSS definition, or <b>null</b> if the property is to be un-set.
		/// </param>
		/// <returns>
		/// The new <see cref="CssBorder"/> instance, or <b>null</b> if the text is empty or invalid.
		/// </returns>
		public static CssBorder? CreateBorder(string? cssDefinition)
		{
			CssBorder? border = null;

			if (!string.IsNullOrEmpty(cssDefinition))
			{
				border = new CssBorder();
				border.ParseCss(cssDefinition);
			}
			return border;
		}
		/// <summary>
		/// Creates the CSS border-* property instance.
		/// </summary>
		/// <param name="side">
		/// A <see cref="ElementSide"/> enumerated value indicating which property to create.
		/// </param>
		/// <param name="cssDefinition">
		/// A string containing the CSS definition, or <b>null</b> if the property is to be un-set.
		/// </param>
		/// <returns>
		/// The new <see cref="CssBorderSide"/> instance, or <b>null</b> if the text is empty or invalid.
		/// </returns>
		public static CssBorderSide? CreateBorderSide(ElementSide side, string? cssDefinition)
		{
			CssBorderSide? border = null;

			if (!string.IsNullOrEmpty(cssDefinition))
			{
				border = new CssBorderSide(side);
				border.ParseCss(cssDefinition);
			}
			return border;
		}
		/// <summary>
		/// Creates the CSS border-bottom property instance.
		/// </summary>
		/// <param name="cssDefinition">
		/// A string containing the CSS definition, or <b>null</b> if the property is to be un-set.
		/// </param>
		/// <returns>
		/// The new <see cref="CssBorderSide"/> instance, or <b>null</b> if the text is empty or invalid.
		/// </returns>
		public static CssBorderSide? CreateBorderBottom(string? cssDefinition)
		{
			return CreateBorderSide(ElementSide.Bottom, cssDefinition);
		}
		/// <summary>
		/// Creates the CSS border-left property instance.
		/// </summary>
		/// <param name="cssDefinition">
		/// A string containing the CSS definition, or <b>null</b> if the property is to be un-set.
		/// </param>
		/// <returns>
		/// The new <see cref="CssBorderSide"/> instance, or <b>null</b> if the text is empty or invalid.
		/// </returns>
		public static CssBorderSide? CreateBorderLeft(string cssDefinition)
		{
			return CreateBorderSide(ElementSide.Left, cssDefinition);
		}
		/// <summary>
		/// Creates the CSS border-right property instance.
		/// </summary>
		/// <param name="cssDefinition">
		/// A string containing the CSS definition, or <b>null</b> if the property is to be un-set.
		/// </param>
		/// <returns>
		/// The new <see cref="CssBorderSide"/> instance, or <b>null</b> if the text is empty or invalid.
		/// </returns>
		public static CssBorderSide? CreateBorderRight(string cssDefinition)
		{
			return CreateBorderSide(ElementSide.Right, cssDefinition);
		}
		/// <summary>
		/// Creates the CSS border-top property instance.
		/// </summary>
		/// <param name="cssDefinition">
		/// A string containing the CSS definition, or <b>null</b> if the property is to be un-set.
		/// </param>
		/// <returns>
		/// The new <see cref="CssBorderSide"/> instance, or <b>null</b> if the text is empty or invalid.
		/// </returns>
		public static CssBorderSide? CreateBorderTop(string cssDefinition)
		{
			return CreateBorderSide(ElementSide.Top, cssDefinition);
		}
		#endregion

		#region CSS Margin
		/// <summary>
		/// Creates the CSS margin property instance from the provided text.
		/// </summary>
		/// <param name="cssDefinition">
		/// A string containing the CSS definition, or <b>null</b> if the property is to be un-set.
		/// </param>
		/// <returns>
		/// The new <see cref="CssMargin"/> instance, or <b>null</b> if the text is empty or invalid.
		/// </returns>
		public static CssMargin? CreateMargin(string? cssDefinition)
		{
			CssMargin? margin = null;

			if (!string.IsNullOrEmpty(cssDefinition))
			{
				margin = new CssMargin();
				margin.ParseCss(cssDefinition);
			}
			return margin;
		}
		/// <summary>
		/// Creates the CSS margin-* property instance.
		/// </summary>
		/// <param name="side">
		/// A <see cref="ElementSide"/> enumerated value indicating which property to create.
		/// </param>
		/// <param name="cssDefinition">
		/// A string containing the CSS definition, or <b>null</b> if the property is to be un-set.
		/// </param>
		/// <returns>
		/// The new <see cref="CssMarginSide"/> instance, or <b>null</b> if the text is empty or invalid.
		/// </returns>
		public static CssMarginSide? CreateMarginSide(ElementSide side, string? cssDefinition)
		{
			CssMarginSide? margin = null;

			if (!string.IsNullOrEmpty(cssDefinition))
			{
				margin = new CssMarginSide(side);
				margin.ParseCss(cssDefinition);
			}
			return margin;
		}
		/// <summary>
		/// Creates the CSS margin-bottom property instance.
		/// </summary>
		/// <param name="cssDefinition">
		/// A string containing the CSS definition, or <b>null</b> if the property is to be un-set.
		/// </param>
		/// <returns>
		/// The new <see cref="CssMarginSide"/> instance, or <b>null</b> if the text is empty or invalid.
		/// </returns>
		public static CssMarginSide? CreateMarginBottom(string? cssDefinition)
		{
			return CreateMarginSide(ElementSide.Bottom, cssDefinition);
		}
		/// <summary>
		/// Creates the CSS margin-left property instance.
		/// </summary>
		/// <param name="cssDefinition">
		/// A string containing the CSS definition, or <b>null</b> if the property is to be un-set.
		/// </param>
		/// <returns>
		/// The new <see cref="CssMarginSide"/> instance, or <b>null</b> if the text is empty or invalid.
		/// </returns>
		public static CssMarginSide? CreateMarginLeft(string cssDefinition)
		{
			return CreateMarginSide(ElementSide.Left, cssDefinition);
		}
		/// <summary>
		/// Creates the CSS margin-right property instance.
		/// </summary>
		/// <param name="cssDefinition">
		/// A string containing the CSS definition, or <b>null</b> if the property is to be un-set.
		/// </param>
		/// <returns>
		/// The new <see cref="CssMarginSide"/> instance, or <b>null</b> if the text is empty or invalid.
		/// </returns>
		public static CssMarginSide? CreateMarginRight(string cssDefinition)
		{
			return CreateMarginSide(ElementSide.Right, cssDefinition);
		}
		/// <summary>
		/// Creates the CSS margin-top property instance.
		/// </summary>
		/// <param name="cssDefinition">
		/// A string containing the CSS definition, or <b>null</b> if the property is to be un-set.
		/// </param>
		/// <returns>
		/// The new <see cref="CssMarginSide"/> instance, or <b>null</b> if the text is empty or invalid.
		/// </returns>
		public static CssMarginSide? CreateMarginTop(string cssDefinition)
		{
			return CreateMarginSide(ElementSide.Top, cssDefinition);
		}
		#endregion

		#region CSS Padding
		/// <summary>
		/// Creates the CSS padding property instance from the provided text.
		/// </summary>
		/// <param name="cssDefinition">
		/// A string containing the CSS definition, or <b>null</b> if the property is to be un-set.
		/// </param>
		/// <returns>
		/// The new <see cref="CssPadding"/> instance, or <b>null</b> if the text is empty or invalid.
		/// </returns>
		public static CssPadding? CreatePadding(string? cssDefinition)
		{
			CssPadding? padding = null;

			if (!string.IsNullOrEmpty(cssDefinition))
			{
				padding = new CssPadding();
				padding.ParseCss(cssDefinition);
			}
			return padding;
		}
		/// <summary>
		/// Creates the CSS padding-* property instance.
		/// </summary>
		/// <param name="side">
		/// A <see cref="ElementSide"/> enumerated value indicating which property to create.
		/// </param>
		/// <param name="cssDefinition">
		/// A string containing the CSS definition, or <b>null</b> if the property is to be un-set.
		/// </param>
		/// <returns>
		/// The new <see cref="CssPaddingSide"/> instance, or <b>null</b> if the text is empty or invalid.
		/// </returns>
		public static CssPaddingSide? CreatePaddingSide(ElementSide side, string? cssDefinition)
		{
			CssPaddingSide? padding = null;

			if (!string.IsNullOrEmpty(cssDefinition))
			{
				padding = new CssPaddingSide(side);
				padding.ParseCss(cssDefinition);
			}
			return padding;
		}
		/// <summary>
		/// Creates the CSS padding-bottom property instance.
		/// </summary>
		/// <param name="cssDefinition">
		/// A string containing the CSS definition, or <b>null</b> if the property is to be un-set.
		/// </param>
		/// <returns>
		/// The new <see cref="CssPaddingSide"/> instance, or <b>null</b> if the text is empty or invalid.
		/// </returns>
		public static CssPaddingSide? CreatePaddingBottom(string? cssDefinition)
		{
			return CreatePaddingSide(ElementSide.Bottom, cssDefinition);
		}
		/// <summary>
		/// Creates the CSS padding-left property instance.
		/// </summary>
		/// <param name="cssDefinition">
		/// A string containing the CSS definition, or <b>null</b> if the property is to be un-set.
		/// </param>
		/// <returns>
		/// The new <see cref="CssPaddingSide"/> instance, or <b>null</b> if the text is empty or invalid.
		/// </returns>
		public static CssPaddingSide? CreatePaddingLeft(string cssDefinition)
		{
			return CreatePaddingSide(ElementSide.Left, cssDefinition);
		}
		/// <summary>
		/// Creates the CSS padding-right property instance.
		/// </summary>
		/// <param name="cssDefinition">
		/// A string containing the CSS definition, or <b>null</b> if the property is to be un-set.
		/// </param>
		/// <returns>
		/// The new <see cref="CssPaddingSide"/> instance, or <b>null</b> if the text is empty or invalid.
		/// </returns>
		public static CssPaddingSide? CreatePaddingRight(string cssDefinition)
		{
			return CreatePaddingSide(ElementSide.Right, cssDefinition);
		}
		/// <summary>
		/// Creates the CSS padding-top property instance.
		/// </summary>
		/// <param name="cssDefinition">
		/// A string containing the CSS definition, or <b>null</b> if the property is to be un-set.
		/// </param>
		/// <returns>
		/// The new <see cref="CssPaddingSide"/> instance, or <b>null</b> if the text is empty or invalid.
		/// </returns>
		public static CssPaddingSide? CreatePaddingTop(string cssDefinition)
		{
			return CreatePaddingSide(ElementSide.Top, cssDefinition);
		}
		#endregion
	}
}
