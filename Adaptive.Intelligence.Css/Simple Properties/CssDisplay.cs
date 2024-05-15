using Adaptive.Intelligence.Shared;
using System.Text;

namespace Adaptive.Intelligence.Css
{
	/// <summary>
	/// Represents a CSS position property.
	/// </summary>
	/// <seealso cref="DisposableObjectBase" />
	public sealed class CssDisplay : DisposableObjectBase, ICssProperty
	{
		#region Private Member Declarations        
		/// <summary>
		/// The position specification.
		/// </summary>
		private DisplayOption? _display;
		#endregion

		#region Constructor / Dispose Methods        
		/// <summary>
		/// Initializes a new instance of the <see cref="CssDisplay"/> class.
		/// </summary>
		/// <remarks>
		/// This is the default constructor.
		/// </remarks>
		public CssDisplay()
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="CssDisplay"/> class.
		/// </summary>
		/// <param name="positionValue">
		/// A <see cref="DisplayOption"/> enumerated value, or <b>null</b> if not set.
		/// </param>
		public CssDisplay(DisplayOption? positionValue)
		{
			_display = positionValue;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="CssDisplay"/> class.
		/// </summary>
		/// <param name="cssDefinition">
		/// A string containing the CSS definition for the property value.
		/// </param>
		public CssDisplay(string cssDefinition)
		{
			ParseCss(cssDefinition);
		}
		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
		/// <b>false</b> to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			_display = null;
			base.Dispose(disposing);
		}
		#endregion

		#region Public Properties		
		/// <summary>
		/// Gets or sets a value indicating whether the contents of the property can/should be rendered.
		/// </summary>
		/// <value>
		///   <c>true</c> if the property can/should be rendered; otherwise, <c>false</c>.
		/// </value>
		public bool CanRender => _display != null && _display != DisplayOption.NotSpecified;
		/// <summary>
		/// Gets or sets the position value.
		/// </summary>
		/// <value>
		/// A <see cref="CssDisplay"/> enumerated value, or <b>null</b> if not set.
		/// </value>
		public DisplayOption? Display
		{
			get => _display;
			set => _display = value;
		}
		#endregion

		#region Public Methods / Functions		
		/// <summary>
		/// Resets the content of the property and sets it to an "un-set" state.
		/// </summary>
		public void Clear()
		{
			_display = null;
		}
		/// <summary>
		/// Parses the CSS content for the instance.
		/// </summary>
		/// <param name="cssDefinition">
		/// A string containing the raw CSS definition / code defining the item.
		/// </param>
		/// <exception cref="System.NotImplementedException"></exception>
		public void ParseCss(string? cssDefinition)
		{
			if (string.IsNullOrEmpty(cssDefinition))
			{
				_display = null;
			}
			else
			{
				_display = DisplayOptionConverter.FromText(cssDefinition);
			}
		}
		/// <summary>
		/// Converts to css.
		/// </summary>
		/// <returns>
		/// A string contianing the CSS code to be used for the item being represented.
		/// </returns>
		public string ToCss()
		{
			StringBuilder builder = new StringBuilder();

			if (_display != null && _display.Value != DisplayOption.NotSpecified)
			{
				builder.Append(CssPropertyNames.Display + CssLiterals.CssSeparator + CssLiterals.Space + DisplayOptionConverter.ToText(_display.Value));
				builder.Append(CssLiterals.CssTerminator);
			}
			
			return builder.ToString();
		}
		#endregion
	}
}
