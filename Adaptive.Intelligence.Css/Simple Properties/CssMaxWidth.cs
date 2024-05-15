using Adaptive.Intelligence.Shared;
using System.Text;

namespace Adaptive.Intelligence.Css
{
	/// <summary>
	/// Contains a deinition of a max-width property.
	/// </summary>
	/// <seealso cref="DisposableObjectBase" />
	public sealed class CssMaxWidth : DisposableObjectBase, ICssProperty
	{
		#region Private Member Declarations
		/// <summary>
		/// The size of the Max, as specified as a unit.
		/// </summary>
		private FloatWithUnit? _unit;
		#endregion

		#region Constructor / Dispose Methods                
		/// <summary>
		/// Initializes a new instance of the <see cref="CssMaxWidth"/> class.
		/// </summary>
		/// <param name="options">
		/// The <see cref="MaxWidthOptions"/> enumerated value indicating which type of Max-width specification is used.
		/// </param>
		public CssMaxWidth()
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="CssMaxWidth"/> class.
		/// </summary>
		/// <param name="cssSpecification">
		/// A string containing the CSS specification code to be parsed.
		/// </param>
		public CssMaxWidth(string cssSpecification)
		{
			ParseCss(cssSpecification);
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="CssMaxWidth"/> class.
		/// </summary>
		/// <param name="value">
		/// A <see cref="float"/> specifying the Max width value.
		/// </param>
		public CssMaxWidth(float value)
		{
			_unit = new FloatWithUnit(value);
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="CssMaxWidth"/> class.
		/// </summary>
		/// <param name="value">
		/// A <see cref="CssUnit"/> enumerated value indicating the unit of measurement.
		/// </param>
		public CssMaxWidth(float value, CssUnit unit)
		{
			_unit = new FloatWithUnit(value, unit);
		}
		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
		/// <b>false</b> to release only unmanaged resources.</param>
		/// <returns></returns>
		protected override void Dispose(bool disposing)
		{
			if (!IsDisposed && disposing)
			{
				_unit?.Dispose();
			}
			_unit = null;
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
		public bool CanRender => _unit != null;
		/// <summary>
		/// Gets or sets the reference to the value and unit of measurement.
		/// </summary>
		/// <value>
		/// A <see cref="FloatWithUnit"/> instance describing the Max width as a value and optional unit,
		/// or <b>null</b> if not used or not set.
		/// </value>
		public FloatWithUnit? Unit
		{
			get => _unit;
			set => _unit = value;
		}
		#endregion

		#region Public Methods / Functions        
		/// <summary>
		/// Resets the content of the property and sets it to an "un-set" state.
		/// </summary>
		public void Clear()
		{
			_unit?.Dispose();
			_unit = null;
		}
		/// <summary>
		/// Parses the CSS content for the instance.
		/// </summary>
		/// <param name="cssDefinition">A string containing the raw CSS definition / code defining the item.</param>
		public void ParseCss(string? cssDefinition)
		{
			_unit?.Dispose();
			_unit = null;

			if (!string.IsNullOrEmpty(cssDefinition))
			{
				_unit = FloatWithUnit.Parse(cssDefinition);
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
			if (CanRender)
			{
				builder.Append(CssPropertyNames.MaxWidth + CssLiterals.CssSeparator + CssLiterals.Space);
				builder.Append(_unit.ToString());
				builder.Append(CssLiterals.CssTerminator);
			}
			return builder.ToString();
		}
		#endregion
	}
}
