namespace Adaptive.CodeDom
{
    /// <summary>
    /// Lists the types of XML comments that can be generated.
    /// </summary>
    public enum XmlCommentType
    {
        /// <summary>
        /// Indicates no comment type.
        /// </summary>
        None = 0,
        /// <summary>
        /// Indicates the XML summary comment type.
        /// </summary>
        Summary = 1,
        /// <summary>
        /// Indicates the XML remarks comment type.
        /// </summary>
        Remarks = 2,
        /// <summary>
        /// Indicates the XML returns comment type.
        /// </summary>
        Returns = 3,
        /// <summary>
        /// Indicates the XML value comment type.
        /// </summary>
        Value = 4,
        /// <summary>
        /// Indicates the XML parameter comment type.
        /// </summary>
        Parameter = 5
    }
}
