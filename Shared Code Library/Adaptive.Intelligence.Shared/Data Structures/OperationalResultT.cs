namespace Adaptive.Intelligence.Shared
{
    /// <summary>
    /// Represents and contains the result of an attempt to execute an operation.
    /// </summary>
    public sealed class OperationalResult<T> : OperationalResult, IOperationalResult<T>
    {
        #region Constructor(s)
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationalResult" /> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public OperationalResult()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationalResult"/> class.
        /// </summary>
        /// <param name="success">
        /// A value indicating whether the related operation was successful.
        /// </param>
        public OperationalResult(bool success) : base(success, null)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationalResult"/> class.
        /// </summary>
        /// <param name="success">
        /// A value indicating whether the related operation was successful.
        /// </param>
        /// <param name="message">
        /// A string containing a user message.
        /// </param>
        public OperationalResult(bool success, string message) : base(success, message)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationalResult"/> class.
        /// </summary>
        /// <remarks>
        /// This overload will set the <see cref="OperationalResult.Success"/> property to <b>false</b>.
        /// </remarks>
        /// <param name="ex">
        /// A reference to the <see cref="Exception"/> that was caught.
        /// </param>
        public OperationalResult(Exception ex) : base(ex, null)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationalResult"/> class.
        /// </summary>
        /// <remarks>
        /// This overload will set the <see cref="OperationalResult.Success"/> property to <b>false</b>.
        /// </remarks>
        /// <param name="ex">
        /// A reference to the <see cref="Exception"/> that was caught.
        /// </param>
        /// <param name="message">
        /// A string containing a user message.
        /// </param>
        public OperationalResult(Exception ex, string message) : base(ex, message)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationalResult"/> class.
        /// </summary>
        /// <remarks>
        /// This overload will set the <see cref="OperationalResult.Success"/> property to <b>true</b>.
        /// </remarks>
        /// <param name="dataContent">
        /// A reference to the object / data content stored as the result of an operation.
        /// </param>
        public OperationalResult(T? dataContent)
        {
            DataContent = dataContent;
            Success = true;
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
        /// <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            DataContent = default;
            base.Dispose(disposing);
        }
        #endregion

        /// <summary>
        /// Gets or sets the content of the data to be returned from a
        /// successful operation.
        /// </summary>
        /// <value>
        /// A reference to the data or instance to return from a successful
        /// operation.
        /// </value>
        public T? DataContent { get; set; }
    }
}