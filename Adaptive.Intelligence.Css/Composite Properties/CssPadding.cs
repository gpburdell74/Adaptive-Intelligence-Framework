using Adaptive.Intelligence.Shared;
using System.Text;

namespace Adaptive.Intelligence.Css
{
	/// <summary>
	/// Represents and manages the CSS "Padding" property.
	/// </summary>
	/// <seealso cref="DisposableObjectBase" />
	public sealed class CssPadding : DisposableObjectBase, ICssProperty
	{
		#region Private Member Declarations				
		/// <summary>
		/// The specification for all Paddings.
		/// </summary>
		private CssPaddingSide? _paddingAll;
		/// <summary>
		/// The bottom Padding specification.
		/// </summary>
		private CssPaddingSide? _paddingBottom;
		/// <summary>
		/// The left Padding specification.
		/// </summary>
		private CssPaddingSide? _paddingLeft;
		/// <summary>
		/// The right Padding specification.
		/// </summary>
		private CssPaddingSide? _paddingRight;
		/// <summary>
		/// The top Padding specification.
		/// </summary>
		private CssPaddingSide? _paddingTop;
		#endregion

		#region Constructor / Dispose Methods		
		/// <summary>
		/// Initializes a new instance of the <see cref="CssPadding"/> class.
		/// </summary>
		/// <remarks>
		/// This is the default constructor.
		/// </remarks>
		public CssPadding()
		{
			_paddingAll = new CssPaddingSide(ElementSide.AllSides);
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="CssPadding"/> class.
		/// </summary>
		/// <param name="cssDefinition">
		/// A string containing the CSS definition.
		/// </param>
		public CssPadding(string cssDefinition)
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
		/// Gets or sets the specification for all Paddings.
		/// </summary>
		/// <value>
		/// A string containing the CSS specification, or <b>null</b> if not set.
		/// </value>
		public CssPaddingSide? All
		{
			get => _paddingAll;
			set
			{
				_paddingAll = value;
				if (value != null)
				{
					_paddingLeft = null;
					_paddingTop = null;
					_paddingBottom = null;
					_paddingRight = null;
				}
			}
		}
		/// <summary>
		/// Gets or sets the specification for the bottom Padding.
		/// </summary>
		/// <remarks>
		/// If this value is set, the <see cref="All"/> property is set to <b>null</b>.
		/// </remarks>
		/// <value>
		/// A string containing the CSS specification, or <b>null</b> if not set.
		/// </value>
		public CssPaddingSide? Bottom
		{
			get => _paddingBottom;
			set
			{
				_paddingBottom = value;
				if (value != null)
					_paddingAll = null;
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
				return ((_paddingAll != null && _paddingAll.CanRender) ||
					(_paddingLeft != null && _paddingLeft.CanRender) ||
					(_paddingRight != null && _paddingRight.CanRender) ||
					(_paddingBottom != null && _paddingBottom.CanRender) ||
					(_paddingTop != null && _paddingTop.CanRender));
			}
		}
		/// <summary>
		/// Gets or sets the specification for the left Padding.
		/// </summary>
		/// <remarks>
		/// If this value is set, the <see cref="All"/> property is set to <b>null</b>.
		/// </remarks>
		/// <value>
		/// A string containing the CSS specification, or <b>null</b> if not set.
		/// </value>
		public CssPaddingSide? Left
		{
			get => _paddingLeft;
			set
			{
				_paddingLeft = value;
				if (value != null)
					_paddingAll = null;
			}
		}
		/// <summary>
		/// Gets or sets the specification for the right Padding.
		/// </summary>
		/// <remarks>
		/// If this value is set, the <see cref="All"/> property is set to <b>null</b>.
		/// </remarks>
		/// <value>
		/// A string containing the CSS specification, or <b>null</b> if not set.
		/// </value>
		public CssPaddingSide? Right
		{
			get => _paddingRight;
			set
			{
				_paddingRight = value;
				if (value != null)
					_paddingAll = null;
			}
		}
		/// <summary>
		/// Gets or sets the specification for the top Padding.
		/// </summary>
		/// <remarks>
		/// If this value is set, the <see cref="All"/> property is set to <b>null</b>.
		/// </remarks>
		/// <value>
		/// A string containing the CSS specification, or <b>null</b> if not set.
		/// </value>
		public CssPaddingSide? Top
		{
			get => _paddingTop;
			set
			{
				_paddingTop = value;
				if (value != null)
					_paddingAll = null;
			}
		}
		#endregion

		#region Public Methods / Functions		
		/// <summary>
		/// Clears this instance and resets it to the "unused" or "unset" state.
		/// </summary>
		public void Clear()
		{
			_paddingTop?.Dispose();
			_paddingLeft?.Dispose();
			_paddingRight?.Dispose();
			_paddingBottom?.Dispose();
			_paddingAll?.Dispose();

			_paddingTop = null;
			_paddingLeft = null;
			_paddingRight = null;
			_paddingBottom = null;
			_paddingAll = null;
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
						case CssPropertyNames.Padding:
							_paddingAll = new CssPaddingSide(propValue);
							break;

						case CssPropertyNames.PaddingTop:
							_paddingTop = new CssPaddingSide(propValue);
							break;

						case CssPropertyNames.PaddingBottom:
							_paddingBottom = new CssPaddingSide(propValue);
							break;

						case CssPropertyNames.PaddingLeft:
							_paddingRight = new CssPaddingSide(propValue);
							break;

						case CssPropertyNames.PaddingRight:
							_paddingLeft = new CssPaddingSide(propValue);
							break;
					}
				}
			}
		}
		/// <summary>
		/// Sets the value of the CSS "Padding-bottom" property.
		/// </summary>
		/// <param name="cssDefinition">
		/// A string containing the CSS definition text, or <b>null</b> to "un-set" the property.
		/// </param>
		public void SetBottom(string? cssDefinition)
		{
			_paddingBottom?.Dispose();
			_paddingBottom = CssPropertyFactory.CreatePaddingBottom(cssDefinition);
			if (_paddingBottom != null)
			{
				// Remove the "all" setting if the a specific Padding side is set/specified.
				_paddingAll?.Dispose();
				_paddingAll = null;
			}
		}
		/// <summary>
		/// Sets the value of the CSS "Padding-left" property.
		/// </summary>
		/// <param name="cssDefinition">
		/// A string containing the CSS definition text, or <b>null</b> to "un-set" the property.
		/// </param>
		public void SetLeft(string? cssDefinition)
		{
			_paddingLeft?.Dispose();
			_paddingLeft = CssPropertyFactory.CreatePaddingLeft(cssDefinition);
			if (_paddingLeft != null)
			{
				// Remove the "all" setting if the a specific Padding side is set/specified.
				_paddingAll?.Dispose();
				_paddingAll = null;
			}
		}
		/// <summary>
		/// Sets the value of the CSS "Padding-right" property.
		/// </summary>
		/// <param name="cssDefinition">
		/// A string containing the CSS definition text, or <b>null</b> to "un-set" the property.
		/// </param>
		public void SetRight(string? cssDefinition)
		{
			_paddingRight?.Dispose();
			_paddingRight = CssPropertyFactory.CreatePaddingRight(cssDefinition);
			if (_paddingRight != null)
			{
				// Remove the "all" setting if the a specific Padding side is set/specified.
				_paddingAll?.Dispose();
				_paddingAll = null;
			}
		}
		/// <summary>
		/// Sets the value of the CSS "Padding-left" property.
		/// </summary>
		/// <param name="cssDefinition">
		/// A string containing the CSS definition text, or <b>null</b> to "un-set" the property.
		/// </param>
		public void SetTop(string? cssDefinition)
		{
			_paddingTop?.Dispose();
			_paddingTop = CssPropertyFactory.CreatePaddingTop(cssDefinition);
			if (_paddingTop != null)
			{
				// Remove the "all" setting if the a specific Padding side is set/specified.
				_paddingAll?.Dispose();
				_paddingAll = null;
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

			if (_paddingAll != null)
			{
				css.Append(_paddingAll.ToCss());
			}
			else
			{
				if (_paddingLeft != null && _paddingLeft.CanRender)
					css.Append(_paddingLeft.ToCss());

				if (_paddingRight != null && _paddingRight.CanRender)
					css.Append(_paddingRight.ToCss());

				if (_paddingTop != null && _paddingTop.CanRender)
					css.Append(_paddingTop.ToCss());

				if (_paddingBottom != null && _paddingBottom.CanRender)
					css.Append(_paddingBottom.ToCss());

			}

			return css.ToString();
		}
		#endregion
	}
}

