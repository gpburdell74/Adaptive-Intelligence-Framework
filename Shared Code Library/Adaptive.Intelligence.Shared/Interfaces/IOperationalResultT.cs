namespace Adaptive.Intelligence.Shared
{
    /// <summary>
    /// Provides the signature definition for an instance that contains the result of an attempt to execute an operation
    /// that returns a specific data type as a successful result.
    /// </summary>
    /// <typeparam name="T">
    /// The data type of the data content to be returned.
    /// </typeparam>
    /// <seealso cref="IOperationalResult" />
    public interface IOperationalResult<T> : IOperationalResult
    {
        /// <summary>
        /// Gets or sets the content of the data to be returned from a
        /// successful operation.
        /// </summary>
        /// <value>
        /// A reference to the data or instance to return from a successful
        /// operation.
        /// </value>
        T? DataContent { get; set; }
    }
}