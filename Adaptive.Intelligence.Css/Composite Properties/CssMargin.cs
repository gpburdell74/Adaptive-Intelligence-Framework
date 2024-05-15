using Adaptive.Intelligence.Shared;
using System.Text;

namespace Adaptive.Intelligence.Css
{
	/// <summary>
	/// Represents and manages the CSS "nargin" property.
	/// </summary>
	/// <seealso cref="DisposableObjectBase" />
	public sealed class CssMargin : DisposableObjectBase, ICssProperty
	{
		#region Private Member Declarations				
		/// <summary>
		/// The specification for all Margins.
		/// </summary>
		private CssMarginSide? _marginAll;
		/// <summary>
		/// The bottom Margin specification.
		/// </summary>
		private CssMarginSide? _marginBottom;
		/// <summary>
		/// The left Margin specification.
		/// </summary>
		private CssMarginSide? _marginLeft;
		/// <summary>
		/// The right Margin specification.
		/// </summary>
		private CssMarginSide? _marginRight;
		/// <summary>
		/// The top Margin specification.
		/// </summary>
		private CssMarginSide? _marginTop;
		#endregion

		#region Constructor / Dispose Methods		
		/// <summary>
		/// Initializes a new instance of the <see cref="CssMargin"/> class.
		/// </summary>
		/// <remarks>
		/// This is the default constructor.
		/// </remarks>
		public CssMargin()
		{
			_marginAll = new CssMarginSide(ElementSide.AllSides);
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="CssMargin"/> class.
		/// </summary>
		/// <param name="cssDefinition">
		/// A string containing the CSS definition.
		/// </param>
		public CssMargin(string? cssDefinition)
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
			Clear();
			base.Dispose(disposing);
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets or sets the specification for all Margins.
		/// </summary>
		/// <value>
		/// A string containing the CSS specification, or <b>null</b> if not set.
		/// </value>
		public CssMarginSide? All
		{
			get => _marginAll;
			set
			{
				_marginAll = value;
				if (value != null)
				{
					_marginLeft = null;
					_marginTop = null;
					_marginBottom = null;
					_marginRight = null;
				}
			}
		}
		/// <summary>
		/// Gets or sets the specification for the bottom Margin.
		/// </summary>
		/// <remarks>
		/// If this value is set, the <see cref="All"/> property is set to <b>null</b>.
		/// </remarks>
		/// <value>
		/// A string containing the CSS specification, or <b>null</b> if not set.
		/// </value>
		public CssMarginSide? Bottom
		{
			get => _marginBottom;
			set
			{
				_marginBottom = value;
				if (value != null)
					_marginAll = null;
			}
		}
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
				return ((_marginAll != null && _marginAll.CanRender) ||
					((_marginLeft != null && _marginLeft.CanRender) ||
					(_marginRight != null && _marginRight.CanRender) ||
					(_marginBottom != null && _marginBottom.CanRender) ||
					(_marginTop != null && _marginTop.CanRender)));
			}
		}
		/// <summary>
		/// Gets or sets the specification for the left Margin.
		/// </summary>
		/// <remarks>
		/// If this value is set, the <see cref="All"/> property is set to <b>null</b>.
		/// </remarks>
		/// <value>
		/// A string containing the CSS specification, or <b>null</b> if not set.
		/// </value>
		public CssMarginSide? Left
		{
			get => _marginLeft;
			set
			{
				_marginLeft = value;
				if (value != null)
					_marginAll = null;
			}
		}
		/// <summary>
		/// Gets or sets the specification for the right Margin.
		/// </summary>
		/// <remarks>
		/// If this value is set, the <see cref="All"/> property is set to <b>null</b>.
		/// </remarks>
		/// <value>
		/// A string containing the CSS specification, or <b>null</b> if not set.
		/// </value>
		public CssMarginSide? Right
		{
			get => _marginRight;
			set
			{
				_marginRight = value;
				if (value != null)
					_marginAll = null;
			}
		}
		/// <summary>
		/// Gets or sets the specification for the top Margin.
		/// </summary>
		/// <remarks>
		/// If this value is set, the <see cref="All"/> property is set to <b>null</b>.
		/// </remarks>
		/// <value>
		/// A string containing the CSS specification, or <b>null</b> if not set.
		/// </value>
		public CssMarginSide? Top
		{
			get => _marginTop;
			set
			{
				_marginTop = value;
				if (value != null)
					_marginAll = null;
			}
		}
		#endregion

