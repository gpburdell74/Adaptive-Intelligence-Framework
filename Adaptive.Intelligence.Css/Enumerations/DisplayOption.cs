using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel;
using System.Data.Common;
using System.Reflection.Emit;
using System.Threading;
using System;

namespace Adaptive.Intelligence.Css
{
	/// <summary>
	/// Lists the the "display" property settings that are currently supported.
	/// </summary>
	public enum DisplayOption
	{
		/// <summary>
		/// Indicates the display property is not specified and should not be rendered.
		/// </summary>
		NotSpecified = 0,
		/// <summary>
		/// Indicates inline: Displays an element as an inline element (like <span>). Any height and width properties will have no effect. This is default.	
		/// </summary>
		Inline = 1,
		/// <summary>
		/// Indicates block: Displays an element as a block element (like <p>). It starts on a new line, and takes up the whole width .
		/// </summary>
		Block = 2,
		/// <summary>
		/// Indicates contents: Makes the container disappear, making the child elements children of the element the next level up in the DOM.
		/// </summary>
		Contents = 3,
		/// <summary>
		/// Indicates flex: Displays an element as a block-level flex container.
		/// </summary>
		Flex = 4,
		/// <summary>
		/// Indicates grid:Displays an element as a block-level grid container.
		/// </summary>
		Grid = 5,
		/// <summary>
		/// Indicates inline-block:Displays an element as an inline-level block container.The element itself is formatted as an inline element, but you can apply height and width values
		/// </summary>
		InlineBlock = 6,
		/// <summary>
		/// Indicates inline-flex: Displays an element as an inline-level flex container.
		/// </summary>
		InlineFlex = 7,
		/// <summary>
		/// Indicates inline-grid: Displays an element as an inline-level grid container.
		/// </summary>
		InlineGrid = 8,
		/// <summary>
		/// Indicates inline-table: The element is displayed as an inline-level table.
		/// </summary>
		InlineTable = 9,
		/// <summary>
		/// Indicates list-item: Let the element behave like a<li> element.
		/// </summary>
		ListItem = 10,
		/// <summary>
		/// Indicates run-in: Displays an element as either block or inline, depending on context.
		/// </summary>
		RunIn = 11,
		/// <summary>
		/// Indicates table: Let the element behave like a<table> element.
		/// </summary>
		Table = 12,
		/// <summary>
		/// Indicates table-caption: Let the element behave like a <caption> element.
		/// </summary>
		TableCaption = 13,
		/// <summary>
		/// Indicates table-column-group: Let the element behave like a <colgroup> element.
		/// </summary>
		TableColumnGroup = 14,
		/// <summary>
		/// Indicates table-header-group: Let the element behave like a <thead> element.
		/// </summary>
		TableHeaderGroup = 15,
		/// <summary>
		/// Indicates table-footer-group: Let the element behave like a <tfoot> element.
		/// </summary>
		TableFooterGroup = 16,
		/// <summary>
		/// Indicates table-row-group:Let the element behave like a<tbody> element
		/// </summary>
		TableRowGroup = 17,
		/// <summary>
		/// Indicates table-cell: Let the element behave like a<td> element.
		/// </summary>
		TableCell = 18,
		/// <summary>
		/// Indicates table-column: Let the element behave like a<col> element.
		/// </summary>
		TableColumn = 19,
		/// <summary>
		/// Indicates table-row: Let the element behave like a<tr> element.
		/// </summary>
		TableRow = 20,
		/// <summary>
		/// Indicates none: The element is completely removed.
		/// </summary>
		None = 21,
		/// <summary>
		/// Indicates initial: Sets this property to its default value.Read about initial.
		/// </summary>
		Initial = 22,
		/// <summary>
		/// Indicates inherit: 
		/// </summary>
		Inherit = 23
	}
}
