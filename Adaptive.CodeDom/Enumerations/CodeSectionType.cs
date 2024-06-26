namespace Adaptive.CodeDom
{
	/// <summary>
	/// Lists the code section types that are currently supported.
	/// </summary>
	public enum CodeSectionType
	{
		/// <summary>
		/// Indicates a user-defined custom code section.
		/// </summary>
		Custom = 0,
		/// <summary>
		/// Indicates a code section for public event declarations.
		/// </summary>
		PublicEvents = 1,
		/// <summary>
		/// Indicates a code section for private constants declarations.
		/// </summary>
		PrivateConstants = 2,
		/// <summary>
		/// Indicates a code section for private member declarations.
		/// </summary>
		PrivateMembers = 3,
		/// <summary>
		/// Indicates a code section for constructor, destructor, and Dispose method declarations.
		/// </summary>
		ConstructorDispose = 4,
		/// <summary>
		/// Indicates a code section for public property declarations.
		/// </summary>
		PublicProperties = 5,
		/// <summary>
		/// Indicates a code section for protected property declarations.
		/// </summary>
		ProtectedProperties = 6,
		/// <summary>
		/// Indicates a code section for abstract method declarations.
		/// </summary>
		AbstractMethods = 7,
		/// <summary>
		/// Indicates a code section for protected method declarations.
		/// </summary>
		ProtectedMethods = 8,
		/// <summary>
		/// Indicates a code section for public method declarations.
		/// </summary>
		PublicMethods = 9,
		/// <summary>
		/// Indicates a code section for private method declarations.
		/// </summary>
		PrivateMethods = 10,
		/// <summary>
		/// Indicates a code section for event handler method declarations.
		/// </summary>
		EventHandlers = 11,
		/// <summary>
		/// Indicates a code section for event-raising method declarations.
		/// </summary>
		EventMethods = 12
	}
}

