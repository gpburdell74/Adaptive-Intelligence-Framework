using Adaptive.Intelligence.Shared;
using System.Text;

namespace Adaptive.Intelligence.Css
{
	/// <summary>
	/// Represents a CSS position property.
	/// </summary>
	/// <seealso cref="DisposableObjectBase" />
	public sealed class CssPosition : DisposableObjectBase, ICssProperty
	{
		#region Private Member Declarations        
		/// <summary>
		/// The position specification.
		/// </summary>
		private ElementPosition? _position;
		#endregion

		#region Constructor / Dispose Methods        
		/// <summary>
		/// Initializes a new instance of the <see cref="CssPosition"/> class.
		/// </summary>
		/// <remarks>
		/// This is the default constructor.
		/// </remarks>
		public CssPosition()
		{

		}
		/// <summary>
		/// Initializes a new instance of the <see cref="CssPosition"/> class.
		/// </summary>
		/// <param name="positionValue">
		/// A <see cref="ElementPosition"/> enumerated value, or <b>null</b> if not set.
		/// </param>
		public CssPosition(ElementPosition? positionValue)
		{
			_position = positionValue;
		}
		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
		/// <b>false</b> to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			_position = null;
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
		public bool CanRender => _position != null && _position != ElementPosition.NotSpecified;
		/// <summary>
		/// Gets or sets the position value.
		/// </summary>
		/// <value>
		/// A <see cref="CssPosition"/> enumerated value, or <b>null</b> if not set.
		/// </value>
		public ElementPosition? Position
		{
			get => _position;
			set => _position = value;
		}
		#endregion

		#region Public Methods / Functions		
		/// <summary>
		/// Resets the content of the property and sets it to an "un-set" state.
		/// </summary>
		public void Clear()
		{
			_position = null;
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
				_position = null;
			}
			else
			{
				_position = ElementPositionConverter.FromText(cssDefinition);
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

			if (_position != null && _position.Value != ElementPosition.NotSpecified) 
			{
				builder.Append(CssPropertyNames.Position + CssLiterals.CssSeparator + CssLiterals.Space + ElementPositionConverter.ToText(_position.Value));
			}
			builder.Append(CssLiterals.CssTerminator);
			return builder.ToString();
		}
		#endregion
	}
}
