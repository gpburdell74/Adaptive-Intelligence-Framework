namespace Adaptive.Intelligence.Shared
{
	/// <summary>
	/// Contains a list of the US State definitions.
	/// </summary>
	public sealed class USStateCollection : List<USState>, ICloneable
	{
		/// <summary>
		/// Gets the State by the postal abbreviation value.
		/// </summary>
		/// <param name="abbreviation">
		/// A string containing a standard 2-character postal abbreviation
		/// for the State.
		/// </param>
		/// <returns>
		/// A <see cref="USState"/> instance, if found; otherwise, returns
		/// <b>null</b>.
		/// </returns>
		public USState? GetStateByAbbreviation(string? abbreviation)
		{
			if (abbreviation == null)
				return null;
			else
			{
				IEnumerable<USState> query =
					from states in this
					where String.Compare(states.Abbreviation, abbreviation, StringComparison.OrdinalIgnoreCase) == 0
					select states;

				List<USState> list = query.ToList();
				if (list.Count == 0)
					return null;
				else
					return list[0];
			}
		}
		/// <summary>
		/// Creates a new object that is a copy of the current instance.
		/// </summary>
		/// <returns>
		/// A new object that is a copy of this instance.
		/// </returns>
		object ICloneable.Clone()
		{
			return Clone();
		}
		/// <summary>
		/// Creates a new object that is a copy of the current instance.
		/// </summary>
		/// <returns>
		/// A new <see cref="USStateCollection"/> that is a copy of this instance.
		/// </returns>
		public USStateCollection Clone()
		{
			USStateCollection collection = new USStateCollection();
			collection.AddRange(this);
			return collection;
		}
	}
}