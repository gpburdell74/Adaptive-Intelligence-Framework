using Adaptive.Intelligence.Shared;
using System.Text;

namespace Adaptive.Intelligence.Css
{
	/// <summary>
	/// Represents and manages the properties for the text-shadow property.
	/// </summary>
	/// <seealso cref="DisposableObjectBase" />
	public sealed class CssTextShadow : DisposableObjectBase, ICssProperty
	{
		#region Private Member Declarations		
		/// <summary>
		/// The horizontal shadow units.
		/// </summary>
		private FloatWithUnit? _hShadow;
		/// <summary>
		/// The vertical shadow units.
		/// </summary>
		private FloatWithUnit? _vShadow;
		/// <summary>
		/// The blur radius.
		/// </summary>
		private float? _blurRadius;
		#endregion

		#region Constructor /Dispose Methods		
		/// <summary>
		/// Initializes a new instance of the <see cref="CssTextShadow"/> class.
		/// </summary>
		/// <remarks>
		/// This is the default constructor.
		/// </remarks>
		public CssTextShadow()
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="CssTextShadow"/> class.
		/// </summary>
		/// <param name="horizontalShadow">
		/// A <see cref="FloatWithUnit"/>instance specifying the horizontal shadow value and units.
		/// </param>
		/// <param name="verticalShadow">
		/// A <see cref="FloatWithUnit"/>instance specifying the vertical shadow value and units.
		/// </param>
		public CssTextShadow(FloatWithUnit? horizontalShadow, FloatWithUnit? verticalShadow)
		{
			_hShadow = horizontalShadow;
			_vShadow = verticalShadow;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="CssTextShadow"/> class.
		/// </summary>
		/// <param name="cssDefinition">
		/// A string containing the CSS definition for the property value.
		/// </param>
		public CssTextShadow(string cssDefinition)
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
            if ( !IsDisposed && disposing)
			{
				_hShadow?.Dispose();
				_vShadow?.Dispose();
            }

			_hShadow = null;
			_vShadow = null;
			Color = null;
			_blurRadius = null;
            base.Dispose(disposing);
		}

		#endregion

		#region Public Properties		
		/// <summary>
		/// Gets or sets the blur radius setting.
		/// </summary>
		/// <value>
		/// A string specifying the blur radius. The default value is zero (0), or <b>null</b> if not set.
		/// </value>
		public string? BlurRadious
		{
			get
			{
				if (_blurRadius == null)
					return null;
				else
					return _blurRadius.Value.ToString();
			}
			set
			{
				if (string.IsNullOrEmpty(value))
					_blurRadius = null;
				else
					_blurRadius = float.Parse(value);
			}

		}
		/// <summary>
		/// Gets or sets a value indicating whether the contents of the property can/should be rendered.
		/// </summary>
		/// <value>
		///   <c>true</c> if the property can/should be rendered; otherwise, <c>false</c>.
		/// </value>
		public bool CanRender => (_hShadow != null && _vShadow != null);
		/// <summary>
		/// Gets or sets the color of the shadow.
		/// </summary>
		/// <value>
		/// A string containing the color value, or <b>null</b> if not set.
		/// </value>
		public string? Color { get; set; }
		/// <summary>
		/// Gets or sets the size of the horizonal shadow.
		/// </summary>
		/// <value>
		/// A string specifying The position of the horizontal shadow. Negative values are allowed.
		/// </value>
		private string? Horizontal
		{
			get
			{
				if (_hShadow == null)
					return null;
				else
					return _hShadow.ToString();
			}
			set
			{
				_hShadow?.Dispose();
				if (value != null)
					_hShadow = FloatWithUnit.Parse(value);
			}
		}
		/// <summary>
		/// Gets or sets the size of the vertical shadow.
		/// </summary>
		/// <value>
		/// A string specifying The position of the vertical shadow. Negative values are allowed.
		/// </value>
		private string? Vertical
		{
			get
			{
				if (_vShadow == null)
					return null;
				else
					return _vShadow.ToString();
			}
			set
			{
				_vShadow?.Dispose();
				if (value != null)
					_vShadow = FloatWithUnit.Parse(value);
			}
		}
		#endregion

		#region Public Methods / Functions		
		/// <summary>
		/// Resets the content of the property and sets it to an "un-set" state.
		/// </summary>
		public void Clear()
		{
			_vShadow?.Dispose();
			_hShadow?.Dispose();
			_vShadow = null;
			_hShadow = null;
			_blurRadius = null;
			Color = null;
		}
		/// <summary>
		/// Parses the CSS content for the instance.
		/// </summary>
		/// <param name="cssDefinition">A string containing the raw CSS definition / code defining the item.</param>
		public void ParseCss(string? cssDefinition)
		{
			if (cssDefinition != null)
			{
				List<string> subItems = CssParsing.ParsePropertyValue(cssDefinition);

				if (subItems.Count >= 2)
				{
					// Horizontal.
					_hShadow = FloatWithUnit.Parse(subItems[0]);

					// Vertical.
					_vShadow = FloatWithUnit.Parse(subItems[1]);

					if (subItems.Count > 2)
					{
						if (CssParsing.IsUnit(subItems[2]))
						{
							_blurRadius = float.Parse(subItems[2]);
							if (subItems.Count > 3)
								Color = subItems[3];
						}
						else
							Color = subItems[2];
					}
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
				builder.Append(CssPropertyNames.FormatPropertyName(CssPropertyNames.TextShadow));

				builder.Append(_hShadow.ToString());
				builder.Append(CssLiterals.Space);
				builder.Append(_vShadow.ToString());

				if (_blurRadius != null)
				{
					builder.Append(CssLiterals.Space);
					builder.Append(_blurRadius.Value.ToString());
				}

				if (Color != null)
				{
					builder.Append(CssLiterals.Space);
					builder.Append(Color);
				}

				builder.Append(CssLiterals.CssTerminator);
			}
			return builder.ToString();
		}
		#endregion
	}
}
