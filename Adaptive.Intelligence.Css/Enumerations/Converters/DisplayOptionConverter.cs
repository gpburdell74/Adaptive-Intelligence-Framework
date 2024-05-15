using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.Css
{
	/// <summary>
	/// Converts the <see cref="DisplayOption"/> enumerated values to strings.
	/// </summary>
	/// <seealso cref="IOneWayValueConverter{FromType, ToType}" />
	public sealed class DisplayOptionConverter : IValueConverter<DisplayOption, string>
	{
		/// <summary>
		/// Converts the original <see cref="DisplayOption"/> enumerated value to the string value used in CSS/HTML.
		/// </summary>
		/// <param name="originalValue">
		/// The <see cref="DisplayOption"/> enumerated value to be converted.
		/// </param>
		/// <returns>
		/// A string containing the text representation of the unit, or <see cref="string.Empty"/> if 
		/// <see cref="DisplayOptions.NotSpecified"/> is specified.
		/// </returns>
		public string Convert(DisplayOption originalValue)
		{
			string value = string.Empty;

			switch (originalValue)
			{
				case DisplayOption.Inline:
					value = DisplayConstants.Inline;
					break;

				case DisplayOption.Block:
					value = DisplayConstants.Block;
					break;

				case DisplayOption.Contents:
					value = DisplayConstants.Contents;
					break;

				case DisplayOption.Flex:
					value = DisplayConstants.Flex;
					break;

				case DisplayOption.Grid:
					value = DisplayConstants.Grid;
					break;

				case DisplayOption.InlineBlock:
					value = DisplayConstants.InlineBlock;
					break;

				case DisplayOption.InlineFlex:
					value = DisplayConstants.InlineFlex;
					break;

				case DisplayOption.InlineGrid:
					value = DisplayConstants.InlineGrid;
					break;

				case DisplayOption.InlineTable:
					value = DisplayConstants.InlineTable;
					break;

				case DisplayOption.ListItem:
					value = DisplayConstants.ListItem;
					break;

				case DisplayOption.RunIn:
					value = DisplayConstants.RunIn;
					break;

				case DisplayOption.Table:
					value = DisplayConstants.Table;
					break;

				case DisplayOption.TableCaption:
					value = DisplayConstants.TableCaption;
					break;

				case DisplayOption.TableColumnGroup:
					value = DisplayConstants.TableColumnGroup;
					break;

				case DisplayOption.TableHeaderGroup:
					value = DisplayConstants.TableHeaderGroup;
					break;

				case DisplayOption.TableRowGroup:
					value = DisplayConstants.TableRowGroup;
					break;

				case DisplayOption.TableFooterGroup:
					value = DisplayConstants.TableFooterGroup;
					break;

				case DisplayOption.TableCell:
					value = DisplayConstants.TableCell;
					break;

				case DisplayOption.TableColumn:
					value = DisplayConstants.TableColumn;
					break;

				case DisplayOption.TableRow:
					value = DisplayConstants.TableRow;
					break;

				case DisplayOption.None:
					value = DisplayConstants.None;
					break;

				case DisplayOption.Initial:
					value = DisplayConstants.Initial;
					break;

				case DisplayOption.Inherit:
					value = DisplayConstants.Inherit;
					break;
			}
			return value;
		}
		/// <summary>
		/// Converts the converted value to the original representation.
		/// </summary>
		/// <param name="convertedValue">The original value to be converted.</param>
		/// <returns>
		/// The <typeparamref name="FromType" /> value.
		/// </returns>
		/// <remarks>
		/// The implementation of this method must be the inverse of the <see cref="M:Adaptive.Intelligence.Shared.IValueConverter`2.Convert(`0)" /> method.
		/// </remarks>
		public DisplayOption ConvertBack(string convertedValue)
		{
			DisplayOption value = DisplayOption.NotSpecified;

			if (!string.IsNullOrEmpty(convertedValue))
			{
				switch (convertedValue.ToLower())
				{
					case DisplayConstants.Inline:
						value = DisplayOption.Inline;
						break;

					case DisplayConstants.Block:
						value = DisplayOption.Block;
						break;

					case DisplayConstants.Contents:
						value = DisplayOption.Contents;
						break;

					case DisplayConstants.Flex:
						value = DisplayOption.Flex;
						break;

					case DisplayConstants.Grid:
						value = DisplayOption.Grid;
						break;

					case DisplayConstants.InlineBlock:
						value = DisplayOption.InlineBlock;
						break;

					case DisplayConstants.InlineFlex:
						value = DisplayOption.InlineFlex;
						break;

					case DisplayConstants.InlineGrid:
						value = DisplayOption.InlineGrid;
						break;

					case DisplayConstants.InlineTable:
						value = DisplayOption.InlineTable;
						break;

					case DisplayConstants.ListItem:
						value = DisplayOption.ListItem;
						break;

					case DisplayConstants.RunIn:
						value = DisplayOption.RunIn;
						break;

					case DisplayConstants.Table:
						value = DisplayOption.Table;
						break;

					case DisplayConstants.TableCaption:
						value = DisplayOption.TableCaption;
						break;

					case DisplayConstants.TableColumnGroup:
						value = DisplayOption.TableColumnGroup;
						break;

					case DisplayConstants.TableHeaderGroup:
						value = DisplayOption.TableHeaderGroup;
						break;

					case DisplayConstants.TableRowGroup:
						value = DisplayOption.TableRowGroup;
						break;

					case DisplayConstants.TableFooterGroup:
						value = DisplayOption.TableFooterGroup;
						break;

					case DisplayConstants.TableCell:
						value = DisplayOption.TableCell;
						break;

					case DisplayConstants.TableColumn:
						value = DisplayOption.TableColumn;
						break;

					case DisplayConstants.TableRow:
						value = DisplayOption.TableRow;
						break;

					case DisplayConstants.None:
						value = DisplayOption.None;
						break;

					case DisplayConstants.Initial:
						value = DisplayOption.Initial;
						break;

					case DisplayConstants.Inherit:
						value = DisplayOption.Inherit;
						break;

				}
			}
			return value;
		}
		/// <summary>
		/// Provides the static implementation to converts the original <see cref="DisplayOptions"/> enumerated value to the string value used in CSS/HTML.
		/// </summary>
		/// <param name="originalValue">
		/// The <see cref="DisplayOptions"/> enumerated value to be converted.
		/// </param>
		/// <returns>
		/// A string containing the text representation of the unit, or <see cref="string.Empty"/> if 
		/// <see cref="DisplayOptions.NotSpecified"/> is specified.
		/// </returns>
		public static DisplayOption? FromText(string originalValue)
		{
			DisplayOption? value = null;

			if (originalValue != null)
			{
				DisplayOptionConverter converter = new DisplayOptionConverter();
				value = converter.ConvertBack(originalValue);
			}

			return value;
		}
		/// <summary>
		/// Provides the static implementation to converts the original <see cref="DisplayOption"/> enumerated value to the string value used in CSS/HTML.
		/// </summary>
		/// <param name="originalValue">
		/// The <see cref="DisplayOption"/> enumerated value to be converted.
		/// </param>
		/// <returns>
		/// A string containing the text representation of the unit, or <see cref="string.Empty"/> if 
		/// <see cref="DisplayOption.NotSpecified"/> is specified.
		/// </returns>
		public static string? ToText(DisplayOption? originalValue)
		{
			string? value = null;

			if (originalValue != null)
			{
				DisplayOptionConverter converter = new DisplayOptionConverter();
				value = converter.Convert(originalValue.Value);
			}

			return value;
		}
	}
}
