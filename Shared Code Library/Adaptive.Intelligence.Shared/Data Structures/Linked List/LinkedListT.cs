using System.Collections;

namespace Adaptive.Intelligence.Shared
{
	/// <summary>
	/// Represents a doubly-linked list for containing a list of data.
	/// </summary>
	/// <seealso cref="DisposableObjectBase" />
	/// <typeparam name="T">
	/// The data type of the data contained in the list.
	/// </typeparam>
	public class LinkedList<T> : DisposableObjectBase, IList<T>
	{
		#region Private Member Declarations
		/// <summary>
		/// The reference to the first node in the list.
		/// </summary>
		private LinkedListNode<T>? _firstNode;
		/// <summary>
		/// The reference to the last node in the list.
		/// </summary>
		private LinkedListNode<T>? _lastNode;
		/// <summary>
		/// The count of items in the list.
		/// </summary>
		private int _count;
		#endregion

		#region Constructor / Dispose Methods
		/// <summary>
		/// Initializes a new instance of the <see cref="LinkedList{T}"/> class.
		/// </summary>
		/// <remarks>
		/// This is the default constructor.
		/// </remarks>
		public LinkedList()
		{
			_firstNode = null;
			_lastNode = null;
			_count = 0;
		}
		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
		/// <b>false</b> to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			Clear();
			_firstNode = null;
			_lastNode = null;
			_count = 0;
			base.Dispose(disposing);
		}
		#endregion

		#region Public Properties		
		/// <summary>
		/// Gets the number of elements contained in the <see cref="LinkedList{T}" />.
		/// </summary>
		/// <value>
		/// An integer indicating the number of items in the list.
		/// </value>
		public int Count => _count;
		/// <summary>
		/// Gets the reference to the first node in the list.
		/// </summary>
		/// <value>
		/// The first <see cref="LinkedListNode{T}"/> in the list, or <b>null</b>.
		/// </value>
		public LinkedListNode<T>? FirstNode => _firstNode;
		/// <summary>
		/// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.
		/// </summary>
		/// <value>
		/// Always returns <b>false</b>.
		/// </value>
		public bool IsReadOnly => false;
		/// <summary>
		/// Gets the reference to the last node in the list.
		/// </summary>
		/// <value>
		/// The last <see cref="LinkedListNode{T}"/> in the list, or <b>null</b>.
		/// </value>
		public LinkedListNode<T>? LastNode => _lastNode;
		/// <summary>
		/// Gets or sets the data of <typeparamref name="T"/> at the specified index in the list.
		/// </summary>
		/// <param name="index">
		/// An integer specifying the ordinal index of the item to find.
		/// </param>
		/// <returns>
		/// The value of <typeparamref name="T"/> at the specified location, if found;
		/// otherwise, returns the default value for <typeparamref name="T"/>.
		/// </returns>
		public T this[int index]
		{
			get
			{
				T? returnData = default;

				LinkedListNode<T>? node = FindNodeAtIndex(index);
				if (node != null)
					returnData = node.Data;

				return returnData!;
			}
			set
			{
				LinkedListNode<T>? node = FindNodeAtIndex(index);
				if (node != null)
				{
					node.Data = value;
				}
			}
		}
		#endregion

