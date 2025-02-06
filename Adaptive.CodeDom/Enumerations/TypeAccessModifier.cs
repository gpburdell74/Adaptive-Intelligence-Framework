namespace Adaptive.CodeDom
{
    /// <summary>
    /// Lists the types of access modifiers that are currently supported.
    /// </summary>
    public enum TypeAccessModifier
    {
        /// <summary>
        /// Indicates the access modified is not specified / generated.
        /// </summary>
        NotSpecified = 0,
        /// <summary>
        /// Indicates the "public" access modifier.
        /// </summary>
        Public = 1,
        /// <summary>
        /// Indicates the "protected" access modifier.
        /// </summary>
        Protected = 2,
        /// <summary>
        /// Indicates the "internal" access modifier ("Friend" in Visual Basic).
        /// </summary>
        Internal = 3,
        /// <summary>
        /// Indicates the "private" access modifier.
        /// </summary>
        Private = 4
    }
}
