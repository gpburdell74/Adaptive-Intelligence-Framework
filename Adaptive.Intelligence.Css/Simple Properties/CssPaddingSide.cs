using Adaptive.Intelligence.Shared;
using System.Text;

namespace Adaptive.Intelligence.Css
{
	/// <summary>
	/// Contains a definition of a Padding side (such as Padding-left, Padding-top, etc.).
	/// </summary>
	/// <seealso cref="DisposableObjectBase" />
	public sealed class CssPaddingSide : DisposableObjectBase, ICssProperty
	{
		#region Private Member Declarations
		/// <summary>
		/// The side being represented.
		/// </summary>
		private ElementSide? _side = ElementSide.AllSides;
		/// <summary>
		/// The sizing of the specified Padding.
		/// </summary>
		private FloatWithUnit? _size;
		#endregion

		#region Constructor / Dispose Methods                
		/// <summary>
		/// Initializes a new instance of the <see cref="CssPaddingSide"/> class.
		/// </summary>
		/// <param name="side">
		/// The <see cref="ElementSide"/> enumerated value indicating which Padding is being represented.
		/// </param>
		public CssPaddingSide(ElementSide side)
		{
			_side = side;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="CssPaddingSide"/> class.
		/// </summary>
		/// <param name="cssSpecification">
		/// A string containing the CSS specification code to be parsed.
		/// </param>
		public CssPaddingSide(string cssSpecification)
		{
			ParseCss(cssSpecification);
		}
		/// <param name="side">
		/// The <see cref="ElementSide"/> enumerated value indicating which Padding is being represented.
		/// </param>
		/// <param name="width">
		/// A <see cref="float"/> containing the width value.
		/// </param>
		/// <param name="unit">
		/// A <see cref="CssUnit"/> enumerated value indicating the unit of measurement.
		/// </param>
		public CssPaddingSide(ElementSide side, float width, CssUnit unit) : this(side)
		{
			if (_size != null)
			{
				_size.Value = width;
				_size.Unit = unit;
			}
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
				_size?.Dispose();
			}
			_size = null;
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
				return (_size != null && _side != ElementSide.NotSpecifed);

			}
		}
		/// <summary>
		/// Gets or sets the side of the Padding being represented.
		/// </summary>
		/// <value>
		/// A <see cref="ElementSide"/> enumerated value indicating the individual side,
		/// or all sides.
		/// </value>
		public ElementSide? Side
		{
			get => _side;
			set => _side = value;
		}
		/// <summary>
		/// Gets or sets the size of the Padding.
		/// </summary>
		/// <value>
		/// A <see cref="FloatWithUnit"/> instance describing the size.
		/// </value>
		public FloatWithUnit? Size
		{
			get => _size;
			set => _size = value;
		}
		#endregion

		#region Public Methods / Functions        
		/// <summary>
		/// Resets the content of the property and sets it to an "un-set" state.
		/// </summary>
		public void Clear()
		{
			_size?.Dispose();

			_size = null;
			_side = ElementSide.NotSpecifed;
		}
		/// <summary>
		/// Parses the CSS content for the instance.
		/// </summary>
		/// <param name="cssDefinition">A string containing the raw CSS definition / code defining the item.</param>
		public void ParseCss(string? cssDefinition)
		{
			if (!string.IsNullOrEmpty(cssDefinition))
			{
				PropertyContent propertyDef = CssParsing.ParseProperty(cssDefinition);
				if (propertyDef != null && !propertyDef.IsEmpty)
				{
					SetPropertyName(propertyDef.Name);
					SetValues(propertyDef.Value);
				}
				else if (propertyDef != null)
					SetValues(cssDefinition);
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
				RenderName(builder);
				if (_size != null)
					builder.Append(_size.ToString());
			}
			builder.Append(CssLiterals.CssTerminator);
			return builder.ToString();

		}
		#endregion

		#region Private Methods / Functions        
		/// <summary>
		/// Renders the appropriate property name.
		/// </summary>
		/// <param name="builder">
		/// The <see cref="StringBuilder"/> instance to append the text to.
		/// </param>
		private void RenderName(StringBuilder builder)
		{
			switch (_side)
			{
				case ElementSide.AllSides:
					builder.Append(CssPropertyNames.Padding + CssLiterals.CssSeparator + CssLiterals.Space);
					break;

				case ElementSide.Top:
					builder.Append(CssPropertyNames.PaddingTop + CssLiterals.CssSeparator + CssLiterals.Space);
					break;

				case ElementSide.Left:
					builder.Append(CssPropertyNames.PaddingLeft + CssLiterals.CssSeparator + CssLiterals.Space);
					break;

				case ElementSide.Right:
					builder.Append(CssPropertyNames.PaddingRight + CssLiterals.CssSeparator + CssLiterals.Space);
					break;

				case ElementSide.Bottom:
					builder.Append(CssPropertyNames.PaddingBottom + CssLiterals.CssSeparator + CssLiterals.Space);
					break;
			}
		}
		/// <summary>
		/// Sets the state of the instance based on the provided property name.
		/// </summary>
		/// <param name="propName">
		/// A string containing the name of the CSS property being set.
		/// </param>
		private void SetPropertyName(string? propName)
		{
			switch (propName.ToLower().Trim())
			{
				case CssPropertyNames.Padding:
					_side = ElementSide.AllSides;
					break;

				case CssPropertyNames.PaddingTop:
					_side = ElementSide.Top;
					break;

				case CssPropertyNames.PaddingLeft:
					_side = ElementSide.Left;
					break;

				case CssPropertyNames.PaddingRight:
					_side = ElementSide.Right;
					break;

				case CssPropertyNames.PaddingBottom:
					_side = ElementSide.Bottom;
					break;
			}

		}
		/// <summary>
		/// Sets the property value(s) from the provided string.
		/// </summary>
		/// <param name="propValue">
		/// A string containing the property value.
		/// </param>
		private void SetValues(string? propValue)
		{
			_size?.Dispose();
			if (string.IsNullOrEmpty(propValue))
			{
				_size = null;
			}
			else
			{
				_size = FloatWithUnit.Parse(propValue);
			}
		}
		#endregion
	}
}
