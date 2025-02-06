﻿namespace Adaptive.Intelligence.Shared
{
    /// <summary>
    /// Represents and contains the result of an attempt to execute an operation.
    /// </summary>
    public class OperationalResult : DisposableObjectBase, IOperationalResult
    {
        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationalResult" /> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public OperationalResult()
        {
            Exceptions = new ExceptionCollection();
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
        public OperationalResult(bool success, string? message = null) : this()
        {
            Success = success;
            Message = message;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationalResult"/> class.
        /// </summary>
        /// <remarks>
        /// This overload will set the <see cref="Success"/> property to <b>false</b>.
        /// </remarks>
        /// <param name="ex">
        /// A reference to the <see cref="Exception"/> that was caught.
        /// </param>
        /// <param name="message">
        /// A string containing a user message.
        /// </param>
        public OperationalResult(Exception ex, string? message = null) : this()
        {
            Success = false;
            Message = message;
            Exceptions?.Add(ex);
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
        /// <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed && disposing)
            {
                Exceptions?.Clear();
            }

            Exceptions = null;
            Message = null;
            base.Dispose(disposing);
        }
        #endregion

        /// <summary>
        /// Gets the reference to the list of exceptions encountered during the operation.
        /// </summary>
        /// <value>
        /// A <see cref="List{T}"/> of <see cref="Exception"/> instances.
        /// </value>
        public ExceptionCollection? Exceptions { get; private set; }
        /// <summary>
        /// Gets the reference to the first exception in the list, if any are present.
        /// </summary>
        /// <value>
        /// The reference to the first <see cref="Exception" /> in the <see cref="Exceptions" /> list, if
        /// the list is not null or empty.
        /// </value>
        public Exception? FirstException
        {
            get
            {
                if (ListExtensions.IsNullOrEmpty(Exceptions))
                    return null;
                else
                    return Exceptions![0];
            }
        }
        /// <summary>
        /// Gets a value indicating whether this instance has exceptions.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has exceptions; otherwise, <c>false</c>.
        /// </value>
        public bool HasExceptions => Exceptions != null && Exceptions.Count > 0;
        /// <summary>
		/// Gets or sets the message text.
		/// </summary>
		/// <value>
		/// A string containing a user message.
		/// </value>
		public string? Message { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the related operation
        /// was successful.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the related operation was successful; otherwise, <c>false</c>.
        /// </value>
        public bool Success { get; set; }

        #region Public Methods / Functions        
        /// <summary>
        /// Adds exception instance to the current instance's list of exceptions.
        /// </summary>
        /// <param name="exception">
        /// The <see cref="Exception"/> instance to add to the current instance's exceptions list.
        /// </param>
        public void AddException(Exception? exception)
        {
            if (exception != null)
            {
                Exceptions ??= new ExceptionCollection();
                Exceptions.Add(exception);
            }
        }
        /// <summary>
        /// Adds the list of exceptions to the current instance's list of exceptions.
        /// </summary>
        /// <param name="exceptions">
        /// An <see cref="IEnumerable{T}"/> list of <see cref="Exception"/> instances to add to the current
        /// instance's list.
        /// </param>
        public void AddExceptions(IEnumerable<Exception>? exceptions)
        {
            if (exceptions != null)
            {
                Exceptions ??= new ExceptionCollection();
                Exceptions.AddRange(exceptions);
            }
        }
        /// <summary>
        /// Copies the data in the current instance to the provided instance.
        /// </summary>
        /// <param name="newResult">
        /// The <see cref="OperationalResult"/> to copy the field values to.
        /// </param>
        public void CopyTo(IOperationalResult? newResult)
        {
            if (newResult != null)
            {
                newResult.Message = Message;
                newResult.Success = Success;
                if (Exceptions != null && Exceptions.Count > 0)
                    newResult.Exceptions?.AddRange(Exceptions);
            }
        }
        #endregion
    }
}