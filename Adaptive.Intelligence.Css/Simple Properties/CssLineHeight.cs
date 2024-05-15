using Adaptive.Intelligence.Shared;
using System.Text;

namespace Adaptive.Intelligence.Css
{
	/// <summary>
	/// Contains a definition of a line-height property.
	/// </summary>
	/// <seealso cref="DisposableObjectBase" />
	public sealed class CssLineHeight : DisposableObjectBase, ICssProperty
	{
		#region Private Member Declarations
		/// <summary>
		/// The type of the line-height specification.
		/// </summary>
		private LineHeightOptions? _option;
		/// <summary>
		/// The size of the line, as specified as a unit.
		/// </summary>
		private FloatWithUnit? _unit;
		#endregion

		#region Constructor / Dispose Methods                
		/// <summary>
		/// Initializes a new instance of the <see cref="CssLineHeight"/> class.
		/// </summary>
		/// <param name="options">
		/// The <see cref="LineHeightOptions"/> enumerated value indicating which type of line-height specification is used.
		/// </param>
		public CssLineHeight(LineHeightOptions options)
		{
			_option = options;
			if (_option == LineHeightOptions.IsUnit)
				_unit = new FloatWithUnit();
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="CssLineHeight"/> class.
		/// </summary>
		/// <param name="cssSpecification">
		/// A string containing the CSS specification code to be parsed.
		/// </param>
		public CssLineHeight(string cssSpecification)
		{
			ParseCss(cssSpecification);
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="CssLineHeight"/> class.
		/// </summary>
		/// <param name="value">
		/// A <see cref="float"/> specifying the line height value.
		/// </param>
		public CssLineHeight(float value)
		{
			_option = LineHeightOptions.IsUnit;
			_unit = new FloatWithUnit(value);
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="CssLineHeight"/> class.
		/// </summary>
		/// <param name="value">
		/// A <see cref="CssUnit"/> enumerated value indicating the unit of measurement.
		/// </param>
		public CssLineHeight(float value, CssUnit unit)
		{
			_option = LineHeightOptions.IsUnit;
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
			_option = null;
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
		public bool CanRender
		{
			get
			{
				bool isValid = (_option != null && _option.Value != LineHeightOptions.NotSpecified);
				if (isValid && _option.Value == LineHeightOptions.IsUnit)
					isValid = _unit != null;
				return isValid;
			}
		}
		/// <summary>
		/// Gets or sets the line-height specification type.
		/// </summary>
		/// <value>
		/// A <see cref="LineHeightOptions"/> containing the specification type.
		/// </value>
		public LineHeightOptions? Option
		{
			get => _option;
			set
			{
				_option = value;
				if (value != LineHeightOptions.IsUnit)
				{
					_unit?.Dispose();
					_unit = null;
				}
			}
		}
		/// <summary>
		/// Gets or sets the reference to the value and unit of measurement.
		/// </summary>
		/// <value>
		/// A <see cref="FloatWithUnit"/> instance describing the line height as a value and optional unit,
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
			_option  = null;
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
				_option = LineHeightOptionsConverter.FromText(cssDefinition);
				if (_option == LineHeightOptions.IsUnit)
				{
					_unit = FloatWithUnit.Parse(cssDefinition);
				}
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
				builder.Append(CssPropertyNames.LineHeight + CssLiterals.CssSeparator + CssLiterals.Space);
				if (_option == LineHeightOptions.IsUnit)
					builder.Append(_unit.ToString());
				else
					builder.Append(LineHeightOptionsConverter.ToText(_option));

				builder.Append(CssLiterals.CssTerminator);
			}
			return builder.ToString();

		}
		#endregion
	}
}
