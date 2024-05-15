using Adaptive.Intelligence.Shared;
using System.Text;

namespace Adaptive.Intelligence.Css
{
	/// <summary>
	/// Represents a CSS align-content property.
	/// </summary>
	/// <seealso cref="DisposableObjectBase" />
	public sealed class CssJustifyContent : DisposableObjectBase, ICssProperty
	{
		#region Private Member Declarations        
		/// <summary>
		/// The position specification.
		/// </summary>
		private AlignContentOption? _alignment;
		#endregion

		#region Constructor / Dispose Methods        
		/// <summary>
		/// Initializes a new instance of the <see cref="CssJustifyContent"/> class.
		/// </summary>
		/// <remarks>
		/// This is the default constructor.
		/// </remarks>
		public CssJustifyContent()
		{

		}
		/// <summary>
		/// Initializes a new instance of the <see cref="CssJustifyContent"/> class.
		/// </summary>
		/// <param name="alignmentValue">
		/// A <see cref="AlignContentOption"/> enumerated value, or <b>null</b> if not set.
		/// </param>
		public CssJustifyContent(AlignContentOption? alignmentValue)
		{
			_alignment = alignmentValue;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="CssJustifyContent"/> class.
		/// </summary>
		/// <param name="cssDefinition">
		/// A string containing the CSS definition for the property value.
		/// </param>
		public CssJustifyContent(string cssDefinition)
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
			_alignment = null;
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
		public bool CanRender => _alignment != null && _alignment != AlignContentOption.NotSpecified;
		/// <summary>
		/// Gets or sets the position value.
		/// </summary>
		/// <value>
		/// A <see cref="AlignContentOption"/> enumerated value, or <b>null</b> if not set.
		/// </value>
		public AlignContentOption? Alignment
		{
			get => _alignment;
			set => _alignment = value;
		}
		#endregion

		#region Public Methods / Functions		
		/// <summary>
		/// Resets the content of the property and sets it to an "un-set" state.
		/// </summary>
		public void Clear()
		{
			_alignment = null;
		}
		/// <summary>
		/// Parses the CSS content for the instance.
		/// </summary>
		/// <param name="cssDefinition">
		/// A string containing the raw CSS definition / code defining the item.
		/// </param>
		public void ParseCss(string? cssDefinition)
		{
			if (string.IsNullOrEmpty(cssDefinition))
			{
				_alignment = null;
			}
			else
			{
				_alignment = AlignContentOptionConverter.FromText(cssDefinition);
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

			if (_alignment != null && _alignment.Value != AlignContentOption.NotSpecified)
			{
				builder.Append(CssPropertyNames.FormatPropertyName(CssPropertyNames.JustifyContent) + AlignContentOptionConverter.ToText(_alignment.Value));
				builder.Append(CssLiterals.CssTerminator);
			}
			
			return builder.ToString();
		}
		#endregion
	}
}
