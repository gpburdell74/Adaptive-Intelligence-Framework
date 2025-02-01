namespace Adaptive.Intelligence.Shared
{
    /// <summary>
    /// Provides the definition for a node in a doubly-linked list.
    /// </summary>
    /// <typeparam name="T">
    /// The data type of the data content stored in the node.d
    /// </typeparam>
    /// <seealso cref="DisposableObjectBase" />
    public sealed class LinkedListNode<T> : DisposableObjectBase
    {
        #region Private Member Declarations		
        /// <summary>
        /// The previous node pointer.
        /// </summary>
        private LinkedListNode<T>? _prev;
        /// <summary>
        /// The next node pointer.
        /// </summary>
        private LinkedListNode<T>? _next;
        /// <summary>
        /// The value.
        /// </summary>
        private T? _value;
        #endregion

        #region Constructor / Dispose Methods		
        /// <summary>
        /// Initializes a new instance of the <see cref="LinkedListNode{T}"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public LinkedListNode()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="LinkedListNode{T}"/> class.
        /// </summary>
        /// <param name="data">
        /// The data to contain in the node.
        /// </param>
        public LinkedListNode(T data)
        {
            _value = data;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="LinkedListNode{T}"/> class.
        /// </summary>
        /// <param name="prevPtr">
        /// The reference to the previous node, or <b>null</b>.
        /// </param>
        /// <param name="nextPtr">
        /// The reference to the next node, or <b>null</b>.
        /// </param>
        /// <param name="data">
        /// The data to contain in the node.
        /// </param>
        public LinkedListNode(LinkedListNode<T> prevPtr, LinkedListNode<T> nextPtr, T data)
        {
            _value = data;
            _next = nextPtr;
            _prev = prevPtr;
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
        /// <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            _value = default!;
            _next = null;
            _prev = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties		
        /// <summary>
        /// Gets or sets the data value contained in the node.
        /// </summary>
        /// <value>
        /// The value of <typeparamref name="T"/>.
        /// </value>
        public T? Data
        {
            get => _value;
            set => _value = value;
        }
        /// <summary>
        /// Gets or sets the reference to the next node in the list.
        /// </summary>
        /// <value>
        /// The next <see cref="LinkedListNode{T}"/> or <b>null</b>.
        /// </value>
        public LinkedListNode<T>? Next
        {
            get => _next;
            set => _next = value;
        }
        /// <summary>
        /// Gets or sets the reference to the previous node in the list.
        /// </summary>
        /// <value>
        /// The previous <see cref="LinkedListNode{T}"/> or <b>null</b>.
        /// </value>
        public LinkedListNode<T>? Previous
        {
            get => _prev;
            set => _prev = value;
        }
        #endregion

        #region Public Methods / Functions		
        /// <summary>
        /// Determines whether the current instance contains the specified data value.
        /// </summary>
        /// <param name="value">
        /// The <typeparamref name="T"/> value to search for.
        /// </param>
        /// <returns>
        ///   <c>true</c> if the specified node has the data; otherwise, <c>false</c>.
        /// </returns>
        public bool HasData(T? value)
        {
            if (_value == null && value == null)
                return true;
            else if (_value != null)
                return _value.Equals(value);
            else
                return false;
        }
        #endregion
    }
}
