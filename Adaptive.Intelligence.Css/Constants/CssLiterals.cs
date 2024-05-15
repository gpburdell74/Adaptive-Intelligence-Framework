namespace Adaptive.Intelligence.Css
{
    /// <summary>
    /// Contains and provides the constants definitions for CSS literals and values.
    /// </summary>
    public static class CssLiterals
    {
		#region CSS Specification Characters        
		/// <summary>
		/// The CSS closing quote character.
		/// </summary>
		public const string CssCloseQuote = "'";
		/// <summary>
		/// The CSS open quote character.
		/// </summary>
		public const string CssOpenQuote = "'";
		/// <summary>
		/// The CSS separator character.
		/// </summary>
		public const string CssSeparator = ":";
        /// <summary>
        /// The CSS terminator character.
        /// </summary>
        public const string CssTerminator = ";";
        
		/// <summary>
		/// The CSS space character.
		/// </summary>
		public const string Space = " ";
        #endregion

        #region CSS Values		
        /// <summary>
        /// A CSS literal value.
        /// </summary>
        public const string ValueAbsolute = "absolute";
        /// <summary>
        /// A CSS literal value.
        /// </summary>
        public const string ValueBold = "bold";
        /// <summary>
        /// A CSS literal value.
        /// </summary>
        public const string ValueBolder = "bolder";
        /// <summary>
        /// A CSS literal value.
        /// </summary>
        public const string ValueCenter = "center";
        /// <summary>
        /// A CSS literal value.
        /// </summary>
        public const string ValueFixed = "fixed";
        /// <summary>
        /// A CSS literal value.
        /// </summary>
        public const string ValueItalic = "italic";
		/// <summary>
		/// A CSS literal value.
		/// </summary>
		public const string ValueHighQuality = "high-quality";
		/// <summary>
		/// A CSS literal value.
		/// </summary>
		public const string ValueJustify = "justify";
        /// <summary>
        /// A CSS literal value.
        /// </summary>
        public const string ValueLeft = "left";
        /// <summary>
        /// A CSS literal value.
        /// </summary>
        public const string ValueLighter = "lighter";
        /// <summary>
        /// A CSS literal value.
        /// </summary>
        public const string ValueNone = "none";
        /// <summary>
        /// A CSS literal value.
        /// </summary>
        public const string ValueNormal = "normal";
        /// <summary>
        /// A CSS literal value.
        /// </summary>
        public const string ValueOblique = "oblique";
        /// <summary>
        /// A CSS literal value.
        /// </summary>
        public const string ValueRelative = "relative";
        /// <summary>
        /// A CSS literal value.
        /// </summary>
        public const string ValueRight = "right";
        /// <summary>
        /// A CSS literal value.
        /// </summary>
        public const string ValueSmallCaps = "small-caps";
        /// <summary>
        /// A CSS literal value.
        /// </summary>
        public const string ValueStatic = "static";
        /// <summary>
        /// A CSS literal value.
        /// </summary>
        public const string ValueSticky = "sticky";
        /// <summary>
        /// A CSS literal value.
        /// </summary>
        public const string ValueBaseline = "baseline";
        /// <summary>
        /// A CSS literal value.
        /// </summary>
        public const string ValueSub = "sub";
        /// <summary>
        /// A CSS literal value.
        /// </summary>
        public const string ValueSuper = "super";
        /// <summary>
        /// A CSS literal value.
        /// </summary>
        public const string ValueTextTop = "text-top";
        /// <summary>
        /// A CSS literal value.
        /// </summary>
        public const string ValueTextBottom = "text-botton";
        /// <summary>
        /// A CSS literal value.
        /// </summary>
        public const string ValueMiddle = "middle";
        /// <summary>
        /// A CSS literal value.
        /// </summary>
        public const string ValueTop = "top";
        /// <summary>
        /// A CSS literal value.
        /// </summary>
        public const string ValueBottom = "bottom";
        /// <summary>
        /// A CSS literal value.
        /// </summary>
        public const string ValueInherit = "inherit";
        /// <summary>
        /// A CSS literal value.
        /// </summary>
        public const string ValueInitial = "initial";
        /// <summary>
        /// A CSS literal value.
        /// </summary>
        public const string ValueRevert = "revert";
        /// <summary>
        /// A CSS literal value.
        /// </summary>
        public const string ValueRevertLayer = "revert-layer";
        /// <summary>
        /// A CSS literal value.
        /// </summary>
        public const string ValueUnset = "unset";
        /// <summary>
        /// A CSS literal value.
        /// </summary>
        public const string Value100 = "100";
        /// <summary>
        /// A CSS literal value.
        /// </summary>
        public const string Value200 = "200";
        /// <summary>
        /// A CSS literal value.
        /// </summary>
        public const string Value300 = "300";
        /// <summary>
        /// A CSS literal value.
        /// </summary>
        public const string Value400 = "400";
        /// <summary>
        /// A CSS literal value.
        /// </summary>
        public const string Value500 = "500";
        /// <summary>
        /// A CSS literal value.
        /// </summary>
        public const string Value600 = "600";
        /// <summary>
        /// A CSS literal value.
        /// </summary>
        public const string Value700 = "700";
        /// <summary>
        /// A CSS literal value.
        /// </summary>
        public const string Value800 = "800";
        /// <summary>
        /// A CSS literal value.
        /// </summary>
        public const string Value900 = "900";
		#endregion

		#region Standard Property Definitions        
		/// <summary>
		/// Renders the "display: none" property setting for making controls invisible.
		/// </summary>
		public const string DisplayNone = CssPropertyNames.Display + CssSeparator + Space + ValueNone;
		/// <summary>
		/// Renders the "pointer: none" property setting for making controls "disabled" by removeing mouse-based events.
		/// </summary>
		public const string PointerEventsNone = CssPropertyNames.PointerEvents + CssSeparator + Space + ValueNone;
		#endregion
	}
}