		#region Public Methods / Functions
		/// <summary>
		/// Adds the new value item to the list.
		/// </summary>
		/// <param name="newValue">
		/// A value of <typeparamref name="T"/> containing new value to add to the end of the list.
		/// </param>
		public void Add(T newValue)
		{
			if (_firstNode == null)
			{
				_firstNode = new LinkedListNode<T>(newValue);
				_lastNode = _firstNode;
				_count = 1;
			}
			else
			{
				LinkedListNode<T> newNode = new LinkedListNode<T>(newValue);
				newNode.Previous = _lastNode;
				if (_lastNode != null)
					_lastNode.Next = newNode;
				_lastNode = newNode;
				_count++;
			}
		}
		/// <summary>
		/// Clears the list and disposes of all nodes.
		/// </summary>
		public void Clear()
		{
			LinkedListNode<T>? ptr = _lastNode;

			while (ptr != null)
			{
				LinkedListNode<T> node = ptr;
				ptr = ptr.Previous;
				node.Dispose();
			}

			_firstNode = null;
			_lastNode = null;
			_count = 0;
		}
		/// <summary>
		/// Determines whether this instance contains the object.
		/// </summary>
		/// <param name="item">
		/// The object to locate in the <see cref="LinkedList{T}" />.
		/// </param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="item" /> is found in the <see cref="LinkedList{T}" />; otherwise, <see langword="false" />.
		/// </returns>
		public bool Contains(T item)
		{
			int pos = FindIndexOf(item);
			return (pos >= 0);
		}
		/// <summary>
		/// Copies the elements of the <see cref="LinkedList{T}" /> to an <see cref="System.Array" />, starting at a particular
		/// <see cref="System.Array" /> index.
		/// </summary>
		/// <param name="array">
		/// The one-dimensional <see cref="System.Array" /> that is the destination of the elements copied from
		/// the <see cref="LinkedList{T}" />. The <see cref="System.Array" /> must have zero-based indexing.
		/// </param>
		/// <param name="arrayIndex">
		/// The zero-based index in <paramref name="array" /> at which copying begins.
		/// </param>
		public void CopyTo(T[] array, int arrayIndex)
		{
			LinkedListNode<T>? ptr = _firstNode;
			int count = 0;

			while (ptr != null)
			{
				array[count + arrayIndex] = ptr.Data!;
				ptr = ptr.Next;
				count++;
			}
		}
		/// <summary>
		/// Determines the index of a specific item in the <see cref="LinkedList{T}" />.
		/// </summary>
		/// <param name="item">The object to locate in the <see cref="LinkedList{T}" />.</param>
		/// <returns>
		/// The index of <paramref name="item" /> if found in the list; otherwise, -1.
		/// </returns>
		public int IndexOf(T item)
		{
			return FindIndexOf(item);
		}
		/// <summary>
		/// Inserts an item to the <see cref="LinkedList{T}" /> at the specified index.
		/// </summary>
		/// <param name="index">
		/// The zero-based index at which <paramref name="item" /> should be inserted.
		/// </param>
		/// <param name="item">
		/// The object to insert into the <see cref="LinkedList{T}" />.
		/// </param>
		public void Insert(int index, T item)
		{
			LinkedListNode<T>? indexNode = FindNodeAtIndex(index);
			if (indexNode != null)
			{
				LinkedListNode<T> newNode = new LinkedListNode<T>(item);
				LinkedListNode<T>? prevNode = indexNode.Previous;
				if (indexNode == _firstNode)
				{
					_firstNode = newNode;
					_firstNode.Next = indexNode;
				}
				else
				{
					if (prevNode != null)
						prevNode.Next = newNode;

					indexNode.Previous = newNode;
					newNode.Previous = prevNode;
					newNode.Next = indexNode;
				}

				_count++;
			}
		}
		/// <summary>
		/// Removes the <see cref="LinkedListNode{T}" /> item at the specified index.
		/// </summary>
		/// <param name="index">
		/// The zero-based index of the item to remove.
		/// </param>
		public void RemoveAt(int index)
		{
			LinkedListNode<T>? nodeToRemove = FindNodeAtIndex(index);
			if (nodeToRemove != null)
			{
				bool success = RemoveNode(nodeToRemove);
				if (success)
					nodeToRemove.Dispose();
			}
		}
		/// <summary>
		/// Removes the first occurrence of a specific object from the <see cref="LinkedListNode{T}" />.
		/// </summary>
		/// <param name="item">
		/// The object to remove from the <see cref="LinkedList{T}" />.
		/// </param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="item" /> was successfully removed from
		/// the <see cref="LinkedList{T}" />; otherwise, <see langword="false" />. This method also returns <see langword="false" /> if
		/// <paramref name="item" /> is not found in the original <see cref="LinkedList{T}" />.
		/// </returns>
		public bool Remove(T item)
		{
			bool success = false;
			LinkedListNode<T>? nodeToRemove = FindNodeFor(item);
			if (nodeToRemove != null)
			{
				success = RemoveNode(nodeToRemove);
				if (success)
					nodeToRemove.Dispose();
			}

			return success;
		}
		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		/// A <see cref="LinkedListEnumerator{T}"/> enumerator that can be used to iterate through the collection.
		/// </returns>
		public IEnumerator<T> GetEnumerator()
		{
			return new LinkedListEnumerator<T>(this);
		}
		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		/// A <see cref="LinkedListEnumerator{T}"/> enumerator that can be used to iterate through the collection.
		/// </returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new LinkedListEnumerator<T>(this);
		}
		#endregion

		#region Private Methods / Functions
		/// <summary>
		/// Finds the ordinal index of the value within the list.
		/// </summary>
		/// <param name="value">
		/// The <typeparamref name="T"/> value to be found.
		/// </param>
		/// <returns>
		/// The ordinal index of the specified value, or -1 if not found.
		/// </returns>
		private int FindIndexOf(T value)
		{
			int pos = -1;

			if (_firstNode != null)
			{
				LinkedListNode<T>? ptr = _firstNode;
				pos = 0;
				while (ptr != null && !ptr.HasData(value))
				{
					ptr = ptr.Next;
					pos++;
				}

				if (ptr == null)
					pos = -1;
			}

			return pos;
		}
		/// <summary>
		/// Finds the first node in the list that contains the specified value.
		/// </summary>
		/// <param name="value">
		/// The <typeparamref name="T"/> value to be found.
		/// </param>
		/// <returns>
		/// The first <see cref="LinkedListNode{T}"/> containing the value, or <b>null</b> if not found.
		/// </returns>
		private LinkedListNode<T>? FindNodeFor(T value)
		{
			LinkedListNode<T>? ptr = _firstNode;

			while (ptr != null && !ptr.HasData(value))
			{
				ptr = ptr.Next;
			}

			return ptr;
		}
		/// <summary>
		/// Gets the node from the list at the specified ordinal index.
		/// </summary>
		/// <param name="index">
		/// An integer specifying the ordinal index.
		/// </param>
		/// <returns>
		/// The <see cref="LinkedListNode{T}"/> at the specified index, or <b>null</b> if not present.
		/// </returns>
		private LinkedListNode<T>? FindNodeAtIndex(int index)
		{
			LinkedListNode<T>? ptr = _firstNode;
			int pos = 0;

			while (ptr != null)
			{
				if (pos < index)
				{
					ptr = ptr.Next;
					pos++;
				}
			}

			return ptr;
		}
		/// <summary>
		/// Removes the specified node from the list.
		/// </summary>
		/// <param name="nodeToRemove">
		/// The reference to the <see cref="LinkedListNode{T}"/> node to remove from the list.
		/// </param>
		/// <returns>
		/// <b>true</b> if the operation is successful; otherwise, returns <b>false</b>.
		/// </returns>
		private bool RemoveNode(LinkedListNode<T>? nodeToRemove)
		{
			bool success = false;

			if (nodeToRemove != null)
			{
				LinkedListNode<T>? prevNode = nodeToRemove.Previous;
				LinkedListNode<T>? nextNode = nodeToRemove.Next;

				if (nodeToRemove == _firstNode)
					_firstNode = nextNode;
				if (nodeToRemove == _lastNode)
					_lastNode = prevNode;

				if (prevNode != null)
					prevNode.Next = nextNode;
				if (nextNode != null)
					nextNode.Previous = prevNode;

				_count--;
				success = true;
			}

			return success;
		}
		#endregion
	}
}
