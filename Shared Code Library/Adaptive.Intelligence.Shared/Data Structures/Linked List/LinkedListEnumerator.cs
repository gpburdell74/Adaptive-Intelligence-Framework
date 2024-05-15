using System.Collections;

namespace Adaptive.Intelligence.Shared
{
	/// <summary>
	/// Implements the <see cref="IEnumerator{T}"/> for the <see cref="LinkedList{T}"/>.
	/// </summary>
	/// <typeparam name="T">
	/// The data type contained by the list.
	/// </typeparam>
	/// <seealso cref="LinkedList{T}" />
	/// <seealso cref="IEnumerator{T}" />
	public sealed class LinkedListEnumerator<T> : IEnumerator<T>
	{
		#region Private Member Declarations
		/// <summary>
		/// The parent list reference.
		/// </summary>
		private LinkedList<T>? _parentList;
		/// <summary>
		/// The current node reference.
		/// </summary>
		private LinkedListNode<T>? _current;
		#endregion

		#region Constructor / Dispose Methods		
		/// <summary>
		/// Initializes a new instance of the <see cref="LinkedListEnumerator{T}"/> class.
		/// </summary>
		/// <param name="parentList">
		/// The reference to the parent <see cref="LinkedList{T}"/> instance to be enumerated.
		/// </param>
		public LinkedListEnumerator(LinkedList<T> parentList)
		{
			_parentList = parentList;
		}
		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			_parentList = null;
			_current = null;
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets the value for the current position in the enumeration.
		/// </summary>
		/// <value>
		/// The <typeparamref name="T"/> value at the current position, or <b>null</b>.
		/// </value>
		public T Current
		{
			get
			{
				if (_current == null)
					return default!;
				else
					return _current.Data!;
			}
		}
		/// <summary>
		/// Gets the value for the current position in the enumeration.
		/// </summary>
		/// <value>
		/// The <typeparamref name="T"/> value at the current position, or <b>null</b>.
		/// </value>
		object IEnumerator.Current => Current!;
		#endregion

		#region Private Methods / Functions		
		/// <summary>
		/// Advances the enumerator to the next element of the enumeration and returns a boolean indicating whether an element is available. Upon
		/// creation, an enumerator is conceptually positioned before the first element of the enumeration, and the first call to MoveNext
		/// brings the first element of the enumeration into view.
		/// </summary>
		/// <value>
		/// <b>true</b> if the element is available.
		/// </value>
		public bool MoveNext()
		{
			if (_current == null && _parentList != null && _parentList.Count > 0)
				_current = _parentList.FirstNode;
			else if (_current != null)
				_current = _current.Next;

			return (_current != null);
		}
		/// <summary>
		/// Resets the enumerator to the beginning of the enumeration, starting over.
		/// The preferred behavior for Reset is to return the exact same enumeration.
		/// This means if you modify the underlying collection then call Reset, your
		/// IEnumerator will be invalid, just as it would have been if you had called
		/// <see cref="MoveNext"/> or <see cref="Current"/>.
		/// </summary>
		public void Reset()
		{
			_current = null;
		}
		#endregion
	}
}