		#region Public Methods / Functions		
		/// <summary>
		/// Clears this instance and resets it to the "unused" or "unset" state.
		/// </summary>
		public void Clear()
		{
			_marginTop?.Dispose();
			_marginLeft?.Dispose();
			_marginRight?.Dispose();
			_marginBottom?.Dispose();
			_marginAll?.Dispose();

			_marginTop = null;
			_marginLeft = null;
			_marginRight = null;
			_marginBottom = null;
			_marginAll = null;
		}
		/// <summary>
		/// Parses the CSS content for the instance.
		/// </summary>
		/// <param name="cssDefinition">
		/// A string containing the raw CSS definition / code defining the item.
		/// </param>
		public void ParseCss(string? cssDefinition)
		{
			Clear();

			if (!string.IsNullOrEmpty(cssDefinition))
			{
				int index = cssDefinition.IndexOf(":");
				if (index > -1)
				{
					string propName = cssDefinition.Substring(0, index).Trim();
					string propValue = cssDefinition.Substring(index + 1, cssDefinition.Length - (index + 1)).Trim();
					switch (propName)
					{
						case CssPropertyNames.Margin:
							_marginAll = new CssMarginSide(propValue);
							break;

						case CssPropertyNames.MarginTop:
							_marginTop = new CssMarginSide(propValue);
							break;

						case CssPropertyNames.MarginBottom:
							_marginBottom = new CssMarginSide(propValue);
							break;

						case CssPropertyNames.MarginLeft:
							_marginRight = new CssMarginSide(propValue);
							break;

						case CssPropertyNames.MarginRight:
							_marginLeft = new CssMarginSide(propValue);
							break;
					}
				}
			}
		}
		/// <summary>
		/// Sets the value of the CSS "Margin-bottom" property.
		/// </summary>
		/// <param name="cssDefinition">
		/// A string containing the CSS definition text, or <b>null</b> to "un-set" the property.
		/// </param>
		public void SetBottom(string? cssDefinition)
		{
			_marginBottom?.Dispose();
			_marginBottom = CssPropertyFactory.CreateMarginBottom(cssDefinition);
			if (_marginBottom != null)
			{
				// Remove the "all" setting if the a specific Margin side is set/specified.
				_marginAll?.Dispose();
				_marginAll = null;
			}
		}
		/// <summary>
		/// Sets the value of the CSS "Margin-left" property.
		/// </summary>
		/// <param name="cssDefinition">
		/// A string containing the CSS definition text, or <b>null</b> to "un-set" the property.
		/// </param>
		public void SetLeft(string? cssDefinition)
		{
			_marginLeft?.Dispose();
			_marginLeft = CssPropertyFactory.CreateMarginLeft(cssDefinition);
			if (_marginLeft != null)
			{
				// Remove the "all" setting if the a specific Margin side is set/specified.
				_marginAll?.Dispose();
				_marginAll = null;
			}
		}
		/// <summary>
		/// Sets the value of the CSS "Margin-right" property.
		/// </summary>
		/// <param name="cssDefinition">
		/// A string containing the CSS definition text, or <b>null</b> to "un-set" the property.
		/// </param>
		public void SetRight(string? cssDefinition)
		{
			_marginRight?.Dispose();
			_marginRight = CssPropertyFactory.CreateMarginRight(cssDefinition);
			if (_marginRight != null)
			{
				// Remove the "all" setting if the a specific Margin side is set/specified.
				_marginAll?.Dispose();
				_marginAll = null;
			}
		}
		/// <summary>
		/// Sets the value of the CSS "Margin-left" property.
		/// </summary>
		/// <param name="cssDefinition">
		/// A string containing the CSS definition text, or <b>null</b> to "un-set" the property.
		/// </param>
		public void SetTop(string? cssDefinition)
		{
			_marginTop?.Dispose();
			_marginTop = CssPropertyFactory.CreateMarginTop(cssDefinition);
			if (_marginTop != null)
			{
				// Remove the "all" setting if the a specific Margin side is set/specified.
				_marginAll?.Dispose();
				_marginAll = null;
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
			StringBuilder css = new StringBuilder();

			if (CanRender)
				{
				if (_marginAll != null)
				{
					css.Append(_marginAll.ToCss());
				}
				else
				{
					if (_marginLeft != null && _marginLeft.CanRender)
						css.Append(_marginLeft.ToCss());

					if (_marginRight != null && _marginRight.CanRender)
						css.Append(_marginRight.ToCss());

					if (_marginTop != null && _marginTop.CanRender)
						css.Append(_marginTop.ToCss());

					if (_marginBottom != null && _marginBottom.CanRender)
						css.Append(_marginBottom.ToCss());

				}
				css.Append(CssLiterals.CssTerminator);
			}
			return css.ToString();
		}
		#endregion
	}
}

