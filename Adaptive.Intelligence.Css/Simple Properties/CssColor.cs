using Adaptive.Intelligence.Shared;
using System.Text;

namespace Adaptive.Intelligence.Css
{
	/// <summary>
	/// Represents a CSS color property.
	/// </summary>
	/// <seealso cref="DisposableObjectBase" />
	public sealed class CssColor : DisposableObjectBase, ICssProperty
	{
		#region Private Member Declarations        
		/// <summary>
		/// The position specification.
		/// </summary>
		private string? _color;
		#endregion

		#region Constructor / Dispose Methods        
		/// <summary>
		/// Initializes a new instance of the <see cref="CssColor"/> class.
		/// </summary>
		/// <remarks>
		/// This is the default constructor.
		/// </remarks>
		public CssColor()
		{

		}
		/// <summary>
		/// Initializes a new instance of the <see cref="CssColor"/> class.
		/// </summary>
		/// <param name="color">
		/// A string containing the color specification.
		/// </param>
		public CssColor(string? color)
		{
			_color = color;
		}
		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
		/// <b>false</b> to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			_color = null;
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
		public bool CanRender => !string.IsNullOrEmpty(_color);
		/// <summary>
		/// Gets or sets the color value.
		/// </summary>
		/// <value>
		/// A string containing the color specification.
		/// </value>
		public string? Color
		{
			get => _color;
			set => _color = value;
		}
		#endregion

		#region Public Methods / Functions		
		/// <summary>
		/// Resets the content of the property and sets it to an "un-set" state.
		/// </summary>
		public void Clear()
		{
			_color = null;
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
				_color = null;
			}
			else
			{
				_color = cssDefinition;
			}
		}
		/// <summary>
		/// Converts to css.
		/// </summary>
		/// <returns>
		/// A string contianing the CSS code to be used for the item being represented.
		/// </returns>
		/// <exception cref="System.NotImplementedException"></exception>
		public string ToCss()
		{
			StringBuilder builder = new StringBuilder();

			if (_color != null)
			{
				builder.Append(CssPropertyNames.Color + CssLiterals.CssSeparator + CssLiterals.Space + _color);
			}
			builder.Append(CssLiterals.CssTerminator);
			return builder.ToString();
		}
		#endregion
	}
}
