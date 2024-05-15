﻿using System.Collections.Specialized;

namespace Adaptive.Intelligence.Shared
{
	/// <summary>
	/// Provides a string collection implementation that contains only a list of unique string values.
	/// </summary>
	/// <seealso cref="StringCollection" />
	[Serializable]
	public sealed class UniqueStringCollection : StringCollection
	{
		/// <summary>
		/// Adds the specified string value to the end of the collection.
		/// </summary>
		/// <param name="newValue">
		/// A string containing the new value to be added.
		/// </param>
		/// <returns>
		/// An integer indicating the zero-based index at which the string is inserted.
		/// </returns>
		public new int Add(string? newValue)
		{
			int result = -1;
			if (newValue != null && IndexOf(newValue) == -1)
			{
				result = base.Add(newValue);
			}
			return result;
		}
		/// <summary>
		/// Adds the list of strings to the collection.
		/// </summary>
		/// <param name="items">
		/// An <see cref="IEnumerable{T}"/> of strings to add to the collection.. Duplicate string values
		/// will not be added.
		/// </param>
		public void AddRange(IEnumerable<string> items)
		{
			foreach (string current in items)
			{
				if (IndexOf(current) == -1)
				{
					Add(current);
				}
			}
		}
	}
}