namespace Adaptive.Intelligence.Css
{
	/// <summary>
	/// Provides the constants definitions for the "display" property.
	/// </summary>
	public static class DisplayConstants
	{
		/// <summary>
		/// Displays an element as an inline element(like<span>). Any height and width properties will have no effect.This is default.	.
		/// </summary>
		public const string Inline = "inline";
		/// <summary>
		/// Displays an element as a block element(like<p>). It starts on a new line, and takes up the whole width.
		/// </summary>
		public const string Block = "block";
		/// <summary>
		/// Makes the container disappear, making the child elements children of the element the next level up in the DOM.
		/// </summary>
		public const string Contents = "contents";
		/// <summary>
		/// Displays an element as a block-level flex container
		/// </summary>
		public const string Flex = "flex";
		/// <summary>
		/// Displays an element as a block-level grid container.
		/// </summary>
		public const string Grid = "grid";
		/// <summary>
		/// Displays an element as an inline-level block container.The element itself is formatted as an inline element, but you can apply height and width values.
		/// </summary>
		public const string InlineBlock = "inline-block";
		/// <summary>
		/// Displays an element as an inline-level flex container.
		/// </summary>
		public const string InlineFlex = "inline-flex";
		/// <summary>
		/// Displays an element as an inline-level grid container.
		/// </summary>
		public const string InlineGrid = "inline-grid";
		/// <summary>
		/// The element is displayed as an inline-level table.
		/// </summary>
		public const string InlineTable = "inline-table";
		/// <summary>
		/// Let the element behave like a<li> element.
		/// </summary>
		public const string ListItem = "list-item";
		/// <summary>
		/// Displays an element as either block or inline, depending on context.
		/// </summary>
		public const string RunIn = "run-in";
		/// <summary>
		/// Let the element behave like a<table> element
		/// </summary>
		public const string Table = "table";
		/// <summary>
		/// Let the element behave like a<caption> element.
		/// </summary>
		public const string TableCaption = "table-caption";
		/// <summary>
		/// Let the element behave like a<colgroup> element.
		/// </summary>
		public const string TableColumnGroup = "table-column-group";
		/// <summary>
		/// Let the element behave like a<thead> element.
		/// </summary>
		public const string TableHeaderGroup = "table-header-group";
		/// <summary>
		/// Let the element behave like a<tfoot> element.
		/// </summary>
		public const string TableFooterGroup = " table-footer-group";
		/// <summary>
		/// Let the element behave like a<tbody> element.
		/// </summary>
		public const string TableRowGroup = "table-row-group";
		/// <summary>
		/// Let the element behave like a<td> element.
		/// </summary>
		public const string TableCell = "table-cell";
		/// <summary>
		/// Let the element behave like a<col> element.
		/// </summary>
		public const string TableColumn = "table-column";
		/// <summary>
		/// Let the element behave like a<tr> element
		/// </summary>
		public const string TableRow = "table-row";
		/// <summary>
		///  The element is completely removed
		/// </summary>
		public const string None = "none";
		/// <summary>
		/// Sets this property to its default value.
		/// </summary>
		public const string Initial = CssLiterals.ValueInitial;
		/// <summary>
		/// Inherits the proeprty from the container.
		/// </summary>
		public const string Inherit = CssLiterals.ValueInherit;
	}
}
